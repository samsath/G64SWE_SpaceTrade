using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop
{
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

        public  double newblance(double balance,double oldprice, double newprice, int amount)
        {
            

            if (amount > 0) 
            {
                balance = (newprice - oldprice) * amount + balance ;
                return balance;
            }
            else if(amount <0)
            { return -1; }

            return 0;
        }
    }
}
