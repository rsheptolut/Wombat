
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLTest
{

    public class WPatternPlane
    {
        public Vector3 Position;
        public WTexture Texture;
        private int[][] Parts;
        const int PlateuSize = 40;
        const int PlateuHSize = PlateuSize / 2;

        public WPatternPlane(string TexturePath, Vector3 Position)
        {
            this.Position = Position;
            this.Texture = new WTexture(TexturePath);

            var Rnd = new Random();
            Parts = new int[PlateuSize][];
            for (int i = 0; i < PlateuSize; i++)
            {
                Parts[i] = new int[PlateuSize];
                for (int k = 0; k < PlateuSize; k++)
                    Parts[i][k] = 16 - (int)Math.Sqrt(Rnd.Next(289));
            }
        }

        public void Update(double Time)
        {
        }

        public void Render()
        {
            float texscale = 100;
            GL.BindTexture(TextureTarget.Texture2D, Texture.TextureId);
            GL.Begin(BeginMode.Quads);

            float texsizex = 1f / 8f;
            float texsizey = 1f / 4f;

            for (int i = 0; i < PlateuSize; i++)
            {
                for (int k = 0; k < PlateuSize; k++)
                {
                    int texpartx = Parts[i][k] / 4 + 4;
                    int texparty = Parts[i][k] % 4;
                    GL.TexCoord2((float)(texpartx + 0) * texsizex, (float)(texparty + 1) * texsizey);
                    GL.Vertex3((i - PlateuHSize + 0) * texscale, (k - PlateuHSize + 1) * texscale, 0);
                    GL.TexCoord2((float)(texpartx + 0) * texsizex, (float)(texparty + 0) * texsizey);
                    GL.Vertex3((i - PlateuHSize + 0) * texscale, (k - PlateuHSize + 0) * texscale, 0);
                    GL.TexCoord2((float)(texpartx + 1) * texsizex, (float)(texparty + 0) * texsizey);
                    GL.Vertex3((i - PlateuHSize + 1) * texscale, (k - PlateuHSize + 0) * texscale, 0);
                    GL.TexCoord2((float)(texpartx + 1) * texsizex, (float)(texparty + 1) * texsizey);
                    GL.Vertex3((i - PlateuHSize + 1) * texscale, (k - PlateuHSize + 1) * texscale, 0);
                }
            }
            GL.End();
        }
    }


  
}
