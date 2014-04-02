﻿using System;
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

using STDatabase;

namespace SpaceGame.GameScreens
{
    public class CharacterGeneratorScreen : BaseGameState
    {
        #region Field Region

        LeftRightSelector shipSelector;
        PictureBox backgroundImage;

        PictureBox shipImage;
        Texture2D[] shipImages;

        Texture2D stopTexture;

        //string[] genderItems = { "Male", "Female" };
        string[] shipItems = { "Human1", "alien1", /*"blue1", "bluecargo1",*/ "blueship1", "greenship1", "orangeship1", "RD1"/*, "wship1"*/ };

        //Keyboard
        //Texture2D sprite;// sprite variable
        KeyboardState oldKeyboardState, currentKeyboardState;// Keyboard state variables

        Label name = new Label();
        int xPos = 400;
        int yPos = 400;

        LinkLabel hero1 = new LinkLabel();
        LinkLabel hero2 = new LinkLabel();
        LinkLabel hero3 = new LinkLabel();
        LinkLabel hero4 = new LinkLabel();

        string hero1Txt;
        string hero2Txt;
        string hero3Txt;
        string hero4Txt;

        string selectedHero;

        public IDatabase dbs = new Database();

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
            currentKeyboardState = new KeyboardState();
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
            oldKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            ControlManager.Update(gameTime, PlayerIndex.One);
            /*if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                string characterName = "A";
                addKeys(characterName);
            }
            base.Update(gameTime);*/
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            base.Draw(gameTime);

            //GameRef.SpriteBatch.Draw(sprite, positionSprite, Color.White);
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

            /*LinkLabel linkLabel1 = new LinkLabel();
            linkLabel1.Text = "Accept This Ship and Your Hero";
            linkLabel1.Position = new Vector2(label1.Position.X-150, 500);
             
            linkLabel1.Selected += new EventHandler(linkLabel1_Selected);
            //Debug.WriteLine("hgfhfdgdffgs "+ship);*/



            shipImage = new PictureBox(
                shipImages[0],
                new Rectangle(600, 200, 96, 96));
            ControlManager.Add(shipImage);

            ControlManager.NextControl();


            //Hero Selection
            Label selectYourHero = new Label();
            selectYourHero.Text = "Please select Your Hero Name";
            selectYourHero.Position = new Vector2(label1.Position.X - 100, 350);
            ControlManager.Add(selectYourHero);

            ControlManager.NextControl();



            //heroes
            hero1 = new LinkLabel();
            hero1.Text = "Sam";
            hero1.Position = new Vector2(label1.Position.X + 50, 400);
            ControlManager.Add(hero1);
            hero1Txt = hero1.ToString();
            hero1.Selected += new EventHandler(hero1Selected);

            ControlManager.NextControl();

            hero2 = new LinkLabel();
            hero2.Text = "Dan";
            hero2.Position = new Vector2(label1.Position.X + 50, 430);
            ControlManager.Add(hero2);
            hero2.Selected += new EventHandler(hero2Selected);

            ControlManager.NextControl();

            hero3 = new LinkLabel();
            hero3.Text = "Truong";
            hero3.Position = new Vector2(label1.Position.X + 50, 460);
            ControlManager.Add(hero3);
            hero3.Selected += new EventHandler(hero3Selected);

            ControlManager.NextControl();

            hero4 = new LinkLabel();
            hero4.Text = "Rfnker";
            hero4.Position = new Vector2(label1.Position.X + 50, 490);
            ControlManager.Add(hero4);
            hero4.Selected += new EventHandler(hero4Selected);

            ControlManager.NextControl();
            //

            //Go Back button
            LinkLabel goBack = new LinkLabel();
            goBack.Text = "Go Back";
            goBack.Position = new Vector2(label1.Position.X + 40, 550);
            goBack.Selected += new EventHandler(goBackButton);

            //ControlManager.Add(linkLabel1);
            ControlManager.Add(goBack);


            //Keyboard name handling            
            name = new Label();
            name.Position = new Vector2(xPos, yPos);



        }

        int owner(string hero)
        {
            int money = GameRef.spaceShip.getMoney();
            int turn = GameRef.spaceShip.getNumberOfTurn();
            int owner = dbs.SetUser(hero, money, turn);
            Console.WriteLine("money " + money + " turn " + turn + " owner " + owner);
            return owner;
        }

        void hero1Selected(object sender, EventArgs e)
        {
            selectedHero = hero1.Text;
            GameRef.spaceShip.setNewGame();
            GameRef.spaceShip.setOwer(owner(selectedHero));
            GameRef.spaceShip.settextureName(shipItems[shipSelector.SelectedIndex]);
            GameRef.spaceShip.setHero(selectedHero);
            GameRef.spaceShip.setShipTexture(shipImages[shipSelector.SelectedIndex]);
            
            StateManager.ChangeState(GameRef.GamePlayScreen);
        }

        void hero2Selected(object sender, EventArgs e)
        {
            selectedHero = hero2.Text;
            GameRef.spaceShip.setNewGame();
            GameRef.spaceShip.setHero(selectedHero);
            GameRef.spaceShip.setOwer(owner(selectedHero));
            GameRef.spaceShip.settextureName(shipItems[shipSelector.SelectedIndex]);
            GameRef.spaceShip.setShipTexture(shipImages[shipSelector.SelectedIndex]);
            StateManager.ChangeState(GameRef.GamePlayScreen);
        }
        void hero3Selected(object sender, EventArgs e)
        {
            selectedHero = hero3.Text;
            GameRef.spaceShip.setHero(selectedHero);
            GameRef.spaceShip.setNewGame();
            GameRef.spaceShip.setOwer(owner(selectedHero));
            GameRef.spaceShip.settextureName(shipItems[shipSelector.SelectedIndex]);
            GameRef.spaceShip.setShipTexture(shipImages[shipSelector.SelectedIndex]);
            StateManager.ChangeState(GameRef.GamePlayScreen);
        }
        void hero4Selected(object sender, EventArgs e)
        {
           
            selectedHero = hero4.Text;
            GameRef.spaceShip.setNewGame();
            GameRef.spaceShip.setHero(selectedHero);
            GameRef.spaceShip.setOwer(owner(selectedHero));
            GameRef.spaceShip.settextureName(shipItems[shipSelector.SelectedIndex]);
            GameRef.spaceShip.setShipTexture(shipImages[shipSelector.SelectedIndex]);
            StateManager.ChangeState(GameRef.GamePlayScreen);
        }

        void addKeys(String characterName)
        {
            string appendedString = characterName;
            name.Position = new Vector2(xPos + 5, yPos);
            name.Text += appendedString;
            ControlManager.Add(name);
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

        /*void linkLabel1_Selected(object sender, EventArgs e)
        {
            GameRef.spaceShip.setShipTexture(shipImages[shipSelector.SelectedIndex]);
            StateManager.ChangeState(GameRef.GamePlayScreen);
        }*/

        void selectionChanged(object sender, EventArgs e)
        {
            shipImage.Image = shipImages[shipSelector.SelectedIndex];
            //Debug.WriteLine("hgfhfdgdffgs " + ship);
        }

        void goBackButton(object sender, EventArgs e)
        {
            StateManager.ChangeState(GameRef.AdminScreen);
        }

        #endregion
    }
}

