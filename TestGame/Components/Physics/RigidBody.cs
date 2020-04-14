using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Jitter.Collision.Shapes;
using Jitter.Dynamics;
using Jitter.LinearMath;
using Microsoft.Xna.Framework;
using TestGame.Enums;
using TestGame.Objects;
using TestGame.Util.Math;
using JRigidBody = Jitter.Dynamics.RigidBody;

namespace TestGame.Components.Physics
{
    public class RigidBody : Component
    {
        private JRigidBody rigidBody;
        private Material material;

        private List<Axis> clampAxis;

        public bool AffectedByGravity
        {
            get { return this.rigidBody.AffectedByGravity; }
            set { this.rigidBody.AffectedByGravity = value; }
        }

        public float Mass
        {
            get { return this.rigidBody.Mass; }
            set { this.rigidBody.Mass = value; }
        }

        public bool IsStatic
        {
            get { return this.rigidBody.IsStatic; }
            set { this.rigidBody.IsStatic = value; }
        }

        public Vector3 Velocity
        {
            get { return this.rigidBody.LinearVelocity.ToVector3(); }
            set { this.rigidBody.LinearVelocity = value.ToJVector(); }
        }

        public Vector3 AngularVelocity
        {
            get { return this.rigidBody.AngularVelocity.ToVector3(); }
            set { this.rigidBody.AngularVelocity = value.ToJVector(); }
        }

        public float Restitution
        {
            get { return this.material.Restitution; }
            set { this.material.Restitution = value; }
        }

        public float StaticFriction
        {
            get { return this.material.StaticFriction; }
            set { this.material.StaticFriction = value; }
        }

        public float KineticFriction
        {
            get { return this.material.KineticFriction; }
            set { this.material.KineticFriction = value; }
        }

        public bool SimulateRotation { get; set; }

        public Shape Shape
        {
            get { return this.rigidBody.Shape; }
            set { this.rigidBody.Shape = value; }
        }

        public RigidBody()
        {
            this.clampAxis = new List<Axis>();
            this.material = new Material();
            this.rigidBody = new JRigidBody(new SphereShape(1.0f), this.material);
            this.AffectedByGravity = false;
        }

        public void SetShapeFromCollider(Collider collider)
        {
            var shape = (Shape) null;
            var size = collider.Size;

            if (collider is BoxCollider)
            {
                shape = new BoxShape(size.X * 2, size.Y * 2, size.Z * 2);
            }

            if (collider is SphereCollider sphereCollider)
            {
                shape = new SphereShape(sphereCollider.BoundingSphere.Radius);
            }

            this.SetShape(shape);
        }

        public void SetTransform(Transform transform)
        {
            this.rigidBody.Position = transform.Position.ToJVector();
            this.rigidBody.Orientation = transform.GetRotationMatrix().ToJMatrix();
        }

        public void SetShape(Shape shape)
        {
            this.rigidBody.Shape = shape;

            shape.UpdateShape();
            
            TestGame.Physics.World.RemoveBody(this.rigidBody);
            TestGame.Physics.World.AddBody(this.rigidBody);
        }

        public void AddForce(Vector3 force)
        {
            this.rigidBody.AddForce(force.ToJVector());
        }

        public void AddForceOnXAxis(float amount)
        {
            this.rigidBody.AddForce(new JVector(amount, 0 ,0));
        }
        public void AddForceOnYAxis(float amount)
        {
            this.rigidBody.AddForce(new JVector(0, amount, 0));
        }
        public void AddForceOnZAxis(float amount)
        {
            this.rigidBody.AddForce(new JVector(0, 0, amount));
        }

        public void AddForceAtPosition(Vector3 force, Vector3 position)
        {
            this.rigidBody.AddForce(force.ToJVector(), position.ToJVector());
        }

        public void AddTorque(Vector3 torque)
        {
            this.rigidBody.AddForce(torque.ToJVector());
        }

        public void ClampAxis(Axis axis)
        {
            if (!this.clampAxis.Contains(axis))
                this.clampAxis.Add(axis);
        }

        private Vector3 GetClampedVecotr3()
        {
             var result = this.rigidBody.Position.ToVector3();

            if (this.clampAxis.Contains(Axis.X))
                result.X = 0;

            if (this.clampAxis.Contains(Axis.Y))
                result.Y = 0;

            if (this.clampAxis.Contains(Axis.Z))
                result.Z = 0;

            this.rigidBody.Position = result.ToJVector();

            return result;
        }

        public override void Update(GameTime gt)
        {
            var position = this.GetClampedVecotr3();
            
            this.GameObject.Transform.Position = position;

            if (this.SimulateRotation)
            {
                var rotationMatrix = this.rigidBody.Orientation.ToMatrix();
                this.GameObject.Transform.SetRotation(rotationMatrix);
            }
        }

        public override void Initialise()
        {
        }

        public override void Dispose()
        {
            TestGame.Physics.World.RemoveBody(this.rigidBody);
        }
    }
}
