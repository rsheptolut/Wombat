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
	/// A material class. Defines how a geoset's surface looks like.
	/// Can consist of multiple layers for more advanced effects,
	/// like teamcolor.
	/// </summary>
	public sealed class CMaterial : CObject<CMaterial>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this material</param>
		public CMaterial(CModel Model) : base(Model)
		{
			//Empty
		}

		internal override void BuildDetacherList(System.Collections.Generic.ICollection<CDetacher> DetacherList)
		{
			base.BuildDetacherList(DetacherList);
			if(_Layers != null) _Layers.BuildDetacherList(DetacherList);
		}

		/// <summary>
		/// Generates a string version of the material.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Material #" + ObjectId;
		}

		/// <summary>
		/// Checks if the material has references pointing to it.
		/// </summary>
		public override bool HasReferences
		{
			get
			{
				if((_Layers != null) && _Layers.HasReferences) return true;
				return base.HasReferences;
			}
		}


		/// <summary>
		/// Gets or sets the priority plane.
		/// </summary>
		public int PriorityPlane
		{
			get
			{
				return _PriorityPlane;
			}
			set
			{
				AddSetObjectFieldCommand("_PriorityPlane", value);
				_PriorityPlane = value;
			}
		}

		/// <summary>
		/// Gets or sets the constant color flag.
		/// </summary>
		public bool ConstantColor
		{
			get
			{
				return _ConstantColor;
			}
			set
			{
				AddSetObjectFieldCommand("_ConstantColor", value);
				_ConstantColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the full resolution flag.
		/// </summary>
		public bool FullResolution
		{
			get
			{
				return _FullResolution;
			}
			set
			{
				AddSetObjectFieldCommand("_FullResolution", value);
				_FullResolution = value;
			}
		}

		/// <summary>
		/// Gets or sets the sort primitives far Z flag.
		/// </summary>
		public bool SortPrimitivesFarZ
		{
			get
			{
				return _SortPrimitivesFarZ;
			}
			set
			{
				AddSetObjectFieldCommand("_SortPrimitivesFarZ", value);
				_SortPrimitivesFarZ = value;
			}
		}

		/// <summary>
		/// Gets or sets the sort primitives near Z flag.
		/// </summary>
		public bool SortPrimitivesNearZ
		{
			get
			{
				return _SortPrimitivesNearZ;
			}
			set
			{
				AddSetObjectFieldCommand("_SortPrimitivesNearZ", value);
				_SortPrimitivesNearZ = value;
			}
		}

		/// <summary>
		/// Checks if there exists some material layers.
		/// </summary>
		public bool HasLayers
		{
			get
			{
				return (_Layers != null) ? (_Layers.Count > 0) : false;
			}
		}

		/// <summary>
		/// Retrieves the material layers container.
		/// </summary>
		public CObjectContainer<CMaterialLayer> Layers
		{
			get
			{
				return _Layers ?? (_Layers = new CObjectContainer<CMaterialLayer>(Model));
			}
		}

		private int _PriorityPlane = 0;
		private bool _ConstantColor = false;
		private bool _FullResolution = false;
		private bool _SortPrimitivesFarZ = false;
		private bool _SortPrimitivesNearZ = false;

		private CObjectContainer<CMaterialLayer> _Layers = null;
	}
}
