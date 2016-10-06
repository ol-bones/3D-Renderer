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
    class PortalExit : Box
    {
        float Spin = 0;

        public PortalExit(Vector3 pos)
            : base(pos)
        {

        }

        public override void Init(Scene s)
        {
            base.Init(s);
            this.Mesh.uMaterial.SetDiffuseReflectivity(
                new Vector3(0.0f, 0.0f, 0.0f)
            );
            this.Mesh.SetTexture("portal_texture.png");
            this.Mesh.Triangles.RemoveRange(2, 4);
            this.Mesh.Triangles.RemoveRange(2, 6);
            this.SetScale(new Vector3(1.0f, 0.75f, 0.75f));
            this.Mesh.UpdateVBO();
        }

        public void Teleport(Ball ball)
        {
            ball.SetPosition(
                new Vector3(
                    ball.Position.Z * this.Scale.Z,
                    ball.Position.X * this.Scale.X,
                    this.Position.Z
                )
            );
            ball.SetVelocity(new Vector3(-ball.Velocity.Y, ball.Velocity.X, ball.Velocity.Z));
        }

        public override void DetectCollision(PhysicalEntity oEntity){}

        public override void Update(float gameTime)
        {
            base.Update(gameTime);
            this.Spin += 0.01f;
            Matrix2 mat = Matrix2.CreateRotation(Spin);
            this.Mesh.uMaterial.SetSpin(mat);
        }
    }
}
