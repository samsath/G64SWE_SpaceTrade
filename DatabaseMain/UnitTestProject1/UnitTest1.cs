using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STDatabase;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        Database db;
        [TestInitialize]
        public void init()
        {
            db = new Database();
        }


        [TestMethod]
        public void CheckIfdatabaseConnects()
        {
            Assert.IsTrue(db.Connect());

        }

        [TestMethod]
        public void CreateNewDatabaseContentWorks()
        {
            Assert.IsTrue(db.newCreat());
        }

        [TestMethod]
        public void CheckifDatabaseisActive()
        {
            Assert.IsTrue(db.Check());
        }

        [TestMethod]
        public void ConnectionClose()
        {
            Assert.IsTrue(db.Close());
        }


    }
}
