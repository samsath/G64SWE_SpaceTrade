using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testLibrary
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
                    default:
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


        public class finance
        {
            public double multiply1(double profit)//Three levels of money multiplier.
            {
                profit *= 1.1;
                return profit;
            }

            public double multiply2(double profit)
            {
                profit *= 1.2;
                return profit;
            }

            public double multiply3(double profit)
            {
                profit *= 1.3;
                return profit;
            }

            public double newblance(double balance, double oldprice, double newprice, int amount)
            {


                if (amount > 0)
                {
                    balance = (newprice - oldprice) * amount + balance;
                    return balance;
                }
                else if (amount < 0)
                { return -1; }

                return 0;
            }
        }
    }
}
    
