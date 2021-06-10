using MdxLib.Primitives;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLTest
{
    public static class Extensions
    {
        public static Vector3 ToOTK(this CVector3 Vector)
        {
            return new Vector3(Vector.X, Vector.Y, Vector.Z);
        }

        public static Vector2 ToOTK(this CVector2 Vector)
        {
            return new Vector2(Vector.X, Vector.Y);
        }

        public static Quaternion ToOTK(this CVector4 Vector)
        {
            return new Quaternion(Vector.X, Vector.Y, Vector.Z, Vector.W);
        }
    }
}
