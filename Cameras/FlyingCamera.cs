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
    class FlyingCamera : Camera
    {
        public Key KEY_W, KEY_A, KEY_S, KEY_D, KEY_SPACE, KEY_LALT;
        
        public FlyingCamera(ref Vector3 Pos, ref EKeyboard mKeyboard, ref ShaderUtility mShader)
            : base(ref Pos, ref mKeyboard, ref mShader)
        {
        }

        public override void Init(Scene s)
        {
            base.Init(s);

            this.KEY_W = this.r_mKeyboard.Key("W");
            this.KEY_A = this.r_mKeyboard.Key("A");
            this.KEY_S = this.r_mKeyboard.Key("S");
            this.KEY_D = this.r_mKeyboard.Key("D");
            this.KEY_SPACE = this.r_mKeyboard.Key("Space");
            this.KEY_LALT = this.r_mKeyboard.Key("LAlt");
        }

        private void FlyControls(float gameTime)
        {
            Matrix4 fly_Matrix = mView;
            if (KEY_W.KeyDown())
            {
                fly_Matrix = fly_Matrix * Matrix4.CreateTranslation(0.0f, 0.0f, MoveSpeed * gameTime);
            }
            if (KEY_A.KeyDown())
            {
                fly_Matrix = fly_Matrix * Matrix4.CreateRotationY(-MoveSpeed * gameTime);
            }
            if (KEY_S.KeyDown())
            {
                fly_Matrix = fly_Matrix * Matrix4.CreateTranslation(0.0f, 0.0f, -MoveSpeed * gameTime);
            }
            if (KEY_D.KeyDown())
            {
                fly_Matrix = fly_Matrix * Matrix4.CreateRotationY(MoveSpeed * gameTime);
            }
            if (KEY_SPACE.KeyDown())
            {
                fly_Matrix = fly_Matrix * Matrix4.CreateTranslation(0.0f, -MoveSpeed * gameTime, 0.0f);
            }
            if (KEY_LALT.KeyDown())
            {
                fly_Matrix = fly_Matrix * Matrix4.CreateTranslation(0.0f, MoveSpeed * gameTime, 0.0f);
            }
            this.mView = fly_Matrix;
        }

        public override void Update(float gameTime)
        {
            if (this.isActive())
            {
                base.Update(gameTime);
                FlyControls(gameTime);
            }
        }
    }
}
