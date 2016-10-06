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
    class FixedCamera : Camera
    {
        public FixedCamera(ref Vector3 Pos, ref EKeyboard mKeyboard, ref ShaderUtility mShader) : base(ref Pos, ref mKeyboard, ref mShader)
        {

        }

        public override void Init(Scene s)
        {
            base.Init(s);
        }
    }
}
