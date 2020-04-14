using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloCube
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HelloCube : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Vector3 camTarget;
        private Vector3 camPos;
        private Matrix mtxProj, mtxView, mtxWorld;

        private Model model;

        private bool orbit;

        public HelloCube()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.camTarget = Vector3.Zero;
            this.camPos = new Vector3(0f, 0f, -10f);

            mtxProj = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60f),
                this.GraphicsDevice.Viewport.AspectRatio, 1f, 1000f);
            mtxView = Matrix.CreateLookAt(camPos, camTarget, Vector3.Up);
            mtxWorld = Matrix.CreateWorld(camTarget, Vector3.Forward, Vector3.Up);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            model = Content.Load<Model>("SampleCube");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || Keyboard.GetState().IsKeyDown(
                    Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camPos.X -= 0.1f;
                camTarget.X -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camPos.X += 0.1f;
                camTarget.X += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                camPos.Y -= 0.1f;
                camTarget.Y -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                camPos.Y += 0.1f;
                camTarget.Y += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            {
                camPos.Z += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                camPos.Z -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                orbit = !orbit;
            }

            mtxView = Matrix.CreateLookAt(camPos, camTarget, Vector3.Up);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.AmbientLightColor = new Vector3(100f,100,100);
                    effect.View = mtxView;
                    effect.Projection = mtxProj;
                    effect.World = mtxWorld;
                }
                mesh.Draw();
            }

            base.Draw(gameTime);
        }
    }
}
