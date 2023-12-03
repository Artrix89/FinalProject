using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace FinalProject
{
    internal class Match
    {
        #region variables
        private Team _homeTeam, pitchingTeam;
        private Team _awayTeam, battingTeam;
        private int _strikes = 0;
        private int _balls = 0;
        private int _outs = 0;
        private int _inning = 1;
        private bool _isBottomInning = false;
        private static Random randomNum = new Random();
        private Batter[] _onBase = new Batter[4];
        private MainWindow UI;
        private Timer timer = new Timer(2000);
        private Batter currentBatter;
        private Pitcher currentPitcher;
        private int nextStep;
        #endregion

        #region constructors
        public Match( MainWindow ui )
        {
            _homeTeam = new Team();
            _awayTeam = new Team();

            UI = ui;
            UI.UpdateScore(_homeTeam.score, _awayTeam.score);
            UI.ChangeHalf(inning, _isBottomInning);

            nextStep = 0;
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        #endregion

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            //Should already have 0 queued
            switch (nextStep)
            {
                case 0: //Every Inning Change, Queues 1 or Ends Game depending on innings
                    ChangeHalf();
                    break;

                case 1: //Every Beginning At Bat, Queues result of the next pitch
                    ChangeBatter();
                    break;

                case 2: //Every Strike, Queues result of next pitch or 4 depending on strikes
                    GetStrike();
                    break;

                case 3: //Every Ball, Queues result of next pitch or 4 depending on balls
                    GetBall();
                    break;

                case 4: //Every End At Bat without a hit, Queues 1 or 0 depending on outs
                    EndAtBat();
                    break;

                case 5: //Every hit ball, Queues 1 or 0 depending on outs
                    GetHitBall();
                    break;

                default: 
                    UI.WriteToLog("Something went wrong, last code was " +  nextStep);
                    break;

            }
        }

        #region properties
        public string homeTeam { get { return _homeTeam.teamName; } }
        public string awayTeam { get { return _awayTeam.teamName; } }
        public Batter homeBatter { get { return _homeTeam.GetCurrentBatter(); } }
        public Batter awayBatter { get { return _awayTeam.GetCurrentBatter(); } }
        public Pitcher homePitcher { get { return _homeTeam.GetNextPitcher(); } }
        public Pitcher awayPitcher { get { return _awayTeam.GetNextPitcher(); } }
        public int strikes { get { return _strikes; } set { _strikes = value; } }
        public int balls { get { return _balls; } set { _balls = value; } }
        public int outs { get { return _outs; } set { _outs = value; } }
        public string inning {
            get 
            {
                if (_inning == 1)
                    return "1st";
                if (_inning == 2)
                    return "2nd";
                if (_inning == 3)
                    return "3rd";
                else
                    return _inning + "th";
            }
            }
        public bool isBottomInning { 
            get
            {
                _isBottomInning = !_isBottomInning;
                return _isBottomInning;
            } 
        }
        #endregion

        #region simulation methods
        private void ChangeHalf( ) //timer code 0
        {
            if (_inning > 9)
            {
                EndGame();
                return;
            }

            outs = 0;
            _onBase[1] = null;
            _onBase[2] = null;
            _onBase[3] = null;

            if (isBottomInning)
            {
                battingTeam = _homeTeam;
                pitchingTeam = _awayTeam;
                UI.WriteToLog("Bottom of the " + inning + ", " + homeTeam + " at bat");
                _inning += 1;
            }
            else
            {
                pitchingTeam = _homeTeam;
                battingTeam = _awayTeam;
                UI.WriteToLog("Top of the " + inning + ", " + awayTeam + " at bat");
            }

            UI.ChangeHalf(inning, _isBottomInning);
            nextStep = 1;

        }

        private void ChangeBatter() //timer code 1
        {
            currentBatter = battingTeam.GetCurrentBatter();
            currentPitcher = pitchingTeam.GetCurrentPitcher();
            balls = 0;
            strikes = 0;

            UI.WriteToLog(currentBatter.name + " at bat for the " + battingTeam.teamName + " against " +
                currentPitcher.name);

            SimulatePitch();
        }

        private void SimulatePitch()
        {
            double pitch = GetRandomDouble();
            if (pitch + .9 < currentBatter.battingPercentage) //Hit + .1
            {
                Console.WriteLine("Next step is Hit");
                nextStep = 5;
            }

            else if (pitch + .9 < currentBatter.battingPercentage) //Ball - .2
            {
                Console.WriteLine("Next step is Ball");
                nextStep = 3;
            }

            else //Strike
            {
                Console.WriteLine("Next step is Strike");
                nextStep = 2;
            }
        }

        private void GetStrike() //timer code 2
        {
            strikes++;
            UI.WriteToLog("Strike, " + balls + "-" + strikes);

            if (strikes >= 3)
                nextStep = 4;
            else
                SimulatePitch();
        }

        private void GetBall() //timer code 3
        {
            balls++;
            UI.WriteToLog("Ball, " + balls + "-" + strikes);

            if (balls >= 4)
                nextStep = 4;
            else
                SimulatePitch();
        }

        private void EndAtBat() //timer code 4
        {
            if (strikes >= 3)
            {
                UI.WriteToLog(currentBatter.name + " struck out");
                outs++;
                if (outs >= 3)
                {
                    nextStep = 0;
                    return;
                }
            }
            else if (balls >= 4)
            {
                UI.WriteToLog(currentBatter.name + " drew a walk");
                if (_onBase[3] != null && _onBase[2] != null && _onBase[1] != null)
                {
                    UI.WriteToScore(_onBase[3].name + " Scored!");
                    _onBase[3] = null;
                    battingTeam.score++;
                    UI.UpdateScore(_homeTeam.score, _awayTeam.score);
                }
                if (_onBase[2] != null && _onBase[1] != null)
                    _onBase[3] = _onBase[2];
                _onBase[2] = _onBase[1];
                _onBase[1] = currentBatter;
            }
            nextStep = 1;
        }

        private void GetHitBall() //timer code 5
        {

            UI.WriteToLog("Hit, " + currentBatter.name + " advances");
            AdvanceBases();
            _onBase[1] = currentBatter;

            nextStep = 1;
        }

        private void AdvanceBases()
        {
            if (_onBase[1] == null && _onBase[2] == null && _onBase[3] == null)
                return;

            if (_onBase[3] != null)
            {
                UI.WriteToScore(_onBase[3].name + " scores!");
                battingTeam.score++;
                UI.UpdateScore(_homeTeam.score, _awayTeam.score);
            }

            _onBase[3] = _onBase[2];
            _onBase[2] = _onBase[1];

        }

        private void EndGame()
        {
            if (_homeTeam.score > _awayTeam.score)
                UI.WriteToLog(_homeTeam.teamName + " wins " + _homeTeam.score + " to " + _awayTeam.score);
            else if (_homeTeam.score < _awayTeam.score)
                UI.WriteToLog(_awayTeam.teamName + " wins " + _awayTeam.score + " to " + _homeTeam.score);
            else
                UI.WriteToLog("Game ends in a tie");

            timer.Enabled = false;
        }

        #endregion

        #region methods for random
        public static double GetRandomDouble()
        {
            return randomNum.NextDouble();
        }
        public static int GetRandomInt( int max)
        {
            return randomNum.Next( max );
        }
        public static int GetRandomInt()
        {
            return randomNum.Next();
        }
        #endregion

    }
}
