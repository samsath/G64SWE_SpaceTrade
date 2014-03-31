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
using SpaceGame.Components;


namespace SpaceGame.GameScreens
{

    internal class ResourceLabelSet
    {
        internal Label Label;
        internal LinkLabel LinkLabel;

        internal ResourceLabelSet(Label label, LinkLabel linkLabel)
        {
            Label = label;
            LinkLabel = linkLabel;
        }
    }

    public class AdminScreen : BaseGameState
    {
        #region Field Region

        int totalResources = 10;
        int unassignedResources = 10;
        int moneyAmount = 100000;
        int turnAmount = 3;
        int ammoAmount = 0;
        int healthAmount = 0;
        int fuelAmount = 0;
        int cargoAmount = 5;

        //Dictionary is to record all changes made here and then pass it along to the userscreen.
        Dictionary<string, int> totransfer = new Dictionary<string, int>();

        PictureBox backgroundImage;
        Label remainingResources;
        Label MoneyLabel;
        Label moneyNumber;
        Label turnNumber;
        Label ammoNumber;
        Label healthNumber;
        Label fuelNumber;
        Label cargoNumber;

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

        public AdminScreen(Game game, GameStateManager stateManager)
            : base(game, stateManager)
        {
            Random rand = new Random();
            Debug.WriteLine(rand.Next(0, 10));
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
            Game.Content.Load<Texture2D>(@"Backgrounds\Sombrero"),
            GameRef.ScreenRectangle);
            ControlManager.Add(backgroundImage);

            //List<ResourceData> resourceData = new List<ResourceData>();

            Vector2 nextControlPosition = new Vector2(300, 100);

            remainingResources = new Label();
            remainingResources.Text = "Total Resources: " + unassignedResources.ToString();
            remainingResources.Position = nextControlPosition;

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;

            ControlManager.Add(remainingResources);

            MoneyLabel = new Label();
            MoneyLabel.Text = "Money";
            MoneyLabel.Position = nextControlPosition;


            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;

            ControlManager.Add(MoneyLabel);

            Label Money = new Label();
            Money.Text = "Initial Amount";
            Money.Position = nextControlPosition;

            LinkLabel linkLabel = new LinkLabel();
            linkLabel.TabStop = true;
            linkLabel.Text = "+";
            linkLabel.Position = new Vector2(nextControlPosition.X + 350, nextControlPosition.Y);

            linkLabel.Selected += addSelectedResource;
            linkLabel.Selected += new EventHandler(augmentMoney);

            moneyNumber = new Label();
            moneyNumber.Text = moneyAmount.ToString();
            moneyNumber.Position = new Vector2(nextControlPosition.X + 500, nextControlPosition.Y);           

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;

            ControlManager.Add(Money);
            ControlManager.Add(linkLabel);
            ControlManager.Add(moneyNumber);

            //Turns

            Label Turns = new Label();
            Turns.Text = "Number of Turns";
            Turns.Position = nextControlPosition;

            LinkLabel linkLabelTurns = new LinkLabel();
            linkLabelTurns.TabStop = true;
            linkLabelTurns.Text = "+";
            linkLabelTurns.Position = new Vector2(nextControlPosition.X + 350, nextControlPosition.Y);           

            linkLabelTurns.Selected += addSelectedResource;

            linkLabelTurns.Selected += new EventHandler(augmentTurns);

            turnNumber = new Label();
            turnNumber.Text = turnAmount.ToString();
            turnNumber.Position = new Vector2(nextControlPosition.X + 500, nextControlPosition.Y);

            ControlManager.Add(Turns);
            ControlManager.Add(linkLabelTurns);
            ControlManager.Add(turnNumber);

            //

            resourceLabel1.Add(new ResourceLabelSet(Money, linkLabel));

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;

            /*
             * Ships
             * */

            Label shipLabel = new Label();
            shipLabel.Text = "Ships.";
            shipLabel.Position = nextControlPosition;

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;

            ControlManager.Add(shipLabel);

            

            //Cargo Capacity

            Label CargoCapacity = new Label();
            CargoCapacity.Text = "Cargo Capacity";
            CargoCapacity.Position = nextControlPosition;

            ControlManager.Add(CargoCapacity);

            LinkLabel CargoCapacityLabel = new LinkLabel();
            CargoCapacityLabel.TabStop = true;
            CargoCapacityLabel.Text = "+";
            CargoCapacityLabel.Position = new Vector2(nextControlPosition.X + 350, nextControlPosition.Y);            

