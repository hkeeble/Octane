using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    class Skybox : ModelEntity
    {
        public Skybox(Model model, Vector3 position, Vector3 rotation, float scale)
            : base(model, position, rotation, scale)
        {

        }

        public override void Draw()
        {
            Matrix[] transforms = new Matrix[_model.Bones.Count];
            _model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh m in _model.Meshes)
            {
                foreach (BasicEffect e in m.Effects)
                {
                    e.LightingEnabled = true;
                    e.EmissiveColor = new Vector3(1f, 1f, 1f);
                    e.World = Matrix.CreateScale(_scale) * transforms[m.ParentBone.Index] * RotationMatrix * Matrix.CreateTranslation(Position);
                    e.View = Camera.View;
                    e.Projection = Camera.Projection;
                }
                m.Draw();
            }

            base.Draw();
        }
    }
}
