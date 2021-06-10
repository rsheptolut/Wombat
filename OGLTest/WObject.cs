using MdxLib.Primitives;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLTest
{
    public class WObject
    {
        public Vector3 Position;

        public WObject(Vector3 Position)
        {
            this.Position = Position;
        }

        public virtual void Update(double Time)
        {

        }

        public virtual void Render()
        {

        }

        public virtual CExtent Extent { get; set; }
    }

    public class WModelObject: WObject
    {
        public WModelInst ModelInstance;

        public WModelObject(WModelSource Model, Vector3 Position): base(Position)
        {
            ModelInstance = new WModelInst(Model);
        }

        public override void Update(double Time)
        {
            ModelInstance.Update(Time);
        }

        public override void Render()
        {
            GL.Uniform3(WScene.Current.Shaders.Shader1.in_ObjectPos, Position);
            ModelInstance.Render();
        }

        public override CExtent Extent
        {
            get
            {
                return ModelInstance.ModelSource.Model.Extent;
            }
        }
    }

    public class WGroundObject : WObject
    {
        public WTexture Texture;

        public WGroundObject(WTexture Texture, Vector3 Position)
            : base(Position)
        {
            this.Texture = Texture;
        }

        public override void Render()
        {
            GL.Uniform3(WScene.Current.Shaders.Shader1.in_ObjectPos, Position);
            //Texture.Render();
        }

        public override CExtent Extent
        {
            get
            {
                return new CExtent();
            }
        }
    }
}
