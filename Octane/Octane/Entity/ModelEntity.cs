using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    abstract class ModelEntity : Entity
    {
        private Model _model;

        protected ModelEntity(Model model, Vector3 position, Vector3 rotation)
            : base(position, rotation)
        {
            _model = model;
        }

        public virtual void Draw()
        {
            Matrix[] transforms = new Matrix[_model.Bones.Count];
            _model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh m in _model.Meshes)
            {
                foreach (BasicEffect e in m.Effects)
                {
                    e.EnableDefaultLighting();
                    e.World = transforms[m.ParentBone.Index] * RotationMatrix * Matrix.CreateTranslation(Position);
                    e.View = Camera.View;
                    e.Projection = Camera.Projection;
                }
                m.Draw();
            }
        }

        public Model model { get { return _model; } }
    }
}
