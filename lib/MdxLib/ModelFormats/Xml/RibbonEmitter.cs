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
	internal sealed class CRibbonEmitter : CNode
	{
		private CRibbonEmitter()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CRibbonEmitter RibbonEmitter)
		{
			LoadNode(Loader, Node, Model, RibbonEmitter);

			RibbonEmitter.EmissionRate = ReadInteger(Node, "emission_rate", RibbonEmitter.EmissionRate);
			RibbonEmitter.LifeSpan = ReadFloat(Node, "life_span", RibbonEmitter.LifeSpan);
			RibbonEmitter.Gravity = ReadFloat(Node, "gravity", RibbonEmitter.Gravity);
			RibbonEmitter.Rows = ReadInteger(Node, "rows", RibbonEmitter.Rows);
			RibbonEmitter.Columns = ReadInteger(Node, "columns", RibbonEmitter.Columns);

			LoadAnimator(Loader, Node, Model, RibbonEmitter.HeightAbove, Value.CFloat.Instance, "height_above");
			LoadAnimator(Loader, Node, Model, RibbonEmitter.HeightBelow, Value.CFloat.Instance, "height_below");
			LoadAnimator(Loader, Node, Model, RibbonEmitter.Alpha, Value.CFloat.Instance, "alpha");
			LoadAnimator(Loader, Node, Model, RibbonEmitter.Color, Value.CVector3.Instance, "color");
			LoadAnimator(Loader, Node, Model, RibbonEmitter.TextureSlot, Value.CInteger.Instance, "texture_slot");
			LoadAnimator(Loader, Node, Model, RibbonEmitter.Visibility, Value.CFloat.Instance, "visibility");

			Loader.Attacher.AddObject(Model.Materials, RibbonEmitter.Material, ReadInteger(Node, "material", CConstants.InvalidId));
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CRibbonEmitter RibbonEmitter)
		{
			SaveNode(Saver, Node, Model, RibbonEmitter);

			WriteInteger(Node, "emission_rate", RibbonEmitter.EmissionRate);
			WriteFloat(Node, "life_span", RibbonEmitter.LifeSpan);
			WriteFloat(Node, "gravity", RibbonEmitter.Gravity);
			WriteInteger(Node, "rows", RibbonEmitter.Rows);
			WriteInteger(Node, "columns", RibbonEmitter.Columns);

			SaveAnimator(Saver, Node, Model, RibbonEmitter.HeightAbove, Value.CFloat.Instance, "height_above");
			SaveAnimator(Saver, Node, Model, RibbonEmitter.HeightBelow, Value.CFloat.Instance, "height_below");
			SaveAnimator(Saver, Node, Model, RibbonEmitter.Alpha, Value.CFloat.Instance, "alpha");
			SaveAnimator(Saver, Node, Model, RibbonEmitter.Color, Value.CVector3.Instance, "color");
			SaveAnimator(Saver, Node, Model, RibbonEmitter.TextureSlot, Value.CInteger.Instance, "texture_slot");
			SaveAnimator(Saver, Node, Model, RibbonEmitter.Visibility, Value.CFloat.Instance, "visibility");

			WriteInteger(Node, "material", RibbonEmitter.Material.ObjectId);
		}

		public static CRibbonEmitter Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CRibbonEmitter Instance = new CRibbonEmitter();
		}
	}
}
