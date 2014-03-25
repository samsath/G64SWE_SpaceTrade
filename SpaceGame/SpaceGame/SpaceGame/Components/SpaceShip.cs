﻿using System;
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
using System.Diagnostics;
using SpaceGame.GameScreens;

using XRpgLibrary;
using XRpgLibrary.Controls;

namespace SpaceGame.Components
{
    /*internal class ResourceLabelSet
    {
        internal Label Label;
        internal LinkLabel LinkLabel;

        internal ResourceLabelSet(Label label, LinkLabel linkLabel)
        {
            Label = label;
            LinkLabel = linkLabel;
        }
    }*/

    public class SpaceShip 
    {
        string name;
        SpriteFont font1;
        Vector2 fontPosition; // red text about the dice

        Random random = new Random();
        int diceRolled = 0;
        int diceRemaining = 0; // variable used for the dice
        int numberOfMoves = 3; // Turns available for the player

        private Texture2D texture; // spaceship texture

        public Vector2 PositionByTile;
        public Vector2 PositionByPixel; // position of the ship

        private Vector2 startingPosition; // starting position of the ship

        KeyboardState keyboardState; // get input "space" to start roll the dice
        enum ShipState
        {
            Waiting,
            Moving//, 
            //end
        }

        ShipState currentState = ShipState.Waiting; // state of the ship

        const int SHIP_SPEED = 100;
        Vector2 shipSpeed;
        Vector2 shipDirection; // help with ship moving
        private string gameState = "playing";



        // Load the content
        public void LoadContent(ContentManager content)
        {
            font1 = content.Load<SpriteFont>(@"Fonts\CourierNew");
            PositionByTile = new Vector2(0, 0);
            startingPosition = new Vector2(50 + Tile.TileWidth / 4, 50 + Tile.TileHeight / 4);
            PositionByPixel.X = startingPosition.X;
            PositionByPixel.Y = startingPosition.Y;
            texture = content.Load<Texture2D>(@"ShipSprites\" + name);
        }

        // Update ship movement
        public void Update(GameTime time)
        {

            shipSpeed = Vector2.Zero;
            shipDirection = Vector2.Zero;
            keyboardState = Keyboard.GetState();
            if (diceRemaining == 0)
            {
                currentState = ShipState.Waiting;
            }
            if (currentState == ShipState.Waiting)
            {
                Debug.WriteLine(numberOfMoves);
                if (numberOfMoves == 0) gameState = "endOfGame";
                else if (keyboardState.IsKeyDown(Keys.Space))
                {
                    //Determine the number of allowed moves
                    numberOfMoves--;
                    diceRolled = random.Next(1, 7);
                    diceRemaining = diceRolled;
                    currentState = ShipState.Moving;
                }
            }
            if (currentState == ShipState.Moving && numberOfMoves >= 0)
            {
                if (PositionByTile.Y == 0 && PositionByTile.X != (Board.NumberofTilesWidth - 1))
                {
                    shipSpeed.X = SHIP_SPEED;
                    shipDirection.X = 1;
                }
                else if (PositionByTile.X == (Board.NumberofTilesWidth - 1) && PositionByTile.Y != (Board.NumberofTilesHeight - 1))
                {
                    shipSpeed.Y = SHIP_SPEED;
                    shipDirection.Y = 1;
                }
                else if (PositionByTile.Y == (Board.NumberofTilesHeight - 1) && PositionByTile.X != 0)
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
            /*
            Debug.WriteLine("DifferenceX: " + differentX);
            Debug.WriteLine("DifferenceY: " + differentY);
            Debug.WriteLine("We are at " + PositionByTile.X + "," + PositionByTile.Y);
            Debug.WriteLine("We are at " + PositionByPixel.X + "," + (startingPosition.X + PositionByTile.X * Tile.TileWidth));
            Debug.WriteLine("We are at " + PositionByPixel.Y + "," + (startingPosition.Y + PositionByTile.Y * Tile.TileHeight));
             */
            if (differentX < 3 || differentY < 3)
            {
                PositionByTile.X = (int)Math.Round((PositionByPixel.X - Tile.TileWidth) / Tile.TileWidth);
                PositionByTile.Y = (int)Math.Round((PositionByPixel.Y - Tile.TileHeight) / Tile.TileHeight);
                diceRemaining--;
            }
        }

        // Draw the ship to the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            /*
             *If else statement that allows the use of number of turns on the game.
             *If the dice gets to 0 then Game Over.
             */

            /*
         *If else statement that allows the use of phases on the game.
         *If the dice gets to 0 then show the buy & sell links.
         *Else show the dice number and the number of remaining moves
         */
            if (diceRemaining == 0)
            {
                //gameState = "Buy/Sell";
                String sellString = "Press the S Button to Sell";
                Vector2 sellVector = font1.MeasureString(sellString) / 2;
                fontPosition = new Vector2(200, 15);
                spriteBatch.DrawString(font1, sellString, fontPosition, Color.White, 0, sellVector, 1.0f, SpriteEffects.None, 0.5f);
                String buyString = "Press the B Button to Buy";
                fontPosition = new Vector2(200, 35);
                spriteBatch.DrawString(font1, buyString, fontPosition, Color.White, 0, sellVector, 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.Draw(texture, new Rectangle((int)PositionByPixel.X, (int)PositionByPixel.Y, Tile.TileWidth / 2, Tile.TileHeight / 2), Color.White);

                if (keyboardState.IsKeyDown(Keys.S))
                {
                    gameState = "Sell";
                }
                else if (keyboardState.IsKeyDown(Keys.B))
                {
                    gameState = "Buy";
                }
                else if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    gameState = "Escape";
                }

            }
            else
            {

                String printDiceRolled = "Dice Rolled is: " + diceRolled.ToString();
                Vector2 FontOrigin = font1.MeasureString(printDiceRolled) / 2;
                fontPosition = new Vector2(100, 15);
                spriteBatch.DrawString(font1, printDiceRolled, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                printDiceRolled = "Moves Remaining is: " + diceRemaining.ToString();
                fontPosition = new Vector2(100, 35);
                spriteBatch.DrawString(font1, printDiceRolled, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                printDiceRolled = "Turns Remaining: " + numberOfMoves.ToString();
                fontPosition = new Vector2(100, 50);
                spriteBatch.DrawString(font1, printDiceRolled, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.Draw(texture, new Rectangle((int)PositionByPixel.X, (int)PositionByPixel.Y, Tile.TileWidth / 2, Tile.TileHeight / 2), Color.White);
            }
            //else
            //{
            //    String endString = "You Are Out of Turns!!";
            //    Vector2 endVector = font1.MeasureString(endString)/3;
            //    fontPosition = new Vector2(100, 15);
            //    spriteBatch.DrawString(font1, endString, fontPosition, Color.White, 0, endVector, 1.0f, SpriteEffects.None, 0.5f);
            //    String buyString = "Game Over.";
            //    fontPosition = new Vector2(100, 35);
            //    spriteBatch.DrawString(font1, buyString, fontPosition, Color.White, 0, endVector, 1.0f, SpriteEffects.None, 0.5f);
            //    spriteBatch.Draw(texture, new Rectangle((int)PositionByPixel.X, (int)PositionByPixel.Y, Tile.TileWidth / 2, Tile.TileHeight / 2), Color.White);

            //}

        }

        public Vector2 getShipLocation()
        {
            return PositionByTile;
        }

        public void setShip(string texture)
        {

            name = texture;
        }


        public string getGameState()
        {
            return gameState;
        }
    }
}
