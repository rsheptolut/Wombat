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
	internal sealed class CGeosetVertex : CObject
	{
		private CGeosetVertex()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CGeoset Geoset, Model.CGeosetVertex GeosetVertex)
		{
			GeosetVertex.Position = ReadVector3(Node, "position", GeosetVertex.Position);
			GeosetVertex.Normal = ReadVector3(Node, "normal", GeosetVertex.Normal);
			GeosetVertex.TexturePosition = ReadVector2(Node, "texture_position", GeosetVertex.TexturePosition);

			Loader.Attacher.AddObject(Geoset.Groups, GeosetVertex.Group, ReadInteger(Node, "geoset_group", CConstants.InvalidId));
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CGeoset Geoset, Model.CGeosetVertex GeosetVertex)
		{
			WriteVector3(Node, "position", GeosetVertex.Position);
			WriteVector3(Node, "normal", GeosetVertex.Normal);
			WriteVector2(Node, "texture_position", GeosetVertex.TexturePosition);

			WriteInteger(Node, "geoset_group", GeosetVertex.Group.ObjectId);
		}

		public static CGeosetVertex Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CGeosetVertex Instance = new CGeosetVertex();
		}
	}
}
