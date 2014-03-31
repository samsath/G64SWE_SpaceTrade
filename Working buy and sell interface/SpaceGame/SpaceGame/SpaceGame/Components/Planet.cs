using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using STDatabase;

namespace SpaceGame.Components
{
    public class Planet : Object
    {
        public string name ;
        public List<Resource> resource;
        public List<Resourcedata> resourcedb;
        Random rand;
        public string p;
        public IDatabase dbs = new Database();
        public int resourcedbCount = 2;

        // int of the planet number in the database
        public int planetid { get; set; }
        public string texture { get; set; }

        public Planet(string name, int id, string texture)
        {
            this.name = name;
            this.planetid = id;
            this.texture = texture;
            resource = new List<Resource>();
            
        }

        public Planet()
        {
            // TODO: Complete member initialization
        }

        public string getName()
        {
            name = "Mars";
            return name;
        }

        public void generateResource()
        {
            // changed this so it gets the relavent information from the database
            rand = new Random();

            //Console.WriteLine(name + " Generate");
            List<Resourcedata> resourcedb = dbs.PlanetResources(planetid);
            //Console.WriteLine(name + " resource got =" + resourcedb.Count);
                
            resourcedbCount = resourcedb.Count;
            for (int i = 0; i < resourcedb.Count; i++)
            {
                // this goes through the database info and then adds the relavent information to the planet generation.
                resource.Add(new Resource(resourcedb[i].Resource_id, resourcedb[i].Name, resourcedb[i].Initial_Price, resourcedb[i].Description, rand.Next(1, 100)));
                //Console.WriteLine(Convert.ToString(resourcedb[i].Resource_id) + " = " + resourcedb[i].Name + " = " + Convert.ToString(resourcedb[i].Initial_Price) + " = " + resourcedb[i].Description+ " = " + Convert.ToString(rand.Next(100)));
            }
            
        }

        public List<Resource> getResourceList()
        {
            return resource;
        }
        
        public void buySell(List<Resource>changedRes)
        {
            Console.WriteLine("Planet buy Sell");
            // this gets the changes from the buy or sell screen then updates the location resource list compaired to the outer one.
            int newResCount = changedRes.Count;
            for (int i = 0; i < changedRes.Count; i++)
            {
                Console.WriteLine("Planet change = " + changedRes[i].resourceid + changedRes[i].name + changedRes[i].amount + changedRes[i].price);
            }

            List<int> nonMatch = new List<int>();
            for (int pr = 0; pr > resource.Count; pr++)
            {
                // first goes through local list and the transaction list and if there are the same (by resource NUmber) the local will have the same information as the transaction
                for (int nR = 0; nR > newResCount; nR++)
                {
                    // adds the changedRes number to the nonMatch list if the resource is in the local resources
                    if (resource[pr].resourceid == changedRes[nR].resourceid)
                    {
                        Console.WriteLine("Ship Resource list = " + resource[pr].resourceid + " " + "Changes = " + changedRes[nR].resourceid);
                        nonMatch.Add(nR);
                        resource[pr].amount = changedRes[nR].amount;
                        resource[pr].price = changedRes[nR].price;
                    }
                }
            }

            // compares the nonMatch and the newResCount as if there are different there will need new items to add.
            if (newResCount != nonMatch.Count)
            {
                // then goes throught the nonMatch list and see's of the res number is in it. If it is not then the resource is added to the local list.
                for (int res = 0; res > newResCount; res++)
                {
                    if (!nonMatch.Contains(res))
                    {
                        Console.WriteLine("add new resource to shipRes = " + changedRes[res].resourceid + changedRes[res].name + changedRes[res].price + changedRes[res].description + changedRes[res].amount);
                        resource.Add(new Resource(changedRes[res].resourceid, changedRes[res].name, changedRes[res].price, changedRes[res].description, changedRes[res].amount));
                    }
                }
            }

        }
        
    }
}
