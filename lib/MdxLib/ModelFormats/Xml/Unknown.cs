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
namespace MdxLib.ModelFormats.Xml
{
	internal abstract class CUnknown
	{
		public CUnknown()
		{
			//Empty
		}

		public bool ReadBoolean(System.Xml.XmlNode Node, string Name, bool DefaultValue)
		{
			System.Xml.XmlElement Element = Node.SelectSingleNode(Name) as System.Xml.XmlElement;
			if(Element == null) return DefaultValue;

			return Bool(Element.GetAttribute("bool"), DefaultValue);
		}

		public int ReadInteger(System.Xml.XmlNode Node, string Name, int DefaultValue)
		{
			System.Xml.XmlElement Element = Node.SelectSingleNode(Name) as System.Xml.XmlElement;
			if(Element == null) return DefaultValue;

			return Int(Element.GetAttribute("int"), DefaultValue);
		}

		public float ReadFloat(System.Xml.XmlNode Node, string Name, float DefaultValue)
		{
			System.Xml.XmlElement Element = Node.SelectSingleNode(Name) as System.Xml.XmlElement;
			if(Element == null) return DefaultValue;

			return Float(Element.GetAttribute("float"), DefaultValue);
		}

		public string ReadString(System.Xml.XmlNode Node, string Name, string DefaultValue)
		{
			System.Xml.XmlElement Element = Node.SelectSingleNode(Name) as System.Xml.XmlElement;
			if(Element == null) return DefaultValue;

			string Attribute = Element.GetAttribute("string");
			if(Attribute == null) return DefaultValue;

			return Attribute;
		}

		public Primitives.CVector2 ReadVector2(System.Xml.XmlNode Node, string Name, Primitives.CVector2 DefaultValue)
		{
			System.Xml.XmlElement Element = Node.SelectSingleNode(Name) as System.Xml.XmlElement;
			if(Element == null) return DefaultValue;

			float X = Float(Element.GetAttribute("x"), DefaultValue.X);
			float Y = Float(Element.GetAttribute("y"), DefaultValue.Y);

			return new Primitives.CVector2(X, Y);
		}

		public Primitives.CVector3 ReadVector3(System.Xml.XmlNode Node, string Name, Primitives.CVector3 DefaultValue)
		{
			System.Xml.XmlElement Element = Node.SelectSingleNode(Name) as System.Xml.XmlElement;
			if(Element == null) return DefaultValue;

			float X = Float(Element.GetAttribute("x"), DefaultValue.X);
			float Y = Float(Element.GetAttribute("y"), DefaultValue.Y);
			float Z = Float(Element.GetAttribute("z"), DefaultValue.Z);

			return new Primitives.CVector3(X, Y, Z);
		}

		public Primitives.CVector4 ReadVector4(System.Xml.XmlNode Node, string Name, Primitives.CVector4 DefaultValue)
		{
			System.Xml.XmlElement Element = Node.SelectSingleNode(Name) as System.Xml.XmlElement;
			if(Element == null) return DefaultValue;

			float X = Float(Element.GetAttribute("x"), DefaultValue.X);
			float Y = Float(Element.GetAttribute("y"), DefaultValue.Y);
			float Z = Float(Element.GetAttribute("z"), DefaultValue.Z);
			float W = Float(Element.GetAttribute("w"), DefaultValue.W);

			return new Primitives.CVector4(X, Y, Z, W);
		}

		public Primitives.CExtent ReadExtent(System.Xml.XmlNode Node, string Name, Primitives.CExtent DefaultValue)
		{
			System.Xml.XmlNode ChildNode = Node.SelectSingleNode(Name);
			if(ChildNode == null) return DefaultValue;

			Primitives.CVector3 Min = ReadVector3(ChildNode, "min", DefaultValue.Min);
			Primitives.CVector3 Max = ReadVector3(ChildNode, "max", DefaultValue.Max);
			float Radius = ReadFloat(ChildNode, "radius", DefaultValue.Radius);

			return new Primitives.CExtent(Min, Max, Radius);
		}

		public Primitives.CSegment ReadSegment(System.Xml.XmlNode Node, string Name, Primitives.CSegment DefaultValue)
		{
			System.Xml.XmlNode ChildNode = Node.SelectSingleNode(Name);
			if(ChildNode == null) return DefaultValue;

			Primitives.CVector3 Color = ReadVector3(ChildNode, "color", DefaultValue.Color);
			float Alpha = ReadFloat(ChildNode, "alpha", DefaultValue.Alpha);
			float Scaling = ReadFloat(ChildNode, "scaling", DefaultValue.Scaling);

			return new Primitives.CSegment(Color, Alpha, Scaling);
		}

		public Primitives.CInterval ReadInterval(System.Xml.XmlNode Node, string Name, Primitives.CInterval DefaultValue)
		{
			System.Xml.XmlNode ChildNode = Node.SelectSingleNode(Name);
			if(ChildNode == null) return DefaultValue;

			int Start = ReadInteger(ChildNode, "start", DefaultValue.Start);
			int End = ReadInteger(ChildNode, "end", DefaultValue.End);
			int Repeat = ReadInteger(ChildNode, "repeat", DefaultValue.Repeat);

			return new Primitives.CInterval(Start, End, Repeat);
		}

