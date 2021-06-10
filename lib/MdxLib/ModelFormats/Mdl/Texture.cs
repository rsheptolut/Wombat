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
	internal sealed class CTexture : CObject
	{
		private CTexture()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
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

				string Tag = Loader.ReadWord();

				switch(Tag)
				{
					case "bitmap":
					{
						Model.CTexture Texture = new Model.CTexture(Model);
						Load(Loader, Model, Texture);
						Model.Textures.Add(Texture);
						break;
					}

					default:
					{
						throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
					}
				}
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CTexture Texture)
		{
			Loader.ExpectToken(Token.EType.CurlyBracketLeft);

			while(true)
			{
				if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
				{
					Loader.ReadToken();
					break;
				}

				string Tag = Loader.ReadWord();

				switch(Tag)
				{
					case "image": { Texture.FileName = LoadString(Loader); break; }
					case "replaceableid": { Texture.ReplaceableId = LoadInteger(Loader); break; }
					case "wrapwidth": { Texture.WrapWidth = LoadBoolean(Loader); break; }
					case "wrapheight": { Texture.WrapHeight = LoadBoolean(Loader); break; }

					default:
					{
						throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
					}
				}
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasTextures)
			{
				Saver.BeginGroup("Textures", Model.Textures.Count);

				foreach(Model.CTexture Texture in Model.Textures)
				{
					Save(Saver, Model, Texture);
				}

				Saver.EndGroup();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CTexture Texture)
		{
			Saver.BeginGroup("Bitmap");

			SaveString(Saver, "Image", Texture.FileName);
			SaveInteger(Saver, "ReplaceableId", Texture.ReplaceableId, ECondition.NotZero);
			SaveBoolean(Saver, "WrapWidth", Texture.WrapWidth);
			SaveBoolean(Saver, "WrapHeight", Texture.WrapHeight);

			Saver.EndGroup();
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
