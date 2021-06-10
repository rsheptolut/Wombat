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
	internal sealed class CParticleEmitter : CNode
	{
		private CParticleEmitter()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CParticleEmitter ParticleEmitter = new Model.CParticleEmitter(Model);
				Load(Loader, Model, ParticleEmitter);
				Model.ParticleEmitters.Add(ParticleEmitter);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many ParticleEmitter bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CParticleEmitter ParticleEmitter)
		{
			Loader.PushLocation();

			int Size = Loader.ReadInt32();
			int Flags = LoadNode(Loader, Model, ParticleEmitter);

			ParticleEmitter.EmissionRate.MakeStatic(Loader.ReadFloat());
			ParticleEmitter.Gravity.MakeStatic(Loader.ReadFloat());
			ParticleEmitter.Longitude.MakeStatic(Loader.ReadFloat());
			ParticleEmitter.Latitude.MakeStatic(Loader.ReadFloat());
			ParticleEmitter.FileName = Loader.ReadString(CConstants.SizeFileName);
			ParticleEmitter.LifeSpan.MakeStatic(Loader.ReadFloat());
			ParticleEmitter.InitialVelocity.MakeStatic(Loader.ReadFloat());

			ParticleEmitter.EmitterUsesMdl = ((Flags & 32768) != 0);
			ParticleEmitter.EmitterUsesTga = ((Flags & 65536) != 0);

			Size -= Loader.PopLocation();
			if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many ParticleEmitter bytes were read!");

			while(Size > 0)
			{
				Loader.PushLocation();

				string Tag = Loader.ReadTag();

				switch(Tag)
				{
					case "KPEE": { LoadAnimator(Loader, Model, ParticleEmitter.EmissionRate, Value.CFloat.Instance); break; }
					case "KPEG": { LoadAnimator(Loader, Model, ParticleEmitter.Gravity, Value.CFloat.Instance); break; }
					case "KPLN": { LoadAnimator(Loader, Model, ParticleEmitter.Longitude, Value.CFloat.Instance); break; }
					case "KPLT": { LoadAnimator(Loader, Model, ParticleEmitter.Latitude, Value.CFloat.Instance); break; }
					case "KPEL": { LoadAnimator(Loader, Model, ParticleEmitter.LifeSpan, Value.CFloat.Instance); break; }
					case "KPES": { LoadAnimator(Loader, Model, ParticleEmitter.InitialVelocity, Value.CFloat.Instance); break; }
					case "KPEV": { LoadAnimator(Loader, Model, ParticleEmitter.Visibility, Value.CFloat.Instance); break; }

					default:
					{
						throw new System.Exception("Error at location " + Loader.Location + ", unknown ParticleEmitter tag \"" + Tag + "\"!");
					}
				}

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many ParticleEmitter bytes were read!");
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasParticleEmitters)
			{
				Saver.WriteTag("PREM");
				Saver.PushLocation();

				foreach(Model.CParticleEmitter ParticleEmitter in Model.ParticleEmitters)
				{
					Save(Saver, Model, ParticleEmitter);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CParticleEmitter ParticleEmitter)
		{
			int Flags = 4096;

			if(ParticleEmitter.EmitterUsesMdl) Flags |= 32768;
			if(ParticleEmitter.EmitterUsesTga) Flags |= 65536;

			Saver.PushLocation();
			SaveNode(Saver, Model, ParticleEmitter, Flags);

			Saver.WriteFloat(ParticleEmitter.EmissionRate.GetValue());
			Saver.WriteFloat(ParticleEmitter.Gravity.GetValue());
			Saver.WriteFloat(ParticleEmitter.Longitude.GetValue());
			Saver.WriteFloat(ParticleEmitter.Latitude.GetValue());
			Saver.WriteString(ParticleEmitter.FileName, CConstants.SizeFileName);
			Saver.WriteFloat(ParticleEmitter.LifeSpan.GetValue());
			Saver.WriteFloat(ParticleEmitter.InitialVelocity.GetValue());

			SaveAnimator(Saver, Model, ParticleEmitter.EmissionRate, Value.CFloat.Instance, "KPEE");
			SaveAnimator(Saver, Model, ParticleEmitter.Gravity, Value.CFloat.Instance, "KPEG");
			SaveAnimator(Saver, Model, ParticleEmitter.Longitude, Value.CFloat.Instance, "KPLN");
			SaveAnimator(Saver, Model, ParticleEmitter.Latitude, Value.CFloat.Instance, "KPLT");
			SaveAnimator(Saver, Model, ParticleEmitter.LifeSpan, Value.CFloat.Instance, "KPEL");
			SaveAnimator(Saver, Model, ParticleEmitter.InitialVelocity, Value.CFloat.Instance, "KPES");
			SaveAnimator(Saver, Model, ParticleEmitter.Visibility, Value.CFloat.Instance, "KPEV");

			Saver.PopInclusiveLocation();
		}

		public static CParticleEmitter Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CParticleEmitter Instance = new CParticleEmitter();
		}
	}
}
