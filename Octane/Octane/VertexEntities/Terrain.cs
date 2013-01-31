using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    class Terrain : VertexEntity
    {
        private Color _color;
        private float[,] heightData;
        private int width;
        private int height;


        public Terrain(Vector3 position, Vector3 rotation, Vector3 scale, GraphicsDevice graphics, float maxHeight, int subdivisions, Color color) : base (position, rotation, scale, graphics)
        {
            //_vertices = new VertexPositionColor[(int)Math.Pow(subdivisions+1, 3.0)];

            Vector3 frontLeft = new Vector3(-1, 0, 1) * scale;
            Vector3 frontRight = new Vector3(1, 0, 1) * scale;
            Vector3 backLeft = new Vector3(-1, 0, -1) * scale;
            Vector3 backRight = new Vector3(1, 0, -1) * scale;

            _color = color;

            SetUpVertices();

            _vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColor), _vertices.Length, BufferUsage.None);
            _vertexBuffer.SetData<VertexPositionColor>(_vertices);
        }

        public new void Draw(GraphicsDevice graphics)
        {
            graphics.SetVertexBuffer(_vertexBuffer);
            _effect.World = RotationMatrix * Matrix.CreateTranslation(Position);
            _effect.View = Source.camera.View;
            _effect.Projection = Source.camera.Projection;
            _effect.VertexColorEnabled = true;

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, _vertices, 0, tris.Count);
            }
        }

        private void SetUpVertices()
        {

        }
    }
}