using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using STDatabase;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using XRpgLibrary;
using XRpgLibrary.Controls;



namespace SpaceGame.Components
{
    public class DatabasePopulate
    {
        public Boolean addplanet = true;
        public Boolean addResource = true;
        public Boolean Resources;
        public int sessionNumber = 0;
        public int resourceCount = 1;
        public int planetscreated = 1;
        public Boolean addedPlanettoSession = true;
        public Boolean addedRestoSession = true;

        public List<Tile> board { get; set; }
        public SpaceShip ship { get; set; }
        /*
         * This is to try and populate the database when a new game starts. It is done here so that it can easily be removed or changed if need by as maybe
         * threaded if it slows down the process.
         */
        public IDatabase dbs = new Database();
        public IDatabase dbf = new Fakedatabse();
        /// <summary>
        /// This populate the database with resources from the xml document in the content fold, by reading the item first then passing them to the database. 
        /// Done on an xml so it can be added over time and not effect how the game loads.
        /// </summary>
        /// <returns></returns>
        public Boolean Startresourceadd()
        {
            Resources = true;
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Content\resourceListCostDescript.xml");
            XmlNodeList elemlist = doc.GetElementsByTagName("Res");
            //Console.WriteLine("ResourcesAdd");
            try
            {
                
                try
                {
                    dbf.NewResourceMedia(elemlist[1].Attributes["Name"].Value, Convert.ToInt16(elemlist[1].Attributes["Price"].Value), elemlist[1].Attributes["Descrp"].Value, 0, 0, elemlist[1].Attributes["loc"].Value, 0);
                    Resources = true;
                }catch (Exception ex) { Console.WriteLine(ex); Resources = false; }

                for (int i = 0; i < elemlist.Count; i++)
                {
                    
                    dbs.NewResourceMedia(elemlist[i].Attributes["Name"].Value, Convert.ToInt16(elemlist[i].Attributes["Price"].Value), elemlist[i].Attributes["Descrp"].Value, 0, 0, elemlist[i].Attributes["loc"].Value, 0);
                    //Console.WriteLine(elemlist[i].Attributes["Name"].Value + "," + elemlist[i].Attributes["Price"].Value + "," + elemlist[i].Attributes["Descrp"].Value + "," + 0 + "," + 0 + "," + elemlist[i].Attributes["loc"].Value + "," + 0);

                    resourceCount++;
                }
                //Console.WriteLine("Resources added to the database at start of the game");
                
            }
            catch (Exception ex) { Console.WriteLine(ex); Resources = false; }
             
           return true;
            
            
        }

        public int startPlanetAdd()
        {
            Random randNum = new Random();
            // creates 40 new planets with name and images 
            string[] planetimages = new string[] { "earth", "jupiter", "mars", "mercury", "neptune", "saturn", "uranus", "venus" };
            
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Content\planetName.xml");
            XmlNodeList planetName = doc.GetElementsByTagName("Planet");
            List<int> usednum = new List<int>();
           
            // This goes through the list of planets and adds them to the loading place but if that name already exsits then it picks another name.
            // so each time there is a new game the planets names are differently.

            while (usednum.Count <= 39)
            {
                int num = 0;

                num = randNum.Next(1, planetName.Count);

                if (usednum.Contains(num))
                {
                    num = randNum.Next(1, planetName.Count);
                }
                else
                {
                    usednum.Add(num);
                }
            }
            for (int i = 0; i < usednum.Count; i++)
            {
                
                // going to have the planet name, then the number it has for the drawing of the board, then 0 for y_loc and Diameter, (80, 60) same as the title size, then one of the planetiamges by random and then 0 for type
                dbs.NewPlanetMedia(planetName[usednum[i]].Attributes["Name"].Value, i, 0, 0, 80, 60, planetimages[randNum.Next(1, planetimages.Length)], 0);
                //Console.WriteLine(planetName[usednum[i]].Attributes["Name"].Value);
                planetscreated++;
            }

                //NewPlanetMedia(string title, int x_loc, int y_loc, int diameter, int m_x_size, int m_y_size, string file_loc, int type)

                //Console.WriteLine(planetscreated);
                return planetscreated;

        }

        public void AddtoSession()
        {
            /// need to add session id to database
            /// 
            // this makes sure that all the resource and planets are on this session, so can load the data.
            try
            {
                dbf.AddPlanettoSession(sessionNumber);
                addedPlanettoSession = true;
                dbs.AddPlanettoSession(sessionNumber);
            }
            catch (Exception ex) { Console.WriteLine("Error " + ex); addedPlanettoSession = false; }
            
            try
            {
                dbf.AddResourcetoSession(sessionNumber);
                addedRestoSession = true;
                dbs.AddResourcetoSession(sessionNumber);
            }
            catch (Exception ex) { Console.WriteLine("Error " + ex); addedRestoSession = false; }
            


        }


        public int getSession()
        {
            int ses = dbs.getLastSession();
            sessionNumber = ses;
            return ses ;
            
        }

        public Boolean AddResourcetoPlanet()
        {
            
            // this will randomly add resources to the different planets
            if (planetscreated > 0 && resourceCount > 0)
            {
                // query that updates the planetresource which will 
                dbs.AddResourcetoPlanet(sessionNumber);
                return true;
            }
            return false;
        }

        public int AddUserandSession(string name, int money, int turns)
        {
            // this will add the user to the database and the session ID
            int userID = dbs.SetUser(name, money, turns);
            return userID;
        }
            
        /// <summary>
        /// this will go through the different tiles and then sellect the different planets then each planets resources and update their entry on the database
        /// </summary>
        public void saveSession(List<Tile> board, SpaceShip ship)
        {
            Console.WriteLine("Save Session");
            List<Tile> gameBoard = board;
            // Saveing planet resource information
            for (int i = 0; i < gameBoard.Count; i++)
            {
                Planet p = gameBoard[i].getPlanet();
                List<Resource> resource = new List<Resource>();
                resource = p.getResourceList();
                for (int r = 0; r < resource.Count; r++)
                {

                    dbs.PlanetResourceUpdate(p.planetid, resource[r].resourceid, resource[r].amount, resource[r].price);
                }

            }
            //Adds all the resources to the ship
            List<Resource> shipres = ship.getResource();
            for (int i = 0; i < shipres.Count; i++)
            {
                dbs.AddResourceToShip(ship.getShipId(), shipres[i].resourceid, shipres[i].amount, shipres[i].InitialPrice);

            }

            // updates all the ship spec
            
            dbs.ShipAdd(ship.getShipId(), 0, 100, ship.getCargoCapacity(), ship.getNumberOfTurn());

            // adds the user's money to the high score
            dbs.SetHightscore(ship.getOwner(), ship.getMoney());

            // saveing the spaceship and user information


            //
            //
            /// Need to add the space ship section here, but need to change the spaceship class to do this.
            //
            //


        }
     
    }
}
