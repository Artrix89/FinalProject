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
        private double _baserunningPercentage;
        #endregion

        public Batter()
        {
            _battingPercentage = 0;
            for ( int k = 0; k < 15; k++ )
            {
                _battingPercentage += Match.GetRandomDouble() / 30;
            }
            _battingPercentage = Math.Round(_battingPercentage, 3);
            //Console.WriteLine("Batting Average: " +  _battingPercentage);
        }

        #region properties
        public double battingPercentage { get { return _battingPercentage; } set { _battingPercentage = value; } }
        public double baserunningPercentage { get { return _baserunningPercentage; } set { _baserunningPercentage = value; } }
        #endregion

        public bool GetBaseDiscipline()
        {
            throw new NotImplementedException();
        }
        public string GetBattedBall()
        {
            throw new NotImplementedException();
        }
        public string GetBaseRunning()
        {
            throw new NotImplementedException();
        }
    }
}