            CargoCapacityLabel.Selected += addSelectedResource;
            CargoCapacityLabel.Selected += new EventHandler(augmentCargo);

            cargoNumber = new Label();
            cargoNumber.Text = cargoAmount.ToString();
            cargoNumber.Position = new Vector2(nextControlPosition.X + 500, nextControlPosition.Y);

            ControlManager.Add(CargoCapacityLabel);
            ControlManager.Add(cargoNumber);

            resourceLabel1.Add(new ResourceLabelSet(CargoCapacity, CargoCapacityLabel));
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 20f;            
            //


            //Undo Label
            LinkLabel undoLabel = new LinkLabel();
            undoLabel.Text = "Reset Values";
            undoLabel.Position = nextControlPosition;
            undoLabel.TabStop = true;
            undoLabel.Selected += new EventHandler(undoLabel_Selected);
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;

            ControlManager.Add(undoLabel);

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
            // change this so it also send the info to the Character screen
            GameRef.spaceShip.setMoney(moneyAmount);
            GameRef.spaceShip.setNumberOfTurn(turnAmount);
            GameRef.spaceShip.setCargoCapacity(cargoAmount);
            undoResources.Clear();
            StateManager.ChangeState(GameRef.CharacterGeneratorScreen);
        }

        void undoLabel_Selected(object sender, EventArgs e)
        {
            if (unassignedResources == TotalResources)
                return;

            /*string resourceName = undoResources.Peek();
            undoResources.Pop();
            unassignedResources++;
            if (moneyAmmount > 0)
            {
                moneyAmmount--;
                moneyNumber.Text = moneyAmmount.ToString();
            }

            // Update the skill points for the appropriate skill
            remainingResources.Text = "Total Resources: " + unassignedResources.ToString();*/

            unassignedResources = 10;
            remainingResources.Text = "Total Resources: " + unassignedResources.ToString();
            moneyAmount = 500;
            moneyNumber.Text = moneyAmount.ToString();
            turnAmount = 3;
            turnNumber.Text = turnAmount.ToString();
            ammoAmount = 0;
            ammoNumber.Text = ammoAmount.ToString();
            healthAmount = 0;
            healthNumber.Text = healthAmount.ToString();
            fuelAmount = 0;
            fuelNumber.Text = fuelAmount.ToString();
            cargoAmount = 0;
            cargoNumber.Text = cargoAmount.ToString();

        }

        void addSelectedResource(object sender, EventArgs e)
        {
            if (unassignedResources <= 0)
            {
                remainingResources.Text = "You Are Out Of Resources";
                //return;
            }
            else
            {             
                string resourceName = ((LinkLabel)sender).Type;
                
                Console.WriteLine(resourceName);
                undoResources.Push(resourceName);
                unassignedResources--;

                // Update the skill points for the appropriate skill
                remainingResources.Text = "Total Resources: " + unassignedResources.ToString();
            }
        }
        void goBack(object sender, EventArgs e)
        {
            StateManager.ChangeState(GameRef.StartMenuScreen);
        }

        void augmentMoney(object sender, EventArgs e)
        {
            if (unassignedResources > 0)
            {
                moneyAmount = moneyAmount + 1000 ;
                moneyNumber.Text = moneyAmount.ToString();
            }
            
        }

        void augmentTurns(object sender, EventArgs e)
        {
            if (unassignedResources > 0)
            {
                turnAmount++;
                turnNumber.Text = turnAmount.ToString();
            }

        }

        void augmentAmmo(object sender, EventArgs e)
        {
            if (unassignedResources > 0)
            {
                ammoAmount++;
                ammoNumber.Text = ammoAmount.ToString();
            }

        }

        void augmentHealth(object sender, EventArgs e)
        {
            if (unassignedResources > 0)
            {
                healthAmount++;
                healthNumber.Text = healthAmount.ToString();
            }

        }

        void augmentFuel(object sender, EventArgs e)
        {
            if (unassignedResources > 0)
            {
                fuelAmount++;
                fuelNumber.Text = fuelAmount.ToString();
            }

        }

        void augmentCargo(object sender, EventArgs e)
        {
            if (unassignedResources > 0)
            {
                cargoAmount++;
                cargoNumber.Text = cargoAmount.ToString();
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
