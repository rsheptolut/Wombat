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
	/// An immutable time. Defines a point in time during a sequence
	/// (or global sequence).
	/// </summary>
	public sealed class CTime
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CTime()
		{
			//Empty
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="Time">The time to copy from</param>
		public CTime(CTime Time)
		{
			_Time = Time._Time;
			_IntervalStart = Time._IntervalStart;
			_IntervalEnd = Time._IntervalEnd;
		}

		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Time">The time to use</param>
		public CTime(int Time)
		{
			_Time = Time;
		}

		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Time">The time to use</param>
		/// <param name="IntervalStart">The time at which the animation starts</param>
		/// <param name="IntervalEnd">The time at which the animation ends</param>
		public CTime(int Time, int IntervalStart, int IntervalEnd)
		{
			_Time = Time;
			_IntervalStart = IntervalStart;
			_IntervalEnd = IntervalEnd;
		}

		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Time">The time to use</param>
		/// <param name="Sequence">The sequence defining when the animation starts and ends</param>
		public CTime(int Time, Model.CSequence Sequence)
		{
			_Time = Time;
			_IntervalStart = Sequence.IntervalStart;
			_IntervalEnd = Sequence.IntervalEnd;
		}

		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Time">The time to use</param>
		/// <param name="GlobalSequence">The global sequence defining when the animation starts and ends</param>
		public CTime(int Time, Model.CGlobalSequence GlobalSequence)
		{
			_Time = Time;
			_IntervalStart = 0;
			_IntervalEnd = GlobalSequence.Duration;
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
		/// Retrieves the time when the animation starts.
		/// </summary>
		public int IntervalStart
		{
			get
			{
				return _IntervalStart;
			}
		}

		/// <summary>
		/// Retrieves the time when the animation ends.
		/// </summary>
		public int IntervalEnd
		{
			get
			{
				return _IntervalEnd;
			}
		}

		private int _Time = 0;
		private int _IntervalStart = int.MinValue;
		private int _IntervalEnd = int.MaxValue;
	}
}
