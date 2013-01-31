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

        Player player;

        VertexEntity test;

        public Source()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Components.Add(new InputHandler(this));

            Camera.AspectRatio = GraphicsDevice.Viewport.AspectRatio;
            Camera.Translate(new Vector3(0, 0, 5));
            Camera.View = Matrix.CreateLookAt(Camera.Position, Vector3.Zero, Vector3.Up);
            Camera.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), Camera.AspectRatio, 1.0f, 10000.0f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(Content.Load<Model>("Models\\cube"), new Vector3(0, 0, 2), Vector3.Zero);
            test = new VertexEntity(new Vector3(0, -2, 0), new Vector3(45f, 0, 0), VertexEntity.Shape.Square, new Vector3(4, 4, 4), Color.LawnGreen, GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            player.Update();

            //test.Rotate(new Vector3(0, 0, 0.01f));

            if(InputHandler.KeyDown(Keys.A))
                Camera.Translate(new Vector3(0, 0, 0.1f));

            //if (Camera.Position.Y > -3f)
            //{
            //    Camera.Translate(new Vector3(0.0f, -0.1f, 0.05f));
            //    Camera.View = Matrix.CreateLookAt(Camera.Position, player.Position, Vector3.Up);
            //}
            //else if (player.Position.Y > -5f)
            //    player.Translate(new Vector3(0.0f, -0.1f, 0.0f));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            test.Draw(GraphicsDevice);
            player.Draw();
            base.Draw(gameTime);
        }
    }
}
