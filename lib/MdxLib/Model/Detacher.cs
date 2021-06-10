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
namespace MdxLib.Model
{
	internal abstract class CDetacher
	{
		public CDetacher()
		{
			//Empty
		}

		public abstract void Detach();
		public abstract void Attach();

		public static void DetachAllDetachers(System.Collections.Generic.IEnumerable<CDetacher> DetacherList)
		{
			foreach(CDetacher Detacher in DetacherList)
			{
				Detacher.Detach();
			}
		}

		public static void AttachAllDetachers(System.Collections.Generic.IEnumerable<CDetacher> DetacherList)
		{
			foreach(CDetacher Detacher in DetacherList)
			{
				Detacher.Attach();
			}
		}
	}

	internal sealed class CObjectDetacher<T> : CDetacher where T : CObject<T>
	{
		public CObjectDetacher(CObjectReference<T> ObjectReference)
		{
			Reference = ObjectReference;
			Object = Reference.Object;
		}

		public override void Detach()
		{
			if(Object != null) Reference.ForceDetach();
		}

		public override void Attach()
		{
			if(Object != null) Reference.ForceAttach(Object);
		}

		private T Object = null;
		private CObjectReference<T> Reference = null;
	}

	internal sealed class CNodeDetacher : CDetacher
	{
		public CNodeDetacher(CNodeReference NodeReference)
		{
			Reference = NodeReference;
			Node = Reference.Node;
		}

		public override void Detach()
		{
			if(Node != null) Reference.ForceDetach();
		}

		public override void Attach()
		{
			if(Node != null) Reference.ForceAttach(Node);
		}

		private INode Node = null;
		private CNodeReference Reference = null;
	}
}
