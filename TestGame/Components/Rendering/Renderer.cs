using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame.Components.Rendering
{
    public abstract class Renderer : Component, IRenderable
    {
        public override void Update(GameTime gt)
        {
        }

        public override void Initialise()
        {
        }

        public abstract void Draw(GraphicsDevice graphicsDevice, Camera camera);
    }
}
