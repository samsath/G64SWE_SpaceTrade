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


        PictureBox backgroundImage;
        Label hi1;
        Label hi2;
        Label hi3;
        Label hi4;
        Label hi5;

        
    
        string[] h11 = new string[5] {"a","b","c","d","e"};
       
        
        List<ResourceLabelSet> resourceLabel1 = new List<ResourceLabelSet>();

        EventHandler linkLabelHandler;




        #region Constructor Region

        public HighscoreScreen(Game game, GameStateManager stateManager)
            : base(game, stateManager)
        {
            linkLabelHandler = new EventHandler(addSelectedResource);
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

            ContentManager content = GameRef.Content;

            CreateControls(content);
        }

        public void CreateControls(ContentManager Content)
        {

            backgroundImage = new PictureBox(
            Game.Content.Load<Texture2D>(@"Backgrounds\DeepSpace"),
            GameRef.ScreenRectangle);
            ControlManager.Add(backgroundImage);



            Label Title = new Label();
            Title.Text = "High score tabke";
            Title.Position = new Vector2(350, 50);
            ControlManager.Add(Title);

             

            Label hi1 = new Label();
            hi1.Text = h11[0];
            hi1.Position = new Vector2(100, 100);
            ControlManager.Add(hi1);


        


            Label hi2 = new Label();
            hi2.Text = h11[1];
            hi2.Position = new Vector2(100, 200);
            ControlManager.Add(hi2);



            Label hi3 = new Label();
            hi3.Text = h11[2];
            hi3.Position = new Vector2(100, 300);
            ControlManager.Add(hi3);
            



            Label hi4 = new Label();
            hi4.Text = h11[3] ;
            hi4.Position = new Vector2(100, 400);
            ControlManager.Add(hi4);
           



            Label hi5 = new Label();
            hi5.Text = h11[4] ;
            hi5.Position = new Vector2(100, 500);
            ControlManager.Add(hi5);
            






            //Back Button
            LinkLabel backLabel = new LinkLabel();
            backLabel.Text = "Go Back";
            backLabel.Position = new Vector2(450, 600);
            backLabel.Selected += new EventHandler(goBack);

            ControlManager.Add(backLabel);
            ControlManager.NextControl();

        }


        void addSelectedResource(object sender, EventArgs e)
        {

            string resourceName = ((LinkLabel)sender).Type;

        }


        void goBack(object sender, EventArgs e)
        {
            GameRef.spaceShip.setGameState("playing");
            StateManager.ChangeState(GameRef.GamePlayScreen);
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
        //---------------------------------------------------------------------------------
        #region
        /*     public void getSocre()
        {

            //dbs.Close();

            List<Userdata> resultUser = dbs.getHighScore();
            for (int i = 0; i < 5; i++)
            {
                int score = resultUser[i].HighScore;
                if (score == 0)
                {
                    string ff = "   " 
                    string TheScore = "not avialble yet ";
                    h11[i] = resultUser[i].User_id + ff + resultUser[i].Name + ff +TheScore; 
                }
                else
                {  h11[i] =resultUser[i].User_id + ff+  resultUser[i].Name + ff +resultUser[i].HighScore; }
                
        
            }
        }*/
        #endregion
    }

}
