using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

using XRpgLibrary;
using SpaceGame.Components;

namespace SpaceGame.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        

        #region Property Region

        GameStateManager manager;

        SpaceShip MyShip;

        Board myBoard; // board object

        Texture2D backgroundScreen;

        #endregion

        #region Constructor Region

        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            myBoard = new Board();
            MyShip = new SpaceShip();
            this.manager = manager;
        }

        #endregion


        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            myBoard.LoadContent(GameRef.Content);
            //Debug.WriteLine("hgfhfdgdffgs " + manager.shipName);
            MyShip.setShip(manager.shipName);
            MyShip.LoadContent(GameRef.Content);
            backgroundScreen = GameRef.Content.Load<Texture2D>(@"Backgrounds\galaxy");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //Debug.WriteLine(MyShip.getGameState());
            if (MyShip.getGameState().Equals("playing"))
            {
                MyShip.Update(gameTime);
            }
            else if (manager.shipName.Equals("playingScreen"))
            {
                manager.shipName = "";
                MyShip.setGameState("playing");
                MyShip.getGameState();
                MyShip.Update(gameTime);

            }
            else if (MyShip.getGameState().Equals("endOfGame"))
            {
                StateManager.ChangeState(GameRef.EndGameScreen, null);
            }
            else if (MyShip.getGameState().Equals("Buy"))
            {
                StateManager.ChangeState(GameRef.BuyScreen, null);
            }
            else if (MyShip.getGameState().Equals("Sell"))
            {
                StateManager.ChangeState(GameRef.SellScreen, null);
            }
            else if (MyShip.getGameState().Equals("Escape"))
            {
                StateManager.ChangeState(GameRef.SaveScreen, MyShip);
            }
            else if (MyShip.getGameState().Equals("Upgrade"))
            {
                StateManager.ChangeState(GameRef.upgradeScreen, null);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            

            GraphicsDevice.Clear(Color.Red);

                    GameRef.SpriteBatch.Begin();
                    GameRef.SpriteBatch.Draw(backgroundScreen, new Rectangle(0, 0, GameRef.Window.ClientBounds.Width, GameRef.Window.ClientBounds.Height), Color.White);

                    // 2. Draw the Board 
                    myBoard.Draw(GameRef, GameRef.SpriteBatch, MyShip);

                    // 3. Draw the Ship
                    MyShip.Draw(GameRef.SpriteBatch);
                    GameRef.SpriteBatch.End();
          

            base.Draw(gameTime);


        }

        

    }
}
