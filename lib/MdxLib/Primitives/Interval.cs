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
namespace MdxLib.Primitives
{
	/// <summary>
	/// An immutable interval. Used by particle emitters to define how
	/// the particle's sprites are animated.
	/// </summary>
	public sealed class CInterval : System.ICloneable
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CInterval()
		{
			//Empty
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="Interval">The interval to copy from</param>
		public CInterval(CInterval Interval)
		{
			_Start = Interval._Start;
			_End = Interval._End;
			_Repeat = Interval._Repeat;
		}

		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Start">The start index to use</param>
		/// <param name="End">The end index to use</param>
		/// <param name="Repeat">The repeat count to use</param>
		public CInterval(int Start, int End, int Repeat)
		{
			_Start = Start;
			_End = End;
			_Repeat = Repeat;
		}

		/// <summary>
		/// Clones the interval.
		/// </summary>
		/// <returns>The cloned interval</returns>
		public object Clone()
		{
			return new CInterval(this);
		}

		/// <summary>
		/// Generates a string version of the interval.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "{ " + _Start + ", " + _End + ", " + _Repeat + " }";
		}

		/// <summary>
		/// Retrieves the start index.
		/// </summary>
		public int Start
		{
			get
			{
				return _Start;
			}
		}

		/// <summary>
		/// Retrieves the end index.
		/// </summary>
		public int End
		{
			get
			{
				return _End;
			}
		}

		/// <summary>
		/// Retrieves the repeat count.
		/// </summary>
		public int Repeat
		{
			get
			{
				return _Repeat;
			}
		}

		private int _Start = 0;
		private int _End = 0;
		private int _Repeat = 0;
	}
}
