using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1
{
    public class upgrade
    {
        

        public int upgradeship(int money, int level, int compacity)
        {
            money -= 2500;
            compacity += 5;
            level += 1;
            if (money > 0 && level<4 &&level >0)//The top level is 3
            {
                return money;
            }
            else
            {
                return -1;//1 represents false
            }
        }
    }
}
