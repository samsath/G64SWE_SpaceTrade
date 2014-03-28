using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop;

namespace shop
{
    [TestClass]
    public class Chance
    {
      

        [TestMethod]
        public void chancecheck_Constructs_OK()
        {
           chancecheck  sys = new chancecheck ();
            Assert.IsNotNull(sys);
        }

        [TestMethod]
        //call the random chance-picking function
        public void getchance()
        {
            chancecheck  sys = new chancecheck();
            sys.getchance(2);
            sys.checkvalue(4);
            Assert.AreEqual(4, sys.checkvalue(4));
        }
          
        [TestMethod]
        public void checkvalue()
        {

           chancecheck  sys = new chancecheck ();
            sys.checkvalue(4);
            Assert.AreEqual(4, sys.checkvalue(4));
            Assert.IsNotNull(sys);
        }



                }
            



         }
        
    

