using MdxLib.Animator;
using MdxLib.Model;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OGLTest
{
    public class WGeosetInst
    {
        private WModelInst Parent;
        public CGeoset Geoset;
        public WStream[] Streams;
        public List<WNodeInst> UsedNodes = new List<WNodeInst>();
        public float[] UsedNodeTransformations;
        public uint[] Faces;
        public List<CGeosetAnimation> GeosetAnimations = new List<CGeosetAnimation>();
        public float Alpha;
        public int VBOHandle;
        public int EBOHandle;
        private WShaderInfo Shaders;
        private const int MaxUsedNodes = 100;

        public WGeosetInst(WModelInst Parent, CGeoset Geoset)
        {
            this.Parent = Parent;
            this.Geoset = Geoset;
            this.Shaders = WScene.Current.Shaders;
            List<WNodeInst> GroupNodes;
            INode Node;
            WNodeInst NodeInstance;

            Streams = new WStream[Geoset.Vertices.Count];
            List<int> VertexObjectIDs = new List<int>(Geoset.Vertices.Count);

            Dictionary<int, List<WNodeInst>> GroupDic = new Dictionary<int, List<WNodeInst>>();
            foreach (var Group in Geoset.Groups)
            {
                GroupNodes = new List<WNodeInst>();
                foreach (var NodeRef in Group.Nodes)
                {
                    Node = NodeRef.Node.Node;
                    NodeInstance = Parent.NodeInstances.Find(Item => Item.Node == Node);
                    if (NodeInstance != null)
                    {
                        GroupNodes.Add(NodeInstance);
                        if (!UsedNodes.Contains(NodeInstance) && UsedNodes.Count < MaxUsedNodes)
                            UsedNodes.Add(NodeInstance);
                    }
                }
                GroupDic[Group.ObjectId] = GroupNodes;
            }

            UsedNodeTransformations = new float[4 * 4 * UsedNodes.Count];
            int i = 0;
            int[] indexes = new int[8];
            foreach (var Vertex in Geoset.Vertices)
            {
                VertexObjectIDs.Add(Vertex.ObjectId);

                Streams[i].Position = Vertex.Position.ToOTK();
                Streams[i].Normal = Vertex.Normal.ToOTK();
                Streams[i].TexCoord = Vertex.TexturePosition.ToOTK();

                for (int k = 0; k < 8; k++)
                    indexes[k] = -1;
                int t = 0;
                foreach (var GrNode in GroupDic[Vertex.Group.ObjectId])
                {
                    indexes[t] = UsedNodes.IndexOf(GrNode);
                    t++;
                }

                Streams[i].BoneIndexes1 = new Vector4i(indexes[0], indexes[1], indexes[2], indexes[3]);
                Streams[i].BoneIndexes2 = new Vector4i(indexes[4], indexes[5], indexes[6], indexes[7]);

                i++;
            }

            Faces = new uint[Geoset.Faces.Count * 3];

            i = 0;
            foreach (var Face in Geoset.Faces)
            {
                Faces[i++] = (uint)VertexObjectIDs.IndexOf(Face.Vertex1.ObjectId);
                Faces[i++] = (uint)VertexObjectIDs.IndexOf(Face.Vertex2.ObjectId);
                Faces[i++] = (uint)VertexObjectIDs.IndexOf(Face.Vertex3.ObjectId);
            }

            foreach (var GeosetAnimation in Parent.ModelSource.Model.GeosetAnimations)
            {
                if (GeosetAnimation.Geoset.Object == Geoset)
                    GeosetAnimations.Add(GeosetAnimation);
            }

            GL.GenBuffers(1, out VBOHandle);
            GL.GenBuffers(1, out EBOHandle);

            SetupEbo();
            SetupVbo();
        }

        public void SetupEbo()
        {
            int stride = System.Runtime.InteropServices.Marshal.SizeOf(typeof(int));
            int ebosize = stride * Faces.Length;
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBOHandle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)ebosize, Faces, BufferUsageHint.StaticDraw);
        }

        public void SetupVbo(bool CopyData = true)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOHandle);

            int stride = System.Runtime.InteropServices.Marshal.SizeOf(typeof(WStream));
            int vector4isize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector4i));
            int vector3size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector3));
            int vector2size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector2));
            int vbosize = stride * Streams.Length;

            if (CopyData)
            {
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)vbosize, IntPtr.Zero, BufferUsageHint.StreamDraw);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)vbosize, Streams, BufferUsageHint.StreamDraw);
            }
            
            GL.VertexAttribPointer(Shaders.Shader1.in_Vertex, 3, VertexAttribPointerType.Float, false, stride, (IntPtr)0);
            GL.VertexAttribPointer(Shaders.Shader1.in_Normal, 3, VertexAttribPointerType.Float, false, stride, (IntPtr)(vector3size));
            GL.VertexAttribPointer(Shaders.Shader1.in_TexCoord, 2, VertexAttribPointerType.Float, false, stride, (IntPtr)(vector3size * 2));
            GL.VertexAttribIPointer(Shaders.Shader1.in_BoneIndexes1, 4, VertexAttribIPointerType.Int, stride, (IntPtr)(vector3size * 2 + vector2size));
            GL.VertexAttribIPointer(Shaders.Shader1.in_BoneIndexes2, 4, VertexAttribIPointerType.Int, stride, (IntPtr)(vector3size * 2 + vector2size + vector4isize));
        }

        private void SetLayerProperties(CMaterialLayer Layer)
        {
            GL.Disable(EnableCap.AlphaTest);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GL.Enable(EnableCap.Blend);

            EMaterialLayerFilterMode FilterMode;
            if (Layer != null)
                FilterMode = Layer.FilterMode;
            else
                FilterMode = EMaterialLayerFilterMode.None;

            switch (FilterMode)
            {
                case EMaterialLayerFilterMode.None:
                    GL.Disable(EnableCap.Blend);
                    break;
                case EMaterialLayerFilterMode.Transparent:
                    GL.Enable(EnableCap.AlphaTest);
                    GL.AlphaFunc(AlphaFunction.Greater, 0.7421875f);
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                    break;
                case EMaterialLayerFilterMode.Blend:
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                   // GL.DepthMask(false);
                    break;
                case EMaterialLayerFilterMode.Additive:
                    GL.DepthFunc(DepthFunction.Always);
                    GL.DepthMask(false);
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.One);
                    break;
                case EMaterialLayerFilterMode.AdditiveAlpha:
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.One);
                    GL.DepthMask(false);
                    break;
                case EMaterialLayerFilterMode.Modulate:
                    GL.BlendFunc(BlendingFactor.Zero, BlendingFactor.SrcColor);
                    GL.DepthMask(false);
                    break;
                default:
                    throw new Exception("Unexpected Filter type");
            }

            if (Layer == null)
                return;

            if (Layer.NoDepthTest)
                GL.Disable(EnableCap.DepthTest);

            if (Layer.NoDepthSet)
                GL.DepthMask(false);

            if (Layer.TwoSided)
                GL.Disable(EnableCap.CullFace);
            else
                GL.Enable(EnableCap.CullFace);
        }

        public void PrepareNodeMatrices()
        {
            int i = 0;
            foreach (var Node in UsedNodes)
            {
                UsedNodeTransformations[i + 0] = Node.TransformationMatrix.Row0.X;
                UsedNodeTransformations[i + 1] = Node.TransformationMatrix.Row0.Y;
                UsedNodeTransformations[i + 2] = Node.TransformationMatrix.Row0.Z;
                UsedNodeTransformations[i + 3] = Node.TransformationMatrix.Row0.W;
                UsedNodeTransformations[i + 4] = Node.TransformationMatrix.Row1.X;
                UsedNodeTransformations[i + 5] = Node.TransformationMatrix.Row1.Y;
                UsedNodeTransformations[i + 6] = Node.TransformationMatrix.Row1.Z;
                UsedNodeTransformations[i + 7] = Node.TransformationMatrix.Row1.W;
                UsedNodeTransformations[i + 8] = Node.TransformationMatrix.Row2.X;
                UsedNodeTransformations[i + 9] = Node.TransformationMatrix.Row2.Y;
                UsedNodeTransformations[i + 10] = Node.TransformationMatrix.Row2.Z;
                UsedNodeTransformations[i + 11] = Node.TransformationMatrix.Row2.W;
                UsedNodeTransformations[i + 12] = Node.TransformationMatrix.Row3.X;
                UsedNodeTransformations[i + 13] = Node.TransformationMatrix.Row3.Y;
                UsedNodeTransformations[i + 14] = Node.TransformationMatrix.Row3.Z;
                UsedNodeTransformations[i + 15] = Node.TransformationMatrix.Row3.W;
                i += 16;
            }
        }

        public void Update(CTime CurrentTime)
        {
            Alpha = 1;

            CAnimatorNode<float> AlphaNode;

            foreach (var Animation in GeosetAnimations)
            {
                if (CurrentTime != null)
                {
                    AlphaNode = Animation.Alpha.GetLowerNodeAtTime(CurrentTime);
                    if (AlphaNode != null)
                        Alpha *= AlphaNode.Value;
                }
                else
                    Alpha *= Animation.Alpha.GetValue();
            }

            foreach (var Layer in Geoset.Material.Object.Layers)
            {
                if (CurrentTime != null)
                    if (Parent.IsAnimated)
                        Layer.Tag = Layer.Alpha.GetValue(CurrentTime);
                    else
                        Layer.Tag = Layer.Alpha.GetValue();
                else
                    Layer.Tag = (float)1;
            }

            if (CurrentTime != null)
            {
                PrepareNodeMatrices();
            }
        }

        public void Render()
        {
            if (this.Alpha < 0.001f)
                return;

            WTexture Texture;

            SetupVbo(false);

            if (Parent.IsAnimated)
            {
                PrepareNodeMatrices();
                GL.UniformMatrix4(Shaders.Shader1.in_Bones, UsedNodes.Count, false, UsedNodeTransformations);
            }
            GL.Uniform1(Shaders.Shader1.in_IsAnimated, Convert.ToInt32(Parent.IsAnimated));
            
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBOHandle);

            EnableStreams(true);

            var Alpha = this.Alpha;
            foreach (var Layer in Geoset.Material.Object.Layers)
            {
                Alpha *= (float)Layer.Tag;
                GL.Color4(1, 1, 1, Alpha);

                Texture = Layer.Texture.Object.Tag as WTexture;

                if (Texture == null)
                    continue;

                GL.BindTexture(TextureTarget.Texture2D, Texture.TextureId);

                SetLayerProperties(Layer);

                if (Geoset.Material.Object.Layers.Count > 1)
                    GL.DepthFunc(DepthFunction.Lequal);
                else
                    GL.DepthFunc(DepthFunction.Less);


                GL.DrawElements(BeginMode.Triangles, Faces.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                Interlocked.Add(ref WScene.TrianglesRendered, Faces.Length);

                SetLayerProperties(null);
            }

            EnableStreams(false);
        }

        private void EnableStreams(bool Enable)
        {
            if (Enable)
            {
                GL.EnableVertexAttribArray(Shaders.Shader1.in_Vertex);
                GL.EnableVertexAttribArray(Shaders.Shader1.in_Normal);
                GL.EnableVertexAttribArray(Shaders.Shader1.in_TexCoord);
                GL.EnableVertexAttribArray(Shaders.Shader1.in_BoneIndexes1);
                GL.EnableVertexAttribArray(Shaders.Shader1.in_BoneIndexes2);
            }
            else
            {
                GL.DisableVertexAttribArray(Shaders.Shader1.in_Vertex);
                GL.DisableVertexAttribArray(Shaders.Shader1.in_Normal);
                GL.DisableVertexAttribArray(Shaders.Shader1.in_TexCoord);
                GL.DisableVertexAttribArray(Shaders.Shader1.in_BoneIndexes1);
                GL.DisableVertexAttribArray(Shaders.Shader1.in_BoneIndexes2);
            }
        }

    }
}
