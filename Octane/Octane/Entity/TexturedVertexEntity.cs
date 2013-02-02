using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    abstract class TexturedVertexEntity : VertexEntity
    {
        protected virtual VertexPositionNormalTexture[] _vertices;
        private Texture2D _texture;

        protected TexturedVertexEntity(Vector3 position, Vector3 rotation, GraphicsDevice graphics, PrimitiveType primitiveType, Texture2D texture)
            : base(position, rotation, graphics, primitiveType)
        {
            _texture = texture;
        }

        public virtual void Draw(GraphicsDevice device)
        {
            _effect.World = RotationMatrix * Matrix.CreateTranslation(Position);
            _effect.View = Camera.View;
            _effect.Projection = Camera.Projection;
            _effect.VertexColorEnabled = true;
            _effect.EnableDefaultLighting();
            _effect.TextureEnabled = true;
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
