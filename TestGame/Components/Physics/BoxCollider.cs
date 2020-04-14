using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TestGame.Objects;

namespace TestGame.Components.Physics
{
    public class BoxCollider : Collider
    {
        private BoundingBox boundingBox;

        public BoundingBox BoundingBox
        {
            get { return this.boundingBox; }
            set
            {
                this.boundingBox = value; 
                this.Compute();
            }
        }


        public BoxCollider(BoundingBox box)
        {
            this.boundingBox = box;
            this.Compute();
        }

        public override void Compute()
        {
            minimum = this.boundingBox.Min;
            maximum = this.boundingBox.Max;
            centre = maximum + minimum;
        }

        public override bool Collides(Collider other)
        {
            if (other is BoxCollider box)
                return this.boundingBox.Intersects(box.BoundingBox);

            if (other is SphereCollider sphere)
                return this.boundingBox.Intersects(sphere.BoundingSphere);

            return false;
        }

        public override bool IntersectedBy(Ray ray)
        {
            return ray.Intersects(this.boundingBox) != null;
        }

        public override void Update(GameTime gt)
        {
            var transform = this.GameObject.Transform;

            this.boundingBox.Min = (minimum - (centre + transform.Position)) * transform.Scale;
            this.boundingBox.Max = (maximum + (centre + transform.Position) * transform.Scale);
        }
    }
}
