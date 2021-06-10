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
	internal sealed class CAttachment : CNode
	{
		private CAttachment()
		{
			//Empty
		}
		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CAttachment Attachment = new Model.CAttachment(Model);
				Load(Loader, Model, Attachment);
				Model.Attachments.Add(Attachment);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Attachment bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CAttachment Attachment)
		{
			Loader.PushLocation();

			int Size = Loader.ReadInt32();
			LoadNode(Loader, Model, Attachment);

			Attachment.Path = Loader.ReadString(CConstants.SizeFileName);
			Attachment.AttachmentId = Loader.ReadInt32();

			Size -= Loader.PopLocation();
			if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Attachment bytes were read!");

			while(Size > 0)
			{
				Loader.PushLocation();

				string Tag = Loader.ReadTag();

				switch(Tag)
				{
					case "KATV": { LoadAnimator(Loader, Model, Attachment.Visibility, Value.CFloat.Instance); break; }

					default:
					{
						throw new System.Exception("Error at location " + Loader.Location + ", unknown Attachment tag \"" + Tag + "\"!");
					}
				}

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Attachment bytes were read!");
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasAttachments)
			{
				Saver.WriteTag("ATCH");
				Saver.PushLocation();

				foreach(Model.CAttachment Attachment in Model.Attachments)
				{
					Save(Saver, Model, Attachment);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CAttachment Attachment)
		{
			Saver.PushLocation();
			SaveNode(Saver, Model, Attachment, 2048);

			Saver.WriteString(Attachment.Path, CConstants.SizeFileName);
			Saver.WriteInt32(Attachment.AttachmentId);

			SaveAnimator(Saver, Model, Attachment.Visibility, Value.CFloat.Instance, "KATV");

			Saver.PopInclusiveLocation();
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
