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
	/// An immutable 3-dimensional vector, usually used for coordinates.
	/// If used for colors then X = Red, Y = Green and Z = Blue.
	/// </summary>
	public sealed class CVector3 : System.ICloneable
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CVector3()
		{
			//Empty
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="Vector">The vector to copy from</param>
		public CVector3(CVector3 Vector)
		{
			_X = Vector._X;
			_Y = Vector._Y;
			_Z = Vector._Z;
		}

		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="X">The X-coordinate to use (Red if it's a color)</param>
		/// <param name="Y">The Y-coordinate to use (Green if it's a color)</param>
		/// <param name="Z">The Z-coordinate to use (Blue if it's a color)</param>
		public CVector3(float X, float Y, float Z)
		{
			_X = X;
			_Y = Y;
			_Z = Z;
		}

		/// <summary>
		/// Clones the vector.
		/// </summary>
		/// <returns>The cloned vector</returns>
		public object Clone()
		{
			return new CVector3(this);
		}

		/// <summary>
		/// Generates a string version of the vector.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "{ " + _X + ", " + _Y + ", " + _Z + " }";
		}

		/// <summary>
		/// Retrieves the X-coordinate (Red if it's a color).
		/// </summary>
		public float X
		{
			get
			{
				return _X;
			}
		}

		/// <summary>
		/// Retrieves the Y-coordinate (Green if it's a color).
		/// </summary>
		public float Y
		{
			get
			{
				return _Y;
			}
		}

		/// <summary>
		/// Retrieves the Z-coordinate (Blue if it's a color).
		/// </summary>
		public float Z
		{
			get
			{
				return _Z;
			}
		}

		private float _X = 0.0f;
		private float _Y = 0.0f;
		private float _Z = 0.0f;
	}
}
