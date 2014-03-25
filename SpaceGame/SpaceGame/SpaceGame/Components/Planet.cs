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

namespace SpaceGame.Components
{
    class Planet : Object
    {
        string name;
        Dictionary<Resource, int> resource;
        Random rand;

        public Planet(string name)
        {
            this.name = name;
            resource = new Dictionary<Resource, int>();
            generateResource();
        }

        public string getName()
        {
            return name;
        }

        public void generateResource()
        {
            if (name.Equals("mercury"))
            {
                rand = new Random(1);
                resource.Add(new Resource("helium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("iron", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("magnesium", rand.Next(1, 9) * 100),4);
            }
            if (name.Equals("venus"))
            {
                rand = new Random(2);
                resource.Add(new Resource("hydrogen", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("lithium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("sodium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("potassium", rand.Next(1, 9) * 100),4);
            }
            if (name.Equals("earth"))
            {
                rand = new Random(3);
                resource.Add(new Resource("rubidium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("cesium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("francium", rand.Next(1, 9) * 100),4);
            }
            if (name.Equals("mars"))
            {
                rand = new Random(4);
                resource.Add(new Resource("beryllium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("calcium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("strontium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("barium", rand.Next(1, 9) * 100),4);
            }
            if (name.Equals("jupiter"))
            {
                rand = new Random(5);
                resource.Add(new Resource("radium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("scandium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("yttrium", rand.Next(1, 9) * 100),4);
            }
            if (name.Equals("saturn"))
            {
                rand = new Random(6);
                resource.Add(new Resource("titanium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("zirconium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("hafnium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("vanadium", rand.Next(1, 9) * 100),4);
            }
            if (name.Equals("uranus"))
            {
                rand = new Random(7);
                resource.Add(new Resource("niobium", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("tantalum", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("chromium", rand.Next(1, 9) * 100),4);
            }
            if (name.Equals("neptune"))
            {
                rand = new Random(8);
                resource.Add(new Resource("molybdenum", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("tungsten", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("manganese", rand.Next(1, 9) * 100),4);
                resource.Add(new Resource("technetium", rand.Next(1, 9) * 100),4);
            }
        }

        public Dictionary<Resource,int> getResourceList()
        {
            return resource;
        }
    }
}
