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
namespace MdxLib.Command
{
	internal sealed class CSetNodeField<T1, T2> : ICommand where T1 : Model.CNode<T1>
	{
		public CSetNodeField(T1 Object, string FieldName, T2 Value)
		{
			FieldInfo = typeof(Model.CNode<T1>).GetField(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			CurrentObject = Object;
			OldValue = (T2)FieldInfo.GetValue(CurrentObject);
			NewValue = Value;
		}

		public void Do()
		{
			FieldInfo.SetValue(CurrentObject, NewValue);
		}

		public void Undo()
		{
			FieldInfo.SetValue(CurrentObject, OldValue);
		}

		private T1 CurrentObject = null;
		private T2 OldValue = default(T2);
		private T2 NewValue = default(T2);
		private System.Reflection.FieldInfo FieldInfo = null;
	}
}
