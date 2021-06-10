using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLTest
{
    public class WResources: IDisposable
    {
        public string AssetRoot { get; private set; }
        public List<WModelSource> Models = new List<WModelSource>();
        public List<WTexture> Textures = new List<WTexture>();
        public static WResources Instance { get; private set; }

        public WResources(string AssetRoot)
        {
            WResources.Instance = this;
            this.AssetRoot = AssetRoot;
        }
        public void Dispose()
        {
            foreach (var Texture in Textures)
                Texture.Dispose();
        }


        internal WModelSource GetModel(string ModelPath)
        {
            ModelPath = ModelPath.ToLower();
            WModelSource Model = Models.Find(Item => Item.FilePath == ModelPath);
            if (Model == null)
            {
                Model = new WModelSource(ModelPath);
                Models.Add(Model);
            }
            return Model;
        }

        public WTexture GetTexture(string FName)
        {          
            WTexture TextureObj = Textures.Find(Item => Item.FilePath == FName);
            if (TextureObj == null)
            {
                TextureObj = new WTexture(FName);
                Textures.Add(TextureObj);
            }
            return TextureObj;
        }
    }
}
