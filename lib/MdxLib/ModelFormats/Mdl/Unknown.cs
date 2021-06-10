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
	internal abstract class CUnknown
	{
		public CUnknown()
		{
			//Empty
		}

		public int LoadId(CLoader Loader)
		{
			int Value = Loader.ReadId();
			Loader.ExpectToken(Token.EType.Separator);
			return Value;
		}

		public bool LoadBoolean(CLoader Loader)
		{
			Loader.ExpectToken(Token.EType.Separator);
			return true;
		}

		public int LoadInteger(CLoader Loader)
		{
			int Value = Loader.ReadInteger();
			Loader.ExpectToken(Token.EType.Separator);
			return Value;
		}

		public float LoadFloat(CLoader Loader)
		{
			float Value = Loader.ReadFloat();
			Loader.ExpectToken(Token.EType.Separator);
			return Value;
		}

		public string LoadString(CLoader Loader)
		{
			string Value = Loader.ReadString();
			Loader.ExpectToken(Token.EType.Separator);
			return Value;
		}

		public string LoadWord(CLoader Loader)
		{
			string Value = Loader.ReadWord();
			Loader.ExpectToken(Token.EType.Separator);
			return Value;
		}

		public Primitives.CVector2 LoadVector2(CLoader Loader)
		{
			Primitives.CVector2 Value = Loader.ReadVector2();
			Loader.ExpectToken(Token.EType.Separator);
			return Value;
		}

		public Primitives.CVector3 LoadVector3(CLoader Loader)
		{
			Primitives.CVector3 Value = Loader.ReadVector3();
			Loader.ExpectToken(Token.EType.Separator);
			return Value;
		}

		public Primitives.CVector4 LoadVector4(CLoader Loader)
		{
			Primitives.CVector4 Value = Loader.ReadVector4();
			Loader.ExpectToken(Token.EType.Separator);
			return Value;
		}

		public Primitives.CVector3 LoadColor(CLoader Loader)
		{
			Primitives.CVector3 Value = Loader.ReadColor();
			Loader.ExpectToken(Token.EType.Separator);
			return Value;
		}

		public void SaveId(CSaver Saver, string Name, int Value)
		{
			SaveId(Saver, Name, Value, ECondition.Always, false);
		}

		public void SaveId(CSaver Saver, string Name, int Value, ECondition Condition)
		{
			SaveId(Saver, Name, Value, Condition, false);
		}

		public void SaveId(CSaver Saver, string Name, int Value, bool UseMultipleAsNone)
		{
			SaveId(Saver, Name, Value, ECondition.Always, UseMultipleAsNone);
		}

		public void SaveId(CSaver Saver, string Name, int Value, ECondition Condition, bool UseMultipleAsNone)
		{
			switch(Condition)
			{
				case ECondition.NotInvalidId:
				{
					if(Value == CConstants.InvalidId) return;
					break;
				}
			}

			Saver.WriteTabs();
			Saver.WriteWord(Name + " ");
			Saver.WriteWord((Value != CConstants.InvalidId) ? Value.ToString() : (UseMultipleAsNone ? "Multiple" : "None"));
			Saver.WriteLine(",");
		}

		public void SaveBoolean(CSaver Saver, string Name, bool Value)
		{
			if(Value)
			{
				Saver.WriteTabs();
				Saver.WriteLine(Name + ",");
			}
		}

		public void SaveInteger(CSaver Saver, string Name, int Value)
		{
			SaveInteger(Saver, Name, Value, ECondition.Always);
		}

		public void SaveInteger(CSaver Saver, string Name, int Value, ECondition Condition)
		{
			switch(Condition)
			{
				case ECondition.NotZero:
				{
					if(Value == 0) return;
					break;
				}

				case ECondition.NotOne:
				{
					if(Value == 1) return;
					break;
				}
			}

			Saver.WriteTabs();
			Saver.WriteWord(Name + " ");
			Saver.WriteInteger(Value);
			Saver.WriteLine(",");
		}

		public void SaveFloat(CSaver Saver, string Name, float Value)
		{
			SaveFloat(Saver, Name, Value, ECondition.Always);
		}

		public void SaveFloat(CSaver Saver, string Name, float Value, ECondition Condition)
		{
			switch(Condition)
			{
				case ECondition.NotZero:
				{
					if(Value == 0.0f) return;
					break;
				}

				case ECondition.NotOne:
				{
					if(Value == 1.0f) return;
					break;
				}
			}

			Saver.WriteTabs();
			Saver.WriteWord(Name + " ");
			Saver.WriteFloat(Value);
			Saver.WriteLine(",");
		}

		public void SaveString(CSaver Saver, string Name, string Value)
		{
			SaveString(Saver, Name, Value, ECondition.Always);
		}

		public void SaveString(CSaver Saver, string Name, string Value, ECondition Condition)
		{
			switch(Condition)
			{
				case ECondition.NotEmpty:
				{
					if(Value == "") return;
					break;
				}
			}

			Saver.WriteTabs();
			Saver.WriteWord(Name + " ");
			Saver.WriteString(Value);
			Saver.WriteLine(",");
		}

		public void SaveVector2(CSaver Saver, string Name, Primitives.CVector2 Value)
		{
			SaveVector2(Saver, Name, Value, ECondition.Always);
		}

		public void SaveVector2(CSaver Saver, string Name, Primitives.CVector2 Value, ECondition Condition)
		{
			switch(Condition)
			{
				case ECondition.NotZero:
				{
					if(Value.X != 0.0f) break;
					if(Value.Y != 0.0f) break;
					return;
				}

				case ECondition.NotOne:
				{
					if(Value.X != 1.0f) break;
					if(Value.Y != 1.0f) break;
					return;
				}
			}

			Saver.WriteTabs();
			Saver.WriteWord(Name + " ");
			Saver.WriteVector2(Value);
			Saver.WriteLine(",");
		}

		public void SaveVector3(CSaver Saver, string Name, Primitives.CVector3 Value)
		{
			SaveVector3(Saver, Name, Value, ECondition.Always);
		}

		public void SaveVector3(CSaver Saver, string Name, Primitives.CVector3 Value, ECondition Condition)
		{
			switch(Condition)
			{
				case ECondition.NotZero:
				{
					if(Value.X != 0.0f) break;
					if(Value.Y != 0.0f) break;
					if(Value.Z != 0.0f) break;
					return;
				}

				case ECondition.NotOne:
				{
					if(Value.X != 1.0f) break;
					if(Value.Y != 1.0f) break;
					if(Value.Z != 1.0f) break;
					return;
				}
			}

			Saver.WriteTabs();
			Saver.WriteWord(Name + " ");
			Saver.WriteVector3(Value);
			Saver.WriteLine(",");
		}

		public void SaveVector4(CSaver Saver, string Name, Primitives.CVector4 Value)
		{
			SaveVector4(Saver, Name, Value, ECondition.Always);
		}

		public void SaveVector4(CSaver Saver, string Name, Primitives.CVector4 Value, ECondition Condition)
		{
			switch(Condition)
			{
				case ECondition.NotZero:
				{
					if(Value.X != 0.0f) break;
					if(Value.Y != 0.0f) break;
					if(Value.Z != 0.0f) break;
					if(Value.W != 0.0f) break;
					return;
				}

				case ECondition.NotOne:
				{
					if(Value.X != 1.0f) break;
					if(Value.Y != 1.0f) break;
					if(Value.Z != 1.0f) break;
					if(Value.W != 1.0f) break;
					return;
				}

				case ECondition.NotDefaultQuaternion:
				{
					if(Value.X != 0.0f) break;
					if(Value.Y != 0.0f) break;
					if(Value.Z != 0.0f) break;
					if(Value.W != 1.0f) break;
					return;
				}
			}

			Saver.WriteTabs();
			Saver.WriteWord(Name + " ");
			Saver.WriteVector4(Value);
			Saver.WriteLine(",");
		}

		public void SaveColor(CSaver Saver, string Name, Primitives.CVector3 Value)
		{
			SaveColor(Saver, Name, Value, ECondition.Always);
		}

		public void SaveColor(CSaver Saver, string Name, Primitives.CVector3 Value, ECondition Condition)
		{
			switch(Condition)
			{
				case ECondition.NotZero:
				{
					if(Value.X != 0.0f) break;
					if(Value.Y != 0.0f) break;
					if(Value.Z != 0.0f) break;
					return;
				}

				case ECondition.NotOne:
				{
					if(Value.X != 1.0f) break;
					if(Value.Y != 1.0f) break;
					if(Value.Z != 1.0f) break;
					return;
				}
			}

			Saver.WriteTabs();
			Saver.WriteWord(Name + " ");
			Saver.WriteColor(Value);
			Saver.WriteLine(",");
		}
	}
}
