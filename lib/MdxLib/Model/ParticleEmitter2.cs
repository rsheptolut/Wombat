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
	/// A particle emitter 2 class. Emits particles that can be animated in
	/// many ways. Is used to create effects such as fire, explosions and blood.
	/// </summary>
	public sealed class CParticleEmitter2 : CNode<CParticleEmitter2>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this particle emitter 2</param>
		public CParticleEmitter2(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the particle emitter 2.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Particle Emitter 2 #" + ObjectId;
		}

		/// <summary>
		/// Retrieves the node ID (if added to a container).
		/// </summary>
		public override int NodeId
		{
			get
			{
				return Model.GetParticleEmitter2NodeId(this);
			}
		}

		/// <summary>
		/// Gets or sets the filter mode.
		/// </summary>
		public EParticleEmitter2FilterMode FilterMode
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
		/// Gets or sets the priority plane.
		/// </summary>
		public int PriorityPlane
		{
			get
			{
				return _PriorityPlane;
			}
			set
			{
				AddSetObjectFieldCommand("_PriorityPlane", value);
				_PriorityPlane = value;
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
		/// Gets or sets the time.
		/// </summary>
		public float Time
		{
			get
			{
				return _Time;
			}
			set
			{
				AddSetObjectFieldCommand("_Time", value);
				_Time = value;
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
		/// Gets or sets the tail length.
		/// </summary>
		public float TailLength
		{
			get
			{
				return _TailLength;
			}
			set
			{
				AddSetObjectFieldCommand("_TailLength", value);
				_TailLength = value;
			}
		}

		/// <summary>
		/// Gets or sets the sort primitives far Z flag.
		/// </summary>
		public bool SortPrimitivesFarZ
		{
			get
			{
				return _SortPrimitivesFarZ;
			}
			set
			{
				AddSetObjectFieldCommand("_SortPrimitivesFarZ", value);
				_SortPrimitivesFarZ = value;
			}
		}

		/// <summary>
		/// Gets or sets the line emitter flag.
		/// </summary>
		public bool LineEmitter
		{
			get
			{
				return _LineEmitter;
			}
			set
			{
				AddSetObjectFieldCommand("_LineEmitter", value);
				_LineEmitter = value;
			}
		}

		/// <summary>
		/// Gets or sets the model space flag.
		/// </summary>
		public bool ModelSpace
		{
			get
			{
				return _ModelSpace;
			}
			set
			{
				AddSetObjectFieldCommand("_ModelSpace", value);
				_ModelSpace = value;
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
		/// Gets or sets the XY quad flag. This decides if the particles will be
		/// aligned with the XY-plane (the ground).
		/// </summary>
		public bool XYQuad
		{
			get
			{
				return _XYQuad;
			}
			set
			{
				AddSetObjectFieldCommand("_XYQuad", value);
				_XYQuad = value;
			}
		}

		/// <summary>
		/// Gets or sets the squirt flag.
		/// </summary>
		public bool Squirt
		{
			get
			{
				return _Squirt;
			}
			set
			{
				AddSetObjectFieldCommand("_Squirt", value);
				_Squirt = value;
			}
		}

		/// <summary>
		/// Gets or sets the head flag.
		/// </summary>
		public bool Head
		{
			get
			{
				return _Head;
			}
			set
			{
				AddSetObjectFieldCommand("_Head", value);
				_Head = value;
			}
		}

		/// <summary>
		/// Gets or sets the tail flag.
		/// </summary>
		public bool Tail
		{
			get
			{
				return _Tail;
			}
			set
			{
				AddSetObjectFieldCommand("_Tail", value);
				_Tail = value;
			}
		}

		/// <summary>
		/// Gets or sets the first segment.
		/// </summary>
		public Primitives.CSegment Segment1
		{
			get
			{
				return _Segment1;
			}
			set
			{
				AddSetObjectFieldCommand("_Segment1", value);
				_Segment1 = value;
			}
		}

		/// <summary>
		/// Gets or sets the second segment.
		/// </summary>
		public Primitives.CSegment Segment2
		{
			get
			{
				return _Segment2;
			}
			set
			{
				AddSetObjectFieldCommand("_Segment2", value);
				_Segment2 = value;
			}
		}

		/// <summary>
		/// Gets or sets the third segment.
		/// </summary>
		public Primitives.CSegment Segment3
		{
			get
			{
				return _Segment3;
			}
			set
			{
				AddSetObjectFieldCommand("_Segment3", value);
				_Segment3 = value;
			}
		}

		/// <summary>
		/// Gets or sets the head life interval.
		/// </summary>
		public Primitives.CInterval HeadLife
		{
			get
			{
				return _HeadLife;
			}
			set
			{
				AddSetObjectFieldCommand("_HeadLife", value);
				_HeadLife = value;
			}
		}

		/// <summary>
		/// Gets or sets the head decay interval.
		/// </summary>
		public Primitives.CInterval HeadDecay
		{
			get
			{
				return _HeadDecay;
			}
			set
			{
				AddSetObjectFieldCommand("_HeadDecay", value);
				_HeadDecay = value;
			}
		}

		/// <summary>
		/// Gets or sets the tail life interval.
		/// </summary>
		public Primitives.CInterval TailLife
		{
			get
			{
				return _TailLife;
			}
			set
			{
				AddSetObjectFieldCommand("_TailLife", value);
				_TailLife = value;
			}
		}

		/// <summary>
		/// Gets or sets the tail decay interval.
		/// </summary>
		public Primitives.CInterval TailDecay
		{
			get
			{
				return _TailDecay;
			}
			set
			{
				AddSetObjectFieldCommand("_TailDecay", value);
				_TailDecay = value;
			}
		}

		/// <summary>
		/// Retrieves the speed animator.
		/// </summary>
		public Animator.CAnimator<float> Speed
		{
			get
			{
				return _Speed ?? (_Speed = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
			}
		}

		/// <summary>
		/// Retrieves the variation animator.
		/// </summary>
		public Animator.CAnimator<float> Variation
		{
			get
			{
				return _Variation ?? (_Variation = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
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
		/// Retrieves the width animator.
		/// </summary>
		public Animator.CAnimator<float> Width
		{
			get
			{
				return _Width ?? (_Width = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
			}
		}

		/// <summary>
		/// Retrieves the length animator.
		/// </summary>
		public Animator.CAnimator<float> Length
		{
			get
			{
				return _Length ?? (_Length = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
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
		/// Retrieves the texture reference.
		/// </summary>
		public CObjectReference<CTexture> Texture
		{
			get
			{
				return _Texture ?? (_Texture = new CObjectReference<CTexture>(Model));
			}
		}

		private EParticleEmitter2FilterMode _FilterMode = EParticleEmitter2FilterMode.Blend;
		private int _Rows = 1;
		private int _Columns = 1;
		private int _PriorityPlane = 0;
		private int _ReplaceableId = CConstants.InvalidId;
		private float _Time = 0.0f;
		private float _LifeSpan = 0.0f;
		private float _TailLength = 0.0f;
		private bool _SortPrimitivesFarZ = false;
		private bool _LineEmitter = false;
		private bool _ModelSpace = false;
		private bool _Unshaded = false;
		private bool _Unfogged = false;
		private bool _XYQuad = false;
		private bool _Squirt = false;
		private bool _Head = false;
		private bool _Tail = false;
		private Primitives.CSegment _Segment1 = CConstants.DefaultSegment;
		private Primitives.CSegment _Segment2 = CConstants.DefaultSegment;
		private Primitives.CSegment _Segment3 = CConstants.DefaultSegment;
		private Primitives.CInterval _HeadLife = CConstants.DefaultInterval;
		private Primitives.CInterval _HeadDecay = CConstants.DefaultInterval;
		private Primitives.CInterval _TailLife = CConstants.DefaultInterval;
		private Primitives.CInterval _TailDecay = CConstants.DefaultInterval;

		private Animator.CAnimator<float> _Speed = null;
		private Animator.CAnimator<float> _Variation = null;
		private Animator.CAnimator<float> _Latitude = null;
		private Animator.CAnimator<float> _Gravity = null;
		private Animator.CAnimator<float> _EmissionRate = null;
		private Animator.CAnimator<float> _Width = null;
		private Animator.CAnimator<float> _Length = null;
		private Animator.CAnimator<float> _Visibility = null;

		private CObjectReference<CTexture> _Texture = null;
	}
}
