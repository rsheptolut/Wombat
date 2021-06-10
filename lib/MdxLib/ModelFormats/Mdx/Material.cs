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
	internal sealed class CMaterial : CObject
	{
		private CMaterial()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CMaterial Material = new Model.CMaterial(Model);
				Load(Loader, Model, Material);
				Model.Materials.Add(Material);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Material bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CMaterial Material)
		{
			Loader.PushLocation();

			int Size = Loader.ReadInt32();
			Material.PriorityPlane = Loader.ReadInt32();
			int Flags = Loader.ReadInt32();

			Material.ConstantColor = ((Flags & 1) != 0);
			Material.SortPrimitivesNearZ = ((Flags & 8) != 0);
			Material.SortPrimitivesFarZ = ((Flags & 16) != 0);
			Material.FullResolution = ((Flags & 32) != 0);

			Size -= Loader.PopLocation();
			if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many Material bytes were read!");

			Loader.ExpectTag("LAYS");
			int NrOfLayers = Loader.ReadInt32();

			for(int Index = 0; Index < NrOfLayers; Index++)
			{
				Model.CMaterialLayer Layer = new Model.CMaterialLayer(Model);
				LoadLayer(Loader, Model, Material, Layer);
				Material.Layers.Add(Layer);
			}
		}

		public void LoadLayer(CLoader Loader, Model.CModel Model, Model.CMaterial Material, Model.CMaterialLayer Layer)
		{
			Loader.PushLocation();

			int Size = Loader.ReadInt32();
			int FilterMode = Loader.ReadInt32();
			int Flags = Loader.ReadInt32();
			int TextureId = Loader.ReadInt32();

			Layer.TextureId.MakeStatic(TextureId);
			Loader.Attacher.AddObject(Model.Textures, Layer.Texture, TextureId);
			Loader.Attacher.AddObject(Model.TextureAnimations, Layer.TextureAnimation, Loader.ReadInt32());
			Layer.CoordId = Loader.ReadInt32();
			Layer.Alpha.MakeStatic(Loader.ReadFloat());

			switch(FilterMode)
			{
				case 0: { Layer.FilterMode = MdxLib.Model.EMaterialLayerFilterMode.None; break; }
				case 1: { Layer.FilterMode = MdxLib.Model.EMaterialLayerFilterMode.Transparent; break; }
				case 2: { Layer.FilterMode = MdxLib.Model.EMaterialLayerFilterMode.Blend; break; }
				case 3: { Layer.FilterMode = MdxLib.Model.EMaterialLayerFilterMode.Additive; break; }
				case 4: { Layer.FilterMode = MdxLib.Model.EMaterialLayerFilterMode.AdditiveAlpha; break; }
				case 5: { Layer.FilterMode = MdxLib.Model.EMaterialLayerFilterMode.Modulate; break; }
				case 6: { Layer.FilterMode = MdxLib.Model.EMaterialLayerFilterMode.Modulate2x; break; }
			}

			Layer.Unshaded = ((Flags & 1) != 0);
			Layer.SphereEnvironmentMap = ((Flags & 2) != 0);
			Layer.TwoSided = ((Flags & 16) != 0);
			Layer.Unfogged = ((Flags & 32) != 0);
			Layer.NoDepthTest = ((Flags & 64) != 0);
			Layer.NoDepthSet = ((Flags & 128) != 0);

			Size -= Loader.PopLocation();
			if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many MaterialLayer bytes were read!");

			while(Size > 0)
			{
				Loader.PushLocation();

				string Tag = Loader.ReadTag();

				switch(Tag)
				{
					case "KMTF": { LoadAnimator(Loader, Model, Layer.TextureId, Value.CInteger.Instance); break; }
					case "KMTA": { LoadAnimator(Loader, Model, Layer.Alpha, Value.CFloat.Instance); break; }

					default:
					{
						throw new System.Exception("Error at location " + Loader.Location + ", unknown MaterialLayer tag \"" + Tag + "\"!");
					}
				}

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many MaterialLayer bytes were read!");
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasMaterials)
			{
				Saver.WriteTag("MTLS");
				Saver.PushLocation();

				foreach(Model.CMaterial Material in Model.Materials)
				{
					Save(Saver, Model, Material);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CMaterial Material)
		{
			int Flags = 0;

			if(Material.ConstantColor) Flags |= 1;
			if(Material.SortPrimitivesNearZ) Flags |= 8;
			if(Material.SortPrimitivesFarZ) Flags |= 16;
			if(Material.FullResolution) Flags |= 32;

			Saver.PushLocation();

			Saver.WriteInt32(Material.PriorityPlane);
			Saver.WriteInt32(Flags);

			Saver.WriteTag("LAYS");
			Saver.WriteInt32(Material.Layers.Count);

			foreach(Model.CMaterialLayer Layer in Material.Layers)
			{
				SaveLayer(Saver, Model, Material, Layer);
			}

			Saver.PopInclusiveLocation();
		}

		public void SaveLayer(CSaver Saver, Model.CModel Model, Model.CMaterial Material, Model.CMaterialLayer Layer)
		{
			int Flags = 0;
			int FilterMode = 0;

			if(Layer.Unshaded) Flags |= 1;
			if(Layer.SphereEnvironmentMap) Flags |= 2;
			if(Layer.TwoSided) Flags |= 16;
			if(Layer.Unfogged) Flags |= 32;
			if(Layer.NoDepthTest) Flags |= 64;
			if(Layer.NoDepthSet) Flags |= 128;

			switch(Layer.FilterMode)
			{
				case MdxLib.Model.EMaterialLayerFilterMode.None: { FilterMode = 0; break; }
				case MdxLib.Model.EMaterialLayerFilterMode.Transparent: { FilterMode = 1; break; }
				case MdxLib.Model.EMaterialLayerFilterMode.Blend: { FilterMode = 2; break; }
				case MdxLib.Model.EMaterialLayerFilterMode.Additive: { FilterMode = 3; break; }
				case MdxLib.Model.EMaterialLayerFilterMode.AdditiveAlpha: { FilterMode = 4; break; }
				case MdxLib.Model.EMaterialLayerFilterMode.Modulate: { FilterMode = 5; break; }
				case MdxLib.Model.EMaterialLayerFilterMode.Modulate2x: { FilterMode = 6; break; }
			}

			Saver.PushLocation();

			Saver.WriteInt32(FilterMode);
			Saver.WriteInt32(Flags);

			Saver.WriteInt32(Layer.Texture.ObjectId);
			Saver.WriteInt32(Layer.TextureAnimation.ObjectId);
			Saver.WriteInt32(Layer.CoordId);
			Saver.WriteFloat(Layer.Alpha.GetValue());

			SaveAnimator(Saver, Model, Layer.TextureId, Value.CInteger.Instance, "KMTF");
			SaveAnimator(Saver, Model, Layer.Alpha, Value.CFloat.Instance, "KMTA");

			Saver.PopInclusiveLocation();
		}

		public static CMaterial Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CMaterial Instance = new CMaterial();
		}
	}
}
