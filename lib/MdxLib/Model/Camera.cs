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
	/// A camera class. Represents a camera that are used for
	/// unit portraits.
	/// </summary>
	public sealed class CCamera : CObject<CCamera>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this camera</param>
		public CCamera(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the camera.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Camera #" + ObjectId;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				AddSetObjectFieldCommand("_Name", value);
				_Name = value;
			}
		}

		/// <summary>
		/// Gets or sets the field of view.
		/// </summary>
		public float FieldOfView
		{
			get
			{
				return _FieldOfView;
			}
			set
			{
				AddSetObjectFieldCommand("_FieldOfView", value);
				_FieldOfView = value;
			}
		}

		/// <summary>
		/// Gets or sets the near clipping distance.
		/// </summary>
		public float NearDistance
		{
			get
			{
				return _NearDistance;
			}
			set
			{
				AddSetObjectFieldCommand("_NearDistance", value);
				_NearDistance = value;
			}
		}

		/// <summary>
		/// Gets or sets the far clipping distance.
		/// </summary>
		public float FarDistance
		{
			get
			{
				return _FarDistance;
			}
			set
			{
				AddSetObjectFieldCommand("_FarDistance", value);
				_FarDistance = value;
			}
		}

		/// <summary>
		/// Gets or sets the position (where the camera is).
		/// </summary>
		public Primitives.CVector3 Position
		{
			get
			{
				return _Position;
			}
			set
			{
				AddSetObjectFieldCommand("_Position", value);
				_Position = value;
			}
		}

		/// <summary>
		/// Gets or sets the target position (where the camera looks at).
		/// </summary>
		public Primitives.CVector3 TargetPosition
		{
			get
			{
				return _TargetPosition;
			}
			set
			{
				AddSetObjectFieldCommand("_TargetPosition", value);
				_TargetPosition = value;
			}
		}

		/// <summary>
		/// Retrieves the rotation animator.
		/// </summary>
		public Animator.CAnimator<float> Rotation
		{
			get
			{
				return _Rotation ?? (_Rotation = new Animator.CAnimator<float>(Model, new Animator.Animatable.CFloat(0.0f)));
			}
		}

		/// <summary>
		/// Retrieves the translation animator (where the camera is).
		/// </summary>
		public Animator.CAnimator<Primitives.CVector3> Translation
		{
			get
			{
				return _Translation ?? (_Translation = new Animator.CAnimator<MdxLib.Primitives.CVector3>(Model, new Animator.Animatable.CVector3(CConstants.DefaultTranslation)));
			}
		}

		/// <summary>
		/// Retrieves the target translation animator (where the camera looks at).
		/// </summary>
		public Animator.CAnimator<Primitives.CVector3> TargetTranslation
		{
			get
			{
				return _TargetTranslation ?? (_TargetTranslation = new Animator.CAnimator<MdxLib.Primitives.CVector3>(Model, new Animator.Animatable.CVector3(CConstants.DefaultTranslation)));
			}
		}

		private string _Name = "";
		private float _FieldOfView = (float)System.Math.PI / 4.0f;
		private float _NearDistance = 10.0f;
		private float _FarDistance = 1000.0f;
		private Primitives.CVector3 _Position = CConstants.DefaultVector3;
		private Primitives.CVector3 _TargetPosition = CConstants.DefaultVector3;

		private Animator.CAnimator<float> _Rotation = null;
		private Animator.CAnimator<Primitives.CVector3> _Translation = null;
		private Animator.CAnimator<Primitives.CVector3> _TargetTranslation = null;
	}
}
