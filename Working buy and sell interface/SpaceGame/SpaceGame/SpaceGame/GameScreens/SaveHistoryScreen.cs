using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XRpgLibrary;
using XRpgLibrary.Controls;
using System.Diagnostics;
using STDatabase;
using SpaceGame.Components;

namespace SpaceGame.GameScreens
{


    public class SaveHistoryScreen : BaseGameState
    {
        #region Field Region

        int totalResources = 10;
        int unassignedResources = 10;
        int moneyAmount = 0;
        int turnAmount = 0;
        int ammoAmount = 0;
        int healthAmount = 0;
        int fuelAmount = 0;
        int cargoAmount = 0;

        // for saving
        IDatabase dbs = new Database();


        //List<ResourceLabelSet> resourceLabel = new List<ResourceLabelSet>();
        
        EventHandler linkLabelHandler;

        #region Property Region
        #endregion

        #region Constructor Region

        public SaveHistoryScreen(Game game, GameStateManager manager)
            : base(game, manager)
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

            Title = new Label();
            Title.Text = "Total Resources: " + unassignedResources.ToString();
            Title.Position = nextControlPosition;

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;

            ControlManager.Add(Title);

            Label load1 = new Label();
            load1.Text = "Initial Amount";
            load1.Position = nextControlPosition;

            LinkLabel linkLabel = new LinkLabel();
            linkLabel.TabStop = true;
            linkLabel.Text = "+";
            linkLabel.Position = new Vector2(nextControlPosition.X + 350, nextControlPosition.Y);

            linkLabel.Selected += addSelectedResource;
            linkLabel.Selected += new EventHandler(augmentMoney);

            load2 = new Label();
            load2.Text = moneyAmount.ToString();
            load2.Position = new Vector2(nextControlPosition.X + 500, nextControlPosition.Y);

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;

            ControlManager.Add(load1);
            ControlManager.Add(load2);
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

            //Ammo
            Label Ammo = new Label();
            Ammo.Text = "Ammo Ammount";
            Ammo.Position = nextControlPosition;

            ControlManager.Add(Ammo);

            LinkLabel AmmoLabel = new LinkLabel();
            AmmoLabel.TabStop = true;
            AmmoLabel.Text = "+";
            AmmoLabel.Position = new Vector2(nextControlPosition.X + 350, nextControlPosition.Y);

            AmmoLabel.Selected += addSelectedResource;
            AmmoLabel.Selected += new EventHandler(augmentAmmo);

            ammoNumber = new Label();
            ammoNumber.Text = ammoAmount.ToString();
            ammoNumber.Position = new Vector2(nextControlPosition.X + 500, nextControlPosition.Y);

            ControlManager.Add(AmmoLabel);
            ControlManager.Add(ammoNumber);

            resourceLabel1.Add(new ResourceLabelSet(Ammo, AmmoLabel));
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;
            //

            //Health

            Label Health = new Label();
            Health.Text = "Maximum Health";
            Health.Position = nextControlPosition;

            ControlManager.Add(Health);

            LinkLabel HealthLabel = new LinkLabel();
            HealthLabel.TabStop = true;
            HealthLabel.Text = "+";
            HealthLabel.Position = new Vector2(nextControlPosition.X + 350, nextControlPosition.Y);

            HealthLabel.Selected += addSelectedResource;
            HealthLabel.Selected += new EventHandler(augmentHealth);

            healthNumber = new Label();
            healthNumber.Text = healthAmount.ToString();
            healthNumber.Position = new Vector2(nextControlPosition.X + 500, nextControlPosition.Y);

            ControlManager.Add(HealthLabel);
            ControlManager.Add(healthNumber);

            resourceLabel1.Add(new ResourceLabelSet(Health, HealthLabel));
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;
            //

            //Fuel

            Label Fuel = new Label();
            Fuel.Text = "Fuel Capacity";
            Fuel.Position = nextControlPosition;

            ControlManager.Add(Fuel);

            LinkLabel FuelLabel = new LinkLabel();
            FuelLabel.TabStop = true;
            FuelLabel.Text = "+";
            FuelLabel.Position = new Vector2(nextControlPosition.X + 350, nextControlPosition.Y);

            FuelLabel.Selected += addSelectedResource;
            FuelLabel.Selected += new EventHandler(augmentFuel);

            fuelNumber = new Label();
            fuelNumber.Text = fuelAmount.ToString();
            fuelNumber.Position = new Vector2(nextControlPosition.X + 500, nextControlPosition.Y);

            ControlManager.Add(FuelLabel);
            ControlManager.Add(fuelNumber);

            resourceLabel1.Add(new ResourceLabelSet(Fuel, FuelLabel));
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;
            //

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
            moneyAmount = 0;
            moneyNumber.Text = moneyAmount.ToString();
            turnAmount = 0;
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
                moneyAmount++;
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
