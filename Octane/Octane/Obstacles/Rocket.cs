using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    class Rocket : Obstacle
    {
        public Rocket(Model model, Vector3 position, Vector3 rotation, float speed)
            : base(model, position, rotation, speed)
        {
            SetRotation(new Vector3(-90, 0, 0));
        }

        public override void Update()
        {
            Rotate(new Vector3(0f, 0f, 1f));

            base.Update();
        }
    }
}
