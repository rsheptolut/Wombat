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
	/// A geoset class. Contains vertices and faces which constructs
	/// the geometry of the model (the shapes you see).
	/// </summary>
	public sealed class CGeoset : CObject<CGeoset>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this geoset</param>
		public CGeoset(CModel Model) : base(Model)
		{
			//Empty
		}

		internal override void BuildDetacherList(System.Collections.Generic.ICollection<CDetacher> DetacherList)
		{
			base.BuildDetacherList(DetacherList);
			if(_Vertices != null) _Vertices.BuildDetacherList(DetacherList);
			if(_Faces != null) _Faces.BuildDetacherList(DetacherList);
			if(_Groups != null) _Groups.BuildDetacherList(DetacherList);
			if(_Extents != null) _Extents.BuildDetacherList(DetacherList);
		}

		/// <summary>
		/// Generates a string version of the geoset.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Geoset #" + ObjectId;
		}

		/// <summary>
		/// Checks if the geoset has references pointing to it.
		/// </summary>
		public override bool HasReferences
		{
			get
			{
				if((_Vertices != null) && _Vertices.HasReferences) return true;
				if((_Faces != null) && _Faces.HasReferences) return true;
				if((_Groups != null) && _Groups.HasReferences) return true;
				if((_Extents != null) && _Extents.HasReferences) return true;
				return base.HasReferences;
			}
		}

		/// <summary>
		/// Gets or sets the selection group.
		/// </summary>
		public int SelectionGroup
		{
			get
			{
				return _SelectionGroup;
			}
			set
			{
				AddSetObjectFieldCommand("_SelectionGroup", value);
				_SelectionGroup = value;
			}
		}

		/// <summary>
		/// Gets or sets the unselectable flag.
		/// </summary>
		public bool Unselectable
		{
			get
			{
				return _Unselectable;
			}
			set
			{
				AddSetObjectFieldCommand("_Unselectable", value);
				_Unselectable = value;
			}
		}

		/// <summary>
		/// Gets or sets the extent.
		/// </summary>
		public Primitives.CExtent Extent
		{
			get
			{
				return _Extent;
			}
			set
			{
				AddSetObjectFieldCommand("_Extent", value);
				_Extent = value;
			}
		}

		/// <summary>
		/// Retrieves the material reference.
		/// </summary>
		public CObjectReference<CMaterial> Material
		{
			get
			{
				return _Material ?? (_Material = new CObjectReference<CMaterial>(Model));
			}
		}

		/// <summary>
		/// Checks if there exists som geoset vertices.
		/// </summary>
		public bool HasVertices
		{
			get
			{
				return (_Vertices != null) ? (_Vertices.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some geoset faces.
		/// </summary>
		public bool HasFaces
		{
			get
			{
				return (_Faces != null) ? (_Faces.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some geoset groups.
		/// </summary>
		public bool HasGroups
		{
			get
			{
				return (_Groups != null) ? (_Groups.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some geoset extents.
		/// </summary>
		public bool HasExtents
		{
			get
			{
				return (_Extents != null) ? (_Extents.Count > 0) : false;
			}
		}

		/// <summary>
		/// Retrieves the geoset vertices container.
		/// </summary>
		public CObjectContainer<CGeosetVertex> Vertices
		{
			get
			{
				return _Vertices ?? (_Vertices = new CObjectContainer<CGeosetVertex>(Model));
			}
		}

		/// <summary>
		/// Retrieves the geoset faces container.
		/// </summary>
		public CObjectContainer<CGeosetFace> Faces
		{
			get
			{
				return _Faces ?? (_Faces = new CObjectContainer<CGeosetFace>(Model));
			}
		}

		/// <summary>
		/// Retrieves the geoset groups container.
		/// </summary>
		public CObjectContainer<CGeosetGroup> Groups
		{
			get
			{
				return _Groups ?? (_Groups = new CObjectContainer<CGeosetGroup>(Model));
			}
		}

		/// <summary>
		/// Retrieves the geoset extents container.
		/// </summary>
		public CObjectContainer<CGeosetExtent> Extents
		{
			get
			{
				return _Extents ?? (_Extents = new CObjectContainer<CGeosetExtent>(Model));
			}
		}

		private int _SelectionGroup = 0;
		private bool _Unselectable = false;
		private Primitives.CExtent _Extent = CConstants.DefaultExtent;

		private CObjectReference<CMaterial> _Material = null;

		private CObjectContainer<CGeosetVertex> _Vertices = null;
		private CObjectContainer<CGeosetFace> _Faces = null;
		private CObjectContainer<CGeosetGroup> _Groups = null;
		private CObjectContainer<CGeosetExtent> _Extents = null;
	}
}
