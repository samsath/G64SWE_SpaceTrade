using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectsClassLibarary
{
    public class Planets : Objects // inhartence  form object class so it must be refrences in the solution explorar 
    {
        private double[] diameter = new double[2] { 1.1, 1.1 };// assign to this value to test it 
        public double[] Diameter { get { return diameter; } set { diameter = value; } }
    }
}
