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
        private static Vector3 _direction;
        private static Vector3 _up;

        const float SPEED = 0.5f;

        public Camera(Game game, GraphicsDevice graphics, Vector3 position, Vector3 target, Vector3 up) : base(game)
        {
            _view = Matrix.CreateLookAt(position, target, up);
            _aspectRatio = graphics.Viewport.AspectRatio;
            _position = position;
            _direction = target - position;
            _up = up;
            _direction.Normalize();
            CreateLookAt();

            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), _aspectRatio, 1, 5000);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            CreateLookAt();

            if (InputHandler.KeyDown(Microsoft.Xna.Framework.Input.Keys.W))
                _position += _direction * SPEED;
            if (InputHandler.KeyDown(Microsoft.Xna.Framework.Input.Keys.S))
                _position -= _direction * SPEED;
            if (InputHandler.KeyDown(Microsoft.Xna.Framework.Input.Keys.A))
                _direction = Vector3.Transform(_direction, Matrix.CreateFromAxisAngle(_up, 0.05f));
            if (InputHandler.KeyDown(Microsoft.Xna.Framework.Input.Keys.D))
                _direction = Vector3.Transform(_direction, Matrix.CreateFromAxisAngle(_up, -0.05f));


            base.Update(gameTime);
        }

        public void Rotate(Vector3 rotation)
        {
            _direction = Vector3.Transform(_direction, Matrix.CreateFromAxisAngle(_up, rotation.Y));
            _direction.Normalize();
        }

        private void CreateLookAt()
        {
            _view = Matrix.CreateLookAt(Position, Position + _direction, _up);
        }

        public static Vector3 Position { get { return _position; } }
        public static float AspectRatio { get { return _aspectRatio; } set { _aspectRatio = value; } }
        public static Matrix View { get { return _view; } set { _view = value; } }
        public static Matrix Projection { get { return _projection; } set { _projection = value; } }
    }
}