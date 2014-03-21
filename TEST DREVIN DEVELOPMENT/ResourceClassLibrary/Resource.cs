using ObjectsClassLibarary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceClassLibrary
{
    public class Resource // : Objects  // refrence the parant class ERROR WHEN WE REFRENCE
    {
        private int initialPrice = 0 ;
        private string description = "c";
        
        public int InitialPrice { get { return initialPrice; } set { initialPrice = value; } }
        public string Description { get { return description; } set { description = value; } }

       
    }
}
