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
	internal sealed class CMaterialLayer : CObject
	{
		private CMaterialLayer()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CMaterial Material, Model.CMaterialLayer MaterialLayer)
		{
			MaterialLayer.FilterMode = StringToFilterMode(ReadString(Node, "filter_mode", FilterModeToString(MaterialLayer.FilterMode)));
			MaterialLayer.CoordId = ReadInteger(Node, "coord_id", MaterialLayer.CoordId);
			MaterialLayer.Unshaded = ReadBoolean(Node, "unshaded", MaterialLayer.Unshaded);
			MaterialLayer.Unfogged = ReadBoolean(Node, "unfogged", MaterialLayer.Unfogged);
			MaterialLayer.TwoSided = ReadBoolean(Node, "two_sided", MaterialLayer.TwoSided);
			MaterialLayer.SphereEnvironmentMap = ReadBoolean(Node, "sphere_environment_map", MaterialLayer.SphereEnvironmentMap);
			MaterialLayer.NoDepthTest = ReadBoolean(Node, "no_depth_test", MaterialLayer.NoDepthTest);
			MaterialLayer.NoDepthSet = ReadBoolean(Node, "no_depth_set", MaterialLayer.NoDepthSet);

			LoadAnimator(Loader, Node, Model, MaterialLayer.TextureId, Value.CInteger.Instance, "texture_id");
			LoadAnimator(Loader, Node, Model, MaterialLayer.Alpha, Value.CFloat.Instance, "alpha");

			Loader.Attacher.AddObject(Model.Textures, MaterialLayer.Texture, ReadInteger(Node, "texture", CConstants.InvalidId));
			Loader.Attacher.AddObject(Model.TextureAnimations, MaterialLayer.TextureAnimation, ReadInteger(Node, "texture_animation", CConstants.InvalidId));
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CMaterial Material, Model.CMaterialLayer MaterialLayer)
		{
			WriteString(Node, "filter_mode", FilterModeToString(MaterialLayer.FilterMode));
			WriteInteger(Node, "coord_id", MaterialLayer.CoordId);
			WriteBoolean(Node, "unshaded", MaterialLayer.Unshaded);
			WriteBoolean(Node, "unfogged", MaterialLayer.Unfogged);
			WriteBoolean(Node, "two_sided", MaterialLayer.TwoSided);
			WriteBoolean(Node, "sphere_environment_map", MaterialLayer.SphereEnvironmentMap);
			WriteBoolean(Node, "no_depth_test", MaterialLayer.NoDepthTest);
			WriteBoolean(Node, "no_depth_set", MaterialLayer.NoDepthSet);

			SaveAnimator(Saver, Node, Model, MaterialLayer.TextureId, Value.CInteger.Instance, "texture_id");
			SaveAnimator(Saver, Node, Model, MaterialLayer.Alpha, Value.CFloat.Instance, "alpha");

			WriteInteger(Node, "texture", MaterialLayer.Texture.ObjectId);
			WriteInteger(Node, "texture_animation", MaterialLayer.TextureAnimation.ObjectId);
		}

		private string FilterModeToString(Model.EMaterialLayerFilterMode FilterMode)
		{
			switch(FilterMode)
			{
				case Model.EMaterialLayerFilterMode.None: return "none";
				case Model.EMaterialLayerFilterMode.Transparent: return "transparent";
				case Model.EMaterialLayerFilterMode.Blend: return "blend";
				case Model.EMaterialLayerFilterMode.Additive: return "additive";
				case Model.EMaterialLayerFilterMode.AdditiveAlpha: return "additive_alpha";
				case Model.EMaterialLayerFilterMode.Modulate: return "modulate";
				case Model.EMaterialLayerFilterMode.Modulate2x: return "modulate_2x";
			}

			return "";
		}

		private Model.EMaterialLayerFilterMode StringToFilterMode(string String)
		{
			switch(String)
			{
				case "none": return Model.EMaterialLayerFilterMode.None;
				case "transparent": return Model.EMaterialLayerFilterMode.Transparent;
				case "blend": return Model.EMaterialLayerFilterMode.Blend;
				case "additive": return Model.EMaterialLayerFilterMode.Additive;
				case "additive_alpha": return Model.EMaterialLayerFilterMode.AdditiveAlpha;
				case "modulate": return Model.EMaterialLayerFilterMode.Modulate;
				case "modulate_2x": return Model.EMaterialLayerFilterMode.Modulate2x;
			}

			return Model.EMaterialLayerFilterMode.None;
		}

		public static CMaterialLayer Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CMaterialLayer Instance = new CMaterialLayer();
		}
	}
}
