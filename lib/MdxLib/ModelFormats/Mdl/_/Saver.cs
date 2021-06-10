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
	internal sealed class CSaver
	{
		public CSaver(string Name, System.IO.Stream Stream)
		{
			_Name = Name;
			OutputStream = Stream;
			OutputBuilder = new System.Text.StringBuilder();
		}

		public void WriteToStream()
		{
			using(System.IO.StreamWriter Writer = new System.IO.StreamWriter(OutputStream, CConstants.TextEncoding))
			{
				Writer.Write(OutputBuilder.ToString());
			}
		}

		public void WriteTabs()
		{
			for(int i = 0; i < TabDepth; i++)
			{
				OutputBuilder.Append("\t");
			}
		}

		public void WriteTabs(int NrOfTabs)
		{
			for(int i = 0; i < NrOfTabs; i++)
			{
				OutputBuilder.Append("\t");
			}
		}

		public void WriteInteger(int Value)
		{
			OutputBuilder.Append(Value);
		}

		public void WriteFloat(float Value)
		{
			OutputBuilder.Append(Value.ToString(CConstants.NumberFormat));
		}

		public void WriteCharacter(char Value)
		{
			OutputBuilder.Append(Value);
		}

		public void WriteWord(string Value)
		{
			OutputBuilder.Append(Value);
		}

		public void WriteLine()
		{
			OutputBuilder.AppendLine();
		}

		public void WriteLine(string Value)
		{
			OutputBuilder.AppendLine(Value);
		}

		public void WriteString(string Value)
		{
			OutputBuilder.Append("\"" + Value.Replace("\"", "\\\"") + "\"");
		}

		public void WriteVector2(Primitives.CVector2 Value)
		{
			WriteWord("{ ");
			WriteFloat(Value.X);
			WriteWord(", ");
			WriteFloat(Value.Y);
			WriteWord(" }");
		}

		public void WriteVector3(Primitives.CVector3 Value)
		{
			WriteWord("{ ");
			WriteFloat(Value.X);
			WriteWord(", ");
			WriteFloat(Value.Y);
			WriteWord(", ");
			WriteFloat(Value.Z);
			WriteWord(" }");
		}

		public void WriteVector4(Primitives.CVector4 Value)
		{
			WriteWord("{ ");
			WriteFloat(Value.X);
			WriteWord(", ");
			WriteFloat(Value.Y);
			WriteWord(", ");
			WriteFloat(Value.Z);
			WriteWord(", ");
			WriteFloat(Value.W);
			WriteWord(" }");
		}

		public void WriteColor(Primitives.CVector3 Value)
		{
			WriteWord("{ ");
			WriteFloat(Value.Z);
			WriteWord(", ");
			WriteFloat(Value.Y);
			WriteWord(", ");
			WriteFloat(Value.X);
			WriteWord(" }");
		}

		public void BeginGroup(string Group)
		{
			WriteTabs();
			OutputBuilder.AppendLine(Group + " {");
			TabDepth++;
		}

		public void BeginGroup(string Group, string Name)
		{
			WriteTabs();
			OutputBuilder.AppendLine(Group + " \"" + Name + "\" {");
			TabDepth++;
		}

		public void BeginGroup(string Group, int Size)
		{
			WriteTabs();
			OutputBuilder.AppendLine(Group + " " + Size + " {");
			TabDepth++;
		}

		public void BeginGroup(string Group, int Size1, int Size2)
		{
			WriteTabs();
			OutputBuilder.AppendLine(Group + " " + Size1 + " " + Size2 + " {");
			TabDepth++;
		}

		public void EndGroup()
		{
			TabDepth--;
			WriteTabs();
			OutputBuilder.AppendLine("}");
		}

		public void EndGroup(string ExtraString)
		{
			TabDepth--;
			WriteTabs();
			OutputBuilder.AppendLine("}" + ExtraString);
		}

		public string Name
		{
			get
			{
				return _Name;
			}
		}

		private string _Name = "";

		private int TabDepth = 0;
		private System.IO.Stream OutputStream = null;
		private System.Text.StringBuilder OutputBuilder = null;
	}
}
