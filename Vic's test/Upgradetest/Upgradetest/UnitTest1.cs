using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;

namespace Upgradetest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void upgrade_construct()
        {
            upgrade sys = new upgrade();
            Assert.IsNotNull(sys);
        }


        [TestMethod]
        public void upgradeship()
        {
            //assume each upgrade costs 2500
            upgrade sys = new upgrade();
            int money = 3000;
            int level = 1;
            int compacity = 5;
            sys.upgradeship(money,level,compacity);
            Assert.AreNotEqual(3000, sys.upgradeship(money, level, compacity));


            
        }
    }
}
