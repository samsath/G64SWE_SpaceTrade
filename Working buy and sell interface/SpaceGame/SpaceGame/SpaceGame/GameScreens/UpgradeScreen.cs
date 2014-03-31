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
        //Turns should be added here

        // Ship's current Money (at the end of the buy phase, this value should return to the gameplay by using the acceptLabel_Selected method)
        int totalMoney; // Ship's current money

        //money remaining should be equal to the totalmoney amount
        int moneyRemaining;

        int turnPrice = 5000;
        int turnAmount;
        int finalAmount = 0;//output
        int upgradeAmount = 1;//output

        int cargoLevel; // Ship's current cargo level //input
        int cargoAmount; // Ship's current cargo capacity //input
        int cargoPrice = 10000;

        string shipType;

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
            linkLabelHandler = new EventHandler(addTurns);
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
            /*cargoLevel = GameRef.spaceShip.getCargoLevel();
            totalMoney = GameRef.spaceShip.getMoney();
            cargoAmount = GameRef.spaceShip.getCargoCapacity();
            turnAmount = GameRef.spaceShip.getNumberOfTurn();
            shipType = GameRef.spaceShip.gettextureName();*/
            ContentManager content = GameRef.Content;

            CreateControls(content);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            cargoLevel = GameRef.spaceShip.getCargoLevel();
            totalMoney = GameRef.spaceShip.getMoney();
            cargoAmount = GameRef.spaceShip.getCargoCapacity();
            turnAmount = GameRef.spaceShip.getNumberOfTurn();
            shipType = GameRef.spaceShip.gettextureName();

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


            moneyRemaining = totalMoney;

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
            quantityNumber.Text = turnAmount.ToString();
            quantityNumber.Position = new Vector2(nextControlPosition.X + 150, nextControlPosition.Y);

            priceNumber = new Label();
            priceNumber.Text = turnPrice.ToString() + "$";
            priceNumber.Position = new Vector2(nextControlPosition.X + 200, nextControlPosition.Y);

            LinkLabel addLabel = new LinkLabel();
            addLabel.TabStop = true;
            addLabel.Text = "+";
            addLabel.Position = new Vector2(nextControlPosition.X + 300, nextControlPosition.Y);

            //addLabel.Selected += new EventHandler(augmentItem);
            addLabel.Selected += addTurns;

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
            upgradeLevelLabel.Text = "Upgrade "+(cargoLevel+1)+ " = " +(cargoAmount*2)+" spaces";
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
            
            /*
             * update the states to the game.
             */
            //StateManager.ChangeState(GameRef.GamePlayScreen, null);

            string newShip = shipType.Substring(0, shipType.Length - 1);
            shipType = newShip += cargoLevel.ToString();


            Texture2D shipType1;
            shipType1 = Game.Content.Load<Texture2D>(@"ShipSprites\" + shipType);

            totalMoney = moneyRemaining;

            GameRef.spaceShip.setMoney(totalMoney);
            GameRef.spaceShip.setNumberOfTurn(turnAmount);
            GameRef.spaceShip.setCargoCapacity(cargoAmount);
            GameRef.spaceShip.settextureName(shipType);
            GameRef.spaceShip.setShip(shipType);
            GameRef.spaceShip.setShipTexture(shipType1);
            //acceptLabel.Text = "Changes Accepted";
            acceptLabel.Text = shipType;
        }

        void addTurns(object sender, EventArgs e)
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
                turnAmount++;
                quantityNumber.Text = turnAmount.ToString();

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

            if (turnAmount > 0 )
            {

                string resourceName = ((LinkLabel)sender).Type;
                undoResources.Push(resourceName);
                moneyRemaining = moneyRemaining + turnPrice;
                remainingMoney.Text = "Total Amount of Money: " + moneyRemaining.ToString() + "$";
                //quantity
                turnAmount--;
                quantityNumber.Text = turnAmount.ToString();
                
                finalAmount = finalAmount - turnPrice;
                finalPrice.Text = finalAmount.ToString() + "$";


            }           

            if (turnAmount == 0)
            {
                finalAmount = 0;
                //return;
            }
        }

        void upgradeCargo(object sender, EventArgs e)
        {
            if (moneyRemaining <= 0)
            {
                remainingMoney.Text = "You Are Out Of Money";

            }
            else 
            {
                cargoLevel++;
                cargoAmount = cargoAmount * 2;
                cargoPrice = cargoPrice * 2;
                upgradeLevelLabel.Text = "Upgrade " + (cargoLevel+1) + " = " + cargoAmount*2 + " spaces";
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

        #region Method Region

        #endregion
    }

}