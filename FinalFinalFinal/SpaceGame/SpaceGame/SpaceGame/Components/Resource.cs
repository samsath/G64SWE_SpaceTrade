using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceGame.Components
{
    public class Resource
    {

        public string name { get; set; }
        public int price { get; set; }
        public int InitialPrice { get; set; }
        public int resourceid { get; set; }
        public string description { get; set; }
        public int amount { get; set; }

        public Resource(int resid, string name, int price, string descript,int amounts)
        {
            this.resourceid = resid;
            this.name = name;
            this.price = price;
            this.InitialPrice = price;
            this.description = descript;
            this.amount = amounts;

        }

        public string getName()
        {
            return name;
        }

        public int getAmount()
        {
            return amount;
        }

        public int getPrice()
        {
            return price;
        }

        public int getResourceID()
        {
            return resourceid;
        }

        public void economicRole()
        {

        }

        public void setAmount(int amount)
        {
            this.amount = amount;
        }
    }
}
