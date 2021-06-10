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
	/// A sequence class. Represents an animation like Stand/Walk.
	/// </summary>
	public sealed class CSequence : CObject<CSequence>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this sequence</param>
		public CSequence(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the sequence.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Sequence #" + ObjectId;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				AddSetObjectFieldCommand("_Name", value);
				_Name = value;
			}
		}

		/// <summary>
		/// Gets or sets the interval start time.
		/// </summary>
		public int IntervalStart
		{
			get
			{
				return _IntervalStart;
			}
			set
			{
				AddSetObjectFieldCommand("_IntervalStart", value);
				_IntervalStart = value;
			}
		}

		/// <summary>
		/// Gets or sets the interval end time.
		/// </summary>
		public int IntervalEnd
		{
			get
			{
				return _IntervalEnd;
			}
			set
			{
				AddSetObjectFieldCommand("_IntervalEnd", value);
				_IntervalEnd = value;
			}
		}

		/// <summary>
		/// Gets or sets the sync point.
		/// </summary>
		public int SyncPoint
		{
			get
			{
				return _SyncPoint;
			}
			set
			{
				AddSetObjectFieldCommand("_SyncPoint", value);
				_SyncPoint = value;
			}
		}

		/// <summary>
		/// Gets or sets the rarity.
		/// </summary>
		public float Rarity
		{
			get
			{
				return _Rarity;
			}
			set
			{
				AddSetObjectFieldCommand("_Rarity", value);
				_Rarity = value;
			}
		}

		/// <summary>
		/// Gets or sets the move speed.
		/// </summary>
		public float MoveSpeed
		{
			get
			{
				return _MoveSpeed;
			}
			set
			{
				AddSetObjectFieldCommand("_MoveSpeed", value);
				_MoveSpeed = value;
			}
		}

		/// <summary>
		/// Gets or sets the non looping flag.
		/// </summary>
		public bool NonLooping
		{
			get
			{
				return _NonLooping;
			}
			set
			{
				AddSetObjectFieldCommand("_NonLooping", value);
				_NonLooping = value;
			}
		}

		/// <summary>
		/// Gets or sets the extent.
		/// </summary>
		public Primitives.CExtent Extent
		{
			get
			{
				return _Extent;
			}
			set
			{
				AddSetObjectFieldCommand("_Extent", value);
				_Extent = value;
			}
		}

		private string _Name = "";
		private int _IntervalStart = 0;
		private int _IntervalEnd = 0;
		private int _SyncPoint = 0;
		private float _Rarity = 0.0f;
		private float _MoveSpeed = 0.0f;
		private bool _NonLooping = false;
		private Primitives.CExtent _Extent = CConstants.DefaultExtent;
	}
}
