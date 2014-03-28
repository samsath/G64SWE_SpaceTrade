using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop
{
    public class finance
    {
        public double newprofit(double profit)
        {
            profit *= 1.1;
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
            { return oldprice; newprice;amount;}
        }
    }
}
