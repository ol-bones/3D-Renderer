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
    class SphereMesh : TMesh
    {
        float Radius;

        public SphereMesh(float Radius, int ShaderID) : base()
        {
            this.ShaderID = ShaderID;
            GL.UseProgram(ShaderID);
            DrawType = PrimitiveType.Triangles;
            this.Radius = Radius;
            CreateSphere(60, 120);

            VertexBufferObject = new VBO(this);
        }

        /// <summary>
        ///  Expanded upon circle generation algorithm used in cylinder
        ///  with extra loop to calculate the tlY
        /// </summary>
        /// <param name="numSegments"></param>
        /// <param name="numHeightments"></param>
        public void CreateSphere(int numSegments, int numHeightments)
        {
            float anglePerHeightMent = (float)Math.PI / numHeightments;
            for (int i = 0; i < numHeightments; i++)
            {
                float yAngle = anglePerHeightMent * (i);
                float yAngle2 = anglePerHeightMent * (i+1);
                for (int j = 0; j < numSegments; j++)
                {
                    float anglePerSegment = (float)Math.PI * 2 / (numSegments);
                    float angle = anglePerSegment * j;

                    // top left vertex
                    float tlX = ((float)Math.Sin(yAngle) * (float)Math.Cos(angle));
                    float tlY = ((float)Math.Cos(yAngle));
                    float tlZ = ((float)Math.Sin(yAngle) * (float)Math.Sin(angle));

                    // top right vertex
                    float trX = ((float)Math.Sin(yAngle2) * (float)Math.Cos(angle));
                    float trZ = ((float)Math.Sin(yAngle2) * (float)Math.Sin(angle));

                    float angle2 = anglePerSegment * (j + 1);

                    // bottom left vertex
                    float blX = ((float)Math.Sin(yAngle) * (float)Math.Cos(angle2));
                    float blZ = ((float)Math.Sin(yAngle) * (float)Math.Sin(angle2));

                    // bottom right vertex
                    float brX = ((float)Math.Sin(yAngle2) * (float)Math.Cos(angle2));
                    float brY = ((float)Math.Cos(yAngle2));
                    float brZ = ((float)Math.Sin(yAngle2) * (float)Math.Sin(angle2));

                    AddTriangle(new Triangle(
                        new Vector3(new Vector3(tlX, tlY, tlZ)),
                        new Vector3(trX, brY, trZ),
                        new Vector3(brX, brY, brZ),
                        new Vector2(angle, tlY),
                        new Vector2(angle, tlY),
                        new Vector2(angle2, brY)
                    ));

                    AddTriangle(new Triangle(
                        new Vector3(blX, tlY, blZ),
                        new Vector3(new Vector3(tlX, tlY, tlZ)),
                        new Vector3(brX, brY, brZ),
                        new Vector2(angle2, tlY),
                        new Vector2(angle, tlY),
                        new Vector2(angle2, brY)
                    ));
                }
            }
           // Console.ForegroundColor = ConsoleColor.Red;
           // Console.WriteLine(this.Triangles.Count + " triangles in sphere");
           // Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
