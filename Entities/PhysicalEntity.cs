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
    class PhysicalEntity : Entity
    {
        public Vector3 Position, Scale, Velocity;
        protected Quaternion Rotation;
        public Mesh Mesh;

        public PhysicalEntity(Vector3 pos) : base()
        {
            this.Position = pos;
            this.Velocity = new Vector3(0.0f, 0.0f, 0.0f);
            this.Rotation = new Quaternion();
            Rotate(new Vector3(0,0,0));
            this.Scale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        public void SetScale(Vector3 s)
        {
            this.Scale = s;
        }

        public override void Init(Scene s)
        {
            base.Init(s);
        }

        public void SetPosition(Vector3 p)
        {
            this.Position = p;
        }

        public void SetVelocity(Vector3 v)
        {
            this.Velocity = v;
        }

        public virtual void OnCollide(PhysicalEntity e, Vector3 n)
        {

        }

        public virtual void DetectCollision(PhysicalEntity oEntity)
        {

        }

        public void Rotate(Vector3 Rotation)
        {
            this.Rotation = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), Rotation.Y);
        }

        public void SetRotation(Vector3 Rotation)
        {
        }

        public Matrix4 GetMatrix()
        {
            return Matrix4.CreateScale(this.Scale) * Matrix4.CreateFromQuaternion(this.Rotation) * Matrix4.CreateTranslation(this.Position); // *Rotation;
        }

        public override void Update(float gameTime)
        {
            base.Update(gameTime);

            this.SetPosition(this.Position + (this.Velocity * gameTime));
        }

        public override void Delete()
        {
            base.Delete();
            this.Position = this.Scale = this.Velocity = Vector3.Zero;
            this.Rotation = Quaternion.Identity;
            this.Mesh.Delete();
            this.Mesh = null;
        }

        public void Draw()
        {
            this.Mesh.Draw(this.GetMatrix());
        }
    }
}
