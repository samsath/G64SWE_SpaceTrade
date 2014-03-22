using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STDatabase;
using System.IO;
using System.Xml;



namespace SpaceGame.Components
{
    public class DatabasePopulate
    {
        public Boolean addplanet = true;
        public Boolean addResource = true;
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
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Content\resourceListCostDescript.xml");
            XmlNodeList elemlist = doc.GetElementsByTagName("Res");
            try
            {
                
                try
                {
                    dbf.NewResourceMedia(elemlist[1].Attributes["Name"].Value, Convert.ToInt16(elemlist[1].Attributes["Price"].Value), elemlist[1].Attributes["Descrp"].Value, 0, 0, elemlist[1].Attributes["loc"].Value, 0);
                    return true;
                }catch (Exception ex) { Console.WriteLine(ex); return false; }

                for (int i = 0; i < elemlist.Count; i++)
                {
                    
                    dbs.NewResourceMedia(elemlist[i].Attributes["Name"].Value, Convert.ToInt16(elemlist[i].Attributes["Price"].Value), elemlist[i].Attributes["Descrp"].Value, 0, 0, elemlist[i].Attributes["loc"].Value, 0);
                    
                }
                Console.WriteLine("Resources added to the database at start of the game");
                
            }
            catch (Exception ex) { Console.WriteLine(ex); return false; }
        }

        public int startPlanetAdd()
        {
            Random randNum = new Random();
            // creates 40 new planets with name and images 
            string[] planetimages = new string[] { "earth", "jupiter", "mars", "mercury", "neptune", "saturn", "uranus", "venus" };
            int planetscreated = 0;
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Content\planetName.xml");
            XmlNodeList planetName = doc.GetElementsByTagName("Planet");
            List<int> usednum = new List<int>();
           
            // This goes through the list of planets and adds them to the loading place but if that name already exsits then it picks another name.
            // so each time there is a new game the planets names are differently.

            while (usednum.Count <= 40)
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
                Console.WriteLine(planetName[usednum[i]].Attributes["Name"].Value);
                planetscreated++;
            }

                //NewPlanetMedia(string title, int x_loc, int y_loc, int diameter, int m_x_size, int m_y_size, string file_loc, int type)

                Console.WriteLine(planetscreated);
                return planetscreated;

        }

        public void AddtoSession(int sessionId)
        {
            // this makes sure that all the resource and planets are on this session, so can load the data.
            try
            {
                dbs.AddPlanettoSession(sessionId);
            }
            catch (Exception ex) { Console.WriteLine("Error " + ex); addplanet = false; }
            
            try
            {
                dbs.AddResourcetoSession(sessionId);
            }
            catch (Exception ex) { Console.WriteLine("Error " + ex); addResource = false; }
            


        }

        public int newSession()
        {
            // this gets the current session number and then adds one to is so that we can have a new session.
            int ses = dbs.getLastSession();
            int fs = dbf.getLastSession();

            return ses + 1;
        }
    }
}
