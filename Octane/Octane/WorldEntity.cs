using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    class WorldEntity
    {
        private Model _model;
        private Vector3 _position, _rotation;

        public WorldEntity(Model model, Vector3 position, Vector3 rotation)
        {
            _model = model;
            _position = position;
            _rotation = rotation;
        }

        public void Rotate(Vector3 rotation)
        {
            _rotation += rotation;
        }

        public void Translate(Vector3 translation)
        {
            _position += translation;
        }

        public void Draw()
        {
            Matrix[] transforms = new Matrix[_model.Bones.Count];
            _model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh m in _model.Meshes)
            {
                foreach (BasicEffect e in m.Effects)
                {
                    e.EnableDefaultLighting();
                    e.World = transforms[m.ParentBone.Index] * RotationMatrix * Matrix.CreateTranslation(_position);
                    e.View = Camera.View;
                    e.Projection = Camera.Projection;
                }
                m.Draw();
            }
        }

        private Matrix RotationMatrix { get { return (Matrix.CreateRotationX(_rotation.X) * Matrix.CreateRotationY(_rotation.Y) * Matrix.CreateRotationZ(_rotation.Z)); } }
        
        public Vector3 Position { get { return _position; } }
        public Vector3 Rotation { get { return _rotation; } }
        public Model model { get { return _model; } }
    }
}
