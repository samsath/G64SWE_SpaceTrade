using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsClassLibarary;

namespace ObjectClassTest
{
    [TestClass]
    public class UnitTest1
    {
        Objects sys;
        [TestInitialize]
        public void init()
        {
            sys = new Objects(); //inlialize the object from the class once on the class initialize  

        }
        [TestMethod]
        public void TheObjects_Constructs_OK()
        {
            Assert.IsNotNull(sys);  // checl wheither the object has been created 
        }
        [TestMethod]
        public void DataBaseID_CheckItHasIntVal()
        {
            int id = sys.Dt_Id;    // assign the object database to the new int variable to test it
            Assert.AreEqual(0, id);// test it with 0 value which is the value initalize for any unassign intger

        }
        [TestMethod]
        public void Check_ItHas_Title()
        {
            string title1 = sys.Title;// assign the title of the object to string virable to use it in the test
            Assert.AreEqual("a", title1);// test the vrible with the variable aready assign to the private filed of the object to pass the test
        }
        [TestMethod]
        public void Check_ItHas_Graphic()
        {
            string graphic1 = sys.Graphic; // create a variable to use it with the test 
            Assert.AreEqual("b", graphic1);// as the test above for testing the graphic
        }


    }
}
