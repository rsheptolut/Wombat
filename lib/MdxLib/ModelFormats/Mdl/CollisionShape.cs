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
	internal sealed class CCollisionShape : CNode
	{
		private CCollisionShape()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			Model.CCollisionShape CollisionShape = new Model.CCollisionShape(Model);
			Load(Loader, Model, CollisionShape);
			Model.CollisionShapes.Add(CollisionShape);
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CCollisionShape CollisionShape)
		{
			CollisionShape.Name = Loader.ReadString();
			Loader.ExpectToken(Token.EType.CurlyBracketLeft);

			while(true)
			{
				if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
				{
					Loader.ReadToken();
					break;
				}

				string Tag = Loader.ReadWord();

				if(!LoadNode(Loader, Model, CollisionShape, Tag))
				{
					switch(Tag)
					{
						case "static":
						{
							Tag = Loader.ReadWord();

							if(!LoadStaticNode(Loader, Model, CollisionShape, Tag))
							{
								switch(Tag)
								{
									default:
									{
										throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
									}
								}
							}

							break;
						}

						case "box": { CollisionShape.Type = MdxLib.Model.ECollisionShapeType.Box; LoadBoolean(Loader); break; }
						case "sphere": { CollisionShape.Type = MdxLib.Model.ECollisionShapeType.Sphere; LoadBoolean(Loader); break; }
						case "boundsradius": { CollisionShape.Radius = LoadFloat(Loader); break; }

						case "vertices":
						{
							int NrOfVertices = Loader.ReadInteger();
							Loader.ExpectToken(Token.EType.CurlyBracketLeft);

							switch(NrOfVertices)
							{
								case 1:
								{
									CollisionShape.Vertex1 = LoadVector3(Loader);
									break;
								}

								case 2:
								{
									CollisionShape.Vertex1 = LoadVector3(Loader);
									CollisionShape.Vertex2 = LoadVector3(Loader);
									break;
								}

								default:
								{
									throw new System.Exception("Bad vertex count at line " + Loader.Line + ", got " + NrOfVertices + " vertices!");
								}
							}

							Loader.ExpectToken(Token.EType.CurlyBracketRight);
							break;
						}

						default:
						{
							throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
						}
					}
				}
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasCollisionShapes)
			{
				foreach(Model.CCollisionShape CollisionShape in Model.CollisionShapes)
				{
					Save(Saver, Model, CollisionShape);
				}
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CCollisionShape CollisionShape)
		{
			Saver.BeginGroup("CollisionShape", CollisionShape.Name);

			SaveNode(Saver, Model, CollisionShape);
			SaveBoolean(Saver, TypeToString(CollisionShape.Type), true);

			switch(CollisionShape.Type)
			{
				case MdxLib.Model.ECollisionShapeType.Box:
				{
					Saver.BeginGroup("Vertices", 2);
					Saver.WriteTabs();
					Saver.WriteVector3(CollisionShape.Vertex1);
					Saver.WriteLine(",");
					Saver.WriteTabs();
					Saver.WriteVector3(CollisionShape.Vertex2);
					Saver.WriteLine(",");
					Saver.EndGroup();
					break;
				}

				case MdxLib.Model.ECollisionShapeType.Sphere:
				{
					Saver.BeginGroup("Vertices", 1);
					Saver.WriteTabs();
					Saver.WriteVector3(CollisionShape.Vertex1);
					Saver.WriteLine(",");
					Saver.EndGroup();
					SaveFloat(Saver, "BoundsRadius", CollisionShape.Radius, ECondition.NotZero);
					break;
				}
			}

			Saver.EndGroup();
		}

		private string TypeToString(Model.ECollisionShapeType Type)
		{
			switch(Type)
			{
				case Model.ECollisionShapeType.Box: return "Box";
				case Model.ECollisionShapeType.Sphere: return "Sphere";
			}

			return "";
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
