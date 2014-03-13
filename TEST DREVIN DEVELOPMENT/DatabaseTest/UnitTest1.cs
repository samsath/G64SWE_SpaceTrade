using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBaseClass;

namespace DatabaseTest
{
    /// <summary>
    /// the test from the lecture note 
    /// create the unit test project then add class libarary named it DataBaseClass 
    /// create class IDatabaseComponent
    /// the test pass 
    /// </summary>
    [TestClass]
    class DB_service
    {
        private IDatabaseComponent _dc;
        public DB_service(IDatabaseComponent dc_in)
        {
	    _dc = dc_in;
	    }
    }

}

