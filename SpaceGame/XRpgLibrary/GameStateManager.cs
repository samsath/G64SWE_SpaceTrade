using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace XRpgLibrary
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameStateManager : GameComponent
    {
        #region Event Region

        public event EventHandler OnStateChange;

        #endregion

        #region Fields and Properties Region

        Stack<GameState> gameStates = new Stack<GameState>();

        const int startDrawOrder = 5000;
        const int drawOrderInc = 100;
        int drawOrder;

        public GameState CurrentState
        {
            get { return gameStates.Peek(); }
        }

        #endregion

        #region Constructor Region

        public GameStateManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            drawOrder = startDrawOrder;
        }

        #endregion

        #region XNA Method Region

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        #endregion

        #region Methods Region

        public void PopState()
        {
            if (gameStates.Count > 0)
            {
                RemoveState();
                drawOrder -= drawOrderInc;

                if (OnStateChange != null)
                    OnStateChange(this, null);
            }
        }

        private void RemoveState()
        {
            GameState State = gameStates.Peek();

            OnStateChange -= State.StateChange;
            Game.Components.Remove(State);
            gameStates.Pop();
        }

        public void PushState(GameState newState)
        {
            drawOrder += drawOrderInc;
            newState.DrawOrder = drawOrder;

            AddState(newState);

            if (OnStateChange != null)
                OnStateChange(this, null);
        }

        private void AddState(GameState newState)
        {
            gameStates.Push(newState);

            Game.Components.Add(newState);

            OnStateChange += newState.StateChange;
        }

        public void ChangeState(GameState newState)
        {
            while (gameStates.Count > 0)
                RemoveState();

            newState.DrawOrder = startDrawOrder;
            drawOrder = startDrawOrder;

            AddState(newState);

            if (OnStateChange != null)
                OnStateChange(this, null);
        }

        #endregion
    }
}
