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
namespace MdxLib.Command.ObjectContainer
{
	internal sealed class CSet<T> : ICommand where T : Model.CObject<T>
	{
		public CSet(Model.CObjectContainer<T> Container, int Index, T Object, System.Collections.Generic.IEnumerable<Model.CDetacher> DetacherList)
		{
			CurrentContainer = Container;
			CurrentIndex = Index;
			OldObject = CurrentContainer.InternalObjectList[CurrentIndex];
			NewObject = Object;
			CurrentDetacherList = DetacherList;
		}

		public void Do()
		{
			Model.CDetacher.DetachAllDetachers(CurrentDetacherList);

			OldObject.ObjectContainer = null;
			CurrentContainer.InternalObjectList[CurrentIndex] = NewObject;
			NewObject.ObjectContainer = CurrentContainer;
		}

		public void Undo()
		{
			NewObject.ObjectContainer = null;
			CurrentContainer.InternalObjectList[CurrentIndex] = OldObject;
			OldObject.ObjectContainer = CurrentContainer;

			Model.CDetacher.AttachAllDetachers(CurrentDetacherList);
		}

		private Model.CObjectContainer<T> CurrentContainer = null;
		private int CurrentIndex = CConstants.InvalidIndex;
		private T OldObject = null;
		private T NewObject = null;
		private System.Collections.Generic.IEnumerable<Model.CDetacher> CurrentDetacherList = null;
	}
}
