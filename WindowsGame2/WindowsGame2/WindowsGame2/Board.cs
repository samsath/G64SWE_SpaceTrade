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

        public void addMovingShip(int p)
        {

        }

        public void LoadContent(ContentManager content)
        {
            emptyTile = content.Load<Texture2D>(@"Textures\emptyTile");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            String printDiceRolled = "Dice rolled is: " + diceRolled.ToString();
            Vector2 FontOrigin = font1.MeasureString(printDiceRolled) / 2;
            fontPosition = new Vector2(100, 15);
            theSpriteBatch.DrawString(font1, printDiceRolled, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            printDiceRolled = "Move remaining is: " + diceRemaining.ToString();
            fontPosition = new Vector2(100, 35);
            theSpriteBatch.DrawString(font1, printDiceRolled, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            theSpriteBatch.Draw(texture, PositionByPixel, new Rectangle(0, 0, Tile.TileWidth / 2, Tile.TileHeight / 2), Color.White);
        }
    }
}
