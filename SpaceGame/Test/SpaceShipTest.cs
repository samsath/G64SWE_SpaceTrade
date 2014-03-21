using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceGame;
using SpaceGame.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace Test
{
    [TestClass]
    public class SpaceShipTest
    {
        DatabasePopulate dbp;
        [TestInitialize]
        public void init()
        {
            dbp = new DatabasePopulate();

        }

        [TestMethod]
        public void IfKeyPressedDiceWillRoll()
        {
            GameTime time = new GameTime();
            SpaceShip myShip = new SpaceShip();
            myShip.setState("waiting");
            myShip.pressKeyboard("space");
            myShip.Update(time);
            Debug.WriteLine("asdfasdfasdf "+myShip.getDiceRolled());
            Assert.IsTrue(myShip.getDiceRolled()>=1&&myShip.getDiceRolled()<=6);
        }
        [TestMethod]
        public void ResourcesAddedatStartofGame()
        {
            Assert.IsTrue(dbp.Startresourceadd());
        }
    }
}
