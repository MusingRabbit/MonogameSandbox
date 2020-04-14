using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TestGame.Components;
using TestGame.Components.Physics;

namespace TestGame.Scripts
{
    public class PlayerShip : Ship
    {
        private BoundingBox playBounds;
        private Transform transform;

        public PlayerShip()
        {
            
        }


        public override void Initialise()
        {
            base.Initialise();

            this.playBounds = new BoundingBox(new Vector3(-55, 0, -30), new Vector3(55, 0, 30));

            this.SpeedLimit = 30;
            this.HitPoints = 100;
            this.Thrust = 2000;
            this.GameObject.Transform.SetScale(0.008f);
            this.GameObject.Transform.SetRotation(MathHelper.Pi, 0, 0);
            this.RigidBody.Mass = 50;

            this.transform = this.GameObject.Transform;

            this.RigidBody.SetTransform(this.transform);
        }

        public override void Update(GameTime gt)
        {
            var kbState = Keyboard.GetState();

            var hasInput = false;

            if (kbState.IsKeyDown(Keys.Up) && this.CanThrustUp())
            {
                this.ThrustForward();
                hasInput = true;
            }

            if (kbState.IsKeyDown(Keys.Down) && this.CanMoveDown())
            {
                this.ThrustBackward();
                hasInput = true;
            }

            if (kbState.IsKeyDown(Keys.Left))
            {
                if (this.CanMoveLeft())
                {
                    this.ThrustLeft();
                    hasInput = true;
                }

                this.RollLeft();
            }
            else if (kbState.IsKeyDown(Keys.Right))
            {
                if (this.CanMoveRight())
                {
                    this.ThrustRight();
                    hasInput = true;
                }

                this.RollRight();
            }
            else
            {
                this.CentreRoll();
            }

            if (!hasInput)
            {
                this.Decelerate();
            }


            base.Update(gt);
        }

        private void CentreRoll()
        {
            if (this.transform.Rotation.Z > 0)
            {
                this.transform.Rotate(0, 0, -0.1f);
            }
            if (this.transform.Rotation.Z < 0)
            {
                this.transform.Rotate(0, 0, 0.1f);
            }
        }

        private void RollRight()
        {
            if (this.transform.Rotation.Z > -MathHelper.PiOver4)
            {
                this.transform.Rotate(0, 0, -0.1f);
            }
        }

        private void RollLeft()
        {
            if (this.transform.Rotation.Z < MathHelper.PiOver4)
            {
                this.transform.Rotate(0, 0, 0.1f);
            }
        }

        private void Decelerate()
        {
            var rbVel = this.RigidBody.Velocity;

            if (rbVel.X > 0)
            {
                this.RigidBody.AddForceOnXAxis(-this.Thrust);
            }

            if (rbVel.X < 0)
            {
                this.RigidBody.AddForceOnXAxis(this.Thrust);
            }

            if (rbVel.Z > 0)
            {
                this.RigidBody.AddForceOnZAxis(-this.Thrust);
            }

            if (rbVel.Z < 0)
            {
                this.RigidBody.AddForceOnZAxis(this.Thrust);
            }
        }

        private bool CanThrustUp()
        {
            return this.playBounds.Max.Z > this.transform.Position.Z && this.SpeedLimit > Math.Abs(this.RigidBody.Velocity.Z);
        }

        private bool CanMoveDown()
        {
            return this.playBounds.Min.Z < this.transform.Position.Z && this.SpeedLimit > Math.Abs(this.RigidBody.Velocity.Z);
        }

        private bool CanMoveLeft()
        {
            return this.playBounds.Max.X > this.transform.Position.X && this.SpeedLimit > Math.Abs(this.RigidBody.Velocity.X);
        }

        private bool CanMoveRight()
        {
            return this.playBounds.Min.X < this.transform.Position.X && this.SpeedLimit > Math.Abs(this.RigidBody.Velocity.X);
        }

    }
}
