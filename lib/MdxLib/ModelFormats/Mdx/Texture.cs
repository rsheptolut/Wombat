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
	internal sealed class CTexture : CObject
	{
		private CTexture()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CTexture Texture = new Model.CTexture(Model);
				Load(Loader, Model, Texture);
				Model.Textures.Add(Texture);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Texture bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CTexture Texture)
		{
			Texture.ReplaceableId = Loader.ReadInt32();
			Texture.FileName = Loader.ReadString(CConstants.SizeFileName);

			int Flags = Loader.ReadInt32();

			Texture.WrapWidth = ((Flags & 1) != 0);
			Texture.WrapHeight = ((Flags & 2) != 0);
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasTextures)
			{
				Saver.WriteTag("TEXS");
				Saver.PushLocation();

				foreach(Model.CTexture Texture in Model.Textures)
				{
					Save(Saver, Model, Texture);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CTexture Texture)
		{
			int Flags = 0;

			if(Texture.WrapWidth) Flags |= 1;
			if(Texture.WrapHeight) Flags |= 2;

			Saver.WriteInt32(Texture.ReplaceableId);
			Saver.WriteString(Texture.FileName,CConstants.SizeFileName);
			Saver.WriteInt32(Flags);
		}

		public static CTexture Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CTexture Instance = new CTexture();
		}
	}
}
