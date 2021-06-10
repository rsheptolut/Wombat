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
	internal sealed class CCamera : CObject
	{
		private CCamera()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CCamera Camera = new Model.CCamera(Model);
				Load(Loader, Model, Camera);
				Model.Cameras.Add(Camera);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Camera bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CCamera Camera)
		{
			Loader.PushLocation();

			int Size = Loader.ReadInt32();
			Camera.Name = Loader.ReadString(CConstants.SizeName);
			Camera.Position = Loader.ReadVector3();
			Camera.FieldOfView = Loader.ReadFloat();
			Camera.FarDistance = Loader.ReadFloat();
			Camera.NearDistance = Loader.ReadFloat();
			Camera.TargetPosition = Loader.ReadVector3();

			Size -= Loader.PopLocation();
			if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Camera bytes were read!");

			while(Size > 0)
			{
				Loader.PushLocation();

				string Tag = Loader.ReadTag();

				switch(Tag)
				{
					case "KCTR": { LoadAnimator(Loader, Model, Camera.Translation, Value.CVector3.Instance); break; }
					case "KTTR": { LoadAnimator(Loader, Model, Camera.TargetTranslation, Value.CVector3.Instance); break; }
					case "KCRL": { LoadAnimator(Loader, Model, Camera.Rotation, Value.CFloat.Instance); break; }

					default:
					{
						throw new System.Exception("Error at location " + Loader.Location + ", unknown Camera tag \"" + Tag + "\"!");
					}
				}

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Camera bytes were read!");
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasCameras)
			{
				Saver.WriteTag("CAMS");
				Saver.PushLocation();

				foreach(Model.CCamera Camera in Model.Cameras)
				{
					Save(Saver, Model, Camera);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CCamera Camera)
		{
			Saver.PushLocation();

			Saver.WriteString(Camera.Name, CConstants.SizeName);
			Saver.WriteVector3(Camera.Position);
			Saver.WriteFloat(Camera.FieldOfView);
			Saver.WriteFloat(Camera.FarDistance);
			Saver.WriteFloat(Camera.NearDistance);
			Saver.WriteVector3(Camera.TargetPosition);

			SaveAnimator(Saver, Model, Camera.Translation, Value.CVector3.Instance, "KCTR");
			SaveAnimator(Saver, Model, Camera.TargetTranslation, Value.CVector3.Instance, "KTTR");
			SaveAnimator(Saver, Model, Camera.Rotation, Value.CFloat.Instance, "KCRL");

			Saver.PopInclusiveLocation();
		}

		public static CCamera Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CCamera Instance = new CCamera();
		}
	}
}
