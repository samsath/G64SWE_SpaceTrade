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
/*
 
This is a simplified ‘repository pattern’-like example, in that it separates the data store implementation from the business logic. It’s not quite strictly repository pattern, but hopefully it’s an intuitive example of TDD.  
It also does some dependency injection, hopefully in an easy to grasp way.
The key point is that we can swap real db access for mock db access at runtime, in the test call.


//create new solution and a testing project

//Write test

namespace TDD_DB_Injection_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestValidUserLoginDetailsViaDB()
        {
            DatabaseRepositoryClass dr = new DatabaseRepositoryClass();
            string validName = "BobSmith";
            string validPassword = "password1";

            string result = dr.getPassword(validName);

            Assert.AreEqual(validPassword, result);        
}
    }
}
//create new project, Class library, namespace DatabaseRepository, add to solution
//generate class and method as required

//Make class look like this (direct injection pattern):
//ie write constructor, and pass in DB access interface’d object

public class DatabaseRepositoryClass
    {
        IDatabaseAccess da;
        
        public DatabaseRepositoryClass()
        {
        }

        public string getPassword(string validName)
        {
            throw new NotImplementedException();
        }
    }

//generate interface in same file
public interface IDatabaseAccess
    {
         string Query_GetPasswordForName(string query);
    }
 Now looks like:
namespace DatabaseRespository
{
    public class DatabaseRepositoryClass
    {
        IDatabaseAccess da;
        
        public DatabaseRepositoryClass(IDatabaseAccess da_in)
        {
            da = da_in;
        }

        public string getPassword(string validName)
        {
            throw new NotImplementedException();
        }
    }

    public interface IDatabaseAccess
    {
         string Query_GetPasswordForName(string query);
    }
}

Now implement getPassword in the repository:
public string getPassword(string validName)
        {
            string query = "SELECT password FROM People WHERE name='"+validName+"'";
            string pw = da.Query_GetPasswordForName(query);
            return pw;
        }

We need to change our test to now Inject our Dependency in the constructor of the repository!

So we need to create a DatabaseAccessMock to handle the query. Write this in the test:
[TestMethod]
        public void TestValidUserLoginDetailsViaDB()
        {
            DatabaseAccessMock dam = new DatabaseAccessMock();
            DatabaseRepositoryClass dr = new DatabaseRepositoryClass(dam);
            string validName = "BobSmith";
            string validPassword = "password1";

            string result = dr.getPassword(validName);

            Assert.AreEqual(validPassword, result);
        } 

And generate it (done with new class, as can be in test project), add a using to the DatabaseRespository namespace (se we can see the interface definition), and implement the necessary method (after telling it to implement the interface):
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDD_DB_injection
{
    class DatabaseAccessMock
    {
    }
}

I.e. above becomes:
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseRespository;

namespace TDD_DB_Injection_Tests
{
    class DatabaseAccessMock:IDatabaseAccess
    {
        public string Query_GetPasswordForName(string query)
        {
            string result = "NULL";
            // lets stub a response. We could create a whole in memory database if we //wanted more complex behaviour
            if (query.Equals("SELECT password FROM People WHERE name='BobSmith'"))
                result = "password1";
            return result;


        }
    }
}

Now inject the mock in the test...
[TestMethod]
        public void TestValidUserLoginDetailsViaDB()
        {
            DatabaseAccessMock dam = new DatabaseAccessMock();
            DatabaseRepositoryClass dr = new DatabaseRepositoryClass(dam);
...
Test passes!!
We can now inject a real database (or webservice etc.) service object when we create the repository class.

 */
