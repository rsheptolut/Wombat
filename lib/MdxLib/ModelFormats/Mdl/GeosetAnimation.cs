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
	internal sealed class CGeosetAnimation : CObject
	{
		private CGeosetAnimation()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			Model.CGeosetAnimation GeosetAnimation = new Model.CGeosetAnimation(Model);
			Load(Loader, Model, GeosetAnimation);
			Model.GeosetAnimations.Add(GeosetAnimation);
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CGeosetAnimation GeosetAnimation)
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
					case "static":
					{
						Tag = Loader.ReadWord();

						switch(Tag)
						{
							case "alpha": { LoadStaticAnimator(Loader, Model, GeosetAnimation.Alpha, Value.CFloat.Instance); break; }
							case "color": { LoadStaticAnimator(Loader, Model, GeosetAnimation.Color, Value.CColor.Instance); GeosetAnimation.UseColor = true; break; }

							default:
							{
								throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
							}
						}

						break;
					}

					case "alpha": { LoadAnimator(Loader, Model, GeosetAnimation.Alpha, Value.CFloat.Instance); break; }
					case "color": { LoadAnimator(Loader, Model, GeosetAnimation.Color, Value.CColor.Instance); break; }
					
					case "geosetid": { Loader.Attacher.AddObject(Model.Geosets, GeosetAnimation.Geoset, LoadId(Loader)); break; }
					case "dropshadow": { GeosetAnimation.DropShadow = LoadBoolean(Loader); break; }

					default:
					{
						throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
					}
				}
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasGeosetAnimations)
			{
				foreach(Model.CGeosetAnimation GeosetAnimation in Model.GeosetAnimations)
				{
					Save(Saver, Model, GeosetAnimation);
				}
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CGeosetAnimation GeosetAnimation)
		{
			Saver.BeginGroup("GeosetAnim");

			SaveAnimator(Saver, Model, GeosetAnimation.Alpha, Value.CFloat.Instance, "Alpha", ECondition.NotOne);
			if(GeosetAnimation.UseColor) SaveAnimator(Saver, Model, GeosetAnimation.Color, Value.CColor.Instance, "Color", ECondition.NotOne);

			SaveId(Saver, "GeosetId", GeosetAnimation.Geoset.ObjectId, ECondition.NotInvalidId);
			SaveBoolean(Saver, "DropShadow", GeosetAnimation.DropShadow);

			Saver.EndGroup();
		}

		public static CGeosetAnimation Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CGeosetAnimation Instance = new CGeosetAnimation();
		}
	}
}
