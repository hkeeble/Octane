using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        private static Vector3 _position;
        private static float _aspectRatio;
        private static Matrix _view;
        private static Matrix _projection;
        private static Vector3 _up;
        private static Vector3 _target;
        private static Vector3 _direction;
        private static bool _rotating;
        private static float _rotationDirection;

        private static float _currentRotation = 0f;

        private static float _newZ; // Used to represent the Z axis target of the next movement

        const float SPEED = 0.5f;
        const float ROTATION_SPEED = 0.01f;

        public Camera(Game game, GraphicsDevice graphics, Vector3 position, Vector3 target, Vector3 up) : base(game)
        {
            _view = Matrix.CreateLookAt(position, target, up);
            _aspectRatio = graphics.Viewport.AspectRatio;
            _position = position;
            _up = up;
            _target = target;
            _direction = target - position;
            _direction.Normalize();

            _rotating = false;
            _rotationDirection = 0f;
            _newZ = 1f;

            CreateLookAt();

            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), _aspectRatio, 1, 1500);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            CreateLookAt();

            if (_rotating)
                Rotate();

            base.Update(gameTime);
        }

        public static void Move(Vector3 direction)
        {
            _position += direction * SPEED;
        }

        public static void Rotate(float direction)
        {
            _rotating = true;
            _rotationDirection = direction;
        }

        private static void Rotate()
        {
            if (_direction.Z == _newZ)
            {
                _newZ = _newZ * (-1);
                _rotating = false;
                _rotationDirection = 0f;
            }
            else
                _direction = Vector3.Transform(_direction, Matrix.CreateFromAxisAngle(_up, _rotationDirection * ROTATION_SPEED));
        }

        private void CreateLookAt()
        {
            _view = Matrix.CreateLookAt(Position, _direction+_position, _up);
        }

        public static Vector3 Target { get { return _target; } set { _target = value; } }
        public static Vector3 Position { get { return _position; } }
        public static float AspectRatio { get { return _aspectRatio; } set { _aspectRatio = value; } }
        public static Matrix View { get { return _view; } set { _view = value; } }
        public static Matrix Projection { get { return _projection; } set { _projection = value; } }
    }
}