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
using XRpgLibrary;
using XRpgLibrary.Controls;
using STDatabase;
using SpaceGame.Components;

namespace SpaceGame.GameScreens
{


    public class SaveHistoryScreen : BaseGameState
    {
        #region Field region

        PictureBox backgroundImage;
        PictureBox arrowImage;
        LinkLabel Session1;
        LinkLabel Session2;
        LinkLabel Session3;
        LinkLabel Session4;
        LinkLabel Session5;
        public int[] sessionNumber = new int[5] { 0, 0, 0, 0, 0 };

        // for saving
        IDatabase dbs = new Database();


        float maxItemWidth = 0f;

        #endregion

        #region Property Region
        #endregion

        #region Constructor Region

        public SaveHistoryScreen(Game game, GameStateManager manager)
            : base(game, manager)
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

            ContentManager Content = Game.Content;

            backgroundImage = new PictureBox(
                Content.Load<Texture2D>(@"Backgrounds\titlescreen"),
                GameRef.ScreenRectangle);
            ControlManager.Add(backgroundImage);

            Texture2D arrowTexture = Content.Load<Texture2D>(@"GUI\leftarrowUp");

            string[] SessionString = new string[5]{"","","","",""};
            

            List<Userdata> ses = dbs.getSessionNum();
            int sesNumber = ses.Count;
            Console.WriteLine(sesNumber);
            for (int i = 0; i < sesNumber; i++)
            {
                Console.WriteLine(ses[i].Session_id + "," + ses[i].Name + "," + ses[i].HighScore);
                SessionString[i] = ses[i].Session_id + "," + ses[i].Name + "," + ses[i].HighScore;
                sessionNumber[i] = ses[i].Session_id;
                if (i > 5)
                {
                    break;
                }
            }

                arrowImage = new PictureBox(
                    arrowTexture,
                    new Rectangle(
                        0,
                        0,
                        arrowTexture.Width,
                        arrowTexture.Height));
            ControlManager.Add(arrowImage);

            Session1 = new LinkLabel();
            Session1.Text = SessionString[0];
            Session1.Size = Session1.SpriteFont.MeasureString(Session1.Text);
            Session1.Selected += new EventHandler(menuItem_Selected);

            ControlManager.Add(Session1);

            Session2 = new LinkLabel();
            Session2.Text = SessionString[1];
            Session2.Size = Session2.SpriteFont.MeasureString(Session2.Text);
            Session2.Selected += menuItem_Selected;

            ControlManager.Add(Session2);

            Session3 = new LinkLabel();
            Session3.Text = SessionString[2];
            Session3.Size = Session3.SpriteFont.MeasureString(Session3.Text);
            Session3.Selected += menuItem_Selected;

            ControlManager.Add(Session3);

            Session4 = new LinkLabel();
            Session4.Text = SessionString[3];
            Session4.Size = Session4.SpriteFont.MeasureString(Session4.Text);
            Session4.Selected += new EventHandler(menuItem_Selected);

            ControlManager.Add(Session4);

            Session5 = new LinkLabel();
            Session5.Text = SessionString[4];
            Session5.Size = Session5.SpriteFont.MeasureString (Session5.Text);
            Session5.Selected += new EventHandler(menuItem_Selected);

            ControlManager.Add(Session5);

            ControlManager.NextControl();

            ControlManager.FocusChanged += new EventHandler(ControlManager_FocusChanged);

            Vector2 position = new Vector2(350, 300);
            foreach (Control c in ControlManager)
            {
                if (c is LinkLabel)
                {
                    if (c.Size.X > maxItemWidth)
                        maxItemWidth = c.Size.X;

                    c.Position = position;
                    position.Y += c.Size.Y + 5f;
                }
            }

            ControlManager_FocusChanged(Session1, null);
        }

        void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Vector2 position = new Vector2(control.Position.X + maxItemWidth + 10f, control.Position.Y);
            arrowImage.SetPosition(position);
        }

        private void menuItem_Selected(object sender, EventArgs e)
        {
            if (sender == Session1)
            {
                GameRef.board.setSession(sessionNumber[0]);
                Console.WriteLine("Session = " + sessionNumber[0]);
                StateManager.ChangeState(GameRef.GamePlayScreen);
                
            }

            if (sender == Session2)
            {
                Console.WriteLine("Session = " + sessionNumber[1]);
                GameRef.board.setSession(sessionNumber[1]);
                StateManager.ChangeState(GameRef.GamePlayScreen);
            }

            if (sender == Session3)
            {
                Console.WriteLine("Session = " + sessionNumber[2]);
                GameRef.board.setSession(sessionNumber[2]);
                StateManager.ChangeState(GameRef.GamePlayScreen);
            }

            if (sender == Session4)
            {
                Console.WriteLine("Session = " + sessionNumber[3]);
                GameRef.board.setSession(sessionNumber[3]);
                StateManager.ChangeState(GameRef.GamePlayScreen);
            }
            if (sender == Session5)
            {
                Console.WriteLine("Session = " + sessionNumber[4]);
                GameRef.board.setSession(sessionNumber[4]);
                StateManager.ChangeState(GameRef.GamePlayScreen);
            }
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, playerIndexInControl);

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

        #region Game State Method Region
        #endregion

    }
}