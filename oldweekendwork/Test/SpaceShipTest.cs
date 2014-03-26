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
using STDatabase;

#region Sam
namespace Test
{
    [TestClass]
    public class SpaceShipTest
    {
        DatabasePopulate dbp;
        Economy eco ;
     
        [TestInitialize]
        public void init()
        {
            dbp = new DatabasePopulate();
            eco = new Economy();
        }

        [TestMethod]
        public void IfKeyPressedDiceWillRoll()
        {
            GameTime time = new GameTime();
            SpaceShip myShip = new SpaceShip();
            myShip.setState("waiting");
            myShip.pressKeyboard("space");
            myShip.Update(time);
            Debug.WriteLine("asdfasdfasdf "+myShip.getDiceRolled());
            Assert.IsTrue(myShip.getDiceRolled()>=1&&myShip.getDiceRolled()<=6);
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
       
        #endregion
        #region // test economy class 
        // check the constrcture 
        [TestMethod]
        public void Economy_Constructs_OK()
        {
            Assert.IsNotNull(eco);
        }
        [TestMethod]
        public void CheckMoney_IsGreaterThanZero()
        {
            int a = eco.Money;
            Assert.IsTrue(a >= 0);
        }
        
        
        [TestMethod]
        public void AddMoney_Add5Gold_CheckMoneyIncreasesBy5()
        {
           
            int new_amount = 5;
            int money_start = eco.Money;
            eco.AddToMoney(new_amount);
            int Money_end = eco.Money;

            Assert.AreEqual(money_start + new_amount, Money_end );
        }

        
        [TestMethod]
        public void DecrementMoney_CallOnce_ReducesCheckMoneyByAmount()
        {

            int new_amount = 5;
           
            int money_start = eco.Money;
            eco.DecrementMoney(new_amount);
            int Money_end = eco.Money;
            Assert.AreEqual(money_start - new_amount, Money_end);
        }
        
        [TestMethod]
        public void DecrementStock_MoneyAtZero_ReturnFalse()
        {
            //stock is zero to start with

            bool result = eco.DecrementMoney(5);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DecrementStock_StockGreaterThanZero_ReturnTrue()
        {
            
            eco.AddToMoney(10);
            bool result = eco.DecrementMoney(5);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DecrementMoney_MoneyAtZero_CheckStockEqualsZero()
        {

            eco.DecrementMoney(1);

            Assert.AreEqual(0, eco.Money);
        }
        [TestMethod]
        public void BuyResourceAndDecrementMoney()
        {
            int ResorceIDPrice = 1;
            eco.AddResorce(ResorceIDPrice);


            Assert.Inconclusive();/////
        }
        public void SellResourceAndAddToMoney()
        {
            int ResorceIDPrice = 1;
            eco.removeoneResorce(ResorceIDPrice);


            Assert.AreEqual(0, eco.Money);
        }
      
    
        
       
         [TestMethod]
        public void Economy_CalculatingSystem_OK()// calculate the start anount of money divided by the amont now
        {
            eco.Money = 1 ; 
            float aa = eco.Profit();
            Assert.AreEqual(1, aa);
        }
        
         [TestMethod]
        public void Economy_UpGradeThecargo_OK()
        {
             int SpaceShipIDCargo =1;
             int LevelID = 1 ;
             int ShipPricee = 1 ;
             eco.ShipUpGrede(SpaceShipIDCargo, LevelID, ShipPricee);// the ship class must have level attribute
        }
    
         [TestMethod]
        public void Economy_GetNewShip_OK()
        {
             int SpaceShipID =1;
             int LevelID = 1 ;
             int ShipPricee = 1;
             eco.ShipChange(SpaceShipID, LevelID, ShipPricee);// the ship class must have other type
        }
        #endregion
       
    }
}