		public void WriteBoolean(System.Xml.XmlNode Node, string Name, bool Value)
		{
			System.Xml.XmlElement Element = AppendElement(Node, Name);

			AppendAttribute(Element, "bool", Value ? "1" : "0");
		}

		public void WriteInteger(System.Xml.XmlNode Node, string Name, int Value)
		{
			System.Xml.XmlElement Element = AppendElement(Node, Name);

			AppendAttribute(Element, "int", Value.ToString());
		}

		public void WriteFloat(System.Xml.XmlNode Node, string Name, float Value)
		{
			System.Xml.XmlElement Element = AppendElement(Node, Name);

			AppendAttribute(Element, "float", Value.ToString(CConstants.NumberFormat));
		}

		public void WriteString(System.Xml.XmlNode Node, string Name, string Value)
		{
			System.Xml.XmlElement Element = AppendElement(Node, Name);

			AppendAttribute(Element, "string", Value);
		}

		public void WriteVector2(System.Xml.XmlNode Node, string Name, Primitives.CVector2 Value)
		{
			System.Xml.XmlElement Element = AppendElement(Node, Name);

			AppendAttribute(Element, "x", Value.X.ToString(CConstants.NumberFormat));
			AppendAttribute(Element, "y", Value.Y.ToString(CConstants.NumberFormat));
		}

		public void WriteVector3(System.Xml.XmlNode Node, string Name, Primitives.CVector3 Value)
		{
			System.Xml.XmlElement Element = AppendElement(Node, Name);

			AppendAttribute(Element, "x", Value.X.ToString(CConstants.NumberFormat));
			AppendAttribute(Element, "y", Value.Y.ToString(CConstants.NumberFormat));
			AppendAttribute(Element, "z", Value.Z.ToString(CConstants.NumberFormat));
		}

		public void WriteVector4(System.Xml.XmlNode Node, string Name, Primitives.CVector4 Value)
		{
			System.Xml.XmlElement Element = AppendElement(Node, Name);

			AppendAttribute(Element, "x", Value.X.ToString(CConstants.NumberFormat));
			AppendAttribute(Element, "y", Value.Y.ToString(CConstants.NumberFormat));
			AppendAttribute(Element, "z", Value.Z.ToString(CConstants.NumberFormat));
			AppendAttribute(Element, "w", Value.W.ToString(CConstants.NumberFormat));
		}

		public void WriteExtent(System.Xml.XmlNode Node, string Name, Primitives.CExtent Value)
		{
			System.Xml.XmlElement Element = AppendElement(Node, Name);

			WriteVector3(Element, "min", Value.Min);
			WriteVector3(Element, "max", Value.Max);
			WriteFloat(Element, "radius", Value.Radius);
		}

		public void WriteSegment(System.Xml.XmlNode Node, string Name, Primitives.CSegment Value)
		{
			System.Xml.XmlElement Element = AppendElement(Node, Name);

			WriteVector3(Element, "color", Value.Color);
			WriteFloat(Element, "alpha", Value.Alpha);
			WriteFloat(Element, "scaling", Value.Scaling);
		}

		public void WriteInterval(System.Xml.XmlNode Node, string Name, Primitives.CInterval Value)
		{
			System.Xml.XmlElement Element = AppendElement(Node, Name);

			WriteInteger(Element, "start", Value.Start);
			WriteInteger(Element, "end", Value.End);
			WriteInteger(Element, "repeat", Value.Repeat);
		}

		public System.Xml.XmlElement AppendElement(System.Xml.XmlNode Node, string Name)
		{
			System.Xml.XmlElement Element = Node.OwnerDocument.CreateElement(Name);
			Node.AppendChild(Element);
			return Element;
		}

		public System.Xml.XmlAttribute AppendAttribute(System.Xml.XmlNode Node, string Name, string Value)
		{
			System.Xml.XmlAttribute Attribute = Node.OwnerDocument.CreateAttribute(Name);
			Attribute.Value = Value;
			Node.Attributes.Append(Attribute);
			return Attribute;
		}

		public bool Bool(string String, bool DefaultValue)
		{
			try
			{
				if(String == null) return DefaultValue;
				return (int.Parse(String) != 0);
			}
			catch(System.Exception)
			{
				return DefaultValue;
			}
		}

		public int Int(string String, int DefaultValue)
		{
			try
			{
				if(String == null) return DefaultValue;
				return int.Parse(String);
			}
			catch(System.Exception)
			{
				return DefaultValue;
			}
		}

		public float Float(string String, float DefaultValue)
		{
			try
			{
				if(String == null) return DefaultValue;
				return float.Parse(String, CConstants.NumberFormat);
			}
			catch(System.Exception)
			{
				return DefaultValue;
			}
		}
	}
}
