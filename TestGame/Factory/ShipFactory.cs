using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TestGame.Components.Behaviour;
using TestGame.Components.Physics;
using TestGame.Components.Rendering;
using TestGame.Enums;
using TestGame.Objects;
using TestGame.Scripts;

namespace TestGame.Factory
{
    public class ShipFactory
    {
        private readonly Dictionary<ShipType, Model> modelDict;

        public ShipFactory()
        {
            this.modelDict = new Dictionary<ShipType, Model>();
        }

        public void SetModel(ShipType shipType, Model model)
        {
            this.modelDict[shipType] = model;
        }



        public GameObject CreateShip(ShipType shipType, Vector3 position)
        {
            if (!modelDict.ContainsKey(shipType))
            {
                throw new KeyNotFoundException($"No model for key ({shipType}) could be found");
            }

            var result = new GameObject(shipType.ToString());

            var collider = this.GetCollider(shipType);

            var rigidBody = new RigidBody();
            rigidBody.SetShapeFromCollider(collider);
            rigidBody.ClampAxis(Axis.Y);

            var script = this.GetBehaviour(shipType);

            var renderer = this.GetRenderer(shipType);
            
            result.AddComponent(collider);
            result.AddComponent(rigidBody);
            result.AddComponent(script);
            result.AddComponent(renderer);

            result.Transform.SetPosition(position.X, position.Y, position.Z);

            result.Initialise();

            return result;
        }

        private Renderer GetRenderer(ShipType shipType)
        {
            var model = this.modelDict[shipType];

            return new ModelRenderer(model);
        }

        private Behaviour GetBehaviour(ShipType shipType)
        {
            switch (shipType)
            {
                case ShipType.PlayerShip:
                    return new PlayerShip();
                case ShipType.DroneShip:
                    return new DroneShip();
                default:
                    throw new ArgumentOutOfRangeException(nameof(shipType), shipType, null);
            }
        }

        private Collider GetCollider(ShipType shipType)
        {
            switch (shipType)
            {
                case ShipType.PlayerShip:
                    return new SphereCollider(4f);
                case ShipType.DroneShip:
                    return new SphereCollider(4f);
                default:
                    throw new ArgumentOutOfRangeException(nameof(shipType), shipType, null);
            }
        }
    }
}
