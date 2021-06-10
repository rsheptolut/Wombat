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
	internal sealed class CGeoset : CObject
	{
		private CGeoset()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CGeoset Geoset = new Model.CGeoset(Model);
				Load(Loader, Model, Geoset);
				Model.Geosets.Add(Geoset);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Geoset bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CGeoset Geoset)
		{
			System.Collections.Generic.List<Primitives.CVector3> VertexPositionList = new System.Collections.Generic.List<Primitives.CVector3>();
			System.Collections.Generic.List<Primitives.CVector3> VertexNormalList = new System.Collections.Generic.List<Primitives.CVector3>();
			System.Collections.Generic.List<int> VertexGroupList = new System.Collections.Generic.List<int>();
			System.Collections.Generic.List<Primitives.CVector2> VertexTexturePositionList = new System.Collections.Generic.List<Primitives.CVector2>();
			System.Collections.Generic.List<int> GroupSizeList = new System.Collections.Generic.List<int>();

			int Size = Loader.ReadInt32();

			Loader.ExpectTag("VRTX");
			int NrOfVertexPositions = Loader.ReadInt32();

			for(int Index = 0; Index < NrOfVertexPositions; Index++)
			{
				VertexPositionList.Add(Loader.ReadVector3());
			}

			Loader.ExpectTag("NRMS");
			int NrOfVertexNormals = Loader.ReadInt32();
			if(NrOfVertexNormals != NrOfVertexPositions) throw new System.Exception("Error at location " + Loader.Location + ", vertex normal miscount (" + NrOfVertexNormals + " normals, " + NrOfVertexPositions + " positions)!");

			for(int Index = 0; Index < NrOfVertexNormals; Index++)
			{
				VertexNormalList.Add(Loader.ReadVector3());
			}

			Loader.ExpectTag("PTYP");
			int NrOfFaceTypeGroups = Loader.ReadInt32();

			for(int Index = 0; Index < NrOfFaceTypeGroups; Index++)
			{
				int FaceType = Loader.ReadInt32();
				if(FaceType != 4) throw new System.Exception("Error at location " + Loader.Location + ", unsupported Geoset face type (type " + FaceType + ")!");
			}

			Loader.ExpectTag("PCNT");
			int NrOfFaceGroups = Loader.ReadInt32();

			for(int Index = 0; Index < NrOfFaceGroups; Index++)
			{
				int FaceGroupSize = Loader.ReadInt32();
			}

			Loader.ExpectTag("PVTX");
			int TotalNrOfIndexes = Loader.ReadInt32();
			if((TotalNrOfIndexes % 3) != 0) throw new System.Exception("Error at location " + Loader.Location + ", bad Geoset, nr of indexes not divisible by 3!");
			int TotalNrOfFaces = TotalNrOfIndexes / 3;

			for(int Index = 0; Index < TotalNrOfFaces; Index++)
			{
				Model.CGeosetFace Face = new Model.CGeosetFace(Model);
				Loader.Attacher.AddObject(Geoset.Vertices, Face.Vertex1, Loader.ReadInt16());
				Loader.Attacher.AddObject(Geoset.Vertices, Face.Vertex2, Loader.ReadInt16());
				Loader.Attacher.AddObject(Geoset.Vertices, Face.Vertex3, Loader.ReadInt16());
				Geoset.Faces.Add(Face);
			}

			Loader.ExpectTag("GNDX");
			int NrOfVertexGroups = Loader.ReadInt32();
			if(NrOfVertexGroups != NrOfVertexPositions) throw new System.Exception("Error at location " + Loader.Location + ", vertex group miscount (" + NrOfVertexGroups + " groups, " + NrOfVertexPositions + " positions)!");

			for(int Index = 0; Index < NrOfVertexGroups; Index++)
			{
				VertexGroupList.Add(Loader.ReadInt8());
			}

			Loader.ExpectTag("MTGC");
			int NrOfMatrixGroups = Loader.ReadInt32();

			for(int Index = 0; Index < NrOfMatrixGroups; Index++)
			{
				GroupSizeList.Add(Loader.ReadInt32());
				Geoset.Groups.Add(new Model.CGeosetGroup(Model));
			}

			Loader.ExpectTag("MATS");
			int NrOfMatrixIndexes = Loader.ReadInt32();

			int CurrentIndex = -1;
			int CurrentGroupSize = 0;
			Model.CGeosetGroup CurrentGroup = null;

			for(int Index = 0; Index < NrOfMatrixIndexes; Index++)
			{
				if(CurrentGroupSize <= 0)
				{
					CurrentIndex++;
					CurrentGroupSize = GroupSizeList[CurrentIndex];
					CurrentGroup = Geoset.Groups[CurrentIndex];
				}

				Model.CGeosetGroupNode Node = new Model.CGeosetGroupNode(Model);
				Loader.Attacher.AddNode(Model, Node.Node, Loader.ReadInt32());
				CurrentGroup.Nodes.Add(Node);

				CurrentGroupSize--;
			}

			Loader.Attacher.AddObject(Model.Materials, Geoset.Material, Loader.ReadInt32());
			int Flags = Loader.ReadInt32();
			Geoset.SelectionGroup = Loader.ReadInt32();
			Geoset.Extent = Loader.ReadExtent();
			int NrOfExtents = Loader.ReadInt32();

			for(int Index = 0; Index < NrOfExtents; Index++)
			{
				Model.CGeosetExtent Extent = new Model.CGeosetExtent(Model);
				Extent.Extent = Loader.ReadExtent();
				Geoset.Extents.Add(Extent);
			}

			Loader.ExpectTag("UVAS");
			int NrOfTextureVertexGroups = Loader.ReadInt32();

			Loader.ExpectTag("UVBS");
			int NrOfVertexTexturePositions = Loader.ReadInt32();
			if(NrOfVertexTexturePositions != NrOfVertexPositions) throw new System.Exception("Error at location " + Loader.Location + ", vertex texture position miscount (" + NrOfVertexTexturePositions + " texture positions, " + NrOfVertexPositions + " positions)!");

			for(int Index = 0; Index < NrOfVertexTexturePositions; Index++)
			{
				VertexTexturePositionList.Add(Loader.ReadVector2());
			}

			Geoset.Unselectable = ((Flags & 4) != 0);

			for(int Index = 0; Index < NrOfVertexPositions; Index++)
			{
				Model.CGeosetVertex Vertex = new Model.CGeosetVertex(Model);
				Vertex.Position = VertexPositionList[Index];
				Vertex.Normal = VertexNormalList[Index];
				Vertex.TexturePosition = VertexTexturePositionList[Index];
				Loader.Attacher.AddObject(Geoset.Groups, Vertex.Group, VertexGroupList[Index]);
				Geoset.Vertices.Add(Vertex);
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasGeosets)
			{
				Saver.WriteTag("GEOS");
				Saver.PushLocation();

				foreach(Model.CGeoset Geoset in Model.Geosets)
				{
					Save(Saver, Model, Geoset);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CGeoset Geoset)
		{
			int Flags = 0;
			int TotalGroupSize = 0;

			if(Geoset.Unselectable) Flags |= 4;

			Saver.PushLocation();

			Saver.WriteTag("VRTX");
			Saver.WriteInt32(Geoset.Vertices.Count);

			foreach(Model.CGeosetVertex Vertex in Geoset.Vertices)
			{
				Saver.WriteVector3(Vertex.Position);
			}

			Saver.WriteTag("NRMS");
			Saver.WriteInt32(Geoset.Vertices.Count);

			foreach(Model.CGeosetVertex Vertex in Geoset.Vertices)
			{
				Saver.WriteVector3(Vertex.Normal);
			}

			Saver.WriteTag("PTYP");
			Saver.WriteInt32(1);
			Saver.WriteInt32(4);

			Saver.WriteTag("PCNT");
			Saver.WriteInt32(1);
			Saver.WriteInt32(Geoset.Faces.Count * 3);

			Saver.WriteTag("PVTX");
			Saver.WriteInt32(Geoset.Faces.Count * 3);

			foreach(Model.CGeosetFace Face in Geoset.Faces)
			{
				Saver.WriteInt16(Face.Vertex1.ObjectId);
				Saver.WriteInt16(Face.Vertex2.ObjectId);
				Saver.WriteInt16(Face.Vertex3.ObjectId);
			}

			Saver.WriteTag("GNDX");
			Saver.WriteInt32(Geoset.Vertices.Count);

			foreach(Model.CGeosetVertex Vertex in Geoset.Vertices)
			{
				Saver.WriteInt8(Vertex.Group.ObjectId);
			}

			Saver.WriteTag("MTGC");
			Saver.WriteInt32(Geoset.Groups.Count);

			foreach(Model.CGeosetGroup Group in Geoset.Groups)
			{
				TotalGroupSize += Group.Nodes.Count;
				Saver.WriteInt32(Group.Nodes.Count);
			}

			Saver.WriteTag("MATS");
			Saver.WriteInt32(TotalGroupSize);

			foreach(Model.CGeosetGroup Group in Geoset.Groups)
			{
				foreach(Model.CGeosetGroupNode Node in Group.Nodes)
				{
					Saver.WriteInt32(Node.Node.ObjectId);
				}
			}

			Saver.WriteInt32(Geoset.Material.ObjectId);
			Saver.WriteInt32(Flags);
			Saver.WriteInt32(Geoset.SelectionGroup);
			Saver.WriteExtent(Geoset.Extent);
			Saver.WriteInt32(Geoset.Extents.Count);

			foreach(Model.CGeosetExtent Extent in Geoset.Extents)
			{
				Saver.WriteExtent(Extent.Extent);
			}

			Saver.WriteTag("UVAS");
			Saver.WriteInt32(1);

			Saver.WriteTag("UVBS");
			Saver.WriteInt32(Geoset.Vertices.Count);

			foreach(Model.CGeosetVertex Vertex in Geoset.Vertices)
			{
				Saver.WriteVector2(Vertex.TexturePosition);
			}

			Saver.PopInclusiveLocation();
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
