using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLTest
{
    public class WTexture: IDisposable
    {
        public string FilePath;
        public int TextureId;
        public int Width;
        public int Height;

        public WTexture(string FilePath, int Width = 0, int Height = 0)
        {
            this.FilePath = FilePath;

            if (String.IsNullOrEmpty(FilePath))
                throw new ArgumentException(FilePath);

            Bitmap bmp = new Bitmap(WResources.Instance.AssetRoot + "\\" + FilePath);

            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            TextureId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TextureId);

            if (Width == 0)
                Width = bmp_data.Width;
            if (Height == 0)
                Height = bmp_data.Height;
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapNearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            float maxAniso;
            GL.GetFloat((GetPName)ExtTextureFilterAnisotropic.MaxTextureMaxAnisotropyExt, out maxAniso);
            GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, maxAniso);
            GL.Ext.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            this.Width = Width;
            this.Height = Height;
        }

        public void Dispose()
        {
            GL.DeleteTexture(TextureId);
        }
    }
}
