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
        private int _inning;
        private static Random randomNum = new Random(10);
        #endregion

        #region constructors
        public Match()
        {
            _homeScore = 0;
            _awayScore = 0;
            _inning = 1;
            _homeTeam = new Team();
            //_awayTeam = new Team();
        }
        #endregion

        #region properties
        public int homeScore { get { return _homeScore; } set { _homeScore = value; } }
        public int awayScore { get { return _awayScore; } set { _awayScore = value; } }
        public int inning { get { return _inning; } set { _inning = value; } }
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
