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


        Texture2D backgroundScreen;

        #endregion

        #region Constructor Region

        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
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
            GameRef.board.LoadContent(GameRef.Content);
            //Debug.WriteLine("hgfhfdgdffgs " + manager.shipName);
            GameRef.spaceShip.LoadContent(GameRef.Content);
            backgroundScreen = GameRef.Content.Load<Texture2D>(@"Backgrounds\galaxy");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //Debug.WriteLine(GameRef.spaceShip.getGameState());
            if (GameRef.spaceShip.getGameState().Equals("playing"))
            {
                GameRef.spaceShip.Update(gameTime);
            }
            else if (GameRef.spaceShip.getGameState().Equals("endOfGame"))
            {
                StateManager.ChangeState(GameRef.EndGameScreen);
            }
            else if (GameRef.spaceShip.getGameState().Equals("Buy"))
            {
                StateManager.ChangeState(GameRef.BuyScreen);
            }
            else if (GameRef.spaceShip.getGameState().Equals("Sell"))
            {
                StateManager.ChangeState(GameRef.SellScreen);
            }
            else if (GameRef.spaceShip.getGameState().Equals("Escape"))
            {
                StateManager.ChangeState(GameRef.PauseScreen);
            }
            else if (GameRef.spaceShip.getGameState().Equals("Upgrade"))
            {
                StateManager.ChangeState(GameRef.upgradeScreen);
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
                    GameRef.board.Draw(GameRef, GameRef.SpriteBatch, GameRef.spaceShip);

                    // 3. Draw the Ship
                    GameRef.spaceShip.Draw(GameRef.SpriteBatch);
                    GameRef.SpriteBatch.End();
          

            base.Draw(gameTime);


        }

        

    }
}
