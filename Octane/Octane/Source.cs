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
        public static Camera camera;
        Player player;

        Terrain terrain;

        public Source()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            camera = new Camera(this, graphics.GraphicsDevice, new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up);

            Components.Add(new InputHandler(this));
            Components.Add(camera);

            RasterizerState stat = new RasterizerState();
            stat.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = stat;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(Content.Load<Model>("Models\\cube"), new Vector3(0, 0, 2), Vector3.Zero);
            terrain = new Terrain(new Vector3(0f, -0.5f, 0f), Vector3.Zero, new Vector3(2, 2, 2), GraphicsDevice, 1, 0, Color.LawnGreen);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //player.Update();

            //test.Rotate(new Vector3(0, 0, 0.01f));

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

            terrain.Draw(GraphicsDevice);
            //player.Draw();
            base.Draw(gameTime);
        }
    }
}
