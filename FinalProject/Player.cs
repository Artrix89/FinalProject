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
        private static string[] firstNameDB = { "Adelaide", "Abbott", "Ajax", "Aldon", "Alejandro", "Allan", "Anastasia", "Atlas", "August", "Axel",
        "Baby", "Ball", "Bennett", "Baseball", "Bob", "Blimp", "Brock", "Brooklyn",
        "Caleb", "Cannonball", "Chambers", "Chorby", "Collins", "Chris", "Cornelius", "Commissioner",
        "Daniel", "Doc", "Dudley", "Don", "Denim",
        "Electric", "Eddie", "Elijah", "Elvis", "Erin", "Emmett", "Ezekiel", "Espresso",
        "Famous", "Fairwood", "Felix", "Fitzgerald", "Frankie", "Fish", "Frazier",
        "Garcia", "Goodwin", "Grant", "Gizmo", "Gloria", "Guy", "Grit",
        "Harriet", "Haunt", "Hendricks", "Hercules", "Howell", "Henry",
        "Ingrid", "Isaac", "Ikea", "Icarus",
        "Jackie", "Jacob", "James", "Jaylen", "Jessica", "Jay", "Javier", "Justice",
        "Kelvin", "Kennedy", "Knight", "King", "Kurt",
        "Lachlan", "Lars", "Lenny", "Lucy", "Luis", "Logan",
        "Major", "Mambo", "Marco", "McBaseball", "Mia", "Mike", "Mindy", "Moody",
        "Nagomi", "Nerd", "Nucleus", "Number", "Noah",
        "Oliver", "Oscar", "Orville", "Owen", "Oops",
        "Parker", "Paula", "Peanut", "Pitcher", "PolkaDot", "Peekaboo",
        "Quinns", "Quack", "Qais",
        "Reese", "Rhombus", "Richardson", "Randy", "Rivers", "Rigby",
        "Sam", "Scratch", "Saturday", "Sebastian", "Simon", "Sixpack", "Summers", "Stu",
        "Theodore", "Thomas", "Tycho", "Tyler", 
        "Uncle", "Valentine", "Vernon", "Viola", 
        "Waldo", "Wyatt", "Winnie", "Wichita",
        "York", "Zack", "Zephyr", "Zero"};

        private static string[] lastNameDB = { "Abbott", "Adams", "Airport", "Alfredo", "Ampersand", "Andante", "Applesauce", "Angry",
        "Baker", "Ballson", "Barley", "Beanpot", "Bathtub", "Beefsteak", "Blacksmith", "Buckley",
        "Campbell", "Carpenter", "Cash", "Cilantro", "Chill", "Clambucket", "Critter", "Cookbook",
        "Davids", "Delacruz", "Doctor", "Dogwalker", "Drama", "Drumsolo", "Dunno", "Duress",
        "Earwig", "Eberhardt", "Eggburt", "Evergreen", "Elliott", "Elftower",
        "Fairwood", "Falconer", "Fantastic", "Fiasco", "Firewall", "Forbes", "Fox", "Frost",
        "Games", "Giant", "Good", "Glover", "Gooseball", "Garbage", "Grackle", "Greatness",
        "Halifax", "Hambone", "Haunt", "Holloway", "Hollywood", "Hotdogfingers", "Horseman",
        "Incarnate", "Internet", "Inning", "Isarobot",
        "James", "Johnson", "Jones", "Judochop", "Junior", "Junior Jr",
        "Kane", "Kiddo", "Kennedy", "Kranch", "Kramer", "Knuckles",
        "Lampman", "Leaf", "Loser", "Loofah", "Looking", "Lemma", "Lanyard", "Lancaster",
        "MacMillan", "Manhattan", "Mason", "Matrix", "Mitchell", "Mueller", "McElroy", "Melon",
        "Nakamoto", "Nameperson", "Nava", "Nolan", "Nugget", "Nightmare",
        "Object", "Olive", "Owens", "Outlaw", "Oki",
        "Pacheco", "Plasma", "Podcast", "Prestige", "Pergame", "Patchwork", "Peterson", "Prettygood",
        "Quartz", "Quicksilver", "Quitter",
        "Ramsey", "Roadhouse", "Rogers", "Ruiz",
        "Safari", "Scandal", "Short", "Silk", "Sports", "Stone", "Sasquatch", "Scott",
        "Telephone", "Toast", "Townsend", "Trombone", "Triumphant", "Tosser", "Turnip",
        "Vapor", "Vine", "Violence",
        "Weatherman", "Wheeler", "Wilson", "Winner", "Weeks",
        "Yardstick", "Yesterday", "Youngblood"};

        #endregion

        #region constructors
        public Player( Match match)
        {
            _name = firstNameDB[match.GetRandomInt(firstNameDB.Length)] + " " +
                lastNameDB[match.GetRandomInt(lastNameDB.Length)];
        }

        public Player(string name)
        {
            _name = name;
        }
        #endregion

        #region properties
        public string name { get { return _name; } set { _name = value; } }
        #endregion
    }
}
