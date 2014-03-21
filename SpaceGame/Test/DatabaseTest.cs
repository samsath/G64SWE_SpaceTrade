using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceGame.Components;

namespace Test
{
    class DatabaseTest
    {
        DatabasePopulate dbp;
        [TestInitialize]
        public void init()
        {
            dbp = new DatabasePopulate();

        }

        [TestMethod]
        public void ResourcesAddedatStartofGame(){
            Assert.IsTrue(dbp.Startresourceadd());
        }

    }
}
