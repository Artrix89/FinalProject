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
        private int _homeScore;
        private int _awayScore;
        private Team _homeTeam;
        private Team _awayTeam;
        private int _homeIndex = 0;
        private int _awayIndex = 0;
        private int _strikes = 0;
        private int _balls = 0;
        private int _outs = 0;
        private int _inning = 1;
        private bool _isBottomInning = false;
        private static Random randomNum = new Random();
        #endregion

        #region constructors
        public Match()
        {
            _homeScore = 0;
            _awayScore = 0;
            _inning = 1;
            _homeTeam = new Team();
            _awayTeam = new Team();
        }
        #endregion

        #region properties
        public int homeScore { get { return _homeScore; } set { _homeScore = value; } }
        public int awayScore { get { return _awayScore; } set { _awayScore = value; } }
        public int inning { get { return _inning; } set { _inning = value; } }
        public int homeIndex { 
            get {
                _homeIndex++;
                if (_homeIndex > 9)
                    _homeIndex = 1;
                return _homeIndex;
            } set { _homeIndex = value; } }
        public int awayIndex
        {
            get
            {
                _awayIndex++;
                if (_awayIndex > 9)
                    _awayIndex = 1;
                return _awayIndex;
            }
            set { _awayIndex = value; }
        }
        public int strikes { get { return _strikes; } set { _strikes = value; } }
        public int balls { get { return _balls; } set { _balls = value; } }
        public int outs { get { return _outs; } set { _outs = value; } }
        #endregion

        #region simulation methods
        public void StartGame()
        {
            SimulateHalf(_homeTeam, _awayTeam);
        }

        private void SimulateHalf( Team pitchingTeam, Team battingTeam)
        {
            while( outs < 3 )
            {

            }



            if (_isBottomInning)
            {
                inning++;
                _isBottomInning = false;
            }
            else
                _isBottomInning = true;

            if (inning > 9)
            {
                EndGame();
                return;
            }
            SimulateHalf( battingTeam, pitchingTeam );
        }

        private void EndGame()
        {
            throw new NotImplementedException();
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
