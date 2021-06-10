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
namespace MdxLib.Animator
{
	internal abstract class CAnimatable<T> where T : new()
	{
		public CAnimatable(T DefaultValue)
		{
			_DefaultValue = DefaultValue;
		}

		public T Interpolate(EInterpolationType Type, CTime Time, CAnimatorNode<T> Node1, CAnimatorNode<T> Node2)
		{
			if(Node1 == null) return _DefaultValue;
			if(Node2 == null) return Node1.Value;
			if(Node1.Time >= Node2.Time) return Node1.Value;
		    switch(Type)
			{
				case EInterpolationType.None: return InterpolateNone(Time, Node1, Node2);
				case EInterpolationType.Linear: return InterpolateLinear(Time, Node1, Node2);
				case EInterpolationType.Bezier: return InterpolateBezier(Time, Node1, Node2);
				case EInterpolationType.Hermite: return InterpolateHermite(Time, Node1, Node2);
			}

			return _DefaultValue;
		}

		public abstract T InterpolateNone(CTime Time, CAnimatorNode<T> Node1, CAnimatorNode<T> Node2);
		public abstract T InterpolateLinear(CTime Time, CAnimatorNode<T> Node1, CAnimatorNode<T> Node2);
		public abstract T InterpolateBezier(CTime Time, CAnimatorNode<T> Node1, CAnimatorNode<T> Node2);
		public abstract T InterpolateHermite(CTime Time, CAnimatorNode<T> Node1, CAnimatorNode<T> Node2);

		public T DefaultValue
		{
			get
			{
				return _DefaultValue;
			}
		}

		private T _DefaultValue = default(T);
	}
}
