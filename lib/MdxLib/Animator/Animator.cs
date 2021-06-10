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
using System.Collections.Generic;

namespace MdxLib.Animator
{
	/// <summary>
	/// The main class for animated values.
	/// </summary>
	/// <typeparam name="T">The value type</typeparam>
	public sealed class CAnimator<T> : System.Collections.Generic.IList<CAnimatorNode<T>> where T : new()
	{
        internal class CTimeComparer: IComparer<CAnimatorNode<T>>
        {
            public int Compare(CAnimatorNode<T> x, CAnimatorNode<T> y)
            {
                return x.Time.CompareTo(y.Time);
            }
        }

		internal CAnimator(Model.CModel Model, CAnimatable<T> Animatable)
		{
			_Model = Model;
			_Animatable = Animatable;
			_StaticValue = Animatable.DefaultValue;

			NodeList = new System.Collections.Generic.List<CAnimatorNode<T>>();
		}

        

		/// <summary>
		/// Retrieves the static value.
		/// </summary>
		/// <returns>The value</returns>
		public T GetValue()
		{
			return _StaticValue;
		}

		/// <summary>
		/// Retrieves the value of the animator. If it's animated it will be interpolated,
		/// otherwise it will be the static value.
		/// </summary>
		/// <param name="Time">The time to interpolate at</param>
		/// <returns>The value</returns>
		public T GetValue(CTime Time)
		{
			return _Animated ? _Animatable.Interpolate(_Type, Time, GetLowerNodeAtTime(Time), GetUpperNodeAtTime(Time)) : _StaticValue;
		}

		/// <summary>
		/// Retrieves the node to the left of a specific point in time.
		/// </summary>
		/// <param name="Time">The time</param>
		/// <returns>The left node, or null if none exists</returns>
		public CAnimatorNode<T> GetLowerNodeAtTime(CTime Time)
		{
            CAnimatorNode<T> LikeThis = new CAnimatorNode<T>(Time.Time, new T());

            int i = NodeList.BinarySearch(LikeThis, new CTimeComparer());
            if (i >= 0)
                return NodeList[i];
            else
            {
                i = ~i;
                if (i > 0)
                    i--;
                else
                    return null;

                if (NodeList[i].Time >= Time.IntervalStart)
                    return NodeList[i];
                else
                    return null;
            }
		}

		/// <summary>
		/// Retrieves the node to the right of a specific point in time.
		/// </summary>
		/// <param name="Time">The time</param>
		/// <returns>The right node, or null if none exists</returns>
		public CAnimatorNode<T> GetUpperNodeAtTime(CTime Time)
		{
            CAnimatorNode<T> LikeThis = new CAnimatorNode<T>(Time.Time, new T());

            int i = NodeList.BinarySearch(LikeThis, new CTimeComparer());
            if (i >= 0)
                return NodeList[i];
            else
            {
                i = ~i;
                if (i >= NodeList.Count)
                    return null;

                if (NodeList[i].Time <= Time.IntervalEnd)
                    return NodeList[i];
                else
                    return null;
            }
		}

		/// <summary>
		/// Makes the animator static.
		/// </summary>
		/// <param name="StaticValue">The new static value to use</param>
		public void MakeStatic(T StaticValue)
		{
			AddSetAnimatorFieldCommand("_StaticValue", StaticValue);
			_StaticValue = StaticValue;
			AddSetAnimatorFieldCommand("_Animated", false);
			_Animated = false;
		}

		/// <summary>
		/// Makes the animator animated.
		/// </summary>
		public void MakeAnimated()
		{
			AddSetAnimatorFieldCommand("_Animated", true);
			_Animated = true;
		}

		/// <summary>
		/// Clears all nodes.
		/// </summary>
		public void Clear()
		{
			if(NodeList.Count <= 0) return;

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.Animator.CClear<T>(this);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				NodeList.Clear();
			}
		}

