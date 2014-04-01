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
    internal class ResourceLabelSetSell
    {
        internal Label Label;
        internal LinkLabel LinkLabel;

        internal ResourceLabelSetSell(Label labelSell, LinkLabel linkLabelSell)
        {
            Label = labelSell;
            LinkLabel = linkLabelSell;
        }
    }

    public class SellScreen : BaseGameState
    {
        #region Field Region

        // Ship's current Money (at the end of the buy phase, this value should return to the gameplay by using the acceptLabel_Selected method)
        int totalMoney; // Ship's current money //input
        int cargoAmount; // Ship's current cargo capacity //input

        //turns should only be in the upgrade screen
        int turnAmount; // Ship's current number of turn//should not be here

        int totalResources = 10000; //money remaining should be equal to the totalmoney amount
        int unassignedResources = 10000;

        //price set by the player
        int priceAmount = 0;

        //original price brings the set price of the resource from the gameplay
        int originalPrice = 5000; //input

        //ammount of the resource brought from the gameplay(Ship)
        int itemAmount = 10; // input
        //ammount of the resource brought from the gameplay(Planet)
        int quantityAmount = 10;//input

        //offer made by the AI
        int offerAmount = 0;
        //Quantity acquired shows the number of resources of the planet and the changes made by buying from the ship
        int quantityAcquired = 0;//output

        PictureBox backgroundImage;
        Label remainingMoney;
        Label NameLabel;
        Label quantityLabel;
        Label priceLabel;
        Label priceNumber;
        Label offerLabel;
        Label offerNumber;
        Label quantityNumber;
        Label planetQuantity;
        Label PlanetResourceLabel;
        Label PlanetResourceText;
        LinkLabel acceptLabel = new LinkLabel();

        //List<ResourceLabelSet> resourceLabel = new List<ResourceLabelSet>();
        List<ResourceLabelSet> resourceLabel1 = new List<ResourceLabelSet>();
        Stack<string> undoResources = new Stack<string>();
        EventHandler linkLabelHandler;

        #endregion

        #region Property Region

        public int TotalResources
        {
            get { return totalResources; }
            set
            {
                totalResources = value;
                unassignedResources = value;
            }
        }

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
            turnAmount = GameRef.spaceShip.getNumberOfTurn();
            cargoAmount = GameRef.spaceShip.getCargoCapacity();
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

            remainingMoney = new Label();
            remainingMoney.Text = "Total Amount of Money: " + unassignedResources.ToString() + "$";
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

            offerLabel = new Label();
            offerLabel.Text = "Offer";
            offerLabel.Position = new Vector2(nextControlPosition.X + 650, nextControlPosition.Y);

            ControlManager.Add(offerLabel);

            //planet quantity

            quantityLabel = new Label();
            quantityLabel.Text = "Quantity";
            quantityLabel.Position = new Vector2(nextControlPosition.X + 750, nextControlPosition.Y);


            ControlManager.Add(quantityLabel);

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
            addLabel.Position = new Vector2(nextControlPosition.X + 250, nextControlPosition.Y+20);
         
            addLabel.Selected += new EventHandler(increasePriceItem);

            LinkLabel substractLabel = new LinkLabel();
            substractLabel.TabStop = true;
            substractLabel.Text = "-";
            substractLabel.Position = new Vector2(nextControlPosition.X + 300, nextControlPosition.Y+20);

            substractLabel.Selected += new EventHandler(decreasePriceItem);

            LinkLabel addPlanetLabel = new LinkLabel();
            addPlanetLabel.TabStop = true;
            addPlanetLabel.Text = "Add";
            addPlanetLabel.Position = new Vector2(nextControlPosition.X + 450, nextControlPosition.Y);


            addPlanetLabel.Selected += new EventHandler(increaseItem);
            addPlanetLabel.Selected += addSelectedResource;

            LinkLabel substractPlanetLabel = new LinkLabel();
            substractPlanetLabel.TabStop = true;
            substractPlanetLabel.Text = "Remove";
            substractPlanetLabel.Position = new Vector2(nextControlPosition.X + 520, nextControlPosition.Y);


            substractPlanetLabel.Selected += new EventHandler(decreaseItem);
            substractPlanetLabel.Selected += new EventHandler(substractSelectedResource);
             

            //Offer
            offerNumber = new Label();
            offerNumber.Text = offerAmount.ToString();
            offerNumber.Position = new Vector2(nextControlPosition.X + 650, nextControlPosition.Y);

            //

            //Planet quantity

            planetQuantity = new Label();
            planetQuantity.Text = offerAmount.ToString();
            planetQuantity.Position = new Vector2(nextControlPosition.X + 750, nextControlPosition.Y);
            //planetQuantity.Text = quantityAcquired.ToString();

            //

            ControlManager.Add(nameOfItem);
            ControlManager.Add(addLabel);
            ControlManager.Add(substractLabel);
            ControlManager.Add(addPlanetLabel);
            ControlManager.Add(substractPlanetLabel);
            ControlManager.Add(quantityNumber);
            ControlManager.Add(priceNumber);
            ControlManager.Add(offerNumber);
            ControlManager.Add(planetQuantity);
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
            //StateManager.ChangeState(GameRef.GamePlayScreen, null);
            //save states to DB method here
            totalMoney = unassignedResources;
            acceptLabel.Text = "Changes accepted.";
        }

        void addSelectedResource(object sender, EventArgs e)
        {
            if (quantityAmount == 0)
            {
                return;
            }
            else
            {
                string resourceName = ((LinkLabel)sender).Type;
                undoResources.Push(resourceName);
                unassignedResources = unassignedResources + offerAmount;

                // Update the skill points for the appropriate resource
                remainingMoney.Text = "Total Amount of Money: " + unassignedResources.ToString() + "$";

                //quantity reduced
                quantityAmount--;
                quantityNumber.Text = quantityAmount.ToString();
                quantityAcquired++;
                planetQuantity.Text = quantityAcquired.ToString();
            }
        }

        void substractSelectedResource(object sender, EventArgs e)
        {
            //max value of the items to be saved so can be used as reference

            if (quantityAmount == itemAmount)
            {
                unassignedResources = totalResources;
                remainingMoney.Text = "Total Amount of Money: " + unassignedResources.ToString() + "$";
                //planetQuantity.Text = "";
            }
            else if (quantityAmount>=0)
            {

                unassignedResources = unassignedResources - offerAmount;
                remainingMoney.Text = "Total Amount of Money: " + unassignedResources.ToString() + "$";
                //quantity
                quantityAmount++;
                quantityNumber.Text = quantityAmount.ToString();
                quantityAcquired--;
                planetQuantity.Text = quantityAcquired.ToString();
            }
        }

        void goBack(object sender, EventArgs e)
        {
            GameRef.spaceShip.setGameState("playing");
            StateManager.ChangeState(GameRef.GamePlayScreen);
        }

        
        void increaseItem(object sender, EventArgs e)
        {
            if (quantityAmount == 0)
            {
                offerNumber.Text = " ";
            }else
            if (unassignedResources > 0)
            {
                //Overpriced amount.

                if (priceAmount >= (originalPrice * 2))
                {
                    //quantity ammount to be changed to number of resources of the planet
                    //offerAmount = originalPrice;
                    //offerNumber.Text = offerAmount.ToString() + "$";
                    if (quantityAcquired == 0)
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice*1.5);
                        offerAmount = random.Next(originalPrice, range);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if((quantityAcquired >0) && (quantityAcquired <= 3))
                    {
                        offerAmount = originalPrice;
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if (quantityAcquired > 3 && (quantityAcquired <= 5))
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 0.75);
                        offerAmount = random.Next(range, originalPrice);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if (quantityAcquired > 5)
                    {
                        offerAmount = 0;
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                }

                //Reasonably overpriced amount.

                if ((priceAmount >= (originalPrice * 1.5)) && (priceAmount < (originalPrice * 2)))
                {
                    if (quantityAcquired == 0)
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 1.5);
                        offerAmount = random.Next(originalPrice, range);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if ((quantityAcquired > 0) && (quantityAcquired <= 3))
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 1.2);
                        offerAmount = random.Next(originalPrice, range);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if (quantityAcquired > 3 && (quantityAcquired <= 5))
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 0.75);
                        offerAmount = random.Next(range, originalPrice);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if (quantityAcquired > 5)
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 0.75);
                        int range1 = (int)(originalPrice * 0.5);
                        offerAmount = random.Next(range1, range);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                }

                // Reasonable amount

                if ((priceAmount > originalPrice) && (priceAmount < (originalPrice * 1.5)))
                {
                    if (quantityAcquired == 0)
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 1.5);
                        int range1 = (int)(originalPrice * 1.2);
                        offerAmount = random.Next(range1, range);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if ((quantityAcquired > 0) && (quantityAcquired <= 3))
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 1.2);
                        offerAmount = random.Next(originalPrice, range);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if (quantityAcquired > 3 && (quantityAcquired <= 5))
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 1.1);
                        offerAmount = random.Next(originalPrice, range);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if (quantityAcquired > 5)
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 0.85);
                        offerAmount = random.Next(range, originalPrice);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                }

                //Equal amount

                if (priceAmount == originalPrice) 
                {
                    if (quantityAcquired == 0)
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 1.7);
                        int range1 = (int)(originalPrice * 1.5);
                        offerAmount = random.Next(range1, range);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if ((quantityAcquired > 0) && (quantityAcquired <= 3))
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 1.5);
                        offerAmount = random.Next(originalPrice, range);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if (quantityAcquired > 3 && (quantityAcquired <= 5))
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 1.2);
                        offerAmount = random.Next(originalPrice, range);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if (quantityAcquired > 5)
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 0.75);
                        offerAmount = random.Next(range, originalPrice);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                }

                //Cheaper price

                if ((priceAmount < originalPrice) && (priceAmount >= (originalPrice * 0.75))) 
                {
                    if (quantityAcquired == 0)
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 1.2);
                        offerAmount = random.Next(originalPrice, range);
                        offerNumber.Text = offerAmount.ToString() + "$";
                        
                    }
                    else if ((quantityAcquired > 0) && (quantityAcquired <= 3))
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 0.75);
                        int range1 = (int)(originalPrice * 1.2);
                        offerAmount = random.Next(range, range1);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if (quantityAcquired > 3 && (quantityAcquired <= 5))
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 0.75);
                        int range1 = (int)(originalPrice * 0.9);
                        offerAmount = random.Next(range, range1);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if (quantityAcquired > 5)
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 0.75);
                        offerAmount = random.Next(range, priceAmount);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                }

                //Really cheap price

                if ((priceAmount < (originalPrice * 0.75)) && (priceAmount >= (originalPrice * 0.5)))
                {
                    if (quantityAcquired == 0)
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 1.2);
                        offerAmount = random.Next(originalPrice, range);
                        offerNumber.Text = offerAmount.ToString() + "$";

                    }
                    else if ((quantityAcquired > 0) && (quantityAcquired <= 3))
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 0.75);
                        int range1 = (int)(originalPrice * 0.9);
                        offerAmount = random.Next(range, range1);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if (quantityAcquired > 3 && (quantityAcquired <= 5))
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 0.5);
                        int range1 = (int)(originalPrice * 0.75);
                        offerAmount = random.Next(range, range1);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                    else if (quantityAcquired > 5)
                    {
                        Random random = new Random();
                        int range = (int)(originalPrice * 0.5);
                        offerAmount = random.Next(range, priceAmount);
                        offerNumber.Text = offerAmount.ToString() + "$";
                    }
                }

                //Always sell price

                if (priceAmount < (originalPrice * 0.5))
                {
                    offerAmount = priceAmount;
                    offerNumber.Text = offerAmount.ToString() + "$";
                }


            }
            /*
             * offerAmount = (priceAmount / quantityAmount);
                offerNumber.Text = offerAmount.ToString()+"$";
             */ 
        }

        void decreaseItem(object sender, EventArgs e)
        {
            //while loop
            if (quantityAmount == itemAmount)
            {
                offerNumber.Text = " ";
            }
            else if (quantityAmount>0)
            
            {
                offerAmount = (priceAmount / quantityAmount);
                offerNumber.Text = offerAmount.ToString() + "$";
            }
            else if (quantityAmount == 0)
            {
                return;
            }

        }

        void increasePriceItem(object sender, EventArgs e)
        {
            priceAmount = priceAmount + 1000;
            priceNumber.Text = priceAmount.ToString() + "$";
        }

        void decreasePriceItem(object sender, EventArgs e)
        {
            if (priceAmount > 0)
            {
                priceAmount = priceAmount - 1000;
                priceNumber.Text = priceAmount.ToString() + "$";
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
