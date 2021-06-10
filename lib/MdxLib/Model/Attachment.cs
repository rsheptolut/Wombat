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
	/// An attachment class. Represents a point to which stuff can
	/// be attached, like buffs and other special effects.
	/// </summary>
	public sealed class CAttachment : CNode<CAttachment>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this attachment</param>
		public CAttachment(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the attachment.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Attachment #" + ObjectId;
		}

		/// <summary>
		/// Retrieves the node ID (if added to a container).
		/// </summary>
		public override int NodeId
		{
			get
			{
				return Model.GetAttachmentNodeId(this);
			}
		}

		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		public string Path
		{
			get
			{
				return _Path;
			}
			set
			{
				AddSetObjectFieldCommand("_Path", value);
				_Path = value;
			}
		}

		/// <summary>
		/// Gets or sets the attachment ID.
		/// </summary>
		public int AttachmentId
		{
			get
			{
				return _AttachmentId;
			}
			set
			{
				AddSetObjectFieldCommand("_AttachmentId", value);
				_AttachmentId = value;
			}
		}

		/// <summary>
		/// Retrieves the visibility animator.
		/// </summary>
		public Animator.CAnimator<float> Visibility
		{
			get
			{
				return _Visibility ?? (_Visibility = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(1.0f)));
			}
		}

		private string _Path = "";
		private int _AttachmentId = CConstants.InvalidId;

		private Animator.CAnimator<float> _Visibility = null;
	}
}
