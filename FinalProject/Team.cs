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
        private double _pitcherLimit;
        private Batter[] _batterList = new Batter[9];
        private Pitcher[] _pitcherList = new Pitcher[4];
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
        public Team( )
        {
            _teamName = teamLocationDB[Match.GetRandomInt(teamLocationDB.Length)] + " " +
                teamMascotDB[Match.GetRandomInt(teamMascotDB.Length)];
            Console.WriteLine("Team Name: " + _teamName);
            FillTeam();
        }

        public Team(string name)
        {
            _teamName = name;
            Console.WriteLine("Team Name: " + _teamName);
            FillTeam();            
        }
        #endregion

        #region properties
        public string teamName { get { return _teamName; } set { _teamName = value; } }
        public double pitcherLimit { get { return _pitcherLimit; } set { _pitcherLimit = value; } }
        #endregion

        private void FillTeam()
        {
            Console.WriteLine("Batters for the " + _teamName + ":");
            for (int k = 1; k <= 9; k++)
            {
                _batterList[1] = new Batter();
            }

            Console.WriteLine("Pitchers for the " + _teamName + ":");
            for (int k = 1; k <= 4; k++)
            {
                _pitcherList[1] = new Pitcher();
            }
        }

        public bool CheckForPullPitcher()
        {
            throw new NotImplementedException();
        }
    }
}
