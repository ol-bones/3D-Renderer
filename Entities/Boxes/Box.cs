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
    class Box : PhysicalEntity
    {

        public Box(Vector3 pos) : base(pos)
        {
            this.Mesh = new CubeMesh(3);
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

            if (oEntity.GetType() == (typeof(Ball)))
            {
                Ball oBall = oEntity as Ball;
                if (oBall.Position.X - oBall.Radius <= this.Position.X - 0.5f)
                {
                    oBall.Position.X = this.Position.X - 0.5f + oBall.Radius;
                    oBall.Velocity.X *= -1;
                }
                else
                if (oBall.Position.X + oBall.Radius >= this.Position.X + 0.5f)
                {
                    oBall.Position.X = this.Position.X + 0.5f - oBall.Radius;
                    oBall.Velocity.X *= -1;
                }
                if (oBall.Position.Z - oBall.Radius <= this.Position.Z - 0.5f)
                {
                    oBall.Position.Z = this.Position.Z - 0.5f + oBall.Radius;
                    oBall.Velocity.Z *= -1;
                }
                else
                if (oBall.Position.Z + oBall.Radius >= this.Position.Z + 0.5f)
                {
                    oBall.Position.Z = this.Position.Z + 0.5f - oBall.Radius;
                    oBall.Velocity.Z *= -1;
                }
            }
            
        }

        public override void Update(float gameTime)
        {
            base.Update(gameTime);

        }
        
    }
}
