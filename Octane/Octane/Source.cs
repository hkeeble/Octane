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
using Octane.Components;

namespace Octane
{
    /// <summary>
    /// Struct that contains VertexDeclarations for Position, Color and Normal
    /// </summary>
    public struct VertexPositionColorNormal
    {
        public Vector3 Position;
        public Color Color;
        public Vector3 Normal;

        public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
        (
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(sizeof(float) * 3, VertexElementFormat.Color, VertexElementUsage.Color, 0),
            new VertexElement(sizeof(float) * 3 + 4, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
        );
    }

    public class Source : Microsoft.Xna.Framework.Game
    {
        #region Declarations
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Camera camera;

        Ingame inGame;
        MainMenu mainMenu;

        #endregion

        public Source()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            camera = new Camera(this, graphics.GraphicsDevice, new Vector3(0, 1, 4), new Vector3(0, 1, -1), Vector3.Up);

            inGame = new Ingame(this);
            mainMenu = new MainMenu(this);

            Components.Add(new InputHandler(this));
            Components.Add(inGame);
            Components.Add(mainMenu);
            Components.Add(camera);

            mainMenu.Enabled = true;
            inGame.Enabled = false;
            inGame.Visible = false;

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (mainMenu.Enabled == true)
            {
                if (InputHandler.GamePadState.Buttons.A == ButtonState.Pressed)
                {
                    mainMenu.Enabled = false;
                    mainMenu.Visible = false;
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                    inGame.Enabled = true;
                    inGame.Visible = true;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            

            base.Draw(gameTime);
        }
    }
}
