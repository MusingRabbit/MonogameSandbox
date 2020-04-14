using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{
    public class Floor
    {
        /// <summary>
        /// Attributes
        /// </summary>
        private int floorWidth;
        private int floorHeight;

        private VertexBuffer floorBuffer;
        private GraphicsDevice device;
        private Color[] floorColors = new Color[2] { Color.Black, Color.White };

        public VertexBuffer VertexBuffer => this.floorBuffer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Floor(GraphicsDevice graphicsDevice, int width, int height)
        {
            this.device = graphicsDevice;
            this.floorWidth = width;
            this.floorHeight = height;
            this.BuildFloorBuffer();
        }

        private void BuildFloorBuffer()
        {
            var vertexList = new List<VertexPositionColor>();
            var counter = 0;

            for (var x = 0; x < floorWidth; x++)
            {
                counter++;
                for (var z = 0; z < floorHeight; z++)
                {
                    counter++;
                    var vertices = GetFloorTileVertices(x, z, floorColors[counter % 2]);
                    vertexList.AddRange(vertices);
                }
            }

            //Create floor buffer
            floorBuffer = new VertexBuffer(device, VertexPositionColor.VertexDeclaration, vertexList.Count, BufferUsage.None);
            floorBuffer.SetData(vertexList.ToArray());
        }

        //Defines a single title in the floor
        private List<VertexPositionColor> GetFloorTileVertices(int xOffset, int zOffset, Color tileColor)
        {
            var result = new List<VertexPositionColor>
            {
                new VertexPositionColor(new Vector3(0 + xOffset, 0, 0 + zOffset), tileColor),
                new VertexPositionColor(new Vector3(1 + xOffset, 0, 0 + zOffset), tileColor),
                new VertexPositionColor(new Vector3(0 + xOffset, 0, 1 + zOffset), tileColor),
                new VertexPositionColor(new Vector3(1 + xOffset, 0, 0 + zOffset), tileColor),
                new VertexPositionColor(new Vector3(1 + xOffset, 0, 1 + zOffset), tileColor),
                new VertexPositionColor(new Vector3(0 + xOffset, 0, 1 + zOffset), tileColor)
            };


            return result;
        }
    }
}
