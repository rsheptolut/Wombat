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
	internal sealed class CMaterial : CObject
	{
		private CMaterial()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CMaterial Material)
		{
			Material.PriorityPlane = ReadInteger(Node, "priority_plane", Material.PriorityPlane);
			Material.ConstantColor = ReadBoolean(Node, "constant_color", Material.ConstantColor);
			Material.FullResolution = ReadBoolean(Node, "full_resolution", Material.FullResolution);
			Material.SortPrimitivesFarZ = ReadBoolean(Node, "sort_primitives_far_z", Material.SortPrimitivesFarZ);
			Material.SortPrimitivesNearZ = ReadBoolean(Node, "sort_primitives_near_z", Material.SortPrimitivesNearZ);

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("material_layer"))
			{
				Model.CMaterialLayer MaterialLayer = new Model.CMaterialLayer(Model);
				CMaterialLayer.Instance.Load(Loader, ChildNode, Model, Material, MaterialLayer);
				Material.Layers.Add(MaterialLayer);
			}
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CMaterial Material)
		{
			WriteInteger(Node, "priority_plane", Material.PriorityPlane);
			WriteBoolean(Node, "constant_color", Material.ConstantColor);
			WriteBoolean(Node, "full_resolution", Material.FullResolution);
			WriteBoolean(Node, "sort_primitives_far_z", Material.SortPrimitivesFarZ);
			WriteBoolean(Node, "sort_primitives_near_z", Material.SortPrimitivesNearZ);

			if(Material.HasLayers)
			{
				foreach(Model.CMaterialLayer MaterialLayer in Material.Layers)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "material_layer");
					CMaterialLayer.Instance.Save(Saver, Element, Model, Material, MaterialLayer);
				}
			}
		}

		public static CMaterial Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CMaterial Instance = new CMaterial();
		}
	}
}
