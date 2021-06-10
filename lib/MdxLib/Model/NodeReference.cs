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
	/// Handles a reference to a node component. References are powerful links to other
	/// parts of the model which will not be invalid even if you add/remove stuff
	/// (like a common ID would).
	/// </summary>
	public sealed class CNodeReference
	{
		internal CNodeReference(CModel Model)
		{
			_Model = Model;
		}

		/// <summary>
		/// Attaches the reference to a node.
		/// </summary>
		/// <param name="Node">The node to attach to</param>
		public void Attach(INode Node)
		{
			Detach();

			if(Node == null) return;
			if(Node.Model != _Model) throw new System.InvalidOperationException("The node belongs to another model!");

			CUnknown Unknown = Node as CUnknown;
			if(Unknown == null) return;

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.CAttachNode(this, Node);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				Unknown.NodeReferenceSet.Add(this);
				_Node = Node;
			}
		}

		/// <summary>
		/// Detachers the reference from the node (if attached).
		/// </summary>
		public void Detach()
		{
			if(_Node == null) return;

			CUnknown Unknown = _Node as CUnknown;
			if(Unknown == null) return;

			if(CanAddCommand)
			{
				Command.ICommand Command = new Command.CDetachNode(this, _Node);
				Command.Do();
				AddCommand(Command);
			}
			else
			{
				Unknown.NodeReferenceSet.Remove(this);
				_Node = null;
			}
		}

		internal void ForceAttach(INode Node)
		{
			ForceDetach();

			CUnknown Unknown = Node as CUnknown;
			if(Unknown != null)
			{
				Unknown.NodeReferenceSet.Add(this);
				_Node = Node;
			}
		}

		internal void ForceDetach()
		{
			CUnknown Unknown = _Node as CUnknown;
			if(Unknown != null)
			{
				Unknown.NodeReferenceSet.Remove(this);
				_Node = null;
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
		/// Retrieves the attached node, or null if not attached.
		/// </summary>
		public INode Node
		{
			get
			{
				return _Node;
			}
		}

		/// <summary>
		/// Retrieves the node ID of the attached node, or InvalidId if not attached.
		/// </summary>
		public int NodeId
		{
			get
			{
				return (_Node != null) ? _Node.NodeId : CConstants.InvalidId;
			}
		}

		/// <summary>
		/// Retrieves the object ID of the attached node, or InvalidId if not attached.
		/// </summary>
		public int ObjectId
		{
			get
			{
				return (_Node != null) ? _Node.ObjectId : CConstants.InvalidId;
			}
		}

		internal bool CanAddCommand
		{
			get
			{
				return (_Model.CommandGroup != null);
			}
		}

		internal INode InternalNode
		{
			get
			{
				return _Node;
			}
			set
			{
				_Node = value;
			}
		}

		private CModel _Model = null;
		private INode _Node = null;
	}
}
