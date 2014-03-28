using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace turnlibra
{
    public class turn
    {
        public int buyturn(int num,int turn,int money)
        {   
            money -= num * 1000;
            turn += num;
 
            if (money>=0)
            {
                return money;
                
            }
            else
            {
                return -1;
            }
            
        }
    }
}
