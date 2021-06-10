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
	internal sealed class CSequence : CObject
	{
		private CSequence()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CSequence Sequence = new Model.CSequence(Model);
				Load(Loader, Model, Sequence);
				Model.Sequences.Add(Sequence);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Sequence bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CSequence Sequence)
		{
			Sequence.Name = Loader.ReadString(CConstants.SizeName);
			Sequence.IntervalStart = Loader.ReadInt32();
			Sequence.IntervalEnd = Loader.ReadInt32();
			Sequence.MoveSpeed = Loader.ReadFloat();

			int Flags = Loader.ReadInt32();

			Sequence.Rarity = Loader.ReadFloat();
			Sequence.SyncPoint = Loader.ReadInt32();
			Sequence.Extent = Loader.ReadExtent();

			Sequence.NonLooping = ((Flags & 1) != 0);
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasSequences)
			{
				Saver.WriteTag("SEQS");
				Saver.PushLocation();

				foreach(Model.CSequence Sequence in Model.Sequences)
				{
					Save(Saver, Model, Sequence);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CSequence Sequence)
		{
			int Flags = 0;

			if(Sequence.NonLooping) Flags |= 1;

			Saver.WriteString(Sequence.Name, CConstants.SizeName);
			Saver.WriteInt32(Sequence.IntervalStart);
			Saver.WriteInt32(Sequence.IntervalEnd);
			Saver.WriteFloat(Sequence.MoveSpeed);
			Saver.WriteInt32(Flags);
			Saver.WriteFloat(Sequence.Rarity);
			Saver.WriteInt32(Sequence.SyncPoint);
			Saver.WriteExtent(Sequence.Extent);
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
