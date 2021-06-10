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
	internal sealed class CCamera : CObject
	{
		private CCamera()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CCamera Camera)
		{
			Camera.Name = ReadString(Node, "name", Camera.Name);
			Camera.FieldOfView = ReadFloat(Node, "field_of_view", Camera.FieldOfView);
			Camera.NearDistance = ReadFloat(Node, "near_distance", Camera.NearDistance);
			Camera.FarDistance = ReadFloat(Node, "far_distance", Camera.FarDistance);
			Camera.Position = ReadVector3(Node, "position", Camera.Position);
			Camera.TargetPosition = ReadVector3(Node, "target_position", Camera.TargetPosition);

			LoadAnimator(Loader, Node, Model, Camera.Translation, Value.CVector3.Instance, "source_translation");
			LoadAnimator(Loader, Node, Model, Camera.TargetTranslation, Value.CVector3.Instance, "target_translation");
			LoadAnimator(Loader, Node, Model, Camera.Rotation, Value.CFloat.Instance, "rotation");
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CCamera Camera)
		{
			WriteString(Node, "name", Camera.Name);
			WriteFloat(Node, "field_of_view", Camera.FieldOfView);
			WriteFloat(Node, "near_distance", Camera.NearDistance);
			WriteFloat(Node, "far_distance", Camera.FarDistance);
			WriteVector3(Node, "position", Camera.Position);
			WriteVector3(Node, "target_position", Camera.TargetPosition);

			SaveAnimator(Saver, Node, Model, Camera.Translation, Value.CVector3.Instance, "source_translation");
			SaveAnimator(Saver, Node, Model, Camera.TargetTranslation, Value.CVector3.Instance, "target_translation");
			SaveAnimator(Saver, Node, Model, Camera.Rotation, Value.CFloat.Instance, "rotation");
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
