using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TestGame.Components;

namespace TestGame.Objects
{
    public class GameObject : IDisposable
    {
        private readonly string id;
        private readonly string name;
        private string tag;
        private Transform transform;
        private readonly List<Component> components;

        public string Id
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public string Tag
        {
            get { return this.tag; }
            set { this.tag = value; }
        }

        public Transform Transform => this.transform;

        public List<Component> Components => this.components;

        public GameObject(string name)
        {
            this.id = $"OBJ-{Guid.NewGuid().ToString().ToUpper()}";
            this.name = name;
            this.components = new List<Component>();

            this.transform = new Transform();
            this.components.Add(this.transform);
        }

        public void AddComponent<T>(T component) 
            where T : Component
        {
            component.GameObject = this;
            this.components.Add(component);
        }

        public T[] GetComponents<T>()
            where T : Component
        {
            var validComponents = this.GetComponentsByType<T>();
            return validComponents.ToArray();
        }

        public T GetComponentById<T>(string id) 
            where T : Component
        {
            var validComponents = this.GetComponentsByType<T>();
            return (T)validComponents.FirstOrDefault(x => x.Id == id);
        }

        private IEnumerable<T> GetComponentsByType<T>()
            where T : Component
        {
            return this.components.Where(x => x.GetType() == typeof(T)).Cast<T>();
        }

        public bool HasComponentOfType<T>() 
            where T : Component
        {
            foreach (var cmp in this.components)
            {
                if (cmp is T)
                {
                    return true;
                }
            }

            return false;
        }

        public T GetComponent<T>()
            where T : Component
        {
            return this.GetComponentsByType<T>().FirstOrDefault();
        }

        private void UpdateComponents(GameTime gt)
        {
            foreach (var component in this.components)
            {
                component.Update(gt);
            }
        }

        private void InitialiseComponents()
        {
            foreach (var component in this.components)
            {
                component.Initialise();
            }
        }

        public void Initialise()
        {
            this.InitialiseComponents();
        }

        public void Update(GameTime gt)
        {
            this.UpdateComponents(gt);
        }

        public bool RemoveComponentById(string id)
        {
            var component = this.components.SingleOrDefault(x => x.Id == id);

            if (component == null)
            {
                return false;
            }

            return this.components.Remove(component);
        }

        public void Dispose()
        {
            foreach (var cmp in this.components)
            {
                cmp.Dispose();
            }

            this.components.Clear();
        }
    }
}
