using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

          HHHHub hub;
        [TestInitialize]
        public void init()
        {
            hub  = new HHHHub();

        }
        [TestMethod]
        public void Thehub_Constructs_OK()
        {
            Assert.IsNotNull(hub);
        }
        [TestMethod]
        public void InitialPrice_CheckItHasIntVal()
        {
            int id = hub.InitialPrice;
            Assert.AreEqual(0, id);

        }
    }
}
