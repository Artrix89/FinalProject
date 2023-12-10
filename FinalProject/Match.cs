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
        private int strikes = 0;
        private int balls = 0;
        private int outs = 0;
        private int _inning = 1;
        private bool _isBottomInning = false;
        private Random randomNum;
        private Batter[] _onBase = new Batter[4];
        private MainWindow UI;
        private Timer timer = new Timer(80);
        private Batter currentBatter;
        private Pitcher currentPitcher;
        private Queue<int> nextStep = new Queue<int>();
        #endregion

        #region constructors
        public Match( MainWindow ui )
        {
            randomNum = new Random();
            _homeTeam = new Team( this );
            _awayTeam = new Team( this );

            UI = ui;
            UI.UpdateScore(_homeTeam.score, _awayTeam.score);

            ChangeHalf();
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public Match( MainWindow ui, int seed )
        {
            randomNum = new Random( seed );
            _homeTeam = new Team(this);
            _awayTeam = new Team(this);

            UI = ui;
            UI.UpdateScore(_homeTeam.score, _awayTeam.score);

            ChangeHalf();
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public Match( MainWindow ui, 
            string homeName, string[] homeBatters, double[] homeBatterStats, string[] homePitchers, double[] homePitcherStats,
            string awayName, string[] awayBatters, double[] awayBatterStats, string[] awayPitchers, double[] awayPitcherStats)
        {
            randomNum = new Random();
            _homeTeam = new Team(homeName, homeBatters, homeBatterStats, homePitchers, homePitcherStats);
            _awayTeam = new Team(awayName, awayBatters, awayBatterStats, awayPitchers, awayPitcherStats);

            UI = ui;
            UI.UpdateScore(_homeTeam.score, _awayTeam.score);

            ChangeHalf();
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        #endregion

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            //Should already have 1 queued
            switch (nextStep.Dequeue())
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

            }
        }

        #region properties
        public string homeTeam { get { return _homeTeam.teamName; } }
        public string awayTeam { get { return _awayTeam.teamName; } }
        public Batter homeBatter { get { return _homeTeam.GetCurrentBatter(); } }
        public Batter awayBatter { get { return _awayTeam.GetCurrentBatter(); } }
        public Pitcher homePitcher { get { return _homeTeam.GetNextPitcher(); } }
        public Pitcher awayPitcher { get { return _awayTeam.GetNextPitcher(); } }
        public int onBase { get
            {
                int code = 0;
                if ( _onBase[1] != null )
                    code += 1;
                if (_onBase[2] != null )
                    code += 2;
                if (_onBase[3] != null )
                    code += 4;
                return code;
            } }
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
            UI.UpdateOuts(outs);
            UI.UpdateBases(onBase);
            UI.UpdateBalls(balls);
            UI.UpdateStrikes(strikes);

            if (_isBottomInning)
            {
                battingTeam = _homeTeam;
                pitchingTeam = _awayTeam;
                UI.WriteToLog("Bottom of the " + inning + ", " + homeTeam + " at bat");
            }
            else
            {
                pitchingTeam = _homeTeam;
                battingTeam = _awayTeam;
                UI.WriteToLog("Top of the " + inning + ", " + awayTeam + " at bat");
            }

            UI.ChangeHalf(inning, _isBottomInning);
            UI.UpdateCurrentPlayers("", "");

            if (_isBottomInning)
            {
                _inning += 1;
                _isBottomInning = false;
            }
            else
                _isBottomInning = true;
            nextStep.Enqueue(1);

        }

        private void ChangeBatter() //timer code 1
        {
            currentBatter = battingTeam.GetCurrentBatter();
            currentPitcher = pitchingTeam.GetCurrentPitcher();
            UI.UpdateCurrentPlayers(currentBatter.name, currentPitcher.name);
            balls = 0;
            strikes = 0;
            UI.UpdateStrikes(strikes);
            UI.UpdateBalls(balls);
            UI.UpdateOuts(outs);

            UI.WriteToLog(currentBatter.name + " at bat for the " + battingTeam.teamName + " against " +
                currentPitcher.name);

            SimulatePitch();
        }

        private void SimulatePitch()
        {
            double pitch = GetRandomDouble();
            if (pitch < (currentPitcher.earnedRunAverage / 9) - .3 + currentBatter.hittingPercentage) //Hit
                nextStep.Enqueue(5);
            

            else if (pitch < (currentPitcher.earnedRunAverage / 9) + currentBatter.hittingPercentage) //Ball
                nextStep.Enqueue(3);            

            else //Strike            
                nextStep.Enqueue(2);
            
        }

        private void GetStrike() //timer code 2
        {
            if (GetRandomDouble() < .5)
            {
                if (strikes < 2)
                    strikes++;
                UI.WriteToLog("Foul ball, " + balls + "-" + strikes);
                UI.UpdateStrikes(strikes);
                SimulatePitch();
                return;
            }

            strikes++;
            UI.UpdateStrikes(strikes);
            if (GetRandomDouble() < .5)
                UI.WriteToLog("Strike looking, " + balls + "-" + strikes);
            else
                UI.WriteToLog("Strike swinging, " + balls + "-" + strikes);

            if (strikes >= 3)
                nextStep.Enqueue(4);
            else
                SimulatePitch();
        }

        private void GetBall() //timer code 3
        {
            balls++;
            UI.UpdateBalls(balls);
            UI.WriteToLog("Ball, " + balls + "-" + strikes);

            if (balls >= 4)
                nextStep.Enqueue(4);
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
                    nextStep.Enqueue(0);
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
                {
                    _onBase[3] = _onBase[2];
                }
                _onBase[2] = _onBase[1];
                _onBase[1] = currentBatter;
                UI.UpdateBases(onBase);
                
            }
            nextStep.Enqueue(1);
        }

        private void GetHitBall() //timer code 5
        {
            double tmp = GetRandomDouble();

            if (tmp > .65 + currentBatter.battingPercentage)
            {
                UI.WriteToLog(currentBatter.name + " hit a ground out");
                outs++;
                if (outs >= 3)
                {
                    nextStep.Enqueue(0);
                    return;
                }
            }
            else if (tmp > .35 + currentBatter.battingPercentage)
            {
                UI.WriteToLog(currentBatter.name + " hit a fly out");
                outs++;
                if (outs >= 3)
                {
                    nextStep.Enqueue(0);
                    return;
                }
            }
            else
            {
                AdvanceBases();
                UI.UpdateBases(onBase);
            }

            nextStep.Enqueue(1);
        }

        private void AdvanceBases()
        {
            double tmp = GetRandomDouble();
            if (tmp < .65) // single
            {
                UI.WriteToLog(currentBatter.name + " hits a single!");
                if (_onBase[3] != null)
                {
                    UI.WriteToScore(_onBase[3].name + " scores!");
                    battingTeam.score++;
                    UI.UpdateScore(_homeTeam.score, _awayTeam.score);
                }

                _onBase[3] = _onBase[2];
                _onBase[2] = _onBase[1];
                _onBase[1] = currentBatter;
            }
            else if (tmp < .8) // double
            {
                UI.WriteToLog(currentBatter.name + " hits a double!");
                if (_onBase[3] != null)
                {
                    UI.WriteToScore(_onBase[3].name + " scores!");
                    battingTeam.score++;
                }
                if (_onBase[2] != null)
                {
                    UI.WriteToScore(Environment.NewLine + _onBase[2].name + " scores!");
                    battingTeam.score++;
                }
                UI.UpdateScore(_homeTeam.score, _awayTeam.score);
                _onBase[3] = _onBase[1];
                _onBase[2] = currentBatter;
                _onBase[1] = null;
            }
            else if (tmp < .9) // triple
            {
                UI.WriteToLog(currentBatter.name + " hits a triple!");
                if (_onBase[3] != null)
                {
                    UI.WriteToScore(_onBase[3].name + " scores!");
                    battingTeam.score++;
                }
                if (_onBase[2] != null)
                {
                    UI.WriteToScore(Environment.NewLine + _onBase[2].name + " scores!");
                    battingTeam.score++;
                }
                if (_onBase[1] != null)
                {
                    UI.WriteToScore(Environment.NewLine + _onBase[1].name + " scores!");
                    battingTeam.score++;
                }
                UI.UpdateScore(_homeTeam.score, _awayTeam.score);
                _onBase[3] = currentBatter;
                _onBase[2] = null;
                _onBase[1] = null;
            }
            else // home run
            {
                UI.WriteToLog(currentBatter.name + " hits a home run!");
                UI.WriteToScore(currentBatter.name + " scores!");
                battingTeam.score++;
                if (_onBase[3] != null)
                {
                    UI.WriteToScore(Environment.NewLine + _onBase[3].name + " scores!");
                    battingTeam.score++;
                }
                if (_onBase[2] != null)
                {
                    UI.WriteToScore(Environment.NewLine + _onBase[2].name + " scores!");
                    battingTeam.score++;
                }
                if (_onBase[1] != null)
                {
                    UI.WriteToScore(Environment.NewLine + _onBase[1].name + " scores!");
                    battingTeam.score++;
                }
                UI.UpdateScore(_homeTeam.score, _awayTeam.score);
                _onBase[3] = null;
                _onBase[2] = null;
                _onBase[1] = null;
            }
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
            UI.EndGame();
        }

        #endregion

        #region methods for random
        public double GetRandomDouble()
        {
            return randomNum.NextDouble();
        }
        public int GetRandomInt( int max)
        {
            return randomNum.Next( max );
        }
        #endregion

    }
}
