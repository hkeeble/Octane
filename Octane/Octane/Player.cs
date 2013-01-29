using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Octane
{
    class Player : PhysicsEntity
    {
        public Player(Model model, Vector3 position, Vector3 rotation) : base(model, position, rotation)
        {
            
        }

        public override void Update()
        {
            if(InputHandler.KeyDown(Keys.A))
                _velocity.X = 20f;
            if (InputHandler.KeyDown(Keys.D))
                _velocity.X = 20f;
            if (InputHandler.KeyDown(Keys.W))
                _velocity.Z = 20f;
            if (InputHandler.KeyDown(Keys.S))
                _velocity.Z = 20f;

            base.Update();
        }
    }
}
