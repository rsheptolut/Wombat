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
	internal sealed class CMaterial : CObject
	{
		private CMaterial()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			Loader.ReadInteger();
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
					case "material":
					{
						Model.CMaterial Material = new Model.CMaterial(Model);
						Load(Loader, Model, Material);
						Model.Materials.Add(Material);
						break;
					}

					default:
					{
						throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
					}
				}
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CMaterial Material)
		{
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
					case "priorityplane": { Material.PriorityPlane = LoadInteger(Loader); break; }
					case "constantcolor": { Material.ConstantColor = LoadBoolean(Loader); break; }
					case "sortprimsnearz": { Material.SortPrimitivesNearZ = LoadBoolean(Loader); break; }
					case "sortprimsfarz": { Material.SortPrimitivesFarZ = LoadBoolean(Loader); break; }
					case "fullresolution": { Material.FullResolution = LoadBoolean(Loader); break; }

					case "layer":
					{
						Model.CMaterialLayer Layer = new Model.CMaterialLayer(Model);

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
								case "static":
								{
									Tag = Loader.ReadWord();

									switch(Tag)
									{
										case "textureid":
										{
											LoadStaticAnimator(Loader, Model, Layer.TextureId, Value.CInteger.Instance);
											Loader.Attacher.AddObject(Model.Textures, Layer.Texture, Layer.TextureId.GetValue());
											break;
										}

										case "alpha": { LoadStaticAnimator(Loader, Model, Layer.Alpha, Value.CFloat.Instance); break; }

										default:
										{
											throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
										}
									}

									break;
								}

								case "textureid": { LoadAnimator(Loader, Model, Layer.TextureId, Value.CInteger.Instance); break; }
								case "alpha": { LoadAnimator(Loader, Model, Layer.Alpha, Value.CFloat.Instance); break; }

								case "tvertexanimid": { Loader.Attacher.AddObject(Model.TextureAnimations, Layer.TextureAnimation, LoadInteger(Loader)); break; }
								case "coordid": { Layer.CoordId = LoadInteger(Loader); break; }
								case "twosided": { Layer.TwoSided = LoadBoolean(Loader); break; }
								case "unshaded": { Layer.Unshaded = LoadBoolean(Loader); break; }
								case "unfogged": { Layer.Unfogged = LoadBoolean(Loader); break; }
								case "sphereenvmap": { Layer.SphereEnvironmentMap = LoadBoolean(Loader); break; }
								case "nodepthtest": { Layer.NoDepthTest = LoadBoolean(Loader); break; }
								case "nodepthset": { Layer.NoDepthSet = LoadBoolean(Loader); break; }
								case "filtermode": { Layer.FilterMode = StringToFilterMode(LoadWord(Loader)); break; }

								default:
								{
									throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
								}
							}
						}

						Material.Layers.Add(Layer);
						break;
					}

					default:
					{
						throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
					}
				}
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasMaterials)
			{
				Saver.BeginGroup("Materials", Model.Materials.Count);

				foreach(Model.CMaterial Material in Model.Materials)
				{
					Save(Saver, Model, Material);
				}

				Saver.EndGroup();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CMaterial Material)
		{
			Saver.BeginGroup("Material");

			SaveBoolean(Saver, "ConstantColor", Material.ConstantColor);
			SaveBoolean(Saver, "SortPrimsNearZ", Material.SortPrimitivesNearZ);
			SaveBoolean(Saver, "SortPrimsFarZ", Material.SortPrimitivesFarZ);
			SaveBoolean(Saver, "FullResolution", Material.FullResolution);
			SaveInteger(Saver, "PriorityPlane", Material.PriorityPlane, ECondition.NotZero);

			foreach(Model.CMaterialLayer Layer in Material.Layers)
			{
				Saver.BeginGroup("Layer");

				Saver.WriteTabs();
				Saver.WriteWord("FilterMode ");
				Saver.WriteWord(FilterModeToString(Layer.FilterMode));
				Saver.WriteLine(",");

				if(Layer.TextureId.Animated)
				{
					SaveAnimator(Saver, Model, Layer.TextureId, Value.CInteger.Instance, "TextureID");
				}
				else
				{
					SaveId(Saver, "static TextureID", Layer.Texture.ObjectId, ECondition.NotInvalidId);
				}

				SaveAnimator(Saver, Model, Layer.Alpha, Value.CFloat.Instance, "Alpha", ECondition.NotOne);

				SaveBoolean(Saver, "TwoSided", Layer.TwoSided);
				SaveBoolean(Saver, "Unshaded", Layer.Unshaded);
				SaveBoolean(Saver, "Unfogged", Layer.Unfogged);
				SaveBoolean(Saver, "SphereEnvMap", Layer.SphereEnvironmentMap);
				SaveBoolean(Saver, "NoDepthTest", Layer.NoDepthTest);
				SaveBoolean(Saver, "NoDepthSet", Layer.NoDepthSet);
				SaveId(Saver, "TVertexAnimId", Layer.TextureAnimation.ObjectId, ECondition.NotInvalidId);
				SaveInteger(Saver, "CoordId", Layer.CoordId, ECondition.NotZero);

				Saver.EndGroup();
			}

			Saver.EndGroup();
		}

		private string FilterModeToString(Model.EMaterialLayerFilterMode FilterMode)
		{
			switch(FilterMode)
			{
				case Model.EMaterialLayerFilterMode.None: return "None";
				case Model.EMaterialLayerFilterMode.Transparent: return "Transparent";
				case Model.EMaterialLayerFilterMode.Blend: return "Blend";
				case Model.EMaterialLayerFilterMode.Additive: return "Additive";
				case Model.EMaterialLayerFilterMode.AdditiveAlpha: return "AddAlpha";
				case Model.EMaterialLayerFilterMode.Modulate: return "Modulate";
				case Model.EMaterialLayerFilterMode.Modulate2x: return "Modulate2x";
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
				case "addalpha": return Model.EMaterialLayerFilterMode.AdditiveAlpha;
				case "modulate": return Model.EMaterialLayerFilterMode.Modulate;
				case "modulate2x": return Model.EMaterialLayerFilterMode.Modulate2x;
			}

			return Model.EMaterialLayerFilterMode.None;
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
