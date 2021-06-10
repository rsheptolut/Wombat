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
	internal sealed class CSaver
	{
		public CSaver(string Name, System.IO.Stream Stream)
		{
			_Name = Name;

			Writer = new System.IO.BinaryWriter(Stream, CConstants.SimpleTextEncoding);
			LocationStack = new System.Collections.Generic.LinkedList<int>();
		}

		public void Write(byte[] Data)
		{
			Writer.Write(Data);
		}

		public void WriteByte(byte Value)
		{
			Writer.Write(Value);
		}

		public void WriteInt8(int Value)
		{
			Writer.Write((byte)Value);
		}

		public void WriteInt16(int Value)
		{
			Writer.Write((short)Value);
		}

		public void WriteInt32(int Value)
		{
			Writer.Write(Value);
		}

		public void WriteFloat(float Value)
		{
			Writer.Write(Value);
		}

		public void WriteDouble(double Value)
		{
			Writer.Write(Value);
		}

		public void WriteString(string Value, int Length)
		{
			int ExtraSpace = Length - Value.Length;
			string TempString = (Value.Length > Length) ? Value.Substring(0, Length) : Value;

			Writer.Write(TempString.ToCharArray());

			for(int i = 0; i < ExtraSpace; i++)
			{
				Writer.Write('\0');
			}
		}

		public void WriteTag(string Value)
		{
			WriteString(Value, CConstants.SizeTag);
		}

		public void WriteVector2(Primitives.CVector2 Value)
		{
			WriteFloat(Value.X);
			WriteFloat(Value.Y);
		}

		public void WriteVector3(Primitives.CVector3 Value)
		{
			WriteFloat(Value.X);
			WriteFloat(Value.Y);
			WriteFloat(Value.Z);
		}

		public void WriteVector4(Primitives.CVector4 Value)
		{
			WriteFloat(Value.X);
			WriteFloat(Value.Y);
			WriteFloat(Value.Z);
			WriteFloat(Value.W);
		}

		public void WriteExtent(Primitives.CExtent Value)
		{
			WriteFloat(Value.Radius);
			WriteVector3(Value.Min);
			WriteVector3(Value.Max);
		}

		public void PushLocation()
		{
			LocationStack.AddLast((int)Writer.BaseStream.Position);
			WriteInt32(0);
		}

		public void PopLocation(int AdditionalSize)
		{
			int CurrentLocation = (int)Writer.BaseStream.Position;
			int Location = LocationStack.Last.Value;
			LocationStack.RemoveLast();

			Writer.BaseStream.Position = Location;
			WriteInt32(CurrentLocation - Location + AdditionalSize);
			Writer.BaseStream.Position = CurrentLocation;
		}

		public void PopInclusiveLocation()
		{
			PopLocation(0);
		}

		public void PopExclusiveLocation()
		{
			PopLocation(-4);
		}

		public string Name
		{
			get
			{
				return _Name;
			}
		}

		private string _Name = "";

		private System.IO.BinaryWriter Writer = null;
		private System.Collections.Generic.LinkedList<int> LocationStack = null;
	}
}
