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
	internal sealed class CGlobalSequence : CObject
	{
		private CGlobalSequence()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CGlobalSequence GlobalSequence = new Model.CGlobalSequence(Model);
				Load(Loader, Model, GlobalSequence);
				Model.GlobalSequences.Add(GlobalSequence);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many GlobalSequence bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CGlobalSequence GlobalSequence)
		{
			GlobalSequence.Duration = Loader.ReadInt32();
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasGlobalSequences)
			{
				Saver.WriteTag("GLBS");
				Saver.PushLocation();

				foreach(Model.CGlobalSequence GlobalSequence in Model.GlobalSequences)
				{
					Save(Saver, Model, GlobalSequence);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CGlobalSequence GlobalSequence)
		{
			Saver.WriteInt32(GlobalSequence.Duration);
		}

		public static CGlobalSequence Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CGlobalSequence Instance = new CGlobalSequence();
		}
	}
}
