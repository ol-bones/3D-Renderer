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
    class EmitterBox : Box
    {

        public EmitterBox(Vector3 pos) : base(pos)
        {

        }

        public override void Init(Scene s)
        {
            base.Init(s);

            //this.scene = s;

            this.Mesh.uMaterial.SetAmbientReflectivity(
                new Vector3(1.0f, 1.0f, 1.0f));

            this.Mesh.uMaterial.SetDiffuseReflectivity(
                new Vector3(1.0f, 1.0f, 1.0f)
                );

            this.Mesh.Triangles.RemoveRange(4,2);

            this.Mesh.SetTexture("reddots_texture.png");

            this.Mesh.UpdateVBO();

            CreateYellowBall();
            CreateGreenBall();
        }

        private void CreateYellowBall()
        {
            Ball newYellowBall = new Ball(
                this.Position - new Vector3(0.2f, 0.0f, 0.0f),
                0.05f
            );

            newYellowBall.Mesh.uMaterial.SetDiffuseReflectivity
            (
                new Vector3(0.0f, 1.0f, 0.0f)
            );
            newYellowBall.Mesh.uMaterial.SetAmbientReflectivity
            (
                new Vector3(3.0f, 3.0f, 0.0f)
            );

            scene.Add(newYellowBall);

            newYellowBall.SetVelocity(new Vector3(
                0.0f,
                0.0f,
                0.0f
            ));
        }

        private void CreateGreenBall()
        {
            Ball newGreenBall = new Ball(
                this.Position - new Vector3(-0.2f, 0.0f, 0.0f),
                0.08f
            );
            newGreenBall.Mesh.uMaterial.SetDiffuseReflectivity
            (
                new Vector3(0.0f, 1.0f, 0.0f)
            );
            newGreenBall.Mesh.uMaterial.SetAmbientReflectivity
            (
                new Vector3(0.0f, 1.0f, 0.0f)
            );

            scene.Add(newGreenBall);
            
            newGreenBall.SetVelocity(new Vector3(
                1.0f,
                0.0f,
                0.2f
            ));
        }

        public override void DetectCollision(PhysicalEntity oEntity)
        {

            if (oEntity.GetType() == (typeof(Ball)))
            {
                Ball oBall = oEntity as Ball;
                if (oBall.Position.Y + oBall.Radius >= this.Position.Y + 0.5f)
                {
                    oBall.Position.Y = this.Position.Y + 0.5f - oBall.Radius;
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
