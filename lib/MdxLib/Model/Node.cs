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
	/// The base interface for all node components.
	/// </summary>
	public interface INode : IObject
	{
		/// <summary>
		/// Retrieves the node ID (if added to a container).
		/// </summary>
		int NodeId { get; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the don't inherit translation flag.
		/// </summary>
		bool DontInheritTranslation { get; set; }

		/// <summary>
		/// Gets or sets the don't inherit rotation flag.
		/// </summary>
		bool DontInheritRotation { get; set; }

		/// <summary>
		/// Gets or sets the don't inherit scaling flag.
		/// </summary>
		bool DontInheritScaling { get; set; }

		/// <summary>
		/// Gets or sets the billboarded flag.
		/// </summary>
		bool Billboarded { get; set; }

		/// <summary>
		/// Gets or sets the billboarded lock X flag.
		/// </summary>
		bool BillboardedLockX { get; set; }

		/// <summary>
		/// Gets or sets the billboarded lock Y flag.
		/// </summary>
		bool BillboardedLockY { get; set; }

		/// <summary>
		/// Gets or sets the billboarded lock Z flag.
		/// </summary>
		bool BillboardedLockZ { get; set; }

		/// <summary>
		/// Gets or sets the camera anchored flag.
		/// </summary>
		bool CameraAnchored { get; set; }
		
		/// <summary>
		/// Gets or sets the pivot point.
		/// </summary>
		Primitives.CVector3 PivotPoint { get; set; }

		/// <summary>
		/// Retrieves the translation animator.
		/// </summary>
		Animator.CAnimator<Primitives.CVector3> Translation { get; }

		/// <summary>
		/// Retrieves the rotation animator.
		/// </summary>
		Animator.CAnimator<Primitives.CVector4> Rotation { get; }

		/// <summary>
		/// Retrieves the scaling animator.
		/// </summary>
		Animator.CAnimator<Primitives.CVector3> Scaling { get; }

		/// <summary>
		/// Retrieves the parent reference. Is used to construct the node skeleton hiearchy.
		/// </summary>
		CNodeReference Parent { get; }
	}

	/// <summary>
	/// The base class for all node components. This class is templated so
	/// use INode if you want non-specified access.
	/// </summary>
	/// <typeparam name="T">The object type (class that inherits this class)</typeparam>
	public abstract class CNode<T> : CObject<T>, INode where T : CNode<T>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this node</param>
		public CNode(CModel Model) : base(Model)
		{
			//Empty
		}

		internal override void BuildNodeDetacherList(System.Collections.Generic.ICollection<CDetacher> DetacherList)
		{
			foreach(object Node in NodeReferenceSet)
			{
				CNodeReference Reference = Node as CNodeReference;
				if(Reference != null) DetacherList.Add(new CNodeDetacher(Reference));
			}
		}

		internal void AddSetNodeFieldCommand<T2>(string FieldName, T2 Value)
		{
			if(Model.CommandGroup != null)
			{
				Model.CommandGroup.Add(new Command.CSetNodeField<T, T2>((T)this, FieldName, Value));
			}
		}

		/// <summary>
		/// Retrieves the node ID (if added to a container).
		/// </summary>
		public abstract int NodeId { get; }

		/// <summary>
		/// Checks if the node has references pointing to it.
		/// </summary>
		public override bool HasReferences
		{
			get
			{
				if(NodeReferenceSet.Count > 0) return true;
				return base.HasReferences;
			}
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
				AddSetNodeFieldCommand("_Name", value);
				_Name = value;
			}
		}

		/// <summary>
		/// Gets or sets the don't inherit translation flag.
		/// </summary>
		public bool DontInheritTranslation
		{
			get
			{
				return _DontInheritTranslation;
			}
			set
			{
				AddSetNodeFieldCommand("_DontInheritTranslation", value);
				_DontInheritTranslation = value;
			}
		}

		/// <summary>
		/// Gets or sets the don't inherit rotation flag.
		/// </summary>
		public bool DontInheritRotation
		{
			get
			{
				return _DontInheritRotation;
			}
			set
			{
				AddSetNodeFieldCommand("_DontInheritRotation", value);
				_DontInheritRotation = value;
			}
		}

		/// <summary>
		/// Gets or sets the don't inherit scaling flag.
		/// </summary>
		public bool DontInheritScaling
		{
			get
			{
				return _DontInheritScaling;
			}
			set
			{
				AddSetNodeFieldCommand("_DontInheritScaling", value);
				_DontInheritScaling = value;
			}
		}

		/// <summary>
		/// Gets or sets the billboarded flag.
		/// </summary>
		public bool Billboarded
		{
			get
			{
				return _Billboarded;
			}
			set
			{
				AddSetNodeFieldCommand("_Billboarded", value);
				_Billboarded = value;
			}
		}

		/// <summary>
		/// Gets or sets the billboarded lock X flag.
		/// </summary>
		public bool BillboardedLockX
		{
			get
			{
				return _BillboardedLockX;
			}
			set
			{
				AddSetNodeFieldCommand("_BillboardedLockX", value);
				_BillboardedLockX = value;
			}
		}

		/// <summary>
		/// Gets or sets the billboarded lock Y flag.
		/// </summary>
		public bool BillboardedLockY
		{
			get
			{
				return _BillboardedLockY;
			}
			set
			{
				AddSetNodeFieldCommand("_BillboardedLockY", value);
				_BillboardedLockY = value;
			}
		}

		/// <summary>
		/// Gets or sets the billboarded lock Z flag.
		/// </summary>
		public bool BillboardedLockZ
		{
			get
			{
				return _BillboardedLockZ;
			}
			set
			{
				AddSetNodeFieldCommand("_BillboardedLockZ", value);
				_BillboardedLockZ = value;
			}
		}

		/// <summary>
		/// Gets or sets the camera anchored flag.
		/// </summary>
		public bool CameraAnchored
		{
			get
			{
				return _CameraAnchored;
			}
			set
			{
				AddSetNodeFieldCommand("_CameraAnchored", value);
				_CameraAnchored = value;
			}
		}

		/// <summary>
		/// Gets or sets the pivot point.
		/// </summary>
		public Primitives.CVector3 PivotPoint
		{
			get
			{
				return _PivotPoint;
			}
			set
			{
				AddSetNodeFieldCommand("_PivotPoint", value);
				_PivotPoint = value;
			}
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

		/// <summary>
		/// Retrieves the parent reference. Is used to construct the node skeleton hiearchy.
		/// </summary>
		public CNodeReference Parent
		{
			get
			{
				return _Parent ?? (_Parent = new CNodeReference(Model));
			}
		}

		private string _Name = "";
		private bool _DontInheritTranslation = false;
		private bool _DontInheritRotation = false;
		private bool _DontInheritScaling = false;
		private bool _Billboarded = false;
		private bool _BillboardedLockX = false;
		private bool _BillboardedLockY = false;
		private bool _BillboardedLockZ = false;
		private bool _CameraAnchored = false;
		private Primitives.CVector3 _PivotPoint = CConstants.DefaultVector3;

		private Animator.CAnimator<Primitives.CVector3> _Translation = null;
		private Animator.CAnimator<Primitives.CVector4> _Rotation = null;
		private Animator.CAnimator<Primitives.CVector3> _Scaling = null;

		private CNodeReference _Parent = null;
	}
}
