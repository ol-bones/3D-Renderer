using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace 3DRenderer
{
    class Texture2D
    {
        public int m_TextureID;

        BitmapData TextureData;
        Bitmap TextureBitmap;
        public string name;

        public Texture2D(string name)
        {
            this.name = name;
            string texture_path = @"Content/Textures/" + name;
            TextureBitmap = new Bitmap(texture_path);
            TextureBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            TextureData = TextureBitmap.LockBits(
                new Rectangle(0, 0, TextureBitmap.Width, TextureBitmap.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppRgb
            );
        }

        public Texture2D()
        {
            string texture_path = @"Content/Textures/black.png";
            name = texture_path;
            TextureBitmap = new Bitmap(texture_path);
            TextureBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            TextureData = TextureBitmap.LockBits(
                new Rectangle(0, 0, TextureBitmap.Width, TextureBitmap.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppRgb
            );
        }

        public void BindTexture(ref int ShaderID)
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

        public void Uniforms(ref int ShaderID)
        {
            int vTexCoords = GL.GetAttribLocation(ShaderID, "vTexCoords");
            GL.VertexAttribPointer(vTexCoords, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * (sizeof(float)));
            GL.EnableVertexAttribArray(vTexCoords);

            int uTextureSamplerLocation = GL.GetUniformLocation(ShaderID, "uTextureSampler");
            GL.Uniform1(uTextureSamplerLocation, 0);
        }
    }
    
}
