using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Octane
{
    class Player
    {
        private WorldEntity _model;
        private Vector3 _translationThisFrame;

        public Player(WorldEntity model)
        {
            _model = model;
        }

        public void Draw()
        {
            _model.Draw();
        }

        public void Update()
        {
            if(InputHandler.KeyDown(Keys.A))
                _translationThisFrame.X -= 20f;
            if (InputHandler.KeyDown(Keys.D))
                _translationThisFrame.X += 20f;
            if (InputHandler.KeyDown(Keys.W))
                _translationThisFrame.Z -= 20f;
            if (InputHandler.KeyDown(Keys.S))
                _translationThisFrame.Z += 20f;

            _model.Translate(_translationThisFrame);
            _translationThisFrame = Vector3.Zero;
        }
    }
}
