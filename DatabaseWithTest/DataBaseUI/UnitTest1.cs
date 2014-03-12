using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseSt;

namespace DataBaseUI
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ConnectionCreate()
        {
            Database dbs = new Database();
            dbs.Connect();
            Assert.IsTrue(dbs.checkConnection());
        }
    }
}
