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

        const float MAX_SPEED = 6.0f;
        const float MIN_SPEED = 0.5f;

        private bool dead = false;

        public Player(Model model, Vector3 position, Vector3 rotation) : base(model, position, rotation)
        {

        }

        public override void Update()
        {
            _velocity.X = InputHandler.GamePadState.ThumbSticks.Left.X * 0.1f;
            _velocity.Y = InputHandler.GamePadState.ThumbSticks.Left.Y * 0.1f;

            if (_velocity.X > 0)
                Rotate(new Vector3(0f, 0f, -1f));
            else if (_velocity.X < 0)
                Rotate(new Vector3(0f, 0f, 1f));
            else
                SetRotation(new Vector3(Rotation.X, Rotation.Y, 0f));

            if (_velocity.Y > 0)
                Rotate(new Vector3(1f, 0f, 0f));
            else if (_velocity.Y < 0)
                Rotate(new Vector3(-1f, 0f, 0f));
            else
                SetRotation(new Vector3(180, Rotation.Y, Rotation.Z));

            if (Rotation.Z > 25)
                SetRotation(new Vector3(Rotation.X, Rotation.Y, 25f));
            if (Rotation.Z < -25)
                SetRotation(new Vector3(Rotation.X, Rotation.Y, -25f));

            if (Rotation.X > 205)
                SetRotation(new Vector3(205, Rotation.Y, Rotation.Z));
            if (Rotation.X < 165)
                SetRotation(new Vector3(165, Rotation.Y, Rotation.Z));

            if (InputHandler.GamePadState.Triggers.Right == 1.0f && _currentSpeed < MAX_SPEED)
                _currentSpeed += 0.1f;
            else if (InputHandler.GamePadState.Triggers.Left == 1.0f && _currentSpeed > MIN_SPEED)
                _currentSpeed -= 0.1f;

            base.Update();
        }

        public float CurrentSpeed { get { return _currentSpeed; } }
        public bool Dead { get { return dead; } set { dead = value; } }
    }
}
