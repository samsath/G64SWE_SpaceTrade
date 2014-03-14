using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
    class Board
    {

        Texture2D emptyTile;
        public const int NumberofTilesWidth = 11;
        public const int NumberofTilesHeight = 11;


        private Tile[] tiles = new Tile[38];
        public Board()
        {
            for (int i = 0; i < 38; i++)
            {
                tiles[i] = new Tile("space");
            }
        }

        public void LoadContent(ContentManager content)
        {
            emptyTile = content.Load<Texture2D>(@"Textures\emptyTile");
        }

        public void Draw(Game1 game, SpriteBatch spriteBatch)
        {
            
            for (int x = 0; x < NumberofTilesWidth; x++)
                for (int y = 0; y < NumberofTilesHeight; y++)
                {
                    if (x == 0 || y == 0 || x == (NumberofTilesWidth - 1) || y == (NumberofTilesHeight - 1))
                    {
                        int pixelX = 50 + (x * Tile.TileWidth);
                        int pixelY = 50 + (y * Tile.TileHeight);
                        spriteBatch.Draw(emptyTile, new Rectangle(pixelX, pixelY, Tile.TileWidth, Tile.TileHeight), Color.White);
                    }
                }
        }
    }
}
