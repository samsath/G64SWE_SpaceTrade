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
            sys.checkvalue(8);
            Console.WriteLine(sys.checkchance(8) );

        }
    }
}
