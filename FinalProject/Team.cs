using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Team
    {
        #region variables
        private string _teamName;
        private double pitcherLimit = 1;
        private Batter[] _batterList = new Batter[10];
        private Pitcher[] _pitcherList = new Pitcher[5];
        private int _batterIndex = 0;
        private int _pitcherIndex = 0;
        private int _score = 0;
        private Pitcher activePitcher;
        private Match currentMatch;
        #endregion

        #region location and mascot database
        private static string[] teamLocationDB = { "Arizona", "Atlanta", "Baltimore", "Boston", "Chicago", "Cincinnati", "Cleveland", 
            "Colorado", "Detroit", "Houston", "Kansas", "Los Angeles", "Miami", "Milwaukee", "Minnesota", 
            "New York", "Oakland", "Philadelphia", "Pittsburgh", "San Diego", "San Francisco", "Seattle", 
            "St. Louis", "Tampa Bay", "Texas", "Toronto", "Washington"};

        private static string[] teamMascotDB = { "Diamondbacks", "Braves", "Orioles", "Red Sox", "Cubs", "White Sox", "Reds", 
            "Guardians", "Rockies", "Tigers", "Astros", "Royals", "Angels", "Dodgers", "Marlins", "Brewers", 
            "Twins", "Mets", "Yankees", "Athletics", "Phillies", "Pirates", "Padres", "Giants", "Mariners", 
            "Cardinals", "Rays", "Rangers", "Blue Jays", "Nationals"};

        #endregion

        #region constructors
        public Team( Match match)
        {
            currentMatch = match;
            _teamName = teamLocationDB[currentMatch.GetRandomInt(teamLocationDB.Length)] + " " +
                teamMascotDB[currentMatch.GetRandomInt(teamMascotDB.Length)];
            FillTeam();
        }

        public Team(string name)
        {
            _teamName = name;
            FillTeam();            
        }
        #endregion

        #region properties
        public string teamName { get { return _teamName; } set { _teamName = value; } }
        public int score { get { return _score; } set { _score = value; } }
        public int batterIndex
        {
            get
            {
                _batterIndex++;
                if (_batterIndex > 9)
                    _batterIndex = 1;
                return _batterIndex;
            }
            set { _batterIndex = value; }
        }
        public int pitcherIndex { 
            get 
            {
                if (_pitcherIndex >= 4)
                    _pitcherIndex = 0;
                _pitcherIndex++;
                return _pitcherIndex; 
            }
            set { _pitcherIndex = value; } }
        #endregion


        public Batter GetCurrentBatter( )
        {
            return _batterList[batterIndex];
        }

        public Pitcher GetNextPitcher( )
        {
            return _pitcherList[pitcherIndex];
        }

        public Pitcher GetCurrentPitcher()
        {
            if ( pitcherLimit  >= 1)
            {
                activePitcher = _pitcherList[pitcherIndex];
                pitcherLimit = 0;
            }
            return _pitcherList[_pitcherIndex];
        }

        private void FillTeam()
        {
            //Console.WriteLine("Batters for the " + _teamName + ":");
            for (int k = 1; k <= 9; k++)
            {
                _batterList[k] = new Batter( currentMatch );
            }

            //Console.WriteLine("Pitchers for the " + _teamName + ":");
            for (int k = 1; k <= 4; k++)
            {
                _pitcherList[k] = new Pitcher( currentMatch );
            }
        }

        public bool CheckForPullPitcher()
        {
            throw new NotImplementedException();
        }
    }
}
