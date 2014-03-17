using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;

namespace Chance
{
    [TestClass]
    public class RandomEvent
    {
        [TestMethod]
        public void checkchance()
        {
            Chancecheck sys = new Chancecheck();
            int chance = sys.CheckEvent;
            Assert.IsTrue(chance > 0);

        }
    }
}
