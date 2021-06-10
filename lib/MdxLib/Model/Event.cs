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
	/// An event class. Performs certain actions during an animation.
	/// </summary>
	public sealed class CEvent : CNode<CEvent>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this event</param>
		public CEvent(CModel Model) : base(Model)
		{
			//Empty
		}

		internal override void BuildDetacherList(System.Collections.Generic.ICollection<CDetacher> DetacherList)
		{
			base.BuildDetacherList(DetacherList);
			if(_Tracks != null) _Tracks.BuildDetacherList(DetacherList);
		}

		/// <summary>
		/// Generates a string version of the event.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Event #" + ObjectId;
		}

		/// <summary>
		/// Retrieves the node ID (if added to a container).
		/// </summary>
		public override int NodeId
		{
			get
			{
				return Model.GetEventNodeId(this);
			}
		}

		/// <summary>
		/// Checks if the event has references pointing to it.
		/// </summary>
		public override bool HasReferences
		{
			get
			{
				if((_Tracks != null) && _Tracks.HasReferences) return true;
				return base.HasReferences;
			}
		}

		/// <summary>
		/// Retrieves the global sequence reference.
		/// </summary>
		public CObjectReference<CGlobalSequence> GlobalSequence
		{
			get
			{
				return _GlobalSequence ?? (_GlobalSequence = new CObjectReference<CGlobalSequence>(Model));
			}
		}

		/// <summary>
		/// Checks if there exists some event tracks.
		/// </summary>
		public bool HasTracks
		{
			get
			{
				return (_Tracks != null) ? (_Tracks.Count > 0) : false;
			}
		}

		/// <summary>
		/// Retrieves the event tracks container.
		/// </summary>
		public CObjectContainer<CEventTrack> Tracks
		{
			get
			{
				return _Tracks ?? (_Tracks = new CObjectContainer<CEventTrack>(Model));
			}
		}

		private CObjectReference<CGlobalSequence> _GlobalSequence = null;

		private CObjectContainer<CEventTrack> _Tracks = null;
	}
}
