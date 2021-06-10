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
	/// <summary>
	/// A global sequence class. Defines a sequence which is not tied to
	/// the common Stand/Walk sequences and totally independent from them.
	/// One example is the Gyrocopter's propeller.
	/// </summary>
	public sealed class CGlobalSequence : CObject<CGlobalSequence>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this global sequence</param>
		public CGlobalSequence(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the global sequence.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Global Sequence #" + ObjectId;
		}

		/// <summary>
		/// Gets or sets the duration. This is the length of the sequence.
		/// </summary>
		public int Duration
		{
			get
			{
				return _Duration;
			}
			set
			{
				AddSetObjectFieldCommand("_Duration", value);
				_Duration = value;
			}
		}

		private int _Duration = 0;
	}
}
