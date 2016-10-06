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
    class Ball : PhysicalEntity
    {
        public float Radius;

        public Ball(Vector3 pos, float radius) : base(pos)
        {
            this.Radius = radius;
            this.SetScale(new Vector3(radius, radius, radius));

            this.Mesh = new SphereMesh(radius, 3);
        }

        public override void Init(Scene s)
        {
            base.Init(s);
        }

        public override void OnCollide(PhysicalEntity e, Vector3 n)
        {
            base.OnCollide(e, n);
            this.SetVelocity(this.Velocity - 2 * Vector3.Dot(n, this.Velocity) * n);
        }

        public override void DetectCollision(PhysicalEntity oEntity)
        {
            base.DetectCollision(oEntity);
            
            float Distance = (this.Position - oEntity.Position).Length;
            Vector3 normal = (this.Position - oEntity.Position).Normalized();

            if (oEntity.GetType() == (typeof(Ball)))
            {
                Ball oBall = oEntity as Ball;
                if (Distance <= this.Radius + oBall.Radius)
                {
                    this.OnCollide(oEntity, normal);
                }
            }
        }

        public override void Update(float gameTime)
        {
            base.Update(gameTime);
            this.Velocity += (scene.Gravity * gameTime);
        }
        
    }
}
