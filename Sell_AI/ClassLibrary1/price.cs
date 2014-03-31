using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1
{
    public class price
    {
        public int takePrice(string p)
        {
           
            switch (p)
            {
                case "gold":
                    return 200; 

                case "silver":
                    return 100;

                case "water":
                    return 150; 

                case "sdf":
                    return 170; 

                case "erer":
                    return 180; 

                case "gfdg":
                    return 190; 
            }

            return 0;
        }

        public int getPriceLevel(int offerPrice, int oringinalPrice)
        {
            double x;
            x = offerPrice /oringinalPrice ;
            if (x > 0 && x < 1 || x ==1)
            {
                return 4;
            }

            if (x>1.0 && x< 1.2)
            {
                return 3;
            }

            if (x == 1.2 || x > 1.2 && x < 1.5)
            {
                return 2;
            }

            if (x == 1.5 || x > 1.5 && x < 2.0)
            {
                return 1;
            }
            else
            { return 0; }

            
        }

        public int offerAmount(int priceLevel )
        {
            if (priceLevel >0 && priceLevel<5)
            {
                switch (priceLevel )
                {
                    case 1:
                        {
                            Random c = new Random();
                            int pick = c.Next(1, 3);
                            return pick;
                        }
                    case 2:
                        {
                            Random c = new Random();
                            int pick = c.Next(4, 8);
                            return pick;
                        }

                    case 3:
                        {
                            Random c = new Random();
                            int pick = c.Next(9, 20);
                            return pick;
                        }

                    case 4:
                        {
                            Random c = new Random();
                            int pick = c.Next(21, 100);
                            return pick;
                        }

                }
            }
            return 0;
        }
    }
}
