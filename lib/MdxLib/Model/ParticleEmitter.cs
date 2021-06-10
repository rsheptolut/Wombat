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
	/// A particle emitter class. Emits particles or other models.
	/// For more advanced emitter options see particle emitter 2.
	/// </summary>
	public sealed class CParticleEmitter : CNode<CParticleEmitter>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this particle emitter</param>
		public CParticleEmitter(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the particle emitter.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Particle Emitter #" + ObjectId;
		}

		/// <summary>
		/// Retrieves the node ID (if added to a container).
		/// </summary>
		public override int NodeId
		{
			get
			{
				return Model.GetParticleEmitterNodeId(this);
			}
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
		/// Gets or sets the emitter uses mdl flag.
		/// </summary>
		public bool EmitterUsesMdl
		{
			get
			{
				return _EmitterUsesMdl;
			}
			set
			{
				AddSetObjectFieldCommand("_EmitterUsesMdl", value);
				_EmitterUsesMdl = value;
			}
		}

		/// <summary>
		/// Gets or sets the emitter uses tga flag.
		/// </summary>
		public bool EmitterUsesTga
		{
			get
			{
				return _EmitterUsesTga;
			}
			set
			{
				AddSetObjectFieldCommand("_EmitterUsesTga", value);
				_EmitterUsesTga = value;
			}
		}

		/// <summary>
		/// Retrieves the emission rate animator.
		/// </summary>
		public Animator.CAnimator<float> EmissionRate
		{
			get
			{
				return _EmissionRate ?? (_EmissionRate = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
			}
		}

		/// <summary>
		/// Retrieves the gravity animator.
		/// </summary>
		public Animator.CAnimator<float> Gravity
		{
			get
			{
				return _Gravity ?? (_Gravity = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
			}
		}

		/// <summary>
		/// Retrives the longitude animator.
		/// </summary>
		public Animator.CAnimator<float> Longitude
		{
			get
			{
				return _Longitude ?? (_Longitude = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
			}
		}

		/// <summary>
		/// Retrieves the latitude animator.
		/// </summary>
		public Animator.CAnimator<float> Latitude
		{
			get
			{
				return _Latitude ?? (_Latitude = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
			}
		}

		/// <summary>
		/// Retrives the life span animator.
		/// </summary>
		public Animator.CAnimator<float> LifeSpan
		{
			get
			{
				return _LifeSpan ?? (_LifeSpan = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
			}
		}

		/// <summary>
		/// Retrives the initial velocity animator.
		/// </summary>
		public Animator.CAnimator<float> InitialVelocity
		{
			get
			{
				return _InitialVelocity ?? (_InitialVelocity = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
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

		private string _FileName = "";
		private bool _EmitterUsesMdl = false;
		private bool _EmitterUsesTga = false;

		private Animator.CAnimator<float> _EmissionRate = null;
		private Animator.CAnimator<float> _Gravity = null;
		private Animator.CAnimator<float> _Longitude = null;
		private Animator.CAnimator<float> _Latitude = null;
		private Animator.CAnimator<float> _LifeSpan = null;
		private Animator.CAnimator<float> _InitialVelocity = null;
		private Animator.CAnimator<float> _Visibility = null;
	}
}
