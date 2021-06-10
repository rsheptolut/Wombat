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
	/// Stores nodes which can be added, removed and enumerated. This is a wrapper
	/// around multiple object containers (for the node types).
	/// </summary>
	public sealed class CNodeContainer : System.Collections.Generic.IList<INode>
	{
		internal CNodeContainer(CModel Model)
		{
			_Model = Model;
		}

		/// <summary>
		/// Clears all nodes.
		/// </summary>
		public void Clear()
		{
			if(_Model.HasBones) _Model.Bones.Clear();
			if(_Model.HasLights) _Model.Lights.Clear();
			if(_Model.HasHelpers) _Model.Helpers.Clear();
			if(_Model.HasAttachments) _Model.Attachments.Clear();
			if(_Model.HasParticleEmitters) _Model.ParticleEmitters.Clear();
			if(_Model.HasParticleEmitters2) _Model.ParticleEmitters2.Clear();
			if(_Model.HasRibbonEmitters) _Model.RibbonEmitters.Clear();
			if(_Model.HasEvents) _Model.Events.Clear();
			if(_Model.HasCollisionShapes) _Model.CollisionShapes.Clear();
		}

		/// <summary>
		/// Adds a new node.
		/// </summary>
		/// <param name="Node">The node to add</param>
		public void Add(INode Node)
		{
			if(Node == null) return;
			if(Node.Model != _Model) throw new System.InvalidOperationException("The node belongs to another model!");

			if(Node is CBone) _Model.Bones.Add(Node as CBone);
			if(Node is CLight) _Model.Lights.Add(Node as CLight);
			if(Node is CHelper) _Model.Helpers.Add(Node as CHelper);
			if(Node is CAttachment) _Model.Attachments.Add(Node as CAttachment);
			if(Node is CParticleEmitter) _Model.ParticleEmitters.Add(Node as CParticleEmitter);
			if(Node is CParticleEmitter2) _Model.ParticleEmitters2.Add(Node as CParticleEmitter2);
			if(Node is CRibbonEmitter) _Model.RibbonEmitters.Add(Node as CRibbonEmitter);
			if(Node is CEvent) _Model.Events.Add(Node as CEvent);
			if(Node is CCollisionShape) _Model.CollisionShapes.Add(Node as CCollisionShape);
		}

		/// <summary>
		/// Inserts a new node at a specific index.
		/// </summary>
		/// <param name="Index">The index to insert at</param>
		/// <param name="Node">The node to insert</param>
		public void Insert(int Index, INode Node)
		{
			if(Node == null) return;
			if(Node.Model != _Model) throw new System.InvalidOperationException("The node belongs to another model!");

			if(_Model.HasBones)
			{
				if(Index < 0) return;
				if(Index < _Model.Bones.Count) _Model.Bones.Insert(Index, Node as CBone);

				Index -= _Model.Bones.Count;
			}

			if(_Model.HasLights)
			{
				if(Index < 0) return;
				if(Index < _Model.Lights.Count) _Model.Lights.Insert(Index, Node as CLight);

				Index -= _Model.Lights.Count;
			}

			if(_Model.HasHelpers)
			{
				if(Index < 0) return;
				if(Index < _Model.Helpers.Count) _Model.Helpers.Insert(Index, Node as CHelper);

				Index -= _Model.Helpers.Count;
			}

			if(_Model.HasAttachments)
			{
				if(Index < 0) return;
				if(Index < _Model.Attachments.Count) _Model.Attachments.Insert(Index, Node as CAttachment);

				Index -= _Model.Attachments.Count;
			}

			if(_Model.HasParticleEmitters)
			{
				if(Index < 0) return;
				if(Index < _Model.ParticleEmitters.Count) _Model.ParticleEmitters.Insert(Index, Node as CParticleEmitter);

				Index -= _Model.ParticleEmitters.Count;
			}

			if(_Model.HasParticleEmitters2)
			{
				if(Index < 0) return;
				if(Index < _Model.ParticleEmitters2.Count) _Model.ParticleEmitters2.Insert(Index, Node as CParticleEmitter2);

				Index -= _Model.ParticleEmitters2.Count;
			}

			if(_Model.HasRibbonEmitters)
			{
				if(Index < 0) return;
				if(Index < _Model.RibbonEmitters.Count) _Model.RibbonEmitters.Insert(Index, Node as CRibbonEmitter);

				Index -= _Model.RibbonEmitters.Count;
			}

			if(_Model.HasEvents)
			{
				if(Index < 0) return;
				if(Index < _Model.Events.Count) _Model.Events.Insert(Index, Node as CEvent);

				Index -= _Model.Events.Count;
			}

			if(_Model.HasCollisionShapes)
			{
				if(Index < 0) return;
				if(Index < _Model.CollisionShapes.Count) _Model.CollisionShapes.Insert(Index, Node as CCollisionShape);
			}
		}

		/// <summary>
		/// Sets a new node at a specific index (removing whatever is there).
		/// </summary>
		/// <param name="Index">The index to set at</param>
		/// <param name="Node">The node to set</param>
		public void Set(int Index, INode Node)
		{
			if(Node == null) return;
			if(Node.Model != _Model) throw new System.InvalidOperationException("The node belongs to another model!");

			if(_Model.HasBones)
			{
				if(Index < 0) return;
				if(Index < _Model.Bones.Count) _Model.Bones.Set(Index, Node as CBone);

				Index -= _Model.Bones.Count;
			}

			if(_Model.HasLights)
			{
				if(Index < 0) return;
				if(Index < _Model.Lights.Count) _Model.Lights.Set(Index, Node as CLight);

				Index -= _Model.Lights.Count;
			}

			if(_Model.HasHelpers)
			{
				if(Index < 0) return;
				if(Index < _Model.Helpers.Count) _Model.Helpers.Set(Index, Node as CHelper);

				Index -= _Model.Helpers.Count;
			}

			if(_Model.HasAttachments)
			{
				if(Index < 0) return;
				if(Index < _Model.Attachments.Count) _Model.Attachments.Set(Index, Node as CAttachment);

				Index -= _Model.Attachments.Count;
			}

			if(_Model.HasParticleEmitters)
			{
				if(Index < 0) return;
				if(Index < _Model.ParticleEmitters.Count) _Model.ParticleEmitters.Set(Index, Node as CParticleEmitter);

				Index -= _Model.ParticleEmitters.Count;
			}

			if(_Model.HasParticleEmitters2)
			{
				if(Index < 0) return;
				if(Index < _Model.ParticleEmitters2.Count) _Model.ParticleEmitters2.Set(Index, Node as CParticleEmitter2);

				Index -= _Model.ParticleEmitters2.Count;
			}

			if(_Model.HasRibbonEmitters)
			{
				if(Index < 0) return;
				if(Index < _Model.RibbonEmitters.Count) _Model.RibbonEmitters.Set(Index, Node as CRibbonEmitter);

				Index -= _Model.RibbonEmitters.Count;
			}

			if(_Model.HasEvents)
			{
				if(Index < 0) return;
				if(Index < _Model.Events.Count) _Model.Events.Set(Index, Node as CEvent);

				Index -= _Model.Events.Count;
			}

			if(_Model.HasCollisionShapes)
			{
				if(Index < 0) return;
				if(Index < _Model.CollisionShapes.Count) _Model.CollisionShapes.Set(Index, Node as CCollisionShape);
			}
		}

		/// <summary>
		/// Removes an existing node.
		/// </summary>
		/// <param name="Node">The node to remove</param>
		/// <returns>True on success, False on failure</returns>
		public bool Remove(INode Node)
		{
			if(Node == null) return false;

			if(Node is CBone) return _Model.Bones.Remove(Node as CBone);
			if(Node is CLight) return _Model.Lights.Remove(Node as CLight);
			if(Node is CHelper) return _Model.Helpers.Remove(Node as CHelper);
			if(Node is CAttachment) return _Model.Attachments.Remove(Node as CAttachment);
			if(Node is CParticleEmitter) return _Model.ParticleEmitters.Remove(Node as CParticleEmitter);
			if(Node is CParticleEmitter2) return _Model.ParticleEmitters2.Remove(Node as CParticleEmitter2);
			if(Node is CRibbonEmitter) return _Model.RibbonEmitters.Remove(Node as CRibbonEmitter);
			if(Node is CEvent) return _Model.Events.Remove(Node as CEvent);
			if(Node is CCollisionShape) return _Model.CollisionShapes.Remove(Node as CCollisionShape);

			return false;
		}

		/// <summary>
		/// Removes an existing node at a specific index.
		/// </summary>
		/// <param name="Index">The index to remove at</param>
		public void RemoveAt(int Index)
		{
			if(_Model.HasBones)
			{
				if(Index < 0) return;
				if(Index < _Model.Bones.Count) _Model.Bones.RemoveAt(Index);

				Index -= _Model.Bones.Count;
			}

			if(_Model.HasLights)
			{
				if(Index < 0) return;
				if(Index < _Model.Lights.Count) _Model.Lights.RemoveAt(Index);

				Index -= _Model.Lights.Count;
			}

			if(_Model.HasHelpers)
			{
				if(Index < 0) return;
				if(Index < _Model.Helpers.Count) _Model.Helpers.RemoveAt(Index);

				Index -= _Model.Helpers.Count;
			}

			if(_Model.HasAttachments)
			{
				if(Index < 0) return;
				if(Index < _Model.Attachments.Count) _Model.Attachments.RemoveAt(Index);

				Index -= _Model.Attachments.Count;
			}

			if(_Model.HasParticleEmitters)
			{
				if(Index < 0) return;
				if(Index < _Model.ParticleEmitters.Count) _Model.ParticleEmitters.RemoveAt(Index);

				Index -= _Model.ParticleEmitters.Count;
			}

			if(_Model.HasParticleEmitters2)
			{
				if(Index < 0) return;
				if(Index < _Model.ParticleEmitters2.Count) _Model.ParticleEmitters2.RemoveAt(Index);

				Index -= _Model.ParticleEmitters2.Count;
			}

			if(_Model.HasRibbonEmitters)
			{
				if(Index < 0) return;
				if(Index < _Model.RibbonEmitters.Count) _Model.RibbonEmitters.RemoveAt(Index);

				Index -= _Model.RibbonEmitters.Count;
			}

			if(_Model.HasEvents)
			{
				if(Index < 0) return;
				if(Index < _Model.Events.Count) _Model.Events.RemoveAt(Index);

				Index -= _Model.Events.Count;
			}

			if(_Model.HasCollisionShapes)
			{
				if(Index < 0) return;
				if(Index < _Model.CollisionShapes.Count) _Model.CollisionShapes.RemoveAt(Index);
			}
		}

		/// <summary>
		/// Retrieves the node at a specific index.
		/// </summary>
		/// <param name="Index">The index to retrieve at</param>
		/// <returns>The retrieved node, null on failure</returns>
		public INode Get(int Index)
		{
			if(_Model.HasBones)
			{
				if(Index < 0) return null;
				if(Index < _Model.Bones.Count) return _Model.Bones.Get(Index);

				Index -= _Model.Bones.Count;
			}

			if(_Model.HasLights)
			{
				if(Index < 0) return null;
				if(Index < _Model.Lights.Count) return _Model.Lights.Get(Index);

				Index -= _Model.Lights.Count;
			}

			if(_Model.HasHelpers)
			{
				if(Index < 0) return null;
				if(Index < _Model.Helpers.Count) return _Model.Helpers.Get(Index);

				Index -= _Model.Helpers.Count;
			}

			if(_Model.HasAttachments)
			{
				if(Index < 0) return null;
				if(Index < _Model.Attachments.Count) return _Model.Attachments.Get(Index);

				Index -= _Model.Attachments.Count;
			}

			if(_Model.HasParticleEmitters)
			{
				if(Index < 0) return null;
				if(Index < _Model.ParticleEmitters.Count) return _Model.ParticleEmitters.Get(Index);

				Index -= _Model.ParticleEmitters.Count;
			}

			if(_Model.HasParticleEmitters2)
			{
				if(Index < 0) return null;
				if(Index < _Model.ParticleEmitters2.Count) return _Model.ParticleEmitters2.Get(Index);

				Index -= _Model.ParticleEmitters2.Count;
			}

			if(_Model.HasRibbonEmitters)
			{
				if(Index < 0) return null;
				if(Index < _Model.RibbonEmitters.Count) return _Model.RibbonEmitters.Get(Index);

				Index -= _Model.RibbonEmitters.Count;
			}

			if(_Model.HasEvents)
			{
				if(Index < 0) return null;
				if(Index < _Model.Events.Count) return _Model.Events.Get(Index);

				Index -= _Model.Events.Count;
			}

			if(_Model.HasCollisionShapes)
			{
				if(Index < 0) return null;
				if(Index < _Model.CollisionShapes.Count) return _Model.CollisionShapes.Get(Index);
			}

			return null;
		}

		/// <summary>
		/// Retrieves the index of an existing node.
		/// </summary>
		/// <param name="Node">The node whose index to retrieve</param>
		/// <returns>The index of the node, InvalidIndex on failure</returns>
		public int IndexOf(INode Node)
		{
			return ((Node != null) && (Node.Model == _Model)) ? Node.NodeId : CConstants.InvalidIndex;
		}

		/// <summary>
		/// Checks if a node exists in the container.
		/// </summary>
		/// <param name="Node">The node to check for</param>
		/// <returns>True if it exists, False otherwise</returns>
		public bool Contains(INode Node)
		{
			if(Node == null) return false;

			if(Node is CBone) return _Model.Bones.Contains(Node as CBone);
			if(Node is CLight) return _Model.Lights.Contains(Node as CLight);
			if(Node is CHelper) return _Model.Helpers.Contains(Node as CHelper);
			if(Node is CAttachment) return _Model.Attachments.Contains(Node as CAttachment);
			if(Node is CParticleEmitter) return _Model.ParticleEmitters.Contains(Node as CParticleEmitter);
			if(Node is CParticleEmitter2) return _Model.ParticleEmitters2.Contains(Node as CParticleEmitter2);
			if(Node is CRibbonEmitter) return _Model.RibbonEmitters.Contains(Node as CRibbonEmitter);
			if(Node is CEvent) return _Model.Events.Contains(Node as CEvent);
			if(Node is CCollisionShape) return _Model.CollisionShapes.Contains(Node as CCollisionShape);

			return false;
		}

		/// <summary>
		/// Checks if an index exists in the container.
		/// </summary>
		/// <param name="Index">The index to check for</param>
		/// <returns>True if it exists, False otherwise</returns>
		public bool ContainsIndex(int Index)
		{
			return (Index >= 0) && (Index < Count);
		}

		/// <summary>
		/// Copies the contents of the container to an array.
		/// </summary>
		/// <param name="Array">The array to copy to</param>
		/// <param name="Index">The index in the array to start copying to</param>
		public void CopyTo(INode[] Array, int Index)
		{
			int Offset = 0;

			if(_Model.HasBones)
			{
				foreach(CBone Bone in _Model.Bones)
				{
					Array[Index + Offset] = Bone;
					Offset++;
				}
			}

			if(_Model.HasLights)
			{
				foreach(CLight Light in _Model.Lights)
				{
					Array[Index + Offset] = Light;
					Offset++;
				}
			}

			if(_Model.HasHelpers)
			{
				foreach(CHelper Helper in _Model.Helpers)
				{
					Array[Index + Offset] = Helper;
					Offset++;
				}
			}

			if(_Model.HasAttachments)
			{
				foreach(CAttachment Attachment in _Model.Attachments)
				{
					Array[Index + Offset] = Attachment;
					Offset++;
				}
			}

			if(_Model.HasParticleEmitters)
			{
				foreach(CParticleEmitter ParticleEmitter in _Model.ParticleEmitters)
				{
					Array[Index + Offset] = ParticleEmitter;
					Offset++;
				}
			}

			if(_Model.HasParticleEmitters2)
			{
				foreach(CParticleEmitter2 ParticleEmitter2 in _Model.ParticleEmitters2)
				{
					Array[Index + Offset] = ParticleEmitter2;
					Offset++;
				}
			}

			if(_Model.HasRibbonEmitters)
			{
				foreach(CRibbonEmitter RibbonEmitter in _Model.RibbonEmitters)
				{
					Array[Index + Offset] = RibbonEmitter;
					Offset++;
				}
			}

			if(_Model.HasEvents)
			{
				foreach(CEvent Event in _Model.Events)
				{
					Array[Index + Offset] = Event;
					Offset++;
				}
			}

			if(_Model.HasCollisionShapes)
			{
				foreach(CCollisionShape CollisionShape in _Model.CollisionShapes)
				{
					Array[Index + Offset] = CollisionShape;
					Offset++;
				}
			}
		}

		/// <summary>
		/// Retrieves an enumerator for the nodes in the container.
		/// </summary>
		/// <returns>The retrieved enumerator</returns>
		public System.Collections.Generic.IEnumerator<INode> GetEnumerator()
		{
			if(_Model.HasBones)
			{
				foreach(CBone Bone in _Model.Bones)
				{
					yield return Bone;
				}
			}

			if(_Model.HasLights)
			{
				foreach(CLight Light in _Model.Lights)
				{
					yield return Light;
				}
			}

			if(_Model.HasHelpers)
			{
				foreach(CHelper Helper in _Model.Helpers)
				{
					yield return Helper;
				}
			}

			if(_Model.HasAttachments)
			{
				foreach(CAttachment Attachment in _Model.Attachments)
				{
					yield return Attachment;
				}
			}

			if(_Model.HasParticleEmitters)
			{
				foreach(CParticleEmitter ParticleEmitter in _Model.ParticleEmitters)
				{
					yield return ParticleEmitter;
				}
			}

			if(_Model.HasParticleEmitters2)
			{
				foreach(CParticleEmitter2 ParticleEmitter2 in _Model.ParticleEmitters2)
				{
					yield return ParticleEmitter2;
				}
			}

			if(_Model.HasRibbonEmitters)
			{
				foreach(CRibbonEmitter RibbonEmitter in _Model.RibbonEmitters)
				{
					yield return RibbonEmitter;
				}
			}

			if(_Model.HasEvents)
			{
				foreach(CEvent Event in _Model.Events)
				{
					yield return Event;
				}
			}

			if(_Model.HasCollisionShapes)
			{
				foreach(CCollisionShape CollisionShape in _Model.CollisionShapes)
				{
					yield return CollisionShape;
				}
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
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
		/// Retrieves the number of nodes in the container.
		/// </summary>
		public int Count
		{
			get
			{
				int TotalCount = 0;

				if(_Model.HasBones) TotalCount += _Model.Bones.Count;
				if(_Model.HasLights) TotalCount += _Model.Lights.Count;
				if(_Model.HasHelpers) TotalCount += _Model.Helpers.Count;
				if(_Model.HasAttachments) TotalCount += _Model.Attachments.Count;
				if(_Model.HasParticleEmitters) TotalCount += _Model.ParticleEmitters.Count;
				if(_Model.HasParticleEmitters2) TotalCount += _Model.ParticleEmitters2.Count;
				if(_Model.HasRibbonEmitters) TotalCount += _Model.RibbonEmitters.Count;
				if(_Model.HasEvents) TotalCount += _Model.Events.Count;
				if(_Model.HasCollisionShapes) TotalCount += _Model.CollisionShapes.Count;

				return TotalCount;
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
		/// Gets or sets a node in the container.
		/// </summary>
		/// <param name="Index">The index to get or set at</param>
		/// <returns>The accessed node</returns>
		public INode this[int Index]
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

		private CModel _Model = null;
	}
}
