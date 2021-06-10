using MdxLib.Model;
using MdxLib.ModelFormats;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLTest
{
    public class WModelSource
    { 
        public CModel Model;
        public string FilePath;

        public WModelSource(string FilePath)
        {
            this.FilePath = FilePath.ToLower();
            Model = LoadModel(WResources.Instance.AssetRoot, FilePath);
        }

        private CModel LoadModel(string RootDir, string FileName)
        {
            Model = LoadFromFile(RootDir, FileName);

            LoadTextures();
            
            return Model;
        }

        public CModel LoadFromFile(string RootDir, string FileName)
        {
            CModel Result = new CModel();
            using (var ModelFS = new FileStream(RootDir + "\\" + FileName, FileMode.Open, FileAccess.Read))
            {
                IModelFormat ModelFormatLoader;
                if (Path.GetExtension(FileName).ToLower() == ".mdx")
                    ModelFormatLoader = new CMdx();
                else if (Path.GetExtension(FileName).ToLower() == ".mdl")
                    ModelFormatLoader = new CMdl();
                else
                    throw new Exception("Unsupported model format. File: " + FileName);

                ModelFormatLoader.Load(RootDir + "\\" + FileName, ModelFS, Result);
            }
            return Result;
        }

        public void LoadTextures()
        {
            foreach (var Texture in Model.Textures)
            {
                if (Texture.ReplaceableId != 0)
                {
                    string TextureName = "";
                    switch (Texture.ReplaceableId)
                    {
                        case 1:
                            TextureName = "TeamColor\\TeamColor01.blp";
                            break;
                        case 2:
                            TextureName = "TeamGlow\\TeamGlow01.blp";
                            break;
                    }
                    if (TextureName != "")
                        Texture.FileName = "ReplaceableTextures\\" + TextureName;
                }
                if (Texture.FileName != "")
                {
                    string FName = Path.ChangeExtension(Texture.FileName, ".png");
                    WTexture TextureObj = WResources.Instance.GetTexture(FName);
                    Texture.Tag = TextureObj;
                }
                else Texture.Tag = null;
            }
        }
    }
}
