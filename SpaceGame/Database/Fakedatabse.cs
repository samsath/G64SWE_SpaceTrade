using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDatabase
{
    public class Fakedatabse : IDatabase
    {
        public string stringData;
        public int intData;
        public void AddResourceToPlanet(int planet_id, int resource_id, int amount, int price)
        {
            intData = planet_id + resource_id;
        }

        public void AddResourceToShip(int ship_id, int resource_id, int amount, int bourghtPrice)
        {
            intData = ship_id + resource_id;
        }

        public bool Check()
        {
            return true;
        }

        public bool Close()
        {
            return true;
        }

        public bool Connect()
        {
            return true;
        }

        public void exeQuery(string[] query)
        {
            stringData = query[1];
        }

        public List<Userdata> getHighScore()
        {
            List<Userdata> result = new List<Userdata>();
            Userdata record = new Userdata(0, 1, "2", 3, 4);
            result.Add(record);

            return result;
        }

        public List<Mediadata> getMedia(string type, int id)
        {
            List<Mediadata> result = new List<Mediadata>();
            Mediadata record = new Mediadata(1, 2, 3, 4, "loc");
            result.Add(record);
            return result;
        }

        public List<Planetdata> getPlanet(int planet_id)
        {
            List<Planetdata> result = new List<Planetdata>();
            Planetdata record = new Planetdata(1, "planet", 3, 4, 5, "loc");
            result.Add(record);
            return result;
        }

        public List<Resourcedata> getResource(int resource_id)
        {
            List<Resourcedata> result = new List<Resourcedata>();
            Resourcedata record = new Resourcedata(1, "resour", 2, "descrp", 1, 2, 3, 4, 5, 6, "loc");
            result.Add(record);
            return result;
        }

        public List<Userdata> getSessionNum()
        {
            List<Userdata> result = new List<Userdata>();
            Userdata record = new Userdata(0, 1, "2", 3, 4);
            result.Add(record);

            return result;
        }

        public List<Shipdata> getShip(int ship_id)
        {
            List<Shipdata> result = new List<Shipdata>();
            Shipdata record = new Shipdata(0,1,2,3,4,5,6,"ex",1,2,3,"loc");
            result.Add(record);
            return result;
        }

        public List<Userdata> getUser()
        {
            List<Userdata> result = new List<Userdata>();
            Userdata record = new Userdata(0, 1, "2", 3, 4);
            result.Add(record);

            return result;
        }

        public bool newCreat()
        {
            return true;
        }

        public void NewPlanetMedia(string title, int x_loc, int y_loc, int diameter, int m_x_size, int m_y_size, string file_loc, int type)
        {
            stringData = title;
        }

        public void NewPlanetOldMedia(string title, int x_loc, int y_loc, int diameter, int media_id)
        {
            stringData = title;
        }

        public void NewResourceMedia(string resource, int initialprice, string descript, int x_s, int y_s, string fileloc, int type)
        {
            stringData = resource;
        }

        public void NewResourceOldMedia(string resource, int initialprice, string descript, int media_id)
        {
            stringData = resource;
        }

        public void NewShipandMedia(int model, int cargo, int owner, int x_s, int y_s, string fileloc, int type, string reason)
        {
            intData = model;
        }

        public void NewShipWithMedia(int model, int cargo, int owner, int media_id, string reason)
        {
            intData = model;
        }

        public List<Resourcedata> PlanetResources(int planetId)
        {
            List<Resourcedata> result = new List<Resourcedata>();
            Resourcedata record = new Resourcedata(planetId, "resour", 2, "descrp", 1, 2, 3, 4, 5, 6, "loc");
            result.Add(record);
            return result;
        }

        public void PlanetResourceUpdate(int planet_id, int resource_id, int amount, int price)
        {
            intData = planet_id;
        }

        public void SetHighscore(int id, int score)
        {
            intData = id + score;
        }

        public void SetHighscore(string name, int score)
        {
            stringData = name;
        }

        public void SetUser(string name, int money)
        {
            stringData = name;
        }

        public void SetUserMoney(int user_id, int money)
        {
            intData = user_id + money;
        }

        public void ShipAdd(int ship_id, int Ammo_level, int Health_level, int Cargo_level, int Fuel_level)
        {
            intData = ship_id;
        }

        public void ShipCargoUpdate(int ship_id, int resource_id, int amount, int price)
        {
            intData = ship_id;
        }

        public void ShipLoc(int ship_id, int x_loc, int y_loc)
        {
            intData = ship_id;
        }

        public List<Resourcedata> ShipResources(int shipId)
        {
            List<Resourcedata> result = new List<Resourcedata>();
            Resourcedata record = new Resourcedata(shipId, "resour", 2, "descrp", 1, 2, 3, 4, 5, 6, "loc");
            result.Add(record);
            return result;
        }

        public List<int> SessionPlanet(int sessionid)
        {
            List<int> result = new List<int>();
            result.Add(1);
            result.Add(2);
            result.Add(3);
            result.Add(4);
            return result;
        }
        public List<int> SessionResource(int sessionid)
        {
            List<int> result = new List<int>();
            result.Add(1);
            result.Add(2);
            result.Add(3);
            result.Add(4);
            return result;
        }
        public List<int> SessionShip(int sessionid)
        {
            List<int> result = new List<int>();
            result.Add(1);
            result.Add(2);
            result.Add(3);
            result.Add(4);
            return result;
        }
        public void AddPlanettoSession(int session_id)
        {
            intData = session_id;
        }

        public void AddResourcetoSession(int session_id)
        {
            intData = session_id;
        }

        public void AddShiptoSession(int session_id)
        {
            intData = session_id;
        }

        public int getLastSession()
        {
            return 1;
        }

        public List<Planetdata> SessionWithPlanet(int session)
        {
            List<Planetdata> result = new List<Planetdata>();
            Planetdata record = new Planetdata(1, "planet", 3, 4, 5, "loc");
            result.Add(record);
            return result;
        }

    }
}
