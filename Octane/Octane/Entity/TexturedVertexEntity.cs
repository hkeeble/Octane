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
        protected new VertexPositionNormalTexture[] _vertices { get; set; }
        protected Texture2D _texture;

        protected TexturedVertexEntity(Vector3 position, Vector3 rotation, GraphicsDevice graphics, PrimitiveType primitiveType, Texture2D texture)
            : base(position, rotation, graphics, primitiveType)
        {
            _texture = texture;
        }

        protected override void InitBuffers(GraphicsDevice graphics)
        {
            _vertexBuffer = new VertexBuffer(graphics, VertexPositionNormalTexture.VertexDeclaration, _vertices.Length, BufferUsage.None);
            _vertexBuffer.SetData<VertexPositionNormalTexture>(_vertices);
            _indexBuffer = new IndexBuffer(graphics, IndexElementSize.ThirtyTwoBits, _indices.Length, BufferUsage.None);
            _indexBuffer.SetData<int>(_indices);
        }

        public override void Draw(GraphicsDevice device)
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
