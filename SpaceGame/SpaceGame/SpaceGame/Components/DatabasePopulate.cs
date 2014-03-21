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

        public void startPlanetAdd()
        {
            // need 40 spaces filled by planets and blank space

        }
    }
}
