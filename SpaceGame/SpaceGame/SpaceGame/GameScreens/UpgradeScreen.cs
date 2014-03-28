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
    internal class ResourceLabelSetUpgrade
    {
        internal Label Label;
        internal LinkLabel LinkLabel;

        internal ResourceLabelSetUpgrade(Label labelUpgrade, LinkLabel linkLabelUpgrade)
        {
            Label = labelUpgrade;
            LinkLabel = linkLabelUpgrade;
        }
    }

    public class UpgradeScreen : BaseGameState
    {
        #region Field Region

        int totalMoney = 100000;
        int moneyRemaining = 100000;
        // int moneyAmount = 0;
        int turnPrice = 5000;
        int quantityAmount = 0;
        int finalAmount = 0;
        //int fuelAmount = 0;
        int upgradeAmount = 0;
        int cargoLevel = 1;
        int cargoAmount = 5;
        int cargoPrice = 2000;

        PictureBox backgroundImage;
        Label remainingMoney;
        Label fuelLabel;
        //Label quantityLabel;
        Label priceLabel;
        Label priceNumber;
        Label upgradeCargoLabel;
        Label finalPrice;
        Label quantityNumber;
        Label upgradeLevelLabel;
        Label upgradeValueLabel;
        LinkLabel addLevelLabel = new LinkLabel();
        LinkLabel acceptLabel = new LinkLabel();
        LinkLabel removeLevelLabel = new LinkLabel();

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

        public UpgradeScreen(Game game, GameStateManager stateManager)
            : base(game, stateManager)
        {
            linkLabelHandler = new EventHandler(addFuel);
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
            Title.Text = "Upgrade Interface";
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

            fuelLabel = new Label();
            fuelLabel.Text = "Buy Fuel";
            fuelLabel.Position = new Vector2(nextControlPosition.X, nextControlPosition.Y - 50); ;


            ControlManager.Add(fuelLabel);

            /*quantityLabel = new Label();
            quantityLabel.Text = "Number";
            quantityLabel.Position = new Vector2(nextControlPosition.X + 100, nextControlPosition.Y);


            ControlManager.Add(quantityLabel);*/

            priceLabel = new Label();
            priceLabel.Text = "Price";
            priceLabel.Position = new Vector2(nextControlPosition.X + 200, nextControlPosition.Y);

            ControlManager.Add(priceLabel);

            upgradeCargoLabel = new Label();
            upgradeCargoLabel.Text = "Upgrade Capacity";
            upgradeCargoLabel.Position = new Vector2(nextControlPosition.X, nextControlPosition.Y + 100);

            ControlManager.Add(upgradeCargoLabel);

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;
            //

            Label nameOfItem = new Label();
            nameOfItem.Text = "Turns";
            nameOfItem.Position = nextControlPosition;

            quantityNumber = new Label();
            quantityNumber.Text = quantityAmount.ToString();
            quantityNumber.Position = new Vector2(nextControlPosition.X + 150, nextControlPosition.Y);

            priceNumber = new Label();
            priceNumber.Text = turnPrice.ToString() + "$";
            priceNumber.Position = new Vector2(nextControlPosition.X + 200, nextControlPosition.Y);

            LinkLabel addLabel = new LinkLabel();
            addLabel.TabStop = true;
            addLabel.Text = "+";
            addLabel.Position = new Vector2(nextControlPosition.X + 300, nextControlPosition.Y);

            //addLabel.Selected += new EventHandler(augmentItem);
            addLabel.Selected += addFuel;

            LinkLabel substractLabel = new LinkLabel();
            substractLabel.TabStop = true;
            substractLabel.Text = "-";
            substractLabel.Position = new Vector2(nextControlPosition.X + 350, nextControlPosition.Y);

            //substractLabel.Selected += new EventHandler(decreaseItem);
            substractLabel.Selected += new EventHandler(removeSelected);

            //addLabel.Selected += new EventHandler(decreaseItem);

            //final price
            finalPrice = new Label();
            finalPrice.Text = finalAmount.ToString();
            finalPrice.Position = new Vector2(nextControlPosition.X + 400, nextControlPosition.Y);

            //

            ControlManager.Add(nameOfItem);
            ControlManager.Add(addLabel);
            ControlManager.Add(substractLabel);
            ControlManager.Add(quantityNumber);
            ControlManager.Add(priceNumber);
            ControlManager.Add(finalPrice);
            //

            //Upgrade Cargo

            upgradeLevelLabel = new Label();
            upgradeLevelLabel.Text = "Upgrade "+cargoLevel+ " = " +cargoAmount+" spaces";
            upgradeLevelLabel.Position = new Vector2(nextControlPosition.X, nextControlPosition.Y + 100);


            ControlManager.Add(upgradeLevelLabel);
            //nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;

            addLevelLabel = new LinkLabel();
            addLevelLabel.Text = "Buy";
            addLevelLabel.Position = new Vector2(nextControlPosition.X + 400, nextControlPosition.Y + 100);
            addLevelLabel.Selected += upgradeCargo;


            ControlManager.Add(addLevelLabel);

            removeLevelLabel = new LinkLabel();
            removeLevelLabel.Text = "Remove";
            removeLevelLabel.Position = new Vector2(nextControlPosition.X + 470, nextControlPosition.Y + 100);
            removeLevelLabel.Selected += decreaseCargo;

            ControlManager.Add(removeLevelLabel);

            upgradeValueLabel = new Label();
            upgradeValueLabel.Text = cargoPrice.ToString();
            upgradeValueLabel.Position = new Vector2(nextControlPosition.X + 600, nextControlPosition.Y + 100);
            ControlManager.Add(upgradeValueLabel);

            //           

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 200f;
            //Accept Label
            acceptLabel = new LinkLabel();
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
            //undoResources.Clear();
            /*
             * update the states to the game.
             */
            //StateManager.ChangeState(GameRef.GamePlayScreen, null);
            acceptLabel.Text = "Changes Accepted";
        }

        void addFuel(object sender, EventArgs e)
        {


            if ((moneyRemaining - turnPrice) < 0)
            {
                remainingMoney.Text = "You Are Out Of Money";

            }
            else if (moneyRemaining >= 0)
            {

                string resourceName = ((LinkLabel)sender).Type;
                undoResources.Push(resourceName);
                moneyRemaining = moneyRemaining - turnPrice;

                // Update the skill points for the appropriate resource
                remainingMoney.Text = "Total Amount of Money: " + moneyRemaining.ToString() + "$";

                //quantity reduced
                quantityAmount++;
                quantityNumber.Text = quantityAmount.ToString();

                finalAmount = finalAmount + turnPrice;
                finalPrice.Text = finalAmount.ToString() + "$";

            }
        }

        void removeSelected(object sender, EventArgs e)
        {


            //if (moneyRemaining <= 0 || quantityAmount <= 0 || finalAmount == 0)
            /*if (quantityAmount < 0 || finalAmount == 0)
            {
                return;
            }*/
            if ((moneyRemaining - turnPrice) < 0)
            {
                remainingMoney.Text = "You Are Out Of Money";

            }

            if (quantityAmount > 0 )
            {

                string resourceName = ((LinkLabel)sender).Type;
                undoResources.Push(resourceName);
                moneyRemaining = moneyRemaining + turnPrice;
                remainingMoney.Text = "Total Amount of Money: " + moneyRemaining.ToString() + "$";
                //quantity
                quantityAmount--;
                quantityNumber.Text = quantityAmount.ToString();
                
                finalAmount = finalAmount - turnPrice;
                finalPrice.Text = finalAmount.ToString() + "$";


            }           

            if (quantityAmount == 0)
            {
                finalAmount = 0;
                //return;
            }
        }

        void upgradeCargo(object sender, EventArgs e)
        {
            if ((moneyRemaining - turnPrice) <= 0)
            {
                remainingMoney.Text = "You Are Out Of Money";

            }
            else
            {
                cargoLevel++;
                cargoAmount = cargoAmount * 2;
                cargoPrice = cargoPrice * 2;
                upgradeLevelLabel.Text = "Upgrade " + cargoLevel + " = " + cargoAmount + " spaces";
                moneyRemaining = moneyRemaining - cargoPrice;
                remainingMoney.Text = "Total Amount of Money: " + moneyRemaining.ToString() + "$";
                upgradeValueLabel.Text = cargoPrice.ToString() + "$";
            }
        }

        void decreaseCargo(object sender, EventArgs e)
        {
            if (cargoLevel > 1)
            {
                cargoLevel--;
                cargoAmount = cargoAmount / 2;
                cargoPrice = cargoPrice / 2;
                upgradeLevelLabel.Text = "Upgrade " + cargoLevel + " = " + cargoAmount + " spaces";
                moneyRemaining = moneyRemaining + cargoPrice;
                remainingMoney.Text = "Total Amount of Money: " + moneyRemaining.ToString() + "$";
                upgradeValueLabel.Text = cargoPrice.ToString() + "$";
            }
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