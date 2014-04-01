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



        string[] hsId = new string[5] { "no avialble", "no avialble", "no avialble", "no avialble", "no avialble" };
        string[] hsName = new string[5] { "no avialble", "no avialble", "no avialble", "no avialble", "no avialble" };
        string[] hsScore = new string[5] { "no avialble", "no avialble", "no avialble", "no avialble", "no avialble" };
        
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
            getSocre();
        }

        public void CreateControls(ContentManager Content)
        {

            backgroundImage = new PictureBox(
            Game.Content.Load<Texture2D>(@"Backgrounds\DeepSpace"),
            GameRef.ScreenRectangle);
            ControlManager.Add(backgroundImage);



            Label Title = new Label();
            Title.Text = "High score table";
            Title.Position = new Vector2(350, 50);
            ControlManager.Add(Title);


            Label hi0 = new Label();
            hi0.Text = hsId[0];
            hi0.Position = new Vector2(200, 100);
            ControlManager.Add(hi0);

            Label hi00 = new Label();
            hi00.Text = hsName[0];
            hi00.Position = new Vector2(400, 100);
            ControlManager.Add(hi00);

            Label hi000 = new Label();
            hi000.Text = hsScore[0];
            hi000.Position = new Vector2(700, 100);
            ControlManager.Add(hi000);

            Label hi1 = new Label();
            hi1.Text = hsId[1];
            hi1.Position = new Vector2(200, 200);
            ControlManager.Add(hi1);

            Label hi11 = new Label();
            hi11.Text = hsName[1];
            hi11.Position = new Vector2(400, 200);
            ControlManager.Add(hi11);

            Label hi111 = new Label();
            hi111.Text = hsScore[1];
            hi111.Position = new Vector2(700,200);
            ControlManager.Add(hi111);


            Label hi2 = new Label();
            hi2.Text = hsId[2];
            hi2.Position = new Vector2(200, 300);
            ControlManager.Add(hi2);

            Label hi22 = new Label();
            hi22.Text = hsName[2];
            hi22.Position = new Vector2(400, 300);
            ControlManager.Add(hi22);

            Label hi222 = new Label();
            hi222.Text = hsScore[2];
            hi222.Position = new Vector2(700, 300);
            ControlManager.Add(hi222);

            Label hi3 = new Label();
            hi3.Text = hsId[3];
            hi3.Position = new Vector2(200, 400);
            ControlManager.Add(hi3);

            Label hi33 = new Label();
            hi33.Text = hsName[3];
            hi33.Position = new Vector2(400, 400);
            ControlManager.Add(hi33);

            Label hi333 = new Label();
            hi333.Text = hsScore[3];
            hi333.Position = new Vector2(700, 400);
            ControlManager.Add(hi333);

            Label hi4 = new Label();
            hi4.Text = hsId[4];
            hi4.Position = new Vector2(200, 500);
            ControlManager.Add(hi4);

            Label hi44 = new Label();
            hi44.Text = hsName[4];
            hi44.Position = new Vector2(400, 500);
            ControlManager.Add(hi44);

            Label hi444 = new Label();
            hi444.Text = hsScore[4];
            hi444.Position = new Vector2(700, 500);
            ControlManager.Add(hi444);



            






            //Back Button
            LinkLabel backLabel = new LinkLabel();
            backLabel.Text = "Go Back";
            backLabel.Position = new Vector2(400, 600);
            backLabel.Selected += new EventHandler(goBack);

            ControlManager.Add(backLabel);
            ControlManager.NextControl();
/*
            LinkLabel update = new LinkLabel();
            update.Text = "update";
            update.Position = new Vector2(600, 600);
            update.Selected += new EventHandler(getSocre);
            ControlManager.Add(update);
            ControlManager.NextControl();
            */
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
        public void getSocre()
        {

            //dbs.Close();

            List<Userdata> resultUser = dbs.getHightScore();

            for (int i = 0; i < resultUser.Count; i++)
            {
                int score = resultUser[i].HighScore;
                if (score == 0)
                {
                    
                   /* string TheScore = "not avialble yet ";
                    hsId[i] = resultUser[i].User_id.ToString();
                    hsName[i] = resultUser[i].Name;
                    hsScore[i] = TheScore;*/
                   
                }
                else
                { hsId[i] = resultUser[i].User_id.ToString();
                hsName[i] = resultUser[i].Name.ToString();
                hsScore[i] = resultUser[i].HighScore.ToString(); 
                }
                
        
            }
        }
        #endregion
    }

}
