﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectsClassLibarary
{
    public class Planets : Objects 
    {
        private double[] diameter = new double[2] { 1.1, 1.1 };
        public double[] Diameter { get { return diameter; } set { diameter = value; } }
    }
}
