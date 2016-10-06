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
    class Camera : Entity
    {
        protected Vector3 Position;
        public Matrix4 mView;
        private ShaderUtility r_mShader;
        protected EKeyboard r_mKeyboard;

        public float MoveSpeed = 5;
        public bool Active;

        public Camera(ref Vector3 Pos, ref EKeyboard mKeyboard, ref ShaderUtility mShader)
        {
            this.Position = Pos;
            this.r_mKeyboard = mKeyboard;
            this.r_mShader = mShader;
            this.Active = false;
        }

        public bool isActive() { return this.Active; }

        public void Activate() { this.Active = true; }
        public void Deactivate() { this.Active = false; }

        public override void Init(Scene s)
        {
            base.Init(s);

            this.mView = Matrix4.CreateTranslation(this.Position);
            int uView = GL.GetUniformLocation(this.r_mShader.ShaderProgramID, "uView");
            GL.UniformMatrix4(uView, true, ref this.mView);

            int uProjectionLocation = GL.GetUniformLocation(this.r_mShader.ShaderProgramID, "uProjection");
            Matrix4 projection = Matrix4.CreateOrthographic(100, 100, -1, 1000);
            GL.UniformMatrix4(uProjectionLocation, true, ref projection);
        }

        protected void MoveCamera()
        {
            int uView = GL.GetUniformLocation(this.r_mShader.ShaderProgramID, "uView");
            GL.UniformMatrix4(uView, true, ref this.mView);
            foreach (Entity Light in this.scene.Entities)
            {
                if (Light.GetType().ToString() == "3DRenderer.PositionalLight")
                {
                    (Light as PositionalLight).SetLightPosition(this.mView);
                }
            }

            int uEyePosition = GL.GetUniformLocation(this.r_mShader.ShaderProgramID, "uEyePosition");
            Vector4 eye = new Vector4(this.mView.ExtractProjection());
            GL.Uniform4(uEyePosition, eye);
        }

        public override void Update(float gameTime)
        {
            if (this.isActive())
            {
                base.Update(gameTime);
                MoveCamera();
            }
        }
    }
}
