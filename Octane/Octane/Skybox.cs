using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    class Skybox : TexturedVertexEntity
    {
        public Skybox(Vector3 position, Vector3 rotation, GraphicsDevice graphics, Texture2D texture)
            : base(position, rotation, graphics, PrimitiveType.TriangleStrip, texture)
        {
            _vertices = new VertexPositionNormalTexture[4];
        }
    }
}
