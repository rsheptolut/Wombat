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
	internal sealed class CTextureAnimation : CObject
	{
		private CTextureAnimation()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CTextureAnimation TextureAnimation)
		{
			LoadAnimator(Loader, Node, Model, TextureAnimation.Translation, Value.CVector3.Instance, "translation");
			LoadAnimator(Loader, Node, Model, TextureAnimation.Rotation, Value.CVector4.Instance, "rotation");
			LoadAnimator(Loader, Node, Model, TextureAnimation.Scaling, Value.CVector3.Instance, "scaling");
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CTextureAnimation TextureAnimation)
		{
			SaveAnimator(Saver, Node, Model, TextureAnimation.Translation, Value.CVector3.Instance, "translation");
			SaveAnimator(Saver, Node, Model, TextureAnimation.Rotation, Value.CVector4.Instance, "rotation");
			SaveAnimator(Saver, Node, Model, TextureAnimation.Scaling, Value.CVector3.Instance, "scaling");
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
