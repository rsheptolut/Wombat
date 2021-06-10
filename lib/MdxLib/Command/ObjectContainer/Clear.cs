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
	internal sealed class CClear<T> : ICommand where T : Model.CObject<T>
	{
		public CClear(Model.CObjectContainer<T> Container, System.Collections.Generic.IEnumerable<Model.CDetacher> DetacherList)
		{
			CurrentContainer = Container;
			OldObjectList = CurrentContainer.InternalObjectList;
			NewObjectList = new System.Collections.Generic.List<T>();
			CurrentDetacherList = DetacherList;
		}

		public void Do()
		{
			Model.CDetacher.DetachAllDetachers(CurrentDetacherList);

			foreach(T Object in OldObjectList)
			{
				Object.ObjectContainer = null;
			}

			CurrentContainer.InternalObjectList = NewObjectList;
		}

		public void Undo()
		{
			CurrentContainer.InternalObjectList = OldObjectList;

			foreach(T Object in OldObjectList)
			{
				Object.ObjectContainer = CurrentContainer;
			}

			Model.CDetacher.AttachAllDetachers(CurrentDetacherList);
		}

		private Model.CObjectContainer<T> CurrentContainer = null;
		private System.Collections.Generic.List<T> OldObjectList = null;
		private System.Collections.Generic.List<T> NewObjectList = null;
		private System.Collections.Generic.IEnumerable<Model.CDetacher> CurrentDetacherList = null;
	}
}
