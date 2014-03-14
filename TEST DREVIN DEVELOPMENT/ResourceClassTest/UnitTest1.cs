using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResourceClassLibrary;

namespace ResourceClassTest
{
    [TestClass]
    public class UnitTest1
    {
        Resource sys;
        [TestInitialize]
        public void init()
        {
            sys = new Resource();

        }
        [TestMethod]
        public void TheResource_Constructs_OK()
        {
            Assert.IsNotNull(sys);
        }
        [TestMethod]
        public void InitialPrice_CheckItHasIntVal()
        {
            int id = sys.InitialPrice;
            Assert.AreEqual(0, id);

        }
        [TestMethod]
        public void Check_ItHas_Description()
        {
            string description1 = sys.Description;
            Assert.AreEqual("c", description1);
        }
        [TestMethod]
        public void Check_ItHas_Name()
        {
            string description1 = sys.Name;
            Assert.AreEqual("c", description1);
        }

    }
}
