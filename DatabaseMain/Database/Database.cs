using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace STDatabase
{
    /// <summary>
    /// The location will need to be changed so that they work on every computer/
    /// </summary>
    public class Database
    {
        public SQLiteConnection dbc;
        string dbpath;
        /// <summary>
        /// This connects to the database and then 
        /// </summary>
        /// <returns></returns>
        public Boolean Connect()
        {
            // need to change this for the you build
            dbpath = @"C:\Users\sam\Programming\CwkSpaceTrade\DatabaseDocSupportFiles\STDatabase.db";
            if (File.Exists(dbpath))
            {
                Console.WriteLine("Sqlite Database exists");
                dbc = new SQLiteConnection("Data Source =" + dbpath);
                dbc.Open();
            }
            else
            {
                Console.WriteLine("Sqlite Database Doesn't Exsist");
                SQLiteConnection.CreateFile(dbpath);
                Console.WriteLine("Sqlite Database Created");
                dbc = new SQLiteConnection(dbpath);
                dbc.Open();
                newCreat();
                

                /// need to add to load sql script here.
            }
            // Connects to the database which is found.

            Console.WriteLine("Connection to database open");

            if (dbc.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public bool newCreat()
        {
            // This load a file into the string then if the database connnection is up it will load it to the 
            // database and created the needed tables.
            Console.WriteLine("NewCreat Started");
            StringBuilder sqlString = new StringBuilder();

            //change this aswell depending on your envirnment.
            using (StreamReader sr = new StreamReader(@"C:\Users\sam\Programming\CwkSpaceTrade\DatabaseDocSupportFiles\database.sql"))
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
                if (dbc.State == ConnectionState.Closed){
                    dbc.Open();
                }

                command.CommandText = query;
                command.ExecuteNonQuery();

                try{
                    string UserQuery = "SELECT * FROM users";
                    command.CommandText = UserQuery;
                    command.ExecuteNonQuery();

                }catch(Exception ex){
                    Console.WriteLine("Tables havent been created: " + ex);
                    return false;
                }
                return true;
                
            }
            dbc.Close();
        }

        /// <summary>
        /// This checks what the current state of the database is. eg Open or Closed.
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            if (dbc != null && dbc.State == ConnectionState.Open)
            {
                Console.WriteLine("Database Connected");
                return true;
            }
            else
            {
                Console.WriteLine("Database Non Connection");
                return false;
            }
        }

        /// <summary>
        /// This closes the database if run.
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            dbc.Close();
            if (Check())
            {
                Console.WriteLine("Database Connection is still open");
                return false;
            }
            else
            {
                Console.WriteLine("Database Connection Closed");
                return true;
            }
        }

        /// <summary>
        /// This will get you the user information, from their User info.
        /// </summary>
        /// <returns>User_id, Session_id, Name, Money</returns>
        public List<Userdata> getUser()
        {
            List<Userdata> result = new List<Userdata>();
            if (Check())
            {
                string UserQuery = "SELECT users.Users_id, sessions.Session_id, users.Name, sessions.Money FROM users, sessions WHERE users.Users_id == sessions.Users_id";
                using (SQLiteCommand command = new SQLiteCommand(UserQuery, dbc))
                {
                    try
                    {

                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                Userdata record = new Userdata(rdq.GetInt32(0), rdq.GetInt32(1), rdq.GetString(2), rdq.GetInt32(3),0);
                                result.Add(record);
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }
                    
                }
                
            }
            return result;
        }

        /// <summary>
        /// Gets the Session values for a user
        /// </summary>
        /// <returns>User_id, Session_id, Name</returns>
        public List<Userdata> getSessionNum()
        {
            List<Userdata> result = new List<Userdata>();
            if (Check())
            {
                string Query = "SELECT users.Users_id, sessions.Session_id, users.Name FROM users, sessions WHERE users.Users_id == sessions.Users_id";
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {
                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                Userdata record = new Userdata(rdq.GetInt32(0), rdq.GetInt32(1), rdq.GetString(2), 0,0);
                                result.Add(record);
                            }
                        }
                    }catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }

            }

            return result;
        }
        /// <summary>
        /// This adds a user and their session into the database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="money"></param>
        public void SetUser(string name, int money)
        {
            if (Check())
            {
                string Query1 = String.Format("INSERT INTO users(Name) VALUES('{0}'); ",name);
                string Query2 = String.Format("INSERT INTO sessions(Users_id,Money) VALUES((SELECT Users_id FROM users WHERE Name = '{0}'),{1});", name, money);
                using (SQLiteCommand command = new SQLiteCommand(dbc))
                {
                    try
                    {
                        command.CommandText = Query1;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                    try
                    {
                        command.CommandText = Query2;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }
                        

                    Console.WriteLine("Users Added");

                }

            }
        }

        /// <summary>
        /// This gets the ship information for the ship of that ID.
        /// </summary>
        /// <param name="ship_id"></param>
        /// <returns></returns>
        public List<Shipdata> getShip(int ship_id)
        {
            List<Shipdata> result = new List<Shipdata>();
            if (Check())
            {
                string Query = string.Format("SELECT ship.Ship_id, ship.Model, ship.Ammo_Level, ship.Health_Level, ship.Cargo_Level, ship.Fuel_Level, ship.Owner, ship.Extenstions, ship.x_loc, ship.y_loc, media.Media_id, media.file_Loc FROM ship, media, shipmedia WHERE ship.Ship_id == shipmedia.Ship_id AND shipmedia.Media_id == media.Media_id AND ship.Ship_id = '{1}';",ship_id);
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {
                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                Shipdata record = new Shipdata(rdq.GetInt32(0), rdq.GetInt32(1), rdq.GetInt32(2), rdq.GetInt32(3), rdq.GetInt32(4), rdq.GetInt32(5), rdq.GetInt32(6), rdq.GetString(7), rdq.GetInt32(8), rdq.GetInt32(9), rdq.GetInt32(10), rdq.GetString(11));
                                result.Add(record);
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }

            }

            return result;
        }

        /// <summary>
        /// This gives you all the planet infomation if you give it the planet id. No resource info.
        /// </summary>
        /// <param name="planet_id"></param>
        /// <returns></returns>
        public List<Planetdata> getPlanet(int planet_id)
        {
            List<Planetdata> result = new List<Planetdata>();
            if (Check())
            {
                string Query = string.Format("SELECT planet.Planet_id, planet.Title, planet.X_loc, planet.Y_loc, media.Media_id, media.file_Loc FROM planet, media, planetmedia WHERE planet.Planet_id == planetmedia.Planet_id AND planetmedia.Media_id == media.Media_id AND planet.Planet_id = '{0}';",planet_id);
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {
                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                Planetdata record = new Planetdata(rdq.GetInt32(0), rdq.GetString(1), rdq.GetInt32(2), rdq.GetInt32(3), rdq.GetInt32(4),  rdq.GetString(5));
                                result.Add(record);
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }

            }

            return result;
        }

        /// <summary>
        /// This gets the resource information if you give it the resource_id.
        /// </summary>
        /// <param name="resource_id"></param>
        /// <returns></returns>
        public List<Resourcedata> getResource(int resource_id)
        {
            List<Resourcedata> result = new List<Resourcedata>();
            if (Check())
            {
                string Query = string.Format("SELECT resources.Resources_id, resources.Name, resources.Initial_Price, resources.Description, media.Media_id, media.file_Loc FROM resources, resourcesmedia, media WHERE (resources.Resources_id == resourcesmedia.Resources_id AND resourcesmedia.Media_id == media.Media_id) AND resources.Resources_id == '{0}';", resource_id);
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {
                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                Resourcedata record = new Resourcedata(rdq.GetInt32(0), rdq.GetString(1), rdq.GetInt32(2), rdq.GetString(3), 0, 0, 0, 0, 0, rdq.GetInt32(4), rdq.GetString(5));
                                result.Add(record);
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }
            }
            return result;
        }
        /// <summary>
        /// Here you will get the media infomration for any type of object in the database aslong as you say what type it is.
        /// </summary>
        /// <param name="type">R = Resource, S = Ship, P = Planet</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Mediadata> getMedia(string type, int id)
        {
            string Query = "";
            List<Mediadata> result = new List<Mediadata>();
            switch(type){
                case "R":
                    Query = string.Format("SELECT media.Media_id, media.X_size, media.Y_size, media.Lenght, media.File_loc FROM media, resourcesmedia WHERE resourcesmedia.Media_id == media.Media_id AND resourcesmedia.Resources_id == '{0}';", id);
                    break;
                case "S":
                    Query = string.Format("SELECT media.Media_id, media.X_size, media.Y_size, media.Lenght, media.File_loc FROM media, shipmedia WHERE shipmedia.Media_id == media.Media_id AND shipmedia.Ship_id == '{0}';", id);
                    break;
                case "P":
                    Query = string.Format("SELECT media.Media_id, media.X_size, media.Y_size, media.Lenght, media.File_loc FROM media, planetmedia WHERE planetmedia.Media_id == media.Media_id AND planetmedia.Planet_id == '{0}';", id);
                    break;
            }
            if(Check()){
                 using (SQLiteCommand command = new SQLiteCommand( Query, dbc))
                {
                    try
                    {
                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                Mediadata record = new Mediadata(rdq.GetInt32(0), rdq.GetInt32(1), rdq.GetInt32(2), rdq.GetInt32(3),rdq.GetString(4));
                                result.Add(record);
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }
            }
            return result;
         }

        /// <summary>
        /// Gets the high score of the everyone in the list
        /// </summary>
        /// <returns></returns>
        public List<Userdata> getHighScore()
        {
            List<Userdata> result = new List<Userdata>();
            if (Check())
            {
                string UserQuery = "SELECT users.Users_id, users.Name, highscoure.Score FROM users, highscore WHERE users.Users_id == highscore.Users_id;";
                using (SQLiteCommand command = new SQLiteCommand(UserQuery, dbc))
                {
                    try
                    {

                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                Userdata record = new Userdata(rdq.GetInt32(0), 0, rdq.GetString(1), 0, rdq.GetInt32(2));
                                result.Add(record);
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }

            }
            return result;
        }

        /// <summary>
        /// Gets all the resources what the ship selected has.
        /// </summary>
        /// <param name="shipId"></param>
        /// <returns></returns>
        public List<Resourcedata> ShipResources(int shipId)
        {
            List<Resourcedata> result = new List<Resourcedata>();
            if (Check())
            {
                string Query = string.Format("SELECT resources.Resources_id, resources.Name, resources.Initial_Price, resources.Description, shipresource.Amount, shipresource.Bought_Price FROM resource, shipresource WHERE resources.Resources_id == shipresource.Resources_id AND shipresource.Ship_id = '{0}';", shipId);
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {
                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                Resourcedata record = new Resourcedata(rdq.GetInt32(0), rdq.GetString(1), rdq.GetInt32(2), rdq.GetString(3), 0, 0, shipId, rdq.GetInt32(4), rdq.GetInt32(5), 0, "null");
                                result.Add(record);
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }
            }
            return result;
        }

        /// <summary>
        /// Gets what resources a planet has. By giving it the Planet id.
        /// </summary>
        /// <param name="planetId"></param>
        /// <returns></returns>
        public List<Resourcedata> PlanetResources(int planetId)
        {
            List<Resourcedata> result = new List<Resourcedata>();
            if (Check())
            {
                string Query = string.Format("SELECT resources.Resources_id, resources.Name, resources.Initial_Price, resources.Description, planetresources.Price FROM resource, planetresources WHERE resources.Resources_id == planetresources.Resources_id AND planetresources.Planet_id = '{0}';", planetId);
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {
                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                Resourcedata record = new Resourcedata(rdq.GetInt32(0), rdq.GetString(1), rdq.GetInt32(2), rdq.GetString(3), planetId, 0, 0, 0, rdq.GetInt32(4), 0, "null");
                                result.Add(record);
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }
            }
            return result;
        }

        /// <summary>
        /// SHOULD USE OTHER METHORD
        /// Sets the high score for that user, by giving it the user name and score.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="score"></param>
        public void SetHighscore(string name, int score)
        {
            if (Check())
            {
                string Query = String.Format("INSERT INTO highscore(Users_id,Score) VALUES((SELECT Users_id FROM users WHERE Name = '{0}'),{1});", name, score);
                using (SQLiteCommand command = new SQLiteCommand(dbc))
                {
                    try
                    {
                        command.CommandText = Query;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }


                    Console.WriteLine("HighScore Added");

                }

            }
        }
        /// <summary>
        /// PREFFERED METHORD
        /// Sets the high score for that user, by giving it the user id and score.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="score"></param>
        public void SetHighscore(int id, int score)
        {
            if (Check())
            {
                string Query = String.Format("INSERT INTO highscore(Users_id,Score) VALUES({0},{1});", id, score);
                using (SQLiteCommand command = new SQLiteCommand(dbc))
                {
                    try
                    {
                        command.CommandText = Query;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }


                    Console.WriteLine("HighScore Added");

                }

            }
        }
        /// <summary>
        /// Adds the a ship and it's media files into the database.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cargo"></param>
        /// <param name="owner"></param>
        /// <param name="x_s"></param>
        /// <param name="y_s"></param>
        /// <param name="fileloc"></param>
        /// <param name="type"></param>
        /// <param name="reason"></param>
        public void NewShipandMedia(int model, int cargo, int owner, int x_s, int y_s, string fileloc, int type, string reason)
        {
            if (Check())
            {
                string Query1 = String.Format("INSERT INTO ship (Model, Health_Level, Cargo_Level, Owner) VALUES ({0},100,{1},{2}); ", model, cargo, owner);
                string Query2 = String.Format("INSERT INTO media (X_size, Y_size,file_Loc,Media_type) VALUES ({3},{4},{5},{6});", x_s,y_s,fileloc,type);
                string Query3 = String.Format("INSERT INTO shipmedia (Ship_id, Media_id, Reason) VALUES ((SELECT Ship_id FROM ship ORDER BY Ship_id DESC LIMIT 1), (SELECT Media_id FROM media ORDER BY Media_id DESC LIMIT 1), '{0}');", reason);
                using (SQLiteCommand command = new SQLiteCommand(dbc))
                {
                    try
                    {
                        command.CommandText = Query1;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                    try
                    {
                        command.CommandText = Query2;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                    try
                    {
                        command.CommandText = Query3;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }


                    Console.WriteLine("Ship, media and connect Added");

                }

            }
        }

        public void NewShipWithMedia(int model, int cargo, int owner, int media_id, string reason)
        {
            if(Check()){
                string Query1 = String.Format("INSERT INTO ship (Model, Health_Level, Cargo_Level, Owner) VALUES ({0},100,{1},{2}); ", model, cargo, owner);
                string Query2 = String.Format("INSERT INTO shipmedia (Ship_id, Media_id, Reason) VALUES ((SELECT Ship_id FROM ship ORDER BY Ship_id DESC LIMIT 1), {0}, '{1}');", media_id, reason);
                using (SQLiteCommand command = new SQLiteCommand(dbc))
                {
                    try
                    {
                        command.CommandText = Query1;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                    try
                    {
                        command.CommandText = Query2;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                    Console.WriteLine("Ship and connect Added");
                }

            }
        }






























    }
}
