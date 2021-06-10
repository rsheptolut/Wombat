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
	internal sealed class CGeosetAnimation : CObject
	{
		private CGeosetAnimation()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CGeosetAnimation GeosetAnimation)
		{
			GeosetAnimation.UseColor = ReadBoolean(Node, "use_color", GeosetAnimation.UseColor);
			GeosetAnimation.DropShadow = ReadBoolean(Node, "drop_shadow", GeosetAnimation.DropShadow);

			LoadAnimator(Loader, Node, Model, GeosetAnimation.Color, Value.CVector3.Instance, "color");
			LoadAnimator(Loader, Node, Model, GeosetAnimation.Alpha, Value.CFloat.Instance, "alpha");

			Loader.Attacher.AddObject(Model.Geosets, GeosetAnimation.Geoset, ReadInteger(Node, "geoset", CConstants.InvalidId));
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CGeosetAnimation GeosetAnimation)
		{
			WriteBoolean(Node, "use_color", GeosetAnimation.UseColor);
			WriteBoolean(Node, "drop_shadow", GeosetAnimation.DropShadow);

			SaveAnimator(Saver, Node, Model, GeosetAnimation.Color, Value.CVector3.Instance, "color");
			SaveAnimator(Saver, Node, Model, GeosetAnimation.Alpha, Value.CFloat.Instance, "alpha");

			WriteInteger(Node, "geoset", GeosetAnimation.Geoset.ObjectId);
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
