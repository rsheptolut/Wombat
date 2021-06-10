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
namespace MdxLib.Model
{
	/// <summary>
	/// A geoset face class. Defines a single face (trinagle).
	/// </summary>
	public sealed class CGeosetFace : CObject<CGeosetFace>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this geoset face</param>
		public CGeosetFace(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the geoset face.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Geoset Face #" + ObjectId;
		}

		/// <summary>
		/// Retrieves the first vertex reference.
		/// </summary>
		public CObjectReference<CGeosetVertex> Vertex1
		{
			get
			{
				return _Vertex1 ?? (_Vertex1 = new CObjectReference<CGeosetVertex>(Model));
			}
		}

		/// <summary>
		/// Retrieves the second vertex reference.
		/// </summary>
		public CObjectReference<CGeosetVertex> Vertex2
		{
			get
			{
				return _Vertex2 ?? (_Vertex2 = new CObjectReference<CGeosetVertex>(Model));
			}
		}

		/// <summary>
		/// Retrieves the third vertex reference.
		/// </summary>
		public CObjectReference<CGeosetVertex> Vertex3
		{
			get
			{
				return _Vertex3 ?? (_Vertex3 = new CObjectReference<CGeosetVertex>(Model));
			}
		}

		private CObjectReference<CGeosetVertex> _Vertex1 = null;
		private CObjectReference<CGeosetVertex> _Vertex2 = null;
		private CObjectReference<CGeosetVertex> _Vertex3 = null;
	}
}
