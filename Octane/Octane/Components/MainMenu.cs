using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Octane.Components
{
    public class MainMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        GraphicsDevice graphics;

        Vector2 titlePosition;
        Vector2 titleVelocity;
        Texture2D title;

        public MainMenu(Game game)
            : base(game)
        {
            title = game.Content.Load<Texture2D>("2DAssets\\Title");
            graphics = game.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphics);
        }

        public override void Initialize()
        {
            titlePosition = new Vector2(-title.Width, 0);
            titleVelocity = new Vector2(20f, 0.0f);

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(title, titlePosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (titlePosition.X < graphics.Viewport.Width - title.Width - 10)
                titlePosition += titleVelocity;

            base.Update(gameTime);
        }
    }
}
