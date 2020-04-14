using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TestGame.Events.Transform;
using TestGame.Extensions;

namespace TestGame.Components
{
    public class Transform : Component
    {
        private Matrix worldMatrix;
        private Vector3 localRotation;
        private Vector3 localPosition;
        private Vector3 localScale;

        public event EventHandler<PositionChangedArgs> OnPositionChanged;
        public event EventHandler<RotationChangedArgs> OnRotationChanged;
        public event EventHandler<ScaleChangedArgs> OnScaleChanged;
        public event EventHandler<EventArgs> OnChanged; 

        public Vector3 Position
        {
            get { return this.localPosition; }
            set
            {
                this.SetPosition(value.X, value.Y, value.Z);
            }
        }

        public Vector3 Rotation
        {
            get { return this.localRotation; }
            set
            { 
                this.SetRotation(value.X, value.Y, value.Z);
            }
        }

        public Vector3 Scale
        {
            get { return this.localScale; }
            set
            {
                this.SetScale(value.X, value.Y, value.Z);
            }
        }

        public Matrix WorldMatrix
        {
            get
            {
                this.UpdateWorldMatrix();
                return this.worldMatrix;
            }
        }

        public Vector3 Forward => this.WorldMatrix.Forward;
        public Vector3 Up => this.WorldMatrix.Up;
        public Vector3 Backward => this.WorldMatrix.Backward;
        public Vector3 Left => this.WorldMatrix.Left;
        public Vector3 Right => this.WorldMatrix.Right;

        public Transform()
        {
            this.localPosition = new Vector3();
            this.localRotation = new Vector3();
            this.localScale = new Vector3(1.0f,1.0f,1.0f);
        }

        private Matrix GetWorldMatrix()
        {
            var result = Matrix.Identity;
            result *= Matrix.CreateScale(this.localScale);
            result *= Matrix.CreateFromQuaternion(Quaternion.CreateFromYawPitchRoll(localRotation.X, localRotation.Y, localRotation.Z));
            result *= Matrix.CreateTranslation(this.localPosition);
            return result;
        }

        private void UpdateWorldMatrix()
        {
            this.worldMatrix = this.GetWorldMatrix();
        }


        public Matrix GetRotationMatrix()
        {
            return Matrix.CreateFromYawPitchRoll(this.localRotation.X, this.localRotation.Y, this.localRotation.Z);
        }

        public void Translate(float x, float y, float z)
        {
            this.localPosition.X += x;
            this.localPosition.Y += y;
            this.localPosition.Z += z;
        }

        public void TranslateX(float x)
        {
            this.Translate(x,0,0);
        }

        public void TranslateY(float y)
        {
            this.Translate(0, y, 0);
        }

        public void TranslateZ(float z)
        {
            this.Translate(0, 0, z);
        }

        public void SetPosition(float x, float y, float z)
        {
            this.localPosition.X = x;
            this.localPosition.Y = y;
            this.localPosition.Z = z;
            this.OnPositionChanged?.Invoke(this, new PositionChangedArgs(this.localPosition));
            this.OnChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetScale(float x, float y, float z)
        {
            this.localScale.X = x;
            this.localScale.Y = y;
            this.localScale.Z = z;
            this.OnScaleChanged?.Invoke(this, new ScaleChangedArgs(this.localScale));
            this.OnChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetRotation(float x, float y, float z)
        {
            this.localRotation.X = x;
            this.localRotation.Y = y;
            this.localRotation.Z = z;
            this?.OnRotationChanged?.Invoke(this, new RotationChangedArgs(this.localRotation));
            this.OnChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Rotate(float x, float y, float z)
        {
            this.localRotation.X += x;
            this.localRotation.Y += y;
            this.localRotation.Z += z;
        }

        public void SetRotation(Matrix rotationMatrix)
        {
            var qRot = Quaternion.CreateFromRotationMatrix(rotationMatrix);
            var rot = qRot.ToEuler();
            this.SetRotation(rot.X, rot.Y, rot.Z);
        }

        public void SetScale(float amount)
        {
            this.SetScale(amount,amount,amount);
        }

        public override void Update(GameTime gt)
        {
        }

        public override void Initialise()
        {
        }
    }
}
