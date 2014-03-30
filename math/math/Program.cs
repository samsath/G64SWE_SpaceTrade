using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("you amount is 3000"); // intialize the price from the planet 
          
            //Random rnd = new Random();
            int planetPrice = 3000;// intialize the price from the planet (database)
            for (int i = 0; i < 5; i++) // try five times 
            {
                int hiswantedprice = int.Parse(Console.ReadLine());    // get int value form the user as the price for sell
                //int hiswantedprice;
                 //   hiswantedprice = Console.ReadLine();
                    //int priceRand = rnd.Next(1, 50);
                    //int newprice = planetPrice +(planetPrice * priceRand) / 100;
                    //string newline = "\n";
                 Console.WriteLine("the offer  is    :" + hiswantedprice ); // write the price the player intered
                if (hiswantedprice < planetPrice * 0.7 ) // first case (swtich statment better ) price cheap
	                    {
		                Console.WriteLine("I willing to buy every thing");
	                    }

                else if (hiswantedprice < planetPrice * 1.3) // second case (swtich statment better ) price resnable price
                {
                    Console.WriteLine("I accpet you offer becose it less the 30% + the orgenal price");
                }
                else // third  case (swtich statment better ) price tooo expencive 
                    	{
                             Console.WriteLine("too expensive");
                    	}
                    
                    
              } 
         }
    }
}

