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
	internal sealed class CBone : CNode
	{
		private CBone()
		{
			//Empty
		}

		public void LoadAll(CLoader Loader, Model.CModel Model)
		{
			Model.CBone Bone = new Model.CBone(Model);
			Load(Loader, Model, Bone);
			Model.Bones.Add(Bone);
		}

		public void Load(CLoader Loader, Model.CModel Model, Model.CBone Bone)
		{
			Bone.Name = Loader.ReadString();
			Loader.ExpectToken(Token.EType.CurlyBracketLeft);

			while(true)
			{
				if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
				{
					Loader.ReadToken();
					break;
				}

				string Tag = Loader.ReadWord();

				if(!LoadNode(Loader, Model, Bone, Tag))
				{
					switch(Tag)
					{
						case "static":
						{
							Tag = Loader.ReadWord();

							if(!LoadStaticNode(Loader, Model, Bone, Tag))
							{
								switch(Tag)
								{
									default:
									{
										throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
									}
								}
							}

							break;
						}

						case "geosetid": { Loader.Attacher.AddObject(Model.Geosets, Bone.Geoset, LoadId(Loader)); break; }
						case "geosetanimid": { Loader.Attacher.AddObject(Model.GeosetAnimations, Bone.GeosetAnimation, LoadId(Loader)); break; }

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
			if(Model.HasBones)
			{
				foreach(Model.CBone Bone in Model.Bones)
				{
					Save(Saver, Model, Bone);
				}
			}
		}

		public void Save(CSaver Saver, Model.CModel Model, Model.CBone Bone)
		{
			Saver.BeginGroup("Bone", Bone.Name);

			SaveNode(Saver, Model, Bone);

			SaveId(Saver, "GeosetId", Bone.Geoset.ObjectId, true);
			SaveId(Saver, "GeosetAnimId", Bone.GeosetAnimation.ObjectId, false);

			Saver.EndGroup();
		}

		public static CBone Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CBone Instance = new CBone();
		}
	}
}
