using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsClassLibarary;

namespace PlanetsClassTest
{
    [TestClass]
    public class UnitTest1
    {
        Planets pln;
        [TestInitialize]
        public void init()
        {
            pln = new Planets();

        }
        [TestMethod]
        public void ThePlanet_Constructs_OK()
        {
            Assert.IsNotNull(pln);
        }
        public void ThePlanet_Diameter_OK()
        {
            double[] b = new double[2];
            b = pln.Diameter;
            double[] a = new double[2] { 1.1, 1.1 };
            CollectionAssert.AreEqual(a, b);
        }

    }
}
