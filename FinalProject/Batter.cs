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
