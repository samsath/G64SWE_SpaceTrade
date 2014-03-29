using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDatabase
{
    /// <summary>
    /// This is a file full of the data types which will allow us to pass data. 
    /// </summary>
   
    /// <summary>
    /// This is to store the User information
    /// </summary>
    public class Userdata
    {
        
        public int User_id { get; set; }
        public int Session_id { get; set; }
        public string Name { get; set; }
        public int Money { get; set; }
        public int HighScore { get; set; }
        public int Turns { get; set; }

        public Userdata(int user, int session, string name, int money, int highscore,int turns)
        {
            User_id = user;
            Session_id = session;
            Name = name;
            Money = money;
            Turns = turns;
        }

    
    }

    public class Shipdata
    {
        public int Ship_id { get; set; }
        public int Model { get; set; }
        public int Ammo_Level { get; set; }
        public int Health_Level { get; set; }
        public int Cargo_Level { get; set; }
        public int Fuel_Level { get; set; }
        public int Owner { get; set; }
        public string Extensions { get; set; }
        public int X_loc { get; set; }
        public int Y_loc { get; set; }
        public int Media_id { get; set; }
        public string file_loc { get; set; }

        public Shipdata(int Ship, int model, int Ammo, int Health, int Cargo, int Fuel, int owner, string extensions, int x_loc, int y_loc, int media, string file)
        {
            Ship_id = Ship;
            Model = model;
            Ammo_Level = Ammo;
            Health_Level = Health;
            Cargo_Level = Cargo;
            Fuel_Level = Fuel;
            Owner = owner;
            Extensions = extensions;
            X_loc = x_loc;
            Y_loc = y_loc;
            Media_id = media;
            file_loc = file;
        }
    }

    public class Planetdata
    {
        public int Planet_id { get; set; }
        public string Name { get; set; }
        public int X_loc { get; set; }
        public int Y_loc { get; set; }
        public int Media_id { get; set; }
        public string File_loc { get; set; }
        public Planetdata(int planet_id, string name, int x_loc, int y_loc, int media_id, string file_loc)
        {
            Planet_id = planet_id;
            Name = name;
            X_loc = x_loc;
            Y_loc = y_loc;
            Media_id = media_id;
            File_loc = file_loc;
        }
    }

    public class Resourcedata
    {
        public int Resource_id { get; set; }
        public string Name { get; set; }
        public int Initial_Price { get; set; }
        public string Description { get; set; }
        public int Planet_id { get; set; }
        public int User_id { get; set; }
        public int Ship_id { get; set; }
        public int Amount { get; set; }
        public int Price{get;set;}

        public int Media_id { get; set; }
        public string File_loc { get; set; }

        public Resourcedata(int resource, string name, int inital_price, string descript, int planet_id, int user_id, int ship_id, int amount ,int price, int media_id, string file_loc)
        {
            Resource_id = resource;
            Name = name;
            Initial_Price = inital_price;
            Description = descript;
            Planet_id = planet_id;
            User_id = user_id;
            Ship_id = ship_id;
            Amount = amount;
            Price = price;
            Media_id = media_id;
            File_loc = file_loc;
        }
    }

    public class Mediadata
    {
        public int Media_int { get; set; }
        public int X_size { get; set; }
        public int Y_size { get; set; }
        public int Lenght { get; set; }
        public string File_loc { get; set; }

        public Mediadata(int mediaid, int x_size, int y_size, int lenght, string fileloc)
        {
            Media_int = mediaid;
            X_size = x_size;
            Y_size = y_size;
            Lenght = lenght;
            File_loc = fileloc;

        }
    }

}
