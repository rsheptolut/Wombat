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
	internal sealed class CModelInfo : CObject
	{
		private CModelInfo()
		{
			//Empty
		}

		public void Load(CLoader Loader, Model.CModel Model)
		{
			float ExtentRadius = 0.0f;
			Primitives.CVector3 ExtentMin = CConstants.DefaultVector3;
			Primitives.CVector3 ExtentMax = CConstants.DefaultVector3;

			Model.Name = Loader.ReadString();
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
					case "formatversion":
					{
						Model.Version = LoadInteger(Loader);
						break;
					}

					case "blendtime": { Model.BlendTime = LoadInteger(Loader); break; }
					case "minimumextent": { ExtentMin = LoadVector3(Loader); break; }
					case "maximumextent": { ExtentMax = LoadVector3(Loader); break; }
					case "boundsradius": { ExtentRadius = LoadFloat(Loader); break; }
					case "animationfile": { Model.AnimationFile = LoadString(Loader); break; }
					case "numgeosets": { LoadInteger(Loader); break; }
					case "numgeosetanims": { LoadInteger(Loader); break; }
					case "numhelpers": { LoadInteger(Loader); break; }
					case "numlights": { LoadInteger(Loader); break; }
					case "numbones": { LoadInteger(Loader); break; }
					case "numattachments": { LoadInteger(Loader); break; }
					case "numparticleemitters": { LoadInteger(Loader); break; }
					case "numparticleemitters2": { LoadInteger(Loader); break; }
					case "numribbonemitters": { LoadInteger(Loader); break; }
					case "numevents": { LoadInteger(Loader); break; }

					default:
					{
						throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
					}
				}

				Model.Extent = new Primitives.CExtent(ExtentMin, ExtentMax, ExtentRadius);
			}
		}

		public void Save(CSaver Saver, Model.CModel Model)
		{
			Saver.BeginGroup("Model", Model.Name);

			SaveInteger(Saver, "NumGeosets", Model.Geosets.Count, ECondition.NotZero);
			SaveInteger(Saver, "NumGeosetAnims", Model.GeosetAnimations.Count, ECondition.NotZero);
			SaveInteger(Saver, "NumHelpers", Model.Helpers.Count, ECondition.NotZero);
			SaveInteger(Saver, "NumLights", Model.Lights.Count, ECondition.NotZero);
			SaveInteger(Saver, "NumBones", Model.Bones.Count, ECondition.NotZero);
			SaveInteger(Saver, "NumAttachments", Model.Attachments.Count, ECondition.NotZero);
			SaveInteger(Saver, "NumParticleEmitters", Model.ParticleEmitters.Count, ECondition.NotZero);
			SaveInteger(Saver, "NumParticleEmitters2", Model.ParticleEmitters2.Count, ECondition.NotZero);
			SaveInteger(Saver, "NumRibbonEmitters", Model.RibbonEmitters.Count, ECondition.NotZero);
			SaveInteger(Saver, "NumEvents", Model.Events.Count, ECondition.NotZero);
			SaveInteger(Saver, "BlendTime", Model.BlendTime);
			SaveVector3(Saver, "MinimumExtent", Model.Extent.Min, ECondition.NotZero);
			SaveVector3(Saver, "MaximumExtent", Model.Extent.Max, ECondition.NotZero);
			SaveFloat(Saver, "BoundsRadius", Model.Extent.Radius, ECondition.NotZero);
			SaveString(Saver, "AnimationFile", Model.AnimationFile, ECondition.NotEmpty);

			Saver.EndGroup();
		}

		public static CModelInfo Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CModelInfo Instance = new CModelInfo();
		}
	}
}
