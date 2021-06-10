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
namespace MdxLib.ModelFormats.Attacher
{
	internal class CAttacherContainer : System.Collections.Generic.IEnumerable<IAttacher>
	{
		public CAttacherContainer()
		{
			AttacherList = new System.Collections.Generic.LinkedList<IAttacher>();
		}

		public void Clear()
		{
			AttacherList.Clear();
		}

		public void Add(IAttacher Attacher)
		{
			AttacherList.AddLast(Attacher);
		}

		public void AddObject<T>(Model.CObjectContainer<T> Container, Model.CObjectReference<T> Reference, int Id) where T : Model.CObject<T>
		{
			if(Id != CConstants.InvalidId)
			{
				Add(new Attacher.CObjectAttacher<T>(Container, Reference, Id));
			}
		}

		public void AddNode(Model.CModel Model, Model.CNodeReference Reference, int Id)
		{
			if(Id != CConstants.InvalidId)
			{
				Add(new Attacher.CNodeAttacher(Model, Reference, Id));
			}
		}

		public void Attach()
		{
			foreach(IAttacher Attacher in AttacherList)
			{
				Attacher.Attach();
			}
		}

		public System.Collections.Generic.IEnumerator<IAttacher> GetEnumerator()
		{
			return AttacherList.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return AttacherList.GetEnumerator();
		}

		private System.Collections.Generic.LinkedList<IAttacher> AttacherList = null;
	}
}
