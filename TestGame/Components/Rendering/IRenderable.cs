using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame.Components.Rendering
{
    interface IRenderable
    {
        void Draw(GraphicsDevice graphicsDevice, Camera camera);
    }
}
