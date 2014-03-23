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
    class Planet : Object
    {
        string name;
        List<Resource> resource;
        Random rand;
        private string p;
        Database dbs;

        // int of the planet number in the database
        public int planetid { get; set; }
        public string texture { get; set; }

        public Planet(string name, int id, string texture)
        {
            this.name = name;
            this.planetid = id;
            this.texture = texture;
            resource = new List<Resource>();
            generateResource();
        }

        public string getName()
        {
            return name;
        }

        public void generateResource()
        {
            // changed this so it gets the relavent information from the database

            List<Resourcedata> resourcedb = new List<Resourcedata>();

            resourcedb = dbs.PlanetResources(planetid);

            for (int i = 0; i < resourcedb.Count; i++)
            {
                // this goes through the database info and then adds the relavent information to the planet generation.
                resource.Add(new Resource(resourcedb[i].Resource_id, resourcedb[i].Name, resourcedb[i].Initial_Price,resourcedb[i].Description,rand.Next(100)));
            }
            /* Replaces to work with the database.
            if (name.Equals("mercury"))
            {
                rand = new Random(1);
                resource.Add(new Resource("helium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("iron", rand.Next(1, 9) * 100));
                resource.Add(new Resource("magnesium", rand.Next(1, 9) * 100));
            }
            if (name.Equals("venus"))
            {
                rand = new Random(2);
                resource.Add(new Resource("hydrogen", rand.Next(1, 9) * 100));
                resource.Add(new Resource("lithium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("sodium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("potassium", rand.Next(1, 9) * 100));
            }
            if (name.Equals("earth"))
            {
                rand = new Random(3);
                resource.Add(new Resource("rubidium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("cesium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("francium", rand.Next(1, 9) * 100));
            }
            if (name.Equals("mars"))
            {
                rand = new Random(4);
                resource.Add(new Resource("beryllium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("calcium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("strontium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("barium", rand.Next(1, 9) * 100));
            }
            if (name.Equals("jupiter"))
            {
                rand = new Random(5);
                resource.Add(new Resource("radium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("scandium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("yttrium", rand.Next(1, 9) * 100));
            }
            if (name.Equals("saturn"))
            {
                rand = new Random(6);
                resource.Add(new Resource("titanium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("zirconium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("hafnium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("vanadium", rand.Next(1, 9) * 100));
            }
            if (name.Equals("uranus"))
            {
                rand = new Random(7);
                resource.Add(new Resource("niobium", rand.Next(1, 9) * 100));
                resource.Add(new Resource("tantalum", rand.Next(1, 9) * 100));
                resource.Add(new Resource("chromium", rand.Next(1, 9) * 100));
            }
            if (name.Equals("neptune"))
            {
                rand = new Random(8);
                resource.Add(new Resource("molybdenum", rand.Next(1, 9) * 100));
                resource.Add(new Resource("tungsten", rand.Next(1, 9) * 100));
                resource.Add(new Resource("manganese", rand.Next(1, 9) * 100));
                resource.Add(new Resource("technetium", rand.Next(1, 9) * 100));
            }
             */
        }

        public List<Resource> getResourceList()
        {
            return resource;
        }
    }
}
