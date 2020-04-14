using Microsoft.Xna.Framework;
using TestGame.Objects;
using TestGame.Util.Math;

namespace TestGame.Components
{
    public class Camera : Component
    {
        private Vector3 position;
        private Vector3 rotation;
        private Vector3 lookAt;
        private Vector3 mouseRotationBuffer;

        private float movementSpeed;
        private float farPlane, nearPlane;

        public Vector3 Position
        {
            get { return this.position; }
            set
            {
                this.position = value;
                this.UpdateLookAt();
            }
        }

        public Vector3 Rotation
        {
            get { return this.rotation; }
            set
            {
                this.rotation = value;
                this.UpdateLookAt();
            }
        }

        public Matrix Projection
        {
            get;
            protected set;
        }

        public Matrix View => Matrix.CreateLookAt(this.position, this.lookAt, Vector3.Up);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game class.</param>
        /// <param name="position">Camera's initial position.</param>
        /// <param name="rotation">Camera's initial rotation.</param>
        /// <param name="speed">Camera's movement speed</param>
        public Camera(Vector3 position, Vector3 rotation, float aspectRatio, float speed) 
            : base()
        {
            this.movementSpeed = speed;

            this.farPlane = 1000.0f;
            this.nearPlane = 0.05f;
            this.mouseRotationBuffer = Vector3.Zero;

            //Setup projection matrix
            this.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, nearPlane, farPlane);

            this.MoveTo(position,rotation);
        }

        /// <summary>
        /// Set the camera's position and rotation.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        private void MoveTo(Vector3 position, Vector3 rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        // Update the look at vector
        private void UpdateLookAt()
        {
            // Build a rotation matrix.
            var rotMatrix = Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(rotation.Y);

            var qRot = Quaternion.CreateFromRotationMatrix(rotMatrix);

            // Build look at offset vector.
            var lookAtOffset = Vector3.Transform(Vector3.UnitZ, qRot);

            //Update the camera's look at vector.
            lookAt = this.position + lookAtOffset;
        }


        /// <summary>
        /// Method that simulates movement
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private Vector3 PreviewMove(Vector3 amount)
        {
            var scaledAmt = amount * movementSpeed;

            // Create a rotate matrix.
            var rotate = Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(rotation.Y);

            // Create a movement vector.
            var movement = new Vector3(scaledAmt.X, scaledAmt.Y, scaledAmt.Z);
            movement = Vector3.Transform(movement, rotate);

            // return the value of camera position + movement vector.
            return this.position + movement;
        }

        //Method that actually moves the camera.
        public void Move(Vector3 scale)
        {
            MoveTo(PreviewMove(scale), rotation);
            UpdateLookAt();
        }

        public void SetLookAt(Vector3 target)
        {
            this.lookAt = target;
        }

        public void SetLookAt(GameObject target)
        {
            var transform = target.GetComponent<Transform>();
            this.lookAt = transform.Position;
        }
        
        public void Rotate(float yaw, float pitch, float roll)
        {
            var minMovement = MathHelper.ToRadians(-75.0f);
            var maxMovement = MathHelper.ToRadians(75.0f);

            mouseRotationBuffer.X += (yaw * 0.01f).Clamp(minMovement, maxMovement);
            mouseRotationBuffer.Y += (pitch * 0.01f).Clamp(minMovement, maxMovement);

            mouseRotationBuffer.Y = mouseRotationBuffer.Y.Clamp(minMovement, maxMovement);

            this.rotation = (new Vector3(mouseRotationBuffer.Y, -MathHelper.WrapAngle(mouseRotationBuffer.X), 0));
            this.UpdateLookAt();
        }


        public override void Update(GameTime gt)
        {
        }

        public override void Initialise()
        {
        }
    }
}
