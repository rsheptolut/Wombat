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
	internal sealed class CCollisionShape : CNode
	{
		private CCollisionShape()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CCollisionShape CollisionShape = new Model.CCollisionShape(Model);
				Load(Loader, Model, CollisionShape);
				Model.CollisionShapes.Add(CollisionShape);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many CollisionShape bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CCollisionShape CollisionShape)
		{
			LoadNode(Loader, Model, CollisionShape);
			int Type = Loader.ReadInt32();

			switch(Type)
			{
				case 0: { CollisionShape.Type = MdxLib.Model.ECollisionShapeType.Box; break; }
				case 2: { CollisionShape.Type = MdxLib.Model.ECollisionShapeType.Sphere; break; }
			}

			switch(CollisionShape.Type)
			{
				case MdxLib.Model.ECollisionShapeType.Box:
				{
					CollisionShape.Vertex1 = Loader.ReadVector3();
					CollisionShape.Vertex2 = Loader.ReadVector3();
					break;
				}

				case MdxLib.Model.ECollisionShapeType.Sphere:
				{
					CollisionShape.Vertex1 = Loader.ReadVector3();
					CollisionShape.Radius = Loader.ReadFloat();
					break;
				}
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasCollisionShapes)
			{
				Saver.WriteTag("CLID");
				Saver.PushLocation();

				foreach(Model.CCollisionShape CollisionShape in Model.CollisionShapes)
				{
					Save(Saver, Model, CollisionShape);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CCollisionShape CollisionShape)
		{
			SaveNode(Saver, Model, CollisionShape, 8192);

			switch(CollisionShape.Type)
			{
				case MdxLib.Model.ECollisionShapeType.Box:
				{
					Saver.WriteInt32(0);
					Saver.WriteVector3(CollisionShape.Vertex1);
					Saver.WriteVector3(CollisionShape.Vertex2);
					break;
				}

				case MdxLib.Model.ECollisionShapeType.Sphere:
				{
					Saver.WriteInt32(2);
					Saver.WriteVector3(CollisionShape.Vertex1);
					Saver.WriteFloat(CollisionShape.Radius);
					break;
				}
			}
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
