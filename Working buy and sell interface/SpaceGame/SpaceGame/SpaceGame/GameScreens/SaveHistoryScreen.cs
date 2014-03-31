using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using XRpgLibrary;
using XRpgLibrary.Controls;
using System.Diagnostics;
using STDatabase;
using SpaceGame.Components;

namespace SpaceGame.GameScreens
{


    public class SaveHistoryScreen : BaseGameState
    {
       #region Field region

        PictureBox backgroundImage;
        PictureBox arrowImage;
        LinkLabel[] Sessions;
        LinkLabel Continue;
        LinkLabel Non;
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

            arrowImage = new PictureBox(
                arrowTexture,
                new Rectangle(
                    0,
                    0,
                    arrowTexture.Width,
                    arrowTexture.Height));
            ControlManager.Add(arrowImage);

            List<Userdata> ses = dbs.getSessionNum();
            if (ses.Count > 0)
            {

                for (int i = 0; i < ses.Count; i++)
                {
                    Sessions[i] = new LinkLabel();
                    Sessions[i].Text = ses[i].Session_id + "  " + ses[i].Name + "  " + ses[i].HighScore;
                    Sessions[i].Size = Sessions[i].SpriteFont.MeasureString(Sessions[i].Text);
                    Sessions[i].Selected += new EventHandler(menuItem_Selected);

                    ControlManager.Add(Sessions[i]);
                }
            }
            else
            {
                Non = new LinkLabel();
                Non.Text = " There is currently no ";
                Non.Size = Non.SpriteFont.MeasureString(Non.Text);
                Non.Selected += new EventHandler(menuItem_Selected);
            }


            Continue = new LinkLabel();
            Continue.Text = "Continue on";
            Continue.Size = Continue.SpriteFont.MeasureString(Continue.Text);
            Continue.Selected += new EventHandler(menuItem_Selected);

            ControlManager.Add(Continue);

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

            ControlManager_FocusChanged(Sessions, null);
        }

        void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Vector2 position = new Vector2(control.Position.X + maxItemWidth + 10f, control.Position.Y);
            arrowImage.SetPosition(position);
        }

        private void menuItem_Selected(object sender, EventArgs e)
        {
            if (sender == Sessions)
            {
                
                
            }

            if (sender == Continue)
            {
                // change this to start the session
                
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
