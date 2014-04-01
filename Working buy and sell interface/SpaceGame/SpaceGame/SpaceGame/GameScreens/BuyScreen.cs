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
using SpaceGame.Components;

using System.Diagnostics;

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

        // Ship's current Money (at the end of the buy phase, this value should return to the gameplay by using the acceptLabel_Selected method)
        int totalMoney;//input

        int moneyRemaining; //Money changes due to apply, moneyremaining should be equal to the totalmoney amount

        int quantityAmount = 0;// Number of resources of that type// input
        //int finalAmount = 0; // cost of the transactions at the end of the buy state. (Should be returned to the gameplay)

        int cargoAmount; // Ship's current cargo capacity //input

        int turnAmount; // Ship's current number of turns (not needed here, should be on the upgrade screen)

        PictureBox backgroundImage;
        Label remainingMoney;
        Label NameLabel;
        Label quantityLabel;
        Label priceLabel;
        Label priceNumber;
        Label finalPriceLabel;
        Label finalPrice;
        Label ShipResourceLabel;
        Label ShipResourceText;
        Label nameOfItem;
        Label[] quantityNumber;
        LinkLabel acceptLabel = new LinkLabel();
        int[] resourceAmountOnPlanet;
        int[] baseResourceAmountOnPlanet;

        //List<ResourceLabelSet> resourceLabel = new List<ResourceLabelSet>();
        List<ResourceLabelSet> resourceLabel1 = new List<ResourceLabelSet>();
        Stack<string> undoResources = new Stack<string>();
        EventHandler linkLabelHandler;

        List<Resource> planetResource;
        List<Resource> shipResource;

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
            totalMoney = GameRef.spaceShip.getMoney();
            moneyRemaining = totalMoney;
            cargoAmount = GameRef.spaceShip.getCargoCapacity();
            turnAmount = GameRef.spaceShip.getNumberOfTurn();
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

            //Planet Resource
            planetResource = new List<Resource>();
            planetResource = GameRef.board.getResourceList();

            quantityNumber = new Label[planetResource.Count];
            resourceAmountOnPlanet = new int[planetResource.Count];
            baseResourceAmountOnPlanet = new int[planetResource.Count];
            for (int i = 0; i < planetResource.Count; i++)
            {
                nameOfItem = new Label();
                nameOfItem.Text = planetResource[i].getName();
                nameOfItem.Position = new Vector2(nextControlPosition.X, nextControlPosition.Y+i*50);

                quantityNumber[i] = new Label();
                quantityNumber[i].Text = planetResource[i].getAmount().ToString();
                quantityNumber[i].Position = new Vector2(nextControlPosition.X + 200, nextControlPosition.Y+i*50);
                resourceAmountOnPlanet[i] = planetResource[i].getAmount();
                baseResourceAmountOnPlanet[i] = planetResource[i].getAmount();

                priceNumber = new Label();
                priceNumber.Text = "$" + planetResource[i].getPrice().ToString();
                priceNumber.Position = new Vector2(nextControlPosition.X + 250, nextControlPosition.Y+i*50);


                LinkLabel addLabel = new LinkLabel();
                addLabel.TabStop = true;
                addLabel.Value = i;
                addLabel.Text = "+";
                addLabel.Position = new Vector2(nextControlPosition.X + 350, nextControlPosition.Y+i*50);

                //addLabel.Selected += new EventHandler(augmentItem);
                addLabel.Selected += addSelectedResource;

                LinkLabel substractLabel = new LinkLabel();
                substractLabel.TabStop = true;
                substractLabel.Value = i;
                substractLabel.Text = "-";
                substractLabel.Position = new Vector2(nextControlPosition.X + 400, nextControlPosition.Y+i*50);

                //substractLabel.Selected += new EventHandler(decreaseItem);
                substractLabel.Selected += new EventHandler(substractSelectedResource);

                //addLabel.Selected += new EventHandler(decreaseItem);
                ControlManager.Add(nameOfItem);
                ControlManager.Add(addLabel);
                ControlManager.Add(substractLabel);
                ControlManager.Add(quantityNumber[i]);
                ControlManager.Add(priceNumber);
            }
            //final price
            finalPrice = new Label();
            finalPrice.Text = totalMoney.ToString();
            finalPrice.Position = new Vector2(nextControlPosition.X + 650, nextControlPosition.Y);

            //


            ControlManager.Add(finalPrice);
            //

            //Ship resources
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;

            ShipResourceLabel = new Label();
            ShipResourceLabel.Text = "Ship Resources";
            ShipResourceLabel.Position = new Vector2(nextControlPosition.X + 550, nextControlPosition.Y + 50);


            ControlManager.Add(ShipResourceLabel);
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;


            shipResource = new List<Resource>();
            shipResource = GameRef.spaceShip.getResource();

            for (int i = 0; i < shipResource.Count; i++)
            {
                ShipResourceText = new Label();
                string DisplayedText = shipResource[i].name + " " + shipResource[i].amount + " $" + shipResource[i].getPrice() + " each!";
                ShipResourceText.Text = DisplayedText;
                Debug.WriteLine(DisplayedText);
                ShipResourceText.Position = new Vector2(nextControlPosition.X + 550, nextControlPosition.Y + 50 + 50 * i);
                ControlManager.Add(ShipResourceText);
            }

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

            acceptLabel.Text = "Changes Accepted";
        }

        // Planet resource function
        void addSelectedResource(object sender, EventArgs e)
        {
            Resource singlePlanetResource = (Resource)((LinkLabel)sender).Value;
            //int tempCost = (int)((LinkLabel)sender).Value;
            if (singlePlanetResource.getAmount() > 0)
            {
                if ((moneyRemaining - singlePlanetResource.getPrice()) < 0)
                {
                    remainingMoney.Text = "You Are Out Of Money";

                }
                else if (moneyRemaining >= 0)
                {
                    // Change the resources on planet
                    string resourceName = ((LinkLabel)sender).Type;
                    //undoResources.Push(resourceName);
                    moneyRemaining = moneyRemaining - singlePlanetResource.getPrice();

                    // Update the skill points for the appropriate resource
                    remainingMoney.Text = "Total Amount of Money: " + moneyRemaining.ToString() + "$";

                    //quantity reduced
                    singlePlanetResource.setAmount(singlePlanetResource.getAmount() - 1);
                    resourceAmountOnPlanet[singlePlanetResource.Item2] = resourceAmountOnPlanet[singlePlanetResource.Item2] - 1;
                    quantityNumber[singlePlanetResource.Item2].Text = resourceAmountOnPlanet[singlePlanetResource.Item2].ToString();

                    totalMoney = totalMoney + singlePlanetResource.Item1.getPrice();
                    finalPrice.Text = totalMoney.ToString() + "$";

                    /*
                    // Change the resources on ship
                    bool newItem = true;
                    for (int i = 0; i<shipResource.Count; i++){
                        if (resource.Item1.getResourceID()==shipResource[i].getResourceID()){
                            newItem = false;
                            break;
                        }
                    }
                    if (newItem)
                    {
                        shipResource.Add(new Resource(resource.Item1));
                    }
                     */
                }
            }
        }

        // Planet resource function
        void substractSelectedResource(object sender, EventArgs e)
        {
            //int tempCost = (int)((LinkLabel)sender).Value;
            //if (moneyRemaining <= 0 || quantityAmount <= 0 || finalAmount == 0)
            /*if (quantityAmount < 0 || finalAmount == 0)
            {
                return;
            }*/
            Tuple<Resource, int> resource = (Tuple<Resource, int>)((LinkLabel)sender).Value;
            if (resourceAmountOnPlanet[resource.Item2] < baseResourceAmountOnPlanet[resource.Item2])
            {

                string resourceName = ((LinkLabel)sender).Type;
                //undoResources.Push(resourceName);
                moneyRemaining = moneyRemaining + resource.Item1.getPrice();
                remainingMoney.Text = "Total Amount of Money: " + moneyRemaining.ToString() + "$";
                //quantity
                resourceAmountOnPlanet[resource.Item2] = resourceAmountOnPlanet[resource.Item2] + 1;
                quantityNumber[resource.Item2].Text = resourceAmountOnPlanet[resource.Item2].ToString();

                totalMoney = totalMoney - resource.Item1.getPrice();
                finalPrice.Text = totalMoney.ToString() + "$";

            }
        }

        void goBack(object sender, EventArgs e)
        {
            GameRef.spaceShip.setGameState("playing");
            StateManager.ChangeState(GameRef.GamePlayScreen);
        }

        /*
        void decreaseItem(object sender, EventArgs e)
        {
            if (quantityAmount > 0)
            {
                moneyRemaining = moneyRemaining - priceAmount;
                finalPrice.Text = moneyRemaining.ToString() + "$";
            }

            if (quantityAmount == 0)
            {
                totalMoney = 0;
                //return;
            }


        }
         */ 

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