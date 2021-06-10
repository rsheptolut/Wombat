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
namespace MdxLib.ModelFormats.Mdl
{
	internal sealed class CAttachment : CNode
	{
		private CAttachment()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			Model.CAttachment Attachment = new Model.CAttachment(Model);
			Load(Loader, Model, Attachment);
			Model.Attachments.Add(Attachment);
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CAttachment Attachment)
		{
			Attachment.Name = Loader.ReadString();
			Loader.ExpectToken(Token.EType.CurlyBracketLeft);

			while(true)
			{
				if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
				{
					Loader.ReadToken();
					break;
				}

				string Tag = Loader.ReadWord();

				if(!LoadNode(Loader, Model, Attachment, Tag))
				{
					switch(Tag)
					{
						case "static":
						{
							Tag = Loader.ReadWord();

							if(!LoadStaticNode(Loader, Model, Attachment, Tag))
							{
								switch(Tag)
								{
									case "visibility": { LoadStaticAnimator(Loader, Model, Attachment.Visibility, Value.CFloat.Instance); break; }

									default:
									{
										throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
									}
								}
							}

							break;
						}

						case "visibility": { LoadAnimator(Loader, Model, Attachment.Visibility, Value.CFloat.Instance); break; }
						
						case "attachmentid": { Attachment.AttachmentId = LoadInteger(Loader); break; }
						case "path": { Attachment.Path = LoadString(Loader); break; }

						default:
						{
							throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
						}
					}
				}
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasAttachments)
			{
				foreach(Model.CAttachment Attachment in Model.Attachments)
				{
					Save(Saver, Model, Attachment);
				}
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CAttachment Attachment)
		{
			Saver.BeginGroup("Attachment", Attachment.Name);

			SaveNode(Saver, Model, Attachment);

			SaveId(Saver, "AttachmentID", Attachment.AttachmentId, ECondition.NotInvalidId);
			SaveString(Saver, "Path", Attachment.Path, ECondition.NotEmpty);
			SaveAnimator(Saver, Model, Attachment.Visibility, Value.CFloat.Instance, "Visibility", ECondition.NotOne);

			Saver.EndGroup();
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
