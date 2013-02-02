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
        protected virtual VertexPositionColorNormal[] _vertices { get; set;}
        protected VertexBuffer _vertexBuffer;
        protected int[] _indices;
        protected IndexBuffer _indexBuffer;
        protected BasicEffect _effect;
        protected PrimitiveType _primType;
        protected int _primCount;

        protected VertexEntity(Vector3 position, Vector3 rotation, GraphicsDevice graphics, PrimitiveType primitiveType)
            : base(position, rotation)
        {
            _effect = new BasicEffect(graphics);
            _primType = primitiveType;
        }

        public virtual void Draw(GraphicsDevice device)
        {
            _effect.World = RotationMatrix * Matrix.CreateTranslation(Position);
            _effect.View = Camera.View;
            _effect.Projection = Camera.Projection;
            _effect.VertexColorEnabled = true;
            _effect.EnableDefaultLighting();
            device.SetVertexBuffer(_vertexBuffer);
            device.Indices = _indexBuffer;

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserIndexedPrimitives(_primType, _vertices, 0, _vertices.Length, _indices, 0, _primCount, VertexPositionColorNormal.VertexDeclaration);
            }
        }
    }
}
