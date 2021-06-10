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
	/// The base interface for all objects.
	/// </summary>
	public interface IUnknown
	{
		/// <summary>
		/// Gets or sets the tag data of the object. Tag data is not saved when the model is.
		/// </summary>
		object Tag { get; set; }
	}

	/// <summary>
	/// The base class for all objects. Exists for internal purposes only.
	/// </summary>
	public abstract class CUnknown : IUnknown
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CUnknown()
		{
			//Empty
		}

		/// <summary>
		/// Gets or sets the tag data of the object. Tag data is not saved when the model is.
		/// </summary>
		public object Tag
		{
			get
			{
				return _Tag;
			}
			set
			{
				_Tag = value;
			}
		}

		internal virtual void BuildDetacherList(System.Collections.Generic.ICollection<CDetacher> DetacherList)
		{
			if(_ObjectReferenceSet != null) BuildObjectDetacherList(DetacherList);
			if(_NodeReferenceSet != null) BuildNodeDetacherList(DetacherList);
		}

		internal virtual void BuildObjectDetacherList(System.Collections.Generic.ICollection<CDetacher> DetacherList)
		{
			//Empty
		}

		internal virtual void BuildNodeDetacherList(System.Collections.Generic.ICollection<CDetacher> DetacherList)
		{
			//Empty
		}

		internal System.Collections.Generic.HashSet<object> ObjectReferenceSet
		{
			get
			{
				return _ObjectReferenceSet ?? (_ObjectReferenceSet = new System.Collections.Generic.HashSet<object>());
			}
		}

		internal System.Collections.Generic.HashSet<object> NodeReferenceSet
		{
			get
			{
				return _NodeReferenceSet ?? (_NodeReferenceSet = new System.Collections.Generic.HashSet<object>());
			}
		}

		private object _Tag = null;

		private System.Collections.Generic.HashSet<object> _ObjectReferenceSet = null;
		private System.Collections.Generic.HashSet<object> _NodeReferenceSet = null;
	}
}
