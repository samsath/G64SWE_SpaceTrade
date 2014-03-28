using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using turnlibra;

namespace turntest
{
    [TestClass]
    public class UnitTest1
    {
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
            sys.buyturn(num,turn,money);
            //Assert.AreNotEqual(0, turn);
            Assert.AreEqual(-1, sys.buyturn(num, turn, money));
        }
    
    
    
    }
}

