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
        public string name;
        public List<Resource> resource;
        public List<Resourcedata> resourcedb;
        Random rand;
        public string p;
        public IDatabase dbs = new Database();

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

        public string getName()
        {
            return name;
        }

        public void generateResource()
        {
            // changed this so it gets the relavent information from the database

            List<Resourcedata> resourcedb = dbs.PlanetResources(planetid);
            

            for (int i = 0; i < resourcedb.Count; i++)
            {
                // this goes through the database info and then adds the relavent information to the planet generation.
                resource.Add(new Resource(resourcedb[i].Resource_id, resourcedb[i].Name, resourcedb[i].Initial_Price,resourcedb[i].Description,rand.Next(100)));
            }
            
        }

        public List<Resource> getResourceList()
        {
            return resource;
        }
    }
}
