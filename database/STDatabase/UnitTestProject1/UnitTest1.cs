using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STDatabase;


namespace DatabaseUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        FakeDatabase db;
        [TestInitialize]
        public void init()
        {
            db = new FakeDatabase();
        }

        [TestMethod]
        public void TestToSeeDBisIniitased
        {
            Assert.IsNotNull(db);
        }
    }
}
