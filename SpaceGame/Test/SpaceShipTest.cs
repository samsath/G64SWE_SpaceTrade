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
        Economy eco;
        Trading trad;

           

        [TestInitialize]
        public void init()
        {
            dbp = new DatabasePopulate();
            time = new GameTime();
            myShip = new SpaceShip();
//<<<<<<< HEAD
            eco = new Economy();
            trad = new Trading();
          
//=======
            br = new Board();
            
//>>>>>>> c8a838d145baad2c15fd9d3c6ac643baa9b44c9f
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
        /*
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
        

        [TestMethod]
        public void LastSessionis()
        {
            Assert.IsInstanceOfType(dbp.newSession(), typeof(int));

        }

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
<<<<<<< HEAD
        // economy test adding
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
        public void Decrement_MoneyAtZero_ReturnFalse()
        {
            //stock is zero to start with

            bool result = eco.DecrementMoney(5);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Decrement_GreaterThanZero_ReturnTrue()
        {
            
            eco.AddToMoney(10);
            bool result = eco.DecrementMoney(5);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DecrementMoney_MoneyAtZero_CheckMoneyEqualsZero()
        {

            eco.DecrementMoney(1);

            Assert.AreEqual(0, eco.Money);
        }
        [TestMethod]
        public void BuyResourceAndDecrementMoney()
        {
            int ResorceIDPrice = 1;
          //  eco.AddResorce(ResorceIDPrice);


            ///Assert.Inconclusive();/////
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
         [TestMethod]
         public void PriceChangeDependOnThePlanentQuentity()
         {
             //each planet has 20 avilable resorses
             //the price for sell or buy vary depend on the quantity of the planet has
             //simple calculation is  price of the resorce + 1 - price of the resorce*(planent quantity /20)
             int GetPriceResorceID = 2;
             eco.SellingPrice(GetPriceResorceID) ;

         }
         public void PriceChangeDependOnThePlanentQuentity()
         {
             //each planet has 20 avilable resorses
             //the price for sell or buy vary depend on the quantity of the planet has
             //simple calculation is  price of the resorce + 1 - price of the resorce*(planent quantity /20)
             int GetPriceResorceID = 2;
             eco.BuyingingPrice(GetPriceResorceID);

         }
        


        #endregion
        #region new test to class economy to monuplite SpaceShip
         [TestMethod]
         public void decresetheMoneyFromTheSpaceShipClass()
         {
             //myShip.
            // int result = myShip.decraseMoney(int PriceOfAnyThing);

         }

        #endregion
=======
    
        [TestMethod]
        public void CheckSessionIdatstartis0()
        {
            Assert.AreEqual(0, dbp.sessionNumber);
        }

        [TestMethod]
        public void CheckLastSession()
        {
            Assert.AreNotEqual(0, dbp.getSession());
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

>>>>>>> c8a838d145baad2c15fd9d3c6ac643baa9b44c9f
    }
}

