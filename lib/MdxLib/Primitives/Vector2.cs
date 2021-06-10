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
	/// An immutable 2-dimensional vector, usually used for coordinates.
	/// </summary>
	public sealed class CVector2 : System.ICloneable
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CVector2()
		{
			//Empty
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="Vector">The vector to copy from</param>
		public CVector2(CVector2 Vector)
		{
			_X = Vector._X;
			_Y = Vector._Y;
		}

		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="X">The X-coordinate to use</param>
		/// <param name="Y">The Y-coordinate to use</param>
		public CVector2(float X, float Y)
		{
			_X = X;
			_Y = Y;
		}

		/// <summary>
		/// Clones the vector.
		/// </summary>
		/// <returns>The cloned vector</returns>
		public object Clone()
		{
			return new CVector2(this);
		}

		/// <summary>
		/// Generates a string version of the vector.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "{ " + _X + ", " + _Y + " }";
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

		private float _X = 0.0f;
		private float _Y = 0.0f;
	}
}
