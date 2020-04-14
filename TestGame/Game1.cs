using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Jitter;
using Jitter.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestGame.Components;
using TestGame.Components.Rendering;
using TestGame.Enums;
using TestGame.Factory;
using TestGame.Objects;
using TestGame.Scripts;
using TestGame.Util.Math;

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private MouseState prevMouseState;

        private Camera camera;

        private List<GameObject> gameObjects;
        private ShipFactory shipFactory;

        public Game1()
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
            this.shipFactory = new ShipFactory();

            this.gameObjects = new List<GameObject>();

            this.camera = new Camera(new Vector3(0, 100, -10), Vector3.Zero, this.GraphicsDevice.Viewport.AspectRatio,5f );
            this.IsMouseVisible = true;
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

            
            this.shipFactory.SetModel(ShipType.PlayerShip,  Content.Load<Model>("Starship2"));
            this.shipFactory.SetModel(ShipType.DroneShip, Content.Load<Model>("Starship_drone1"));

            this.gameObjects.Add(this.shipFactory.CreateShip(ShipType.PlayerShip, new Vector3(0, 0, -30)));
            this.gameObjects.Add(this.shipFactory.CreateShip(ShipType.DroneShip, new Vector3(0, 0, 0)));
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
            this.UpdateGameObjects(gameTime);


            //var currMouseState = Mouse.GetState();
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Physics.World.Step(dt, true);



            //camera.Move(moveVector);
            //camera.SetLookAt(this.playerShip);
            camera.SetLookAt(Vector3.Zero);

            // Handle mouse movement.

            //if (currMouseState != prevMouseState)
            //{
            //    float deltaX = currMouseState.X - (this.GraphicsDevice.Viewport.Width / 2);
            //    float deltaY = currMouseState.Y - (this.GraphicsDevice.Viewport.Height / 2);

            //    camera.Rotate(deltaX, deltaY, 0);

            //    deltaX = deltaY = 0;
            //}

            //Mouse.SetPosition(this.GraphicsDevice.Viewport.Width / 2, this.GraphicsDevice.Viewport.Height / 2);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        private void UpdateGameObjects(GameTime gt)
        {
            foreach (var gameObj in this.gameObjects)
            {
                gameObj.Update(gt);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var renderableComponents = this.gameObjects
                .SelectMany(x => x.Components)
                .Where(x => x is IRenderable)
                .Cast<IRenderable>();

            foreach (var cmp in renderableComponents)
            {
                cmp.Draw(this.GraphicsDevice, this.camera);
            }

            //this.playerShip.Draw(this.GraphicsDevice, this.camera);
            //this.enemyShip.Draw(this.GraphicsDevice, this.camera);

            //var basicEffect = new BasicEffect(GraphicsDevice)
            //{
            //    VertexColorEnabled = true,
            //    View = camera.View,
            //    Projection = camera.Projection,
            //    World = Matrix.Identity
            //};

            //foreach (var pass in basicEffect.CurrentTechnique.Passes)
            //{
            //    pass.Apply();
            //    this.GraphicsDevice.SetVertexBuffer(floor.VertexBuffer);
            //    this.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, floor.VertexBuffer.VertexCount / 3);
            //}

            base.Draw(gameTime);
        }
    }
}
