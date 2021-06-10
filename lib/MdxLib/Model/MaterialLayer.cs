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
	/// A material layer class. Represents a single layer for a material.
	/// </summary>
	public sealed class CMaterialLayer : CObject<CMaterialLayer>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this material layer</param>
		public CMaterialLayer(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the material layer.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Material Layer #" + ObjectId;
		}

		/// <summary>
		/// Gets or sets the filter mode.
		/// </summary>
		public EMaterialLayerFilterMode FilterMode
		{
			get
			{
				return _FilterMode;
			}
			set
			{
				AddSetObjectFieldCommand("_FilterMode", value);
				_FilterMode = value;
			}
		}

		/// <summary>
		/// Gets or sets the coord ID.
		/// </summary>
		public int CoordId
		{
			get
			{
				return _CoordId;
			}
			set
			{
				AddSetObjectFieldCommand("_CoordId", value);
				_CoordId = value;
			}
		}

		/// <summary>
		/// Gets or sets the unshaded flag.
		/// </summary>
		public bool Unshaded
		{
			get
			{
				return _Unshaded;
			}
			set
			{
				AddSetObjectFieldCommand("_Unshaded", value);
				_Unshaded = value;
			}
		}

		/// <summary>
		/// Gets or sets the unfogged flag.
		/// </summary>
		public bool Unfogged
		{
			get
			{
				return _Unfogged;
			}
			set
			{
				AddSetObjectFieldCommand("_Unfogged", value);
				_Unfogged = value;
			}
		}

		/// <summary>
		/// Gets or sets the two sided flag.
		/// </summary>
		public bool TwoSided
		{
			get
			{
				return _TwoSided;
			}
			set
			{
				AddSetObjectFieldCommand("_TwoSided", value);
				_TwoSided = value;
			}
		}

		/// <summary>
		/// Gets or sets the sphere environment map flag.
		/// </summary>
		public bool SphereEnvironmentMap
		{
			get
			{
				return _SphereEnvironmentMap;
			}
			set
			{
				AddSetObjectFieldCommand("_SphereEnvironmentMap", value);
				_SphereEnvironmentMap = value;
			}
		}

		/// <summary>
		/// Gets or sets the no depth test flag.
		/// </summary>
		public bool NoDepthTest
		{
			get
			{
				return _NoDepthTest;
			}
			set
			{
				AddSetObjectFieldCommand("_NoDepthTest", value);
				_NoDepthTest = value;
			}
		}

		/// <summary>
		/// Gets or sets the no depth set flag.
		/// </summary>
		public bool NoDepthSet
		{
			get
			{
				return _NoDepthSet;
			}
			set
			{
				AddSetObjectFieldCommand("_NoDepthSet", value);
				_NoDepthSet = value;
			}
		}

		/// <summary>
		/// Retrieves the texture ID animator. Use this if the texture is animated,
		/// otherwise use the Texture reference.
		/// </summary>
		public Animator.CAnimator<int> TextureId
		{
			get
			{
				return _TextureId ?? (_TextureId = new Animator.CAnimator<int>(Model, new Animator.Animatable.CInteger(CConstants.InvalidId)));
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
		/// Retrieves the texture reference. Use this if the texture is static,
		/// otherwise use the TextureId animator.
		/// </summary>
		public CObjectReference<CTexture> Texture
		{
			get
			{
				return _Texture ?? (_Texture = new CObjectReference<CTexture>(Model));
			}
		}

		/// <summary>
		/// Retrieves the texture animation reference.
		/// </summary>
		public CObjectReference<CTextureAnimation> TextureAnimation
		{
			get
			{
				return _TextureAnimation ?? (_TextureAnimation = new CObjectReference<CTextureAnimation>(Model));
			}
		}

		private EMaterialLayerFilterMode _FilterMode = EMaterialLayerFilterMode.None;
		private int _CoordId = 0;
		private bool _Unshaded = false;
		private bool _Unfogged = false;
		private bool _TwoSided = false;
		private bool _SphereEnvironmentMap = false;
		private bool _NoDepthTest = false;
		private bool _NoDepthSet = false;

		private Animator.CAnimator<int> _TextureId = null;
		private Animator.CAnimator<float> _Alpha = null;

		private CObjectReference<CTexture> _Texture = null;
		private CObjectReference<CTextureAnimation> _TextureAnimation = null;
	}
}
