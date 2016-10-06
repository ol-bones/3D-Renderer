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
    class GridBox1 : Box
    {
        public GridBox1(Vector3 pos) : base(pos)
        {

        }

        public override void Init(Scene s)
        {
            base.Init(s);

            this.Mesh.Triangles.RemoveRange(4, 2);
            this.Mesh.Triangles.RemoveRange(8, 2);

            this.Mesh.SetTexture("reddots_texture.png");

            this.Mesh.UpdateVBO();
        }

        public override void DetectCollision(PhysicalEntity oEntity)
        {
            base.DetectCollision(oEntity);
        }


        public override void Update(float gameTime)
        {
            base.Update(gameTime);
        }
    }
}
