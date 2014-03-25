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

namespace SpaceGame.GameScreens
{
    internal class ResourceLabelSetBuy
    {
        internal Label Label;
        internal LinkLabel LinkLabel;

        internal ResourceLabelSetBuy(Label labelBuy, LinkLabel linkLabelBuy)
        {
            Label = labelBuy;
            LinkLabel = linkLabelBuy;
        }
    }

    public class BuyScreen : BaseGameState
    {
     #region Field Region

        int totalMoney = 10000;
        int moneyRemaining = 10000;
       // int moneyAmount = 0;
        int priceAmount = 3000;
        int quantityAmount = 0;
        int finalAmount = 0;
        int fuelAmount = 0;
        int cargoAmount = 0;

        PictureBox backgroundImage;
        Label remainingMoney;
        Label NameLabel;
        Label quantityLabel;
        Label priceLabel;
        Label priceNumber;
        Label finalPriceLabel;
        Label finalPrice;
        Label quantityNumber;
        Label PlanetResourceLabel;
        Label PlanetResourceText;

        //List<ResourceLabelSet> resourceLabel = new List<ResourceLabelSet>();
        List<ResourceLabelSet> resourceLabel1 = new List<ResourceLabelSet>();
        Stack<string> undoResources = new Stack<string>();
        EventHandler linkLabelHandler;

        #endregion

        #region Property Region

        public int TotalResources
        {
            get { return totalMoney; }
            set
            {
                totalMoney = value;
                moneyRemaining = value;
            }
        }

        #endregion

        #region Constructor Region

        public BuyScreen(Game game, GameStateManager stateManager)
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
            ControlManager.Add(backgroundImage);

            //List<ResourceData> resourceData = new List<ResourceData>();

            Label Title = new Label();
            Title.Text = "Buying Interface";
            Title.Position = new Vector2(100, 50);
            ControlManager.Add(Title);
            Vector2 nextControlPosition = new Vector2(100, 50);
            

            

            remainingMoney = new Label();
            remainingMoney.Text = "Total Amount of Money: " + moneyRemaining.ToString() + "$";
            remainingMoney.Position = new Vector2(nextControlPosition.X + 400, nextControlPosition.Y); 

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;

            ControlManager.Add(remainingMoney);

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;
                        
            // Labels

            NameLabel = new Label();
            NameLabel.Text = "Name";
            NameLabel.Position = nextControlPosition;


            ControlManager.Add(NameLabel);

            quantityLabel = new Label();
            quantityLabel.Text = "Quantity";
            quantityLabel.Position = new Vector2(nextControlPosition.X + 100, nextControlPosition.Y);


            ControlManager.Add(quantityLabel);

            priceLabel = new Label();
            priceLabel.Text = "Price";
            priceLabel.Position = new Vector2(nextControlPosition.X + 250, nextControlPosition.Y);

            ControlManager.Add(priceLabel);

            finalPriceLabel = new Label();
            finalPriceLabel.Text = "Final Price";
            finalPriceLabel.Position = new Vector2(nextControlPosition.X + 650, nextControlPosition.Y);

            ControlManager.Add(finalPriceLabel);

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;
            //

            Label nameOfItem = new Label();
            nameOfItem.Text = "Iron";
            nameOfItem.Position = nextControlPosition;

            quantityNumber = new Label();
            quantityNumber.Text = quantityAmount.ToString();
            quantityNumber.Position = new Vector2(nextControlPosition.X + 200, nextControlPosition.Y);

            priceNumber = new Label();
            priceNumber.Text = priceAmount.ToString() + "$";
            priceNumber.Position = new Vector2(nextControlPosition.X + 250, nextControlPosition.Y);

            LinkLabel addLabel = new LinkLabel();
            addLabel.TabStop = true;
            addLabel.Text = "+";
            addLabel.Position = new Vector2(nextControlPosition.X + 350, nextControlPosition.Y);

            addLabel.Selected += new EventHandler(augmentItem);
            addLabel.Selected += addSelectedResource;

            LinkLabel substractLabel = new LinkLabel();
            substractLabel.TabStop = true;
            substractLabel.Text = "-";
            substractLabel.Position = new Vector2(nextControlPosition.X + 400, nextControlPosition.Y);

            substractLabel.Selected += new EventHandler(decreaseItem);
            substractLabel.Selected += new EventHandler(substractSelectedResource);            
            
            //addLabel.Selected += new EventHandler(decreaseItem);

