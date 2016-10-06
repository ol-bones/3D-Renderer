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
    class BallOfDoom : Ball
    {
        float Spin = 0;
        public BallOfDoom(Vector3 pos, float radius): base(pos, radius)
        {

        }

        public override void Init(Scene s)
        {
            base.Init(s);
            this.Mesh.uMaterial.SetDiffuseReflectivity(
                new Vector3(1.0f, 1.0f, 1.0f)
            );
            this.Mesh.SetTexture("cloudy_texture.png");
            this.Mesh.uMaterial.SetOpacity(0.5f);
        }

        public override void OnCollide(PhysicalEntity e, Vector3 n)
        {
            base.OnCollide(e, n);
            Ball oBall = e as Ball;
            if (oBall.Radius <= 0)
            {
                scene.Remove(oBall);
            }
            else
            {
                oBall.Radius -= 0.01f;
                Vector3 scl = new Vector3(oBall.Radius, oBall.Radius, oBall.Radius);
                oBall.SetScale(scl);
            }
            Vector3 newPosition = oBall.Position + (n*oBall.Radius);
            e.SetPosition(newPosition);
        }

        public override void Update(float gameTime)
        {
            this.Spin += 0.01f;
            Matrix2 mat = Matrix2.CreateRotation(Spin);
            this.Mesh.uMaterial.SetSpin(mat);
        }
    }
}
