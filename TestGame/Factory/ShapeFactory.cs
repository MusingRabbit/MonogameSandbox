using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame.Factory
{
    public class ShapeFactory
    {
        private GraphicsDevice graphics;

        public ShapeFactory(GraphicsDevice graphics)
        {
            this.graphics = graphics;
        }

        public ModelMesh CreateSphere()
        {
            return null;
        }
    }
}
