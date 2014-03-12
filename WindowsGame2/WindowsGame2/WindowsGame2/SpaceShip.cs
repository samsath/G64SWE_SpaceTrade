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
        private float scale = 1.0f;

        KeyboardState keyboardState;
        enum State
        {
            Waiting,
            Moving
        }
        State currentState = State.Waiting;

        string name = "spaceship";

        Vector2 shipSpeed;
        Vector2 shipDirection;

        public void LoadContent(ContentManager content)
        {
            font1 = content.Load<SpriteFont>(@"Font\CourierNew");
            PositionByTile = new Vector2(0, 0);
            base.LoadContent(content, name);
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
                    diceRolled = random.Next(1,7);
                    diceRemaining = diceRolled;
                    currentState = State.Moving;
                }
            }
            if (currentState == State.Moving){
                if (PositionByTile.Y == 0 && PositionByTile.X != 9)
                {
                    shipSpeed.X = SHIP_SPEED;
                    shipDirection.X = 1;
                }
                else if (PositionByTile.X == 9 && PositionByTile.Y != 9){
                    shipSpeed.Y = SHIP_SPEED;
                    shipDirection.Y = 1;
                }
                else if (PositionByTile.Y == 9 && PositionByTile.X != 0){
                    shipSpeed.X = SHIP_SPEED;
                    shipDirection.X = -1;
                }
                else if (PositionByTile.X == 0 && PositionByTile.Y != 0){
                    shipSpeed.Y = SHIP_SPEED;
                    shipDirection.Y = -1;
                }
                PositionByPixel += shipDirection * shipSpeed * (float)time.ElapsedGameTime.TotalSeconds;
            }
            float differentX = Math.Abs(Math.Abs(PositionByPixel.X - (50+BoardLocation.TileWidth/4 + PositionByTile.X*BoardLocation.TileWidth))-70);
            float differentY = Math.Abs(Math.Abs(PositionByPixel.Y - (50+BoardLocation.TileHeight/4 + PositionByTile.Y*BoardLocation.TileHeight))-50);
            Debug.WriteLine("DifferenceX: " + differentX);
            Debug.WriteLine("DifferenceY: " + differentY);
            Debug.WriteLine("We are at " + PositionByTile.X + "," + PositionByTile.Y);
            Debug.WriteLine("We are at " + PositionByPixel.X + "," + (50.0 + BoardLocation.TileWidth / 4.0 + PositionByTile.X * BoardLocation.TileWidth));
            Debug.WriteLine("We are at " + PositionByPixel.Y + "," + (50.0 + BoardLocation.TileHeight / 4.0 + PositionByTile.Y * BoardLocation.TileHeight));
            if (differentX<3||differentY<3)
            {
                PositionByTile.X = (int)Math.Round((PositionByPixel.X - 70) / BoardLocation.TileWidth);
                PositionByTile.Y = (int)Math.Round((PositionByPixel.Y - 60) / BoardLocation.TileHeight);
                diceRemaining--;
            }
        }

        //Draw the sprite to the screen
        public void subDraw(SpriteBatch theSpriteBatch)
        {
            String printDiceRolled = "Dice rolled is: " + diceRolled.ToString();
            Vector2 FontOrigin = font1.MeasureString(printDiceRolled) / 2;
            fontPosition = new Vector2(100, 15);
            theSpriteBatch.DrawString(font1, printDiceRolled, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            printDiceRolled = "Move remaining is: " + diceRemaining.ToString();
            fontPosition = new Vector2(100, 35);
            theSpriteBatch.DrawString(font1, printDiceRolled, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            base.Draw(theSpriteBatch);
        }

        //Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        //public void Update(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection)
        //{
        //    Position += theDirection * theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
        //}
    }
}
