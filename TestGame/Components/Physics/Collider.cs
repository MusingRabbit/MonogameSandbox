using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestGame.Components.Physics
{
    public abstract class Collider : Component
    {
        protected bool autoCompute;
        protected Vector3 minimum;
        protected Vector3 maximum;
        protected Vector3 centre;

        /// <summary>
        /// Gets or sets whether the collider can be picked by ray cast.
        /// </summary>
        public bool IsPickable { get; set; }

        /// <summary>
        /// Indicates ehther the collider is a trigger.
        /// </summary>
        public bool IsTrigger { get; set; }

        public Vector3 Centre
        {
            get { return this.centre; }
            set
            {
                this.centre = value;
                minimum = maximum - value;
                autoCompute = false;
            }
        }

        public Vector3 Maximum
        {
            get { return this.maximum; }
            set
            {
                this.maximum = value;
                this.Size = this.maximum - this.minimum;
            }
        }

        public Vector3 Minimum
        {
            get { return this.minimum; }
            set
            {
                this.minimum = value;
                this.Size = this.maximum - this.minimum;
            }
        }

        public Vector3 Size { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    (int)(centre.X - minimum.X), 
                    (int)(centre.Z - minimum.Z), 
                    (int)(centre.X + minimum.X), 
                    (int)(centre.Z + minimum.Z));
            }
        }

        public Collider()
        {
            IsPickable = true;
            IsTrigger = false;
            centre = Vector3.Zero;
            minimum = Vector3.Zero;
            autoCompute = true;
        }

        public override void Initialise()
        {

        }

        /// <summary>
        /// Compute the collider if a renderable component is attached to the scene object.
        /// This method can be called by a renderable component if needed.
        /// </summary>
        public abstract void Compute();

        /// <summary>
        /// Check if the collider enter in collision with another collider.
        /// </summary>
        /// <param name="other">A collider</param>
        /// <returns>Returns true if it collides, otherwise it returns false.</returns>
        public abstract bool Collides(Collider other);

        /// <summary>
        /// Check if the collider is intersected by a ray.
        /// </summary>
        /// <param name="ray">A ray</param>
        /// <returns>Returns true if the ray intersects this collider</returns>
        public abstract bool IntersectedBy(Ray ray);

        public void SetSize(float x, float y, float z)
        {
            this.maximum.X = x;
            this.minimum.X = -x;

            this.maximum.Y = y;
            this.minimum.Y = -y;

            this.maximum.Z = z;
            this.minimum.Z = -z;

            this.Size = this.maximum - this.minimum;
        }

        public void SetCentre(float x, float y, float z)
        {
            this.centre.X = x;
            this.centre.Y = y;
            this.centre.Z = z;
        }
    }
}
