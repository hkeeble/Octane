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
        private int _terrainWidth;
        private int _terrainHeight;
        private float[,] _heightData;
        private Vector3 _scale;

        private bool _isWater = false;

        public Terrain(int width, int height, GraphicsDevice device, Vector3 position, Vector3 rotation, Vector3 scale) : base(position, rotation, device, PrimitiveType.TriangleList)
        {
            _terrainWidth = width;
            _terrainHeight = height;

            _scale = scale;

            GenerateHeightData();
            GenerateVertices();

            _effect = new BasicEffect(device);

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

            CalculateNormals();

            _primCount = _indices.Length / 3;

            InitBuffers(device);
        }

        private void GenerateHeightData()
        {
            Random random = new Random(DateTime.Now.Millisecond);

            _heightData = new float[_terrainWidth, _terrainHeight];

            for (int x = 0; x < _terrainWidth; x++)
                for (int y = 0; y < _terrainHeight; y++)
                {
                    _heightData[x, y] = (float)random.NextDouble();

                    if(!_isWater)
                        if (_heightData[x, y] > 0.95f)
                            _heightData[x, y] = (float)random.NextDouble() * 4;
                    else
                        if (_heightData[x, y] > 0.5f)
                            _heightData[x, y] = (float)random.NextDouble() * 2;
                }
        }

        private void GenerateVertices()
        {
            _vertices = new VertexPositionColorNormal[_terrainHeight * _terrainWidth];

            for (int x = 0; x < _terrainWidth; x++)
            {
                for (int y = 0; y < _terrainHeight; y++)
                {
                    _vertices[x + y * _terrainWidth].Position = new Vector3(x, _heightData[x, y], -y) * _scale;
                    _vertices[x + y * _terrainWidth].Normal = new Vector3(0, 0, 0);
                    if (!_isWater)
                    {
                        if (_heightData[x, y] > 3)
                            _vertices[x + y * _terrainWidth].Color = Color.LightGray;
                        else
                            _vertices[x + y * _terrainWidth].Color = Color.Green;
                    }
                    else
                    {
                            _vertices[x + y * _terrainWidth].Color = Color.DeepSkyBlue;
                    }
                }
            }
        }

        private void CalculateNormals()
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

        public void Reset(bool isWater)
        {
            _isWater = isWater;

            GenerateHeightData();
            GenerateVertices();
            CalculateNormals();
        }

        public bool IsWater { get { return _isWater; } }
    }
}
