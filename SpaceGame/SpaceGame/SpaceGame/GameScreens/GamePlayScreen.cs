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
        GameStateManager manager;

        SpaceShip MyShip;

        Board myBoard; // board object
        
        Texture2D backgroundScreen;

        #region Property Region
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
            myBoard = new Board();
            MyShip = new SpaceShip();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            myBoard.LoadContent(GameRef.Content);
            Debug.WriteLine("hgfhfdgdffgs " + manager.shipName);
            MyShip.setShip(manager.shipName);
            MyShip.LoadContent(GameRef.Content);
            backgroundScreen = GameRef.Content.Load<Texture2D>(@"Textures\background");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            
            MyShip.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            /*GameRef.SpriteBatch.Begin(
                SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                Matrix.Identity);

            map.Draw(GameRef.SpriteBatch, player.Camera);

            base.Draw(gameTime);

            GameRef.SpriteBatch.End();*/

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
