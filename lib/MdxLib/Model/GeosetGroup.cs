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
	/// A geoset group class. Defines which matrices (nodes) its transformation
	/// should be constructed from. Each vertex connects to one group and gets
	/// transformed through it.
	/// </summary>
	public sealed class CGeosetGroup : CObject<CGeosetGroup>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this geoset group</param>
		public CGeosetGroup(CModel Model) : base(Model)
		{
			//Empty
		}

		internal override void BuildDetacherList(System.Collections.Generic.ICollection<CDetacher> DetacherList)
		{
			base.BuildDetacherList(DetacherList);
			if(_Nodes != null) _Nodes.BuildDetacherList(DetacherList);
		}

		/// <summary>
		/// Generates a string version of the geoset group.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Geoset Group #" + ObjectId;
		}

		/// <summary>
		/// Checks if the geoset group has references pointing to it.
		/// </summary>
		public override bool HasReferences
		{
			get
			{
				if((_Nodes != null) && _Nodes.HasReferences) return true;
				return base.HasReferences;
			}
		}

		/// <summary>
		/// Checks if there exists some geoset group nodes.
		/// </summary>
		public bool HasNodes
		{
			get
			{
				return (_Nodes != null) ? (_Nodes.Count > 0) : false;
			}
		}

		/// <summary>
		/// Retrieves the geoset group nodes container.
		/// </summary>
		public CObjectContainer<CGeosetGroupNode> Nodes
		{
			get
			{
				return _Nodes ?? (_Nodes = new CObjectContainer<CGeosetGroupNode>(Model));
			}
		}

		private CObjectContainer<CGeosetGroupNode> _Nodes = null;
	}
}
