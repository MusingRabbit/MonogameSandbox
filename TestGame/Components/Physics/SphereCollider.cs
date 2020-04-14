using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TestGame.Objects;

namespace TestGame.Components.Physics
{
    public class SphereCollider : Collider
    {
        private BoundingSphere sphere;

        public BoundingSphere BoundingSphere
        {
            get { return this.sphere; }
            set { this.sphere = value; }
        }

        public SphereCollider(float radius)
        {
            this.sphere = new BoundingSphere(Vector3.Zero, radius);
        }

        public override void Update(GameTime gt)
        {
            this.sphere.Center = this.GameObject.Transform.Position;
        }

        public override void Compute()
        {
            
        }

        public override bool Collides(Collider other)
        {
            if (other is SphereCollider collider)
                return sphere.Intersects(collider.BoundingSphere);

            if (other is BoxCollider boxCollider)
                return sphere.Intersects(boxCollider.BoundingBox);

            return false;
        }

        public override bool IntersectedBy(Ray ray)
        {
            return ray.Intersects(this.BoundingSphere) != null;
        }
    }
}
