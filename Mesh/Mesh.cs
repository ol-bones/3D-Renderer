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
    class Triangle
    {
        public List<Vertex> Vertices;
        public Vector3 Normal, colour;

        public Triangle(Vector3 v1, Vector3 v2, Vector3 v3, Vector2 t1, Vector2 t2, Vector2 t3 )
        {
            Vertices = new List<Vertex>();

            this.Vertices.Add(new Vertex(v3, t3));
            this.Vertices.Add(new Vertex(v2, t2));
            this.Vertices.Add(new Vertex(v1, t1));

            SetNormal(calcNormal());
            this.colour = new Vector3(1, 1, 1);
        }

        private void SetNormal(Vector3 n)
        {
            foreach (Vertex v in Vertices)
            {
                v.Normal = n;
            }
            this.Normal = n;
        }

        private Vector3 calcNormal() // thanks to opengl wiki - https://www.opengl.org/wiki/Calculating_a_Surface_Normal
        {
            Vector3 u = this.Vertices[1].Position - this.Vertices[0].Position;
            Vector3 v = this.Vertices[2].Position - this.Vertices[0].Position;

            return Vector3.Cross(u, v).Normalized();
        }
    }

    class TMesh : Mesh
    {
        public TMesh()
            : base()
        {
            this.Triangles = new List<Triangle>();
        }

        public override int VertexCount()
        {
            return Triangles.Count * 3;
        }

        public void AddTriangle(Triangle t)
        {
            Triangles.Add(t);
            Indices.Add(Indices.Count);
            Indices.Add(Indices.Count);
            Indices.Add(Indices.Count);
        }

        public override float[] GetVertexData()
        {
            List<float> f = new List<float>();
            foreach (Triangle t in Triangles)
            {
                foreach (Vertex v in t.Vertices)
                {
                    f.Add(v.Position.X);
                    f.Add(v.Position.Y);
                    f.Add(v.Position.Z);

                    f.Add(v.Normal.X);
                    f.Add(v.Normal.Y);
                    f.Add(v.Normal.Z);

                    f.Add(v.TexCoord.X);
                    f.Add(v.TexCoord.Y);
                }
            }
            return f.ToArray();
        }
    }
    /// <summary>
    /// This is meant to be a generic class for meshes
    /// with different draw types and structures i.e. trianglefan
    /// etc, but I didn't use them - only Triangles with TMesh
    /// </summary>
    class Mesh
    {
        public List<Vertex> Vertices;
        public List<float> Indices;
        public List<Triangle> Triangles;

        public PrimitiveType DrawType;
        public VBO VertexBufferObject;

        public Material uMaterial;
        public Texture2D uTexture;

        public Vector3 Origin, Scale;
        public int ShaderID;

        public Mesh()
        {
            this.uTexture = new Texture2D();
            this.Vertices = new List<Vertex>();
            this.Indices = new List<float>();
            this.Origin = new Vector3(0.0f, 0.0f, 0.0f);
            this.Scale = new Vector3(1.0f, 1.0f, 1.0f);

            this.uMaterial = (new Material()).Default();
        }

        public void SetScale(Vector3 s)
        {
            this.Scale = s;
        }

        public void SetTexture(string path)
        {
            this.uTexture = new Texture2D(path);
            this.VertexBufferObject.Load();
        }

        public virtual void Rotate(Vector3 r)
        {

        }

        public virtual int VertexCount()
        {
            switch (DrawType)
            {
                case PrimitiveType.TriangleFan:
                    return Vertices.Count;
                case PrimitiveType.Triangles:
                    return Triangles.Count * 3;
                default:
                    throw new Exception("Draw type unspecified");
            }
        }

        public void AddVertex(Vector3 v, Vector2 t, Vector3 n)
        {
            float ind;

            Vertex newVertex = new Vertex(v, t, n);
            Vertices.Add(newVertex);

            ind = (float)Indices.Count;
            Indices.Add(ind);
            newVertex.index = ind;
        }

        /// <summary>
        /// Returns a float array containing vertex data in the form:
        /// vPosition  x, y, z
        /// vNormal    x, y, z
        /// vTexCoords x, y
        /// </summary>
        /// <returns></returns>
        public virtual float[] GetVertexData()
        {
            List<float> f = new List<float>();
            foreach (Vertex v in this.Vertices)
            {
                f.Add(v.Position.X);
                f.Add(v.Position.Y);
                f.Add(v.Position.Z);

                f.Add(v.Normal.X);
                f.Add(v.Normal.Y);
                f.Add(v.Normal.Z);

                f.Add(v.TexCoord.X);
                f.Add(v.TexCoord.Y);
            }
            return f.ToArray();
        }

        // If the vertices changed / texture changed
        // then this was useful mostly for debugging / when attempting to make the shapes
        public void UpdateVBO()
        {
            if (this.VertexBufferObject == null) { return; }
            this.VertexBufferObject.SetVertexBufferData(this.GetVertexData());
        }

        public void Update(float gameTime)
        {
        }

        public void Delete()
        {
            this.Vertices.Clear();
            this.Indices.Clear();
            this.Triangles.Clear();
            this.Origin = this.Scale = Vector3.Zero;
            this.ShaderID = -1;
            this.VertexBufferObject.Delete();
        }

        public void Draw(Matrix4 Matrix)
        {
            GL.UseProgram(ShaderID);
            VertexBufferObject.Draw(Matrix);
        }
    }
}
