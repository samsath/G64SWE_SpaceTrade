using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;

namespace Upgradetest
{
    [TestClass]
    public class UnitTest1
    {
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
            sys.upgradeship(money,level,compacity);
            Assert.AreNotEqual(3000, sys.upgradeship(money, level, compacity));//test if the balances are the same after the test
            Assert.AreEqual(3000, money );

            
        }
    }
}
