using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jitter;
using Jitter.Collision;

namespace TestGame
{
    public static class Physics
    {
        private static World worldInstance;

        public static World World
        {
            get { return worldInstance = worldInstance ?? new World(new CollisionSystemPersistentSAP()); }
        }
    }
}
