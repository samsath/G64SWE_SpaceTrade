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

        Texture2D space;
        public const int NumberofTilesWidth = 11;
        public const int NumberofTilesHeight = 11;
        Random random = new Random();


        private Tile[] tiles = new Tile[40];
        public Board()
        {
            int tileType = random.Next(0, 3);
            for (int i = 0; i < 40; i++)
            {
                switch (tileType)
                {
                    case 0:
                        tiles[i] = new Tile("space");
                        break;
                    case 1:
                        tiles[i] = new Tile("planet");
                        break;
                    case 2:
                        tiles[i] = new Tile("blackhole");
                        break;
                }
                
            }
        }

        public void LoadContent(ContentManager content)
        {
            space = content.Load<Texture2D>(@"Textures\space");
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
                        spriteBatch.Draw(space, new Rectangle(pixelX, pixelY, Tile.TileWidth, Tile.TileHeight), Color.White);
                    }
                }
        }
    }
}
