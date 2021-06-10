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
	internal sealed class CGeosetAnimation : CObject
	{
		private CGeosetAnimation()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CGeosetAnimation GeosetAnimation = new Model.CGeosetAnimation(Model);
				Load(Loader, Model, GeosetAnimation);
				Model.GeosetAnimations.Add(GeosetAnimation);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many GeosetAnimation bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CGeosetAnimation GeosetAnimation)
		{
			Loader.PushLocation();

			int Size = Loader.ReadInt32();
			GeosetAnimation.Alpha.MakeStatic(Loader.ReadFloat());
			int Flags = Loader.ReadInt32();
			GeosetAnimation.Color.MakeStatic(Loader.ReadVector3());
			Loader.Attacher.AddObject(Model.Geosets, GeosetAnimation.Geoset, Loader.ReadInt32());

			GeosetAnimation.DropShadow = ((Flags & 1) != 0);
			GeosetAnimation.UseColor = ((Flags & 2) != 0);

			Size -= Loader.PopLocation();
			if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many GeosetAnimation bytes were read!");

			while(Size > 0)
			{
				Loader.PushLocation();

				string Tag = Loader.ReadTag();

				switch(Tag)
				{
					case "KGAO": { LoadAnimator(Loader, Model, GeosetAnimation.Alpha, Value.CFloat.Instance); break; }
					case "KGAC": { LoadAnimator(Loader, Model, GeosetAnimation.Color, Value.CVector3.Instance); break; }

					default:
					{
						throw new System.Exception("Error at location " + Loader.Location + ", unknown GeosetAnimation tag \"" + Tag + "\"!");
					}
				}

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many GeosetAnimation bytes were read!");
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasGeosetAnimations)
			{
				Saver.WriteTag("GEOA");
				Saver.PushLocation();

				foreach(Model.CGeosetAnimation GeosetAnimation in Model.GeosetAnimations)
				{
					Save(Saver, Model, GeosetAnimation);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CGeosetAnimation GeosetAnimation)
		{
			int Flags = 0;

			if(GeosetAnimation.DropShadow) Flags |= 1;
			if(GeosetAnimation.UseColor) Flags |= 2;

			Saver.PushLocation();

			Saver.WriteFloat(GeosetAnimation.Alpha.GetValue());
			Saver.WriteInt32(Flags);
			Saver.WriteVector3(GeosetAnimation.Color.GetValue());
			Saver.WriteInt32(GeosetAnimation.Geoset.ObjectId);

			SaveAnimator(Saver, Model, GeosetAnimation.Alpha, Value.CFloat.Instance, "KGAO");
			SaveAnimator(Saver, Model, GeosetAnimation.Color, Value.CVector3.Instance, "KGAC");

			Saver.PopInclusiveLocation();
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
