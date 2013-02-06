using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    class Asteroid : PhysicsEntity
    {
        public Asteroid(Model model, Vector3 position, Vector3 rotation, float speed)
            : base(model, position, rotation)
        {
            _velocity = new Vector3(0, 0, speed);

            //VertexPositionNormalTexture[] data = new VertexPositionNormalTexture[model.Meshes[0].MeshParts[0].VertexBuffer.VertexCount];
            //model.Meshes[0].MeshParts[0].VertexBuffer.GetData<VertexPositionNormalTexture>(data);

            //Random rand = new Random(DateTime.Now.Millisecond);

            //for (int i = 0; i < data.Length; i+=2)
            //{
            //    data[i].Position.X += (float)rand.NextDouble();
            //    //data[i].Position.Y += (float)rand.NextDouble();
            //    //data[i].Position.Z += (float)rand.NextDouble();
            //}

            //this.model.Meshes[0].MeshParts[0].VertexBuffer.SetData<VertexPositionNormalTexture>(data);
        }

        public void SetSpeed(float speed)
        {
            _velocity = new Vector3(0, 0, speed);
        }
    }
}
