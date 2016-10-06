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
    class VBO
    {
        public Mesh mesh;
        int size;
        int[] m_VBO = new int[2];
        int m_VAO;
        int shader;

        public VBO(Mesh m)
        {
            shader = m.ShaderID;

            this.mesh = m;
            init();
            Load();
        }

        public void init()
        {
            m_VAO = GL.GenVertexArray();
            GL.BindVertexArray(m_VAO);

            GL.GenBuffers(2, m_VBO);
        }

        public void Load()
        {
            GL.BindVertexArray(m_VAO);

            BindTexture();

            SetVertexBufferData(mesh.GetVertexData());
            SetIndexBufferData(mesh.Indices.ToArray());

            int vPositionLocation = GL.GetAttribLocation(shader, "vPosition");
            int vNormalLocation = GL.GetAttribLocation(shader, "vNormal");

            GL.VertexAttribPointer(vPositionLocation, 3, VertexAttribPointerType.Float, false, 8 *
            sizeof(float), 0);
            GL.EnableVertexAttribArray(vPositionLocation);
            
            GL.VertexAttribPointer(vNormalLocation, 3, VertexAttribPointerType.Float, true, 8 *
            sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(vNormalLocation);

            SetTextureUniform();

            GL.BindVertexArray(0);
        }

        public void BindTexture()
        {
            this.mesh.uTexture.BindTexture(ref shader);
        }

        public void SetTextureUniform()
        {
            this.mesh.uTexture.Uniforms(ref shader);
        }

        public void SetVertexBufferData(float[] verts)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_VBO[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts.Length * sizeof(float)), verts, BufferUsageHint.StaticDraw); 
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);
            if (verts.Length * sizeof(float) != size)
            {
                throw new ApplicationException("Vertex data not loaded onto graphics card correctly");
            }
        }

        private void SetIndexBufferData(float[] indcs)
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_VBO[1]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indcs.Length * sizeof(float)), indcs, BufferUsageHint.StaticDraw); 
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
            if (indcs.Length * sizeof(float) != size)
            {
                throw new ApplicationException("Index data not loaded onto graphics card correctly");
            }
        }

        public void UseMaterial(ref Material uMaterial)
        {
            int AmbientReflectivity = GL.GetUniformLocation(shader, "uMaterial.AmbientReflectivity");
            int DiffuseReflectivity = GL.GetUniformLocation(shader, "uMaterial.DiffuseReflectivity");
            int SpecularReflectivity = GL.GetUniformLocation(shader, "uMaterial.SpecularReflectivity");

            int Shininess = GL.GetUniformLocation(shader, "uMaterial.Shininess");
            int Opacity = GL.GetUniformLocation(shader, "uMaterial.Opacity");
            int Spin = GL.GetUniformLocation(shader, "uMaterial.Spin");

            GL.Uniform3(AmbientReflectivity, uMaterial.AmbientReflectivity);
            GL.Uniform3(DiffuseReflectivity, uMaterial.DiffuseReflectivity);
            GL.Uniform3(SpecularReflectivity, uMaterial.SpecularReflectivity);

            GL.Uniform1(Shininess, uMaterial.Shininess);
            GL.Uniform1(Opacity, uMaterial.Opacity);
            GL.UniformMatrix2(Spin, false, ref uMaterial.Spin);
        }

        public void Delete()
        {
            this.size = -1;
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.DeleteBuffers(m_VBO.Length, m_VBO);
            GL.DeleteVertexArray(m_VAO);

            this.m_VBO = new int[0];
            this.m_VAO = -1;
            this.shader = -1;
        }

        public void Draw(Matrix4 Matrix)
        {
            int uModel = GL.GetUniformLocation(shader, "uModel");
            UseMaterial(ref this.mesh.uMaterial);
            GL.UniformMatrix4(uModel, true, ref Matrix);
            GL.BindVertexArray(m_VAO);
            GL.BindTexture(TextureTarget.Texture2D, mesh.uTexture.m_TextureID);

            GL.DrawArrays(this.mesh.DrawType, 0, this.mesh.VertexCount());

            GL.BindVertexArray(0);
        }
    }
}
