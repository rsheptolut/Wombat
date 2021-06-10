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
	internal sealed class CLight : CNode
	{
		private CLight()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			Model.CLight Light = new Model.CLight(Model);
			Load(Loader, Model, Light);
			Model.Lights.Add(Light);
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CLight Light)
		{
			Light.Name = Loader.ReadString();
			Loader.ExpectToken(Token.EType.CurlyBracketLeft);

			while(true)
			{
				if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
				{
					Loader.ReadToken();
					break;
				}

				string Tag = Loader.ReadWord();

				if(!LoadNode(Loader, Model, Light, Tag))
				{
					switch(Tag)
					{
						case "static":
						{
							Tag = Loader.ReadWord();

							if(!LoadStaticNode(Loader, Model, Light, Tag))
							{
								switch(Tag)
								{
									case "attenuationstart": { LoadStaticAnimator(Loader, Model, Light.AttenuationStart, Value.CFloat.Instance); break; }
									case "attenuationend": { LoadStaticAnimator(Loader, Model, Light.AttenuationEnd, Value.CFloat.Instance); break; }
									case "color": { LoadStaticAnimator(Loader, Model, Light.Color, Value.CColor.Instance); break; }
									case "intensity": { LoadStaticAnimator(Loader, Model, Light.Intensity, Value.CFloat.Instance); break; }
									case "ambcolor": { LoadStaticAnimator(Loader, Model, Light.AmbientColor, Value.CColor.Instance); break; }
									case "ambintensity": { LoadStaticAnimator(Loader, Model, Light.AmbientIntensity, Value.CFloat.Instance); break; }
									case "visibility": { LoadStaticAnimator(Loader, Model, Light.Visibility, Value.CFloat.Instance); break; }

									default:
									{
										throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
									}
								}
							}

							break;
						}

						case "attenuationstart": { LoadAnimator(Loader, Model, Light.AttenuationStart, Value.CFloat.Instance); break; }
						case "attenuationend": { LoadAnimator(Loader, Model, Light.AttenuationEnd, Value.CFloat.Instance); break; }
						case "color": { LoadAnimator(Loader, Model, Light.Color, Value.CColor.Instance); break; }
						case "intensity": { LoadAnimator(Loader, Model, Light.Intensity, Value.CFloat.Instance); break; }
						case "ambcolor": { LoadAnimator(Loader, Model, Light.AmbientColor, Value.CColor.Instance); break; }
						case "ambintensity": { LoadAnimator(Loader, Model, Light.AmbientIntensity, Value.CFloat.Instance); break; }
						case "visibility": { LoadAnimator(Loader, Model, Light.Visibility, Value.CFloat.Instance); break; }
						
						case "omnidirectional": { Light.Type = MdxLib.Model.ELightType.Omnidirectional; LoadBoolean(Loader); break; }
						case "directional": { Light.Type = MdxLib.Model.ELightType.Directional; LoadBoolean(Loader); break; }
						case "ambient": { Light.Type = MdxLib.Model.ELightType.Ambient; LoadBoolean(Loader); break; }

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
			if(Model.HasLights)
			{
				foreach(Model.CLight Light in Model.Lights)
				{
					Save(Saver, Model, Light);
				}
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CLight Light)
		{
			Saver.BeginGroup("Light", Light.Name);

			SaveNode(Saver, Model, Light);

			SaveBoolean(Saver, TypeToString(Light.Type), true);
			SaveAnimator(Saver, Model, Light.AttenuationStart, Value.CFloat.Instance, "AttenuationStart");
			SaveAnimator(Saver, Model, Light.AttenuationEnd, Value.CFloat.Instance, "AttenuationEnd");
			SaveAnimator(Saver, Model, Light.Color, Value.CColor.Instance, "Color");
			SaveAnimator(Saver, Model, Light.Intensity, Value.CFloat.Instance, "Intensity");
			SaveAnimator(Saver, Model, Light.AmbientColor, Value.CColor.Instance, "AmbColor");
			SaveAnimator(Saver, Model, Light.AmbientIntensity, Value.CFloat.Instance, "AmbIntensity");
			SaveAnimator(Saver, Model, Light.Visibility, Value.CFloat.Instance, "Visibility", ECondition.NotOne);

			Saver.EndGroup();
		}

		private string TypeToString(Model.ELightType Type)
		{
			switch(Type)
			{
				case Model.ELightType.Omnidirectional: return "Omnidirectional";
				case Model.ELightType.Directional: return "Directional";
				case Model.ELightType.Ambient: return "Ambient";
			}

			return "";
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
