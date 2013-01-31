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
        private Vector3 _position;
        private float _aspectRatio;
        private Matrix _view;
        private Matrix _projection;
        private Vector3 _direction;
        private Vector3 _up;

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

            Vector3 rotation = Vector3.Zero;

            if (InputHandler.KeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                _position -= _direction * 0.5f;
            if (InputHandler.KeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                _position += _direction * 0.5f;
            if (InputHandler.KeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                rotation.Y = 0.01f;
            if (InputHandler.KeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                rotation.Y = -0.01f;

            Rotate(rotation);

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

        public Vector3 Position { get { return _position; } }
        public float AspectRatio { get { return _aspectRatio; } set { _aspectRatio = value; } }
        public Matrix View { get { return _view; } set { _view = value; } }
        public Matrix Projection { get { return _projection; } set { _projection = value; } }
    }
}