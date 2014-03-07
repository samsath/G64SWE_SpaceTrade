using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using Devart.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseConnect;


namespace DatabaseUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        Database dbconnect;
        [TestInitialize]
        public void init()
        {
            dbconnect = new Database();
        }

        [TestMethod]
        public void DatabaseConnectionOpen()
        {
            //Assert.IsTrue(dbconnect.Connect());
        }

        [TestMethod]
        public void DatabaseConnectionClose()
        {
            //Assert.IsTrue(dbconnect.Close());

        }
        [TestMethod]
        public void DatabaseTableExsits()
        {
            Assert.IsInstanceOfType(dbconnect.doesTableExists("user"), typeof(string));
        }
    }
}
