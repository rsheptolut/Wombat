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
	/// A ribbon emitter class. Is used to create trailing ribbon effects,
	/// like a floating band that slowly dissipates.
	/// </summary>
	public sealed class CRibbonEmitter : CNode<CRibbonEmitter>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this ribbon emitter</param>
		public CRibbonEmitter(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the ribbon emitter.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Ribbon Emitter #" + ObjectId;
		}

		/// <summary>
		/// Retrieves the node ID (if added to a container).
		/// </summary>
		public override int NodeId
		{
			get
			{
				return Model.GetRibbonEmitterNodeId(this);
			}
		}

		/// <summary>
		/// Gets or sets the rows.
		/// </summary>
		public int Rows
		{
			get
			{
				return _Rows;
			}
			set
			{
				AddSetObjectFieldCommand("_Rows", value);
				_Rows = value;
			}
		}

		/// <summary>
		/// Gets or sets the columns.
		/// </summary>
		public int Columns
		{
			get
			{
				return _Columns;
			}
			set
			{
				AddSetObjectFieldCommand("_Columns", value);
				_Columns = value;
			}
		}

		/// <summary>
		/// Gets or sets the emission rate.
		/// </summary>
		public int EmissionRate
		{
			get
			{
				return _EmissionRate;
			}
			set
			{
				AddSetObjectFieldCommand("_EmissionRate", value);
				_EmissionRate = value;
			}
		}

		/// <summary>
		/// Gets or sets the life span.
		/// </summary>
		public float LifeSpan
		{
			get
			{
				return _LifeSpan;
			}
			set
			{
				AddSetObjectFieldCommand("_LifeSpan", value);
				_LifeSpan = value;
			}
		}

		/// <summary>
		/// Gets or sets the gravity.
		/// </summary>
		public float Gravity
		{
			get
			{
				return _Gravity;
			}
			set
			{
				AddSetObjectFieldCommand("_Gravity", value);
				_Gravity = value;
			}
		}

		/// <summary>
		/// Retrieves the height above animator.
		/// </summary>
		public Animator.CAnimator<float> HeightAbove
		{
			get
			{
				return _HeightAbove ?? (_HeightAbove = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
			}
		}

		/// <summary>
		/// Retrieves the height below animator.
		/// </summary>
		public Animator.CAnimator<float> HeightBelow
		{
			get
			{
				return _HeightBelow ?? (_HeightBelow = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
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
		/// Retrieves the texture slot animator.
		/// </summary>
		public Animator.CAnimator<int> TextureSlot
		{
			get
			{
				return _TextureSlot ?? (_TextureSlot = new Animator.CAnimator<int>(Model, new Animator.Animatable.CInteger(0)));
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

		/// <summary>
		/// Retrieves the material reference.
		/// </summary>
		public CObjectReference<CMaterial> Material
		{
			get
			{
				return _Material ?? (_Material = new CObjectReference<CMaterial>(Model));
			}
		}

		private int _Rows = 1;
		private int _Columns = 1;
		private int _EmissionRate = 0;
		private float _LifeSpan = 0.0f;
		private float _Gravity = 1.0f;

		private Animator.CAnimator<float> _HeightAbove = null;
		private Animator.CAnimator<float> _HeightBelow = null;
		private Animator.CAnimator<float> _Alpha = null;
		private Animator.CAnimator<Primitives.CVector3> _Color = null;
		private Animator.CAnimator<int> _TextureSlot = null;
		private Animator.CAnimator<float> _Visibility = null;

		private CObjectReference<CMaterial> _Material = null;
	}
}
