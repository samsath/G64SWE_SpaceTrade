using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    class Tile
    {
        Texture2D texture;

        Planet planet;

        public static int TileWidth = 64;
        public static int TileHeight = 47;


        public Tile(Texture2D texture, Planet planet)
        {
            this.texture = texture;
            this.planet = planet;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public Planet getPlanet()
        {
            return planet;
        }

        public Vector2 getBoardLocation(int listLocation)
        {
            if (listLocation >= 0 && listLocation < 11)
            {
                return new Vector2(listLocation, 0);
            }
            else if (listLocation >= 11 && listLocation < 21)
            {
                return new Vector2(10, listLocation - 10);
            }
            else if (listLocation >= 21 && listLocation < 31)
            {
                return new Vector2(listLocation - 10 - (listLocation - 20) * 2, 10);
            }
            else
            {
                return new Vector2(0, listLocation - 20 - 2 * (listLocation - 30));
            }
        }
    }
}
