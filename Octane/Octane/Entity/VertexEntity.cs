using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    class VertexEntity : Entity
    {
        public enum Shape
        {
            Triangle = 3,
            Square = 4,
            Cube = 6
        }

        private VertexPositionColor[] _vertices;
        private VertexBuffer _vertexBuffer;
        private BasicEffect _effect;
        private PrimitiveType _primType;

        public VertexEntity(Vector3 position, Vector3 rotation, Shape shape, Vector3 scale, Color color, GraphicsDevice graphics) : base (position, rotation)
        {
            _vertices = new VertexPositionColor[(int)shape];

            if(shape == Shape.Triangle)
            {
                _vertices[0] = new VertexPositionColor(new Vector3(0, 1, 0), color);
                _vertices[1] = new VertexPositionColor(new Vector3(1, -1, 0), color);
                _vertices[2] = new VertexPositionColor(new Vector3(-1, -1, 0), color);
                _primType = PrimitiveType.TriangleStrip;
            }

            if (shape == Shape.Square)
            {
                _vertices[0] = new VertexPositionColor(new Vector3(-1, 1, 0), color);
                _vertices[1] = new VertexPositionColor(new Vector3(1, 1, 0), color);
                _vertices[2] = new VertexPositionColor(new Vector3(-1, -1, 0), color);
                _vertices[3] = new VertexPositionColor(new Vector3(1, -1, 0), color);
                _primType = PrimitiveType.TriangleStrip;
            }

            for (int i = 0; i < _vertices.Length; i++)
            {
                _vertices[i].Position *= scale;
            }

            _vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColor), _vertices.Length, BufferUsage.None);
            _vertexBuffer.SetData<VertexPositionColor>(_vertices);

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
                graphics.DrawUserPrimitives<VertexPositionColor>(_primType, _vertices, 0, 2);
            }
        }
    }
}
