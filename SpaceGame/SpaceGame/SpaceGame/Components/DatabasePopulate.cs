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
        Database dbs = new Database();
        public Boolean Startresourceadd()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Content\resourceListCostDescript.xml");
            XmlNodeList elemlist = doc.GetElementsByTagName("Res");
            try
            {
                for (int i = 0; i < elemlist.Count; i++)
                {

                    dbs.NewResourceMedia(elemlist[i].Attributes["Name"].Value, Convert.ToInt16(elemlist[i].Attributes["Price"].Value), elemlist[i].Attributes["Descrp"].Value, 0, 0, elemlist[i].Attributes["loc"].Value, 0);
                }
                Console.WriteLine("Resources added to the database at start of the game");
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex); return false; }
        }
    }
}
