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
    class Cylinder : PhysicalEntity
    {
        float Radius;

        public Cylinder(Vector3 pos, float radius) : base(pos)
        {
            this.Radius = radius;

            this.Mesh = new CylinderMesh(radius, 3);
            this.Mesh.SetScale(new Vector3(radius, radius, radius));
            this.Mesh.SetTexture("wood_texture.png");
        }

        public override void Init(Scene s)
        {
            base.Init(s);
        }

        public override void OnCollide(PhysicalEntity e, Vector3 n)
        {
            base.OnCollide(e, n);
        }

        public override void DetectCollision(PhysicalEntity oEntity)
        {
            base.DetectCollision(oEntity);

            if (oEntity.GetType() == typeof(Ball))
            {
                Ball ball = oEntity as Ball;

                Vector3 L1 = Vector3.Transform(new Vector3(0, 0, -0.5f), this.GetMatrix());
                Vector3 L2 = Vector3.Transform(new Vector3(0, 0, 0.5f), this.GetMatrix());

                Vector3 normal = (L2 - L1).Normalized();
                Vector3 A = Vector3.Dot(ball.Position - L2, normal) * normal;
                Vector3 F = L2 + A - ball.Position;
                                                   
                if (F.Length <= this.Radius + ball.Radius)
                {
                    ball.SetVelocity(ball.Velocity - 2 * Vector3.Dot(F.Normalized(), ball.Velocity) * F.Normalized());
                }
            }
        }

        public override void Update(float gameTime)
        {
            base.Update(gameTime);
        }
    }
}
