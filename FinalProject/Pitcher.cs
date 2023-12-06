using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Pitcher : Player
    {
        #region variables
        private double _earnedRunAverage;
        #endregion

        #region constructors
        public Pitcher( Match match) : base( match )
        {
            _earnedRunAverage = 0;
            for(int i = 0; i < 15; i++) 
            {
                _earnedRunAverage += match.GetRandomDouble() / 16.875;
            }
            _earnedRunAverage = Math.Round(_earnedRunAverage, 3);
        }

        public Pitcher( string name, double stat ) : base( name )
        {
            _earnedRunAverage = stat / 9;
        }
        #endregion

        #region properties
        public double earnedRunAverage {  get { return _earnedRunAverage * 9;} set { _earnedRunAverage = value;} }
        #endregion

    }
}
