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
namespace MdxLib.ModelFormats.Xml
{
	internal sealed class CEvent : CNode
	{
		private CEvent()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CEvent Event)
		{
			LoadNode(Loader, Node, Model, Event);

			Loader.Attacher.AddObject(Model.GlobalSequences, Event.GlobalSequence, ReadInteger(Node, "global_sequence", CConstants.InvalidId));

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("event_track"))
			{
				Model.CEventTrack EventTrack = new Model.CEventTrack(Model);
				CEventTrack.Instance.Load(Loader, ChildNode, Model, Event, EventTrack);
				Event.Tracks.Add(EventTrack);
			}
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CEvent Event)
		{
			SaveNode(Saver, Node, Model, Event);

			WriteInteger(Node, "global_sequence", Event.GlobalSequence.ObjectId);

			if(Event.HasTracks)
			{
				foreach(Model.CEventTrack EventTrack in Event.Tracks)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "event_track");
					CEventTrack.Instance.Save(Saver, Element, Model, Event, EventTrack);
				}
			}
		}

		public static CEvent Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CEvent Instance = new CEvent();
		}
	}
}
