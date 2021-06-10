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
	internal sealed class CParticleEmitter2 : CNode
	{
		private CParticleEmitter2()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			Model.CParticleEmitter2 ParticleEmitter2 = new Model.CParticleEmitter2(Model);
			Load(Loader, Model, ParticleEmitter2);
			Model.ParticleEmitters2.Add(ParticleEmitter2);
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CParticleEmitter2 ParticleEmitter2)
		{
			SSegment Segment1 = new SSegment(CConstants.DefaultColor, 1.0f, 1.0f);
			SSegment Segment2 = new SSegment(CConstants.DefaultColor, 1.0f, 1.0f);
			SSegment Segment3 = new SSegment(CConstants.DefaultColor, 1.0f, 1.0f);

			ParticleEmitter2.Name = Loader.ReadString();
			Loader.ExpectToken(Token.EType.CurlyBracketLeft);

			while(true)
			{
				if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
				{
					Loader.ReadToken();
					break;
				}

				string Tag = Loader.ReadWord();

				if(!LoadNode(Loader, Model, ParticleEmitter2, Tag))
				{
					switch(Tag)
					{
						case "static":
						{
							Tag = Loader.ReadWord();

							if(!LoadStaticNode(Loader, Model, ParticleEmitter2, Tag))
							{
								switch(Tag)
								{
									case "speed": { LoadStaticAnimator(Loader, Model, ParticleEmitter2.Speed, Value.CFloat.Instance); break; }
									case "variation": { LoadStaticAnimator(Loader, Model, ParticleEmitter2.Variation, Value.CFloat.Instance); break; }
									case "latitude": { LoadStaticAnimator(Loader, Model, ParticleEmitter2.Latitude, Value.CFloat.Instance); break; }
									case "gravity": { LoadStaticAnimator(Loader, Model, ParticleEmitter2.Gravity, Value.CFloat.Instance); break; }
									case "visibility": { LoadStaticAnimator(Loader, Model, ParticleEmitter2.Visibility, Value.CFloat.Instance); break; }
									case "emissionrate": { LoadStaticAnimator(Loader, Model, ParticleEmitter2.EmissionRate, Value.CFloat.Instance); break; }
									case "width": { LoadStaticAnimator(Loader, Model, ParticleEmitter2.Width, Value.CFloat.Instance); break; }
									case "length": { LoadStaticAnimator(Loader, Model, ParticleEmitter2.Length, Value.CFloat.Instance); break; }

									default:
									{
										throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
									}
								}
							}

							break;
						}

						case "speed": { LoadAnimator(Loader, Model, ParticleEmitter2.Speed, Value.CFloat.Instance); break; }
						case "variation": { LoadAnimator(Loader, Model, ParticleEmitter2.Variation, Value.CFloat.Instance); break; }
						case "latitude": { LoadAnimator(Loader, Model, ParticleEmitter2.Latitude, Value.CFloat.Instance); break; }
						case "gravity": { LoadAnimator(Loader, Model, ParticleEmitter2.Gravity, Value.CFloat.Instance); break; }
						case "visibility": { LoadAnimator(Loader, Model, ParticleEmitter2.Visibility, Value.CFloat.Instance); break; }
						case "emissionrate": { LoadAnimator(Loader, Model, ParticleEmitter2.EmissionRate, Value.CFloat.Instance); break; }
						case "width": { LoadAnimator(Loader, Model, ParticleEmitter2.Width, Value.CFloat.Instance); break; }
						case "length": { LoadAnimator(Loader, Model, ParticleEmitter2.Length, Value.CFloat.Instance); break; }

						case "rows": { ParticleEmitter2.Rows = LoadInteger(Loader); break; }
						case "columns": { ParticleEmitter2.Columns = LoadInteger(Loader); break; }
						case "textureid": { Loader.Attacher.AddObject(Model.Textures, ParticleEmitter2.Texture, LoadId(Loader)); break; }
						case "replaceableid": { ParticleEmitter2.ReplaceableId = LoadInteger(Loader); break; }
						case "priorityplane": { ParticleEmitter2.PriorityPlane = LoadInteger(Loader); break; }
						case "time": { ParticleEmitter2.Time = LoadFloat(Loader); break; }
						case "lifespan": { ParticleEmitter2.LifeSpan = LoadFloat(Loader); break; }
						case "taillength": { ParticleEmitter2.TailLength = LoadFloat(Loader); break; }

						case "segmentcolor":
						{
							Loader.ExpectToken(Token.EType.CurlyBracketLeft);
							Loader.ExpectWord("color");
							Segment1.Color = Loader.ReadColor();
							Loader.ExpectToken(Token.EType.Separator);
							Loader.ExpectWord("color");
							Segment2.Color = Loader.ReadColor();
							Loader.ExpectToken(Token.EType.Separator);
							Loader.ExpectWord("color");
							Segment3.Color = Loader.ReadColor();
							Loader.ExpectToken(Token.EType.Separator);
							Loader.ExpectToken(Token.EType.CurlyBracketRight);
							Loader.ExpectToken(Token.EType.Separator);
							break;
						}

						case "alpha":
						{
							Primitives.CVector3 TempVector = LoadVector3(Loader);
							Segment1.Alpha = TempVector.X / 255.0f;
							Segment2.Alpha = TempVector.Y / 255.0f;
							Segment3.Alpha = TempVector.Z / 255.0f;
							break;
						}

						case "particlescaling":
						{
							Primitives.CVector3 TempVector = LoadVector3(Loader);
							Segment1.Scaling = TempVector.X;
							Segment2.Scaling = TempVector.Y;
							Segment3.Scaling = TempVector.Z;
							break;
						}

						case "lifespanuvanim":
						{
							Primitives.CVector3 TempVector = LoadVector3(Loader);
							ParticleEmitter2.HeadLife = new Primitives.CInterval((int)TempVector.X, (int)TempVector.Y, (int)TempVector.Z);
							break;
						}

						case "decayuvanim":
						{
							Primitives.CVector3 TempVector = LoadVector3(Loader);
							ParticleEmitter2.HeadDecay = new Primitives.CInterval((int)TempVector.X, (int)TempVector.Y, (int)TempVector.Z);
							break;
						}

						case "tailuvanim":
						{
							Primitives.CVector3 TempVector = LoadVector3(Loader);
							ParticleEmitter2.TailLife = new Primitives.CInterval((int)TempVector.X, (int)TempVector.Y, (int)TempVector.Z);
							break;
						}

						case "taildecayuvanim":
						{
							Primitives.CVector3 TempVector = LoadVector3(Loader);
							ParticleEmitter2.TailDecay = new Primitives.CInterval((int)TempVector.X, (int)TempVector.Y, (int)TempVector.Z);
							break;
						}

						case "sortprimsfarz": { ParticleEmitter2.SortPrimitivesFarZ = LoadBoolean(Loader); break; }
						case "lineemitter": { ParticleEmitter2.LineEmitter = LoadBoolean(Loader); break; }
						case "modelspace": { ParticleEmitter2.ModelSpace = LoadBoolean(Loader); break; }
						case "unshaded": { ParticleEmitter2.Unshaded = LoadBoolean(Loader); break; }
						case "unfogged": { ParticleEmitter2.Unfogged = LoadBoolean(Loader); break; }
						case "xyquad": { ParticleEmitter2.XYQuad = LoadBoolean(Loader); break; }
						case "squirt": { ParticleEmitter2.Squirt = LoadBoolean(Loader); break; }
						case "head": { ParticleEmitter2.Head = LoadBoolean(Loader); break; }
						case "tail": { ParticleEmitter2.Tail = LoadBoolean(Loader); break; }
						case "both": { ParticleEmitter2.Head = ParticleEmitter2.Tail = LoadBoolean(Loader); break; }
						case "blend": { ParticleEmitter2.FilterMode = MdxLib.Model.EParticleEmitter2FilterMode.Blend; LoadBoolean(Loader); break; }
						case "additive": { ParticleEmitter2.FilterMode = MdxLib.Model.EParticleEmitter2FilterMode.Additive; LoadBoolean(Loader); break; }
						case "modulate": { ParticleEmitter2.FilterMode = MdxLib.Model.EParticleEmitter2FilterMode.Modulate; LoadBoolean(Loader); break; }
						case "modulate2x": { ParticleEmitter2.FilterMode = MdxLib.Model.EParticleEmitter2FilterMode.Modulate2x; LoadBoolean(Loader); break; }
						case "alphakey": { ParticleEmitter2.FilterMode = MdxLib.Model.EParticleEmitter2FilterMode.AlphaKey; LoadBoolean(Loader); break; }

						default:
						{
							throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
						}
					}
				}
			}

			ParticleEmitter2.Segment1 = new Primitives.CSegment(Segment1.Color, Segment1.Alpha, Segment1.Scaling);
			ParticleEmitter2.Segment2 = new Primitives.CSegment(Segment2.Color, Segment2.Alpha, Segment2.Scaling);
			ParticleEmitter2.Segment3 = new Primitives.CSegment(Segment3.Color, Segment3.Alpha, Segment3.Scaling);
		}

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasParticleEmitters2)
			{
				foreach(Model.CParticleEmitter2 ParticleEmitter2 in Model.ParticleEmitters2)
				{
					Save(Saver, Model, ParticleEmitter2);
				}
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CParticleEmitter2 ParticleEmitter2)
		{
			Saver.BeginGroup("ParticleEmitter2", ParticleEmitter2.Name);

			SaveNode(Saver, Model, ParticleEmitter2);

			SaveBoolean(Saver, FilterModeToString(ParticleEmitter2.FilterMode), true);
			SaveBoolean(Saver, "SortPrimsFarZ", ParticleEmitter2.SortPrimitivesFarZ);
			SaveBoolean(Saver, "LineEmitter", ParticleEmitter2.LineEmitter);
			SaveBoolean(Saver, "ModelSpace", ParticleEmitter2.ModelSpace);
			SaveBoolean(Saver, "Unshaded", ParticleEmitter2.Unshaded);
			SaveBoolean(Saver, "Unfogged", ParticleEmitter2.Unfogged);
			SaveBoolean(Saver, "XYQuad", ParticleEmitter2.XYQuad);
			SaveBoolean(Saver, "Squirt", ParticleEmitter2.Squirt);
			SaveBoolean(Saver, "Head", (ParticleEmitter2.Head && !ParticleEmitter2.Tail));
			SaveBoolean(Saver, "Tail", (ParticleEmitter2.Tail && !ParticleEmitter2.Head));
			SaveBoolean(Saver, "Both", (ParticleEmitter2.Head && ParticleEmitter2.Tail));

			SaveInteger(Saver, "Rows", ParticleEmitter2.Rows, ECondition.NotZero);
			SaveInteger(Saver, "Columns", ParticleEmitter2.Columns, ECondition.NotZero);
			SaveId(Saver, "TextureID", ParticleEmitter2.Texture.ObjectId, ECondition.NotInvalidId);
			SaveInteger(Saver, "ReplaceableId", ParticleEmitter2.ReplaceableId, ECondition.NotZero);
			SaveInteger(Saver, "PriorityPlane", ParticleEmitter2.PriorityPlane, ECondition.NotZero);
			SaveFloat(Saver, "Time", ParticleEmitter2.Time, ECondition.NotZero);
			SaveFloat(Saver, "LifeSpan", ParticleEmitter2.LifeSpan, ECondition.NotZero);
			SaveFloat(Saver, "TailLength", ParticleEmitter2.TailLength, ECondition.NotZero);

			Saver.BeginGroup("SegmentColor");
			SaveColor(Saver, "Color", ParticleEmitter2.Segment1.Color);
			SaveColor(Saver, "Color", ParticleEmitter2.Segment2.Color);
			SaveColor(Saver, "Color", ParticleEmitter2.Segment3.Color);
			Saver.EndGroup(",");

			Saver.WriteTabs();
			Saver.WriteWord("Alpha { ");
			Saver.WriteInteger((int)(ParticleEmitter2.Segment1.Alpha * 255.0f));
			Saver.WriteWord(", ");
			Saver.WriteInteger((int)(ParticleEmitter2.Segment2.Alpha * 255.0f));
			Saver.WriteWord(", ");
			Saver.WriteInteger((int)(ParticleEmitter2.Segment3.Alpha * 255.0f));
			Saver.WriteLine(" },");

			SaveVector3(Saver, "ParticleScaling", new Primitives.CVector3(ParticleEmitter2.Segment1.Scaling, ParticleEmitter2.Segment2.Scaling, ParticleEmitter2.Segment3.Scaling));
			SaveVector3(Saver, "LifeSpanUVAnim", new Primitives.CVector3((float)ParticleEmitter2.HeadLife.Start, (float)ParticleEmitter2.HeadLife.End, (float)ParticleEmitter2.HeadLife.Repeat));
			SaveVector3(Saver, "DecayUVAnim", new Primitives.CVector3((float)ParticleEmitter2.HeadDecay.Start, (float)ParticleEmitter2.HeadDecay.End, (float)ParticleEmitter2.HeadDecay.Repeat));
			SaveVector3(Saver, "TailUVAnim", new Primitives.CVector3((float)ParticleEmitter2.TailLife.Start, (float)ParticleEmitter2.TailLife.End, (float)ParticleEmitter2.TailLife.Repeat));
			SaveVector3(Saver, "TailDecayUVAnim", new Primitives.CVector3((float)ParticleEmitter2.TailDecay.Start, (float)ParticleEmitter2.TailDecay.End, (float)ParticleEmitter2.TailDecay.Repeat));

			SaveAnimator(Saver, Model, ParticleEmitter2.Speed, Value.CFloat.Instance, "Speed");
			SaveAnimator(Saver, Model, ParticleEmitter2.Variation, Value.CFloat.Instance, "Variation");
			SaveAnimator(Saver, Model, ParticleEmitter2.Latitude, Value.CFloat.Instance, "Latitude");
			SaveAnimator(Saver, Model, ParticleEmitter2.Gravity, Value.CFloat.Instance, "Gravity");
			SaveAnimator(Saver, Model, ParticleEmitter2.EmissionRate, Value.CFloat.Instance, "EmissionRate");
			SaveAnimator(Saver, Model, ParticleEmitter2.Width, Value.CFloat.Instance, "Width");
			SaveAnimator(Saver, Model, ParticleEmitter2.Length, Value.CFloat.Instance, "Length");
			SaveAnimator(Saver, Model, ParticleEmitter2.Visibility, Value.CFloat.Instance, "Visibility", ECondition.NotOne);

			Saver.EndGroup();
		}

		private string FilterModeToString(Model.EParticleEmitter2FilterMode FilterMode)
		{
			switch(FilterMode)
			{
				case Model.EParticleEmitter2FilterMode.Blend: return "Blend";
				case Model.EParticleEmitter2FilterMode.Additive: return "Additive";
				case Model.EParticleEmitter2FilterMode.Modulate: return "Modulate";
				case Model.EParticleEmitter2FilterMode.Modulate2x: return "Modulate2x";
				case Model.EParticleEmitter2FilterMode.AlphaKey: return "AlphaKey";
			}

			return "";
		}

		private struct SSegment
		{
			public SSegment(Primitives.CVector3 Color, float Alpha, float Scaling)
			{
				this.Color = Color;
				this.Alpha = Alpha;
				this.Scaling = Scaling;
			}

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
