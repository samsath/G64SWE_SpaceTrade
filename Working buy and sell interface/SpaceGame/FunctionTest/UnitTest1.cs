using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using testLibrary;

namespace FunctionTest
{
    [TestClass]
    public class FUNCTION
    {
       
        [TestMethod]
        public void chancecheck_Constructs_OK()
        {
            chancecheck sys = new chancecheck();
            Assert.IsNotNull(sys);
        }

        [TestMethod]
        //call the random chance-picking function
        public void getchance()
        {
            chancecheck sys = new chancecheck();
            sys.getchance(2);
            sys.checkvalue(4);
            Assert.AreEqual(4, sys.checkvalue(4));
        }

        [TestMethod]
        public void checkvalue()
        {

            chancecheck sys = new chancecheck();
            sys.checkvalue(4);
            Assert.AreEqual(4, sys.checkvalue(4));
            Assert.IsNotNull(sys);
        }

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
            finance sys = new finance(); double balance = 100;
            double oldprice = 10;
            double newprice = 20;
            int amount = 15;
            sys.newblance(balance, oldprice, newprice, amount);
            Assert.AreNotEqual(sys.newblance(balance, oldprice, newprice, amount), balance);
        }
        [TestMethod]
        public void turnConstruct()
        {
            turn sys = new turn();
            Assert.IsNotNull(sys);
        }
        [TestMethod]
        public void turn_purchase()
        {
            //defualt price of one turn is 1000
            int money = 3000;
            int turn = 0;
            int num = 4;

            turn sys = new turn();
            Assert.IsNotNull(sys);
            sys.buyturn(num, turn, money);
            //Assert.AreNotEqual(0, turn);
            Assert.AreEqual(-1, sys.buyturn(num, turn, money));
        }
        [TestMethod]//build the function
        public void upgradeConstruct()
        {
            upgrade sys = new upgrade();
            Assert.IsNotNull(sys);
        }


        [TestMethod]
        public void upgradeShip()
        {
            //assume each upgrade costs 2500 and will add 5 more capacity unit
            upgrade sys = new upgrade();//defult top level is 4
            int money = 3000;
            int level = 1;
            int compacity = 5;
            sys.upgradeship(money, level, compacity);
            Assert.AreNotEqual(3000, sys.upgradeship(money, level, compacity));//test if the balances are the same after the test
            Assert.AreEqual(3000, money);


        }
    }
}
