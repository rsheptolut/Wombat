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
	internal sealed class CGlobalSequence : CObject
	{
		private CGlobalSequence()
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
					case "duration":
					{
						Model.CGlobalSequence GlobalSequence = new Model.CGlobalSequence(Model);
						Load(Loader, Model, GlobalSequence);
						Model.GlobalSequences.Add(GlobalSequence);
						break;
					}

					default:
					{
						throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
					}
				}
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CGlobalSequence GlobalSequence)
		{
			GlobalSequence.Duration = LoadInteger(Loader);
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasGlobalSequences)
			{
				Saver.BeginGroup("GlobalSequences", Model.GlobalSequences.Count);

				foreach(Model.CGlobalSequence GlobalSequence in Model.GlobalSequences)
				{
					Save(Saver, Model, GlobalSequence);
				}

				Saver.EndGroup();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CGlobalSequence GlobalSequence)
		{
			Saver.WriteTabs();
			Saver.WriteWord("Duration ");
			Saver.WriteInteger(GlobalSequence.Duration);
			Saver.WriteLine(",");
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
