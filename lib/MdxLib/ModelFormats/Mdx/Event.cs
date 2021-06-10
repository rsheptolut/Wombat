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
namespace MdxLib.ModelFormats.Mdx
{
	internal sealed class CEvent : CNode
	{
		private CEvent()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CEvent Event = new Model.CEvent(Model);
				Load(Loader, Model, Event);
				Model.Events.Add(Event);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Event bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CEvent Event)
		{
			LoadNode(Loader, Model, Event);
			Loader.ExpectTag("KEVT");
			int NrOfTracks = Loader.ReadInt32();
			Loader.Attacher.AddObject(Model.GlobalSequences, Event.GlobalSequence, Loader.ReadInt32());

			for(int i = 0; i < NrOfTracks; i++)
			{
				Model.CEventTrack Track = new Model.CEventTrack(Model);
				LoadTrack(Loader, Model, Event, Track);
				Event.Tracks.Add(Track);
			}
		}

		public void LoadTrack(CLoader Loader, Model.CModel Model, Model.CEvent Event, Model.CEventTrack Track)
		{
			Track.Time = Loader.ReadInt32();
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasEvents)
			{
				Saver.WriteTag("EVTS");
				Saver.PushLocation();

				foreach(Model.CEvent Event in Model.Events)
				{
					Save(Saver, Model, Event);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CEvent Event)
		{
			SaveNode(Saver, Model, Event, 1024);

			Saver.WriteTag("KEVT");
			Saver.WriteInt32(Event.Tracks.Count);
			Saver.WriteInt32(Event.GlobalSequence.ObjectId);

			foreach(Model.CEventTrack Track in Event.Tracks)
			{
				SaveTrack(Saver, Model, Event, Track);
			}
		}

		public void SaveTrack(CSaver Saver, Model.CModel Model, Model.CEvent Event, Model.CEventTrack Track)
		{
			Saver.WriteInt32(Track.Time);
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
