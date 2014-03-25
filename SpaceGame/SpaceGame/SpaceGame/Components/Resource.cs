using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceGame.Components
{
    public class Resource
    {
        private string name;
        private int price;

        public Resource(string name, int price)
        {
            this.name = name;
            this.price = price;
        }

        public string getName()
        {
            return name;
        }

        public int getPrice()
        {
            return price;
        }
        public void changePrice(int price)
        {
            this.price = price;
        }
        
    }
}
