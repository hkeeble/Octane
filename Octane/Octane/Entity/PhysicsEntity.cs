using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    abstract class PhysicsEntity : ModelEntity
    {
        protected Vector3 _velocity;

        protected PhysicsEntity(Model model, Vector3 position, Vector3 rotation)
            : base(model, position, rotation)
        {
            _velocity = Vector3.Zero;
        }

        public virtual void Update()
        {
            Translate(_velocity);
        }
    }
}
