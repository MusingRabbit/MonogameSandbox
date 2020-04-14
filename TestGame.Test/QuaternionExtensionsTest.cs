using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using TestGame.Util.Math;

namespace TestGame.Test
{
    [TestClass]
    public class QuaternionExtensionsTest
    {
        [TestMethod]
        public void ToEuler()
        {
            var rotVec = new Vector3(3,4,5);

            var qRot = Quaternion.CreateFromYawPitchRoll(rotVec.X, rotVec.Y, rotVec.Z);
            var rotVec2 = qRot.ToEuler();
            
            Assert.IsTrue(rotVec2 == rotVec);
        }
    }
}
