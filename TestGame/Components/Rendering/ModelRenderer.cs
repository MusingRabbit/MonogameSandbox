using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame.Components.Rendering
{
    public class ModelRenderer : Renderer
    {
        private readonly Model model;
        private Transform transform;

        public Model Model => this.model;

        public ModelRenderer(Model model)
        {
            this.model = model;
            this.transform = new Transform();
        }

        public override void Update(GameTime gt)
        {
            this.transform = this.GameObject.Transform;
        }

        public override void Initialise()
        {
            
        }

        public override void Draw(GraphicsDevice graphicsDevice, Camera camera)
        {
            foreach (var mesh in this.model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.AmbientLightColor = new Vector3(100f, 100f, 100f);
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                    effect.World = this.transform.WorldMatrix;
                    effect.EnableDefaultLighting();
                    effect.Alpha = 1;
                }

                mesh.Draw();
            }
        }
    }
}
