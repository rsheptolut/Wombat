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
	internal sealed class CCollisionShape : CNode
	{
		private CCollisionShape()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CCollisionShape CollisionShape)
		{
			LoadNode(Loader, Node, Model, CollisionShape);

			CollisionShape.Type = StringToType(ReadString(Node, "type", TypeToString(CollisionShape.Type)));
			CollisionShape.Radius = ReadFloat(Node, "radius", CollisionShape.Radius);
			CollisionShape.Vertex1 = ReadVector3(Node, "vertex_1", CollisionShape.Vertex1);
			CollisionShape.Vertex2 = ReadVector3(Node, "vertex_2", CollisionShape.Vertex2);
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CCollisionShape CollisionShape)
		{
			SaveNode(Saver, Node, Model, CollisionShape);

			WriteString(Node, "type", TypeToString(CollisionShape.Type));
			WriteFloat(Node, "radius", CollisionShape.Radius);
			WriteVector3(Node, "vertex_1", CollisionShape.Vertex1);
			WriteVector3(Node, "vertex_2", CollisionShape.Vertex2);
		}

		private string TypeToString(Model.ECollisionShapeType Type)
		{
			switch(Type)
			{
				case Model.ECollisionShapeType.Box: return "box";
				case Model.ECollisionShapeType.Sphere: return "sphere";
			}

			return "";
		}

		private Model.ECollisionShapeType StringToType(string String)
		{
			switch(String)
			{
				case "box": return Model.ECollisionShapeType.Box;
				case "sphere": return Model.ECollisionShapeType.Sphere;
			}

			return Model.ECollisionShapeType.Box;
		}

		public static CCollisionShape Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CCollisionShape Instance = new CCollisionShape();
		}
	}
}
