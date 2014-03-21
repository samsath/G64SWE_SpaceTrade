using System;
namespace STDatabase
{
    public interface IDatabase
    {
        void AddResourceToPlanet(int planet_id, int resource_id, int amount, int price);
        void AddResourceToShip(int ship_id, int resource_id, int amount, int bourghtPrice);
        bool Check();
        bool Close();
        bool Connect();
        void exeQuery(string[] query);
        System.Collections.Generic.List<Userdata> getHighScore();
        System.Collections.Generic.List<Mediadata> getMedia(string type, int id);
        System.Collections.Generic.List<Planetdata> getPlanet(int planet_id);
        System.Collections.Generic.List<Resourcedata> getResource(int resource_id);
        System.Collections.Generic.List<Userdata> getSessionNum();
        System.Collections.Generic.List<Shipdata> getShip(int ship_id);
        System.Collections.Generic.List<Userdata> getUser();
        bool newCreat();
        void NewPlanetMedia(string title, int x_loc, int y_loc, int diameter, int m_x_size, int m_y_size, string file_loc, int type);
        void NewPlanetOldMedia(string title, int x_loc, int y_loc, int diameter, int media_id);
        void NewResourceMedia(string resource, int initialprice, string descript, int x_s, int y_s, string fileloc, int type);
        void NewResourceOldMedia(string resource, int initialprice, string descript, int media_id);
        void NewShipandMedia(int model, int cargo, int owner, int x_s, int y_s, string fileloc, int type, string reason);
        void NewShipWithMedia(int model, int cargo, int owner, int media_id, string reason);
        System.Collections.Generic.List<Resourcedata> PlanetResources(int planetId);
        void PlanetResourceUpdate(int planet_id, int resource_id, int amount, int price);
        void SetHighscore(int id, int score);
        void SetHighscore(string name, int score);
        void SetUser(string name, int money);
        void SetUserMoney(int user_id, int money);
        void ShipAdd(int ship_id, int Ammo_level, int Health_level, int Cargo_level, int Fuel_level);
        void ShipCargoUpdate(int ship_id, int resource_id, int amount, int price);
        void ShipLoc(int ship_id, int x_loc, int y_loc);
        System.Collections.Generic.List<Resourcedata> ShipResources(int shipId);

        System.Collections.Generic.List<int> SessionPlanet(int sessionid);
        System.Collections.Generic.List<int> SessionResource(int sessionid);
        System.Collections.Generic.List<int> SessionShip(int sessionid);
    }
}
