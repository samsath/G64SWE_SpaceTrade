using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace DatabaseSt
{
    public class Database
    {
        SQLiteConnection dbc;
        string dbpath;

        public void Connect()
        {

            dbpath = @"SpaceTradeDatabase.sqlite3";
            if (File.Exists(dbpath))
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
        public void Close()
        {
            dbc.Close();
        }
        /// <summary>
        /// This will popuate a new database with the correct tables and relations.
        /// </summary>
        public void NewCreat()
        {
            // This load a file into the string then if the database connnection is up it will load it to the 
            // database and created the needed tables.
            Console.WriteLine("NewCreat Started");
            StringBuilder sqlString = new StringBuilder();
            using (StreamReader sr = new StreamReader("database.sql"))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    sqlString.AppendLine(line);
                }
            }
            string query = sqlString.ToString();

            using (SQLiteCommand command = new SQLiteCommand(dbc))
            {
                if (dbc.State == ConnectionState.Closed)
                {
                    dbc.Open();
                }
                else
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();

                }
                dbc.Close();
            }
        }

        public Boolean checkConnection()
        {
            // this checks if the database connection is open then returns true else opens the connects and true.
            if (dbc.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                System.
                dbc.Open();
                return true;
            }
        }

    }
}
