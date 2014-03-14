﻿using System;
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
            sys = new Objects();

        }
        [TestMethod]
        public void TheObjects_Constructs_OK()
        {
            Assert.IsNotNull(sys);
        }
        [TestMethod]
        public void DataBaseID_CheckItHasIntVal()
        {
            int id = sys.Dt_Id;
            Assert.AreEqual(0, id);

        }
        [TestMethod]
        public void Check_ItHas_Title()
        {
            string title1 = sys.Title;
            Assert.AreEqual("a", title1);
        }
        [TestMethod]
        public void Check_ItHas_Graphic()
        {
            string graphic1 = sys.Graphic;
            Assert.AreEqual("b", graphic1);
        }


    }
}
