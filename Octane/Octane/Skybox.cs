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
        public Skybox(Vector3 position, Vector3 rotation, Vector3 scale, GraphicsDevice graphics, Texture2D texture)
            : base(position, rotation, graphics, PrimitiveType.TriangleStrip, texture)
        {
            _vertices = new VertexPositionNormalTexture[4];

            _vertices[0].Position = new Vector3(-1.0f, 1.0f, 0.0f) * scale;
            _vertices[1].Position = new Vector3(1.0f, 1.0f, 0.0f) * scale;
            _vertices[2].Position = new Vector3(-1.0f, -1.0f, 0.0f) * scale;
            _vertices[3].Position = new Vector3(1.0f, -1.0f, 0.0f) * scale;

            _vertices[0].Normal = new Vector3(0.0f, 1.0f, 0.0f);
            _vertices[1].Normal = new Vector3(0.0f, 1.0f, 0.0f);
            _vertices[2].Normal = new Vector3(0.0f, 1.0f, 0.0f);
            _vertices[3].Normal = new Vector3(0.0f, 1.0f, 0.0f);

            _vertices[0].TextureCoordinate = new Vector2(0.0f, 0.0f);
            _vertices[1].TextureCoordinate = new Vector2(1.0f, 0.0f);
            _vertices[2].TextureCoordinate = new Vector2(0.0f, 1.0f);
            _vertices[3].TextureCoordinate = new Vector2(1.0f, 1.0f);

            _indices = new int[4];

            for (int i = 0; i < _indices.Length; i++)
                _indices[i] = i;

            _primCount = 2;
            _primType = PrimitiveType.TriangleStrip;

            InitBuffers(graphics);
        }

        public override void Draw(GraphicsDevice device)
        {
            _effect.World = RotationMatrix * Matrix.CreateTranslation(Position);
            _effect.View = Camera.View;
            _effect.Projection = Camera.Projection;
            _effect.TextureEnabled = true;
            _effect.Texture = _texture;
            device.SetVertexBuffer(_vertexBuffer);
            device.Indices = _indexBuffer;

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserIndexedPrimitives(_primType, _vertices, 0, _vertices.Length, _indices, 0, _primCount, VertexPositionNormalTexture.VertexDeclaration);
            }
        }
    }
}
