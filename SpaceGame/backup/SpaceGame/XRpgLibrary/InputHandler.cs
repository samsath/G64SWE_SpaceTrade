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
    public class InputHandler : Microsoft.Xna.Framework.GameComponent
    {
        #region Keyboard Field Region

        static KeyboardState keyboardState; ///Current state of the keyboard.
        static KeyboardState lastKeyboardState; ///State of the keyboard in the previous frame.

        #endregion

        #region Game Pad Field Region

        static GamePadState[] gamePadStates;
        static GamePadState[] lastGamePadStates;

        #endregion

        #region Keyboard Property Region
        
        /// <summary>
        /// Holds properties that expose the fields of the class (Encapsulation).
        /// </summary>

        public static KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }

        public static KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
        }

        #endregion

        #region Game Pad Property Region

        public static GamePadState[] GamePadStates
        {
            get { return gamePadStates; }
        }

        public static GamePadState[] LastGamePadStates
        {
            get { return lastGamePadStates; }
        }

        #endregion

        #region Constructor Region

        public InputHandler(Game game)
            : base(game)
        {
            keyboardState = Keyboard.GetState(); ///Keyboard gets the state of the frame.

            gamePadStates = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];

            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
                gamePadStates[(int)index] = GamePad.GetState(index);
        }

        #endregion

        #region XNA methods

        public override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// Update the state of the game in a frame.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            lastGamePadStates = (GamePadState[])gamePadStates.Clone();
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
                gamePadStates[(int)index] = GamePad.GetState(index);

            base.Update(gameTime);
        }

        #endregion

        #region General Method Region
        
        /// <summary>
        /// Method flush sets the lastKeyboardState field to the keyboardState field (input buffer).
        /// </summary>

        public static void Flush()
        {
            ///Detects the change of state.
            lastKeyboardState = keyboardState;
        }

        #endregion

        #region Keyboard Region
        
        /// <summary>
        /// Methods related to the use of the keyboard.
        /// </summary>

        public static bool KeyReleased(Keys key)
        {
            ///Detects the single key press when released
            return keyboardState.IsKeyUp(key) &&
                lastKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            ///Detects the single key press down
            return keyboardState.IsKeyDown(key) &&
                lastKeyboardState.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            ///Detects the key kept pressed down.
            return keyboardState.IsKeyDown(key);
        }

        #endregion

        #region Game Pad Region

        public static bool ButtonReleased(Buttons button, PlayerIndex index)
        {
            return gamePadStates[(int)index].IsButtonUp(button) &&
                lastGamePadStates[(int)index].IsButtonDown(button);
        }

        public static bool ButtonPressed(Buttons button, PlayerIndex index)
        {
            return gamePadStates[(int)index].IsButtonDown(button) &&
                lastGamePadStates[(int)index].IsButtonUp(button);
        }

        public static bool ButtonDown(Buttons button, PlayerIndex index)
        {
            return gamePadStates[(int)index].IsButtonDown(button);
        }

        #endregion
    }
}
