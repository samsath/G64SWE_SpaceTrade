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
    class Utility
    {
        public List<Resource> AddResource(List<Resource> baseResource, List<Resource> changeResource){
            Console.WriteLine("=== Base Resource");
            for (int i = 0; i < baseResource.Count; i++)
            {
                Console.WriteLine(baseResource[i].resourceid + " " + baseResource[i].name + " " + baseResource[i].amount);
            }
            Console.WriteLine("=== Resource add");
            for (int i = 0; i < changeResource.Count; i++)
            {
                Console.WriteLine(changeResource[i].resourceid + " " + changeResource[i].name + " " + changeResource[i].amount);
            }

            for (int cr = 0; cr < changeResource.Count; cr++)
            {
                int count = 1;
                for (int sp = 0; sp < baseResource.Count; sp++)
                {
                    if (baseResource[sp].resourceid == changeResource[cr].resourceid)
                    {
                        // Adds the change to the ship resource then remove it from the chnagedRes list.
                        baseResource[sp].amount = baseResource[sp].amount + changeResource[cr].amount;
                        count = 2;

                    }
                }
                if (count == 1)
                {
                    Resource result = new Resource(changeResource[cr].resourceid, changeResource[cr].name, changeResource[cr].price, changeResource[cr].description, changeResource[cr].amount);
                    baseResource.Add(result);
                }
            }
            Console.WriteLine("=== Resource AFTER");
            for (int i = 0; i < baseResource.Count; i++)
            {
                Console.WriteLine(baseResource[i].resourceid + " " + baseResource[i].name + " " + baseResource[i].amount);
            }
            return baseResource;
        }
        public List<Resource> RemoveResource(List<Resource> baseResource, List<Resource> changeResource)
        {
            Console.WriteLine("==== Base Resource");
            for (int i = 0; i < baseResource.Count; i++)
            {
                Console.WriteLine(baseResource[i].resourceid + " " + baseResource[i].name + " " + baseResource[i].amount);
            }
            Console.WriteLine("=== Resource remove");
            for (int i = 0; i < changeResource.Count; i++)
            {
                Console.WriteLine(changeResource[i].resourceid + " " + changeResource[i].name + " " + changeResource[i].amount);
            }

            for (int cr = 0; cr < changeResource.Count; cr++)
            {
                for (int sp = 0; sp < baseResource.Count; sp++)
                {
                    if (baseResource[sp].resourceid == changeResource[cr].resourceid)
                    {
                        // Adds the change to the ship resource then remove it from the chnagedRes list.
                        baseResource[sp].amount = baseResource[sp].amount - changeResource[cr].amount;
                    }
                }
            }
            Console.WriteLine("=== Resource AFTER");
            for (int i = 0; i < baseResource.Count; i++)
            {
                Console.WriteLine(baseResource[i].resourceid + " " + baseResource[i].name + " " + baseResource[i].amount);
            }
            return baseResource;
        }
    }
}
