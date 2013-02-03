﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    abstract class Entity
    {
        private Vector3 _position, _rotation;

        public Entity(Vector3 position, Vector3 rotation)
        {
            _position = position;
            _rotation = rotation;
        }

        public void Rotate(Vector3 rotation)
        {
            _rotation += rotation;
        }

        public void SetRotation(Vector3 rotation)
        {
            rotation = new Vector3(MathHelper.ToRadians(rotation.X), MathHelper.ToRadians(rotation.Y), MathHelper.ToRadians(rotation.Z));
            _rotation = rotation;
        }

        public void Translate(Vector3 translation)
        {
            _position += translation;
        }

        public void SetPosition(Vector3 position)
        {
            _position = position;
        }

        protected Matrix RotationMatrix { get { return (Matrix.CreateRotationX(_rotation.X) * Matrix.CreateRotationY(_rotation.Y) * Matrix.CreateRotationZ(_rotation.Z)); } }
        public Vector3 Position { get { return _position; } }
        public Vector3 Rotation { get { return _rotation; } }
    }
}
