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
            if (value > 0)
            {
                switch (pick)
                {
                    case 1:
                        Console.WriteLine("lose 50 dollars"); break;
                    case 2:
                        Console.WriteLine("get 100 money"); break;
                    case 3:
                        Console.WriteLine("lose 50 dollars"); break;
                    case 4:
                        Console.WriteLine("lose 50 dollars"); break;
                    case 5:
                        Console.WriteLine("lose 50 dollars"); break;
                    case 6:
                        Console.WriteLine("lose 50 dollars"); break;
                    case 7:
                        Console.WriteLine("lose 50 dollars"); break;
                    case 8:
                        Console.WriteLine("lose 50 dollars"); break;
                    case 9:
                        Console.WriteLine("lose 50 dollars"); break;
                    case 10:
                        Console.WriteLine("lose 50 dollars"); break;



                }
            }
            else
            { Console.WriteLine("regular stop"); }




        }




        public int checkvalue(int p)
        {
             return p; 
        }

        public string checkchance(int p)
        {
            throw new NotImplementedException();
        }
    }
}
