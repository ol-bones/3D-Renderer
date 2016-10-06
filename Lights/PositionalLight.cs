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
    class PositionalLight : Entity
    {
        Vector4 Position;
        Vector3 Colour;
        private float Strength;
        private int Index;

        public PositionalLight(Vector4 p, Vector3 c, float s, int Shader)
        {
            this.Position = new Vector4(p);
            this.Colour = new Vector3(c);
            this.Strength = s;
            this.ShaderID = Shader;
        }

        public override void Init(Scene s)
        {
            base.Init(s);

            this.Index = scene.num_PosLights;
            scene.num_PosLights++;

            int uLightColour = GL.GetUniformLocation(this.ShaderID, "uPositionalLights[" + this.Index + "].LightColour");
            GL.Uniform3(uLightColour, this.Colour);

            int uLightStrength = GL.GetUniformLocation(this.ShaderID, "uPositionalLights[" + this.Index + "].LightStrength");
            GL.Uniform1(uLightStrength, this.Strength);

        }

        public void SetLightPosition(Matrix4 mView)
        {
            Vector4 tPosition = Vector4.Transform(this.Position, mView);

            int uLightPosition = GL.GetUniformLocation(this.ShaderID, "uPositionalLights[" + this.Index + "].LightPosition");
            GL.Uniform4(uLightPosition, tPosition);
        }
    }
}
