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
	internal sealed class CTextureAnimation : CObject
	{
		private CTextureAnimation()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CTextureAnimation TextureAnimation = new Model.CTextureAnimation(Model);
				Load(Loader, Model, TextureAnimation);
				Model.TextureAnimations.Add(TextureAnimation);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many TextureAnimation bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CTextureAnimation TextureAnimation)
		{
			Loader.PushLocation();

			int Size = Loader.ReadInt32();

			Size -= Loader.PopLocation();
			if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many TextureAnimation bytes were read!");

			while(Size > 0)
			{
				Loader.PushLocation();

				string Tag = Loader.ReadTag();

				switch(Tag)
				{
					case "KTAT": { LoadAnimator(Loader, Model, TextureAnimation.Translation, Value.CVector3.Instance); break; }
					case "KTAR": { LoadAnimator(Loader, Model, TextureAnimation.Rotation, Value.CVector4.Instance); break; }
					case "KTAS": { LoadAnimator(Loader, Model, TextureAnimation.Scaling, Value.CVector3.Instance); break; }

					default:
					{
						throw new System.Exception("Error at location " + Loader.Location + ", unknown TextureAnimation tag \"" + Tag + "\"!");
					}
				}

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many TextureAnimation bytes were read!");
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasTextureAnimations)
			{
				Saver.WriteTag("TXAN");
				Saver.PushLocation();

				foreach(Model.CTextureAnimation TextureAnimation in Model.TextureAnimations)
				{
					Save(Saver, Model, TextureAnimation);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CTextureAnimation TextureAnimation)
		{
			Saver.PushLocation();

			SaveAnimator(Saver, Model, TextureAnimation.Translation, Value.CVector3.Instance, "KTAT");
			SaveAnimator(Saver, Model, TextureAnimation.Rotation, Value.CVector4.Instance, "KTAR");
			SaveAnimator(Saver, Model, TextureAnimation.Scaling, Value.CVector3.Instance, "KTAS");

			Saver.PopInclusiveLocation();
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
