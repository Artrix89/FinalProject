using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Match
    {
        #region variables
        private Team _homeTeam;
        private Team _awayTeam;
        private int _strikes = 0;
        private int _balls = 0;
        private int _outs = 0;
        private int _inning = 1;
        private bool _isBottomInning = false;
        private static Random randomNum = new Random();
        private Batter[] _onBase = new Batter[4];
        #endregion

        #region constructors
        public Match()
        {
            _inning = 1;
            _homeTeam = new Team();
            _awayTeam = new Team();
        }
        #endregion

        #region properties
        public int inning { get { return _inning; } set { _inning = value; } }
        public int strikes { get { return _strikes; } set { _strikes = value; } }
        public int balls { get { return _balls; } set { _balls = value; } }
        public int outs { get { return _outs; } set { _outs = value; } }
        #endregion

        #region simulation methods
        public void StartGame()
        {
            SimulateHalf(_homeTeam, _awayTeam);
        }

        private void SimulateHalf( Team pitchingTeam, Team battingTeam )
        {
            if (_isBottomInning)
            {
                Console.WriteLine("Bottom of the " + inning + ", " + battingTeam.teamName + " at bat");
            }
            else
            {
                Console.WriteLine("Top of the " + inning + ", " + battingTeam.teamName + " at bat");
            }

            while( outs < 3 )
            {
                SimulateAtBat( pitchingTeam, battingTeam );
                strikes = 0;
                balls = 0;
            }

            outs = 0;
            _onBase[1] = null;
            _onBase[2] = null;
            _onBase[3] = null;

            if (_isBottomInning)
            {
                inning++;
                _isBottomInning = false;
            }
            else
                _isBottomInning = true;

            if (inning > 1)
            {
                EndGame();
                return;
            }
            SimulateHalf( battingTeam, pitchingTeam );
        }

        private void SimulateAtBat(Team pitchingTeam, Team battingTeam )
        {
            Batter batter = battingTeam.GetCurrentBatter();
            Pitcher pitcher = pitchingTeam.GetCurrentPitcher();

            Console.WriteLine( batter.name + " at bat for the " + battingTeam.teamName + " against " + 
                pitcher.name );

            while (strikes < 3 && balls < 4)
            {
                switch(SimulatePitch(batter, pitcher))
                {
                    case 0:                        
                        strikes++;
                        Console.WriteLine("Strike, " + balls + "-" + strikes);
                        break;
                    case 1:
                        Console.WriteLine("Hit, " + batter.name + " advances");
                        AdvanceBases( battingTeam );
                        _onBase[1] = batter;
                        return;
                    case 2:
                        balls++;
                        Console.WriteLine("Ball, " + balls + "-" + strikes);
                        break;
                }
                Console.ReadLine();
                
            }
            if (strikes >= 3)
            {
                Console.WriteLine(batter.name + " struck out");
                outs++;
            }
            else if (balls >= 4)
            {
                Console.WriteLine(batter.name + " drew a walk");
                AdvanceBases(battingTeam);
                _onBase[1] = batter;
            }

        }

        private int SimulatePitch( Batter batter, Pitcher pitcher )
        {
            double pitch = GetRandomDouble();
            if (pitch + .1 < batter.battingPercentage)
                return 1;
            else if (pitch - .2 < batter.battingPercentage)
                return 2;
            else
                return 0;
        }

        private void AdvanceBases( Team battingTeam )
        {
            if (_onBase[1] == null && _onBase[2] == null && _onBase[3] == null)
                return;

            if (_onBase[3] != null)
            {
                Console.WriteLine(_onBase[3].name + " scores!");
                battingTeam.score++;
                Console.ReadLine();
            }

            _onBase[3] = _onBase[2];
            _onBase[2] = _onBase[1];

        }

        private void EndGame()
        {
            if (_homeTeam.score > _awayTeam.score)
                Console.WriteLine(_homeTeam.teamName + " wins " + _homeTeam.score + " to " + _awayTeam.score);
            else if (_homeTeam.score < _awayTeam.score)
                Console.WriteLine(_awayTeam.teamName + " wins " + _awayTeam.score + " to " + _homeTeam.score);
            else
                Console.WriteLine("Game ends in a tie");
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
