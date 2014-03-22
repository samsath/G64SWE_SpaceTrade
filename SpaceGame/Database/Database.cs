using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data;

/*
 * The System.Data.Sqlite is located in the DatabaseDocSupportFiles/dll
 * 
 */


namespace STDatabase
{
    /// <summary>
    /// The location will need to be changed so that they work on every computer/
    /// </summary>
    public class Database : IDatabase
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
            dbpath = @"STDatabase.db";
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
            using (StreamReader sr = new StreamReader(@"database.sql"))
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

                command.CommandText = query;
                command.ExecuteNonQuery();

                try
                {
                    string UserQuery = "SELECT * FROM users";
                    command.CommandText = UserQuery;
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
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
                Connect();
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

        public void exeQuery(string[] query)
        {

            using (SQLiteCommand command = new SQLiteCommand(dbc))
            {
                for (int i = 0; i < query.Length; i++)
                {
                    try
                    {
                        command.CommandText = query[i];
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }
                }
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
                                Userdata record = new Userdata(rdq.GetInt32(0), rdq.GetInt32(1), rdq.GetString(2), rdq.GetInt32(3), 0);
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
                                Userdata record = new Userdata(rdq.GetInt32(0), rdq.GetInt32(1), rdq.GetString(2), 0, 0);
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
        /// This adds a user and their session into the database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="money"></param>
        public void SetUser(string name, int money)
        {
            if (Check())
            {
                string Query1 = String.Format("INSERT INTO users(Name) VALUES('{0}'); ", name);
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
                string Query = string.Format("SELECT ship.Ship_id, ship.Model, ship.Ammo_Level, ship.Health_Level, ship.Cargo_Level, ship.Fuel_Level, ship.Owner, ship.Extenstions, ship.x_loc, ship.y_loc, media.Media_id, media.file_Loc FROM ship, media, shipmedia WHERE ship.Ship_id == shipmedia.Ship_id AND shipmedia.Media_id == media.Media_id AND ship.Ship_id = {1};", ship_id);
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
                string Query = string.Format("SELECT planet.Planet_id, planet.Title, planet.X_loc, planet.Y_loc, media.Media_id, media.file_Loc FROM planet, media, planetmedia WHERE planet.Planet_id == planetmedia.Planet_id AND planetmedia.Media_id == media.Media_id AND planet.Planet_id = {0};", planet_id);
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {
                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                Planetdata record = new Planetdata(rdq.GetInt32(0), rdq.GetString(1), rdq.GetInt32(2), rdq.GetInt32(3), rdq.GetInt32(4), rdq.GetString(5));
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
        public List<Resourcedata> getResource(int resources_id)
        {
            List<Resourcedata> result = new List<Resourcedata>();
            if (Check())
            {
                string Query = string.Format("SELECT resources.Resources_id, resources.Name, resources.Initial_Price, resources.Description, media.Media_id, media.file_Loc FROM resources, resourcesmedia, media WHERE (resources.Resources_id == resourcesmedia.Resources_id AND resourcesmedia.Media_id == media.Media_id) AND resources.Resources_id == {0};", resources_id);
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
            switch (type)
            {
                case "R":
                    Query = string.Format("SELECT media.Media_id, media.X_size, media.Y_size, media.Lenght, media.File_loc FROM media, resourcesmedia WHERE resourcesmedia.Media_id == media.Media_id AND resourcesmedia.Resources_id == {0};", id);
                    break;
                case "S":
                    Query = string.Format("SELECT media.Media_id, media.X_size, media.Y_size, media.Lenght, media.File_loc FROM media, shipmedia WHERE shipmedia.Media_id == media.Media_id AND shipmedia.Ship_id == {0};", id);
                    break;
                case "P":
                    Query = string.Format("SELECT media.Media_id, media.X_size, media.Y_size, media.Lenght, media.File_loc FROM media, planetmedia WHERE planetmedia.Media_id == media.Media_id AND planetmedia.Planet_id == {0};", id);
                    break;
            }
            if (Check())
            {
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {
                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                Mediadata record = new Mediadata(rdq.GetInt32(0), rdq.GetInt32(1), rdq.GetInt32(2), rdq.GetInt32(3), rdq.GetString(4));
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
                string Query = string.Format("SELECT resources.Resources_id, resources.Name, resources.Initial_Price, resources.Description, shipresource.Amount, shipresource.Bought_Price FROM resource, shipresource WHERE resources.Resources_id == shipresource.Resources_id AND shipresource.Ship_id = {0};", shipId);
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
                string Query = string.Format("SELECT resources.Resources_id, resources.Name, resources.Initial_Price, resources.Description, planetresources.Price FROM resource, planetresources WHERE resources.Resources_id == planetresources.Resources_id AND planetresources.Planet_id = {0};", planetId);
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
                string[] Query = new string[1] { String.Format("INSERT INTO highscore(Users_id,Score) VALUES((SELECT Users_id FROM users WHERE Name = '{0}'),{1});", name, score) };

                exeQuery(Query);

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
                string[] Query = new string[1] { String.Format("INSERT INTO highscore(Users_id,Score) VALUES({0},{1});", id, score) };

                exeQuery(Query);

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
                string[] Query = new string[3] { 
                    String.Format("INSERT INTO ship (Model, Health_Level, Cargo_Level, Owner) VALUES ({0},100,{1},{2}); ", model, cargo, owner),
                    String.Format("INSERT INTO media (X_size, Y_size,file_Loc,Media_type) VALUES ({0},{1},'{2}',{3});", x_s,y_s,fileloc,type),
                    String.Format("INSERT INTO shipmedia (Ship_id, Media_id, Reason) VALUES ((SELECT Ship_id FROM ship ORDER BY Ship_id DESC LIMIT 1), (SELECT Media_id FROM media ORDER BY Media_id DESC LIMIT 1), '{0}');", reason)
                };

                exeQuery(Query);

            }
        }
        /// <summary>
        /// This adds new ships to the database where media info already exsists.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cargo"></param>
        /// <param name="owner"></param>
        /// <param name="media_id"></param>
        /// <param name="reason"></param>
        public void NewShipWithMedia(int model, int cargo, int owner, int media_id, string reason)
        {
            if (Check())
            {
                string[] Query = new string[2] {
                    String.Format("INSERT INTO ship (Model, Health_Level, Cargo_Level, Owner) VALUES ({0},100,{1},{2}); ", model, cargo, owner),
                    String.Format("INSERT INTO shipmedia (Ship_id, Media_id, Reason) VALUES ((SELECT Ship_id FROM ship ORDER BY Ship_id DESC LIMIT 1), {0}, '{1}');", media_id, reason)
                };

                exeQuery(Query);

            }
        }
        /// <summary>
        /// Adds a new resource to th database and the new media that goes along with it.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="initialprice"></param>
        /// <param name="descript"></param>
        /// <param name="x_s"></param>
        /// <param name="y_s"></param>
        /// <param name="fileloc"></param>
        /// <param name="type"></param>
        public void NewResourceMedia(string resource, int initialprice, string descript, int x_s, int y_s, string fileloc, int type)
        {
            if (Check())
            {
                string[] Query = new string[3]{
                    String.Format("INSERT INTO resources (Name, Initial_Price, Description) VALUES ('{0}',{1},'{2}');",resource, initialprice, descript),
                    String.Format("INSERT INTO media (X_size, Y_size,file_Loc,Media_type) VALUES ({0},{1},'{2}',{3});", x_s ,y_s ,fileloc ,type),
                    "INSERT INTO resourcesmedia (Resources_id, Media_id) VALUES ((SELECT Resources_id FROM Resources ORDER BY Resources_id DESC LIMIT 1), (SELECT Media_id FROM Media ORDER BY Media_id DESC LIMIT 1))"
                };
                /*
                for (int i = 0; i < Query.Length; i++)
                {
                    Console.WriteLine(Query[i]);
                }
                */
                    exeQuery(Query);

            }
        }
        /// <summary>
        /// Adds a new resource to the database which already has media in the database
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="initialprice"></param>
        /// <param name="descript"></param>
        /// <param name="media_id"></param>
        public void NewResourceOldMedia(string resource, int initialprice, string descript, int media_id)
        {
            if (Check())
            {
                string[] Query = new string[2]{
                    String.Format("INSERT INTO resources (Name, Initial_Price, Description) VALUES ('{0}',{1},'{2}');",resource, initialprice, descript),
                    String.Format( "INSERT INTO resourcesmedia (Resources_id, Media_id) VALUES ((SELECT Resources_id FROM Resources ORDER BY Resources_id DESC LIMIT 1), {0})",media_id )
                };

                exeQuery(Query);

            }
        }

        /// <summary>
        /// Add new planet with the new media to the databae
        /// </summary>
        /// <param name="title"></param>
        /// <param name="x_loc"></param>
        /// <param name="y_loc"></param>
        /// <param name="diameter"></param>
        /// <param name="m_x_size"></param>
        /// <param name="m_y_size"></param>
        /// <param name="file_loc"></param>
        /// <param name="type"></param>
        public void NewPlanetMedia(string title, int x_loc, int y_loc, int diameter, int m_x_size, int m_y_size, string file_loc, int type)
        {
            if (Check())
            {
                string[] query = new string[3]{
                    String.Format("INSERT INTO planet (Title, X_loc,Y_loc,Diameter) VALUES ('{0}',{1},{2},{3})",title, x_loc, y_loc, diameter),
                    String.Format("INSERT INTO media (X_size, Y_size,file_Loc,Media_type) VALUES ({0},{1},'{2}',{3});", m_x_size ,m_y_size ,file_loc ,type),
                    "INSERT INTO planetmedia(Planet_id,Media_id) VALUES ((SELECT Planet_id FROM Planet ORDER BY Planet_id DESC LIMIT 1), (SELECT Media_id FROM Media ORDER BY Media_id DESC LIMIT 1))"
                };

                exeQuery(query);
            }
        }

        /// <summary>
        /// Adds new planet to the database which already has the media.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="x_loc"></param>
        /// <param name="y_loc"></param>
        /// <param name="diameter"></param>
        /// <param name="media_id"></param>
        public void NewPlanetOldMedia(string title, int x_loc, int y_loc, int diameter, int media_id)
        {
            if (Check())
            {
                string[] query = new string[2] {
                    String.Format("INSERT INTO planet (Title, X_loc,Y_loc,Diameter) VALUES ('{0}',{1},{2},{3})",title, x_loc, y_loc, diameter),
                    String.Format("INSERT INTO planetmedia(Planet_id,Media_id) VALUES ((SELECT Planet_id FROM Planet ORDER BY Planet_id DESC LIMIT 1), {0})",media_id)
                };

                exeQuery(query);

            }
        }
        /// <summary>
        /// Add new resource to the ship
        /// </summary>
        /// <param name="ship_id"></param>
        /// <param name="resource_id"></param>
        /// <param name="amount"></param>
        /// <param name="bourghtPrice"></param>
        public void AddResourceToShip(int ship_id, int resource_id, int amount, int bourghtPrice)
        {
            if (Check())
            {
                string[] query = new string[2] {
                    String.Format("INSERT INTO shipresource (Ship_id, Resource_id, amount, Bought_Price) VALUES ({0},{1},{2},{3})", ship_id, resource_id, amount, bourghtPrice),
                    String.Format("UPDATE ship SET Ship.Cargo_Level = Ship.Cargo_Level - {0} WHERE Ship_id = {1}", amount, ship_id)
                };

                exeQuery(query);

                Console.WriteLine("Resource added to Ship");

            }
        }

        /// <summary>
        /// Add new resources to the planet
        /// </summary>
        /// <param name="planet_id"></param>
        /// <param name="resource_id"></param>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        public void AddResourceToPlanet(int planet_id, int resource_id, int amount, int price)
        {
            if (Check())
            {
                string[] query = new string[1] { String.Format("INSERT INTO planetresources (Planet_id, Resources_id, Amount, Price) VALUSE ({0}, {1}, {2}, {3})", planet_id, resource_id, amount, price) };

                exeQuery(query);

                Console.WriteLine("Resource added to Planet");
            }
        }

        /// <summary>
        /// updates the money on a users account
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="money"></param>
        public void SetUserMoney(int user_id, int money)
        {
            if (Check())
            {
                string[] query = new string[1] { String.Format("UPDATE users SET money = {0} WHERE Users_id = {1}", money, user_id) };

                exeQuery(query);

                Console.WriteLine("User has more money");
            }
        }

        /// <summary>
        /// Update ship information like helath, ammo, and cargo
        /// </summary>
        /// <param name="ship_id"></param>
        /// <param name="Ammo_level"></param>
        /// <param name="Health_level"></param>
        /// <param name="Cargo_level"></param>
        /// <param name="Fuel_level"></param>
        public void ShipAdd(int ship_id, int Ammo_level, int Health_level, int Cargo_level, int Fuel_level)
        {
            if (Check())
            {
                string[] query = new string[1] { String.Format("UPDATE ship SET Ammo_Level = {0}, Health_Level = {1}, Cargo_Level = {2}, Fuel_Level = {3} WHERE Ship_id = {4}", Ammo_level, Health_level, Cargo_level, Fuel_level, ship_id) };

                exeQuery(query);

                Console.WriteLine("Ship stats updated");
            }
        }

        /// <summary>
        /// Updates the reosurce the ship has in the cargo if it already has it.
        /// </summary>
        /// <param name="ship_id"></param>
        /// <param name="resource_id"></param>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        public void ShipCargoUpdate(int ship_id, int resource_id, int amount, int price)
        {
            if (Check())
            {
                string[] query = new string[1] { String.Format("UPDATE shipresource SET amount = {0}, Bought_Price = {1} WHERE Ship_id = {2} AND Resources_id = {3}", amount, price, ship_id, resource_id) };

                exeQuery(query);

                Console.WriteLine("Ship Cargo updated");
            }
        }

        /// <summary>
        /// updates the ships location
        /// </summary>
        /// <param name="ship_id"></param>
        /// <param name="x_loc"></param>
        /// <param name="y_loc"></param>
        public void ShipLoc(int ship_id, int x_loc, int y_loc)
        {
            if (Check())
            {
                string[] query = new string[1] { String.Format("UPDATE ship SET x_loc = {0}, y_loc = {1} WHERE Ship_id = {2}", x_loc, y_loc, ship_id) };

                exeQuery(query);

                Console.WriteLine("Ship Location updated");
            }
        }

        /// <summary>
        /// Updates the resources on a planet
        /// </summary>
        /// <param name="planet_id"></param>
        /// <param name="resource_id"></param>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        public void PlanetResourceUpdate(int planet_id, int resource_id, int amount, int price)
        {
            if (Check())
            {
                string[] query = new string[1] { String.Format("UPDATE planetresources SET Amount = {0}, Price = {1} WHERE (Planet_id = {2} AND Resources_id = {3})", amount, price, planet_id, resource_id) };

                exeQuery(query);

                Console.WriteLine("Ship Location updated");
            }
        }

        /// <summary>
        /// This will return the list of planets on that session.
        /// </summary>
        /// <param name="sessionid"></param>
        /// <returns>Array List of int(planet id's)</returns>
        public List<int> SessionPlanet(int sessionid)
        {
            List<int> result = new List<int>();
            if (Check())
            {
                string Query = String.Format("SELECT Planet_id FROM sessiontoplanet WHERE Session_id = {0}", sessionid);
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {

                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                result.Add(rdq.GetInt32(0));
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }
            }

            return result;
        }

        /// <summary>
        /// This will return a list of ships in this session.
        /// </summary>
        /// <param name="sessionid"></param>
        /// <returns>Array List of int(ship id's)</returns>
        public List<int> SessionShip(int sessionid)
        {
            List<int> result = new List<int>();
            if (Check())
            {
                string Query = String.Format("SELECT Ship_id FROM sessiontoship WHERE Session_id = {0}", sessionid);
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {

                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                result.Add(rdq.GetInt32(0));
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }
            }

            return result;
        }

        /// <summary>
        /// This will return a list of resources in this session.
        /// </summary>
        /// <param name="sessionid"></param>
        /// <returns>Array List of int(ship id's)</returns>
        public List<int> SessionResource(int sessionid)
        {
            List<int> result = new List<int>();
            if (Check())
            {
                string Query = String.Format("SELECT Resource_id FROM sessiontoresources WHERE Session_id = {0}", sessionid);
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {

                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                result.Add(rdq.GetInt32(0));
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }
            }

            return result;
        }

        /// <summary>
        /// This adds all the planets not currently with a session to a session id
        /// </summary>
        /// <param name="session_id"></param>
        public void AddPlanettoSession(int session_id)
        {
            if (Check())
            {
                List<int> result = new List<int>();
                string Query = "SELECT Planet_id FROM planet WHERE Planet_id NOT IN (SELECT Planet_id FROM sessiontoplanet)";
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {
                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                int record = rdq.GetInt32(0);
                                result.Add(record);
                                Console.WriteLine(record);
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }
                for (int i = 0; i < result.Count; i++)
                {
                    try
                    {
                        string[] query = new String[1] { String.Format("INSERT INTO sessiontoplanet (Session_id, Planet_id) VALUES({0},{1})", session_id, result[i]) };
                        exeQuery(query);
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }
                }
            }
        }

        /// <summary>
        /// This adds all the resources not currently with a session to a session id
        /// </summary>
        /// <param name="session_id"></param>
        public void AddResourcetoSession(int session_id)
        {
            if (Check())
            {
                List<int> result = new List<int>();
                string Query = "SELECT Resources_id FROM resources WHERE Resources_id NOT IN (SELECT Resource_id FROM sessiontoresources)";
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {
                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                int record = rdq.GetInt32(0);
                                result.Add(record);
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }
                for (int i = 0; i < result.Count; i++)
                {
                    try
                    {
                        string[] query = new String[1] { String.Format("INSERT INTO sessiontoresources (Session_id, Resource_id) VALUES({0},{1})", session_id, result[i]) };
                        exeQuery(query);
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }
                }
            }
        }

        /// <summary>
        /// This adds all the ships not currently with a session to a session id
        /// </summary>
        /// <param name="session_id"></param>
        public void AddShiptoSession(int session_id)
        {
            if (Check())
            {
                List<int> result = new List<int>();
                string Query = "SELECT Ship_id FROM ship WHERE Ship_id NOT IN (SELECT Ship_id FROM sessiontoship)";
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {
                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                int record = rdq.GetInt32(0);
                                result.Add(record);
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }
                for (int i = 0; i < result.Count; i++)
                {
                    try
                    {
                        string[] query = new String[1] { String.Format("INSERT INTO sessiontoship (Session_id, Ship_id) VALUES({0},{1})", session_id, result[i]) };
                        exeQuery(query);
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }
                }
            }
        }

        /// <summary>
        /// Gets the last session id.
        /// </summary>
        /// <returns></returns>
        public int getLastSession()
        {
            int lastsession = 0;
            if (Check())
            {
                string Query = "SELECT Session_id FROM sessions ORDER BY Session_id DESC LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(Query, dbc))
                {
                    try
                    {
                        using (SQLiteDataReader rdq = command.ExecuteReader())
                        {
                            while (rdq.Read())
                            {
                                lastsession = rdq.GetInt32(0);
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Error " + ex); }

                }

            }

            return lastsession;
        }


    }

}
