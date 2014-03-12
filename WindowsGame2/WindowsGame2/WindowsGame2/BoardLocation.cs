using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame2
{
    class BoardLocation
    {
        private string locationType;
        public static string[] LocationType = 
        { 
            "planet", 
            "space",
            "blackhole", 
            "corner",
        };
        public static int TileWidth = 70;
        public static int TileHeight = 50;

        
        public BoardLocation(string type)
        {
            locationType = type;
        }

    }
}
