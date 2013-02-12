using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octane
{
    public class ChargeBar : SpriteEntity
    {
        Color[] _segment;
        Color[] originalData;

        int segmentsFilled = 0;
        int numberOfSegments;

        const int MILLISECONDS_BETWEEN_CHARGE = 30;
        TimeSpan ticksSinceFill = TimeSpan.Zero;
        

        public ChargeBar(Texture2D texture, Vector2 position, GraphicsDevice device)
            : base(texture, position)
        {
            // Assign Original Texture
            originalData = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(originalData);

            _segment = new Color[texture.Height-10];

            for (int i = 0; i < _segment.Length; i++)
                _segment[i] = new Color(0.5f, 0.5f, 0.0f, 1.0f);

            numberOfSegments = texture.Width - 10;
        }

        public void Update(GameTime gameTime, GraphicsDevice device)
        {
            if (!Full)
            {
                ticksSinceFill += gameTime.ElapsedGameTime;

                if (ticksSinceFill >= TimeSpan.FromMilliseconds(MILLISECONDS_BETWEEN_CHARGE))
                {
                    device.Textures[0] = null;
                    _texture.SetData<Color>(0, new Rectangle(segmentsFilled + 5, 5, 1, _segment.Length), _segment, 0, _segment.Length);
                    ticksSinceFill = TimeSpan.Zero;
                    segmentsFilled++;
                }
            }
        }

        public void Empty()
        {
            segmentsFilled = 0;
            _texture.SetData<Color>(originalData);
        }

        public bool Full { get { if(segmentsFilled == numberOfSegments) return true; else return false; } }
    }
}
