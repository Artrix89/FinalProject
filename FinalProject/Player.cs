using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Player
    {
        #region variables
        private string _name;
        #endregion

        #region first and last name database
        private static string[] firstNameDB = { "Aldon", "Dudley", "Nagomi", "Jaxon", "Valentine", "Rai", 
            "Jessica", "Bonk", "Jacob", "Burke", "Wyatt", "Theodore", "Qais", "Gunther" };
        private static string[] lastNameDB = { "Cashmoney", "Mueller", "McDaniel", "Buckley", "Games", "Spliff", 
            "Telephone", "Jokes", "Haynes", "Gonzales", "Pothos", "Cervantes", "Dogwalker", "O'Brian"};
        #endregion

        public Player( Match match)
        {
            _name = firstNameDB[match.GetRandomInt(firstNameDB.Length)] + " " +
                lastNameDB[match.GetRandomInt(lastNameDB.Length)];

            //Console.WriteLine("Name: " +  _name);
        }

        #region properties
        public string name { get { return _name; } set { _name = value; } }
        #endregion
    }
}
