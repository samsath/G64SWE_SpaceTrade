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
using SpaceGame.GameScreens;
using STDatabase;
using XRpgLibrary;
using XRpgLibrary.Controls;

namespace SpaceGame.Components
{
    public class SpaceShip : Object
    {
        public IDatabase dbs = new Database();
        string name;
        string hero;
        SpriteFont font1;
        Vector2 fontPosition; // red text about the dice
        string textureName;
        int owner; // user_id
        int shipId;
        bool newGame;

        Random random = new Random();
        int diceRolled = 0;
        int diceRemaining = 0; // variable used for the dice
        int numberOfTurns = 3; // Turns available for the player

        private Texture2D texture; // spaceship texture

        public Vector2 PositionByTile;
        public Vector2 PositionByPixel; // position of the ship

        private Vector2 startingPosition; // starting position of the ship

        KeyboardState keyboardState; // get input "space" to start roll the dice
        enum ShipState
        {
            Waiting,
            Moving
        }

        ShipState currentState = ShipState.Waiting; // state of the ship

        const int SHIP_SPEED = 100;
        Vector2 shipSpeed;
        Vector2 shipDirection; // help with ship moving

        int money;



        private Texture2D shipTexture;
        List<Resource> shipResource;
        //Dictionary<Resource, int> result;
        private string gameState = "playing";

        
        int cargoCapacity;
        int cargoLevel = 1;

        public SpaceShip()
        {
            shipResource = new List<Resource>();
        }

        // Load the content
        public void LoadContent(ContentManager content)
        {
            Console.WriteLine("Space Ship LoadContent");
            font1 = content.Load<SpriteFont>(@"Fonts\CourierNew");
            PositionByTile = new Vector2(0, 0);
            startingPosition = new Vector2(50 + Tile.TileWidth / 4, 50 + Tile.TileHeight / 4);
            PositionByPixel.X = startingPosition.X;
            PositionByPixel.Y = startingPosition.Y;
            texture = shipTexture;
            if (newGame == true)
            {
                Console.WriteLine("Load to the ship");
                shipId = dbs.NewShipandMedia(1, getCargoCapacity(), getOwner(), 0, 0, gettextureName(), 0, "reason");
                Console.WriteLine("shipID = " + shipId);
            }
           
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
                if (numberOfTurns == 0) gameState = "endOfGame";
                else
                {
                    Debug.WriteLine(gameState + " " + random.Next(0, 99));
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
                    else if (keyboardState.IsKeyDown(Keys.U))
                    {
                        gameState = "Upgrade";
                    }

                    else if (keyboardState.IsKeyDown(Keys.Space))
                    {
                        //Determine the number of allowed moves
                        numberOfTurns--;
                        diceRolled = random.Next(1, 7);
                        diceRemaining = diceRolled;
                        currentState = ShipState.Moving;
                    }
                    Debug.WriteLine(gameState + " " + random.Next(0, 99));
                }
            }
            if (currentState == ShipState.Moving && numberOfTurns >= 0)
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
            texture = shipTexture;
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
                String sellString = "Press the S Button to Sell!";
                Vector2 sellVector = font1.MeasureString(sellString) / 2;
                fontPosition = new Vector2(200, 15);
                spriteBatch.DrawString(font1, sellString, fontPosition, Color.White, 0, sellVector, 1.0f, SpriteEffects.None, 0.5f);

