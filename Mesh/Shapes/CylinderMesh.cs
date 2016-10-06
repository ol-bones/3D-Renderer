using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace 3DRenderer
{
    class CylinderMesh : TMesh
    {
        float Radius;

        public CylinderMesh(float Radius, int ShaderID) : base()
        {
            this.ShaderID = ShaderID;
            GL.UseProgram(ShaderID);
            DrawType = PrimitiveType.Triangles;

            this.Radius = Radius;

            CreateCylinder(500);
            VertexBufferObject = new VBO(this);
        }

        /// <summary>
        ///     Creates two circles 0.5f from the origin,
        ///     filling the gap with rectangles.
        ///     
        ///     Circle generation from David Parker's example
        ///     in 2D Graphics
        ///     
        /// </summary>
        /// <param name="numSegments"></param>
        void CreateCylinder(int numSegments)
        {
            float anglePerSegment = (float)Math.PI * 2 / numSegments;
            for (int i = 0; i <= numSegments; i++)
            {
                // calculate tlX and tlY for first vertex
                float angle = anglePerSegment * i;
                float x = this.Radius * (float)Math.Cos(angle);
                float y = this.Radius * (float)Math.Sin(angle);
                float texCoord1 = (angle);

                // calculate tlX and tlY for next vertex
                angle = anglePerSegment * (i+1);
                float nx = this.Radius * (float)Math.Cos(angle);
                float ny = this.Radius * (float)Math.Sin(angle);
                float texCoord2 = (angle);

                // first circle
                AddTriangle(new Triangle(
                    new Vector3(nx, ny, 0.5f),
                    new Vector3(x, y, 0.5f),
                    new Vector3(new Vector3(0.0f, 0.0f, 0.5f)), // centre of circle
                    new Vector2(nx + 0.5f, ny + 0.5f),
                    new Vector2(x + 0.5f, y + 0.5f),
                    new Vector2(0.5f, 0.5f)
                ));

                // body of cylinder
                AddTriangle(new Triangle(
                    new Vector3(nx, ny, -0.5f),
                    new Vector3(x, y, 0.5f),
                    new Vector3(nx, ny, 0.5f),
                    new Vector2(0.0f, texCoord2),
                    new Vector2(1.0f, texCoord1),
                    new Vector2(1.0f, texCoord2)
                ));

                // body of cylinder
                AddTriangle(new Triangle(
                    new Vector3(nx, ny, -0.5f),
                    new Vector3(x, y, -0.5f),
                    new Vector3(x, y, 0.5f),
                    new Vector2(0.0f, texCoord2),
                    new Vector2(0.0f, texCoord1),
                    new Vector2(1.0f, texCoord1)
                ));

                // other circle
                AddTriangle(new Triangle(
                    new Vector3(new Vector3(0.0f, 0.0f, -0.5f)),
                    new Vector3(x, y, -0.5f),
                    new Vector3(nx, ny, -0.5f),
                    new Vector2(0.5f, 0.5f),
                    new Vector2(x + 0.5f, y + 0.5f),
                    new Vector2(nx + 0.5f, ny + 0.5f)
                ));
            }
        }
    }
}
