using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gane01
{
    class Texture2D
    {
        int m_TextureID;

        BitmapData TextureData;
        Bitmap TextureBitmap;

        ShaderUtility mShader;

        public Texture2D(string path, ref ShaderUtility shader)
        {
            mShader = shader;
            string wood = @"Lab5/windmill_wood_01.jpg";
            TextureBitmap = new Bitmap(wood);
            TextureData = TextureBitmap.LockBits(
                new Rectangle(0, 0, TextureBitmap.Width, TextureBitmap.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppRgb
            );
        }

        public void BindTexture()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.GenTextures(1, out m_TextureID);
            GL.BindTexture(TextureTarget.Texture2D, m_TextureID);
            GL.TexImage2D(TextureTarget.Texture2D,
            0, PixelInternalFormat.Rgba, TextureData.Width, TextureData.Height,
            0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
            PixelType.UnsignedByte, TextureData.Scan0);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
            (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
            (int)TextureMagFilter.Linear);
            TextureBitmap.UnlockBits(TextureData);
        }

        public void Uniforms()
        {
            int vTexCoords = GL.GetAttribLocation(mShader.ShaderProgramID, "vTexCoords");
            GL.EnableVertexAttribArray(vTexCoords);
            GL.VertexAttribPointer(vTexCoords, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * (sizeof(float)));

            int uTextureSamplerLocation = GL.GetUniformLocation(this.mShader.ShaderProgramID, "uTextureSampler");
            GL.Uniform1(uTextureSamplerLocation, 0);
        }
    }
}
