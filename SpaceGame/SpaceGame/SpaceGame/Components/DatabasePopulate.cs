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
        public int sessionNumber = 0;
        public Boolean Resources;
        public int resourceCount = 0;
        public int planetscreated = 0;
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

            return Resources;
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
                //Console.WriteLine(planetName[usednum[i]].Attributes["Name"].Value);
                planetscreated++;
            }

                //NewPlanetMedia(string title, int x_loc, int y_loc, int diameter, int m_x_size, int m_y_size, string file_loc, int type)

                //Console.WriteLine(planetscreated);
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

        public int getSession()
        {
            if (sessionNumber == 0)
            {
                int ses = dbs.getLastSession();
                sessionNumber = ses;
            }
            return sessionNumber;
        }

        public void AddResourcetoPlanet()
        {
            // this will randomly add resources to the different planets
            if (planetscreated > 0 && resourceCount > 0)
            {
                // query that updates the planetresource which will 
                dbs.AddResourcetoPlanet(sessionNumber);
            }
        }
    }
}
