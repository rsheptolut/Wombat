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
	internal sealed class CCamera : CObject
	{
		private CCamera()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			Model.CCamera Camera = new Model.CCamera(Model);
			Load(Loader, Model, Camera);
			Model.Cameras.Add(Camera);
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CCamera Camera)
		{
			Camera.Name = Loader.ReadString();
			Loader.ExpectToken(Token.EType.CurlyBracketLeft);

			while(true)
			{
				if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
				{
					Loader.ReadToken();
					break;
				}

				string Tag = Loader.ReadWord();

				switch(Tag)
				{
					case "static":
					{
						Tag = Loader.ReadWord();

						switch(Tag)
						{
							case "translation": { LoadStaticAnimator(Loader, Model, Camera.Translation, Value.CVector3.Instance); break; }
							case "rotation": { LoadStaticAnimator(Loader, Model, Camera.Rotation, Value.CFloat.Instance); break; }

							default:
							{
								throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
							}
						}

						break;
					}

					case "translation": { LoadAnimator(Loader, Model, Camera.Translation, Value.CVector3.Instance); break; }
					case "rotation": { LoadAnimator(Loader, Model, Camera.Rotation, Value.CFloat.Instance); break; }
					
					case "position": { Camera.Position = LoadVector3(Loader); break; }
					case "fieldofview": { Camera.FieldOfView = LoadFloat(Loader); break; }
					case "nearclip": { Camera.NearDistance = LoadFloat(Loader); break; }
					case "farclip": { Camera.FarDistance = LoadFloat(Loader); break; }

					case "target":
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
										case "translation": { LoadStaticAnimator(Loader, Model, Camera.TargetTranslation, Value.CVector3.Instance); break; }

										default:
										{
											throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
										}
									}

									break;
								}

								case "translation": { LoadAnimator(Loader, Model, Camera.TargetTranslation, Value.CVector3.Instance); break; }
								case "position": { Camera.TargetPosition = LoadVector3(Loader); break; }

								default:
								{
									throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
								}
							}

							break;
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

		public void SaveAll(CSaver Saver, Model.CModel Model)
		{
			if(Model.HasCameras)
			{
				foreach(Model.CCamera Camera in Model.Cameras)
				{
					Save(Saver, Model, Camera);
				}
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CCamera Camera)
		{
			Saver.BeginGroup("Camera", Camera.Name);

			SaveFloat(Saver, "FieldOfView", Camera.FieldOfView);
			SaveFloat(Saver, "FarClip", Camera.FarDistance);
			SaveFloat(Saver, "NearClip", Camera.NearDistance);
			SaveVector3(Saver, "Position", Camera.Position);
			SaveAnimator(Saver, Model, Camera.Translation, Value.CVector3.Instance, "Translation", ECondition.NotZero);

			Saver.BeginGroup("Target");

			SaveVector3(Saver, "Position", Camera.TargetPosition);
			SaveAnimator(Saver, Model, Camera.TargetTranslation, Value.CVector3.Instance, "Translation", ECondition.NotZero);
			SaveAnimator(Saver, Model, Camera.Rotation, Value.CFloat.Instance, "Rotation", ECondition.NotZero);

			Saver.EndGroup();
			Saver.EndGroup();
		}

		public static CCamera Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CCamera Instance = new CCamera();
		}
	}
}
