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
	internal sealed class CParticleEmitter2 : CNode
	{
		private CParticleEmitter2()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			int Size = Loader.ReadInt32();
			while(Size > 0)
			{
				Loader.PushLocation();

				Model.CParticleEmitter2 ParticleEmitter2 = new Model.CParticleEmitter2(Model);
				Load(Loader, Model, ParticleEmitter2);
				Model.ParticleEmitters2.Add(ParticleEmitter2);

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many ParticleEmitter2 bytes were read!");
			}
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CParticleEmitter2 ParticleEmitter2)
		{
			SSegment Segment1 = new SSegment();
			SSegment Segment2 = new SSegment();
			SSegment Segment3 = new SSegment();

			SInterval HeadLife = new SInterval();
			SInterval HeadDecay = new SInterval();
			SInterval TailLife = new SInterval();
			SInterval TailDecay = new SInterval();

			Loader.PushLocation();

			int Size = Loader.ReadInt32();
			int Flags = LoadNode(Loader, Model, ParticleEmitter2);

			ParticleEmitter2.Speed.MakeStatic(Loader.ReadFloat());
			ParticleEmitter2.Variation.MakeStatic(Loader.ReadFloat());
			ParticleEmitter2.Latitude.MakeStatic(Loader.ReadFloat());
			ParticleEmitter2.Gravity.MakeStatic(Loader.ReadFloat());
			ParticleEmitter2.LifeSpan = Loader.ReadFloat();
			ParticleEmitter2.EmissionRate.MakeStatic(Loader.ReadFloat());
			ParticleEmitter2.Length.MakeStatic(Loader.ReadFloat());
			ParticleEmitter2.Width.MakeStatic(Loader.ReadFloat());

			int FilterMode = Loader.ReadInt32();
			ParticleEmitter2.Rows = Loader.ReadInt32();
			ParticleEmitter2.Columns = Loader.ReadInt32();
			int HeadOrTail = Loader.ReadInt32();
			ParticleEmitter2.TailLength = Loader.ReadFloat();
			ParticleEmitter2.Time = Loader.ReadFloat();

			Segment1.Color = Loader.ReadVector3();
			Segment2.Color = Loader.ReadVector3();
			Segment3.Color = Loader.ReadVector3();
			Segment1.Alpha = (Loader.ReadInt8() / 255.0f);
			Segment2.Alpha = (Loader.ReadInt8() / 255.0f);
			Segment3.Alpha = (Loader.ReadInt8() / 255.0f);
			Segment1.Scaling = Loader.ReadFloat();
			Segment2.Scaling = Loader.ReadFloat();
			Segment3.Scaling = Loader.ReadFloat();
			
			HeadLife.Start = Loader.ReadInt32();
			HeadLife.End = Loader.ReadInt32();
			HeadLife.Repeat = Loader.ReadInt32();
			HeadDecay.Start = Loader.ReadInt32();
			HeadDecay.End = Loader.ReadInt32();
			HeadDecay.Repeat = Loader.ReadInt32();
			TailLife.Start = Loader.ReadInt32();
			TailLife.End = Loader.ReadInt32();
			TailLife.Repeat = Loader.ReadInt32();
			TailDecay.Start = Loader.ReadInt32();
			TailDecay.End = Loader.ReadInt32();
			TailDecay.Repeat = Loader.ReadInt32();

			ParticleEmitter2.Segment1 = new Primitives.CSegment(Segment1.Color, Segment1.Alpha, Segment1.Scaling);
			ParticleEmitter2.Segment2 = new Primitives.CSegment(Segment2.Color, Segment2.Alpha, Segment2.Scaling);
			ParticleEmitter2.Segment3 = new Primitives.CSegment(Segment3.Color, Segment3.Alpha, Segment3.Scaling);
			
			ParticleEmitter2.HeadLife = new Primitives.CInterval(HeadLife.Start, HeadLife.End, HeadLife.Repeat);
			ParticleEmitter2.HeadDecay = new Primitives.CInterval(HeadDecay.Start, HeadDecay.End, HeadDecay.Repeat);
			ParticleEmitter2.TailLife = new Primitives.CInterval(TailLife.Start, TailLife.End, TailLife.Repeat);
			ParticleEmitter2.TailDecay = new Primitives.CInterval(TailDecay.Start, TailDecay.End, TailDecay.Repeat);

			Loader.Attacher.AddObject(Model.Textures, ParticleEmitter2.Texture, Loader.ReadInt32());

			int Squirt = Loader.ReadInt32();
			ParticleEmitter2.PriorityPlane = Loader.ReadInt32();
			ParticleEmitter2.ReplaceableId = Loader.ReadInt32();

			switch(FilterMode)
			{
				case 0: { ParticleEmitter2.FilterMode = MdxLib.Model.EParticleEmitter2FilterMode.Blend; break; }
				case 1: { ParticleEmitter2.FilterMode = MdxLib.Model.EParticleEmitter2FilterMode.Additive; break; }
				case 2: { ParticleEmitter2.FilterMode = MdxLib.Model.EParticleEmitter2FilterMode.Modulate; break; }
				case 3: { ParticleEmitter2.FilterMode = MdxLib.Model.EParticleEmitter2FilterMode.Modulate2x; break; }
				case 4: { ParticleEmitter2.FilterMode = MdxLib.Model.EParticleEmitter2FilterMode.AlphaKey; break; }
			}

			ParticleEmitter2.Unshaded = ((Flags & 32768) != 0);
			ParticleEmitter2.SortPrimitivesFarZ = ((Flags & 65536) != 0);
			ParticleEmitter2.LineEmitter = ((Flags & 131072) != 0);
			ParticleEmitter2.Unfogged = ((Flags & 262144) != 0);
			ParticleEmitter2.ModelSpace = ((Flags & 524288) != 0);
			ParticleEmitter2.XYQuad = ((Flags & 1048576) != 0);

			ParticleEmitter2.Head = ((HeadOrTail == 0) || (HeadOrTail == 2));
			ParticleEmitter2.Tail = ((HeadOrTail == 1) || (HeadOrTail == 2));
			ParticleEmitter2.Squirt = (Squirt == 1);

			Size -= Loader.PopLocation();
			if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many ParticleEmitter2 bytes were read!");

			while(Size > 0)
			{
				Loader.PushLocation();

				string Tag = Loader.ReadTag();

				switch(Tag)
				{
					case "KP2S": { LoadAnimator(Loader, Model, ParticleEmitter2.Speed, Value.CFloat.Instance); break; }
					case "KP2R": { LoadAnimator(Loader, Model, ParticleEmitter2.Variation, Value.CFloat.Instance); break; }
					case "KP2L": { LoadAnimator(Loader, Model, ParticleEmitter2.Latitude, Value.CFloat.Instance); break; }
					case "KP2G": { LoadAnimator(Loader, Model, ParticleEmitter2.Gravity, Value.CFloat.Instance); break; }
					case "KP2E": { LoadAnimator(Loader, Model, ParticleEmitter2.EmissionRate, Value.CFloat.Instance); break; }
					case "KP2W": { LoadAnimator(Loader, Model, ParticleEmitter2.Width, Value.CFloat.Instance); break; }
					case "KP2N": { LoadAnimator(Loader, Model, ParticleEmitter2.Length, Value.CFloat.Instance); break; }
					case "KP2V": { LoadAnimator(Loader, Model, ParticleEmitter2.Visibility, Value.CFloat.Instance); break; }
					
					default:
					{
						throw new System.Exception("Error at location " + Loader.Location + ", unknown ParticleEmitter2 tag \"" + Tag + "\"!");
					}
				}

				Size -= Loader.PopLocation();
				if(Size < 0) throw new System.Exception("Error at location " + Loader.Location + ", too many ParticleEmitter2 bytes were read!");
			}
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasParticleEmitters2)
			{
				Saver.WriteTag("PRE2");
				Saver.PushLocation();

				foreach(Model.CParticleEmitter2 ParticleEmitter2 in Model.ParticleEmitters2)
				{
					Save(Saver, Model, ParticleEmitter2);
				}

				Saver.PopExclusiveLocation();
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CParticleEmitter2 ParticleEmitter2)
		{
			int Flags = 4096;
			int HeadOrTail = 0;
			int FilterMode = 0;

			if(ParticleEmitter2.Unshaded) Flags |= 32768;
			if(ParticleEmitter2.SortPrimitivesFarZ) Flags |= 65536;
			if(ParticleEmitter2.LineEmitter) Flags |= 131072;
			if(ParticleEmitter2.Unfogged) Flags |= 262144;
			if(ParticleEmitter2.ModelSpace) Flags |= 524288;
			if(ParticleEmitter2.XYQuad) Flags |= 1048576;

			if(ParticleEmitter2.Head)
			{
				if(ParticleEmitter2.Tail)
				{
					HeadOrTail = 2;
				}
				else
				{
					HeadOrTail = 0;
				}
			}
			else
			{
				if(ParticleEmitter2.Tail)
				{
					HeadOrTail = 1;
				}
				else
				{
					HeadOrTail = 0;
				}
			}

			switch(ParticleEmitter2.FilterMode)
			{
				case MdxLib.Model.EParticleEmitter2FilterMode.Blend: { FilterMode = 0; break; }
				case MdxLib.Model.EParticleEmitter2FilterMode.Additive: { FilterMode = 1; break; }
				case MdxLib.Model.EParticleEmitter2FilterMode.Modulate: { FilterMode = 2; break; }
				case MdxLib.Model.EParticleEmitter2FilterMode.Modulate2x: { FilterMode = 3; break; }
				case MdxLib.Model.EParticleEmitter2FilterMode.AlphaKey: { FilterMode = 4; break; }
			}

			Saver.PushLocation();
			SaveNode(Saver, Model, ParticleEmitter2, Flags);

			Saver.WriteFloat(ParticleEmitter2.Speed.GetValue());
			Saver.WriteFloat(ParticleEmitter2.Variation.GetValue());
			Saver.WriteFloat(ParticleEmitter2.Latitude.GetValue());
			Saver.WriteFloat(ParticleEmitter2.Gravity.GetValue());
			Saver.WriteFloat(ParticleEmitter2.LifeSpan);
			Saver.WriteFloat(ParticleEmitter2.EmissionRate.GetValue());
			Saver.WriteFloat(ParticleEmitter2.Length.GetValue());
			Saver.WriteFloat(ParticleEmitter2.Width.GetValue());

			Saver.WriteInt32(FilterMode);
			Saver.WriteInt32(ParticleEmitter2.Rows);
			Saver.WriteInt32(ParticleEmitter2.Columns);
			Saver.WriteInt32(HeadOrTail);
			Saver.WriteFloat(ParticleEmitter2.TailLength);
			Saver.WriteFloat(ParticleEmitter2.Time);

			Saver.WriteVector3(ParticleEmitter2.Segment1.Color);
			Saver.WriteVector3(ParticleEmitter2.Segment2.Color);
			Saver.WriteVector3(ParticleEmitter2.Segment3.Color);
			Saver.WriteInt8((int)(ParticleEmitter2.Segment1.Alpha * 255.0f));
			Saver.WriteInt8((int)(ParticleEmitter2.Segment2.Alpha * 255.0f));
			Saver.WriteInt8((int)(ParticleEmitter2.Segment3.Alpha * 255.0f));
			Saver.WriteFloat(ParticleEmitter2.Segment1.Scaling);
			Saver.WriteFloat(ParticleEmitter2.Segment2.Scaling);
			Saver.WriteFloat(ParticleEmitter2.Segment3.Scaling);

			Saver.WriteInt32(ParticleEmitter2.HeadLife.Start);
			Saver.WriteInt32(ParticleEmitter2.HeadLife.End);
			Saver.WriteInt32(ParticleEmitter2.HeadLife.Repeat);
			Saver.WriteInt32(ParticleEmitter2.HeadDecay.Start);
			Saver.WriteInt32(ParticleEmitter2.HeadDecay.End);
			Saver.WriteInt32(ParticleEmitter2.HeadDecay.Repeat);
			Saver.WriteInt32(ParticleEmitter2.TailLife.Start);
			Saver.WriteInt32(ParticleEmitter2.TailLife.End);
			Saver.WriteInt32(ParticleEmitter2.TailLife.Repeat);
			Saver.WriteInt32(ParticleEmitter2.TailDecay.Start);
			Saver.WriteInt32(ParticleEmitter2.TailDecay.End);
			Saver.WriteInt32(ParticleEmitter2.TailDecay.Repeat);

			Saver.WriteInt32(ParticleEmitter2.Texture.ObjectId);

			Saver.WriteInt32(ParticleEmitter2.Squirt ? 1 : 0);
			Saver.WriteInt32(ParticleEmitter2.PriorityPlane);
			Saver.WriteInt32(ParticleEmitter2.ReplaceableId);

			SaveAnimator(Saver, Model, ParticleEmitter2.Speed, Value.CFloat.Instance, "KP2S");
			SaveAnimator(Saver, Model, ParticleEmitter2.Variation, Value.CFloat.Instance, "KP2R");
			SaveAnimator(Saver, Model, ParticleEmitter2.Latitude, Value.CFloat.Instance, "KP2L");
			SaveAnimator(Saver, Model, ParticleEmitter2.Gravity, Value.CFloat.Instance, "KP2G");
			SaveAnimator(Saver, Model, ParticleEmitter2.EmissionRate, Value.CFloat.Instance, "KP2E");
			SaveAnimator(Saver, Model, ParticleEmitter2.Width, Value.CFloat.Instance, "KP2W");
			SaveAnimator(Saver, Model, ParticleEmitter2.Length, Value.CFloat.Instance, "KP2N");
			SaveAnimator(Saver, Model, ParticleEmitter2.Visibility, Value.CFloat.Instance, "KP2V");

			Saver.PopInclusiveLocation();
		}

		private struct SInterval
		{
			public int Start;
			public int End;
			public int Repeat;
		}

		private struct SSegment
		{
			public Primitives.CVector3 Color;
			public float Alpha;
			public float Scaling;
		}

		public static CParticleEmitter2 Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CParticleEmitter2 Instance = new CParticleEmitter2();
		}
	}
}
