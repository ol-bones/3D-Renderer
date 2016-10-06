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
    class Vertex
    {
        public Vector3 Position;
        public Vector3 Colour;
        public Vector3 Normal;
        public Vector2 TexCoord;

        public float index;

        public Vertex(float x, float y, float z)
        {
            this.SetColour(new Vector3(255, 0, 0));
            this.Position = new Vector3(255, 0, 0);
            this.Normal = new Vector3(255, 0, 0);
            this.Set(x, y, z);
        }

        public Vertex(Vector3 p)
        {
            this.SetColour(new Vector3(255, 0, 0));
            this.Set(p);
        }

        public Vertex(Vector3 p, Vector2 t)
        {
            this.SetTexCoord(t);
            this.Set(p);
        }

        public Vertex(Vector3 p, Vector2 t, Vector3 n)
        {
            this.SetTexCoord(t);
            this.SetNormal(n);
            this.Set(p);
        }

        public void Set(float x, float y, float z)
        {
            this.Position.X = x;
            this.Position.Y = y;
            this.Position.Z = z;
        }

        public void Set(Vector3 p)
        {
            this.Position = new Vector3(p);
        }

        public void SetTexCoord(Vector2 t)
        {
            this.TexCoord = new Vector2(t);
        }

        public void SetTexCoord(float x, float y)
        {
            this.TexCoord.X = x;
            this.TexCoord.Y = y;
        }

        public Vector2 GetTexCoord()
        {
            return this.TexCoord;
        }

        public void SetColour(Vector3 c)
        {
            this.Colour = new Vector3(c);
        }

        public void SetColour(float r, float g, float b)
        {
            this.Colour.X = r;
            this.Colour.Y = g;
            this.Colour.Z = b;
        }

        public Vector3 GetColour()
        {
            return this.Colour;
        }

        public void SetNormal(Vector3 c)
        {
            this.Normal = new Vector3(c);
        }

        public void SetNormal(float x, float y, float z)
        {
            this.Normal.X = x;
            this.Normal.Y = y;
            this.Normal.Z = z;
        }

        public Vector3 GetNormal()
        {
            return this.Normal;
        }

        public void Rotate(Vector3 R)
        {
            Matrix4 rx = Matrix4.CreateRotationX(R.X);
            Matrix4 ry = Matrix4.CreateRotationY(R.Y);
            Matrix4 rz = Matrix4.CreateRotationZ(R.Z);

            Vector4 p = new Vector4(Position, 0);
            p = Vector4.Transform(p, rx);
            p = Vector4.Transform(p, ry);
            p = Vector4.Transform(p, rz);

            Set(p.X, p.Y, p.Z);
        }

        public void Print()
        {
            Console.WriteLine
            (
                "Position: {0},\nColour: {1},\nNormal: {2}",
                this.Position.ToString(),
                this.Colour.ToString(),
                this.Normal.ToString()
            );
        }
    }
}
