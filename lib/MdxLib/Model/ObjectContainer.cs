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
	/// Stores objects which can be added, removed and enumerated.
	/// </summary>
	/// <typeparam name="T">The object type</typeparam>
	public sealed class CObjectContainer<T> : System.Collections.Generic.IList<T> where T : CObject<T>
	{
		internal CObjectContainer(CModel Model)
		{
			_Model = Model;
			ObjectList = new System.Collections.Generic.List<T>();
		}

		/// <summary>
		/// Clears all objects.
		/// </summary>
		public void Clear()
		{
			if(ObjectList.Count <= 0) return;

			System.Collections.Generic.LinkedList<CDetacher> DetacherList = new System.Collections.Generic.LinkedList<CDetacher>();
			BuildDetacherList(DetacherList);

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.ObjectContainer.CClear<T>(this, DetacherList);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				CDetacher.DetachAllDetachers(DetacherList);

				foreach(T Object in ObjectList)
				{
					Object.ObjectContainer = null;
				}

				ObjectList.Clear();
			}
		}

		/// <summary>
		/// Adds a new object.
		/// </summary>
		/// <param name="Object">The object to add</param>
		public void Add(T Object)
		{
			if(Object == null) return;
			if(Object.Model != _Model) throw new System.InvalidOperationException("The object belongs to another model!");
			if(Object.ObjectContainer != null) throw new System.InvalidOperationException("The object is already in a container!");

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.ObjectContainer.CAdd<T>(this, Object);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				ObjectList.Add(Object);
				Object.ObjectContainer = this;
			}
		}

		/// <summary>
		/// Inserts a new object at a specific index.
		/// </summary>
		/// <param name="Index">The index to insert at</param>
		/// <param name="Object">The object to insert</param>
		public void Insert(int Index, T Object)
		{
			if(Object == null) return;
			if(Object.Model != _Model) throw new System.InvalidOperationException("The object belongs to another model!");
			if(Object.ObjectContainer != null) throw new System.InvalidOperationException("The object is already in a container!");
			if(Index < 0) return;
			if(Index > ObjectList.Count) return;

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.ObjectContainer.CInsert<T>(this, Index, Object);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				ObjectList.Insert(Index, Object);
				Object.ObjectContainer = this;
			}
		}

		/// <summary>
		/// Sets a new object at a specific index (removing whatever is there).
		/// </summary>
		/// <param name="Index">The index to set at</param>
		/// <param name="Object">The object to set</param>
		public void Set(int Index, T Object)
		{
			if(Object == null) return;
			if(Object.Model != _Model) throw new System.InvalidOperationException("The object belongs to another model!");
			if(Object.ObjectContainer != null) throw new System.InvalidOperationException("The object is already in a container!");
			if(!ContainsIndex(Index)) return;

			T OldObject = ObjectList[Index];

			System.Collections.Generic.LinkedList<CDetacher> DetacherList = new System.Collections.Generic.LinkedList<CDetacher>();
			OldObject.BuildDetacherList(DetacherList);

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.ObjectContainer.CSet<T>(this, Index, Object, DetacherList);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				CDetacher.DetachAllDetachers(DetacherList);

				OldObject.ObjectContainer = null;
				ObjectList[Index] = Object;
				Object.ObjectContainer = this;
			}
		}

		/// <summary>
		/// Removes an existing object.
		/// </summary>
		/// <param name="Object">The object to remove</param>
		/// <returns>True on success, False on failure</returns>
		public bool Remove(T Object)
		{
			if(Object == null) return false;

			int Index = IndexOf(Object);
			if(Index == CConstants.InvalidIndex) return false;

			System.Collections.Generic.LinkedList<CDetacher> DetacherList = new System.Collections.Generic.LinkedList<CDetacher>();
			Object.BuildDetacherList(DetacherList);

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.ObjectContainer.CRemoveAt<T>(this, Index, DetacherList);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				CDetacher.DetachAllDetachers(DetacherList);

				Object.ObjectContainer = null;
				ObjectList.RemoveAt(Index);
			}

			return true;
		}

		/// <summary>
		/// Removes an existing object at a specific index.
		/// </summary>
		/// <param name="Index">The index to remove at</param>
		public void RemoveAt(int Index)
		{
			T Object = Get(Index);
			if(Object == null) return;

			System.Collections.Generic.LinkedList<CDetacher> DetacherList = new System.Collections.Generic.LinkedList<CDetacher>();
			Object.BuildDetacherList(DetacherList);

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.ObjectContainer.CRemoveAt<T>(this, Index, DetacherList);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				CDetacher.DetachAllDetachers(DetacherList);

				Object.ObjectContainer = null;
				ObjectList.RemoveAt(Index);
			}
		}

		/// <summary>
		/// Retrieves the object at a specific index.
		/// </summary>
		/// <param name="Index">The index to retrieve at</param>
		/// <returns>The retrieved object, null on failure</returns>
		public T Get(int Index)
		{
			return ContainsIndex(Index) ? ObjectList[Index] : null;
		}

		/// <summary>
		/// Retrieves the index of an existing object.
		/// </summary>
		/// <param name="Object">The object whose index to retrieve</param>
		/// <returns>The index of the object, InvalidIndex on failure</returns>
		public int IndexOf(T Object)
		{
			return ObjectList.IndexOf(Object);
		}

		/// <summary>
		/// Checks if an object exists in the container.
		/// </summary>
		/// <param name="Object">The object to check for</param>
		/// <returns>True if it exists, False otherwise</returns>
		public bool Contains(T Object)
		{
			return (Object != null) ? ObjectList.Contains(Object) : false;
		}

		/// <summary>
		/// Checks if an index exists in the container.
		/// </summary>
		/// <param name="Index">The index to check for</param>
		/// <returns>True if it exists, False otherwise</returns>
		public bool ContainsIndex(int Index)
		{
			return (Index >= 0) && (Index < ObjectList.Count);
		}

		/// <summary>
		/// Copies the contents of the container to an array.
		/// </summary>
		/// <param name="Array">The array to copy to</param>
		/// <param name="Index">The index in the array to start copying to</param>
		public void CopyTo(T[] Array, int Index)
		{
			ObjectList.CopyTo(Array, Index);
		}

		/// <summary>
		/// Retrieves an enumerator for the objects in the container.
		/// </summary>
		/// <returns>The retrieved enumerator</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			return ObjectList.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return ObjectList.GetEnumerator();
		}

		internal void BuildDetacherList(System.Collections.Generic.ICollection<CDetacher> DetacherList)
		{
			foreach(T Object in ObjectList)
			{
				Object.BuildDetacherList(DetacherList);
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
		/// Checks if the container has any objects with references pointing to them.
		/// </summary>
		public bool HasReferences
		{
			get
			{
				foreach(T Object in ObjectList)
				{
					if(Object.HasReferences) return true;
				}

				return false;
			}
		}

		/// <summary>
		/// Retrieves the number of objects in the container.
		/// </summary>
		public int Count
		{
			get
			{
				return ObjectList.Count;
			}
		}

		/// <summary>
		/// Checks if the container is read-only (which it isn't).
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Gets or set an object in the container.
		/// </summary>
		/// <param name="Index">The index to get or set at</param>
		/// <returns>The accessed object</returns>
		public T this[int Index]
		{
			get
			{
				return Get(Index);
			}
			set
			{
				if(value != null)
				{
					Set(Index, value);
				}
				else
				{
					RemoveAt(Index);
				}
			}
		}

		internal bool CanAddCommand
		{
			get
			{
				return (_Model.CommandGroup != null);
			}
		}

		internal System.Collections.Generic.List<T> InternalObjectList
		{
			get
			{
				return ObjectList;
			}
			set
			{
				ObjectList = value;
			}
		}

		private CModel _Model = null;
		private System.Collections.Generic.List<T> ObjectList = null;
	}
}
