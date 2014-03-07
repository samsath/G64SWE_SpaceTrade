using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace ConsoleApplication1
{
    class database
    {
        SQLiteConnection dbc;
        internal void connect()
        {
            // Checks to see if the database exists, if it doesn't then it will create it.
            string dbpath = "spacetradeDatabase.sqlite";
            if(File.Exists(dbpath))
            {
                Console.WriteLine("Sqlite Database exists");
                
            }
            else
            {
                Console.WriteLine("Sqlite Database Doesn't Exsist");
                SQLiteConnection.CreateFile("spacetradeDatabase.sqlite");
                Console.WriteLine("Sqlite Database Created");

                /// need to add to load sql script here.
            }
            // Connects to the database which is found.
            SQLiteConnection dbc = new SQLiteConnection(dbpath);
            dbc.Open();
            Console.WriteLine("Connection to database open");
        }

        internal void run(string type, string objecttype, int oname, string col)
        {
            /* 
             * idea if you run this will objecttype(Planet,Ship...), oname(db id) 
             * then you will get the information for that item.
            */
            SQLiteCommand dbq = new SQLiteCommand(dbc);
            string query = "";
            switch (type)
            {
                case "Get":
                    query += "SELECT ";
                    break;
                case "Add":
                    query += "INSERT ";
                    break;
                case "Update":
                    query += "Update";
            }

            query += col + " ";

            switch (objecttype){
                case "Planet":
                    query += "FROM Planet";
                    break;
                case "Ship":
                    query += "FROM Ship";
                    break;
                case "Resource":
                    query += "FROM Resource";
                    break;
                case "User":
                    query += "FROM User";
                    break;
            }

            query += " WHERE id = " + oname;


 

        }

        internal void close()
        {
            // this is to close the connection to the database, and then tell the user.
            dbc.Close();
            Console.WriteLine("Connection to database Closed");
        }
    }
}
