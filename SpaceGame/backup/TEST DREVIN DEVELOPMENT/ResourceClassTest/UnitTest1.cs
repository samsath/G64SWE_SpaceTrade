using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResourceClassLibrary;

namespace ResourceClassTest
{
    [TestClass]
    public class UnitTest1
    {
        Resource res;
        [TestInitialize]
        public void init()
        {
            res = new Resource();

        }
        [TestMethod]
        public void TheResource_Constructs_OK()
        {
            Assert.IsNotNull(res);
        }
        [TestMethod]
        public void InitialPrice_CheckItHasIntVal()
        {
            int id = res.InitialPrice;
            Assert.AreEqual(0, id);

        }
        [TestMethod]
        public void Check_ItHas_Description()
        {
            string description1 = res.Description;
            Assert.AreEqual("c", description1);
        }

    }
}
