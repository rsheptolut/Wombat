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
	/// An immutable 4-dimensional vector, usually used for quaternions.
	/// </summary>
	public sealed class CVector4 : System.ICloneable
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CVector4()
		{
			//Empty
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="Vector">The vector to copy from</param>
		public CVector4(CVector4 Vector)
		{
			_X = Vector._X;
			_Y = Vector._Y;
			_Z = Vector._Z;
			_W = Vector._W;
		}

		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="X">The X-coordinate to use</param>
		/// <param name="Y">The Y-coordinate to use</param>
		/// <param name="Z">The Z-coordinate to use</param>
		/// <param name="W">The W-coordinate to use</param>
		public CVector4(float X, float Y, float Z, float W)
		{
			_X = X;
			_Y = Y;
			_Z = Z;
			_W = W;
		}

		/// <summary>
		/// Clones the vector.
		/// </summary>
		/// <returns>The cloned vector</returns>
		public object Clone()
		{
			return new CVector4(this);
		}

		/// <summary>
		/// Generates a string version of the vector.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "{ " + _X + ", " + _Y + ", " + _Z + ", " + _W + " }";
		}

		/// <summary>
		/// Retrieves the X-coordinate.
		/// </summary>
		public float X
		{
			get
			{
				return _X;
			}
		}

		/// <summary>
		/// Retrieves the Y-coordinate.
		/// </summary>
		public float Y
		{
			get
			{
				return _Y;
			}
		}

		/// <summary>
		/// Retrieves the Z-coordinate.
		/// </summary>
		public float Z
		{
			get
			{
				return _Z;
			}
		}

		/// <summary>
		/// Retrieves the W-coordinate.
		/// </summary>
		public float W
		{
			get
			{
				return _W;
			}
		}

        public static CVector4 operator *(CVector4 v1, float scale)
        {
            return new CVector4(v1._X * scale, v1._Y * scale, v1._Z * scale, v1._W * scale);
        }

        public static CVector4 operator *(float scale, CVector4 v1)
        {
            return new CVector4(v1._X * scale, v1._Y * scale, v1._Z * scale, v1._W * scale);
        }

        public static CVector4 operator +(CVector4 v1, CVector4 v2)
        {
            return new CVector4(v1._X + v2._X, v1._Y + v2._Y, v1._Z + v2._Z, v1._W + v2._W);
        }

		private float _X = 0.0f;
		private float _Y = 0.0f;
		private float _Z = 0.0f;
		private float _W = 0.0f;
	}
}
