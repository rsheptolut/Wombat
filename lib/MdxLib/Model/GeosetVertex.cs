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
	/// A geoset vertex class. Defines a single vertex (point).
	/// </summary>
	public sealed class CGeosetVertex : CObject<CGeosetVertex>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this geoset vertex</param>
		public CGeosetVertex(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the geoset vertex.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Geoset Vertex #" + ObjectId;
		}


		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		public Primitives.CVector3 Position
		{
			get
			{
				return _Position;
			}
			set
			{
				AddSetObjectFieldCommand("_Position", value);
				_Position = value;
			}
		}


		/// <summary>
		/// Gets or sets the normal.
		/// </summary>
		public Primitives.CVector3 Normal
		{
			get
			{
				return _Normal;
			}
			set
			{
				AddSetObjectFieldCommand("_Normal", value);
				_Normal = value;
			}
		}


		/// <summary>
		/// Gets or sets the texture position.
		/// </summary>
		public Primitives.CVector2 TexturePosition
		{
			get
			{
				return _TexturePosition;
			}
			set
			{
				AddSetObjectFieldCommand("_TexturePosition", value);
				_TexturePosition = value;
			}
		}

		/// <summary>
		/// Retrieves the geoset group reference.
		/// </summary>
		public CObjectReference<CGeosetGroup> Group
		{
			get
			{
				return _Group ?? (_Group = new CObjectReference<CGeosetGroup>(Model));
			}
		}

		private Primitives.CVector3 _Position = CConstants.DefaultVector3;
		private Primitives.CVector3 _Normal = CConstants.DefaultVector3;
		private Primitives.CVector2 _TexturePosition = CConstants.DefaultVector2;

		private CObjectReference<CGeosetGroup> _Group = null;
	}
}
