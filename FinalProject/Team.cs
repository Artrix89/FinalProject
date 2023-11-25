using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Team
    {
        private string _teamName;
        private string _ballparkName;
        private double _pitcherLimit;

        public string teamName { get { return _teamName; } set { _teamName = value; } }
        public string ballparkName { get { return _ballparkName; } set { _ballparkName = value; } }
        public double pitcherLimit { get { return _pitcherLimit; } set { _pitcherLimit = value; } }

        public bool CheckForPullPitcher()
        {
            throw new NotImplementedException();
        }
    }
}
