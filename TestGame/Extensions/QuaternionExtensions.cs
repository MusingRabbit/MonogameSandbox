using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestGame.Extensions
{
    public static class QuaternionExtensions
    {
        public static Vector3 ToEuler(this Quaternion rotation)
        {
            var forward = Vector3.Transform(Vector3.Forward, rotation);
            var up = Vector3.Transform(Vector3.Up, rotation);
            var result = AngleTo(new Vector3(), forward);

            if (result.X == MathHelper.PiOver2)
            {
                result.Y = ArcTanAngle(up.Z, up.X);
                result.Z = 0;
            }
            else if (result.X == -MathHelper.PiOver2)
            {
                result.Y = ArcTanAngle(-up.Z, -up.X);
                result.Z = 0;
            }
            else
            {
                up = Vector3.Transform(up, Matrix.CreateRotationY(-result.Y));
                up = Vector3.Transform(up, Matrix.CreateRotationX(-result.X));
                result.Z = ArcTanAngle(up.Y, -up.X);
            }

            return result;
        }

        private static float ArcTanAngle(float x, float y)
        {
            if (x == 0)
            {
                return (y == 1) ? MathHelper.PiOver2 : -MathHelper.PiOver2;
            }
            else if (x > 0)
            {
                return (float) Math.Atan(y / x);
            }
            else if (x < 0)
            {
                return (float)Math.Atan(y / x) + ((y > 0) ? MathHelper.Pi : -MathHelper.Pi);
            }

            return 0;
        }

        private static Vector3 AngleTo(Vector3 pointA, Vector3 pointB)
        {
            var angle = new Vector3();
            var v3 = Vector3.Normalize(pointB - pointA);
            angle.X = (float) Math.Asin(v3.Y);
            angle.Y = ArcTanAngle(-v3.Z, -v3.Y);
            return angle;
        }
    }
}
