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
	internal sealed class CAttachment : CNode
	{
		private CAttachment()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CAttachment Attachment)
		{
			LoadNode(Loader, Node, Model, Attachment);

			Attachment.Path = ReadString(Node, "path", Attachment.Path);
			Attachment.AttachmentId = ReadInteger(Node, "attachment_id", Attachment.AttachmentId);

			LoadAnimator(Loader, Node, Model, Attachment.Visibility, Value.CFloat.Instance, "visibility");
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CAttachment Attachment)
		{
			SaveNode(Saver, Node, Model, Attachment);

			WriteString(Node, "path", Attachment.Path);
			WriteInteger(Node, "attachment_id", Attachment.AttachmentId);

			SaveAnimator(Saver, Node, Model, Attachment.Visibility, Value.CFloat.Instance, "visibility");
		}

		public static CAttachment Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CAttachment Instance = new CAttachment();
		}
	}
}
