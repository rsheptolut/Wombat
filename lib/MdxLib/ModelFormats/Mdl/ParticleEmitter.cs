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
	internal sealed class CParticleEmitter : CNode
	{
		private CParticleEmitter()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			Model.CParticleEmitter ParticleEmitter = new Model.CParticleEmitter(Model);
			Load(Loader, Model, ParticleEmitter);
			Model.ParticleEmitters.Add(ParticleEmitter);
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CParticleEmitter ParticleEmitter)
		{
			ParticleEmitter.Name = Loader.ReadString();
			Loader.ExpectToken(Token.EType.CurlyBracketLeft);

			while(true)
			{
				if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
				{
					Loader.ReadToken();
					break;
				}

				string Tag = Loader.ReadWord();

				if(!LoadNode(Loader, Model, ParticleEmitter, Tag))
				{
					switch(Tag)
					{
						case "static":
						{
							Tag = Loader.ReadWord();

							if(!LoadStaticNode(Loader, Model, ParticleEmitter, Tag))
							{
								switch(Tag)
								{
									case "emissionrate": { LoadStaticAnimator(Loader, Model, ParticleEmitter.EmissionRate, Value.CFloat.Instance); break; }
									case "gravity": { LoadStaticAnimator(Loader, Model, ParticleEmitter.Gravity, Value.CFloat.Instance); break; }
									case "longitude": { LoadStaticAnimator(Loader, Model, ParticleEmitter.Longitude, Value.CFloat.Instance); break; }
									case "latitude": { LoadStaticAnimator(Loader, Model, ParticleEmitter.Latitude, Value.CFloat.Instance); break; }
									case "visibility": { LoadStaticAnimator(Loader, Model, ParticleEmitter.Visibility, Value.CFloat.Instance); break; }

									default:
									{
										throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
									}
								}
							}

							break;
						}

						case "emissionrate": { LoadAnimator(Loader, Model, ParticleEmitter.EmissionRate, Value.CFloat.Instance); break; }
						case "gravity": { LoadAnimator(Loader, Model, ParticleEmitter.Gravity, Value.CFloat.Instance); break; }
						case "longitude": { LoadAnimator(Loader, Model, ParticleEmitter.Longitude, Value.CFloat.Instance); break; }
						case "latitude": { LoadAnimator(Loader, Model, ParticleEmitter.Latitude, Value.CFloat.Instance); break; }
						case "visibility": { LoadAnimator(Loader, Model, ParticleEmitter.Visibility, Value.CFloat.Instance); break; }

						case "emitterusesmdl": { ParticleEmitter.EmitterUsesMdl = LoadBoolean(Loader); break; }
						case "emitterusestga": { ParticleEmitter.EmitterUsesTga = LoadBoolean(Loader); break; }

						case "particle":
						{
							Loader.ExpectToken(Token.EType.CurlyBracketLeft);

							while(true)
							{
								if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
								{
									Loader.ReadToken();
									break;
								}

								Tag = Loader.ReadWord();

								switch(Tag)
								{
									case "static":
									{
										Tag = Loader.ReadWord();

										switch(Tag)
										{
											case "lifespan": { LoadStaticAnimator(Loader, Model, ParticleEmitter.LifeSpan, Value.CFloat.Instance); break; }
											case "initvelocity": { LoadStaticAnimator(Loader, Model, ParticleEmitter.InitialVelocity, Value.CFloat.Instance); break; }

											default:
											{
												throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
											}
										}

										break;
									}

									case "lifespan": { LoadAnimator(Loader, Model, ParticleEmitter.LifeSpan, Value.CFloat.Instance); break; }
									case "initvelocity": { LoadAnimator(Loader, Model, ParticleEmitter.InitialVelocity, Value.CFloat.Instance); break; }

									case "path": { ParticleEmitter.FileName = LoadString(Loader); break; }

									default:
									{
										throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
									}
								}
							}

							break;
						}

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
			if(Model.HasParticleEmitters)
			{
				foreach(Model.CParticleEmitter ParticleEmitter in Model.ParticleEmitters)
				{
					Save(Saver, Model, ParticleEmitter);
				}
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CParticleEmitter ParticleEmitter)
		{
			Saver.BeginGroup("ParticleEmitter", ParticleEmitter.Name);

			SaveNode(Saver, Model, ParticleEmitter);

			SaveBoolean(Saver, "EmitterUsesMDL", ParticleEmitter.EmitterUsesMdl);
			SaveBoolean(Saver, "EmitterUsesTGA", ParticleEmitter.EmitterUsesTga);

			SaveAnimator(Saver, Model, ParticleEmitter.EmissionRate, Value.CFloat.Instance, "EmissionRate");
			SaveAnimator(Saver, Model, ParticleEmitter.Gravity, Value.CFloat.Instance, "Gravity");
			SaveAnimator(Saver, Model, ParticleEmitter.Longitude, Value.CFloat.Instance, "Longitude");
			SaveAnimator(Saver, Model, ParticleEmitter.Latitude, Value.CFloat.Instance, "Latitude");
			SaveAnimator(Saver, Model, ParticleEmitter.Visibility, Value.CFloat.Instance, "Visibility", ECondition.NotOne);

			Saver.BeginGroup("Particle");

			SaveString(Saver, "Path", ParticleEmitter.FileName);
			SaveAnimator(Saver, Model, ParticleEmitter.LifeSpan, Value.CFloat.Instance, "LifeSpan");
			SaveAnimator(Saver, Model, ParticleEmitter.InitialVelocity, Value.CFloat.Instance, "InitVelocity");

			Saver.EndGroup();
			Saver.EndGroup();
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
