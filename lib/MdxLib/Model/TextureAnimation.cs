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
	/// A texture animation class. Animates the vertex coordinates of the
	/// texture to create effects like flowing water.
	/// </summary>
	public sealed class CTextureAnimation : CObject<CTextureAnimation>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this texture animation</param>
		public CTextureAnimation(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the texture animation.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Texture Animation #" + ObjectId;
		}

		/// <summary>
		/// Retrieves the translation animator.
		/// </summary>
		public Animator.CAnimator<Primitives.CVector3> Translation
		{
			get
			{
				return _Translation ?? (_Translation = new Animator.CAnimator<Primitives.CVector3>(Model, new Animator.Animatable.CVector3(CConstants.DefaultTranslation)));
			}
		}

		/// <summary>
		/// Retrieves the rotation animator.
		/// </summary>
		public Animator.CAnimator<Primitives.CVector4> Rotation
		{
			get
			{
				return _Rotation ?? (_Rotation = new Animator.CAnimator<Primitives.CVector4>(Model, new Animator.Animatable.CQuaternion(CConstants.DefaultRotation)));
			}
		}

		/// <summary>
		/// Retrieves the scaling animator.
		/// </summary>
		public Animator.CAnimator<Primitives.CVector3> Scaling
		{
			get
			{
				return _Scaling ?? (_Scaling = new Animator.CAnimator<Primitives.CVector3>(Model, new Animator.Animatable.CVector3(CConstants.DefaultScaling)));
			}
		}

		private Animator.CAnimator<Primitives.CVector3> _Translation = null;
		private Animator.CAnimator<Primitives.CVector4> _Rotation = null;
		private Animator.CAnimator<Primitives.CVector3> _Scaling = null;
	}
}
