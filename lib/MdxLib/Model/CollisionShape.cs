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
	/// A collision shape class. Defines bounds in which a user can
	/// click to select the model.
	/// </summary>
	public sealed class CCollisionShape : CNode<CCollisionShape>
	{
		/// <summary>
		/// Parameterized constructor.
		/// </summary>
		/// <param name="Model">The model to be associated with this collision shape</param>
		public CCollisionShape(CModel Model) : base(Model)
		{
			//Empty
		}

		/// <summary>
		/// Generates a string version of the collision shape.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Collision Shape #" + ObjectId;
		}

		/// <summary>
		/// Retrieves the node ID (if added to a container).
		/// </summary>
		public override int NodeId
		{
			get
			{
				return Model.GetCollisionShapeNodeId(this);
			}
		}

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		public ECollisionShapeType Type
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
		/// Gets or sets the radius (for Sphere).
		/// </summary>
		public float Radius
		{
			get
			{
				return _Radius;
			}
			set
			{
				AddSetObjectFieldCommand("_Radius", value);
				_Radius = value;
			}
		}

		/// <summary>
		/// Gets or sets the first vertex (corner 1 for Box, center for Sphere).
		/// </summary>
		public Primitives.CVector3 Vertex1
		{
			get
			{
				return _Vertex1;
			}
			set
			{
				AddSetObjectFieldCommand("_Vertex1", value);
				_Vertex1 = value;
			}
		}

		/// <summary>
		/// Gets or sets the second vertex (corner 2 for Box).
		/// </summary>
		public Primitives.CVector3 Vertex2
		{
			get
			{
				return _Vertex2;
			}
			set
			{
				AddSetObjectFieldCommand("_Vertex2", value);
				_Vertex2 = value;
			}
		}

		private ECollisionShapeType _Type = ECollisionShapeType.Box;
		private float _Radius = 0.0f;
		private Primitives.CVector3 _Vertex1 = CConstants.DefaultVector3;
		private Primitives.CVector3 _Vertex2 = CConstants.DefaultVector3;
	}
}
