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
        int totalMoney = 1000;

        //Dictionary is to record all changes made here and then pass it along to the userscreen.
        Dictionary<string, int> totransfer = new Dictionary<string, int>();

        PictureBox backgroundImage;
        Label remainingResources;
        Label MoneyLabel;
        Label CostLabel;

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
            Game.Content.Load<Texture2D>(@"Backgrounds\titlescreen"),
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

            CostLabel = new Label();
            CostLabel.Text = totalMoney.ToString();
            CostLabel.Position = new Vector2(nextControlPosition.X + 350, nextControlPosition.Y);

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;

            ControlManager.Add(MoneyLabel);
            ControlManager.Add(CostLabel);

            Label Money = new Label();
            Money.Text = "Initial Ammount";
            Money.Position = nextControlPosition;

            LinkLabel linkLabel = new LinkLabel();
            linkLabel.TabStop = true;
            linkLabel.Text = "+";
            linkLabel.Position = new Vector2(nextControlPosition.X + 350, nextControlPosition.Y);

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;

            linkLabel.Selected += addSelectedResource;

            ControlManager.Add(Money);
            ControlManager.Add(linkLabel);

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

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;

            AmmoLabel.Selected += addSelectedResource;

            ControlManager.Add(AmmoLabel);

            resourceLabel1.Add(new ResourceLabelSet(Ammo, AmmoLabel));
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

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;

            HealthLabel.Selected += addSelectedResource;

            ControlManager.Add(HealthLabel);

            resourceLabel1.Add(new ResourceLabelSet(Health, HealthLabel));

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

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;

            FuelLabel.Selected += addSelectedResource;

            ControlManager.Add(FuelLabel);

            resourceLabel1.Add(new ResourceLabelSet(Fuel, FuelLabel));

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

            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;

            CargoCapacityLabel.Selected += addSelectedResource;

            ControlManager.Add(CargoCapacityLabel);

            resourceLabel1.Add(new ResourceLabelSet(CargoCapacity, CargoCapacityLabel));
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 5f;

            //


            //Undo Label
            LinkLabel undoLabel = new LinkLabel();
            undoLabel.Text = "Undo";
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

            ControlManager.Add(acceptLabel);
            ControlManager.NextControl();
            nextControlPosition.Y += ControlManager.SpriteFont.LineSpacing + 10f;
			
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

            string resourceName = undoResources.Peek();
            undoResources.Pop();
            unassignedResources++;

            // Update the skill points for the appropriate skill
            remainingResources.Text = "Total Resources: " + unassignedResources.ToString();
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
