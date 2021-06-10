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
	internal sealed class CParticleEmitter : CNode
	{
		private CParticleEmitter()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Model.CParticleEmitter ParticleEmitter)
		{
			LoadNode(Loader, Node, Model, ParticleEmitter);

			ParticleEmitter.FileName = ReadString(Node, "filename", ParticleEmitter.FileName);
			ParticleEmitter.EmitterUsesMdl = ReadBoolean(Node, "emitter_uses_mdl", ParticleEmitter.EmitterUsesMdl);
			ParticleEmitter.EmitterUsesTga = ReadBoolean(Node, "emitter_uses_tga", ParticleEmitter.EmitterUsesTga);

			LoadAnimator(Loader, Node, Model, ParticleEmitter.EmissionRate, Value.CFloat.Instance, "emission_rate");
			LoadAnimator(Loader, Node, Model, ParticleEmitter.Gravity, Value.CFloat.Instance, "gravity");
			LoadAnimator(Loader, Node, Model, ParticleEmitter.Longitude, Value.CFloat.Instance, "longitude");
			LoadAnimator(Loader, Node, Model, ParticleEmitter.Latitude, Value.CFloat.Instance, "latitude");
			LoadAnimator(Loader, Node, Model, ParticleEmitter.Visibility, Value.CFloat.Instance, "visibility");
			LoadAnimator(Loader, Node, Model, ParticleEmitter.LifeSpan, Value.CFloat.Instance, "life_span");
			LoadAnimator(Loader, Node, Model, ParticleEmitter.InitialVelocity, Value.CFloat.Instance, "initial_velocity");
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Model.CParticleEmitter ParticleEmitter)
		{
			SaveNode(Saver, Node, Model, ParticleEmitter);

			WriteString(Node, "filename", ParticleEmitter.FileName);
			WriteBoolean(Node, "emitter_uses_mdl", ParticleEmitter.EmitterUsesMdl);
			WriteBoolean(Node, "emitter_uses_tga", ParticleEmitter.EmitterUsesTga);

			SaveAnimator(Saver, Node, Model, ParticleEmitter.EmissionRate, Value.CFloat.Instance, "emission_rate");
			SaveAnimator(Saver, Node, Model, ParticleEmitter.Gravity, Value.CFloat.Instance, "gravity");
			SaveAnimator(Saver, Node, Model, ParticleEmitter.Longitude, Value.CFloat.Instance, "longitude");
			SaveAnimator(Saver, Node, Model, ParticleEmitter.Latitude, Value.CFloat.Instance, "latitude");
			SaveAnimator(Saver, Node, Model, ParticleEmitter.Visibility, Value.CFloat.Instance, "visibility");
			SaveAnimator(Saver, Node, Model, ParticleEmitter.LifeSpan, Value.CFloat.Instance, "life_span");
			SaveAnimator(Saver, Node, Model, ParticleEmitter.InitialVelocity, Value.CFloat.Instance, "initial_velocity");
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
