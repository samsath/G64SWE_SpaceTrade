using System;
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
        DatabasePopulate dbp;
        
        Planet pl;
        Board br;
        GameTime time;
        SpaceShip myShip;

        [TestInitialize]
        public void init()
        {
            dbp = new DatabasePopulate();
            time = new GameTime();
            myShip = new SpaceShip();
            br = new Board();
            
        }
        /*
        [TestMethod]
        public void IfSpaceKeyPressedDiceWillRoll()
        {
            myShip.setState("waiting");
            myShip.pressKeyboard("space");
            myShip.Update(time);
            Debug.WriteLine("asdfasdfasdf "+myShip.getDiceRolled());
            Assert.IsTrue(myShip.getDiceRolled()>=1&&myShip.getDiceRolled()<=6);
        }
        */
        [TestMethod]
        public void ShipCurrentMoneyCannotBeLowerThanZero()
        {
            Assert.IsTrue(myShip.getMoney()>=0);
        }
        /*
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
            expectedResource.Add(new Resource(0, "gold", 200, "gold gold gold", 5), 5);
            expectedResource.Add(new Resource(1, "silver", 100, "silver silver", 2), 4);
            expectedResource.Add(new Resource(2, "copper", 100, "Copper copper copper", 3), 4);
            myShip.setResource();
            
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
            //public Resource(int resid, string name, int price, string descript,int amounts)
            Dictionary<Resource, int> expectedResource = new Dictionary<Resource, int>();
            expectedResource.Add(new Resource(0,"gold", 200,"gold gold gold", 5), 5);
            expectedResource.Add(new Resource(1,"silver", 100,"silver silver",2), 4);
            expectedResource.Add(new Resource(2,"copper", 100,"Copper copper copper",3), 4);
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
        */
        
        /*
        [TestMethod]
        public void LastSessionis()
        {
            Assert.IsInstanceOfType(dbp.newSession(), typeof(int));

        }
        */
        [TestMethod]
        public void ResorceAdded()
        {
            //Assert.IsTrue(dbp.addResource);
            Assert.IsTrue(dbp.Startresourceadd());
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
    
        [TestMethod]
        public void CheckSessionIdatstartis0()
        {
            Assert.AreEqual(0, dbp.sessionNumber);
        }
        /*
        [TestMethod]
        public void CheckLastSession()
        {
            Assert.AreNotEqual(0, dbp.getSession());
        }
        */
        [TestMethod]
        public void AddResourcetoSessionisTrue()
        {
            Assert.IsTrue(dbp.addedRestoSession);
        }

        [TestMethod]
        public void AddPlanettoSessionisTrue()
        {
            Assert.IsTrue(dbp.addedPlanettoSession);
        }

        [TestMethod]
        public void AddingResourcetoPlanets()
        {
            Assert.IsTrue(dbp.AddResourcetoPlanet());
        }

        [TestMethod]
        public void CheckifPlanetsLoadResourcs()
        {
            
            Assert.AreNotEqual(0, pl.resourcedbCount);
        }
  
        [TestMethod]
        public void CheckifSwitchofResourceReturnssomething() 
        {
            Assert.IsNotNull(br.textureReturn("space"));
        }

        [TestMethod]
        public void CheckifPlantgetsResoruceData(){
            Assert.IsTrue(br.PlanetData());
        }

        [TestMethod]
        public void PlanethasName()
        {
            Assert.IsNotNull(pl.getName());
        }
        
        [TestMethod]
        public void PlanethasResources()
        {
            Assert.IsNotNull(pl.getResourceList());
        }
        [TestMethod]
        public void SessionAndUserAddedtoDB()
        {
            Assert.IsInstanceOfType(dbp.AddUserandSession("paul", 1, 0),typeof(int));
                
        }

    }
}
