using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    public static class Camera
    {
        private static Vector3 _position;
        private static float _aspectRatio;
        private static Matrix _view;
        private static Matrix _projection;

        public static void Translate(Vector3 translation)
        {
            _position += translation;
        }

        public static Vector3 Position { get { return _position; } }
        public static float AspectRatio { get { return _aspectRatio; } set { _aspectRatio = value; } }
        public static Matrix View { get { return _view; } set { _view = value; } }
        public static Matrix Projection { get { return _projection; } set { _projection = value; } }
    }
}