		/// <summary>
		/// Adds a new node.
		/// </summary>
		/// <param name="Node">The node to add</param>
		public void Add(CAnimatorNode<T> Node)
		{
			int InsertIndex = GetInsertIndex(Node);

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.Animator.CInsert<T>(this, InsertIndex, Node);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				NodeList.Insert(InsertIndex, Node);
			}
		}

		/// <summary>
		/// Inserts a new node at a specific index. Since the nodes are sorted
		/// it doesn't actually insert at the specified index.
		/// </summary>
		/// <param name="Index">The index to insert at</param>
		/// <param name="Node">The node to insert</param>
		public void Insert(int Index, CAnimatorNode<T> Node)
		{
			int InsertIndex = GetInsertIndex(Node);

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.Animator.CInsert<T>(this, InsertIndex, Node);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				NodeList.Insert(InsertIndex, Node);
			}
		}

		/// <summary>
		/// Sets a new node at a specific index (removing whatever is there).
		/// </summary>
		/// <param name="Index">The index to set at</param>
		/// <param name="Node">The node to set</param>
		public void Set(int Index, CAnimatorNode<T> Node)
		{
			if(!ContainsIndex(Index)) return;

			int Time = NodeList[Index].Time;
			CAnimatorNode<T> NewNode = new CAnimatorNode<T>(Time, Node.Value, Node.InTangent, Node.OutTangent);

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.Animator.CSet<T>(this, Index, NewNode);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				NodeList[Index] = NewNode;
			}
		}

		/// <summary>
		/// Removes an existing node.
		/// </summary>
		/// <param name="Node">The node to remove</param>
		/// <returns>True on success, False on failure</returns>
		public bool Remove(CAnimatorNode<T> Node)
		{
			int Index = NodeList.IndexOf(Node);
			if(!ContainsIndex(Index)) return false;

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.Animator.CRemoveAt<T>(this, Index);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				NodeList.RemoveAt(Index);
			}

			return true;
		}

		/// <summary>
		/// Removes an existing node at a specific index.
		/// </summary>
		/// <param name="Index">The index to remove at</param>
		public void RemoveAt(int Index)
		{
			if(!ContainsIndex(Index)) return;

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.Animator.CRemoveAt<T>(this, Index);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				NodeList.RemoveAt(Index);
			}
		}

		/// <summary>
		/// Retrieves the node at a specific index.
		/// </summary>
		/// <param name="Index">The index to retrieve at</param>
		/// <returns>The retrieved node, null on failure</returns>
		public CAnimatorNode<T> Get(int Index)
		{
			return ContainsIndex(Index) ? NodeList[Index] : null;
		}

		/// <summary>
		/// Retrieves the index of an existing node.
		/// </summary>
		/// <param name="Node">The node whose index to retrieve</param>
		/// <returns>The index of the node, InvalidIndex on failure</returns>
		public int IndexOf(CAnimatorNode<T> Node)
		{
			return NodeList.IndexOf(Node);
		}

		/// <summary>
		/// Checks if a node exists in the animator.
		/// </summary>
		/// <param name="Node">The node to check for</param>
		/// <returns>True if it exists, False otherwise</returns>
		public bool Contains(CAnimatorNode<T> Node)
		{
			return NodeList.Contains(Node);
		}

		/// <summary>
		/// Checks if an index exists in the animator.
		/// </summary>
		/// <param name="Index">The index to check for</param>
		/// <returns>True if it exists, False otherwise</returns>
		public bool ContainsIndex(int Index)
		{
			return (Index >= 0) && (Index < NodeList.Count);
		}

		/// <summary>
		/// Copies the contents of the animator to an array.
		/// </summary>
		/// <param name="Array">The array to copy to</param>
		/// <param name="Index">The index in the array to start copying to</param>
		public void CopyTo(CAnimatorNode<T>[] Array, int Index)
		{
			NodeList.CopyTo(Array, Index);
		}

		/// <summary>
		/// Retrieves an enumerator for the nodes in the animator.
		/// </summary>
		/// <returns>The retrieved enumerator</returns>
		public System.Collections.Generic.IEnumerator<CAnimatorNode<T>> GetEnumerator()
		{
			return NodeList.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return NodeList.GetEnumerator();
		}

		private int GetInsertIndex(CAnimatorNode<T> Node)
		{
			if(NodeList.Count > 0)
			{
				if(Node.Time > NodeList[NodeList.Count - 1].Time) return NodeList.Count;
			}

			for(int Index = 0; Index < NodeList.Count; Index++)
			{
				if(Node.Time < NodeList[Index].Time) return Index;
			}

			return NodeList.Count;
		}

		internal void AddCommand(Command.ICommand Command)
		{
			if(_Model.CommandGroup != null)
			{
				_Model.CommandGroup.Add(Command);
			}
		}

		internal void AddSetAnimatorFieldCommand<T2>(string FieldName, T2 Value)
		{
			if(_Model.CommandGroup != null)
			{
				_Model.CommandGroup.Add(new Command.CSetAnimatorField<T, T2>(this, FieldName, Value));
			}
		}

		/// <summary>
		/// Checks if the animator is static (the opposite of animated).
		/// </summary>
		public bool Static
		{
			get
			{
				return !_Animated;
			}
		}

		/// <summary>
		/// Checks if the animator is animated (the opposite of static).
		/// </summary>
		public bool Animated
		{
			get
			{
				return _Animated;
			}
		}

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		public EInterpolationType Type
		{
			get
			{
				return _Type;
			}
			set
			{
				AddSetAnimatorFieldCommand("_Type", value);
				_Type = value;
			}
		}

		/// <summary>
		/// Retrieves the number of nodes in the animator.
		/// </summary>
		public int Count
		{
			get
			{
				return NodeList.Count;
			}
		}

		/// <summary>
		/// Checks if the animator is read-only (which it isn't).
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Gets or sets a node in the animator.
		/// </summary>
		/// <param name="Index">The index to get or set at</param>
		/// <returns>The accessed node</returns>
		public CAnimatorNode<T> this[int Index]
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

		/// <summary>
		/// Retrieves the associated model.
		/// </summary>
		public Model.CModel Model
		{
			get
			{
				return _Model;
			}
		}

		/// <summary>
		/// Retrieves the global sequence reference.
		/// </summary>
		public Model.CObjectReference<Model.CGlobalSequence> GlobalSequence
		{
			get
			{
				return _GlobalSequence ?? (_GlobalSequence = new Model.CObjectReference<Model.CGlobalSequence>(Model));
			}
		}

		internal bool CanAddCommand
		{
			get
			{
				return (_Model.CommandGroup != null);
			}
		}
		
		internal System.Collections.Generic.List<CAnimatorNode<T>> InternalNodeList
		{
			get
			{
				return NodeList;
			}
			set
			{
				NodeList = value;
			}
		}

		private Model.CModel _Model = null;
		private CAnimatable<T> _Animatable = null;
		private Model.CObjectReference<Model.CGlobalSequence> _GlobalSequence = null;

		private bool _Animated = false;
		private EInterpolationType _Type = EInterpolationType.None;
		private T _StaticValue = default(T);		

		private System.Collections.Generic.List<CAnimatorNode<T>> NodeList = null;
	}
}
