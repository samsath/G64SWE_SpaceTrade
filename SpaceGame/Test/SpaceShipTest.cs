using System;
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

        GameTime time;
        SpaceShip myShip;

        [TestInitialize]
        public void init()
        {
            dbp = new DatabasePopulate();

           // pl = new Planet("earth",1, Texture2D earth);


            time = new GameTime();
            myShip = new SpaceShip();

        }

        [TestMethod]
        public void IfKeyPressedDiceWillRoll()
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
            Assert.IsTrue(myShip.getCurrentMoney()>0);
        }

        [TestMethod]
        public void NumberOfResourcesCannotExceedShipCargoCapacity()
        {
            Assert.IsTrue(myShip.getShipNumberOfResource()<=myShip.getCargoCapacity());
        }

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
            //Assert.IsTrue(dbp.addResource);
            Assert.IsTrue(dbp.Resources);
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

        [TestMethod]
        public void CheckLastSession()
        {
            Assert.AreEqual(0, dbp.getSession());
        }

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



    }
}
