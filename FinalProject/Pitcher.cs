using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Pitcher : Player
    {
        private double _earnedRunAverage;
        private double _burnout;

        public double earnedRunAverage {  get { return _earnedRunAverage;} set { _earnedRunAverage = value;} }
        public double burnout { get { return _burnout;} set {  _burnout = value;} }

        public string GetPitchThrown()
        {
            throw new NotImplementedException();
        }
    }
}
