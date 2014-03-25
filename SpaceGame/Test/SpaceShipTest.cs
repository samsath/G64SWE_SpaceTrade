﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceGame;
using SpaceGame.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using System.Threading;

namespace Test
{
    [TestClass]
    public class SpaceShipTest
    {
        //DatabasePopulate dbp;
        GameTime time;
        SpaceShip myShip;
        Dictionary<Resource, int> resource;
        Dictionary<Resource, int> initialResource;
        Dictionary<Resource, int> laterResource;

        [TestInitialize]
        public void init()
        {
            //dbp = new DatabasePopulate();
            time = new GameTime();
            myShip = new SpaceShip();
            resource = new Dictionary<Resource, int>();
            initialResource = new Dictionary<Resource, int>();
            laterResource = new Dictionary<Resource, int>();
            
            initialResource.Add(new Resource("gold", 100), 6);
            initialResource.Add(new Resource("silver", 100), 7);
            initialResource.Add(new Resource("copper", 100), 8);
            resource.Add(new Resource("gold", 100), 1);
            resource.Add(new Resource("silver", 100), 3);
            resource.Add(new Resource("copper", 100), 4);

        }

        [TestMethod]
        public void IfSpaceKeyPressedDiceWillRoll()
        {
            myShip.setState("waiting");
            myShip.pressKeyboard("space");
            myShip.Update(time);
            Debug.WriteLine("asdfasdfasdf "+myShip.getDiceRolled());
            Assert.IsTrue(myShip.getDiceRolled()>=1&&myShip.getDiceRolled()<=6);
        }

        [TestMethod]
        public void ShipCurrentMoneyCannotBeLowerThanZero()
        {
            Assert.IsTrue(myShip.getCurrentMoney()>=0);
        }

        [TestMethod]
        public void AmountOfResourcesCannotExceedShipCargoCapacity()
        {
            Assert.IsTrue(myShip.getShipNumberOfResource()<=myShip.getCargoCapacity());
        }

        [TestMethod]
        public void BuyingIsOK()
        {
            Boolean testResult = true;
            Dictionary<Resource, int> expectedResource = new Dictionary<Resource, int>();
            expectedResource.Add(new Resource("gold", 200), 7);
            expectedResource.Add(new Resource("silver", 100), 10);
            expectedResource.Add(new Resource("copper", 100), 12);
            myShip.setResource(initialResource);
            
            myShip.buy(resource);
            laterResource = myShip.getResultList();
            
            foreach (KeyValuePair<Resource, int> expected in expectedResource)
            {
                foreach (KeyValuePair<Resource, int> later in laterResource)
                {
                    if (expected.Key.getName().Equals(later.Key.getName()))
                    {
                        if (expected.Value != later.Value)
                        {
                            testResult = false;
                            break;
                        }
                    }

                }
            }
            Assert.IsTrue(testResult);
        }

        [TestMethod]
        public void SellingIsOK()
        {
            Boolean testResult = true;
            Dictionary<Resource, int> expectedResource = new Dictionary<Resource, int>();
            expectedResource.Add(new Resource("gold", 200), 5);
            expectedResource.Add(new Resource("silver", 100), 4);
            expectedResource.Add(new Resource("copper", 100), 4);
            myShip.setResource(initialResource);
            myShip.sell(resource);
            laterResource = myShip.getResultList();

            foreach (KeyValuePair<Resource, int> expected in expectedResource)
            {
                foreach (KeyValuePair<Resource, int> later in laterResource)
                {
                    if (expected.Key.getName().Equals(later.Key.getName()))
                    {
                        if (expected.Value != later.Value)
                        {
                            testResult = false;
                            break;
                        }
                    }

                }
            }
            Assert.IsTrue(testResult);
        }
        /*
        [TestMethod]
        public void ResourcesAddedatStartofGame()
        {
            Assert.IsTrue(dbp.Startresourceadd());
        }

        [TestMethod]
        public void LastSessionis()
        {
            Assert.IsInstanceOfType(dbp.newSession(), typeof(int));

        }

        [TestMethod]
        public void ResorceAdded()
        {
            Assert.IsTrue(dbp.addResource);
        }

        [TestMethod]
        public void PlanetAdded()
        {
            Assert.IsTrue(dbp.addplanet);
        }

        [TestMethod]
        public void checkif40PlanetsareCreated()
        {
            
            
            Assert.IsInstanceOfType(dbp.startPlanetAdd(), typeof(int));
        }
         */
    }
}
