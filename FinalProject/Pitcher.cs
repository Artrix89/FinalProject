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
        private double _hitBallChance;
        private double _burnout;
        #endregion

        public Pitcher()
        {
            _earnedRunAverage = 0;
            for(int i = 0; i < 15; i++) 
            {
                _earnedRunAverage += Match.GetRandomDouble() / 16.875;
            }
            _earnedRunAverage = Math.Round(_earnedRunAverage, 3);
            //Console.WriteLine("ERA: " + earnedRunAverage);
        }

        #region properties
        public double earnedRunAverage {  get { return _earnedRunAverage * 9;} set { _earnedRunAverage = value;} }
        public double burnout { get { return _burnout;} set {  _burnout = value;} }
        #endregion

        public string GetPitchThrown()
        {
            throw new NotImplementedException();
        }
    }
}
