using Jitter.LinearMath;
using Microsoft.Xna.Framework;

namespace TestGame.Util.Math
{
    public static class MatrixExtensions
    {
        public static Quaternion ExtractRotationToQuaternion(this Matrix matrix)
        {
            var result = Quaternion.Identity;

            if (float.IsNaN(matrix.M11))
            {
                return result;
            }

            var sx = System.Math.Sign(matrix.M11 * matrix.M12 * matrix.M13 * matrix.M14) < 0 ? -1.0f : 1.0f;
            var sy = System.Math.Sign(matrix.M21 * matrix.M22 * matrix.M23 * matrix.M24) < 0 ? -1.0f : 1.0f;
            var sz = System.Math.Sign(matrix.M31 * matrix.M32 * matrix.M33 * matrix.M34) < 0 ? -1.0f : 1.0f;

            sx *= (float)System.Math.Sqrt((matrix.M11 * matrix.M11) + (matrix.M12 * matrix.M12) + (matrix.M13 * matrix.M13));
            sy *= (float)System.Math.Sqrt((matrix.M21 * matrix.M21) + (matrix.M22 * matrix.M22) + (matrix.M23 * matrix.M23));
            sz *= (float)System.Math.Sqrt((matrix.M31 * matrix.M31) + (matrix.M32 * matrix.M32) + (matrix.M33 * matrix.M33));

            if (sx == 0.0 || sy == 0.0 || sz == 0.0)
            {
                return result;
            }

            var m = new Matrix(
                matrix.M11 / sx, matrix.M12 / sx, matrix.M13 / sx, 0.0f, 
                matrix.M21 / sy, matrix.M22 / sy, matrix.M23 / sy, 0.0f, 
                matrix.M31 / sz, matrix.M32 / sz, matrix.M33 / sz, 0.0f, 
                0.0f, 0.0f, 0.0f, 1.0f
            );

            result = Quaternion.CreateFromRotationMatrix(m);

            return result;
        }

        public static Vector3 ExtractRotation(this Matrix matrix)
        {
            var q = ExtractRotationToQuaternion(matrix);
            return q.ToEuler();
        }

        public static JMatrix ToJMatrix(this Matrix mat)
        {
            var result = new JMatrix
            {
                M11 = mat.M11,
                M12 = mat.M12,
                M13 = mat.M13,
                M21 = mat.M21,
                M22 = mat.M22,
                M23 = mat.M23,
                M31 = mat.M31,
                M32 = mat.M32,
                M33 = mat.M33
            };

            return result;
        }

        public static Matrix ToMatrix(this JMatrix mat)
        {
            return new Matrix(
                mat.M11, mat.M12, mat.M13, 0.0f,
                mat.M21, mat.M22, mat.M23, 0.0f,
                mat.M31, mat.M32, mat.M33, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
                );
        }
    }
}
