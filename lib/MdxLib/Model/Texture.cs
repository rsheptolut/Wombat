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
	/// A texture class. Represents a texture which can be a real image
	/// or a replaceable texture (like teamcolor).
	/// </summary>
	public sealed class CTexture : CObject<CTexture>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this texture</param>
		public CTexture(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the texture.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Texture #" + ObjectId;
		}

		/// <summary>
		/// Gets or sets the filename.
		/// </summary>
		public string FileName
		{
			get
			{
				return _FileName;
			}
			set
			{
				AddSetObjectFieldCommand("_FileName", value);
				_FileName = value;
			}
		}

		/// <summary>
		/// Gets or sets the replaceable ID.
		/// </summary>
		public int ReplaceableId
		{
			get
			{
				return _ReplaceableId;
			}
			set
			{
				AddSetObjectFieldCommand("_ReplaceableId", value);
				_ReplaceableId = value;
			}
		}

		/// <summary>
		/// Gets or sets the wrap width flag.
		/// </summary>
		public bool WrapWidth
		{
			get
			{
				return _WrapWidth;
			}
			set
			{
				AddSetObjectFieldCommand("_WrapWidth", value);
				_WrapWidth = value;
			}
		}

		/// <summary>
		/// Gets or sets the wrap height flag.
		/// </summary>
		public bool WrapHeight
		{
			get
			{
				return _WrapHeight;
			}
			set
			{
				AddSetObjectFieldCommand("_WrapHeight", value);
				_WrapHeight = value;
			}
		}

		private string _FileName = "";
		private int _ReplaceableId = 0;
		private bool _WrapWidth = false;
		private bool _WrapHeight = false;
	}
}
