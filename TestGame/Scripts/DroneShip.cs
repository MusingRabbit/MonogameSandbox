using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TestGame.Components.Physics;

namespace TestGame.Scripts
{
    public class DroneShip : Ship
    {
        public DroneShip()
        {

        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            this.ThrustForward();
        }

        public override void Initialise()
        {
            base.Initialise();
            this.RigidBody.Mass = 50;
            this.Thrust = 20;
            this.GameObject.Transform.SetScale(0.006f);
        }
    }
}
