using MdxLib.Animator;
using MdxLib.Model;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLTest
{
    public class WNodeInst
    {
        public INode Node;
        public Matrix4 TransformationMatrix;
        public Vector3 PivotPoint;
        public WNodeInst Parent;
        Vector3 TranslationVector;
        Quaternion RotationVector;
        Vector3 ScalingVector;
        bool HasTranslation;
        bool HasRotation;
        bool HasScaling;
        public int TranslationGlobalSeqIndex;
        public int RotationGlobalSeqIndex;
        public int ScalingGlobalSeqIndex;

        public WNodeInst(INode Node, CModel Model)
        {
            TransformationMatrix = Matrix4.Identity;
            this.PivotPoint = Node.PivotPoint.ToOTK();
            this.Node = Node;
            TranslationGlobalSeqIndex = Model.GlobalSequences.IndexOf(Node.Translation.GlobalSequence.Object) + 1;
            RotationGlobalSeqIndex = Model.GlobalSequences.IndexOf(Node.Rotation.GlobalSequence.Object) + 1;
            ScalingGlobalSeqIndex = Model.GlobalSequences.IndexOf(Node.Scaling.GlobalSequence.Object) + 1;
        }

        public void PrepareToTransform(CTime TranslationTime, CTime RotationTime, CTime ScalingTime)
        {
            TranslationVector = Node.Translation.GetValue(TranslationTime).ToOTK();
            RotationVector = Node.Rotation.GetValue(RotationTime).ToOTK();
            ScalingVector = Node.Scaling.GetValue(ScalingTime).ToOTK();

            HasTranslation = TranslationVector.X != 0 || TranslationVector.Y != 0 || TranslationVector.Z != 0;
            HasRotation = RotationVector.X != 0 || RotationVector.Y != 0 || RotationVector.Z != 0 || RotationVector.W != 1;
            HasScaling = ScalingVector.X != 1 || ScalingVector.Y != 1 || ScalingVector.Z != 1;
        }

        public static void FastScale(ref Matrix4 TM, float X, float Y, float Z)
        {
            TM.Row0.X *= X;
            TM.Row0.Y *= Y;
            TM.Row0.Z *= Z;
            TM.Row1.X *= X;
            TM.Row1.Y *= Y;
            TM.Row1.Z *= Z;
            TM.Row2.X *= X;
            TM.Row2.Y *= Y;
            TM.Row2.Z *= Z;
            TM.Row3.X *= X;
            TM.Row3.Y *= Y;
            TM.Row3.Z *= Z;
        }
        private void FastTranslate(ref Matrix4 TM, float X, float Y, float Z)
        {
            TM.Row0.X += TM.Row0.W * X;
            TM.Row0.Y += TM.Row0.W * Y;
            TM.Row0.Z += TM.Row0.W * Z;
            TM.Row1.X += TM.Row1.W * X;
            TM.Row1.Y += TM.Row1.W * Y;
            TM.Row1.Z += TM.Row1.W * Z;
            TM.Row2.X += TM.Row2.W * X;
            TM.Row2.Y += TM.Row2.W * Y;
            TM.Row2.Z += TM.Row2.W * Z;
            TM.Row3.X += TM.Row3.W * X;
            TM.Row3.Y += TM.Row3.W * Y;
            TM.Row3.Z += TM.Row3.W * Z;
        }

        public static void FastRotate(ref Matrix4 TM, Matrix4 RM)
        {
            float X;
            float Y;
            float Z;
            X = TM.Row0.X;
            Y = TM.Row0.Y;
            Z = TM.Row0.Z;
            TM.Row0.X = X * RM.Row0.X + Y * RM.Row1.X + Z * RM.Row2.X;
            TM.Row0.Y = X * RM.Row0.Y + Y * RM.Row1.Y + Z * RM.Row2.Y;
            TM.Row0.Z = X * RM.Row0.Z + Y * RM.Row1.Z + Z * RM.Row2.Z;
            X = TM.Row1.X;
            Y = TM.Row1.Y;
            Z = TM.Row1.Z;
            TM.Row1.X = X * RM.Row0.X + Y * RM.Row1.X + Z * RM.Row2.X;
            TM.Row1.Y = X * RM.Row0.Y + Y * RM.Row1.Y + Z * RM.Row2.Y;
            TM.Row1.Z = X * RM.Row0.Z + Y * RM.Row1.Z + Z * RM.Row2.Z;
            X = TM.Row2.X;
            Y = TM.Row2.Y;
            Z = TM.Row2.Z;
            TM.Row2.X = X * RM.Row0.X + Y * RM.Row1.X + Z * RM.Row2.X;
            TM.Row2.Y = X * RM.Row0.Y + Y * RM.Row1.Y + Z * RM.Row2.Y;
            TM.Row2.Z = X * RM.Row0.Z + Y * RM.Row1.Z + Z * RM.Row2.Z;
            X = TM.Row3.X;
            Y = TM.Row3.Y;
            Z = TM.Row3.Z;
            TM.Row3.X = X * RM.Row0.X + Y * RM.Row1.X + Z * RM.Row2.X;
            TM.Row3.Y = X * RM.Row0.Y + Y * RM.Row1.Y + Z * RM.Row2.Y;
            TM.Row3.Z = X * RM.Row0.Z + Y * RM.Row1.Z + Z * RM.Row2.Z;
        }

        public void BuildTransformationMatrix(CTime CurrentTime)
        {
            TransformationMatrix = Matrix4.Identity;
            if (HasScaling || HasRotation)
            {
                // Смещаем узел к центру
                TransformationMatrix.Row3.X = -PivotPoint.X;
                TransformationMatrix.Row3.Y = -PivotPoint.Y;
                TransformationMatrix.Row3.Z = -PivotPoint.Z;

                // Меняем размер
                if (HasScaling)
                    FastScale(ref TransformationMatrix, ScalingVector.X, ScalingVector.Y, ScalingVector.Z);

                // Вращаем
                if (HasRotation)
                    FastRotate(ref TransformationMatrix, Matrix4.CreateFromQuaternion(RotationVector));

                // Возвращаем узел на место
                FastTranslate(ref TransformationMatrix, PivotPoint.X, PivotPoint.Y, PivotPoint.Z);
            };

            // Перемещаем узел куда следует
            if (HasTranslation)
                FastTranslate(ref TransformationMatrix, TranslationVector.X, TranslationVector.Y, TranslationVector.Z);
        }

        public int IsChildTo(WNodeInst Item2)
        {
            var Item1 = this;
            if (Item1 == Item2)
                return 0;
            if (Item1.Parent == Item2.Parent)
                return 0;
            if (Item1.Parent == null)
                return -1;
            if (Item2.Parent == null)
                return 1;
            if (Item1.Parent == Item2)
                return 1;
            if (Item2.Parent == Item1)
                return -1;
            return Item1.Parent.IsChildTo(Item2.Parent);
        }
    }

    public struct Vector4i
    {
        public int X;
        public int Y;
        public int Z;
        public int W;

        public Vector4i(int X, int Y, int Z, int W)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
        }
    }

    public struct WStream
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TexCoord;
        public Vector4i BoneIndexes1;
        public Vector4i BoneIndexes2;
    }

}
