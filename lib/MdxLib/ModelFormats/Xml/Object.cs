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
	internal abstract class CObject : CUnknown
	{
		public CObject()
		{
			//Empty
		}

		public void LoadAnimator<T>(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model, Animator.CAnimator<T> Animator, Value.IValue<T> ValueHandler, string Name) where T : new()
		{
			System.Xml.XmlElement Element = Node.SelectSingleNode(Name) as System.Xml.XmlElement;
			if(Element == null) return;

			T DefaultValue = new T();
			bool Animated = Bool(Element.GetAttribute("animated"), false);

			if(Animated)
			{
				Animator.MakeAnimated();
				Animator.Type = StringToType(ReadString(Element, "type", TypeToString(Animator.Type)));
				Loader.Attacher.AddObject(Model.GlobalSequences, Animator.GlobalSequence, ReadInteger(Element, "global_sequence", CConstants.InvalidId));

				foreach(System.Xml.XmlNode ChildNode in Element.SelectNodes("node"))
				{
					int Time = ReadInteger(ChildNode, "time", 0);
					T Value = ValueHandler.Read(ChildNode, "value", Animator.GetValue());
					T InTangent = ValueHandler.Read(ChildNode, "in_tangent", DefaultValue);
					T OutTangent = ValueHandler.Read(ChildNode, "out_tangent", DefaultValue);

					MdxLib.Animator.CAnimatorNode<T> AnimatorNode = new MdxLib.Animator.CAnimatorNode<T>(Time, Value, InTangent, OutTangent);
					Animator.Add(AnimatorNode);
				}
			}
			else
			{
				T StaticValue = ValueHandler.Read(Element, "static", Animator.GetValue());
				Animator.MakeStatic(StaticValue);
			}
		}

		public void SaveAnimator<T>(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model, Animator.CAnimator<T> Animator, Value.IValue<T> ValueHandler, string Name) where T : new()
		{
			System.Xml.XmlElement Element = AppendElement(Node, Name);

			AppendAttribute(Element, "animated", Animator.Animated ? "1" : "0");

			if(Animator.Animated)
			{
				WriteString(Element, "type", TypeToString(Animator.Type));
				WriteInteger(Node, "global_sequence", Animator.GlobalSequence.ObjectId);

				foreach(MdxLib.Animator.CAnimatorNode<T> AnimatorNode in Animator)
				{
					System.Xml.XmlNode ChildNode = AppendElement(Element, "node");

					WriteInteger(ChildNode, "time", AnimatorNode.Time);
					ValueHandler.Write(ChildNode, "value", AnimatorNode.Value);
					ValueHandler.Write(ChildNode, "in_tangent", AnimatorNode.InTangent);
					ValueHandler.Write(ChildNode, "out_tangent", AnimatorNode.OutTangent);
				}
			}
			else
			{
				ValueHandler.Write(Element, "static", Animator.GetValue());
			}
		}

		private string TypeToString(Animator.EInterpolationType Type)
		{
			switch(Type)
			{
				case Animator.EInterpolationType.None: return "none";
				case Animator.EInterpolationType.Linear: return "linear";
				case Animator.EInterpolationType.Bezier: return "bezier";
				case Animator.EInterpolationType.Hermite: return "hermite";
			}

			return "";
		}

		private Animator.EInterpolationType StringToType(string String)
		{
			switch(String)
			{
				case "none": return Animator.EInterpolationType.None;
				case "linear": return Animator.EInterpolationType.Linear;
				case "bezier": return Animator.EInterpolationType.Bezier;
				case "hermite": return Animator.EInterpolationType.Hermite;
			}

			return Animator.EInterpolationType.None;
		}
	}
}
