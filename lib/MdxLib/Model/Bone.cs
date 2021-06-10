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
	/// A bone class. The leaf object in the node skeleton hiearchy
	/// and the object which geosets can attach themselves to.
	/// </summary>
	public sealed class CBone : CNode<CBone>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this bone</param>
		public CBone(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the bone.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Bone #" + ObjectId;
		}

		/// <summary>
		/// Retrieves the node ID (if added to a container).
		/// </summary>
		public override int NodeId
		{
			get
			{
				return Model.GetBoneNodeId(this);
			}
		}

		/// <summary>
		/// Retrieves the geoset reference.
		/// </summary>
		public CObjectReference<CGeoset> Geoset
		{
			get
			{
				return _Geoset ?? (_Geoset = new CObjectReference<CGeoset>(Model));
			}
		}

		/// <summary>
		/// Retrieves the geoset animation reference.
		/// </summary>
		public CObjectReference<CGeosetAnimation> GeosetAnimation
		{
			get
			{
				return _GeosetAnimation ?? (_GeosetAnimation = new CObjectReference<CGeosetAnimation>(Model));
			}
		}

		private CObjectReference<CGeoset> _Geoset = null;
		private CObjectReference<CGeosetAnimation> _GeosetAnimation = null;
	}
}
