using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame2
{
    class Tile
    {
        private string locationType;
        public static string[] LocationType = 
        { 
            "planet", 
            "space",
            "blackhole", 
            "corner",
        };
        public static int TileWidth = 64;
        public static int TileHeight = 47;

        
        public Tile(string type)
        {
            locationType = type;
        }

    }
}
