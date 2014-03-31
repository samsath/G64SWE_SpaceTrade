using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;

namespace priceChanging
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void pricesSell_construct()
        {
            price sys = new price();
            Assert.IsNotNull(sys);
        }

        [TestMethod]
        public void nameTake()
        {
            //get the original price from the resource name
            price sys = new price();
            string resourceName = "gold";
            sys.takePrice(resourceName);
            Assert.AreEqual(200, sys.takePrice(resourceName));
        }

        [TestMethod]
        public void priceLevel()
        {
            
            int offerPrice = 220;
            int oringinalPrice = 200;
            //return the computer purchase level(from 1 to 3)
            price sys = new price();
            sys.getPriceLevel(offerPrice,oringinalPrice );
            Assert.AreEqual(4, sys.getPriceLevel(offerPrice, oringinalPrice));
            
        }
        [TestMethod]
        public void amountOffer()
        {

            int priceLevel = 3;
            price sys = new price();
            sys.offerAmount(priceLevel);
            Assert.AreNotEqual(21, sys.offerAmount(priceLevel));
            
        }
    }
}
