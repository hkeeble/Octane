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
        const float DRAG = 1f;

        public Player(Model model, Vector3 position, Vector3 rotation) : base(model, position, rotation)
        {
            
        }

        public override void Update()
        {
            if (InputHandler.KeyDown(Keys.Left))
                _velocity.X = -20f;
            if (InputHandler.KeyDown(Keys.Right))
                _velocity.X = 20f;
            else
            {
                if (_velocity.X > 0)
                    _velocity.X -= DRAG;
                if (_velocity.X < 0)
                    _velocity.X += DRAG;
            }

            if (_velocity.X > 0)
                    SetRotation(new Vector3(0.0f, 0.0f, -0.3f));
            else if (_velocity.X < 0)
                    SetRotation(new Vector3(0.0f, 0.0f, 0.3f));
            else
                SetRotation(Vector3.Zero);

            base.Update();
        }
    }
}
