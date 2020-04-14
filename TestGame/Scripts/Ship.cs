using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TestGame.Components.Behaviour;
using TestGame.Components.Physics;

namespace TestGame.Scripts
{
    public class Ship : Behaviour
    {
        public int HitPoints { get; set; }
        public SphereCollider SphereCollider { get; set; }
        public RigidBody RigidBody { get; set; }
        public float SpeedLimit { get; set; }
        public float Thrust { get; set; }

        public Ship()
        {
            
        }


        public override void Update(GameTime gt)
        {

        }

        protected void ThrustForward()
        {
            var fwdVec = this.GameObject.Transform.Forward;
            fwdVec.Normalize();
            fwdVec.Y = 0;

            Debug.WriteLine(fwdVec * this.Thrust);

            this.RigidBody.AddForce(fwdVec * this.Thrust);
        }

        protected void ThrustBackward()
        {
            var bckVec = this.GameObject.Transform.Backward;
            bckVec.Normalize();
            bckVec.Y = 0;
            Debug.WriteLine(bckVec * this.Thrust);
            this.RigidBody.AddForce(bckVec * this.Thrust);
        }

        protected void ThrustLeft()
        {
            var lVec = this.GameObject.Transform.Left;
            lVec.Normalize();
            lVec.Y = 0;
            Debug.WriteLine(lVec * this.Thrust);
            this.RigidBody.AddForce(lVec * this.Thrust);
        }

        protected void ThrustRight()
        {
            var rVec = this.GameObject.Transform.Right;
            rVec.Normalize();
            rVec.Y = 0;
            Debug.WriteLine(rVec * this.Thrust);
            this.RigidBody.AddForce(rVec * this.Thrust);
        }

        public override void Initialise()
        {
            this.SpeedLimit = 5;
            this.HitPoints = 20;
            this.SphereCollider = this.GameObject.GetComponent<SphereCollider>();
            this.RigidBody = this.GameObject.GetComponent<RigidBody>();
            this.RigidBody.Restitution = 1;
        }
    }
}
