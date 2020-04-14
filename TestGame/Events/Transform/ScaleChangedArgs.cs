using Microsoft.Xna.Framework;

namespace TestGame.Events.Transform
{
    public class ScaleChangedArgs
    {
        public Vector3 Scale { get; set; }

        public ScaleChangedArgs(Vector3 scale)
        {
            this.Scale = scale;
        }
    }
}
