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
using System.Collections.Generic;  //need to be added 

namespace Test
{
    [TestClass]
    public class SpaceShipTest
    {
        DatabasePopulate dbp;
        Economy eco;
        [TestInitialize]
        public void init()
        {
            dbp = new DatabasePopulate();
            eco = new Economy
        }

        [TestMethod]
        public void IfKeyPressedDiceWillRoll()
        {
            GameTime time = new GameTime();
            SpaceShip myShip = new SpaceShip();
            myShip.setState("waiting");
            myShip.pressKeyboard("space");
            myShip.Update(time);
            Debug.WriteLine("asdfasdfasdf " + myShip.getDiceRolled());
            Assert.IsTrue(myShip.getDiceRolled() >= 1 && myShip.getDiceRolled() <= 6);
        }
        [TestMethod]
        public void ResourcesAddedatStartofGame()
        {
            Assert.IsTrue(dbp.Startresourceadd());
        }

        [TestMethod]
        public void PlanetDatabaseCreate()
        {
            // see if planet is added to the database.
        }
       
        
        
        
        #region // test economy class 
        // check the constrcture 
        [TestMethod]
        public void Economy_Constructs_OK()
        {
            Assert.IsNotNull(eco);
        }
        [TestMethod]
        public void Economy_AmountOFMoney_OK()
        {
            int a = eco.Money;
            Assert.AreEqual(0, a);
        }
         [TestMethod]
        public void Economy_NumberOfResources_OK()
        {
         Dictionary<string, int> eco.ResorceList = new Dictionary <string, int>();  // or dictionary with the name and the quantitiy ResorceList  
        }


         [TestMethod]
        public void Economy_BuyingSystem_OK()
        {
         eco.Add(Resource x);     // sell resources + we can increace the  quantity (quantitiy parameter)
         
        }
         [TestMethod]
        public void Economy_SellingSystem_OK()
        {
         eco.Remove(Resource x);   // sell resources + we can decrease  the quantity (quantitiy parameter)
        }
         [TestMethod]
        public void Economy_CalculatingSystem_OK()// calculate the start anount of money divided by the amont now
        {
            eco.Profit();
        }
         
         [TestMethod]
        public void Economy_UpGradeTheShip_OK()
        {
            eco.ShipUpGrede(SpaceShip s ,int Level);// the ship class must have level attribute
        }
         [TestMethod]
        public void Economy_GetNewShip_OK()
        {
            eco.ShipChange(SpaceShip s, int Level);// the ship class must have other type
        }
        #endregion
    }
}