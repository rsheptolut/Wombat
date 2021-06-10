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
	/// A geoset animation class. Can animate certain aspects of a geoset,
	/// like its color.
	/// </summary>
	public sealed class CGeosetAnimation : CObject<CGeosetAnimation>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this geoset animation</param>
		public CGeosetAnimation(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the geoset animation.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Geoset Animation #" + ObjectId;
		}

		/// <summary>
		/// Gets or sets the use color flag.
		/// </summary>
		public bool UseColor
		{
			get
			{
				return _UseColor;
			}
			set
			{
				AddSetObjectFieldCommand("_UseColor", value);
				_UseColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the drop shadow flag.
		/// </summary>
		public bool DropShadow
		{
			get
			{
				return _DropShadow;
			}
			set
			{
				AddSetObjectFieldCommand("_DropShadow", value);
				_DropShadow = value;
			}
		}

		/// <summary>
		/// Retrieves the color animator.
		/// </summary>
		public Animator.CAnimator<Primitives.CVector3> Color
		{
			get
			{
				return _Color ?? (_Color = new Animator.CAnimator<Primitives.CVector3>(Model, new Animator.Animatable.CVector3(CConstants.DefaultColor)));
			}
		}

		/// <summary>
		/// Retrieves the alpha animator.
		/// </summary>
		public Animator.CAnimator<float> Alpha
		{
			get
			{
				return _Alpha ?? (_Alpha = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(1.0f)));
			}
		}

		/// <summary>
		/// Retrieves the geoset reference.
		/// </summary>
		public CObjectReference<CGeoset> Geoset
		{
			get
			{
				return _Geoset ?? (_Geoset = new CObjectReference<CGeoset>(Model));
			}
		}

		private bool _UseColor = false;
		private bool _DropShadow = false;

		private Animator.CAnimator<Primitives.CVector3> _Color = null;
		private Animator.CAnimator<float> _Alpha = null;

		private CObjectReference<CGeoset> _Geoset = null;
	}
}
