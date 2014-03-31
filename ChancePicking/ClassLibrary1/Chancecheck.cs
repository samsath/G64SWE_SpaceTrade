using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop
{
    public class chancecheck
    {


        public void getchance(int value)
        {
            Random c = new Random();
            int pick = c.Next(1, 10);
            //the value of stop over 0, then pick the chance card
            if (pick > 0)
            {
                switch (pick)
                {
                    case 1:
                        Console.WriteLine("lose 50 dollars"); break;
                    case 2:
                        Console.WriteLine("get 100 money"); break;
                    case 3:
                        Console.WriteLine("lose 60 dollars"); break;
                    case 4:
                        Console.WriteLine("lose 70 dollars"); break;
                    case 5:
                        Console.WriteLine("lose 80 dollars"); break;
                    case 6:
                        Console.WriteLine("lose 90 dollars"); break;
                    case 7:
                        Console.WriteLine("lose 150 dollars"); break;
                    case 8:
                        Console.WriteLine("lose 500 dollars"); break;
                    case 9:
                        Console.WriteLine("lose 5000 dollars"); break;
                    case 10:
                        Console.WriteLine("lose 500000 dollars"); break;
                    default :
                        Console.WriteLine("default case"); break;

                }
            }
            else
            { Console.WriteLine("regular stop"); }

        }

        public int checkvalue(int p)
        {
             return p; 
        }

     
    }
}
