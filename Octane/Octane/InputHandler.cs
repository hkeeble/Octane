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


namespace Octane
{
    public class InputHandler : Microsoft.Xna.Framework.GameComponent
    {
        private static KeyboardState keyboardState;
        private static KeyboardState lastKeyboardState;

        public InputHandler(Game game)
            : base(game)
        {
            keyboardState = Keyboard.GetState();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        public static bool KeyDown(Keys key)
        {
            if (keyboardState.IsKeyDown(key))
                return true;
            else
                return false;
        }
    }
}
