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
	/// Enumerates the available material layer filter modes.
	/// </summary>
	public enum EMaterialLayerFilterMode
	{
		/// <summary>
		/// Represents no filtering.
		/// </summary>
		None,

		/// <summary>
		/// Represents transparent filtering, makes parts transparent allowing
		/// geometry behind the geoset to shine through.
		/// </summary>
		Transparent,

		/// <summary>
		/// Represents blending filtering, makes parts transparent allowing
		/// sublayers to shine through (usually used for teamcolors).
		/// </summary>
		Blend,

		/// <summary>
		/// Represents additive filtering (like addition), makes material brighter.
		/// Does not add to alpha channel.
		/// </summary>
		Additive,

		/// <summary>
		/// Represents additive filtering (like addition), makes material brighter.
		/// Also adds to alpha channel.
		/// </summary>
		AdditiveAlpha,

		/// <summary>
		/// Represents modulation (like multiplication), makes material darker.
		/// </summary>
		Modulate,

		/// <summary>
		/// Represents even more modulation (like multiplication), makes material darker.
		/// </summary>
		Modulate2x,
	}
}
