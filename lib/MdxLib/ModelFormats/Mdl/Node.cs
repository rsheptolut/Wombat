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
	internal abstract class CNode : CObject
	{
		public CNode()
		{
			//Empty
		}

		public bool LoadNode<T>(CLoader Loader, Model.CModel Model, Model.CNode<T> Node, string Tag) where T : Model.CNode<T>
		{
			switch(Tag)
			{
				case "translation": { LoadAnimator(Loader, Model, Node.Translation, Value.CVector3.Instance); return true; }
				case "rotation": { LoadAnimator(Loader, Model, Node.Rotation, Value.CVector4.Instance); return true; }
				case "scaling": { LoadAnimator(Loader, Model, Node.Scaling, Value.CVector3.Instance); return true; }
				
				case "objectid": { LoadInteger(Loader); return true; }
				case "parent": { Loader.Attacher.AddNode(Model, Node.Parent, LoadId(Loader)); return true; }
				case "billboarded": { Node.Billboarded = LoadBoolean(Loader); return true; }
				case "billboardedlockx": { Node.BillboardedLockX = LoadBoolean(Loader); return true; }
				case "billboardedlocky": { Node.BillboardedLockY = LoadBoolean(Loader); return true; }
				case "billboardedlockz": { Node.BillboardedLockZ = LoadBoolean(Loader); return true; }
				case "cameraanchored": { Node.CameraAnchored = LoadBoolean(Loader); return true; }

				case "dontinherit":
				{
					Loader.ExpectToken(Token.EType.CurlyBracketLeft);

					while(true)
					{
						if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
						{
							Loader.ReadToken();
							Loader.ExpectToken(Token.EType.Separator);
							break;
						}

						if(Loader.PeekToken() == Token.EType.Separator)
						{
							Loader.ReadToken();
							continue;
						}

						Tag = Loader.ReadWord();

						switch(Tag)
						{
							case "translation": { Node.DontInheritTranslation = true; break; }
							case "rotation": { Node.DontInheritRotation = true; break; }
							case "scaling": { Node.DontInheritScaling = true; break; }

							default:
							{
								throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
							}
						}
					}

					return true;
				}
			}

			return false;
		}

		public bool LoadStaticNode<T>(CLoader Loader, Model.CModel Model, Model.CNode<T> Node, string Tag) where T : Model.CNode<T>
		{
			switch(Tag)
			{
				case "translation": { LoadStaticAnimator(Loader, Model, Node.Translation, Value.CVector3.Instance); return true; }
				case "rotation": { LoadStaticAnimator(Loader, Model, Node.Rotation, Value.CVector4.Instance); return true; }
				case "scaling": { LoadStaticAnimator(Loader, Model, Node.Scaling, Value.CVector3.Instance); return true; }
			}

			return false;
		}

		public void SaveNode<T>(CSaver Saver, Model.CModel Model, Model.CNode<T> Node) where T : Model.CNode<T>
		{
			int DontInheritCounter = 0;

			SaveId(Saver, "ObjectId", Node.NodeId, ECondition.NotInvalidId);
			SaveId(Saver, "Parent", Node.Parent.NodeId, ECondition.NotInvalidId);

			if(Node.DontInheritTranslation) DontInheritCounter++;
			if(Node.DontInheritRotation) DontInheritCounter++;
			if(Node.DontInheritScaling) DontInheritCounter++;

			if(DontInheritCounter > 0)
			{
				Saver.WriteTabs();
				Saver.WriteWord("DontInherit { ");

				if(Node.DontInheritTranslation)
				{
					DontInheritCounter--;
					Saver.WriteWord("Translation");
					Saver.WriteWord((DontInheritCounter > 0) ? ", " : "");
				}

				if(Node.DontInheritRotation)
				{
					DontInheritCounter--;
					Saver.WriteWord("Rotation");
					Saver.WriteWord((DontInheritCounter > 0) ? ", " : "");
				}

				if(Node.DontInheritScaling)
				{
					DontInheritCounter--;
					Saver.WriteWord("Scaling");
					Saver.WriteWord((DontInheritCounter > 0) ? ", " : "");
				}

				Saver.WriteLine(" },");
			}

			SaveBoolean(Saver, "Billboarded", Node.Billboarded);
			SaveBoolean(Saver, "BillboardedLockX", Node.BillboardedLockX);
			SaveBoolean(Saver, "BillboardedLockY", Node.BillboardedLockY);
			SaveBoolean(Saver, "BillboardedLockZ", Node.BillboardedLockZ);
			SaveBoolean(Saver, "CameraAnchored", Node.CameraAnchored);

			SaveAnimator(Saver, Model, Node.Translation, Value.CVector3.Instance, "Translation", ECondition.NotZero);
			SaveAnimator(Saver, Model, Node.Rotation, Value.CVector4.Instance, "Rotation", ECondition.NotDefaultQuaternion);
			SaveAnimator(Saver, Model, Node.Scaling, Value.CVector3.Instance, "Scaling", ECondition.NotOne);
		}
	}
}
