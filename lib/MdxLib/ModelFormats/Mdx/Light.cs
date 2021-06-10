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
	internal sealed class CLight : CNode
	{
		private CLight()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CLight Light = new Model.CLight(Model);
				Load(Loader, Model, Light);
				Model.Lights.Add(Light);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Light bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CLight Light)
		{
			Loader.PushLocation();

			int Size = Loader.ReadInt32();
			LoadNode(Loader, Model, Light);
			int Type = Loader.ReadInt32();

			Light.AttenuationStart.MakeStatic(Loader.ReadFloat());
			Light.AttenuationEnd.MakeStatic(Loader.ReadFloat());
			Light.Color.MakeStatic(Loader.ReadVector3());
			Light.Intensity.MakeStatic(Loader.ReadFloat());
			Light.AmbientColor.MakeStatic(Loader.ReadVector3());
			Light.AmbientIntensity.MakeStatic(Loader.ReadFloat());

			switch(Type)
			{
				case 0: { Light.Type = MdxLib.Model.ELightType.Omnidirectional; break; }
				case 1: { Light.Type = MdxLib.Model.ELightType.Directional; break; }
				case 2: { Light.Type = MdxLib.Model.ELightType.Ambient; break; }
			}

			Size -= Loader.PopLocation();
			if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Light bytes were read!");

			while(Size > 0)
			{
				Loader.PushLocation();

				string Tag = Loader.ReadTag();

				switch(Tag)
				{
					case "KLAS": { LoadAnimator(Loader, Model, Light.AttenuationStart, Value.CFloat.Instance); break; }
					case "KLAE": { LoadAnimator(Loader, Model, Light.AttenuationEnd, Value.CFloat.Instance); break; }
					case "KLAC": { LoadAnimator(Loader, Model, Light.Color, Value.CVector3.Instance); break; }
					case "KLAI": { LoadAnimator(Loader, Model, Light.Intensity, Value.CFloat.Instance); break; }
					case "KLBC": { LoadAnimator(Loader, Model, Light.AmbientColor, Value.CVector3.Instance); break; }
					case "KLBI": { LoadAnimator(Loader, Model, Light.AmbientIntensity, Value.CFloat.Instance); break; }
					case "KLAV": { LoadAnimator(Loader, Model, Light.Visibility, Value.CFloat.Instance); break; }

					default:
					{
						throw new System.Exception("Error at location " + Loader.Location + ", unknown Light tag \"" + Tag + "\"!");
					}
				}

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Light bytes were read!");
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasLights)
			{
				Saver.WriteTag("LITE");
				Saver.PushLocation();

				foreach(Model.CLight Light in Model.Lights)
				{
					Save(Saver, Model, Light);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CLight Light)
		{
			int Type = 0;

			switch(Light.Type)
			{
				case MdxLib.Model.ELightType.Omnidirectional: { Type = 0; break; }
				case MdxLib.Model.ELightType.Directional: { Type = 1; break; }
				case MdxLib.Model.ELightType.Ambient: { Type = 2; break; }
			}

			Saver.PushLocation();
			SaveNode(Saver, Model, Light, 512);

			Saver.WriteInt32(Type);
			Saver.WriteFloat(Light.AttenuationStart.GetValue());
			Saver.WriteFloat(Light.AttenuationEnd.GetValue());
			Saver.WriteVector3(Light.Color.GetValue());
			Saver.WriteFloat(Light.Intensity.GetValue());
			Saver.WriteVector3(Light.AmbientColor.GetValue());
			Saver.WriteFloat(Light.AmbientIntensity.GetValue());

			SaveAnimator(Saver, Model, Light.AttenuationStart, Value.CFloat.Instance, "KLAS");
			SaveAnimator(Saver, Model, Light.AttenuationEnd, Value.CFloat.Instance, "KLAE");
			SaveAnimator(Saver, Model, Light.Color, Value.CVector3.Instance, "KLAC");
			SaveAnimator(Saver, Model, Light.Intensity, Value.CFloat.Instance, "KLAI");
			SaveAnimator(Saver, Model, Light.AmbientColor, Value.CVector3.Instance, "KLBC");
			SaveAnimator(Saver, Model, Light.AmbientIntensity, Value.CFloat.Instance, "KLBI");
			SaveAnimator(Saver, Model, Light.Visibility, Value.CFloat.Instance, "KLAV");

			Saver.PopInclusiveLocation();
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
