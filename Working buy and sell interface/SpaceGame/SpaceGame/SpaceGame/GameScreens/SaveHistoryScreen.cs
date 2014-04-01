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


namespace SpaceGame.GameScreens
{


    public class SaveHistoryScreen : BaseGameState
    {
        int totalResources = 10;
        int unassignedResources = 10;
        int moneyAmount = 0;
        int turnAmount = 0;
        int ammoAmount = 0;
        int healthAmount = 0;
        int fuelAmount = 0;
        int cargoAmount = 0;

        //Dictionary is to record all changes made here and then pass it along to the userscreen.
        Dictionary<string, int> totransfer = new Dictionary<string, int>();

        PictureBox backgroundImage;
        Label Title;
        Label load1;
        Label load2;
        Label load3;
        Label load4;
        Label load5;

        //List<ResourceLabelSet> resourceLabel = new List<ResourceLabelSet>();

        EventHandler linkLabelHandler;


        #region Constructor Region

        public SaveHistoryScreen(Game game, GameStateManager stateManager)
            : base(game, stateManager)
        {
            Random rand = new Random();
            Debug.WriteLine(rand.Next(0, 10));
            //linkLabelHandler = new EventHandler(addSelectedResource);
        }

        #endregion
    }

}
