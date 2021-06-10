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
	internal sealed class CRibbonEmitter : CNode
	{
		private CRibbonEmitter()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CRibbonEmitter RibbonEmitter = new Model.CRibbonEmitter(Model);
				Load(Loader, Model, RibbonEmitter);
				Model.RibbonEmitters.Add(RibbonEmitter);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many RibbonEmitter bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CRibbonEmitter RibbonEmitter)
		{
			Loader.PushLocation();

			int Size = Loader.ReadInt32();
			LoadNode(Loader, Model, RibbonEmitter);

			RibbonEmitter.HeightAbove.MakeStatic(Loader.ReadFloat());
			RibbonEmitter.HeightBelow.MakeStatic(Loader.ReadFloat());
			RibbonEmitter.Alpha.MakeStatic(Loader.ReadFloat());
			RibbonEmitter.Color.MakeStatic(Loader.ReadVector3());
			RibbonEmitter.LifeSpan = Loader.ReadFloat();
			RibbonEmitter.TextureSlot.MakeStatic(Loader.ReadInt32());
			RibbonEmitter.EmissionRate = Loader.ReadInt32();
			RibbonEmitter.Rows = Loader.ReadInt32();
			RibbonEmitter.Columns = Loader.ReadInt32();
			Loader.Attacher.AddObject(Model.Materials, RibbonEmitter.Material, Loader.ReadInt32());
			RibbonEmitter.Gravity = Loader.ReadFloat();

			Size -= Loader.PopLocation();
			if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many RibbonEmitter bytes were read!");

			while(Size > 0)
			{
				Loader.PushLocation();

				string Tag = Loader.ReadTag();

				switch(Tag)
				{
					case "KRHA": { LoadAnimator(Loader, Model, RibbonEmitter.HeightAbove, Value.CFloat.Instance); break; }
					case "KRHB": { LoadAnimator(Loader, Model, RibbonEmitter.HeightBelow, Value.CFloat.Instance); break; }
					case "KRAL": { LoadAnimator(Loader, Model, RibbonEmitter.Alpha, Value.CFloat.Instance); break; }
					case "KRCO": { LoadAnimator(Loader, Model, RibbonEmitter.Color, Value.CVector3.Instance); break; }
					case "KRTX": { LoadAnimator(Loader, Model, RibbonEmitter.TextureSlot, Value.CInteger.Instance); break; }
					case "KRVS": { LoadAnimator(Loader, Model, RibbonEmitter.Visibility, Value.CFloat.Instance); break; }

					default:
					{
						throw new System.Exception("Error at location " + Loader.Location + ", unknown RibbonEmitter tag \"" + Tag + "\"!");
					}
				}

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many RibbonEmitter bytes were read!");
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasRibbonEmitters)
			{
				Saver.WriteTag("RIBB");
				Saver.PushLocation();

				foreach(Model.CRibbonEmitter RibbonEmitter in Model.RibbonEmitters)
				{
					Save(Saver, Model, RibbonEmitter);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CRibbonEmitter RibbonEmitter)
		{
			Saver.PushLocation();
			SaveNode(Saver, Model, RibbonEmitter, 16384);

			Saver.WriteFloat(RibbonEmitter.HeightAbove.GetValue());
			Saver.WriteFloat(RibbonEmitter.HeightBelow.GetValue());
			Saver.WriteFloat(RibbonEmitter.Alpha.GetValue());
			Saver.WriteVector3(RibbonEmitter.Color.GetValue());
			Saver.WriteFloat(RibbonEmitter.LifeSpan);
			Saver.WriteInt32(RibbonEmitter.TextureSlot.GetValue());
			Saver.WriteInt32(RibbonEmitter.EmissionRate);
			Saver.WriteInt32(RibbonEmitter.Rows);
			Saver.WriteInt32(RibbonEmitter.Columns);
			Saver.WriteInt32(RibbonEmitter.Material.ObjectId);
			Saver.WriteFloat(RibbonEmitter.Gravity);

			SaveAnimator(Saver, Model, RibbonEmitter.HeightAbove, Value.CFloat.Instance, "KRHA");
			SaveAnimator(Saver, Model, RibbonEmitter.HeightBelow, Value.CFloat.Instance, "KRHB");
			SaveAnimator(Saver, Model, RibbonEmitter.Alpha, Value.CFloat.Instance, "KRAL");
			SaveAnimator(Saver, Model, RibbonEmitter.Color, Value.CVector3.Instance, "KRCO");
			SaveAnimator(Saver, Model, RibbonEmitter.TextureSlot, Value.CInteger.Instance, "KRTX");
			SaveAnimator(Saver, Model, RibbonEmitter.Visibility, Value.CFloat.Instance, "KRVS");

			Saver.PopInclusiveLocation();
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
