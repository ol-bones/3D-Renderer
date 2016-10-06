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
    class AmbientLight : Entity
    {
        Vector3 Colour;
        private float Strength;
        private int Index;

        public AmbientLight(Vector3 c, float s, int Shader)
        {
            this.Colour = c;
            this.Strength = s;
            this.ShaderID = Shader;
        }

        public override void Init(Scene s)
        {
            base.Init(s);

            this.Index = scene.num_Ambientights;
            scene.num_Ambientights++;

            int uLightColour = GL.GetUniformLocation(this.ShaderID, "uAmbientLights[" + this.Index + "].LightColour");
            GL.Uniform3(uLightColour, this.Colour);

            int uLightStrength = GL.GetUniformLocation(this.ShaderID, "uAmbientLights[" + this.Index + "].LightStrength");
            GL.Uniform1(uLightStrength, this.Strength);
        }
    }
}
