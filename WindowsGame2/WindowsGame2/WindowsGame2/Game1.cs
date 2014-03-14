using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Board myBoard;
        SpaceShip myShip;

        enum GameStates { TitleScreen, Playing, GameOver };
        GameStates gameState = GameStates.Playing;

        Texture2D backgroundScreen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            myBoard = new Board();
            myShip = new SpaceShip();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            myBoard.LoadContent(this.Content);
            myShip.LoadContent(this.Content);
            backgroundScreen = Content.Load<Texture2D>(@"Textures\background");
            
            
            
            /////////////////////

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            switch (gameState)
            {
                case GameStates.TitleScreen:

                    ////////////////////

                    break;
                case GameStates.Playing:
                    myShip.Update(gameTime);
                    
                    break;
                case GameStates.GameOver:
                    break;
            }
            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            switch (gameState)
            {
                case GameStates.TitleScreen:

                    ////////////////////

                    break;
                case GameStates.Playing:
                    spriteBatch.Begin();
                    spriteBatch.Draw(backgroundScreen, new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height), Color.White);
                    for (int x = 0; x < Board.GameBoardWidth; x++)
                        for (int y = 0; y < Board.GameBoardHeight; y++)
                        {
                            if (x == 0||y == 0||x == (Board.GameBoardWidth - 1)||y==(Board.GameBoardHeight - 1))
                            {
                                int pixelX = 50 + (x * Tile.TileWidth);
                                int pixelY = 50 + (y * Tile.TileHeight);
                                DrawEmptyTile(pixelX, pixelY);
                            }
                        }
                    myShip.Draw(this.spriteBatch);
                    spriteBatch.End();
                    break;
                case GameStates.GameOver:
                    break;
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void DrawEmptyTile(int pixelX, int pixelY)
        {
            spriteBatch.Draw(emptyTile, new Rectangle(pixelX, pixelY, Tile.TileWidth, Tile.TileHeight), Color.White);
        }
    }
}
