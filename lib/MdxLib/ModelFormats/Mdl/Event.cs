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
namespace MdxLib.ModelFormats.Mdl
{
	internal sealed class CEvent : CNode
	{
		private CEvent()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			Model.CEvent Event = new Model.CEvent(Model);
			Load(Loader, Model, Event);
			Model.Events.Add(Event);
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CEvent Event)
		{
			Event.Name = Loader.ReadString();
			Loader.ExpectToken(Token.EType.CurlyBracketLeft);

			while(true)
			{
				if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
				{
					Loader.ReadToken();
					break;
				}

				string Tag = Loader.ReadWord();

				if(!LoadNode(Loader, Model, Event, Tag))
				{
					switch(Tag)
					{
						case "static":
						{
							Tag = Loader.ReadWord();

							if(!LoadStaticNode(Loader, Model, Event, Tag))
							{
								switch(Tag)
								{
									default:
									{
										throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
									}
								}
							}

							break;
						}

						case "eventtrack":
						{
							Loader.ReadInteger();
							Loader.ExpectToken(Token.EType.CurlyBracketLeft);

							while(true)
							{
								if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
								{
									Loader.ReadToken();
									break;
								}

								Model.CEventTrack Track = new Model.CEventTrack(Model);
								Track.Time = LoadInteger(Loader);
								Event.Tracks.Add(Track);
							}

							break;
						}

						default:
						{
							throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
						}
					}
				}
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasEvents)
			{
				foreach(Model.CEvent Event in Model.Events)
				{
					Save(Saver, Model, Event);
				}
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CEvent Event)
		{
			Saver.BeginGroup("EventObject", Event.Name);

			SaveNode(Saver, Model, Event);

			Saver.BeginGroup("EventTrack", Event.Tracks.Count);

			foreach(Model.CEventTrack Track in Event.Tracks)
			{
				Saver.WriteTabs();
				Saver.WriteInteger(Track.Time);
				Saver.WriteLine(",");
			}

			Saver.EndGroup();
			Saver.EndGroup();
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
