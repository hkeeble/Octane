using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    public class SpriteEntity
    {
        protected Vector2 _position;
        protected Texture2D _texture;

        public SpriteEntity(Texture2D texture, Vector2 position)
        {
            _position = position;
            _texture = texture;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }

        public Vector2 Position { get { return _position; } }
    }
}
