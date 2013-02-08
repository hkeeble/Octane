using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    class Obstacle : PhysicsEntity
    {
        public Obstacle(Model model, Vector3 position, Vector3 rotation, float speed)
            : base(model, position, rotation)
        {
            _velocity = new Vector3(0, 0, speed);
        }

        public void SetSpeed(float speed)
        {
            _velocity = new Vector3(0, 0, speed);
        }


    }
}
