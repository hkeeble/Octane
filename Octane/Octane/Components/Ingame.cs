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
    public class Ingame : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Random rand;

        Player player;

        Skybox skyBox;

        Terrain[] terrainBlocks;
        Vector2 terrainBlockSize;
        Vector2 terrainOffset;
        Vector3 nextTerrainBlockPosition;
        bool overSea = false;
        const int timeBetweenWaterTest = 600;
        int timeSinceWaterTest = timeBetweenWaterTest;

        public Ingame(Game game)
            : base(game)
        {
            player = new Player(game.Content.Load<Model>("Models\\ship"), Vector3.Zero, new Vector3(90, 0, 0));
            skyBox = new Skybox(new Vector3(0, 5, -700), Vector3.Zero, new Vector3(550, 550, 550), game.GraphicsDevice, game.Content.Load<Texture2D>("Textures\\Skybox"));
        }

        protected override void LoadContent()
        {
            InitTerrain();
            base.LoadContent();
        }

        public override void Initialize()
        {
            rand = new Random(DateTime.Now.Millisecond);
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            for (int i = 0; i < terrainBlocks.Length; i++)
                terrainBlocks[i].Draw(GraphicsDevice);

            player.Draw();
            skyBox.Draw(GraphicsDevice);

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceWaterTest--;

            if (Camera.Position.Y > 1)
            {
                Camera.Move(new Vector3(0, -0.5f, 0.5f));
                Camera.Target = new Vector3(Camera.Target.X, Camera.Target.Y + 0.03f, Camera.Target.Z + 0.02f);
                player.SetPosition(new Vector3(Camera.Position.X, Camera.Position.Y, Camera.Position.Z - 3));
                player.SetRotation(new Vector3(180, 0, 0));
            }
            else
            {
                player.Update();
                UpdateTerrain();
            }

            base.Update(gameTime);
        }

        private void InitTerrain()
        {
            terrainOffset = new Vector2(-80, -10);
            nextTerrainBlockPosition = new Vector3(terrainOffset, -700);

            terrainBlocks = new Terrain[2];
            terrainBlockSize = new Vector2(100, 500);
            terrainBlocks[0] = new Terrain((int)terrainBlockSize.X, (int)terrainBlockSize.Y, GraphicsDevice, new Vector3(terrainOffset, 0), Vector3.Zero, new Vector3(1.5f, 1.0f, 1.5f));
            terrainBlocks[1] = new Terrain((int)terrainBlockSize.X, (int)terrainBlockSize.Y, GraphicsDevice, nextTerrainBlockPosition, Vector3.Zero, new Vector3(1.5f, 1.0f, 1.5f));
        }

        private void UpdateTerrain()
        {
            if (timeSinceWaterTest <= 0)
            {
                if (rand.Next(10) > 5)
                    overSea = true;
                else
                    overSea = false;

                timeSinceWaterTest = timeBetweenWaterTest;
            }

            for (int i = 0; i < terrainBlocks.Length; i++)
            {
                terrainBlocks[i].Translate(new Vector3(0, 0, player.CurrentSpeed));
                if (terrainBlocks[i].Position.Z > 700)
                {
                    terrainBlocks[i].Reset(overSea);
                    terrainBlocks[i].SetPosition(nextTerrainBlockPosition);
                }
            }
        }
    }
}
