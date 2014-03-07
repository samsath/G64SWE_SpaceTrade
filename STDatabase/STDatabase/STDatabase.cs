using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;


namespace STDatabase
{
    /// <summary>
    /// This will be the class that allows the rest of the game to communicate to the database
    /// </summary>
    class Database
    {
        SQLiteConnection dbc;
        string dbpath;

        /// <summary>
        /// This will allow the program to connect to the database
        /// </summary>
        void Connect()
        {
            
            dbpath = @"SpaceTradeDatabase.sqlite3";
            if(File.Exists(dbpath))
            {
                Console.WriteLine("Sqlite Database exists");
                SQLiteConnection dbc = new SQLiteConnection(dbpath);
                dbc.Open();
            }
            else
            {
                Console.WriteLine("Sqlite Database Doesn't Exsist");
                SQLiteConnection.CreateFile(dbpath);
                Console.WriteLine("Sqlite Database Created");
                SQLiteConnection dbc = new SQLiteConnection(dbpath);
                dbc.Open();
                NewCreat();

                /// need to add to load sql script here.
            }
            // Connects to the database which is found.
            
            Console.WriteLine("Connection to database open");
        }
        /// <summary>
        /// This will close the connection to the database
        /// </summary>
        void Close()
        {
            dbc.Close();
        }
        /// <summary>
        /// This will popuate a new database with the correct tables and relations.
        /// </summary>
        void NewCreat()
        {

        }

        /// <summary>
        /// This is to get the all the user info. Giving them the User_id or Name.
        /// Then returning the User_id, Name, and Money 
        /// </summary>
        /// <param name="Userid or Username"></param>
        void GetUser(int Userid)
        {
            //SELECT * FROM Users WHERE Users_id = {0}; User_id
        }
        void GetUser(string Username)
        {
            //SELECT * FROM Users WHERE Name = {0}; Username
        }

        /// <summary>
        /// This will Return info of the Ship :
        /// Model, Ammo_Level, Health_Level, Cargo_Level, Fuel_Level,Owner, Extenstion, Location(X,Y) and Media file
        /// You can specify what sections you want by the par request.
        /// </summary>
        /// <param name="Ship_id"></param>
        void getShip(int Ship_id, string par="*")
        {

        })
    }
}
