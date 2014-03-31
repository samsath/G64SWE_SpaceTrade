using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop;

namespace Money
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void finance_construct()

        {
            finance sys = new finance();
            Assert.IsNotNull(sys);

        }

        [TestMethod]
        public void moneyMultiplier()
        {
            finance sys = new finance();
            //sys.newprofit(profit);
            double profit = 2.0;
            Assert.AreNotEqual(sys.newprofit(profit), profit);

        }

        [TestMethod]
        public void makingMoney()
        {
            finance sys = new finance();
            double profit = 140;
            sys.newprofit(profit);
            Assert.AreNotEqual(sys.newprofit(profit), profit);
           // string cargoname = "coal";
            double balance=100;
            double oldprice=10;
            double newprice = 20; 
            int amount = 15;
            sys.newblance(balance, oldprice, newprice, amount);
            Assert.AreNotEqual(sys.newblance(balance, oldprice, newprice, amount), balance);
        }

        

        
      
       

        
    }
}
