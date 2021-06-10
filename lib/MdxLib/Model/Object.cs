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
	/// The base interface for all object components.
	/// </summary>
	public interface IObject : IUnknown
	{
		/// <summary>
		/// Retrieves the object ID (if added to a container).
		/// </summary>
		int ObjectId { get; }
		
		/// <summary>
		/// Retrieves the associated model.
		/// </summary>
		CModel Model { get; }

		/// <summary>
		/// Checks if the object has references pointing to it.
		/// </summary>
		bool HasReferences { get; }
	}

	/// <summary>
	/// The base class for all object components. This class is templated so
	/// use IObject if you want non-specified access.
	/// </summary>
	/// <typeparam name="T">The object type (class that inherits this class)</typeparam>
	public abstract class CObject<T> : CUnknown, IObject where T : CObject<T>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this object</param>
		public CObject(CModel Model)
		{
			_Model = Model;
		}

		internal override void BuildObjectDetacherList(System.Collections.Generic.ICollection<CDetacher> DetacherList)
		{
			foreach(object Object in ObjectReferenceSet)
			{
				CObjectReference<T> Reference = Object as CObjectReference<T>;
				if(Reference != null) DetacherList.Add(new CObjectDetacher<T>(Reference));
			}
		}

		internal void AddSetObjectFieldCommand<T2>(string FieldName, T2 Value)
		{
			if(_Model.CommandGroup != null)
			{
				_Model.CommandGroup.Add(new Command.CSetObjectField<T, T2>((T)this, FieldName, Value));
			}
		}

		/// <summary>
		/// Retrieves the object ID (if added to a container).
		/// </summary>
		public int ObjectId
		{
			get
			{
				return (_ObjectContainer != null) ? _ObjectContainer.IndexOf((T)this) : CConstants.InvalidId;
			}
		}

		/// <summary>
		/// Retrieves the associated model.
		/// </summary>
		public CModel Model
		{
			get
			{
				return _Model;
			}
		}

		/// <summary>
		/// Checks if the object has references pointing to it.
		/// </summary>
		public virtual bool HasReferences
		{
			get
			{
				return (ObjectReferenceSet.Count > 0);
			}
		}

		internal CObjectContainer<T> ObjectContainer
		{
			get
			{
				return _ObjectContainer;
			}
			set
			{
				_ObjectContainer = value;
			}
		}

		private CModel _Model = null;

		private CObjectContainer<T> _ObjectContainer = null;
	}
}
