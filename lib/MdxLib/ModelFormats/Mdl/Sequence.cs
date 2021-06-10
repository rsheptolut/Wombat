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
	internal sealed class CSequence : CObject
	{
		private CSequence()
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
					case "anim":
					{
						Model.CSequence Sequence = new Model.CSequence(Model);
						Load(Loader, Model, Sequence);
						Model.Sequences.Add(Sequence);
						break;
					}

					default:
					{
						throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
					}
				}
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CSequence Sequence)
		{
			float ExtentRadius = 0.0f;
			Primitives.CVector3 ExtentMin = CConstants.DefaultVector3;
			Primitives.CVector3 ExtentMax = CConstants.DefaultVector3;

			Sequence.Name = Loader.ReadString();
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
					case "syncpoint": { Sequence.SyncPoint = LoadInteger(Loader); break; }
					case "rarity": { Sequence.Rarity = LoadFloat(Loader); break; }
					case "movespeed": { Sequence.MoveSpeed = LoadFloat(Loader); break; }
					case "minimumextent": { ExtentMin = LoadVector3(Loader); break; }
					case "maximumextent": { ExtentMax = LoadVector3(Loader); break; }
					case "boundsradius": { ExtentRadius = LoadFloat(Loader); break; }
					case "nonlooping": { Sequence.NonLooping = LoadBoolean(Loader); break; }

					case "interval":
					{
						Loader.ExpectToken(Token.EType.CurlyBracketLeft);
						Sequence.IntervalStart = Loader.ReadInteger();
						Loader.ExpectToken(Token.EType.Separator);
						Sequence.IntervalEnd = Loader.ReadInteger();
						Loader.ExpectToken(Token.EType.CurlyBracketRight);
						Loader.ExpectToken(Token.EType.Separator);
						break;
					}

					default:
					{
						throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
					}
				}
			}

			Sequence.Extent = new Primitives.CExtent(ExtentMin, ExtentMax, ExtentRadius);
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasSequences)
			{
				Saver.BeginGroup("Sequences", Model.Sequences.Count);

				foreach(Model.CSequence Sequence in Model.Sequences)
				{
					Save(Saver, Model, Sequence);
				}

				Saver.EndGroup();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CSequence Sequence)
		{
			Saver.BeginGroup("Anim", Sequence.Name);

			Saver.WriteTabs();
			Saver.WriteWord("Interval { ");
			Saver.WriteInteger(Sequence.IntervalStart);
			Saver.WriteWord(", ");
			Saver.WriteInteger(Sequence.IntervalEnd);
			Saver.WriteLine(" },");

			SaveInteger(Saver, "SyncPoint", Sequence.SyncPoint, ECondition.NotZero);
			SaveFloat(Saver, "Rarity", Sequence.Rarity, ECondition.NotZero);
			SaveFloat(Saver, "MoveSpeed", Sequence.MoveSpeed, ECondition.NotZero);
			SaveBoolean(Saver, "NonLooping", Sequence.NonLooping);
			SaveVector3(Saver, "MinimumExtent", Sequence.Extent.Min, ECondition.NotZero);
			SaveVector3(Saver, "MaximumExtent", Sequence.Extent.Max, ECondition.NotZero);
			SaveFloat(Saver, "BoundsRadius", Sequence.Extent.Radius, ECondition.NotZero);

			Saver.EndGroup();
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
