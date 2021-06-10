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
	internal sealed class CTextureAnimation : CObject
	{
		private CTextureAnimation()
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
					case "tvertexanim":
					{
						Model.CTextureAnimation TextureAnimation = new Model.CTextureAnimation(Model);
						Load(Loader, Model, TextureAnimation);
						Model.TextureAnimations.Add(TextureAnimation);
						break;
					}

					default:
					{
						throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
					}
				}
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CTextureAnimation TextureAnimation)
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
							case "translation": { LoadStaticAnimator(Loader, Model, TextureAnimation.Translation, Value.CVector3.Instance); break; }
							case "rotation": { LoadStaticAnimator(Loader, Model, TextureAnimation.Rotation, Value.CVector4.Instance); break; }
							case "scaling": { LoadStaticAnimator(Loader, Model, TextureAnimation.Scaling, Value.CVector3.Instance); break; }

							default:
							{
								throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
							}
						}

						break;
					}

					case "translation": { LoadAnimator(Loader, Model, TextureAnimation.Translation, Value.CVector3.Instance); break; }
					case "rotation": { LoadAnimator(Loader, Model, TextureAnimation.Rotation, Value.CVector4.Instance); break; }
					case "scaling": { LoadAnimator(Loader, Model, TextureAnimation.Scaling, Value.CVector3.Instance); break; }

					default:
					{
						throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
					}
				}
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasTextureAnimations)
			{
				Saver.BeginGroup("TextureAnims", Model.TextureAnimations.Count);

				foreach(Model.CTextureAnimation TextureAnimation in Model.TextureAnimations)
				{
					Save(Saver, Model, TextureAnimation);
				}

				Saver.EndGroup();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CTextureAnimation TextureAnimation)
		{
			Saver.BeginGroup("TVertexAnim");

			SaveAnimator(Saver, Model, TextureAnimation.Translation, Value.CVector3.Instance, "Translation", ECondition.NotZero);
			SaveAnimator(Saver, Model, TextureAnimation.Rotation, Value.CVector4.Instance, "Rotation", ECondition.NotDefaultQuaternion);
			SaveAnimator(Saver, Model, TextureAnimation.Scaling, Value.CVector3.Instance, "Scaling", ECondition.NotOne);

			Saver.EndGroup();
		}

		public static CTextureAnimation Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CTextureAnimation Instance = new CTextureAnimation();
		}
	}
}