            //final price
            finalPrice = new Label();
            finalPrice.Text = finalAmount.ToString();
            finalPrice.Position = new Vector2(nextControlPosition.X + 650, nextControlPosition.Y);

            //

            ControlManager.Add(nameOfItem);
            ControlManager.Add(addLabel);
            ControlManager.Add(substractLabel);
            ControlManager.Add(quantityNumber);
            ControlManager.Add(priceNumber);
            ControlManager.Add(finalPrice);
            //

            //Planet resources

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;

            PlanetResourceLabel = new Label();
            PlanetResourceLabel.Text = "Planet Resources";
            PlanetResourceLabel.Position = new Vector2(nextControlPosition.X + 550, nextControlPosition.Y +50);


            ControlManager.Add(PlanetResourceLabel);
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;

            PlanetResourceText = new Label();
            PlanetResourceText.Text = "List of Resources";
            PlanetResourceText.Position = new Vector2(nextControlPosition.X + 550, nextControlPosition.Y+ 50);


            ControlManager.Add(PlanetResourceText);

            //

            //Undo Label

            /*nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;

            LinkLabel undoLabel = new LinkLabel();
            undoLabel.Text = "Reset Values";
            undoLabel.Position = nextControlPosition;
            undoLabel.TabStop = true;
            undoLabel.Selected += new EventHandler(undoLabel_Selected);
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;

            ControlManager.Add(undoLabel);*/

            //Accept Label
            LinkLabel acceptLabel = new LinkLabel();
            acceptLabel.Text = "Accept Changes";
            acceptLabel.Position = nextControlPosition;
            acceptLabel.TabStop = true;
            acceptLabel.Selected += new EventHandler(acceptLabel_Selected);
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;

            ControlManager.Add(acceptLabel);
            ControlManager.NextControl();

            //Back Button
            LinkLabel backLabel = new LinkLabel();
            backLabel.Text = "Go Back";
            backLabel.Position = nextControlPosition;
            backLabel.Selected += new EventHandler(goBack);

            ControlManager.Add(backLabel);
            ControlManager.NextControl();

        }

        void acceptLabel_Selected(object sender, EventArgs e)
        {
            undoResources.Clear();
            StateManager.ChangeState(GameRef.GamePlayScreen);
        }

        /*void undoLabel_Selected(object sender, EventArgs e)
        {
            if (moneyRemaining == 0)
                return;
            
            moneyRemaining = 10;
            remainingMoney.Text = "Total Resources: " + moneyRemaining.ToString();
            totalMoney = 0;
            

        }*/

        void addSelectedResource(object sender, EventArgs e)
        {


            if ((moneyRemaining - priceAmount) <= 0)
            {
                remainingMoney.Text = "You Are Out Of Money";
                
            }
            else{

                string resourceName = ((LinkLabel)sender).Type;
                undoResources.Push(resourceName);
                moneyRemaining= moneyRemaining - priceAmount;

                // Update the skill points for the appropriate resource
                remainingMoney.Text = "Total Amount of Money: " + moneyRemaining.ToString()+"$";

                //quantity reduced
                quantityAmount++;
                quantityNumber.Text = quantityAmount.ToString();

                

            }
        }

        void substractSelectedResource(object sender, EventArgs e)
        {


            //if (moneyRemaining <= 0 || quantityAmount <= 0 || finalAmount == 0)
            /*if (quantityAmount < 0 || finalAmount == 0)
            {
                return;
            }*/
            if(quantityAmount > 0){

                string resourceName = ((LinkLabel)sender).Type;
                undoResources.Push(resourceName);
                moneyRemaining = moneyRemaining + priceAmount;
                remainingMoney.Text = "Total Amount of Money: " + moneyRemaining.ToString()+"$";
                //quantity
                quantityAmount--;
                quantityNumber.Text = quantityAmount.ToString();

            
            }
        }

        void goBack(object sender, EventArgs e)
        {
            StateManager.ChangeState(GameRef.GamePlayScreen);
        }

        
        void augmentItem(object sender, EventArgs e)
        {
            if ((moneyRemaining - priceAmount) <= 0)
            {
                return;

            }
            if (moneyRemaining > 0)
            {
                finalAmount = finalAmount + priceAmount;
                finalPrice.Text = finalAmount.ToString()+"$";
            }

        }

        void decreaseItem(object sender, EventArgs e)
        {
            if (quantityAmount>0)
            {
                finalAmount = finalAmount - priceAmount;
                finalPrice.Text = finalAmount.ToString() + "$";
            }

            if (quantityAmount == 0)
            {
                finalAmount = 0;
                //return;
            }
         

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