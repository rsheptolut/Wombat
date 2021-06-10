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
	internal abstract class CObject : CUnknown
	{
		public CObject()
		{
			//Empty
		}

		public static void LoadAnimator<T>(CLoader Loader, Model.CModel Model, Animator.CAnimator<T> Animator, Value.IValue<T> ValueHandler) where T : new()
		{
			Animator.MakeAnimated();

			int NrOfTracks = Loader.ReadInt32();
			int InterpolationType = Loader.ReadInt32();
			Loader.Attacher.AddObject(Model.GlobalSequences, Animator.GlobalSequence, Loader.ReadInt32());

			switch(InterpolationType)
			{
				case 0: { Animator.Type = MdxLib.Animator.EInterpolationType.None; break; }
				case 1: { Animator.Type = MdxLib.Animator.EInterpolationType.Linear; break; }
				case 2: { Animator.Type = MdxLib.Animator.EInterpolationType.Hermite; break; }
				case 3: { Animator.Type = MdxLib.Animator.EInterpolationType.Bezier; break; }
			}

			for(int i = 0; i < NrOfTracks; i++)
			{
				int Time = Loader.ReadInt32();
				T Value = ValueHandler.Read(Loader);

				if(InterpolationType > 1)
				{
					T InTangent = ValueHandler.Read(Loader);
					T OutTangent = ValueHandler.Read(Loader);

					Animator.Add(new Animator.CAnimatorNode<T>(Time, Value, InTangent, OutTangent));
				}
				else
				{
					Animator.Add(new Animator.CAnimatorNode<T>(Time, Value));
				}
			}
		}

		public static void SaveAnimator<T>(CSaver Saver, Model.CModel Model, Animator.CAnimator<T> Animator, Value.IValue<T> ValueHandler, string Tag) where T : new()
		{
			int InterpolationType = 0;

			if(Animator.Static) return;

			switch(Animator.Type)
			{
				case MdxLib.Animator.EInterpolationType.None: { InterpolationType = 0; break; }
				case MdxLib.Animator.EInterpolationType.Linear: { InterpolationType = 1; break; }
				case MdxLib.Animator.EInterpolationType.Hermite: { InterpolationType = 2; break; }
				case MdxLib.Animator.EInterpolationType.Bezier: { InterpolationType = 3; break; }
			}

			Saver.WriteTag(Tag);
			Saver.WriteInt32(Animator.Count);
			Saver.WriteInt32(InterpolationType);
			Saver.WriteInt32(Animator.GlobalSequence.ObjectId);

			foreach(Animator.CAnimatorNode<T> Node in Animator)
			{
				Saver.WriteInt32(Node.Time);
				ValueHandler.Write(Saver, Node.Value);

				if(InterpolationType > 1)
				{
					ValueHandler.Write(Saver, Node.InTangent);
					ValueHandler.Write(Saver, Node.OutTangent);
				}
			}
		}
	}
}
