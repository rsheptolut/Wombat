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
	internal abstract class CObject : CUnknown
	{
		public CObject()
		{
			//Empty
		}

		public void LoadAnimator<T>(CLoader Loader, Model.CModel Model, Animator.CAnimator<T> Animator, Value.IValue<T> ValueHandler) where T : new()
		{
			Animator.MakeAnimated();

			Loader.ReadInteger();
			Loader.ExpectToken(Token.EType.CurlyBracketLeft);

			while(Loader.PeekToken() == Token.EType.Word)
			{
				string Tag = Loader.ReadWord();

				switch(Tag)
				{
					case "dontinterp":
					{
						Animator.Type = MdxLib.Animator.EInterpolationType.None;
						Loader.ExpectToken(Token.EType.Separator);
						break;
					}

					case "linear":
					{
						Animator.Type = MdxLib.Animator.EInterpolationType.Linear;
						Loader.ExpectToken(Token.EType.Separator);
						break;
					}

					case "bezier":
					{
						Animator.Type = MdxLib.Animator.EInterpolationType.Bezier;
						Loader.ExpectToken(Token.EType.Separator);
						break;
					}

					case "hermite":
					{
						Animator.Type = MdxLib.Animator.EInterpolationType.Hermite;
						Loader.ExpectToken(Token.EType.Separator);
						break;
					}

					case "globalseqid":
					{
						Loader.Attacher.AddObject(Model.GlobalSequences, Animator.GlobalSequence, Loader.ReadInteger());
						Loader.ExpectToken(Token.EType.Separator);
						break;
					}

					default:
					{
						throw new System.Exception("Syntax error at line " + Loader.Line + ", unknown tag \"" + Tag + "\"!");
					}
				}
			}

			while(true)
			{
				if(Loader.PeekToken() == Token.EType.CurlyBracketRight)
				{
					Loader.ReadToken();
					break;
				}

				int Time = Loader.ReadInteger();
				Loader.ExpectToken(Token.EType.Colon);
				T Value = ValueHandler.Read(Loader);
				Loader.ExpectToken(Token.EType.Separator);

				switch(Animator.Type)
				{
					case MdxLib.Animator.EInterpolationType.None:
					case MdxLib.Animator.EInterpolationType.Linear:
					{
						Animator.Add(new MdxLib.Animator.CAnimatorNode<T>(Time, Value));
						break;
					}

					case MdxLib.Animator.EInterpolationType.Bezier:
					case MdxLib.Animator.EInterpolationType.Hermite:
					{
						Loader.ExpectWord("intan");
						T InTangent = ValueHandler.Read(Loader);
						Loader.ExpectToken(Token.EType.Separator);
						Loader.ExpectWord("outtan");
						T OutTangent = ValueHandler.Read(Loader);
						Loader.ExpectToken(Token.EType.Separator);
						Animator.Add(new MdxLib.Animator.CAnimatorNode<T>(Time, Value, InTangent, OutTangent));
						break;
					}
				}
			}
		}

		public void LoadStaticAnimator<T>(CLoader Loader, Model.CModel Model, Animator.CAnimator<T> Animator, Value.IValue<T> ValueHandler) where T : new()
		{
			Animator.MakeStatic(ValueHandler.Read(Loader));
			Loader.ExpectToken(Token.EType.Separator);
		}

		public void SaveAnimator<T>(CSaver Saver, Model.CModel Model, Animator.CAnimator<T> Animator, Value.IValue<T> ValueHandler, string Name) where T : new()
		{
			SaveAnimator(Saver, Model, Animator, ValueHandler, Name, ECondition.Always);
		}

		public void SaveAnimator<T>(CSaver Saver, Model.CModel Model, Animator.CAnimator<T> Animator, Value.IValue<T> ValueHandler, string Name, ECondition Condition) where T : new()
		{
			if(Animator.Static)
			{
				if(!ValueHandler.ValidCondition(Animator.GetValue(), Condition)) return;

				Saver.WriteTabs();
				Saver.WriteWord("static " + Name + " ");
				ValueHandler.Write(Saver, Animator.GetValue());
				Saver.WriteLine(",");
			}
			else
			{
				Saver.BeginGroup(Name, Animator.Count);
				SaveBoolean(Saver, TypeToString(Animator.Type), true);
				SaveId(Saver, "GlobalSeqId", Animator.GlobalSequence.ObjectId, ECondition.NotInvalidId);

				foreach(Animator.CAnimatorNode<T> Node in Animator)
				{
					Saver.WriteTabs();
					Saver.WriteInteger(Node.Time);
					Saver.WriteWord(": ");
					ValueHandler.Write(Saver, Node.Value);
					Saver.WriteLine(",");

					switch(Animator.Type)
					{
						case MdxLib.Animator.EInterpolationType.Bezier:
						case MdxLib.Animator.EInterpolationType.Hermite:
						{
							Saver.WriteTabs();
							Saver.WriteTabs(1);
							Saver.WriteWord("InTan ");
							ValueHandler.Write(Saver, Node.InTangent);
							Saver.WriteLine(",");

							Saver.WriteTabs();
							Saver.WriteTabs(1);
							Saver.WriteWord("OutTan ");
							ValueHandler.Write(Saver, Node.OutTangent);
							Saver.WriteLine(",");

							break;
						}
					}
				}

				Saver.EndGroup();
			}
		}

		private string TypeToString(Animator.EInterpolationType Type)
		{
			switch(Type)
			{
				case Animator.EInterpolationType.None: return "DontInterp";
				case Animator.EInterpolationType.Linear: return "Linear";
				case Animator.EInterpolationType.Bezier: return "Bezier";
				case Animator.EInterpolationType.Hermite: return "Hermite";
			}

			return "";
		}
	}
}
