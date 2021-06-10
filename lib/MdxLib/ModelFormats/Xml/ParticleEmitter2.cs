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
	internal sealed class CParticleEmitter2 : CNode
	{
		private CParticleEmitter2()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CParticleEmitter2 ParticleEmitter2)
		{
			LoadNode(Loader, Node, Model, ParticleEmitter2);

			ParticleEmitter2.FilterMode = StringToFilterMode(ReadString(Node, "filter_mode", FilterModeToString(ParticleEmitter2.FilterMode)));
			ParticleEmitter2.Rows = ReadInteger(Node, "rows", ParticleEmitter2.Rows);
			ParticleEmitter2.Columns = ReadInteger(Node, "columns", ParticleEmitter2.Columns);
			ParticleEmitter2.PriorityPlane = ReadInteger(Node, "priority_plane", ParticleEmitter2.PriorityPlane);
			ParticleEmitter2.ReplaceableId = ReadInteger(Node, "replaceable_id", ParticleEmitter2.ReplaceableId);
			ParticleEmitter2.Time = ReadFloat(Node, "time", ParticleEmitter2.Time);
			ParticleEmitter2.LifeSpan = ReadFloat(Node, "life_span", ParticleEmitter2.LifeSpan);
			ParticleEmitter2.TailLength = ReadFloat(Node, "tail_length", ParticleEmitter2.TailLength);
			ParticleEmitter2.SortPrimitivesFarZ = ReadBoolean(Node, "sort_primitives_far_z", ParticleEmitter2.SortPrimitivesFarZ);
			ParticleEmitter2.LineEmitter = ReadBoolean(Node, "line_emitter", ParticleEmitter2.LineEmitter);
			ParticleEmitter2.ModelSpace = ReadBoolean(Node, "model_space", ParticleEmitter2.ModelSpace);
			ParticleEmitter2.Unshaded = ReadBoolean(Node, "unshaded", ParticleEmitter2.Unshaded);
			ParticleEmitter2.Unfogged = ReadBoolean(Node, "unfogged", ParticleEmitter2.Unfogged);
			ParticleEmitter2.XYQuad = ReadBoolean(Node, "xy_quad", ParticleEmitter2.XYQuad);
			ParticleEmitter2.Squirt = ReadBoolean(Node, "squirt", ParticleEmitter2.Squirt);
			ParticleEmitter2.Head = ReadBoolean(Node, "head", ParticleEmitter2.Head);
			ParticleEmitter2.Tail = ReadBoolean(Node, "tail", ParticleEmitter2.Tail);
			ParticleEmitter2.Segment1 = ReadSegment(Node, "segment_1", ParticleEmitter2.Segment1);
			ParticleEmitter2.Segment2 = ReadSegment(Node, "segment_2", ParticleEmitter2.Segment2);
			ParticleEmitter2.Segment3 = ReadSegment(Node, "segment_3", ParticleEmitter2.Segment3);
			ParticleEmitter2.HeadLife = ReadInterval(Node, "head_life", ParticleEmitter2.HeadLife);
			ParticleEmitter2.HeadDecay = ReadInterval(Node, "head_decay", ParticleEmitter2.HeadDecay);
			ParticleEmitter2.TailLife = ReadInterval(Node, "tail_life", ParticleEmitter2.TailLife);
			ParticleEmitter2.TailDecay = ReadInterval(Node, "tail_decay", ParticleEmitter2.TailDecay);

			LoadAnimator(Loader, Node, Model, ParticleEmitter2.Speed, Value.CFloat.Instance, "speed");
			LoadAnimator(Loader, Node, Model, ParticleEmitter2.Variation, Value.CFloat.Instance, "variation");
			LoadAnimator(Loader, Node, Model, ParticleEmitter2.Latitude, Value.CFloat.Instance, "latitude");
			LoadAnimator(Loader, Node, Model, ParticleEmitter2.Gravity, Value.CFloat.Instance, "gravity");
			LoadAnimator(Loader, Node, Model, ParticleEmitter2.Visibility, Value.CFloat.Instance, "visibility");
			LoadAnimator(Loader, Node, Model, ParticleEmitter2.EmissionRate, Value.CFloat.Instance, "emission_rate");
			LoadAnimator(Loader, Node, Model, ParticleEmitter2.Width, Value.CFloat.Instance, "width");
			LoadAnimator(Loader, Node, Model, ParticleEmitter2.Length, Value.CFloat.Instance, "length");

			Loader.Attacher.AddObject(Model.Textures, ParticleEmitter2.Texture, ReadInteger(Node, "texture", CConstants.InvalidId));
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CParticleEmitter2 ParticleEmitter2)
		{
			SaveNode(Saver, Node, Model, ParticleEmitter2);

			WriteString(Node, "filter_mode", FilterModeToString(ParticleEmitter2.FilterMode));
			WriteInteger(Node, "rows", ParticleEmitter2.Rows);
			WriteInteger(Node, "columns", ParticleEmitter2.Columns);
			WriteInteger(Node, "priority_plane", ParticleEmitter2.PriorityPlane);
			WriteInteger(Node, "replaceable_id", ParticleEmitter2.ReplaceableId);
			WriteFloat(Node, "time", ParticleEmitter2.Time);
			WriteFloat(Node, "life_span", ParticleEmitter2.LifeSpan);
			WriteFloat(Node, "tail_length", ParticleEmitter2.TailLength);
			WriteBoolean(Node, "sort_primitives_far_z", ParticleEmitter2.SortPrimitivesFarZ);
			WriteBoolean(Node, "line_emitter", ParticleEmitter2.LineEmitter);
			WriteBoolean(Node, "model_space", ParticleEmitter2.ModelSpace);
			WriteBoolean(Node, "unshaded", ParticleEmitter2.Unshaded);
			WriteBoolean(Node, "unfogged", ParticleEmitter2.Unfogged);
			WriteBoolean(Node, "xy_quad", ParticleEmitter2.XYQuad);
			WriteBoolean(Node, "squirt", ParticleEmitter2.Squirt);
			WriteBoolean(Node, "head", ParticleEmitter2.Head);
			WriteBoolean(Node, "tail", ParticleEmitter2.Tail);
			WriteSegment(Node, "segment_1", ParticleEmitter2.Segment1);
			WriteSegment(Node, "segment_2", ParticleEmitter2.Segment2);
			WriteSegment(Node, "segment_3", ParticleEmitter2.Segment3);
			WriteInterval(Node, "head_life", ParticleEmitter2.HeadLife);
			WriteInterval(Node, "head_decay", ParticleEmitter2.HeadDecay);
			WriteInterval(Node, "tail_life", ParticleEmitter2.TailLife);
			WriteInterval(Node, "tail_decay", ParticleEmitter2.TailDecay);

			SaveAnimator(Saver, Node, Model, ParticleEmitter2.Speed, Value.CFloat.Instance, "speed");
			SaveAnimator(Saver, Node, Model, ParticleEmitter2.Variation, Value.CFloat.Instance, "variation");
			SaveAnimator(Saver, Node, Model, ParticleEmitter2.Latitude, Value.CFloat.Instance, "latitude");
			SaveAnimator(Saver, Node, Model, ParticleEmitter2.Gravity, Value.CFloat.Instance, "gravity");
			SaveAnimator(Saver, Node, Model, ParticleEmitter2.Visibility, Value.CFloat.Instance, "visibility");
			SaveAnimator(Saver, Node, Model, ParticleEmitter2.EmissionRate, Value.CFloat.Instance, "emission_rate");
			SaveAnimator(Saver, Node, Model, ParticleEmitter2.Width, Value.CFloat.Instance, "width");
			SaveAnimator(Saver, Node, Model, ParticleEmitter2.Length, Value.CFloat.Instance, "length");

			WriteInteger(Node, "texture", ParticleEmitter2.Texture.ObjectId);
		}

		private string FilterModeToString(Model.EParticleEmitter2FilterMode FilterMode)
		{
			switch(FilterMode)
			{
				case Model.EParticleEmitter2FilterMode.Blend: return "blend";
				case Model.EParticleEmitter2FilterMode.Additive: return "additive";
				case Model.EParticleEmitter2FilterMode.Modulate: return "modulate";
				case Model.EParticleEmitter2FilterMode.Modulate2x: return "modulate_2x";
				case Model.EParticleEmitter2FilterMode.AlphaKey: return "alpha_key";
			}

			return "";
		}

		private Model.EParticleEmitter2FilterMode StringToFilterMode(string String)
		{
			switch(String)
			{
				case "blend": return Model.EParticleEmitter2FilterMode.Blend;
				case "additive": return Model.EParticleEmitter2FilterMode.Additive;
				case "modulate": return Model.EParticleEmitter2FilterMode.Modulate;
				case "modulate_2x": return Model.EParticleEmitter2FilterMode.Modulate2x;
				case "alpha_key": return Model.EParticleEmitter2FilterMode.AlphaKey;
			}

			return Model.EParticleEmitter2FilterMode.Blend;
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
