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
    class Database : STDatabase.IDatabase, STDatabase.IDatabase
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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will get you the user information, from their UserID.
        /// </summary>
        /// <param name="Userid"></param>
        /// <returns> User Name, and User Money</returns>
        public string[,] GetUser(int Userid)
        {
            //SELECT * FROM Users WHERE Users_id = {0}; User_id
            String[,] result = new String[][];
            return result;
        }

        /// <summary>
        /// This will get you the user information, from their UserID.
        /// </summary>
        /// <param name="Username"></param>
        /// <returns>Userid and User money</returns>
        public string[,] GetUser(string Username)
        {
            //SELECT * FROM Users WHERE Name = {0}; Username
            String[,] result = new String[][];
            return result;
        }

        /// <summary>
        /// This returns the ship information.
        /// </summary>
        /// <param name="Ship_id"></param>
        /// <param name="par"></param>
        /// <returns>Model, Ammo_level, Health_Level, Cargo_Level, Fuel_Level, Owner, Extensions, x_loc, y_loc, media_id, X_size. Y_size, file_location, Lenghtm type</returns>
        public string[,] getShip(int Ship_id, string par="*")
        {
            /*SELECT * FROM Ship, Ship_to_Media, Media WHERE(
	           Ship.Ship_id == Ship_to_Media.Ship_id AND
	            Ship_to_Media.Media_id == Media.Media_id ) 
	            AND Ship_id = {0}; 
             */
            String[,] result = new String[][];
            return result;
        }

        /// <summary>
        /// Gets the planet information form the Planet_id
        /// </summary>
        /// <param name="Planet_id"></param>
        /// <returns> Name, X_loc, Y_loc, Diamater, media_id, X_size. Y_size, file_location, Lenght, type</returns>
        public string[,] getPlanet(int Planet_id)
        {
            /*SELECT * FROM Planet, Planet_to_Media, Media WHERE ( 
	            Planet.Planet_id == Planet_to_Media.Planet_id AND
	            Planet_to_Media.Planet_id == Media.Media_id
	            ) AND Planet_id = {0};
            */
            String[,] result = new String[][];
            return result;
        }
        /// <summary>
        /// Get Resource Information form Resoruce ID
        /// </summary>
        /// <param name="Resource_id"></param>
        /// <returns>Name, Initial_Price, Description, media_id, X_size. Y_size, file_location, Lenght, type</returns>
        public string[,] getResources(int Resource_id)
        {
            /* SELECT * FROM Resources, Resources_to_Media, Media WHERE (
	            Resources.Resource_id == Resources_to_Media.Resource_id AND 
	            Resources_to_Media.Media_id == Media.Media_id)
	            AND Resources_id = {0};
             */
            String[,] result = new String[][];
            return result;
        }
        /// <summary>
        /// Get media information by Media_id
        /// </summary>
        /// <param name="Media_id"></param>
        /// <returns>media_id, X_size. Y_size, file_location, Lenght, type</returns>
        public string[,] getMedia(int Media_id)
        {
            /*
             * SELECT * FROM Media WHERE Media_id = {0};
             */
            String[,] result = new String[][];
            return result;
        }
        /// <summary>
        /// Gets alls the information from the high score.
        /// </summary>
        /// <returns>User_id, Name, Money, HightScore_id, Score</returns>
        public string[,] getHighScore()
        {
            /*
             * SELECT * FROM Users, HighScore WHERE Users.Users_id == HighScore.Users_id;
             */
            String[,] result = new String[][5];
            return result;
        }

        /// <summary>
        /// This gets EVERYTHING that the ship will have which is owned by the User.
        /// It will also not be in this order.
        /// </summary>
        /// <param name="User_id"></param>
        /// <returns>User_id, Name, Money, Ship_id, Model, Ammo_level, Health_Level, Cargo_Level, Fuel_Level, Owner, Extensions, x_loc, y_loc, media_id, X_size. Y_size, file_location, Lenght, type
        /// Resource_id, Name, Initial_Price, Description, Amount, Bought_Price
        /// </returns>
        public string[,] getShipResource(int User_id)
        {
            /*
             * SELECT * FROM Users, Ship, Ship_to_Resource, Resources, Ship_to_Media, Media WHERE ( 
	            Users.Users_id == Ship.Owner AND 
	            Ship.Ship_id == Ship_to_Resource.Ship_id AND 
	            Ship_to_Resource.Resource_id == Resource.Resource_id AND
	            Ship.Ship_id == Ship_to_Media.Ship_id AND
	            Ship_to_Media.Media_id == Media.Media_id) 
	            AND Users_id = {0};
             */
            String[,] result = new String[][];
            return result;
        }

        /// <summary>
        /// Gets the planet information with it's resources.
        /// </summary>
        /// <param name="Planet_id"></param>
        /// <returns>PR_id, Planet_id, Resource_id, Price, (P)Title, X_loc, Y_loc, Diamater, (R)Name, Initial_Price, Description</returns>
        public string[,] getPlanetResource(int Planet_id)
        {
            /*
             * SELECT Resources.Name, Planet_to_Resource.Price FROM Planet, Planet_to_Resource, Resources WHERE (
	            Resources.Resources_id == Planet_to_Resources.Resources_id AND
	            Planet_to_Resources.Planet_id == Planet.Planet_id) 
	            AND Planet.Planet_id = {0} OR Planet.Title = {0};
             */
            String[,] result = new String[][];
            return result;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Adds the user to the system.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Money"></param>
        public void addUser(string Name, int Money)
        {
            /*
             * INSERT INTO Users (Name, Money) VALUES({0},{1});
             */
        }

        /// <summary>
        /// Adds the user's to the HighScore board.
        /// </summary>
        /// <param name="User_id"></param>
        /// <param name="Score"></param>
        public void addHighscore(int User_id, int Score)
        {
            /*
             * INSERT INTO HighScore (Users_id, Score) VALUES({0},{1});
             */
        }

        /// <summary>
        /// This adds a new ship to the database.
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="Cargo_Level"></param>
        /// <param name="User_id"></param>
        /// <param name="x_size"></param>
        /// <param name="y_size"></param>
        /// <param name="file_locationn"></param>
        /// <param name="types"></param>
        public void addNewShip(int Model, int Cargo_Level, int User_id, int x_size, int y_size, string file_locationn, int types )
        {
            /*
             * INSERT INTO Ship (Model, Health_Level, Cargo_Level, Owner) VALUES ({0},100,{1},{2});
                INSERT INTO Media (x_size, y_size,file_location) VALUES ({3},{4},{5},{6});
                INSERT INTO Ship_to_Media (Ship_id, Media_id, Reason) VALUES ((SELECT Ship_id FROM Ship ORDER BY Ship_id DESC LIMIT 1), 
             * (SELECT Media_id FROM Media ORDER BY Media_id DESC LIMIT 1), {6});
             */
        }

        /// <summary>
        /// Creates ship and the effiliated media file.
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="Cargo_Level"></param>
        /// <param name="User_id"></param>
        /// <param name="file_locationn"></param>
        /// <param name="types"></param>
        public void addShipWMedia(int Model, int Cargo_Level, int User_id, string file_locationn, int types)
        {
            /*
             *INSERT INTO Ship (Model, Health_Level, Cargo_Level, Owner) VALUES ({0},100,{1},{2});
                INSERT INTO Ship_to_Media (Ship_id, Media_id, Reason) VALUES ((SELECT Ship_id FROM Ship ORDER BY Ship_id DESC LIMIT 1),
             * (SELECT Media_id FROM Media, Ship_2_Resources, Ship WHERE
	            (Ship.Ship_id == Ship_to_Media.Ship_id AND
	            Ship_to_Media.Media_id == Media.Media_id) AND Ship.Model = {3}), {0});
             */
        }

        /// <summary>
        /// Add new Resources to the database.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="InitalPrice"></param>
        /// <param name="Descript"></param>
        /// <param name="x_size"></param>
        /// <param name="y_size"></param>
        /// <param name="file_locationn"></param>
        /// <param name="types"></param>
        public void addNewResources(string Name, int InitalPrice, string Descript, int x_size, int y_size, string file_locationn, int types)
        {
            /*
             *  INSERT INTO Resources (Name, Inital_Price, Description) VALUES ({0},{1},{2});
                INSERT INTO Media (x_size, y_size,file_location,types) VALUES ({3},{4},{5},{6});
                INSERT INTO Resources_to_Media (Resourcs_id, Media_id) VALUES ((SELECT Resources_id FROM Resources ORDER BY Resources_id DESC LIMIT 1),
             * (SELECT Media_id FROM Media ORDER BY Media_id DESC LIMIT 1), {6});
             */
        }

        /// <summary>
        /// Add resource where the media files are already located in the database. By the use of Resource_ID
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="InitalPrice"></param>
        /// <param name="Descript"></param>
        /// <param name="Resources_id"></param>
        public void addResourcesWMedia(string Name, int InitalPrice, string Descript, int Resources_id)
        {
            /*
            * INSERT INTO Resources (Name, Inital_Price, Description) VALUES ({0},{1},{2});
            INSERT INTO Resources_to_Media(Resources_id,Media_id) VALUES ((SELECT Resources_id FROM Resources ORDER BY Resources_id DESC LIMIT 1),
             * (SELECT Resources_to_Media.Media_id FROM Resources_to_Media, Resources WHERE
	        Resources.Resources_id == Resources_to_Media.Resources_id AND Resources.Resources_id = {3}));
             */
        }

        /// <summary>
        ///  Add resource where the media files are already located in the database. By the use of Resource_Name
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="InitalPrice"></param>
        /// <param name="Descript"></param>
        /// <param name="Resources_Name"></param>
        public void addResourcesWMedia(string Name, int InitalPrice, string Descript, string Resources_Name)
        {
            /*
            * INSERT INTO Resources (Name, Inital_Price, Description) VALUES ({0},{1},{2});
            INSERT INTO Resources_to_Media(Resources_id,Media_id) VALUES ((SELECT Resources_id FROM Resources ORDER BY Resources_id DESC LIMIT 1),(SELECT Resources_to_Media.Media_id FROM Resources_to_Media, Resources WHERE
	        Resources.Resources_id == Resources_to_Media.Resources_id AND  Resources.Name = {3}));
             */
        }
        
        /// <summary>
        /// Adds a new Planet with new media file.
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="X_loc"></param>
        /// <param name="Y_Loc"></param>
        /// <param name="Diameter"></param>
        /// <param name="x_size"></param>
        /// <param name="y_size"></param>
        /// <param name="fileLocation"></param>
        /// <param name="type"></param>
        public void addNewPlanet(string Title, int X_loc, int Y_Loc, int Diameter, int x_size, int y_size, string fileLocation, int type )
        {
            /*
             * INSERT INTO Planet (Title, X_loc,Y_loc,Diameter) VALUES ({0},{1},{2},{3});
                INSERT INTO Media (x_size, y_size,file_location,types) VALUES ({4},{5},{6},{7});
                INSERT INTO Planet_to_Media(Planet_id,Media_id) VALUES ((SELECT Planet_id FROM Planet ORDER BY Planet_id DESC LIMIT 1), 
             * (SELECT Media_id FROM Media ORDER BY Media_id DESC LIMIT 1));
             */
        }

        /// <summary>
        /// Adds Planet info to the database which already has the media files.
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="X_loc"></param>
        /// <param name="Y_Loc"></param>
        /// <param name="Diameter"></param>
        /// <param name="fileLocation"></param>
        public void addPlanetWMedia(string Title, int X_loc, int Y_Loc, int Diameter, string fileLocation)
        {
            /*
             * INSERT INTO Planet (Title, X_loc,Y_loc,Diameter) VALUES ({0},{1},{2},{3});
                INSERT INTO Planet_to_Media(Planet_id,Media_id) VALUES ((SELECT Planet_id FROM Planet ORDER BY Planet_id DESC LIMIT 1), (SELECT Media_id FROM Media WHERE file_location = {4}));
             */
        }

        /// <summary>
        /// Adds resources to each ship.
        /// </summary>
        /// <param name="Ship_id"></param>
        /// <param name="Resource_id"></param>
        /// <param name="amount"></param>
        /// <param name="BoughtPrice"></param>
        public void addResourceToShip(int Ship_id, int Resource_id, int amount, int BoughtPrice)
        {
            /*
             * INSERT INTO Ship_to_Resource (Ship_id, Resource_id, amount,Bought_Price) VALUES (
	            (SELECT Ship_id FROM Ship WHERE (Owner = {1} AND Ship_id = {0})),
	            (SELECT Resource_id FROM Resources WHERE Name = {2}),
	            {3}, {4});
                UPDATE Ship SET Ship.Cargo_Level = Ship.Cargo_Level - {3} WHERE (SELECT Ship_id FROM Ship WHERE (Owner = {1} AND Ship_id = {0}));
             */
        }

        /// <summary>
        /// Adds resources to Planets
        /// </summary>
        /// <param name="Planet_id"></param>
        /// <param name="Resource_id"></param>
        /// <param name="Price"></param>
        public void addResourceToPlanet(int Planet_id, int Resource_id, int Price)
        {
            /*
             * INSERT INTO Planet_to_Resource (Planet_id, Resource_id, Price) VALUES (
	            (SELECT Planet_id FROM Planet WHERE (Planet.Title = {1} OR Planet.Planet_id ={2})),
	            (SELECT Resource_id FROM Resources WHERE (Resources.Name = {3} OR Resources.Resources_id = {4})),{5});
            */
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// Updates the amount of money the user has.
        /// </summary>
        /// <param name="User_id"></param>
        /// <param name="Money"></param>
        public void updateUserMoney(int User_id, int Money)
        {
            /*
             * UPDATE Users SET money = {0} WHERE Users_id = {1};
             */
        }

        /// <summary>
        /// Updates the ships information by these paramaters.
        /// </summary>
        /// <param name="Ship_id"></param>
        /// <param name="Ammo_Level"></param>
        /// <param name="Health_Level"></param>
        /// <param name="Cargo_Level"></param>
        /// <param name="Fuel_level"></param>
        public void updateShipStats(int Ship_id, int Ammo_Level, int Health_Level, int Cargo_Level, int Fuel_level)
        {
            /*
             * UPDATE Ship SET Ammo_Level = {0}, Health_Level ={1}, Cargo_Level={2}, Fuel_Level={3} WHERE Ship_id = {5};
             */
        }

        /// <summary>
        /// Updates the cargo each ship holds.
        /// </summary>
        /// <param name="Ship_id"></param>
        /// <param name="Resource_id"></param>
        /// <param name="amount"></param>
        /// <param name="BoughtPrice"></param>
        public void updateShipCargo(int Ship_id, int Resource_id, int amount, int BoughtPrice)
        {
            /*
             *UPDATE Ship_to_Resource SET amount = {0}, Bought_Price = {1} WHERE (Resource_id = {2} AND Ship_id = {3}));
                UPDATE Ship SET Cargo_Level = Cargo_Level + {0} WHERE Ship_id = {4};
             */
        }

        /// <summary>
        /// Updates the ships location
        /// </summary>
        /// <param name="Ship_id"></param>
        /// <param name="x_loc"></param>
        /// <param name="y_loc"></param>
        public void updateShipLoc(int Ship_id, int x_loc, int y_loc)
        {
            /*
             * UPDATE Ship SET x_loc = {0}, y_loc = {1} WHERE Ship_id = {2};
             */
        }

        /// <summary>
        /// Updates the Planets Resources.
        /// </summary>
        /// <param name="Planet_id"></param>
        /// <param name="Resource_id"></param>
        /// <param name="Price"></param>
        public void updatePlanetResources(int Planet_id, int Resource_id, int Price)
        {
            /*
             * UPDATE Planet_to_Resource SET Price = {0} WHERE Planet_id = {1} AND Resource_id ={2}; 
             */
        }
    }
}
