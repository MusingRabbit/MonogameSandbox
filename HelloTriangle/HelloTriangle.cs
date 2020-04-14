using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloTriangle
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HelloTriangle : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Camera
        private Vector3 camTarget;
        private Vector3 camPos;
        private Matrix mtxProjection;
        private Matrix mtxView;
        private Matrix mtxWorld;

        //BasicEffect for rendering
        private BasicEffect basicEffect;

        //Geometry Data
        private VertexPositionColor[] vertices;
        private VertexBuffer vertexBuffer;

        public HelloTriangle()
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
            base.Initialize();

            camTarget = new Vector3(0.0f, 0.0f, 0.0f);
            camPos = new Vector3(0.0f,0.0f,-100.0f);

            mtxProjection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f),
                GraphicsDevice.DisplayMode.AspectRatio, 1f, 1000f);
            mtxView = Matrix.CreateLookAt(camPos, camTarget, Vector3.Up);

            mtxWorld = Matrix.CreateWorld(camTarget, Vector3.Forward, Vector3.Up);

            basicEffect = new BasicEffect(GraphicsDevice)
            {
                Alpha = 1f,
                VertexColorEnabled = true,  //In order to see vertex colour - this must be enabled
                LightingEnabled = false     //Lighting requires normal information which VertexPositionColor does not have
            };

            

            


            //Triangle Mesh Data
            vertices = new VertexPositionColor[3];
            vertices[0] = new VertexPositionColor(new Vector3(0,20,0), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(-20, -20, 0), Color.Green);
            vertices[2] = new VertexPositionColor(new Vector3(20, -20, 0), Color.Blue);

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keyState = Keyboard.GetState();
            
            if (keyState.IsKeyDown(Keys.Left))
            {
                if (keyState.IsKeyDown(Keys.RightControl))
                {
                    var rotMtx = Matrix.CreateRotationY(MathHelper.ToRadians(1f));
                    camPos = Vector3.Transform(camPos, rotMtx);
                }
                else
                {
                    camPos.X += 1f;
                    camTarget.X += 1f;
                }
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                if (keyState.IsKeyDown(Keys.RightControl))
                {
                    var rotMtx = Matrix.CreateRotationY(MathHelper.ToRadians(-1f));
                    camPos = Vector3.Transform(camPos, rotMtx);
                }
                else
                {
                    camPos.X -= 1f;
                    camTarget.X -= 1f;
                }
            }
            if (keyState.IsKeyDown(Keys.Up))
            {
                camPos.Y += 1f;
                camTarget.Y += 1f;
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                camPos.Y -= 1f;
                camTarget.Y -= 1f;
            }
            if (keyState.IsKeyDown(Keys.OemPlus))
            {
                camPos.Z += 1f;
            }
            if (keyState.IsKeyDown(Keys.OemMinus))
            {
                camPos.Z -= 1f;
            }

            mtxView = Matrix.CreateLookAt(camPos, camTarget, Vector3.Up);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            basicEffect.Projection = mtxProjection;
            basicEffect.View = mtxView;
            basicEffect.World = mtxWorld;

            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            //Turn off culling so that both sides of the rendered triangle can be seen.
            GraphicsDevice.RasterizerState = new RasterizerState { CullMode = CullMode.None };

            foreach (var pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);
            }

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
