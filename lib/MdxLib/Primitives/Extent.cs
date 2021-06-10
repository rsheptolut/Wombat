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
	/// An immutable extent. Defines a shell in which no geoset
	/// (static or animated) should exceed.
	/// </summary>
	public sealed class CExtent : System.ICloneable
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CExtent()
		{
			_Min = new CVector3();
			_Max = new CVector3();
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="Extent">The extent to copy from</param>
		public CExtent(CExtent Extent)
		{
			_Min = Extent._Min;
			_Max = Extent._Max;
			_Radius = Extent._Radius;
		}

		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Min">The minimum point to use</param>
		/// <param name="Max">The maximum point to use</param>
		/// <param name="Radius">The radius to use</param>
		public CExtent(CVector3 Min, CVector3 Max, float Radius)
		{
			_Min = Min;
			_Max = Max;
			_Radius = Radius;
		}

		/// <summary>
		/// Clones the extent.
		/// </summary>
		/// <returns>The cloned extent</returns>
		public object Clone()
		{
			return new CExtent(this);
		}

		/// <summary>
		/// Generates a string version of the extent.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "{ " + _Min + ", " + _Max + ", " + _Radius + " }";
		}

		/// <summary>
		/// Retrieves the minimum point.
		/// </summary>
		public CVector3 Min
		{
			get
			{
				return _Min;
			}
		}

		/// <summary>
		/// Retrieves the maximum point.
		/// </summary>
		public CVector3 Max
		{
			get
			{
				return _Max;
			}
		}

		/// <summary>
		/// Retrieves the radius.
		/// </summary>
		public float Radius
		{
			get
			{
				return _Radius;
			}
		}

		private CVector3 _Min = null;
		private CVector3 _Max = null;
		private float _Radius = 0.0f;
	}
}
