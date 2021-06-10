//+-----------------------------------------------------------------------------
//|
//| Copyright (C) 2008, Magnus Ostberg, aka Magos
//| Contact:    , http://www.magosx.com
//|
//| This file is part of MdxLib.
//| MdxLib is a library to manipulate (load, modify, save) models for the
//| game WarCraft 3. It can (and is supposed to) be freely used in tools
//| and programs made by other developers.
//|
//| WarCraft is a trademark of Blizzard Entertainment, Inc.
//|
//| MdxLib is free software: you can redistribute it and/or modify
//| it under the terms of the GNU General Public License as published by
//| the Free Software Foundation, either version 3 of the License, or
//| (at your option) any later version.
//|
//| MdxLib is distributed in the hope that it will be useful,
//| but WITHOUT ANY WARRANTY; without even the implied warranty of
//| MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//| GNU General Public License for more details.
//|
//| You should have received a copy of the GNU General Public License
//| along with MdxLib. If not, see <http://www.gnu.org/licenses/>.
//|
//| This header must remain unaltered if changes are made to the file.
//| Additional information may be added as needed.
//|
//+-----------------------------------------------------------------------------
namespace MdxLib.Animator.Animatable
{
	internal sealed class CQuaternion : CAnimatable<Primitives.CVector4>
	{
		public CQuaternion(Primitives.CVector4 DefaultValue) : base(DefaultValue)
		{
			//Empty
		}

		public override Primitives.CVector4 InterpolateNone(CTime Time, CAnimatorNode<Primitives.CVector4> Node1, CAnimatorNode<Primitives.CVector4> Node2)
		{
			return Node1.Value;
		}

        public float QtNorm(Primitives.CVector4 a)
        {
            return a.X * a.X + a.Y * a.Y + a.Z * a.Z + a.W * a.W;
        }

        public Primitives.CVector4 QtLn(Primitives.CVector4 a)
        {
            double r = System.Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);
            float t = r > 0.00001f ? (float)(System.Math.Atan2(r, a.W) / r) : 0.0f;
            return new Primitives.CVector4(t * a.X, t * a.Y, t * a.Z, (float)(0.5 * System.Math.Log(QtNorm(a))));
        }

        public Primitives.CVector4 QtConj(Primitives.CVector4 a)
        {
            return new Primitives.CVector4(-a.X, -a.Y, -a.Z, a.W);
        }

        public Primitives.CVector4 QtInv(Primitives.CVector4 a)
        {
            float norm = 1.0f / QtNorm(a);

            Primitives.CVector4 conj = QtConj(a);
            return new Primitives.CVector4(conj.X * norm, conj.Y * norm, conj.Z * norm, conj.W * norm);
        }

        public Primitives.CVector4 QtExp(Primitives.CVector4 a)
        {
            double r = System.Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);
	        double et = System.Math.Exp(a.W);
	        float t  = r >= 0.00001f ? (float)(et * System.Math.Sin(r) / r) : 0.0f;
            return new Primitives.CVector4(t * a.X, t * a.Y, t * a.Z, (float)(et * System.Math.Cos(r)));	        
        }

		public override Primitives.CVector4 InterpolateLinear(CTime Time, CAnimatorNode<Primitives.CVector4> Node1, CAnimatorNode<Primitives.CVector4> Node2)
		{
            
			float Factor = (float)(Time.Time - Node1.Time) / (float)(Node2.Time - Node1.Time);
            
			return GetSlerp(Node1.Value, Node2.Value, Factor, true);
            
            
		}

		public override Primitives.CVector4 InterpolateBezier(CTime Time, CAnimatorNode<Primitives.CVector4> Node1, CAnimatorNode<Primitives.CVector4> Node2)
		{
			float Factor = (float)(Time.Time - Node1.Time) / (float)(Node2.Time - Node1.Time);

            Primitives.CVector4 Quaternion1 = GetSlerp(Node1.Value, Node1.OutTangent, Factor, true);
            Primitives.CVector4 Quaternion2 = GetSlerp(Node1.OutTangent, Node2.InTangent, Factor, true);
            Primitives.CVector4 Quaternion3 = GetSlerp(Node2.InTangent, Node2.Value, Factor, true);

            Primitives.CVector4 QuaternionSub1 = GetSlerp(Quaternion1, Quaternion2, Factor, true);
            Primitives.CVector4 QuaternionSub2 = GetSlerp(Quaternion2, Quaternion3, Factor, true);

			return GetSlerp(QuaternionSub1, QuaternionSub2, Factor, true);
		}

		public override Primitives.CVector4 InterpolateHermite(CTime Time, CAnimatorNode<Primitives.CVector4> Node1, CAnimatorNode<Primitives.CVector4> Node2)
		{
            float Factor = (float)(Time.Time - Node1.Time) / (float)(Node2.Time - Node1.Time);

            Primitives.CVector4 Quaternion1 = GetSlerp(Node1.Value, Node2.Value, Factor, true);
            Primitives.CVector4 Quaternion2 = GetSlerp(Node1.OutTangent, Node2.InTangent, Factor, true);

            return GetSlerp(Quaternion1, Quaternion2, (2 * Factor * (1.0f - Factor)), true); 
		}

		private float GetDotProduct(Primitives.CVector4 Quaternion1, Primitives.CVector4 Quaternion2)
		{
			return (Quaternion1.X * Quaternion2.X) + (Quaternion1.Y * Quaternion2.Y) + (Quaternion1.Z * Quaternion2.Z) + (Quaternion1.W * Quaternion2.W);
		}

		private Primitives.CVector4 GetSlerp(Primitives.CVector4 Quaternion1, Primitives.CVector4 Quaternion2, float Factor, bool InvertIfNeccessary)
		{
			float InverseFactor = 1.0f - Factor;
			float DotProduct = GetDotProduct(Quaternion1, Quaternion2);

			if(InvertIfNeccessary && (DotProduct < 0.0f))
			{
				DotProduct = -DotProduct;
				Quaternion2 = new MdxLib.Primitives.CVector4(-Quaternion2.X, -Quaternion2.Y, -Quaternion2.Z, -Quaternion2.W);
			}

			if((DotProduct > -Threshold) && (DotProduct < Threshold))
			{
                
				float Angle = (float)System.Math.Acos(DotProduct);
				float Scale = 1.0f / (float)System.Math.Sin(Angle);
				float Scale1 = Scale * (float)System.Math.Sin(Angle * InverseFactor);
				float Scale2 = Scale * (float)System.Math.Sin(Angle * Factor);

				return new Primitives.CVector4((Quaternion1.X * Scale1 + Quaternion2.X * Scale2), (Quaternion1.Y * Scale1 + Quaternion2.Y * Scale2), (Quaternion1.Z * Scale1 + Quaternion2.Z * Scale2), (Quaternion1.W * Scale1 + Quaternion2.W * Scale2));
			}
			else
			{
				float X = (Quaternion1.X * InverseFactor) + (Quaternion2.X * Factor);
				float Y = (Quaternion1.Y * InverseFactor) + (Quaternion2.Y * Factor);
				float Z = (Quaternion1.Z * InverseFactor) + (Quaternion2.Z * Factor);
				float W = (Quaternion1.W * InverseFactor) + (Quaternion2.W * Factor);
				float Length = (float)System.Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
				float Scale = (Length != 0.0f) ? (1.0f / Length) : 0.0f;

				return new Primitives.CVector4((X * Scale), (Y * Length), (Z * Length), (W * Length));
			}
		}

		private const float Threshold = 0.9995f;
	}
}
