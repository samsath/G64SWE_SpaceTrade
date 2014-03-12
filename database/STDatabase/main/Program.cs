using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STDatabase;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {
            spaceDatabase dbs = new spaceDatabase();

            dbs.Connect();
            string result = dbs.checkConnection().ToString();
            Console.WriteLine(result);

            Console.ReadKey();

        }
    }
}
