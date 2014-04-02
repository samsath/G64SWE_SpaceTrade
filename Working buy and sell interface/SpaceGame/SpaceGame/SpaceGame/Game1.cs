using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using XRpgLibrary;
using SpaceGame.GameScreens;
using SpaceGame.Components;

namespace SpaceGame
{
        /*
         * Main class of the game.
         */
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region XNA Field Region

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);

        GraphicsDeviceManager graphics;
        public SpriteBatch SpriteBatch;

        #endregion

        #region Game State Region

        GameStateManager stateManager;

        public TitleScreen TitleScreen;
        public StartMenuScreen StartMenuScreen;
        public GamePlayScreen GamePlayScreen;
        public AdminScreen AdminScreen;
        public CharacterGeneratorScreen CharacterGeneratorScreen;
        public EndGameScreen EndGameScreen;
        public BuyScreen BuyScreen;
        public SellScreen SellScreen;
        public PauseScreen PauseScreen;
        public HighscoreScreen highscoreScreen;
        public UpgradeScreen upgradeScreen;
        public SaveHistoryScreen saveHistory;
        public InitialPriceScreen initPrice;

        public SpaceShip spaceShip;
        public Board board;

        #endregion

        #region Screen Field Region

        const int screenWidth = 1024;
        const int screenHeight = 768;

        public readonly Rectangle ScreenRectangle;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

            ScreenRectangle = new Rectangle(
                0,
                0,
                screenWidth,
                screenHeight);

            Content.RootDirectory = "Content";

            Components.Add(new InputHandler(this));

            stateManager = new GameStateManager(this);
            Components.Add(stateManager);

            TitleScreen = new TitleScreen(this, stateManager);
            StartMenuScreen = new StartMenuScreen(this, stateManager);
            GamePlayScreen = new GamePlayScreen(this, stateManager);
            AdminScreen = new AdminScreen(this, stateManager);
            CharacterGeneratorScreen = new CharacterGeneratorScreen(this, stateManager);
            EndGameScreen = new EndGameScreen(this, stateManager);
            PauseScreen = new PauseScreen(this, stateManager);
            highscoreScreen = new HighscoreScreen(this, stateManager);
            upgradeScreen = new UpgradeScreen(this, stateManager);
            saveHistory = new SaveHistoryScreen(this, stateManager);
            initPrice = new InitialPriceScreen(this, stateManager);

            spaceShip = new SpaceShip();
            board = new Board();

            stateManager.ChangeState(TitleScreen);
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            BuyScreen = new BuyScreen(this, stateManager);
            SellScreen = new SellScreen(this, stateManager);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
