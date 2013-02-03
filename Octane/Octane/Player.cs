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
        const float DRAG = 0.001f;
        private float _currentSpeed = 1.0f;

        public Player(Model model, Vector3 position, Vector3 rotation) : base(model, position, rotation)
        {

        }

        public override void Update()
        {
            if (InputHandler.KeyDown(Keys.Left))
                _velocity.X = -0.01f;
            if (InputHandler.KeyDown(Keys.Right))
                _velocity.X = 0.01f;
            if (InputHandler.KeyDown(Keys.Up))
                _velocity.Y = 0.01f;
            if (InputHandler.KeyDown(Keys.Down))
                _velocity.Y = -0.01f;
            else
            {
                if (_velocity.X > 0)
                    _velocity.X -= DRAG;
                if (_velocity.X < 0)
                    _velocity.X += DRAG;
                if (_velocity.Y > 0)
                    _velocity.Y -= DRAG;
                if (_velocity.Y < 0)
                    _velocity.Y += DRAG;
            }

            if (InputHandler.KeyDown(Keys.Space))
            {
                _currentSpeed += 0.1f;
            }

            base.Update();
        }

        public float CurrentSpeed { get { return _currentSpeed; } }
    }
}
