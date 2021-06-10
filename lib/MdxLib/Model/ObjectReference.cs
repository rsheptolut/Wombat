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
	/// Handles a reference to an object component. References are powerful links to other
	/// parts of the model which will not be invalid even if you add/remove stuff
	/// (like a common ID would).
	/// </summary>
	/// <typeparam name="T">The object type</typeparam>
	public sealed class CObjectReference<T> where T : CObject<T>
	{
		internal CObjectReference(CModel Model)
		{
			_Model = Model;
		}

		/// <summary>
		/// Attaches the reference to an object.
		/// </summary>
		/// <param name="Object">The object to attach to</param>
		public void Attach(T Object)
		{
			Detach();

			if(Object == null) return;
			if(Object.Model != _Model) throw new System.InvalidOperationException("The object belongs to another model!");

			CUnknown Unknown = Object as CUnknown;
			if(Unknown == null) return;

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.CAttachObject<T>(this, Object);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				Unknown.ObjectReferenceSet.Add(this);
				_Object = Object;
			}
		}

		/// <summary>
		/// Detachers the reference from the object (if attached).
		/// </summary>
		public void Detach()
		{
			if(_Object == null) return;

			CUnknown Unknown = _Object as CUnknown;
			if(Unknown == null) return;

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.CDetachObject<T>(this, _Object);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				Unknown.ObjectReferenceSet.Remove(this);
				_Object = null;
			}
		}

		internal void ForceAttach(T Object)
		{
			ForceDetach();

			CUnknown Unknown = Object as CUnknown;
			if(Unknown != null)
			{
				Unknown.ObjectReferenceSet.Add(this);
				_Object = Object;
			}
		}

		internal void ForceDetach()
		{
			CUnknown Unknown = _Object as CUnknown;
			if(Unknown != null)
			{
				Unknown.ObjectReferenceSet.Remove(this);
				_Object = null;
			}
		}

		internal void AddCommand(Command.ICommand Command)
		{
			if(_Model.CommandGroup != null)
			{
				_Model.CommandGroup.Add(Command);
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
		/// Retrieves the attached object, or null if not attached.
		/// </summary>
		public T Object
		{
			get
			{
				return _Object;
			}
		}

		/// <summary>
		/// Retrieves the object ID of the attached object, or InvalidId if not attached.
		/// </summary>
		public int ObjectId
		{
			get
			{
				return (_Object != null) ? _Object.ObjectId : CConstants.InvalidId;
			}
		}

		internal bool CanAddCommand
		{
			get
			{
				return (_Model.CommandGroup != null);
			}
		}

		internal T InternalObject
		{
			get
			{
				return _Object;
			}
			set
			{
				_Object = value;
			}
		}

		private CModel _Model = null;
		private T _Object = null;
	}
}
