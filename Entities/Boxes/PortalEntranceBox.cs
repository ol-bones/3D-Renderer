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
    class PortalEntranceBox : Box
    {

        public PortalEntranceBox(Vector3 pos)
            : base(pos)
        {

        }

        public override void Init(Scene s)
        {
            base.Init(s);

            this.Mesh.SetTexture("reddots_texture.png");
            this.Mesh.Triangles.RemoveRange(10, 2);
            this.SetScale(new Vector3(1.0f, 1.0f, 1.0f));
            this.Mesh.UpdateVBO();
        }

        public override void DetectCollision(PhysicalEntity oEntity)
        {
            base.DetectCollision(oEntity);

            if (oEntity.GetType() == (typeof(Ball)))
            {
                Ball oBall = oEntity as Ball;
                if (oBall.Position.Y - oBall.Radius <= this.Position.Y - 0.5f)
                {
                    oBall.Position.Y = this.Position.Y - 0.5f + 0.05f;
                    oBall.Velocity.Y *= -1;
                }
            }
        }


        public override void Update(float gameTime)
        {
            base.Update(gameTime);
        }
    }
}
