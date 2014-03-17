using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsClassLibarary;

namespace PlanetsClassTest
{
    [TestClass]
    public class UnitTest1
    {
        Planets pln; // refrenceing class object and the libary for class plantes because of reoder the place of the class libaray
        [TestInitialize]
        public void init()
        {
            pln = new Planets();// create an object from the class for all testmethoed 

        }
        [TestMethod]
        public void ThePlanet_Constructs_OK()
        {
            Assert.IsNotNull(pln); // test the constractor 
        }
        public void ThePlanet_Diameter_OK()
        {
            double[] b = new double[2];// create an array of duble type for the planet diameter 
            b = pln.Diameter; //assign it to array variable  to help testing it
            double[] a = new double[2] { 1.1, 1.1 };// declare a class variable for the test
            CollectionAssert.AreEqual(a, b);
        }

    }
}
