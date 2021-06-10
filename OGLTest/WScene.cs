using MdxLib.Primitives;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLTest
{
    public class WFrustum
    {
        private Vector4[] Planes = new Vector4[6];
        private Vector3[] BoxCorners = new Vector3[8];
        public Vector3 Position { get; set; }

        internal void FromModelView(ref Matrix4 MVPMatrix, bool DoNormalize = true)
        {
            Planes[0].X = MVPMatrix.M14 + MVPMatrix.M11;
            Planes[0].Y = MVPMatrix.M24 + MVPMatrix.M21;
            Planes[0].Z = MVPMatrix.M34 + MVPMatrix.M31;
            Planes[0].W = MVPMatrix.M44 + MVPMatrix.M41;
            // Right clipping plane
            Planes[1].X = MVPMatrix.M14 - MVPMatrix.M11;
            Planes[1].Y = MVPMatrix.M24 - MVPMatrix.M21;
            Planes[1].Z = MVPMatrix.M34 - MVPMatrix.M31;
            Planes[1].W = MVPMatrix.M44 - MVPMatrix.M41;
            // Top clipping plane
            Planes[2].X = MVPMatrix.M14 - MVPMatrix.M12;
            Planes[2].Y = MVPMatrix.M24 - MVPMatrix.M22;
            Planes[2].Z = MVPMatrix.M34 - MVPMatrix.M32;
            Planes[2].W = MVPMatrix.M44 - MVPMatrix.M42;
            // Bottom clipping plane
            Planes[3].X = MVPMatrix.M14 + MVPMatrix.M12;
            Planes[3].Y = MVPMatrix.M24 + MVPMatrix.M22;
            Planes[3].Z = MVPMatrix.M34 + MVPMatrix.M32;
            Planes[3].W = MVPMatrix.M44 + MVPMatrix.M42;
            // Near clipping plane
            Planes[4].X = MVPMatrix.M13;
            Planes[4].Y = MVPMatrix.M23;
            Planes[4].Z = MVPMatrix.M33;
            Planes[4].W = MVPMatrix.M43;
            // Far clipping plane
            Planes[5].X = MVPMatrix.M14 - MVPMatrix.M13;
            Planes[5].Y = MVPMatrix.M24 - MVPMatrix.M23;
            Planes[5].Z = MVPMatrix.M34 - MVPMatrix.M33;
            Planes[5].W = MVPMatrix.M44 - MVPMatrix.M43;
            // Normalize the plane equations, if requested
            if (DoNormalize == true)
            {
                NormalizePlane(ref Planes[0]);
                NormalizePlane(ref Planes[1]);
                NormalizePlane(ref Planes[2]);
                NormalizePlane(ref Planes[3]);
                NormalizePlane(ref Planes[4]);
                NormalizePlane(ref Planes[5]);
            }
        }

        void NormalizePlane(ref Vector4 Plane)
        {
            float mag;
            mag = (float)Math.Sqrt(Plane.X * Plane.X + Plane.Y * Plane.Y + Plane.Z * Plane.Z);
            Plane.X = Plane.X / mag;
            Plane.Y = Plane.Y / mag;
            Plane.Z = Plane.Z / mag;
            Plane.W = Plane.W / mag;
        }

        private bool ContainsBox(Vector3 Vertex1, Vector3 Vertex2)
        {
            BoxCorners[0] = new Vector3(Vertex1.X, Vertex1.Y, Vertex1.Z);
            BoxCorners[1] = new Vector3(Vertex1.X, Vertex1.Y, Vertex2.Z);
            BoxCorners[2] = new Vector3(Vertex1.X, Vertex2.Y, Vertex1.Z);
            BoxCorners[3] = new Vector3(Vertex1.X, Vertex2.Y, Vertex2.Z);
            BoxCorners[4] = new Vector3(Vertex2.X, Vertex1.Y, Vertex1.Z);
            BoxCorners[5] = new Vector3(Vertex2.X, Vertex1.Y, Vertex2.Z);
            BoxCorners[6] = new Vector3(Vertex2.X, Vertex2.Y, Vertex1.Z);
            BoxCorners[7] = new Vector3(Vertex2.X, Vertex2.Y, Vertex2.Z);

            int iTotalIn = 0;

            foreach (var Plane in Planes)
            {
                int iInCount = 8;
                int iPtIn = 1;

                foreach (var Corner in BoxCorners)
                {
                    if (Plane.X * Corner.X + Plane.Y * Corner.Y + Plane.Z * Corner.Z + Plane.W <= 0)
                    {
                        iPtIn = 0;
                        iInCount--;
                    }
                }

                // were all the points outside of plane p?
                if (iInCount == 0)
                    return false;

                // check if they were all on the right side of the plane
                iTotalIn += iPtIn;
            }

            // so if iTotalIn is 6, then all are inside the view
            if (iTotalIn == 6)
                return true;

            // we must be partly in then otherwise
            return true;
        }

        private bool ContainsSphere(Vector3 Center, float Radius)
        {
            foreach (var Plane in Planes)
                if (Plane.X * Center.X + Plane.Y * Center.Y + Plane.Z * Center.Z + Plane.W <= -Radius)
                    return false;
            return true;
        }
        /*
        internal bool IsCollidingWithFrustum(Vector4[] Planes, Vector3 CamPos)
        {
            Vector3 Pos = Position - CamPos;

            foreach (var Shape in ModelInstance.ModelSource.Model.CollisionShapes)
            {
                bool Result = true;
                if (Shape.Type == MdxLib.Model.ECollisionShapeType.Sphere)
                {
                    Result = IsSphereInFrustum(Planes, Pos + Shape.Vertex1.ToOTK(), Shape.Radius);
                }
                else
                {
                    Result = IsBoxInFrustum(Planes, Shape.Vertex1.ToOTK() + Pos, Shape.Vertex2.ToOTK() + Pos);
                }
                if (Result == true)
                    return true;
            }
            return false;
        }
        */
        internal bool ContainsObject(WObject Obj)
        {
            CExtent Extent = Obj.Extent;
            Vector3 ObjPos = Obj.Position - this.Position;
            return ContainsBox(ObjPos + Extent.Min.ToOTK(), ObjPos + Extent.Max.ToOTK());
        }
    }
    public class WScene : IDisposable
    {
        public WPatternPlane Floor;
        public WInput Input;
        public GameWindow Window;
        public List<WObject> Objects = new List<WObject>();
        public List<WObject> DisplayList = new List<WObject>();
        public Random Rnd = new Random();
        public WShaderInfo Shaders;
        public WResources Resources;
        public Matrix4 ProjectionMatrix;
        public Matrix4 MVPMarix;
        public WFrustum Frustum;
        public static WScene Current { get; private set; }
        public volatile static int TrianglesRendered;
        public double Fps { get; set; }

        public WScene(GameWindow Parent)
        {
            WScene.Current = this;
            this.Window = Parent;
            Input = new WInput();
            Shaders = new WShaderInfo();
            Shaders.Initialize();
            Frustum = new WFrustum();
            Resources = new WResources(@"D:\Projects\Wombat\Gamefiles");
            LoadWorld();

            // Floor = new WPatternPlane(@"TerrainArt\LordaeronSummer\Lords_Grass.png", new Vector3(0, 0, 0)); 
        }

        public void Update(double Time)
        {
            Input.Update(Time);

            Matrix4 ModelViewMatrix = Input.ModelViewMatrix;
            Matrix4.Mult(ref ModelViewMatrix, ref ProjectionMatrix, out MVPMarix);
            Frustum.FromModelView(ref MVPMarix);
            Frustum.Position = Input.CamPos;

            DisplayList.Clear();

            foreach (var Obj in Objects)
                if (Frustum.ContainsObject(Obj))
                    DisplayList.Add(Obj);


            DisplayList.AsParallel().ForAll(Item => Item.Update(Time));
            Fps = 1 / Time;
            // DisplayList.All(Item =>
            // {
            //     Item.Update(Time);
            //     return true;
            //  });
        }

        public void Render()
        {
            TrianglesRendered = 0;
            GL.UseProgram(Shaders.Program1);
            GL.UniformMatrix4(Shaders.Shader1.in_MVP, false, ref MVPMarix);
            foreach (var Unit in DisplayList)
                Unit.Render();
            GL.UseProgram(0);
            Console.WriteLine(TrianglesRendered + "," + (int)Math.Round(Fps, 0));
        }

        public void LoadWorld()
        {
            float Scale = 150;

            //for (int i = 0; i < 64; i++)
            //     for (int k = 0; k < 64; k++)
            //        AddObj("Human", "Arthas", i, k, Scale);

            AddObj("Human", "Arthas", 0, 0, Scale);
            AddObj("Human", "HeroArchMage", 0, 1, Scale);
            AddObj("Orc", "Thrall", 0, 2, Scale);
            AddObj("Human", "HeroPaladin", 0, 3, Scale);
            AddObj("Human", "Jaina", 0, 4, Scale);

            AddObj("Human", "Footman", 1, 0, Scale);
            AddObj("Orc", "Peon", 1, 1, Scale);
            AddObj("Human", "GyroCopter", 1, 2, Scale);
            AddObj("Human", "Militia", 1, 3, Scale);
            AddObj("Human", "MortarTeam", 1, 4, Scale);

            AddObj("Orc", "KotoBeast", 2, 0, Scale);
            AddObj("Undead", "Banshee", 2, 1, Scale);
            AddObj("Human", "Rifleman", 2, 2, Scale);
            AddObj("Human", "Sorceress", 2, 3, Scale);
            AddObj("Undead", "Acolyte", 2, 4, Scale);

            AddObj("Orc", "Tauren", 3, 0, Scale);

            AddObj("Undead", "Acolyte", 3, 1, Scale);
            AddObj("Undead", "HeroLich", 3, 2, Scale);
            AddObj("Undead", "Necromancer", 3, 3, Scale);
            AddObj("Orc", "Wolfrider", 3, 4, Scale);

            AddObj("Human", "Arthas", 4, 0, Scale);
            AddObj("Human", "HeroArchMage", 4, 1, Scale);
            AddObj("Orc", "Thrall", 4, 2, Scale);
            AddObj("Human", "HeroPaladin", 4, 3, Scale);
            AddObj("Human", "Jaina", 4, 4, Scale);

            WModelObject Obj = new WModelObject(WResources.Instance.GetModel("Buildings\\Human\\TownHall\\TownHall.mdx"),
                new Vector3(Scale * -2, Scale * 3, 0));
            Obj.ModelInstance.StartAnimation("Stand Work Upgrade Second", true);
            Objects.Add(Obj);
        }

        private void AddObj(string p1, string p2, int p3, int p4, float Scale)
        {
            WModelObject Obj = new WModelObject(WResources.Instance.GetModel("Units\\" + p1 + "\\" + p2 + "\\" + p2 + ".mdx"),
                new Vector3(Scale * p3, Scale * p4, 0));
            Obj.ModelInstance.StartAnimation("Walk", true);
            Objects.Add(Obj);
        }

        internal void Initialize()
        {
            GL.ClearColor(0.1f, 0.35f, 0.35f, 0.0f);
            GL.Enable(EnableCap.Texture2D);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.EnableClientState(ArrayCap.NormalArray);
        }

        public void Dispose()
        {
            Resources.Dispose();
        }
    }
}


