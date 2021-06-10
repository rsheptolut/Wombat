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
	internal abstract class CNode : CObject
	{
		public CNode()
		{
			//Empty
		}

		public void LoadNode<T>(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, T ModelNode) where T : Model.CNode<T>
		{
			ModelNode.Name = ReadString(Node, "name", ModelNode.Name);
			ModelNode.DontInheritTranslation = ReadBoolean(Node, "dont_inherit_translation", ModelNode.DontInheritTranslation);
			ModelNode.DontInheritRotation = ReadBoolean(Node, "dont_inherit_rotation", ModelNode.DontInheritRotation);
			ModelNode.DontInheritScaling = ReadBoolean(Node, "dont_inherit_scaling", ModelNode.DontInheritScaling);
			ModelNode.Billboarded = ReadBoolean(Node, "billboarded", ModelNode.Billboarded);
			ModelNode.BillboardedLockX = ReadBoolean(Node, "billboarded_lock_x", ModelNode.BillboardedLockX);
			ModelNode.BillboardedLockY = ReadBoolean(Node, "billboarded_lock_y", ModelNode.BillboardedLockY);
			ModelNode.BillboardedLockZ = ReadBoolean(Node, "billboarded_lock_z", ModelNode.BillboardedLockZ);
			ModelNode.CameraAnchored = ReadBoolean(Node, "camera_anchored", ModelNode.CameraAnchored);
			ModelNode.PivotPoint = ReadVector3(Node, "pivot_point", ModelNode.PivotPoint);

			Loader.Attacher.AddNode(Model, ModelNode.Parent, ReadInteger(Node, "parent", CConstants.InvalidId));

			LoadAnimator(Loader, Node, Model, ModelNode.Translation, Value.CVector3.Instance, "translation");
			LoadAnimator(Loader, Node, Model, ModelNode.Rotation, Value.CVector4.Instance, "rotation");
			LoadAnimator(Loader, Node, Model, ModelNode.Scaling, Value.CVector3.Instance, "scaling");
		}

		public void SaveNode<T>(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, T ModelNode) where T : Model.CNode<T>
		{
			WriteString(Node, "name", ModelNode.Name);
			WriteBoolean(Node, "dont_inherit_translation", ModelNode.DontInheritTranslation);
			WriteBoolean(Node, "dont_inherit_rotation", ModelNode.DontInheritRotation);
			WriteBoolean(Node, "dont_inherit_scaling", ModelNode.DontInheritScaling);
			WriteBoolean(Node, "billboarded", ModelNode.Billboarded);
			WriteBoolean(Node, "billboarded_lock_x", ModelNode.BillboardedLockX);
			WriteBoolean(Node, "billboarded_lock_y", ModelNode.BillboardedLockY);
			WriteBoolean(Node, "billboarded_lock_z", ModelNode.BillboardedLockZ);
			WriteBoolean(Node, "camera_anchored", ModelNode.CameraAnchored);
			WriteVector3(Node, "pivot_point", ModelNode.PivotPoint);

			WriteInteger(Node, "parent", ModelNode.Parent.NodeId);

			SaveAnimator(Saver, Node, Model, ModelNode.Translation, Value.CVector3.Instance, "translation");
			SaveAnimator(Saver, Node, Model, ModelNode.Rotation, Value.CVector4.Instance, "rotation");
			SaveAnimator(Saver, Node, Model, ModelNode.Scaling, Value.CVector3.Instance, "scaling");
		}
	}
}
