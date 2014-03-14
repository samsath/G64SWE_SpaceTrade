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
    class SpaceShip : Object
    {
        SpriteFont font1;
        Random random = new Random();
        const int SHIP_SPEED = 100;
        int diceRolled = 0;
        int diceRemaining = 0;
        Vector2 fontPosition;
        private Texture2D texture;
        public Vector2 PositionByTile = Vector2.Zero;
        public Vector2 PositionByPixel;

        private Vector2 startingPosition;

        KeyboardState keyboardState;
        enum State
        {
            Waiting,
            Moving
        }

        State currentState = State.Waiting;

        Vector2 shipSpeed;
        Vector2 shipDirection;

        public void LoadContent(ContentManager content)
        {
            font1 = content.Load<SpriteFont>(@"Font\CourierNew");
            PositionByTile = new Vector2(0, 0);
            startingPosition = new Vector2(50 + Tile.TileWidth / 4, 50 + Tile.TileHeight / 4);
            PositionByPixel.X = startingPosition.X;
            PositionByPixel.Y = startingPosition.Y;
            texture = content.Load<Texture2D>(@"Textures\background");
        }

        public void Update(GameTime time)
        {
            shipSpeed = Vector2.Zero;
            shipDirection = Vector2.Zero;
            keyboardState = Keyboard.GetState();
            if (diceRemaining == 0)
            {
                currentState = State.Waiting;
            }
            if (currentState == State.Waiting)
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    diceRolled = random.Next(1, 7);
                    diceRemaining = diceRolled;
                    currentState = State.Moving;
                }
            }
            if (currentState == State.Moving)
            {
                if (PositionByTile.Y == 0 && PositionByTile.X != (Board.NumberofTilesWidth-1))
                {
                    shipSpeed.X = SHIP_SPEED;
                    shipDirection.X = 1;
                }
                else if (PositionByTile.X == (Board.NumberofTilesWidth-1) && PositionByTile.Y != (Board.NumberofTilesHeight-1))
                {
                    shipSpeed.Y = SHIP_SPEED;
                    shipDirection.Y = 1;
                }
                else if (PositionByTile.Y == (Board.NumberofTilesHeight-1) && PositionByTile.X != 0)
                {
                    shipSpeed.X = SHIP_SPEED;
                    shipDirection.X = -1;
                }
                else if (PositionByTile.X == 0 && PositionByTile.Y != 0)
                {
                    shipSpeed.Y = SHIP_SPEED;
                    shipDirection.Y = -1;
                }
                PositionByPixel += shipDirection * shipSpeed * (float)time.ElapsedGameTime.TotalSeconds;
            }
            float differentX = Math.Abs(Math.Abs(PositionByPixel.X - (startingPosition.X + PositionByTile.X * Tile.TileWidth)) - Tile.TileWidth);
            float differentY = Math.Abs(Math.Abs(PositionByPixel.Y - (startingPosition.Y + PositionByTile.Y * Tile.TileHeight)) - Tile.TileHeight);
            Debug.WriteLine("DifferenceX: " + differentX);
            Debug.WriteLine("DifferenceY: " + differentY);
            Debug.WriteLine("We are at " + PositionByTile.X + "," + PositionByTile.Y);
            Debug.WriteLine("We are at " + PositionByPixel.X + "," + (startingPosition.X + PositionByTile.X * Tile.TileWidth));
            Debug.WriteLine("We are at " + PositionByPixel.Y + "," + (startingPosition.Y + PositionByTile.Y * Tile.TileHeight));
            if (differentX < 3 || differentY < 3)
            {
                PositionByTile.X = (int)Math.Round((PositionByPixel.X - Tile.TileWidth) / Tile.TileWidth);
                PositionByTile.Y = (int)Math.Round((PositionByPixel.Y - Tile.TileHeight) / Tile.TileHeight);
                diceRemaining--;
            }
        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            String printDiceRolled = "Dice rolled is: " + diceRolled.ToString();
            Vector2 FontOrigin = font1.MeasureString(printDiceRolled) / 2;
            fontPosition = new Vector2(100, 15);
            spriteBatch.DrawString(font1, printDiceRolled, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            printDiceRolled = "Move remaining is: " + diceRemaining.ToString();
            fontPosition = new Vector2(100, 35);
            spriteBatch.DrawString(font1, printDiceRolled, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(texture, PositionByPixel, new Rectangle(0, 0, Tile.TileWidth / 2, Tile.TileHeight / 2), Color.White);
        }
    }
}
