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
        public void moneyMultiplier()//level 1 multiplier is 1.1.
        {
            finance sys = new finance();
            //sys.newprofit(profit);
            double profit = 2.0;
            Assert.AreEqual(sys.multiply1(profit), 2.2);

        }

        [TestMethod]
        public void multiplierTest()//There are three different money_multipliers from 1.1 to 1.3.
        {
            finance sys = new finance();
            double profit = 100;
            sys.multiply1(profit);
            Assert.AreNotEqual(sys.multiply1(profit), sys.multiply2(profit));
            Assert.AreEqual(120, sys.multiply2(profit));
            Assert.AreEqual(profit, 100);
            Assert.AreEqual(130, sys.multiply3(profit));
        }
          
        [TestMethod]
        public void makingMoney()//making money via buy and sell
        {   
             finance sys = new finance();double balance=100;
            double oldprice=10;
            double newprice = 20; 
            int amount = 15;
            sys.newblance(balance, oldprice, newprice, amount);
            Assert.AreNotEqual(sys.newblance(balance, oldprice, newprice, amount), balance);
        }

        

        
      
       

        
    }
}
