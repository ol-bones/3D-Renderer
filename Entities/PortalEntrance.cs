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
    class PortalEntrance : Box
    {
        PortalExit Exit;
        float Spin = 0;

        public PortalEntrance(Vector3 pos, PortalExit exit)
            : base(pos)
        {
            this.Exit = exit;
        }

        public override void Init(Scene s)
        {
            base.Init(s);
            this.Mesh.uMaterial.SetDiffuseReflectivity(
                new Vector3(0.0f, 0.0f, 0.0f)
            );
            this.Mesh.SetTexture("portal_texture.png");
            this.Mesh.Triangles.RemoveRange(0, 4);
            this.Mesh.Triangles.RemoveRange(2, 6);
            this.SetScale(new Vector3(0.75f, 1.0f, 0.75f));
            this.Mesh.UpdateVBO();
        }

        public override void DetectCollision(PhysicalEntity oEntity)
        {
            if (oEntity.GetType() == (typeof(Ball)))
            {
                Ball oBall = oEntity as Ball;

                if (oBall.Position.Y - oBall.Radius <= this.Position.Y - this.Scale.Y/2)
                {
                    if (oBall.Position.X  <= this.Position.X - this.Scale.X/2)
                    {
                        return;
                    }
                    else
                    if (oBall.Position.X >= this.Position.X + this.Scale.X/2)
                    {
                        return;
                    }
                    if (oBall.Position.Z  <= this.Position.Z - this.Scale.Z/2)
                    {
                        return;
                    }
                    else
                    if (oBall.Position.Z >= this.Position.Z + this.Scale.Z/2)
                    {
                        return;
                    }
                    this.Exit.Teleport(oBall);
                }
            }

        }

        public override void Update(float gameTime)
        {
            base.Update(gameTime);
            this.Spin += 0.01f;
            Matrix2 mat = Matrix2.CreateRotation(Spin);
            this.Mesh.uMaterial.SetSpin(mat);
        }
    }
}
