using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceClassLibrary
{
    public class Resource
    {
        private int initialPrice;
        private string description = "c";
        public int InitialPrice { get { return initialPrice; } set { initialPrice = value; } }
        public string Description { get { return description; } set { description = value; } }
    }
}
