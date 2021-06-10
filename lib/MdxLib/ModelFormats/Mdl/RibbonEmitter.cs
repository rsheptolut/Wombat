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
	internal sealed class CRibbonEmitter : CNode
	{
		private CRibbonEmitter()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			Model.CRibbonEmitter RibbonEmitter = new Model.CRibbonEmitter(Model);
			Load(Loader, Model, RibbonEmitter);
			Model.RibbonEmitters.Add(RibbonEmitter);
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CRibbonEmitter RibbonEmitter)
		{
			RibbonEmitter.Name = Loader.ReadString();
			Loader.ExpectToken(Token.EType.CurlyBracketLeft);

			while(true)
			{
				if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
				{
					Loader.ReadToken();
					break;
				}

				string Tag = Loader.ReadWord();

				if(!LoadNode(Loader, Model, RibbonEmitter, Tag))
				{
					switch(Tag)
					{
						case "static":
						{
							Tag = Loader.ReadWord();

							if(!LoadStaticNode(Loader, Model, RibbonEmitter, Tag))
							{
								switch(Tag)
								{
									case "heightabove": { LoadStaticAnimator(Loader, Model, RibbonEmitter.HeightAbove, Value.CFloat.Instance); break; }
									case "heightbelow": { LoadStaticAnimator(Loader, Model, RibbonEmitter.HeightBelow, Value.CFloat.Instance); break; }
									case "alpha": { LoadStaticAnimator(Loader, Model, RibbonEmitter.Alpha, Value.CFloat.Instance); break; }
									case "color": { LoadStaticAnimator(Loader, Model, RibbonEmitter.Color, Value.CColor.Instance); break; }
									case "textureslot": { LoadStaticAnimator(Loader, Model, RibbonEmitter.TextureSlot, Value.CInteger.Instance); break; }
									case "visibility": { LoadStaticAnimator(Loader, Model, RibbonEmitter.Visibility, Value.CFloat.Instance); break; }

									default:
									{
										throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
									}
								}
							}

							break;
						}

						case "heightabove": { LoadAnimator(Loader, Model, RibbonEmitter.HeightAbove, Value.CFloat.Instance); break; }
						case "heightbelow": { LoadAnimator(Loader, Model, RibbonEmitter.HeightBelow, Value.CFloat.Instance); break; }
						case "alpha": { LoadAnimator(Loader, Model, RibbonEmitter.Alpha, Value.CFloat.Instance); break; }
						case "color": { LoadAnimator(Loader, Model, RibbonEmitter.Color, Value.CColor.Instance); break; }
						case "textureslot": { LoadAnimator(Loader, Model, RibbonEmitter.TextureSlot, Value.CInteger.Instance); break; }
						case "visibility": { LoadAnimator(Loader, Model, RibbonEmitter.Visibility, Value.CFloat.Instance); break; }

						case "emissionrate": { RibbonEmitter.EmissionRate = LoadInteger(Loader); break; }
						case "lifespan": { RibbonEmitter.LifeSpan = LoadFloat(Loader); break; }
						case "gravity": { RibbonEmitter.Gravity = LoadFloat(Loader); break; }
						case "rows": { RibbonEmitter.Rows = LoadInteger(Loader); break; }
						case "columns": { RibbonEmitter.Columns = LoadInteger(Loader); break; }
						case "materialid": { Loader.Attacher.AddObject(Model.Materials, RibbonEmitter.Material, LoadId(Loader)); break; }

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
			if(Model.HasRibbonEmitters)
			{
				foreach(Model.CRibbonEmitter RibbonEmitter in Model.RibbonEmitters)
				{
					Save(Saver, Model, RibbonEmitter);
				}
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CRibbonEmitter RibbonEmitter)
		{
			Saver.BeginGroup("RibbonEmitter", RibbonEmitter.Name);

			SaveNode(Saver, Model, RibbonEmitter);

			SaveInteger(Saver, "EmissionRate", RibbonEmitter.EmissionRate);
			SaveFloat(Saver, "LifeSpan", RibbonEmitter.LifeSpan);
			SaveFloat(Saver, "Gravity", RibbonEmitter.Gravity, ECondition.NotZero);
			SaveInteger(Saver, "Rows", RibbonEmitter.Rows);
			SaveInteger(Saver, "Columns", RibbonEmitter.Columns);
			SaveId(Saver, "MaterialID", RibbonEmitter.Material.ObjectId, ECondition.NotInvalidId);

			SaveAnimator(Saver, Model, RibbonEmitter.HeightAbove, Value.CFloat.Instance, "HeightAbove");
			SaveAnimator(Saver, Model, RibbonEmitter.HeightBelow, Value.CFloat.Instance, "HeightBelow");
			SaveAnimator(Saver, Model, RibbonEmitter.Alpha, Value.CFloat.Instance, "Alpha");
			SaveAnimator(Saver, Model, RibbonEmitter.Color, Value.CColor.Instance, "Color");
			SaveAnimator(Saver, Model, RibbonEmitter.TextureSlot, Value.CInteger.Instance, "TextureSlot");
			SaveAnimator(Saver, Model, RibbonEmitter.Visibility, Value.CFloat.Instance, "Visibility", ECondition.NotOne);

			Saver.EndGroup();
		}

		public static CRibbonEmitter Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CRibbonEmitter Instance = new CRibbonEmitter();
		}
	}
}
