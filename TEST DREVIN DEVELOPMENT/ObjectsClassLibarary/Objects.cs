using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
 */
///adding the using from the file 

namespace ObjectsClassLibarary
{
    public class Objects
        /// create the class with the name object + s  so we dont use the key word for the C# 
    {
        private  int dt_Id;
        private string title = "a";
        private  string graphic = "b";


        public int Dt_Id { get{return dt_Id;} set{dt_Id=value;} }

        public string Title { get { return title; } set { title = value; } }

        public string Graphic { get { return graphic; } set { graphic = value; } }
    }
}
