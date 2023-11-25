using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Match
    {
        private int _homeScore;
        private int _awayScore;
        private string _homeTeam;
        private string _awayTeam;
        private int _inning;

        public int homeScore { get { return _homeScore; } set { _homeScore = value; } }
        public int awayScore { get { return _awayScore; } set { _awayScore = value; } }
        public string homeTeam {  get { return _homeTeam; } set { _homeTeam = value; } }
        public string awayTeam { get { return _awayTeam; } set { _awayTeam = value; } }
        public int inning { get { return _inning; } set { _inning = value; } }

        public double GetRandomNum()
        {
            throw new NotImplementedException();
        }
    }
}
