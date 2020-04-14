using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TestGame.Objects;

namespace TestGame.Components
{
    public abstract class Component : IDisposable
    {
        private string id;
        private bool enabled;
        private GameObject gameObject;

        public string Id
        {
            get { return this.id; }
        }

        public bool Enabled
        {
            get { return this.enabled; }
            set
            {

                if (value != enabled)
                {
                    this.enabled = value;

                    if (this.enabled)
                    {
                        this.OnEnabled();
                    }
                    else
                    {
                        this.OnDisabled();
                    }

                    this.NotifyPropertyChanged(nameof(this.Enabled));
                }
            }
        }

        public GameObject GameObject
        {
            get { return this.gameObject; }
            set { this.gameObject = value; }
        }

        public event EventHandler<PropertyChangedEventArgs> PropertyChanged = null;

        public Component()
        {
            this.id = $"CMP-{Guid.NewGuid().ToString().ToUpper()}";
            this.enabled = true;
        }

        public virtual void OnEnabled()
        {

        }

        public virtual void OnDisabled()
        {

        }

        public abstract void Update(GameTime gt);

        public abstract void Initialise();

        protected void NotifyPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public virtual void Dispose()
        {

        }
    }
}
