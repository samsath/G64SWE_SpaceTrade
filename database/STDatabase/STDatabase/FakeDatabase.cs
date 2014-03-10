using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDatabase
{
    public class FakeDatabase : IDatabase
    {
        public string data; // this is to place everything;
        public int number;
        void addHighscore(int User_id, int Score)
        {
            int uI = User_id;
            int Scr = Score;
            number = Scr;
        }
        void addNewPlanet(string Title, int X_loc, int Y_Loc, int Diameter, int x_size, int y_size, string fileLocation, int type);
        void addNewResources(string Name, int InitalPrice, string Descript, int x_size, int y_size, string file_locationn, int types);
        void addNewShip(int Model, int Cargo_Level, int User_id, int x_size, int y_size, string file_locationn, int types);
        void addPlanetWMedia(string Title, int X_loc, int Y_Loc, int Diameter, string fileLocation);
        void addResourcesWMedia(string Name, int InitalPrice, string Descript, int Resources_id);
        void addResourcesWMedia(string Name, int InitalPrice, string Descript, string Resources_Name);
        void addResourceToPlanet(int Planet_id, int Resource_id, int Price);
        void addResourceToShip(int Ship_id, int Resource_id, int amount, int BoughtPrice);
        void addShipWMedia(int Model, int Cargo_Level, int User_id, string file_locationn, int types);
        void addUser(string Name, int Money);
        string[,] getHighScore();
        string[,] getMedia(int Media_id);
        string[,] getPlanet(int Planet_id);
        string[,] getPlanetResource(int Planet_id);
        string[,] getResources(int Resource_id);
        string[,] getShip(int Ship_id, string par = "*");
        string[,] getShipResource(int User_id);
        string[,] GetUser(int Userid);
        string[,] GetUser(string Username);
        void updatePlanetResources(int Planet_id, int Resource_id, int Price);
        void updateShipCargo(int Ship_id, int Resource_id, int amount, int BoughtPrice);
        void updateShipLoc(int Ship_id, int x_loc, int y_loc);
        void updateShipStats(int Ship_id, int Ammo_Level, int Health_Level, int Cargo_Level, int Fuel_level);
        void updateUserMoney(int User_id, int Money);
    }
}
