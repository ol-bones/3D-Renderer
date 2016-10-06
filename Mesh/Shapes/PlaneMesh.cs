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
    class PlaneMesh : TMesh
    {
        public PlaneMesh(int ShaderID) : base()
        {
            this.ShaderID = ShaderID;
            DrawType = PrimitiveType.Triangles;

            AddTriangle(new Triangle(
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, -1.0f),
                new Vector3(-1.0f, 1.0f, -1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(1.0f, 0.0f),
                new Vector2(0.0f, 0.0f)
            ));

            AddTriangle(new Triangle(
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(-1.0f, 1.0f, -1.0f),
                new Vector3(-1.0f, 1.0f, 1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(0.0f, 0.0f),
                new Vector2(0.0f, 1.0f)
            ));

            VertexBufferObject = new VBO(this);
        }
    }
}
