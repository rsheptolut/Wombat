//+-----------------------------------------------------------------------------
//|
//| Copyright (C) 2008, Magnus Ostberg, aka Magos
//| Contact: MagosX@GMail.com, http://www.magosx.com
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
	internal sealed class CVector2 : CAnimatable<Primitives.CVector2>
	{
		public CVector2(Primitives.CVector2 DefaultValue) : base(DefaultValue)
		{
			//Empty
		}

		public override Primitives.CVector2 InterpolateNone(CTime Time, CAnimatorNode<Primitives.CVector2> Node1, CAnimatorNode<Primitives.CVector2> Node2)
		{
			return Node1.Value;
		}

		public override Primitives.CVector2 InterpolateLinear(CTime Time, CAnimatorNode<Primitives.CVector2> Node1, CAnimatorNode<Primitives.CVector2> Node2)
		{
			float Factor = (float)(Time.Time - Node1.Time) / (float)(Node2.Time - Node1.Time);
			float InverseFactor = 1.0f - Factor;

			float X = (Node1.Value.X * InverseFactor) + (Node2.Value.X * Factor);
			float Y = (Node1.Value.Y * InverseFactor) + (Node2.Value.Y * Factor);

			return new Primitives.CVector2(X, Y);
		}

		public override Primitives.CVector2 InterpolateBezier(CTime Time, CAnimatorNode<Primitives.CVector2> Node1, CAnimatorNode<Primitives.CVector2> Node2)
		{
			float Factor = (float)(Time.Time - Node1.Time) / (float)(Node2.Time - Node1.Time);
			float FactorX2 = Factor * Factor;
			float InverseFactor = 1.0f - Factor;
			float InverseFactorX2 = InverseFactor * InverseFactor;

			float Factor1 = InverseFactorX2 * InverseFactor;
			float Factor2 = 3.0f * Factor * InverseFactorX2;
			float Factor3 = 3.0f * FactorX2 * InverseFactor;
			float Factor4 = FactorX2 * Factor;

			float X = (Node1.Value.X * Factor1) + (Node1.OutTangent.X * Factor2) + (Node2.InTangent.X * Factor3) + (Node2.Value.X * Factor4);
			float Y = (Node1.Value.Y * Factor1) + (Node1.OutTangent.Y * Factor2) + (Node2.InTangent.Y * Factor3) + (Node2.Value.Y * Factor4);

			return new Primitives.CVector2(X, Y);
		}

		public override Primitives.CVector2 InterpolateHermite(CTime Time, CAnimatorNode<Primitives.CVector2> Node1, CAnimatorNode<Primitives.CVector2> Node2)
		{
			float Factor = (float)(Time.Time - Node1.Time) / (float)(Node2.Time - Node1.Time);
			float FactorX2 = Factor * Factor;

			float Factor1 = FactorX2 * (2.0f * Factor - 3.0f) + 1;
			float Factor2 = FactorX2 * (Factor - 2.0f) + Factor;
			float Factor3 = FactorX2 * (Factor - 1.0f);
			float Factor4 = FactorX2 * (3.0f - 2.0f * Factor);

			float X = (Node1.Value.X * Factor1) + (Node1.OutTangent.X * Factor2) + (Node2.InTangent.X * Factor3) + (Node2.Value.X * Factor4);
			float Y = (Node1.Value.Y * Factor1) + (Node1.OutTangent.Y * Factor2) + (Node2.InTangent.Y * Factor3) + (Node2.Value.Y * Factor4);

			return new Primitives.CVector2(X, Y);
		}
	}
}
