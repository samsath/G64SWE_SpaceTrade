using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame2
{
    class Resource
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

        private int initialPrice;
        public int InitialPrice
        {
            get { return initialPrice; }
            set { initialPrice = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public void economicRole()
        {

        }
    }
}
