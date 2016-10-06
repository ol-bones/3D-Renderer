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
    class DirectionalLight : Entity
    {
        private Vector3 Direction, NormalisedDirection, Colour;
        private float Strength;
        private int Index;

        public DirectionalLight(Vector3 d, Vector3 c, float s, int Shader)
        {
            this.Direction = new Vector3(d);
            this.Colour = new Vector3(c);
            this.Strength = s;
            this.ShaderID = Shader;

        }

        public override void Init(Scene s)
        {
            base.Init(s);

            this.Index = scene.num_DirLights;
            scene.num_DirLights++;

            int uLightDirectionLocation = GL.GetUniformLocation(this.ShaderID, "uDirectionalLights[" + this.Index + "].LightDirection");

            Vector3.Normalize(ref this.Direction, out this.NormalisedDirection);
            GL.Uniform3(uLightDirectionLocation, this.NormalisedDirection);

            int uLightColour = GL.GetUniformLocation(this.ShaderID, "uDirectionalLights[" + this.Index + "].LightColour");
            GL.Uniform3(uLightColour, this.Colour);

            int uLightStrength = GL.GetUniformLocation(this.ShaderID, "uDirectionalLights[" + this.Index + "].LightStrength");
            GL.Uniform1(uLightStrength, this.Strength);
        }
    }
}
