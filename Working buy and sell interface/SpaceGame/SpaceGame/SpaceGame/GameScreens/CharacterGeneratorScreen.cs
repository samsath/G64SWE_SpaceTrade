using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using XRpgLibrary;
using XRpgLibrary.Controls;
using SpaceGame.Components;
using System.Diagnostics;

namespace SpaceGame.GameScreens
{
    public class CharacterGeneratorScreen : BaseGameState
    {
        #region Field Region
        //LeftRightSelector genderSelector;
        LeftRightSelector shipSelector;
        PictureBox backgroundImage;

        PictureBox shipImage;
        Texture2D[] shipImages;

        Texture2D stopTexture;
        int valueMoney;
        int valueNumberOfTurn;
        int valueInitialCargoCapacity;


        //string[] genderItems = { "Male", "Female" };
        string[] shipItems = { "Human1", "alien1", /*"blue1", "bluecargo1",*/ "blueship1", "greenship1", "orangeship1", "RD1"/*, "wship1"*/ };
        

        #endregion

        #region Property Region

        /*public string SelectedGender
        {
            get { return genderSelector.SelectedItem; }
        }*/

        public string SelectedShip
        {
            get { return shipSelector.SelectedItem; }
        }

        #endregion

        #region Constructor Region

        public CharacterGeneratorScreen(Game game, GameStateManager stateManager)
            : base(game, stateManager)
        {
        }

        #endregion

        #region XNA Method Region

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            base.LoadContent();

            LoadImages();
            CreateControls();
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            base.Draw(gameTime);

            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        #endregion

        #region Method Region

        private void CreateControls()
        {
            Texture2D leftTexture = Game.Content.Load<Texture2D>(@"GUI\leftarrowUp");
            Texture2D rightTexture = Game.Content.Load<Texture2D>(@"GUI\rightarrowUp");
            stopTexture = Game.Content.Load<Texture2D>(@"GUI\StopBar");

            backgroundImage = new PictureBox(
                Game.Content.Load<Texture2D>(@"Backgrounds\titlescreen"),
                GameRef.ScreenRectangle);
            ControlManager.Add(backgroundImage);

            Label label1 = new Label();

            label1.Text = "Choose Your Ship";
            label1.Size = label1.SpriteFont.MeasureString(label1.Text);
            label1.Position = new Vector2((GameRef.Window.ClientBounds.Width - label1.Size.X) / 2, 150);

            ControlManager.Add(label1);

            /*genderSelector = new LeftRightSelector(leftTexture, rightTexture, stopTexture);
            genderSelector.SetItems(genderItems, 125);
            genderSelector.Position = new Vector2(label1.Position.X, 200);
            genderSelector.SelectionChanged += new EventHandler(selectionChanged);

            ControlManager.Add(genderSelector);*/

            shipSelector = new LeftRightSelector(leftTexture, rightTexture, stopTexture);
            shipSelector.SetItems(shipItems, 125);
            shipSelector.Position = new Vector2(label1.Position.X, 250);
            shipSelector.SelectionChanged += selectionChanged;

            ControlManager.Add(shipSelector);

            LinkLabel linkLabel1 = new LinkLabel();
            linkLabel1.Text = "Use this Ship.";
            linkLabel1.Position = new Vector2(label1.Position.X, 300);
            linkLabel1.Selected += new EventHandler(linkLabel1_Selected);
            //Debug.WriteLine("hgfhfdgdffgs "+ship);

            ControlManager.Add(linkLabel1);

            shipImage = new PictureBox(
                shipImages[0],
                new Rectangle(600, 200, 96, 96));
            ControlManager.Add(shipImage);

            ControlManager.NextControl();

            //Go Back button
            LinkLabel goBack = new LinkLabel();
            goBack.Text = "Go Back";
            goBack.Position = new Vector2(label1.Position.X, 450);
            goBack.Selected += new EventHandler(goBackButton);
            ControlManager.Add(goBack);
        }

        private void LoadImages()
        {
            shipImages = new Texture2D[shipItems.Length];

            //for (int i = 0; i < genderItems.Length; i++)
            //{
            for (int j = 0; j < shipItems.Length; j++)
            {
                shipImages[j] = Game.Content.Load<Texture2D>(@"ShipSprites\" + shipItems[j]);
            }
            //}
        }

        void linkLabel1_Selected(object sender, EventArgs e)
        {
            GameRef.spaceShip.setShipTexture(shipImages[shipSelector.SelectedIndex]);
            StateManager.ChangeState(GameRef.GamePlayScreen);
        }

        void selectionChanged(object sender, EventArgs e)
        {
            shipImage.Image = shipImages[shipSelector.SelectedIndex];
            //Debug.WriteLine("hgfhfdgdffgs " + ship);
        }

        void goBackButton(object sender, EventArgs e)
        {
            StateManager.ChangeState(GameRef.AdminScreen);
        }

        public void setMoney(int moneyAmount)
        {
            valueMoney = moneyAmount;
        }

        public void setNumberOfTurn(int turns)
        {
            valueNumberOfTurn = turns;
        }

        public void setInitialCargoCapacity(int capacity)
        {
            valueInitialCargoCapacity = capacity;
        }



        #endregion
    }
}

