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

namespace Octane
{
    public class Source : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        WorldEntity Plane;
        Player player;

        public Source()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Components.Add(new InputHandler(this));

            Camera.AspectRatio = GraphicsDevice.Viewport.AspectRatio;
            Camera.Translate(new Vector3(0.0f, 8000.0f, 500.0f));
            Camera.View = Matrix.CreateLookAt(Camera.Position, Vector3.Zero, Vector3.Up);
            Camera.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), Camera.AspectRatio, 1.0f, 10000.0f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(Content.Load<Model>("Models\\p1_wedge"), Vector3.Zero, Vector3.Zero);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            player.Update();

            if (Camera.Position.Y > -50f)
            {
                Camera.Translate(new Vector3(0.0f, -25.0f, 20.0f));
                Camera.View = Matrix.CreateLookAt(Camera.Position, player.Position, Vector3.Up);
            }
            else if (player.Position.Y > -1500.0f)
                player.Translate(new Vector3(0.0f, -10.0f, 0.0f));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            player.Draw();

            base.Draw(gameTime);
        }
    }
}
