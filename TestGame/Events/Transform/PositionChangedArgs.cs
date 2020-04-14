using Microsoft.Xna.Framework;

namespace TestGame.Events.Transform
{
    public class PositionChangedArgs
    {
        public Vector3 Position { get; set; }

        public PositionChangedArgs(Vector3 position)
        {
            this.Position = position;
        }
    }
}
