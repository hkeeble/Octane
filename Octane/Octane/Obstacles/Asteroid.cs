using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    class Asteroid : Obstacle
    {
        public Asteroid(Model model, Vector3 position, Vector3 rotation, float speed)
            : base(model, position, rotation, speed, 1f)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            SetRotation(new Vector3(rand.Next(0, 90), rand.Next(0, 90), rand.Next(0, 90)));
        }

        public override void Update()
        {
            Rotate(new Vector3(0f, -3f, 0f));

            base.Update();
        }
    }
}
