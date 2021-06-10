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
	internal sealed class CLoader
	{
		public CLoader(string Name, System.IO.Stream Stream)
		{
			_Name = Name;
			_Attacher = new Attacher.CAttacherContainer();

			Reader = new System.IO.BinaryReader(Stream, CConstants.SimpleTextEncoding);
			LocationStack = new System.Collections.Generic.LinkedList<int>();
		}

		public bool Eof()
		{
			return (Reader.BaseStream.Position >= Reader.BaseStream.Length);
		}

		public byte[] Read(int Size)
		{
			return Reader.ReadBytes(Size);
		}

		public byte ReadByte()
		{
			return Reader.ReadByte();
		}

		public int ReadInt8()
		{
			return (int)Reader.ReadByte();
		}

		public int ReadInt16()
		{
			return (int)Reader.ReadInt16();
		}

		public int ReadInt32()
		{
			return Reader.ReadInt32();
		}

		public float ReadFloat()
		{
			return Reader.ReadSingle();
		}

		public double ReadDouble()
		{
			return Reader.ReadDouble();
		}

		public string ReadString(int Length)
		{
			int BufferLength = Length;
			char[] Buffer = Reader.ReadChars(Length);

			while(BufferLength > 0)
			{
				if(Buffer[BufferLength - 1] != '\0') break;
				BufferLength--;
			}

			return new string(Buffer, 0, BufferLength);
		}

		public string ReadTag()
		{
			return ReadString(CConstants.SizeTag);
		}

		public Primitives.CVector2 ReadVector2()
		{
			float X = ReadFloat();
			float Y = ReadFloat();

			return new Primitives.CVector2(X, Y);
		}

		public Primitives.CVector3 ReadVector3()
		{
			float X = ReadFloat();
			float Y = ReadFloat();
			float Z = ReadFloat();

			return new Primitives.CVector3(X, Y, Z);
		}

		public Primitives.CVector4 ReadVector4()
		{
			float X = ReadFloat();
			float Y = ReadFloat();
			float Z = ReadFloat();
			float W = ReadFloat();

			return new Primitives.CVector4(X, Y, Z, W);
		}

		public Primitives.CExtent ReadExtent()
		{
			float Radius = ReadFloat();
			Primitives.CVector3 Min = ReadVector3();
			Primitives.CVector3 Max = ReadVector3();

			return new Primitives.CExtent(Min, Max, Radius);
		}

		public void ExpectTag(string ExpectedTag)
		{
			string Tag = ReadTag();
			if(Tag != ExpectedTag)
			{
				throw new System.Exception("Error at location " + Location + ", expected \"" + ExpectedTag + "\", got \"" + Tag + "\"!");
			}
		}

		public void Skip(int NrOfBytes)
		{
			Reader.ReadBytes(NrOfBytes);
		}

		public void PushLocation()
		{
			LocationStack.AddLast((int)Reader.BaseStream.Position);
		}

		public int PopLocation()
		{
			int Location = LocationStack.Last.Value;
			LocationStack.RemoveLast();
			return (int)Reader.BaseStream.Position - Location;
		}

		public long Location
		{
			get
			{
				return Reader.BaseStream.Position;
			}
		}

		public string Name
		{
			get
			{
				return _Name;
			}
		}

		public Attacher.CAttacherContainer Attacher
		{
			get
			{
				return _Attacher;
			}
		}

		private string _Name = "";
		private Attacher.CAttacherContainer _Attacher = null;

		private System.IO.BinaryReader Reader = null;
		private System.Collections.Generic.LinkedList<int> LocationStack = null;
	}
}
