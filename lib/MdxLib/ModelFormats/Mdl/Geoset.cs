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
	internal sealed class CGeoset : CObject
	{
		private CGeoset()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			Model.CGeoset Geoset = new Model.CGeoset(Model);
			Load(Loader, Model, Geoset);
			Model.Geosets.Add(Geoset);
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CGeoset Geoset)
		{
			int NrOfVertexPositions = 0;
			int NrOfVertexNormals = 0;
			int NrOfVertexGroups = 0;
			int NrOfVertexTexturePositions = 0;

			float ExtentRadius = 0.0f;
			Primitives.CVector3 ExtentMin = CConstants.DefaultVector3;
			Primitives.CVector3 ExtentMax = CConstants.DefaultVector3;

			System.Collections.Generic.List<Primitives.CVector3> VertexPositionList = new System.Collections.Generic.List<Primitives.CVector3>();
			System.Collections.Generic.List<Primitives.CVector3> VertexNormalList = new System.Collections.Generic.List<Primitives.CVector3>();
			System.Collections.Generic.List<int> VertexGroupList = new System.Collections.Generic.List<int>();
			System.Collections.Generic.List<Primitives.CVector2> VertexTexturePositionList = new System.Collections.Generic.List<Primitives.CVector2>();
			System.Collections.Generic.List<int> IndexList = new System.Collections.Generic.List<int>();

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
					case "minimumextent": { ExtentMin = LoadVector3(Loader); break; }
					case "maximumextent": { ExtentMax = LoadVector3(Loader); break; }
					case "boundsradius": { ExtentRadius = LoadFloat(Loader); break; }
					case "materialid": { Loader.Attacher.AddObject(Model.Materials, Geoset.Material, LoadId(Loader)); break; }
					case "selectiongroup": { Geoset.SelectionGroup = LoadInteger(Loader); break; }
					case "unselectable": { Geoset.Unselectable = LoadBoolean(Loader); break; }

					case "vertices":
					{
						NrOfVertexPositions = Loader.ReadInteger();
						Loader.ExpectToken(Token.EType.CurlyBracketLeft);

						for(int Index = 0; Index < NrOfVertexPositions; Index++)
						{
							VertexPositionList.Add(LoadVector3(Loader));
						}

						Loader.ExpectToken(Token.EType.CurlyBracketRight);
						break;
					}

					case "normals":
					{
						NrOfVertexNormals = Loader.ReadInteger();
						if(NrOfVertexNormals != NrOfVertexPositions) throw new System.Exception("Vertex normal miscount at line " + Loader.Line + " (" + NrOfVertexNormals + " normals, " + NrOfVertexPositions + " positions)!");
						Loader.ExpectToken(Token.EType.CurlyBracketLeft);

						for(int Index = 0; Index < NrOfVertexNormals; Index++)
						{
							VertexNormalList.Add(LoadVector3(Loader));
						}

						Loader.ExpectToken(Token.EType.CurlyBracketRight);
						break;
					}

					case "tvertices":
					{
						NrOfVertexTexturePositions = Loader.ReadInteger();
						if(NrOfVertexTexturePositions != NrOfVertexPositions) throw new System.Exception("Vertex texture position miscount at line " + Loader.Line + " (" + NrOfVertexTexturePositions + " texture positions, " + NrOfVertexPositions + " positions)!");
						Loader.ExpectToken(Token.EType.CurlyBracketLeft);

						for(int Index = 0; Index < NrOfVertexTexturePositions; Index++)
						{
							VertexTexturePositionList.Add(LoadVector2(Loader));
						}

						Loader.ExpectToken(Token.EType.CurlyBracketRight);
						break;
					}

					case "vertexgroup":
					{
						NrOfVertexGroups = NrOfVertexPositions;
						Loader.ExpectToken(Token.EType.CurlyBracketLeft);

						for(int Index = 0; Index < NrOfVertexGroups; Index++)
						{
							VertexGroupList.Add(LoadInteger(Loader));
						}

						Loader.ExpectToken(Token.EType.CurlyBracketRight);
						break;
					}

					case "faces":
					{
						int NrOfIndexGroups = Loader.ReadInteger();
						int NrOfIndexes = Loader.ReadInteger();

						Loader.ExpectToken(Token.EType.CurlyBracketLeft);
						Loader.ExpectWord("triangles");
						Loader.ExpectToken(Token.EType.CurlyBracketLeft);
						Loader.ExpectToken(Token.EType.CurlyBracketLeft);

						for(int Index = 0; Index < NrOfIndexes; Index++)
						{
							IndexList.Add(Loader.ReadInteger());

							if(Loader.PeekToken() == Token.EType.Separator)
							{
								Loader.ExpectToken(Token.EType.Separator);
							}
							else
							{
								Loader.ExpectToken(Token.EType.CurlyBracketRight);
								Loader.ExpectToken(Token.EType.Separator);

								if(Index < (NrOfIndexes - 1))
								{
									Loader.ExpectToken(Token.EType.CurlyBracketLeft);
								}
							}
						}

						Loader.ExpectToken(Token.EType.CurlyBracketRight);
						Loader.ExpectToken(Token.EType.CurlyBracketRight);
						break;
					}

					case "groups":
					{
						int NrOfMatrixGroups = Loader.ReadInteger();
						int NrOfMatrices = Loader.ReadInteger();

						Loader.ExpectToken(Token.EType.CurlyBracketLeft);

						for(int Index = 0; Index < NrOfMatrixGroups; Index++)
						{
							Model.CGeosetGroup Group = new Model.CGeosetGroup(Model);
							Geoset.Groups.Add(Group);

							Loader.ExpectWord("matrices");
							Loader.ExpectToken(Token.EType.CurlyBracketLeft);

							while(true)
							{
								Model.CGeosetGroupNode Node = new Model.CGeosetGroupNode(Model);
								Loader.Attacher.AddNode(Model, Node.Node, Loader.ReadInteger());
								Group.Nodes.Add(Node);

								if(Loader.PeekToken() == Token.EType.Separator)
								{
									Loader.ExpectToken(Token.EType.Separator);
								}
								else
								{
									Loader.ExpectToken(Token.EType.CurlyBracketRight);
									Loader.ExpectToken(Token.EType.Separator);
									break;
								}
							}
						}

						Loader.ExpectToken(Token.EType.CurlyBracketRight);
						break;
					}

					case "anim":
					{
						float SubExtentRadius = 0.0f;
						Primitives.CVector3 SubExtentMin = CConstants.DefaultVector3;
						Primitives.CVector3 SubExtentMax = CConstants.DefaultVector3;

						Loader.ExpectToken(Token.EType.CurlyBracketLeft);

						while(true)
						{
							if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
							{
								Loader.ReadToken();
								break;
							}

							Tag = Loader.ReadWord();

							switch(Tag)
							{
								case "minimumextent": { SubExtentMin = LoadVector3(Loader); break; }
								case "maximumextent": { SubExtentMax = LoadVector3(Loader); break; }
								case "boundsradius": { SubExtentRadius = LoadFloat(Loader); break; }

								default:
								{
									throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
								}
							}
						}

						Model.CGeosetExtent Extent = new Model.CGeosetExtent(Model);
						Extent.Extent = new Primitives.CExtent(SubExtentMin, SubExtentMax, SubExtentRadius);
						Geoset.Extents.Add(Extent);
						break;
					}

					default:
					{
						throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
					}
				}
			}

			Geoset.Extent = new Primitives.CExtent(ExtentMin, ExtentMax, ExtentRadius);

			for(int Index = 0; Index < NrOfVertexPositions; Index++)
			{
				Model.CGeosetVertex Vertex = new Model.CGeosetVertex(Model);
				Vertex.Position = VertexPositionList[Index];
				Vertex.Normal = VertexNormalList[Index];
				Vertex.TexturePosition = VertexTexturePositionList[Index];
				Loader.Attacher.AddObject(Geoset.Groups, Vertex.Group, VertexGroupList[Index]);
				Geoset.Vertices.Add(Vertex);
			}

			if((IndexList.Count % 3) != 0) throw new System.Exception("Bad Geoset at line " + Loader.Line + ", nr of indexes not divisible by 3!");
			int NrOfFaces = IndexList.Count / 3;

			for(int Index = 0; Index < NrOfFaces; Index++)
			{
				Model.CGeosetFace Face = new Model.CGeosetFace(Model);
				Loader.Attacher.AddObject(Geoset.Vertices, Face.Vertex1, IndexList[(Index * 3) + 0]);
				Loader.Attacher.AddObject(Geoset.Vertices, Face.Vertex2, IndexList[(Index * 3) + 1]);
				Loader.Attacher.AddObject(Geoset.Vertices, Face.Vertex3, IndexList[(Index * 3) + 2]);
				Geoset.Faces.Add(Face);
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasGeosets)
			{
				foreach(Model.CGeoset Geoset in Model.Geosets)
				{
					Save(Saver, Model, Geoset);
				}
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CGeoset Geoset)
		{
			Saver.BeginGroup("Geoset");

			if(Geoset.Vertices.Count > 0)
			{
				Saver.BeginGroup("Vertices", Geoset.Vertices.Count);

				foreach(Model.CGeosetVertex Vertex in Geoset.Vertices)
				{
					Saver.WriteTabs();
					Saver.WriteVector3(Vertex.Position);
					Saver.WriteLine(",");
				}

				Saver.EndGroup();
				Saver.BeginGroup("Normals", Geoset.Vertices.Count);

				foreach(Model.CGeosetVertex Vertex in Geoset.Vertices)
				{
					Saver.WriteTabs();
					Saver.WriteVector3(Vertex.Normal);
					Saver.WriteLine(",");
				}

				Saver.EndGroup();
				Saver.BeginGroup("TVertices", Geoset.Vertices.Count);

				foreach(Model.CGeosetVertex Vertex in Geoset.Vertices)
				{
					Saver.WriteTabs();
					Saver.WriteVector2(Vertex.TexturePosition);
					Saver.WriteLine(",");
				}

				Saver.EndGroup();
				Saver.BeginGroup("VertexGroup");

				foreach(Model.CGeosetVertex Vertex in Geoset.Vertices)
				{
					Saver.WriteTabs();
					Saver.WriteInteger(Vertex.Group.ObjectId);
					Saver.WriteLine(",");
				}

				Saver.EndGroup();
			}

			if(Geoset.Faces.Count > 0)
			{
				bool FirstFace = true;

				Saver.BeginGroup("Faces", 1, (Geoset.Faces.Count * 3));
				Saver.BeginGroup("Triangles");
				Saver.WriteTabs();
				Saver.WriteWord("{ ");

				foreach(Model.CGeosetFace Face in Geoset.Faces)
				{
					if(!FirstFace) Saver.WriteWord(", ");

					Saver.WriteInteger(Face.Vertex1.ObjectId);
					Saver.WriteWord(", ");
					Saver.WriteInteger(Face.Vertex2.ObjectId);
					Saver.WriteWord(", ");
					Saver.WriteInteger(Face.Vertex3.ObjectId);

					FirstFace = false;
				}

				Saver.WriteLine(" },");
				Saver.EndGroup();
				Saver.EndGroup();
			}

			if(Geoset.Groups.Count > 0)
			{
				int TotalNrOfNodes = 0;

				foreach(Model.CGeosetGroup Group in Geoset.Groups)
				{
					TotalNrOfNodes += Group.Nodes.Count;
				}

				Saver.BeginGroup("Groups", Geoset.Groups.Count, TotalNrOfNodes);

				foreach(Model.CGeosetGroup Group in Geoset.Groups)
				{
					bool FirstNode = true;

					Saver.WriteTabs();
					Saver.WriteWord("Matrices { ");

					foreach(Model.CGeosetGroupNode Node in Group.Nodes)
					{
						if(!FirstNode) Saver.WriteWord(", ");

						Saver.WriteInteger(Node.Node.NodeId);

						FirstNode = false;
					}

					Saver.WriteLine(" },");
				}

				Saver.EndGroup();
			}

			SaveVector3(Saver, "MinimumExtent", Geoset.Extent.Min, ECondition.NotZero);
			SaveVector3(Saver, "MaximumExtent", Geoset.Extent.Max, ECondition.NotZero);
			SaveFloat(Saver, "BoundsRadius", Geoset.Extent.Radius, ECondition.NotZero);

			foreach(Model.CGeosetExtent Extent in Geoset.Extents)
			{
				Saver.BeginGroup("Anim");
				SaveVector3(Saver, "MinimumExtent", Extent.Extent.Min, ECondition.NotZero);
				SaveVector3(Saver, "MaximumExtent", Extent.Extent.Max, ECondition.NotZero);
				SaveFloat(Saver, "BoundsRadius", Extent.Extent.Radius, ECondition.NotZero);
				Saver.EndGroup();
			}

			SaveId(Saver, "MaterialID", Geoset.Material.ObjectId, ECondition.NotInvalidId);
			SaveInteger(Saver, "SelectionGroup", Geoset.SelectionGroup);
			SaveBoolean(Saver, "Unselectable", Geoset.Unselectable);

			Saver.EndGroup();
		}

		public static CGeoset Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CGeoset Instance = new CGeoset();
		}
	}
}
