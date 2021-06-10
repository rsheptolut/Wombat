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
	internal sealed class CGeoset : CObject
	{
		private CGeoset()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CGeoset Geoset)
		{
			Geoset.SelectionGroup = ReadInteger(Node, "selection_group", Geoset.SelectionGroup);
			Geoset.Unselectable = ReadBoolean(Node, "unselectable", Geoset.Unselectable);
			Geoset.Extent = ReadExtent(Node, "extent", Geoset.Extent);

			Loader.Attacher.AddObject(Model.Materials, Geoset.Material, ReadInteger(Node, "material", CConstants.InvalidId));

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("geoset_vertex"))
			{
				Model.CGeosetVertex GeosetVertex = new Model.CGeosetVertex(Model);
				CGeosetVertex.Instance.Load(Loader, ChildNode, Model, Geoset, GeosetVertex);
				Geoset.Vertices.Add(GeosetVertex);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("geoset_face"))
			{
				Model.CGeosetFace GeosetFace = new Model.CGeosetFace(Model);
				CGeosetFace.Instance.Load(Loader, ChildNode, Model, Geoset, GeosetFace);
				Geoset.Faces.Add(GeosetFace);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("geoset_group"))
			{
				Model.CGeosetGroup GeosetGroup = new Model.CGeosetGroup(Model);
				CGeosetGroup.Instance.Load(Loader, ChildNode, Model, Geoset, GeosetGroup);
				Geoset.Groups.Add(GeosetGroup);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("geoset_extent"))
			{
				Model.CGeosetExtent GeosetExtent = new Model.CGeosetExtent(Model);
				CGeosetExtent.Instance.Load(Loader, ChildNode, Model, Geoset, GeosetExtent);
				Geoset.Extents.Add(GeosetExtent);
			}
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CGeoset Geoset)
		{
			WriteInteger(Node, "selection_group", Geoset.SelectionGroup);
			WriteBoolean(Node, "unselectable", Geoset.Unselectable);
			WriteExtent(Node, "extent", Geoset.Extent);

			WriteInteger(Node, "material", Geoset.Material.ObjectId);

			if(Geoset.HasVertices)
			{
				foreach(Model.CGeosetVertex GeosetVertex in Geoset.Vertices)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "geoset_vertex");
					CGeosetVertex.Instance.Save(Saver, Element, Model, Geoset, GeosetVertex);
				}
			}

			if(Geoset.HasFaces)
			{
				foreach(Model.CGeosetFace GeosetFace in Geoset.Faces)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "geoset_face");
					CGeosetFace.Instance.Save(Saver, Element, Model, Geoset, GeosetFace);
				}
			}

			if(Geoset.HasGroups)
			{
				foreach(Model.CGeosetGroup GeosetGroup in Geoset.Groups)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "geoset_group");
					CGeosetGroup.Instance.Save(Saver, Element, Model, Geoset, GeosetGroup);
				}
			}

			if(Geoset.HasExtents)
			{
				foreach(Model.CGeosetExtent GeosetExtent in Geoset.Extents)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "geoset_extent");
					CGeosetExtent.Instance.Save(Saver, Element, Model, Geoset, GeosetExtent);
				}
			}
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
