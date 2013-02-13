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
        protected Model _model;
        protected float _scale;

        protected ModelEntity(Model model, Vector3 position, Vector3 rotation, float scale)
            : base(position, rotation)
        {
            _model = model;
            _scale = scale;
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
                    e.World = Matrix.CreateScale(_scale) * transforms[m.ParentBone.Index] * RotationMatrix * Matrix.CreateTranslation(Position);
                    e.View = Camera.View;
                    e.Projection = Camera.Projection;
                }
                m.Draw();
            }
        }

        public BoundingSphere BoundingSphere
        {
            get
            {
                BoundingSphere BSphere = _model.Meshes[0].BoundingSphere;
                BSphere.Center += Position;
                return BSphere;
            }
        }

        public Model model { get { return _model; } }
    }
}
