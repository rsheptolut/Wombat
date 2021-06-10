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
	internal sealed class CMetaData : CObject
	{
		private CMetaData()
		{
			//Empty
		}

		public void Load(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			string MetaData = Loader.ReadString(Size);

			using(System.IO.StringReader Stream = new System.IO.StringReader(MetaData))
			{
				using(System.Xml.XmlTextReader Reader = new System.Xml.XmlTextReader(Stream))
				{
					System.Xml.XmlDocument Document = new System.Xml.XmlDocument();
					Document.Load(Reader);

					System.Xml.XmlNode MetaNode = Document.SelectSingleNode("meta");
					if((MetaNode != null) && (MetaNode.ChildNodes.Count > 0))
					{
						System.Xml.XmlNode ImportedNode = Model.MetaData.ImportNode(MetaNode, true);
						Model.MetaData.ReplaceChild(ImportedNode, Model.MetaData.DocumentElement);
					}
				}
			}
		}

		public void Save(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasMetaData)
			{
				Saver.WriteTag("META");
				Saver.PushLocation();

				using(System.IO.MemoryStream Stream = new System.IO.MemoryStream())
				{
					using(System.Xml.XmlTextWriter Writer = new System.Xml.XmlTextWriter(Stream, CConstants.SimpleTextEncoding))
					{
						Writer.Formatting = System.Xml.Formatting.None;
						Writer.WriteStartDocument();
						Model.MetaData.Save(Writer);
						Saver.Write(Stream.GetBuffer());
					}
				}

				Saver.PopExclusiveLocation();
			}
		}

		public static CMetaData Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CMetaData Instance = new CMetaData();
		}
	}
}
