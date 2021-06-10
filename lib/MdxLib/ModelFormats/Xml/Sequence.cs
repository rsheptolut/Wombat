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
	internal sealed class CSequence : CObject
	{
		private CSequence()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CSequence Sequence)
		{
			Sequence.Name = ReadString(Node, "name", Sequence.Name);
			Sequence.Rarity = ReadFloat(Node, "rarity", Sequence.Rarity);
			Sequence.MoveSpeed = ReadFloat(Node, "move_speed", Sequence.MoveSpeed);
			Sequence.IntervalStart = ReadInteger(Node, "interval_start", Sequence.IntervalStart);
			Sequence.IntervalEnd = ReadInteger(Node, "interval_end", Sequence.IntervalEnd);
			Sequence.SyncPoint = ReadInteger(Node, "sync_point", Sequence.SyncPoint);
			Sequence.NonLooping = ReadBoolean(Node, "non_looping", Sequence.NonLooping);
			Sequence.Extent = ReadExtent(Node, "extent", Sequence.Extent);
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CSequence Sequence)
		{
			WriteString(Node, "name", Sequence.Name);
			WriteFloat(Node, "rarity", Sequence.Rarity);
			WriteFloat(Node, "move_speed", Sequence.MoveSpeed);
			WriteInteger(Node, "interval_start", Sequence.IntervalStart);
			WriteInteger(Node, "interval_end", Sequence.IntervalEnd);
			WriteInteger(Node, "sync_point", Sequence.SyncPoint);
			WriteBoolean(Node, "non_looping", Sequence.NonLooping);
			WriteExtent(Node, "extent", Sequence.Extent);
		}

		public static CSequence Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CSequence Instance = new CSequence();
		}
	}
}