                String buyString = "Press the B Button to Buy!";
                fontPosition = new Vector2(200, 35);
                spriteBatch.DrawString(font1, buyString, fontPosition, Color.White, 0, sellVector, 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.Draw(texture, new Rectangle((int)PositionByPixel.X, (int)PositionByPixel.Y, Tile.TileWidth / 2, Tile.TileHeight / 2), Color.White);

                String upgradeString = "Press the U Button to Upgrade your Ship!";
                fontPosition = new Vector2(600, 15);
                spriteBatch.DrawString(font1, upgradeString, fontPosition, Color.White, 0, sellVector, 1.0f, SpriteEffects.None, 0.5f);
                spriteBatch.Draw(texture, new Rectangle((int)PositionByPixel.X, (int)PositionByPixel.Y, Tile.TileWidth / 2, Tile.TileHeight / 2), Color.White);
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
                printDiceRolled = "Turns Remaining: " + numberOfTurns.ToString();
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
        public int getDiceRolled()
        {
            return diceRolled;
        }
        public void setState(string p)
        {
            if (p.Equals("waiting")) currentState = ShipState.Waiting;
        }
        public string getGameState()
        {
            return gameState;
        }
        /*
        public int getShipNumberOfResource()
        {
            int numberOfResource = 0;
            foreach (KeyValuePair<Resource, int> pair in shipResource)
            {
                Debug.WriteLine(pair.Key.getName() + " " + pair.Value);
                numberOfResource = numberOfResource + pair.Value;
            }
            return numberOfResource;
        }
         */ 
        public int getMoney()
        {
            return money;
        }


        /*
        public void buy(Dictionary<Resource, int> resource)
        {
            result = new Dictionary<Resource, int>();
            if (shipResource == null) shipResource = resource;
            else foreach (KeyValuePair<Resource, int> buying in resource)
                {
                    foreach (KeyValuePair<Resource, int> current in shipResource)
                    {
                        if (buying.Key.getName().Equals(current.Key.getName()))
                        {
                            result.Add(buying.Key, buying.Value + current.Value);
                        }


                    }
                }
        }
         */
        /*
        public Dictionary<Resource, int> getResourceList()
        {
            return shipResource;
        }
        */
        /*
        public void sell(Dictionary<Resource, int> resource)
        {
            result = new Dictionary<Resource, int>();
            foreach (KeyValuePair<Resource, int> buying in resource)
            {
                foreach (KeyValuePair<Resource, int> current in shipResource)
                {
                    if (buying.Key.getName().Equals(current.Key.getName()))
                    {
                        result.Add(buying.Key, current.Value - buying.Value);
                    }

                }
            }
        }
         */
        /*
        public void setResource(Dictionary<Resource, int> resource)
        {
            shipResource = resource;
        }
        */
        /*
        public Dictionary<Resource, int> getResultList()
        {
            return result;
        }
         */ 
        public void setGameState(string p)
        {
            gameState = p;
        }



        public void setNumberOfTurn(int turnAmount)
        {
            numberOfTurns = turnAmount;
        }
        public int getNumberOfTurn()
        {
            return numberOfTurns;
        }

        public void setCargoCapacity(int cargoAmount)
        {
            cargoCapacity = cargoAmount;
        }

        public void setShipTexture(Texture2D texture2D)
        {
            shipTexture = texture2D;
        }
        public int getCargoCapacity()
        {
            return cargoCapacity;
        }


        public void setMoney(int moneyAmount)
        {
            money = moneyAmount;
        }

        public int getCargoLevel()
        {
            return cargoLevel;
        }

        public void setHero(string name)
        {
            hero = name;
        }

        public string getHero()
        {
            return name;
        }

        public List<Resource> getResource()
        {
            return shipResource;
        }

        public void settextureName(string name)
        {
            textureName = name;
        }

        public string gettextureName()
        {
            return textureName;
        }

        public void setOwer(int id)
        {
            owner = id;
        }

        public int getOwner()
        {
            return owner;
        }


        public void setNewGame()
        {
            Console.WriteLine("SpaceShip New Game set");
            newGame = true;
        }

        public int getShipId()
        {
            return shipId;
        }

        public void addResource(int rId, string name, int price, string des, int amount )
        {
            Resource stuff = new Resource(rId, name, price,des,amount);
            shipResource.Add(stuff);
        }

        public void buySell(List<Resource> changedRes)
        {
            Console.WriteLine("SpaceShip buy Sell");
            for (int i = 0; i < changedRes.Count; i++)
            {
                Console.WriteLine("Spaceship change = " + changedRes[i].resourceid + changedRes[i].name + changedRes[i].amount + changedRes[i].price);
            }
            int newResCount = changedRes.Count;
            // this gets the changes from the buy or sell screen then updates the location resource list compaired to the outer one.
            List<int> nonMatch = new List<int>();
            for (int pr = 0; pr > shipResource.Count; pr++)
            {
                // first goes through local list and the transaction list and if there are the same (by resource NUmber) the local will have the same information as the transaction
                for (int nR = 0; nR > newResCount; nR++)
                {
                    if (shipResource[pr].resourceid == changedRes[nR].resourceid)
                    {
                        Console.WriteLine("Ship Resource list = " + shipResource[pr].resourceid + " " + "Changes = " + changedRes[nR].resourceid);
                        // adds the changedRes number to the nonMatch list if the resource is in the local resources
                        nonMatch.Add(nR);
                        shipResource[pr].amount = changedRes[nR].amount;
                        shipResource[pr].price = changedRes[nR].price;
                    }
                }
            }

            // compares the nonMatch and the newResCount as if there are different there will need new items to add.
            if (newResCount != nonMatch.Count)
            {
                // then goes throught the nonMatch list and see's of the res number is in it. If it is not then the resource is added to the local list.
                for (int res = 0; res > newResCount; res++)
                {
                    if (!nonMatch.Contains(res))
                    {
                        Console.WriteLine("add new resource to shipRes = " + changedRes[res].resourceid + changedRes[res].name + changedRes[res].price + changedRes[res].description + changedRes[res].amount);
                        shipResource.Add(new Resource(changedRes[res].resourceid, changedRes[res].name, changedRes[res].price, changedRes[res].description, changedRes[res].amount));
                    }
                }
            }

        }
    }
}
