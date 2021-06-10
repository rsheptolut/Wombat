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
	internal abstract class CNode : CObject
	{
		public CNode()
		{
			//Empty
		}

		public static int LoadNode<T>(CLoader Loader, Model.CModel Model, Model.CNode<T> Node) where T : Model.CNode<T>
		{
			Loader.PushLocation();

			int Size = Loader.ReadInt32();
			Node.Name = Loader.ReadString(CConstants.SizeName);
			int NodeId = Loader.ReadInt32();
			Loader.Attacher.AddNode(Model, Node.Parent, Loader.ReadInt32());
			int Flags = Loader.ReadInt32();

			Node.DontInheritTranslation = ((Flags & 1) != 0);
			Node.DontInheritRotation = ((Flags & 2) != 0);
			Node.DontInheritScaling = ((Flags & 4) != 0);
			Node.Billboarded = ((Flags & 8) != 0);
			Node.BillboardedLockX = ((Flags & 16) != 0);
			Node.BillboardedLockY = ((Flags & 32) != 0);
			Node.BillboardedLockZ = ((Flags & 64) != 0);
			Node.CameraAnchored = ((Flags & 128) != 0);

			Size -= Loader.PopLocation();
			if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Node bytes were read!");

			while(Size > 0)
			{
				Loader.PushLocation();

				string Tag = Loader.ReadTag();

				switch(Tag)
				{
					case "KGTR": { LoadAnimator(Loader, Model, Node.Translation, Value.CVector3.Instance); break; }
					case "KGRT": { LoadAnimator(Loader, Model, Node.Rotation, Value.CVector4.Instance); break; }
					case "KGSC": { LoadAnimator(Loader, Model, Node.Scaling, Value.CVector3.Instance); break; }

					default:
					{
						throw new System.Exception("Error at location " + Loader.Location + ", unknown tag \"" + Tag + "\"!");
					}
				}

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Node bytes were read!");
			}

			return Flags;
		}

		public static void SaveNode<T>(CSaver Saver, Model.CModel Model, Model.CNode<T> Node, int Flags) where T : Model.CNode<T>
		{
			if(Node.DontInheritTranslation) Flags |= 1;
			if(Node.DontInheritRotation) Flags |= 2;
			if(Node.DontInheritScaling) Flags |= 4;
			if(Node.Billboarded) Flags |= 8;
			if(Node.BillboardedLockX) Flags |= 16;
			if(Node.BillboardedLockY) Flags |= 32;
			if(Node.BillboardedLockZ) Flags |= 64;
			if(Node.CameraAnchored) Flags |= 128;

			Saver.PushLocation();

			Saver.WriteString(Node.Name, CConstants.SizeName);
			Saver.WriteInt32(Node.NodeId);
			Saver.WriteInt32(Node.Parent.NodeId);
			Saver.WriteInt32(Flags);

			SaveAnimator(Saver, Model, Node.Translation, Value.CVector3.Instance, "KGTR");
			SaveAnimator(Saver, Model, Node.Rotation, Value.CVector4.Instance, "KGRT");
			SaveAnimator(Saver, Model, Node.Scaling, Value.CVector3.Instance, "KGSC");

			Saver.PopInclusiveLocation();
		}
	}
}
