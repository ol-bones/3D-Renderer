using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace 3DRenderer
{
    class PathCamera : Camera
    {
        private List<Vector3> Path;
        private int CurPath;

        public PathCamera(ref Vector3 Pos, ref EKeyboard mKeyboard, ref ShaderUtility mShader)
            : base(ref Pos, ref mKeyboard, ref mShader)
        {
            this.Path = new List<Vector3>();
            this.CurPath = 0;
        }

        public override void Init(Scene s)
        {
            base.Init(s);
        }

        public void AddPath(Vector3 p)
        {
            Path.Add(p);
        }

        private void MoveToNextPath(float gameTime)
        {
            Matrix4 moveMatrix = this.mView;
            Vector3 L =  (moveMatrix.ExtractTranslation() - this.Path[this.CurPath]);
            float Dist = L.Length;
            if (Dist > 0.1f)
            {
                moveMatrix *= Matrix4.CreateTranslation(-L.Normalized()/(100-MoveSpeed*gameTime));
                this.mView = moveMatrix;
            }
            else
            {
                this.CurPath++;
                if (this.CurPath >= this.Path.Count)
                {
                    this.CurPath = 0;
                }
            }
        }

        public override void Update(float gameTime)
        {
            if (this.isActive())
            {
                base.Update(gameTime);
                if (this.Path.Count > 0)
                {
                    MoveToNextPath(gameTime);
                }
            }
        }
    }
}
