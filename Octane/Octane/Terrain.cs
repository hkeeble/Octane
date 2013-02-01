using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    class Terrain
    {
        private VertexPositionColorNormal[] _vertices;
        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;
        private BasicEffect _effect;
        private Vector3 _rotation;
        private int[] _indices;
        private Color _color;

        private int _terrainWidth;
        private int _terrainHeight;
        private float[,] _heightData;

        public Terrain(int width, int height, float heightMultiplier, Color color, GraphicsDevice device)
        {
            _terrainWidth = width;
            _terrainHeight = height;
            _color = color;
            LoadHeightData(heightMultiplier);
            SetUpVertices(device);
            SetUpIndices(device);
            CalculateNormals();
        }

        public void LoadHeightData(float heightMultiplier)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            _heightData = new float[_terrainWidth, _terrainHeight];

            for (int x = 0; x < _terrainWidth; x++)
                for (int y = 0; y < _terrainHeight; y++)
                    _heightData[x, y] = (float)random.NextDouble() * heightMultiplier;
        }

        public void SetUpVertices(GraphicsDevice device)
        {
            _rotation = new Vector3(0, 0, 0);
            _vertices = new VertexPositionColorNormal[_terrainHeight * _terrainWidth];

            for (int x = 0; x < _terrainWidth; x++)
            {
                for (int y = 0; y < _terrainHeight; y++)
                {
                    _vertices[x + y * _terrainWidth].Position = new Vector3(x, _heightData[x,y], -y);
                    _vertices[x + y * _terrainWidth].Color = _color;
                    _vertices[x + y * _terrainWidth].Normal = new Vector3(0, 0, 0);
                }
            }

            _effect = new BasicEffect(device);

            _vertexBuffer = new VertexBuffer(device, VertexPositionColorNormal.VertexDeclaration, _vertices.Length, BufferUsage.None);
            _vertexBuffer.SetData<VertexPositionColorNormal>(_vertices);

            //_indices = new IndexBuffer(device, typeof(int), 
        }

        public void CalculateNormals()
        {

            for (int i = 0; i < _indices.Length / 3; i++)
            {
                int index1 = _indices[i * 3];
                int index2 = _indices[i * 3 + 1];
                int index3 = _indices[i * 3 + 2];

                Vector3 side1 = _vertices[index1].Position - _vertices[index2].Position;
                Vector3 side2 = _vertices[index1].Position - _vertices[index3].Position;
                Vector3 normal = Vector3.Cross(side1, side2);

                _vertices[index1].Normal += normal;
                _vertices[index2].Normal += normal;
                _vertices[index3].Normal += normal;
            }

            for (int i = 0; i < _vertices.Length; i++)
                _vertices[i].Normal.Normalize();
        }

        public void SetUpIndices(GraphicsDevice device)
        {
            _indices = new int[(_terrainWidth - 1) * (_terrainHeight - 1) * 6];
            int counter = 0;
            for (int y = 0; y < _terrainHeight - 1; y++)
            {
                for (int x = 0; x < _terrainWidth - 1; x++)
                {
                    int lowerLeft = x + y * _terrainWidth;
                    int lowerRight = (x + 1) + y * _terrainWidth;
                    int topLeft = x + (y + 1) * _terrainWidth;
                    int topRight = (x + 1) + (y + 1) * _terrainWidth;

                    _indices[counter++] = topLeft;
                    _indices[counter++] = lowerRight;
                    _indices[counter++] = lowerLeft;

                    _indices[counter++] = topLeft;
                    _indices[counter++] = topRight;
                    _indices[counter++] = lowerRight;
                }
            }

            _indexBuffer = new IndexBuffer(device, IndexElementSize.ThirtyTwoBits, _indices.Length, BufferUsage.None);
            _indexBuffer.SetData<int>(_indices);
        }

        public void Draw(GraphicsDevice device)
        {
            _effect.World = Matrix.Identity;
            _effect.View = Camera.View;
            _effect.Projection = Camera.Projection;
            _effect.VertexColorEnabled = true;
            _effect.EnableDefaultLighting();
            device.SetVertexBuffer(_vertexBuffer);
            device.Indices = _indexBuffer;

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _vertices, 0, _vertices.Length, _indices, 0, _indices.Length / 3, VertexPositionColorNormal.VertexDeclaration);
            }
        }

        private Matrix RotationMatrix { get { return (Matrix.CreateRotationX(_rotation.X) * Matrix.CreateRotationY(_rotation.Y) * Matrix.CreateRotationZ(_rotation.Z)); } }
    }
}
