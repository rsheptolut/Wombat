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
	/// A light class. Illuminates the model and its surrounding environment.
	/// </summary>
	public sealed class CLight : CNode<CLight>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this light</param>
		public CLight(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the light.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Light #" + ObjectId;
		}

		/// <summary>
		/// Retrieves the node ID (if added to a container).
		/// </summary>
		public override int NodeId
		{
			get
			{
				return Model.GetLightNodeId(this);
			}
		}

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		public ELightType Type
		{
			get
			{
				return _Type;
			}
			set
			{
				AddSetObjectFieldCommand("_Type", value);
				_Type = value;
			}
		}

		/// <summary>
		/// Retrieves the attenuation start animator.
		/// </summary>
		public Animator.CAnimator<float> AttenuationStart
		{
			get
			{
				return _AttenuationStart ?? (_AttenuationStart = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
			}
		}

		/// <summary>
		/// Retrieves the attenuation end animator.
		/// </summary>
		public Animator.CAnimator<float> AttenuationEnd
		{
			get
			{
				return _AttenuationEnd ?? (_AttenuationEnd = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
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
		/// Retrieves the intensity animator.
		/// </summary>
		public Animator.CAnimator<float> Intensity
		{
			get
			{
				return _Intensity ?? (_Intensity = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
			}
		}

		/// <summary>
		/// Retrieves the ambient color animator.
		/// </summary>
		public Animator.CAnimator<Primitives.CVector3> AmbientColor
		{
			get
			{
				return _AmbientColor ?? (_AmbientColor = new Animator.CAnimator<Primitives.CVector3>(Model, new Animator.Animatable.CVector3(CConstants.DefaultColor)));
			}
		}

		/// <summary>
		/// Retrieves the ambient intensity animator.
		/// </summary>
		public Animator.CAnimator<float> AmbientIntensity
		{
			get
			{
				return _AmbientIntensity ?? (_AmbientIntensity = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
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

		private ELightType _Type = ELightType.Omnidirectional;

		private Animator.CAnimator<float> _AttenuationStart = null;
		private Animator.CAnimator<float> _AttenuationEnd = null;
		private Animator.CAnimator<Primitives.CVector3> _Color = null;
		private Animator.CAnimator<float> _Intensity = null;
		private Animator.CAnimator<Primitives.CVector3> _AmbientColor = null;
		private Animator.CAnimator<float> _AmbientIntensity = null;
		private Animator.CAnimator<float> _Visibility = null;
	}
}
