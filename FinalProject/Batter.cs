using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Batter : Player
    {
        #region variables
        private double _battingPercentage;
        private double _hittingPercentage;
        #endregion

        public Batter( Match match ) : base( match )
        {
            _battingPercentage = 0;
            for ( int k = 0; k < 15; k++ )
            {
                _battingPercentage += match.GetRandomDouble() / 30;
            }
            _battingPercentage = Math.Round(_battingPercentage, 3);

            _hittingPercentage = _battingPercentage * .25;
        }

        #region properties
        public double battingPercentage { get { return _battingPercentage; } set { _battingPercentage = value; } }
        public double hittingPercentage { get { return _hittingPercentage; } set { _hittingPercentage = value; } }
        #endregion


    }
}
