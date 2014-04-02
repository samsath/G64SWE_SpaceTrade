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
    internal class ResourceLabelSetSell
    {
        internal Label Label;
        internal LinkLabel LinkLabel;

        internal ResourceLabelSetSell(Label labelBuy, LinkLabel linkLabelBuy)
        {
            Label = labelBuy;
            LinkLabel = linkLabelBuy;
        }
    }

    public class SellScreen : BaseGameState
    {
        #region Field Region

        // Ship's current Money (at the end of the buy phase, this value should return to the gameplay by using the acceptLabel_Selected method)
        int totalMoney;//input
        int totalCost = 0;
        int moneyRemaining; //Money changes due to apply, moneyremaining should be equal to the totalmoney amount

        //int finalAmount = 0; // cost of the transactions at the end of the buy state. (Should be returned to the gameplay)

        int cargoAmount; // Ship's current cargo capacity //input


        PictureBox backgroundImage;
        Label remainingMoney;
        Label NameLabel;
        Label remaining;
        Label quantity;
        Label priceLabel;
        Label priceNumber;
        Label finalPriceLabel;
        Label finalPrice;
        Label PlanetResourceLabel;
        Label[] remainingNumber;
        Label[] quantityNumber;
        Label nameOfItem;
        LinkLabel acceptLabel = new LinkLabel();
        int[] baseResourceAmountOnShip;
        SpriteFont font1;
        Vector2 fontPosition;

        //List<ResourceLabelSet> resourceLabel = new List<ResourceLabelSet>();
        List<ResourceLabelSet> resourceLabel1 = new List<ResourceLabelSet>();
        Stack<string> undoResources = new Stack<string>();
        EventHandler linkLabelHandler;

        List<Resource> backupShipResource;
        List<Resource> planetResource;
        List<Resource> tempPlanetResource;
        List<Resource> shipResource;
        List<Resource> tempShipResource;
        List<Resource> tempChange;
        int[] amountChange;
        int currentCargo = 0;

        #endregion


        #region Constructor Region

        public SellScreen(Game game, GameStateManager stateManager)
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
            font1 = content.Load<SpriteFont>(@"Fonts\CourierNew");
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
            Title.Text = "Selling Interface";
            Title.Position = new Vector2(100, 50);
            ControlManager.Add(Title);
            Vector2 nextControlPosition = new Vector2(100, 50);
            tempPlanetResource = new List<Resource>();
            tempShipResource = new List<Resource>();
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

            remaining = new Label();
            remaining.Text = "Remaining";
            remaining.Position = new Vector2(nextControlPosition.X + 100, nextControlPosition.Y);
            ControlManager.Add(remaining);

            priceLabel = new Label();
            priceLabel.Text = "Price";
            priceLabel.Position = new Vector2(nextControlPosition.X + 250, nextControlPosition.Y);
            ControlManager.Add(priceLabel);

            quantity = new Label();
            quantity.Text = "Quantity";
            quantity.Position = new Vector2(nextControlPosition.X + 450, nextControlPosition.Y);
            ControlManager.Add(quantity);


            finalPriceLabel = new Label();
            finalPriceLabel.Text = "Final Price";
            finalPriceLabel.Position = new Vector2(nextControlPosition.X + 650, nextControlPosition.Y);

            ControlManager.Add(finalPriceLabel);

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;

            //Planet Resource
            shipResource = new List<Resource>();
            shipResource = GameRef.spaceShip.getResource();
            backupShipResource = new List<Resource>();
            for (int i = 0; i < shipResource.Count; i++)
            {
                currentCargo = currentCargo + shipResource[i].getAmount();
                tempShipResource.Add(new Resource(shipResource[i].getResourceID(), shipResource[i].getName(), shipResource[i].getPrice(), shipResource[i].description, shipResource[i].getAmount()));
                backupShipResource.Add(new Resource(shipResource[i].getResourceID(), shipResource[i].getName(), shipResource[i].getPrice(), shipResource[i].description, shipResource[i].getAmount()));
            }

            remainingNumber = new Label[shipResource.Count];
            quantityNumber = new Label[shipResource.Count];
            amountChange = new int[shipResource.Count];
            baseResourceAmountOnShip = new int[shipResource.Count];
            for (int i = 0; i < shipResource.Count; i++)
            {
                nameOfItem = new Label();
                nameOfItem.Text = shipResource[i].name;
                nameOfItem.Position = new Vector2(nextControlPosition.X, nextControlPosition.Y + i * 50);

                amountChange[i] = 0;

                remainingNumber[i] = new Label();
                remainingNumber[i].Text = shipResource[i].getAmount().ToString();
                remainingNumber[i].Position = new Vector2(nextControlPosition.X + 150, nextControlPosition.Y + i * 50);
                baseResourceAmountOnShip[i] = shipResource[i].getAmount();

                quantityNumber[i] = new Label();
                quantityNumber[i].Text = "0";
                quantityNumber[i].Position = new Vector2(nextControlPosition.X + 450, nextControlPosition.Y + i * 50);

                priceNumber = new Label();
                priceNumber.Text = "$" + shipResource[i].getPrice().ToString();
                priceNumber.Position = new Vector2(nextControlPosition.X + 250, nextControlPosition.Y + i * 50);


                LinkLabel addLabel = new LinkLabel();
                addLabel.TabStop = true;
                addLabel.Value = new Tuple<Resource, int>(shipResource[i], i);
                addLabel.Text = "+";
                addLabel.Position = new Vector2(nextControlPosition.X + 350, nextControlPosition.Y + i * 50);

                //addLabel.Selected += new EventHandler(augmentItem);
                addLabel.Selected += addSelectedResource;

                LinkLabel substractLabel = new LinkLabel();
                substractLabel.TabStop = true;
                substractLabel.Value = new Tuple<Resource, int>(shipResource[i], i);
                substractLabel.Text = "-";
                substractLabel.Position = new Vector2(nextControlPosition.X + 380, nextControlPosition.Y + i * 50);

                //substractLabel.Selected += new EventHandler(decreaseItem);
                substractLabel.Selected += new EventHandler(substractSelectedResource);

                //addLabel.Selected += new EventHandler(decreaseItem);
                ControlManager.Add(nameOfItem);
                ControlManager.Add(addLabel);
                ControlManager.Add(substractLabel);
                ControlManager.Add(remainingNumber[i]);
                ControlManager.Add(quantityNumber[i]);
                ControlManager.Add(priceNumber);
            }
            //final price
            finalPrice = new Label();
            finalPrice.Text = "0";
            finalPrice.Position = new Vector2(nextControlPosition.X + 650, nextControlPosition.Y);

            //


            ControlManager.Add(finalPrice);
            //

            //Ship resources
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;

            PlanetResourceLabel = new Label();
            PlanetResourceLabel.Text = "Planet Resources";
            PlanetResourceLabel.Position = new Vector2(nextControlPosition.X + 550, nextControlPosition.Y + 50);


            ControlManager.Add(PlanetResourceLabel);
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;


            planetResource = new List<Resource>();
            planetResource = GameRef.board.getResourceList();
            for (int i = 0; i < planetResource.Count; i++)
            {

                tempShipResource.Add(new Resource(planetResource[i].getResourceID(), planetResource[i].getName(), planetResource[i].getPrice(), planetResource[i].description, planetResource[i].getAmount()));
            }

            //           

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
            GameRef.spaceShip.setResource(tempShipResource);
            GameRef.board.getPlanet().setResource(tempPlanetResource);
            backupShipResource = new List<Resource>();
            for (int i = 0; i < shipResource.Count; i++)
            {
                backupShipResource.Add(new Resource(shipResource[i].getResourceID(), shipResource[i].getName(), shipResource[i].getPrice(), shipResource[i].description, shipResource[i].getAmount()));
            }
            acceptLabel.Text = "Changes Accepted";
        }

        void addSelectedResource(object sender, EventArgs e)
        {
            tempChange = new List<Resource>();
            Tuple<Resource, int> tempResource = (Tuple<Resource, int>)((LinkLabel)sender).Value;
            //int tempCost = (int)((LinkLabel)sender).Value;
            Debug.WriteLine(tempResource.Item1.getAmount());
            if (tempResource.Item1.getAmount() > 0)
            {
                // Change the resources on planet
                string resourceName = ((LinkLabel)sender).Type;

                // reduce cargo
                currentCargo = currentCargo - 1;

                // CHange resource
                Resource res = new Resource(tempResource.Item1.getResourceID(), tempResource.Item1.getName(), tempResource.Item1.getPrice(), tempResource.Item1.description, 1);
                tempChange.Add(res);
                Utility ut = new Utility();

                Debug.WriteLine("===== Plus button");
                tempShipResource = ut.RemoveResource(tempShipResource, tempChange);
                tempPlanetResource = ut.AddResource(tempPlanetResource, tempChange);

                moneyRemaining = moneyRemaining + tempResource.Item1.getPrice();

                // Update the skill points for the appropriate resource
                remainingMoney.Text = "Total Amount of Money: " + moneyRemaining.ToString() + "$";

                //quantity reduced

                tempResource.Item1.setAmount(tempResource.Item1.getAmount() - 1);
                remainingNumber[tempResource.Item2].Text = tempResource.Item1.getAmount().ToString();

                amountChange[tempResource.Item2]++;
                quantityNumber[tempResource.Item2].Text = amountChange[tempResource.Item2].ToString();

                totalCost = totalCost + tempResource.Item1.getPrice();
                finalPrice.Text = totalCost.ToString() + "$";

            }
        }

        // Planet resource function
        void substractSelectedResource(object sender, EventArgs e)
        {
            tempChange = new List<Resource>();
            Tuple<Resource, int> tempResource = (Tuple<Resource, int>)((LinkLabel)sender).Value;
            Debug.WriteLine(tempResource.Item1.getAmount());
            if (tempResource.Item1.getAmount() < baseResourceAmountOnShip[tempResource.Item2] && currentCargo < GameRef.spaceShip.getCargoCapacity())
            {
                string resourceName = ((LinkLabel)sender).Type;

                // decrease current cargo
                currentCargo = currentCargo + 1;

                Resource res = new Resource(tempResource.Item1.getResourceID(), tempResource.Item1.getName(), tempResource.Item1.getPrice(), tempResource.Item1.description, 1);
                tempChange.Add(res);
                Utility ut = new Utility();
                List<Resource> removeList = new List<Resource>();
                Debug.WriteLine("===== Minus button");
                tempPlanetResource = ut.RemoveResource(tempPlanetResource, tempChange);
                tempShipResource = ut.AddResource(tempShipResource, tempChange);

                //undoResources.Push(resourceName);
                moneyRemaining = moneyRemaining - tempResource.Item1.getPrice();
                remainingMoney.Text = "Total Amount of Money: " + moneyRemaining.ToString() + "$";
                //quantity
                tempResource.Item1.setAmount(tempResource.Item1.getAmount() + 1);
                remainingNumber[tempResource.Item2].Text = tempResource.Item1.getAmount().ToString();

                amountChange[tempResource.Item2]--;
                quantityNumber[tempResource.Item2].Text = amountChange[tempResource.Item2].ToString();

                totalCost = totalCost - tempResource.Item1.getPrice();
                finalPrice.Text = totalCost.ToString() + "$";
            }
        }

        void goBack(object sender, EventArgs e)
        {
            GameRef.board.getPlanet().setResource(backupShipResource);
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
            ControlManager.Draw(GameRef.SpriteBatch);

            string cargo = "Cargo capacity: " + currentCargo.ToString();
            fontPosition = new Vector2(300, 500);
            GameRef.SpriteBatch.DrawString(font1, cargo, fontPosition, Color.White);
            string maxCargo = "Maximum Cargo capacity: " + GameRef.spaceShip.getCargoCapacity().ToString();
            fontPosition = new Vector2(300, 530);
            GameRef.SpriteBatch.DrawString(font1, maxCargo, fontPosition, Color.White);
            for (int i = 0; i < tempPlanetResource.Count; i++)
            {
                string resource = tempPlanetResource[i].name + " " + tempPlanetResource[i].amount + " $" + tempPlanetResource[i].getPrice() + " each!";
                fontPosition = new Vector2(670, 360 + 30 * i);
                GameRef.SpriteBatch.DrawString(font1, resource, fontPosition, Color.White);
            }
            GameRef.SpriteBatch.End();
            base.Draw(gameTime);

        }

        #endregion

        #region Method Region

        #endregion
    }

}
