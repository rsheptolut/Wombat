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
namespace MdxLib.Animator
{
	/// <summary>
	/// A node for the animator. Animator values are interpolated
	/// between these nodes.
	/// </summary>
	/// <typeparam name="T">The value type</typeparam>
	public sealed class CAnimatorNode<T> where T : new()
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CAnimatorNode()
		{
			_Value = new T();
			_InTangent = new T();
			_OutTangent = new T();
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="Node">The node to copy from</param>
		public CAnimatorNode(CAnimatorNode<T> Node)
		{
			_Time = Node._Time;
			_Value = Node._Value;
			_InTangent = Node._InTangent;
			_OutTangent = Node._OutTangent;
		}

		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Time">The time to use</param>
		/// <param name="Value">The value to use</param>
		public CAnimatorNode(int Time, T Value)
		{
			_Time = Time;
			_Value = Value;
			_InTangent = new T();
			_OutTangent = new T();
		}

		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Time">The time to use</param>
		/// <param name="Value">The value to use</param>
		/// <param name="InTangent">The in tangent to use</param>
		/// <param name="OutTangent">The out tangent to use</param>
		public CAnimatorNode(int Time, T Value, T InTangent, T OutTangent)
		{
			_Time = Time;
			_Value = Value;
			_InTangent = InTangent;
			_OutTangent = OutTangent;
		}

		/// <summary>
		/// Retrieves the time.
		/// </summary>
		public int Time
		{
			get
			{
				return _Time;
			}
		}

		/// <summary>
		/// Retrieves the value.
		/// </summary>
		public T Value
		{
			get
			{
				return _Value;
			}
		}

		/// <summary>
		/// Retrieves the incoming tangent.
		/// </summary>
		public T InTangent
		{
			get
			{
				return _InTangent;
			}
		}

		/// <summary>
		/// Retrieves the outgoing tangent.
		/// </summary>
		public T OutTangent
		{
			get
			{
				return _OutTangent;
			}
		}

		private int _Time = 0;
		private T _Value = default(T);
		private T _InTangent = default(T);
		private T _OutTangent = default(T);
	}
}
