using Microsoft.Xna.Framework;

namespace TestGame.Events.Transform
{
    public class RotationChangedArgs
    {
        public Vector3 Rotation { get; set; }

        public RotationChangedArgs(Vector3 rotation)
        {
            this.Rotation = rotation;
        }
    }
}
