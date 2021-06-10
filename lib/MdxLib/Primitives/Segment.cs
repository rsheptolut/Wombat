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
namespace MdxLib.Primitives
{
	/// <summary>
	/// An immutable segment. Used by particle emitters to define how
	/// the particles are animated.
	/// </summary>
	public sealed class CSegment : System.ICloneable
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CSegment()
		{
			_Color = new CVector3(1.0f, 1.0f, 1.0f);
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="Segment">The segment to copy from</param>
		public CSegment(CSegment Segment)
		{
			_Color = Segment._Color;
			_Alpha = Segment._Alpha;
			_Scaling = Segment._Scaling;
		}

		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Color">The color to use</param>
		/// <param name="Alpha">The alpha (solidity) to use</param>
		/// <param name="Scaling">The scaling to use</param>
		public CSegment(CVector3 Color, float Alpha, float Scaling)
		{
			_Color = Color;
			_Alpha = Alpha;
			_Scaling = Scaling;
		}

		/// <summary>
		/// Clones the segment.
		/// </summary>
		/// <returns>The cloned segment</returns>
		public object Clone()
		{
			return new CSegment(this);
		}

		/// <summary>
		/// Generates a string version of the segment.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "";
		}

		/// <summary>
		/// Retrieves the color.
		/// </summary>
		public CVector3 Color
		{
			get
			{
				return _Color;
			}
		}

		/// <summary>
		/// Retrieves the alpha (solidity).
		/// </summary>
		public float Alpha
		{
			get
			{
				return _Alpha;
			}
		}

		/// <summary>
		/// Retrieves the scaling.
		/// </summary>
		public float Scaling
		{
			get
			{
				return _Scaling;
			}
		}

		private CVector3 _Color = null;
		private float _Alpha = 0.0f;
		private float _Scaling = 0.0f;
	}
}
