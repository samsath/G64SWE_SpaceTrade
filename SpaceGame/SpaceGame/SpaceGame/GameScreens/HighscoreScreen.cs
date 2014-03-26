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
using STDatabase;

namespace SpaceGame.GameScreens
{
    
    internal class ResourceLabelSetHighScore
    {
        internal Label Label;
        internal LinkLabel LinkLabel;

        internal ResourceLabelSetHighScore(Label labelScore, LinkLabel linkLabelScore)
        {
            Label = labelScore;
            LinkLabel = linkLabelScore;
        }
    }

    public class HighscoreScreen : BaseGameState
    {
        public IDatabase dbs = new Database();
        #region Field Region

        SpriteFont font1;
        Vector2 fontPosition;

        PictureBox backgroundImage;
        Label remainingMoney;
        Label NameLabel;
        Label PlanetResourceLabel;
        Label PlanetResourceText;

        //List<ResourceLabelSet> resourceLabel = new List<ResourceLabelSet>();
        List<ResourceLabelSet> resourceLabel1 = new List<ResourceLabelSet>();
        Stack<string> undoResources = new Stack<string>();
        EventHandler linkLabelHandler;

        #endregion

        #region Property Region

       

        #endregion

        #region Constructor Region

        public HighscoreScreen(Game game, GameStateManager stateManager)
            : base(game, stateManager)
        {
            linkLabelHandler = new EventHandler(addSelectedResource);
        }

        #endregion

        #region Method Region
        #endregion

        #region Virtual Method region
        #endregion

        #region XNA Method Region

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            ContentManager content = GameRef.Content;
            
            CreateControls(content);
        }

        public void CreateControls(ContentManager Content)
        {

            backgroundImage = new PictureBox(
            Game.Content.Load<Texture2D>(@"Backgrounds\DeepSpace"),
            GameRef.ScreenRectangle);
            font1 = Content.Load<SpriteFont>(@"Fonts\CourierNew");
            ControlManager.Add(backgroundImage);

            //List<ResourceData> resourceData = new List<ResourceData>();

            Label Title = new Label();
            Title.Text = "High Score of the Top 10";
            Title.Position = new Vector2(100, 50);
            ControlManager.Add(Title);
            Vector2 nextControlPosition = new Vector2(100, 50);
            LinkLabel[] top10 = new LinkLabel[10];

            // This will go through the list of user and get the highscore for each one.
               /*
            List<Userdata> resultUser = dbs.getHighScore();
            Vector2 FontOrigin = 
            
            // this goes through the list of users and grabs the top 10 peoples highscore
            int length = 1;
            for (int i = 0; i < resultUser.Count && i <= 10; i++)
            {
                
                fontPosition = new Vector2(230, 150 + length);
                //spriteBatch.DrawString(font1, theScores, fontPosition, Color.Red, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

                top10[i].Text =  "" + resultUser[i].User_id + "       " + resultUser[i].Name + "       " + resultUser[i].HighScore;
                top10[i].Position = fontPosition;


            }
            */

            
            

           

            //Back Button
            LinkLabel backLabel = new LinkLabel();
            backLabel.Text = "Go Back";
            backLabel.Position = nextControlPosition;
            backLabel.Selected += new EventHandler(goBack);

            ControlManager.Add(backLabel);
            ControlManager.NextControl();

        }


        void addSelectedResource(object sender, EventArgs e)
        {

           
        }

        

        void goBack(object sender, EventArgs e)
        {
            StateManager.ChangeState(GameRef.GamePlayScreen, "playingScreen");
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

        #endregion
    }

}
