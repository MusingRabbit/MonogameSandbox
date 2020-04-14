using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jitter.LinearMath;
using Microsoft.Xna.Framework;

namespace TestGame.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 ToVector3(this JVector jVector)
        {
            return new Vector3(jVector.X, jVector.Y, jVector.Z);
        }

        public static JVector ToJVector(this Vector3 vec3)
        {
            return new JVector(vec3.X, vec3.Y, vec3.Z);
        }
    }
}
