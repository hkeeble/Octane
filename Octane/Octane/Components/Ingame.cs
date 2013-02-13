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
    public class Ingame : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        Random rand;

        Player player;

        Skybox skyBox;

        Song bgMusic;
        SoundEffect rocket;

        public static ChargeBar chargeBar;
        List<SpriteEntity> HUD;

        List<Obstacle> obstacles = new List<Obstacle>();

        Terrain[] terrainBlocks;
        Vector2 terrainBlockSize;
        Vector2 terrainOffset;
        Vector3 nextTerrainBlockPosition;
        bool overSea = false;
        TimeSpan waterCheckTimer = TimeSpan.Zero;
        const int SECONDS_BETWEEN_WATER_CHECKS = 15;

        TimeSpan obstacleSpawnTimer = TimeSpan.Zero;
        double secondsBetweenObstacles = 1;

        public Ingame(Game game)
            : base(game)
        {
            skyBox = new Skybox(game.Content.Load<Model>("Models\\Skybox"), Vector3.Zero, Vector3.Zero, 200f);

            rocket = game.Content.Load<SoundEffect>("Sounds\\rocket");
            bgMusic = game.Content.Load<Song>("Sounds\\music");

            player = new Player(game.Content.Load<Model>("Models\\ship"), Vector3.Zero, new Vector3(90, 0, 0), rocket);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            InitTerrain();

            HUD = new List<SpriteEntity>();
            chargeBar = new ChargeBar(Game.Content.Load<Texture2D>("2DAssets\\ChargeBar"), new Vector2(10, Game.Window.ClientBounds.Height - 90), GraphicsDevice);
            HUD.Add(chargeBar);

            base.LoadContent();
        }

        public override void Initialize()
        {
            rand = new Random(DateTime.Now.Millisecond);
            base.Initialize();
            MediaPlayer.Play(bgMusic);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            for (int i = 0; i < terrainBlocks.Length; i++)
                terrainBlocks[i].Draw(GraphicsDevice);
            skyBox.Draw();
            spriteBatch.Begin();
            DrawHUD();
            spriteBatch.End();

            GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            player.Draw();

            if (obstacles.Count > 0)
                foreach (Obstacle o in obstacles)
                    o.Draw();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.GamePadState.Buttons.Back == ButtonState.Pressed)
                Game.Exit();

            if (Camera.Position.Y > 1.5f)
            {
                Camera.Move(new Vector3(0, -0.5f, 0.5f));
                Camera.Target = new Vector3(Camera.Target.X, Camera.Target.Y + 0.01f, Camera.Target.Z + 0.005f);
                player.SetPosition(new Vector3(Camera.Position.X, Camera.Position.Y, Camera.Position.Z - 3));
                player.SetRotation(new Vector3(180, 0, 0));
            }
            else
            {
                if(player.CurrentSpeed > 3)
                    Camera.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f + player.CurrentSpeed * 2), Camera.AspectRatio, 1, 1000);

                chargeBar.Update(gameTime, GraphicsDevice);

                secondsBetweenObstacles = 1 / player.CurrentSpeed;
                waterCheckTimer += gameTime.ElapsedGameTime;
                obstacleSpawnTimer += gameTime.ElapsedGameTime;

                if (obstacleSpawnTimer > TimeSpan.FromSeconds(secondsBetweenObstacles))
                    SpawnObstacles();

                if (!player.Dead)
                    player.Update();
                UpdateTerrain();

                for (int i = 0; i < obstacles.Count; i++)
                {
                    obstacles[i].Update();
                    obstacles[i].SetSpeed(player.CurrentSpeed / 2);

                    if (obstacles[i].Position.Z < player.Position.Z)
                    {
                        BoundingSphere bSphere = player.model.Meshes[0].BoundingSphere;
                        bSphere.Center += player.Position;

                        if (player.BoundingSphere.Intersects(obstacles[i].BoundingSphere))
                        {
                            obstacles.Remove(obstacles[i]);
                            player.Dead = true;
                        }
                    }
                }
            }
            if (player.Dead)
            {
                if (player.Position.Y > -2)
                {
                    if (player.Rotation.X > 90)
                        player.Rotate(new Vector3(-5.0f, 0.0f, 0.0f));
                    else
                        player.Rotate(new Vector3(0f, -25f, 0f));
                    player.Translate(new Vector3(0f, -0.05f, 0f));
                }
            }

            base.Update(gameTime);
        }

        private void InitTerrain()
        {
            terrainOffset = new Vector2(-180, -10);
            nextTerrainBlockPosition = new Vector3(terrainOffset, -700);

            terrainBlocks = new Terrain[2];
            terrainBlockSize = new Vector2(300, 500);
            terrainBlocks[0] = new Terrain((int)terrainBlockSize.X, (int)terrainBlockSize.Y, GraphicsDevice, new Vector3(terrainOffset, 0), Vector3.Zero, new Vector3(1.5f, 1.0f, 1.5f));
            terrainBlocks[1] = new Terrain((int)terrainBlockSize.X, (int)terrainBlockSize.Y, GraphicsDevice, nextTerrainBlockPosition, Vector3.Zero, new Vector3(1.5f, 1.0f, 1.5f));
        }

        private void UpdateTerrain()
        {
            if (waterCheckTimer > TimeSpan.FromSeconds(SECONDS_BETWEEN_WATER_CHECKS))
            {
                if (rand.Next(10) > 5)
                    overSea = true;
                else
                    overSea = false;

                waterCheckTimer = TimeSpan.Zero;
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

        private void SpawnObstacles()
        {
            rand = new Random(DateTime.Now.Millisecond);

            if (rand.Next(2) == 1)
                obstacles.Add(new Asteroid(Game.Content.Load<Model>("Models\\asteroid"), new Vector3(rand.Next(-10, 10), rand.Next(1, 5), player.Position.Z - 100), Vector3.Zero, player.CurrentSpeed / 2));
            else
                obstacles.Add(new Rocket(Game.Content.Load<Model>("Models\\rocket"), new Vector3(rand.Next(-10, 10), rand.Next(1, 5), player.Position.Z - 100), Vector3.Zero, player.CurrentSpeed / 2));

            obstacleSpawnTimer = TimeSpan.Zero;
        }

        private void DrawHUD()
        {
            foreach (SpriteEntity s in HUD)
            {
                s.Draw(spriteBatch);
            }
            GraphicsDevice.Textures[0] = null;
        }
    }
}