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
	internal sealed class CLight : CNode
	{
		private CLight()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CLight Light)
		{
			LoadNode(Loader, Node, Model, Light);

			Light.Type = StringToType(ReadString(Node, "type", TypeToString(Light.Type)));

			LoadAnimator(Loader, Node, Model, Light.AttenuationStart, Value.CFloat.Instance, "attenuation_start");
			LoadAnimator(Loader, Node, Model, Light.AttenuationEnd, Value.CFloat.Instance, "attenuation_end");
			LoadAnimator(Loader, Node, Model, Light.Color, Value.CVector3.Instance, "color");
			LoadAnimator(Loader, Node, Model, Light.Intensity, Value.CFloat.Instance, "intensity");
			LoadAnimator(Loader, Node, Model, Light.AmbientColor, Value.CVector3.Instance, "ambient_color");
			LoadAnimator(Loader, Node, Model, Light.AmbientIntensity, Value.CFloat.Instance, "ambient_intensity");
			LoadAnimator(Loader, Node, Model, Light.Visibility, Value.CFloat.Instance, "visibility");
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CLight Light)
		{
			SaveNode(Saver, Node, Model, Light);

			WriteString(Node, "type", TypeToString(Light.Type));

			SaveAnimator(Saver, Node, Model, Light.AttenuationStart, Value.CFloat.Instance, "attenuation_start");
			SaveAnimator(Saver, Node, Model, Light.AttenuationEnd, Value.CFloat.Instance, "attenuation_end");
			SaveAnimator(Saver, Node, Model, Light.Color, Value.CVector3.Instance, "color");
			SaveAnimator(Saver, Node, Model, Light.Intensity, Value.CFloat.Instance, "intensity");
			SaveAnimator(Saver, Node, Model, Light.AmbientColor, Value.CVector3.Instance, "ambient_color");
			SaveAnimator(Saver, Node, Model, Light.AmbientIntensity, Value.CFloat.Instance, "ambient_intensity");
			SaveAnimator(Saver, Node, Model, Light.Visibility, Value.CFloat.Instance, "visibility");
		}

		private string TypeToString(Model.ELightType Type)
		{
			switch(Type)
			{
				case Model.ELightType.Omnidirectional: return "omnidirectional";
				case Model.ELightType.Directional: return "directional";
				case Model.ELightType.Ambient: return "ambient";
			}

			return "";
		}

		private Model.ELightType StringToType(string String)
		{
			switch(String)
			{
				case "omnidirectional": return Model.ELightType.Omnidirectional;
				case "directional": return Model.ELightType.Directional;
				case "ambient": return Model.ELightType.Ambient;
			}

			return Model.ELightType.Omnidirectional;
		}

		public static CLight Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CLight Instance = new CLight();
		}
	}
}
