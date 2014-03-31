using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop;


namespace main
{
    class Program
    {
        static void Main()
        {
            chancecheck sys = new chancecheck() ;
            Console.WriteLine(sys.checkvalue(8));
            sys.getchance(8);
            Console.ReadKey();
        }
    }
}
