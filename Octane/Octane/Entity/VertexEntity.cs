using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    abstract class VertexEntity : Entity
    {
        protected VertexPositionColor[] _vertices;
        protected VertexBuffer _vertexBuffer;
        protected BasicEffect _effect;

        public VertexEntity(Vector3 position, Vector3 rotation, Vector3 scale, GraphicsDevice graphics) : base (position, rotation)
        {
            _effect = new BasicEffect(graphics);
        }

        public void Draw(GraphicsDevice graphics)
        {
            graphics.SetVertexBuffer(_vertexBuffer);
            _effect.World = RotationMatrix * Matrix.CreateTranslation(Position);
            _effect.View = Camera.View;
            _effect.Projection = Camera.Projection;
            _effect.VertexColorEnabled = true;

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, _vertices, 0, 12);
            }
        }
    }
}
