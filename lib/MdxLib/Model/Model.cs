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
	/// The central class and the container of most other components.
	/// This is the main object you're working with.
	/// </summary>
	public sealed class CModel
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CModel()
		{
			//Empty
		}

		/// <summary>
		/// Begins a new undo/redo session. All changes on the model within
		/// this session will be stored in a command object.
		/// </summary>
		public void BeginUndoRedoSession()
		{
			if(CommandGroup == null)
			{
				CommandGroup = new Command.CCommandGroup();
			}
		}

		/// <summary>
		/// Ends the current undo/redo session. All changes on the model within
		/// this session is returned as a command object.
		/// </summary>
		/// <returns>The generated undo/redo command object</returns>
		public Command.ICommand EndUndoRedoSession()
		{
			if(CommandGroup != null)
			{
				Command.CCommandGroup CurrentCommandGroup = CommandGroup;
				CommandGroup = null;
				return CurrentCommandGroup;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Generates a string version of the model.
		/// </summary>
		/// <returns>The generated string</returns>
		public override string ToString()
		{
			return "Model \"" + Name + "\"";
		}

		internal void AddSetModelFieldCommand<T>(string FieldName, T Value)
		{
			if(CommandGroup != null)
			{
				CommandGroup.Add(new Command.CSetModelField<T>(this, FieldName, Value));
			}
		}

		internal int GetAttachmentNodeId(CAttachment Attachment)
		{
			if(_Attachments == null) return CConstants.InvalidId;

			int Id = _Attachments.IndexOf(Attachment);
			if(Id == CConstants.InvalidId) return CConstants.InvalidId;

			if(_Bones != null) Id += _Bones.Count;
			if(_Lights != null) Id += _Lights.Count;
			if(_Helpers != null) Id += _Helpers.Count;

			return Id;
		}

		internal int GetBoneNodeId(CBone Bone)
		{
			if(_Bones == null) return CConstants.InvalidId;

			int Id = _Bones.IndexOf(Bone);
			if(Id == CConstants.InvalidId) return CConstants.InvalidId;

			return Id;
		}

		internal int GetCollisionShapeNodeId(CCollisionShape CollisionShape)
		{
			if(_CollisionShapes == null) return CConstants.InvalidId;

			int Id = _CollisionShapes.IndexOf(CollisionShape);
			if(Id == CConstants.InvalidId) return CConstants.InvalidId;

			if(_Bones != null) Id += _Bones.Count;
			if(_Lights != null) Id += _Lights.Count;
			if(_Helpers != null) Id += _Helpers.Count;
			if(_Attachments != null) Id += _Attachments.Count;
			if(_ParticleEmitters != null) Id += _ParticleEmitters.Count;
			if(_ParticleEmitters2 != null) Id += _ParticleEmitters2.Count;
			if(_RibbonEmitters != null) Id += _RibbonEmitters.Count;
			if(_Events != null) Id += _Events.Count;

			return Id;
		}

		internal int GetEventNodeId(CEvent Event)
		{
			if(_Events == null) return CConstants.InvalidId;

			int Id = _Events.IndexOf(Event);
			if(Id == CConstants.InvalidId) return CConstants.InvalidId;

			if(_Bones != null) Id += _Bones.Count;
			if(_Lights != null) Id += _Lights.Count;
			if(_Helpers != null) Id += _Helpers.Count;
			if(_Attachments != null) Id += _Attachments.Count;
			if(_ParticleEmitters != null) Id += _ParticleEmitters.Count;
			if(_ParticleEmitters2 != null) Id += _ParticleEmitters2.Count;
			if(_RibbonEmitters != null) Id += _RibbonEmitters.Count;

			return Id;
		}

		internal int GetHelperNodeId(CHelper Helper)
		{
			if(_Helpers == null) return CConstants.InvalidId;

			int Id = _Helpers.IndexOf(Helper);
			if(Id == CConstants.InvalidId) return CConstants.InvalidId;

			if(_Bones != null) Id += _Bones.Count;
			if(_Lights != null) Id += _Lights.Count;

			return Id;
		}

		internal int GetLightNodeId(CLight Light)
		{
			if(_Lights == null) return CConstants.InvalidId;

			int Id = _Lights.IndexOf(Light);
			if(Id == CConstants.InvalidId) return CConstants.InvalidId;

			if(_Bones != null) Id += _Bones.Count;

			return Id;
		}

		internal int GetParticleEmitterNodeId(CParticleEmitter ParticleEmitter)
		{
			if(_ParticleEmitters == null) return CConstants.InvalidId;

			int Id = _ParticleEmitters.IndexOf(ParticleEmitter);
			if(Id == CConstants.InvalidId) return CConstants.InvalidId;

			if(_Bones != null) Id += _Bones.Count;
			if(_Lights != null) Id += _Lights.Count;
			if(_Helpers != null) Id += _Helpers.Count;
			if(_Attachments != null) Id += _Attachments.Count;

			return Id;
		}

		internal int GetParticleEmitter2NodeId(CParticleEmitter2 ParticleEmitter2)
		{
			if(_ParticleEmitters2 == null) return CConstants.InvalidId;

			int Id = _ParticleEmitters2.IndexOf(ParticleEmitter2);
			if(Id == CConstants.InvalidId) return CConstants.InvalidId;

			if(_Bones != null) Id += _Bones.Count;
			if(_Lights != null) Id += _Lights.Count;
			if(_Helpers != null) Id += _Helpers.Count;
			if(_Attachments != null) Id += _Attachments.Count;
			if(_ParticleEmitters != null) Id += _ParticleEmitters.Count;

			return Id;
		}

		internal int GetRibbonEmitterNodeId(CRibbonEmitter RibbonEmitter)
		{
			if(_RibbonEmitters == null) return CConstants.InvalidId;

			int Id = _RibbonEmitters.IndexOf(RibbonEmitter);
			if(Id == CConstants.InvalidId) return CConstants.InvalidId;

			if(_Bones != null) Id += _Bones.Count;
			if(_Lights != null) Id += _Lights.Count;
			if(_Helpers != null) Id += _Helpers.Count;
			if(_Attachments != null) Id += _Attachments.Count;
			if(_ParticleEmitters != null) Id += _ParticleEmitters.Count;
			if(_ParticleEmitters2 != null) Id += _ParticleEmitters2.Count;

			return Id;
		}

		/// <summary>
		/// Gets or sets the tag data of the model. Tag data is not saved when the model is.
		/// </summary>
		public object Tag
		{
			get
			{
				return _Tag;
			}
			set
			{
				_Tag = value;
			}
		}

		/// <summary>
		/// Gets or sets the version. Should be DefaultModelVersion.
		/// </summary>
		public int Version
		{
			get
			{
				return _Version;
			}
			set
			{
				AddSetModelFieldCommand("_Version", value);
				_Version = value;
			}
		}

		/// <summary>
		/// Gets or sets the blend time.
		/// </summary>
		public int BlendTime
		{
			get
			{
				return _BlendTime;
			}
			set
			{
				AddSetModelFieldCommand("_BlendTime", value);
				_BlendTime = value;
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
				AddSetModelFieldCommand("_Name", value);
				_Name = value;
			}
		}

		/// <summary>
		/// Gets or sets the animation file.
		/// </summary>
		public string AnimationFile
		{
			get
			{
				return _AnimationFile;
			}
			set
			{
				AddSetModelFieldCommand("_AnimationFile", value);
				_AnimationFile = value;
			}
		}

		/// <summary>
		/// Gets or sets the extent.
		/// </summary>
		public Primitives.CExtent Extent
		{
			get
			{
				return _Extent;
			}
			set
			{
				AddSetModelFieldCommand("_Extent", value);
				_Extent = value;
			}
		}

		/// <summary>
		/// Checks if there exists some attachments.
		/// </summary>
		public bool HasAttachments
		{
			get
			{
				return (_Attachments != null) ? (_Attachments.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some bones.
		/// </summary>
		public bool HasBones
		{
			get
			{
				return (_Bones != null) ? (_Bones.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some collision shapes.
		/// </summary>
		public bool HasCollisionShapes
		{
			get
			{
				return (_CollisionShapes != null) ? (_CollisionShapes.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some events.
		/// </summary>
		public bool HasEvents
		{
			get
			{
				return (_Events != null) ? (_Events.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some helpers.
		/// </summary>
		public bool HasHelpers
		{
			get
			{
				return (_Helpers != null) ? (_Helpers.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some lights.
		/// </summary>
		public bool HasLights
		{
			get
			{
				return (_Lights != null) ? (_Lights.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some particle emitters.
		/// </summary>
		public bool HasParticleEmitters
		{
			get
			{
				return (_ParticleEmitters != null) ? (_ParticleEmitters.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some particle emitters 2.
		/// </summary>
		public bool HasParticleEmitters2
		{
			get
			{
				return (_ParticleEmitters2 != null) ? (_ParticleEmitters2.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some ribbon emitters.
		/// </summary>
		public bool HasRibbonEmitters
		{
			get
			{
				return (_RibbonEmitters != null) ? (_RibbonEmitters.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some cameras.
		/// </summary>
		public bool HasCameras
		{
			get
			{
				return (_Cameras != null) ? (_Cameras.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some geosets.
		/// </summary>
		public bool HasGeosets
		{
			get
			{
				return (_Geosets != null) ? (_Geosets.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some geoset animations.
		/// </summary>
		public bool HasGeosetAnimations
		{
			get
			{
				return (_GeosetAnimations != null) ? (_GeosetAnimations.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some global sequences.
		/// </summary>
		public bool HasGlobalSequences
		{
			get
			{
				return (_GlobalSequences != null) ? (_GlobalSequences.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some materials.
		/// </summary>
		public bool HasMaterials
		{
			get
			{
				return (_Materials != null) ? (_Materials.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some sequences.
		/// </summary>
		public bool HasSequences
		{
			get
			{
				return (_Sequences != null) ? (_Sequences.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some textures.
		/// </summary>
		public bool HasTextures
		{
			get
			{
				return (_Textures != null) ? (_Textures.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some texture animations.
		/// </summary>
		public bool HasTextureAnimations
		{
			get
			{
				return (_TextureAnimations != null) ? (_TextureAnimations.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some nodes.
		/// </summary>
		public bool HasNodes
		{
			get
			{
				return (_Nodes != null) ? (_Nodes.Count > 0) : false;
			}
		}

		/// <summary>
		/// Checks if there exists some metadata.
		/// </summary>
		public bool HasMetaData
		{
			get
			{
				if(_MetaData == null) return false;
				if(_MetaData.DocumentElement == null) return false;

				return (_MetaData.DocumentElement.ChildNodes.Count > 0);
			}
		}

		/// <summary>
		/// Retrieves the attachments container.
		/// </summary>
		public CObjectContainer<CAttachment> Attachments
		{
			get
			{
				return _Attachments ?? (_Attachments = new CObjectContainer<CAttachment>(this));
			}
		}

		/// <summary>
		/// Retrieves the bones container.
		/// </summary>
		public CObjectContainer<CBone> Bones
		{
			get
			{
				return _Bones ?? (_Bones = new CObjectContainer<CBone>(this));
			}
		}

		/// <summary>
		/// Retrieves the collision shapes container.
		/// </summary>
		public CObjectContainer<CCollisionShape> CollisionShapes
		{
			get
			{
				return _CollisionShapes ?? (_CollisionShapes = new CObjectContainer<CCollisionShape>(this));
			}
		}

		/// <summary>
		/// Retrieves the events container.
		/// </summary>
		public CObjectContainer<CEvent> Events
		{
			get
			{
				return _Events ?? (_Events = new CObjectContainer<CEvent>(this));
			}
		}

		/// <summary>
		/// Retrieves the helpers container.
		/// </summary>
		public CObjectContainer<CHelper> Helpers
		{
			get
			{
				return _Helpers ?? (_Helpers = new CObjectContainer<CHelper>(this));
			}
		}

		/// <summary>
		/// Retrieves the lights container.
		/// </summary>
		public CObjectContainer<CLight> Lights
		{
			get
			{
				return _Lights ?? (_Lights = new CObjectContainer<CLight>(this));
			}
		}

		/// <summary>
		/// Retrieves the particle emitters container.
		/// </summary>
		public CObjectContainer<CParticleEmitter> ParticleEmitters
		{
			get
			{
				return _ParticleEmitters ?? (_ParticleEmitters = new CObjectContainer<CParticleEmitter>(this));
			}
		}

		/// <summary>
		/// Retrieves the particle emitters 2 container.
		/// </summary>
		public CObjectContainer<CParticleEmitter2> ParticleEmitters2
		{
			get
			{
				return _ParticleEmitters2 ?? (_ParticleEmitters2 = new CObjectContainer<CParticleEmitter2>(this));
			}
		}

		/// <summary>
		/// Retrieves the ribbon emitters container.
		/// </summary>
		public CObjectContainer<CRibbonEmitter> RibbonEmitters
		{
			get
			{
				return _RibbonEmitters ?? (_RibbonEmitters = new CObjectContainer<CRibbonEmitter>(this));
			}
		}

		/// <summary>
		/// Retrieves the cameras container.
		/// </summary>
		public CObjectContainer<CCamera> Cameras
		{
			get
			{
				return _Cameras ?? (_Cameras = new CObjectContainer<CCamera>(this));
			}
		}

		/// <summary>
		/// Retrieves the geosets container.
		/// </summary>
		public CObjectContainer<CGeoset> Geosets
		{
			get
			{
				return _Geosets ?? (_Geosets = new CObjectContainer<CGeoset>(this));
			}
		}

		/// <summary>
		/// Retrieves the geoset animations container.
		/// </summary>
		public CObjectContainer<CGeosetAnimation> GeosetAnimations
		{
			get
			{
				return _GeosetAnimations ?? (_GeosetAnimations = new CObjectContainer<CGeosetAnimation>(this));
			}
		}

		/// <summary>
		/// Retrieves the global sequences container.
		/// </summary>
		public CObjectContainer<CGlobalSequence> GlobalSequences
		{
			get
			{
				return _GlobalSequences ?? (_GlobalSequences = new CObjectContainer<CGlobalSequence>(this));
			}
		}

		/// <summary>
		/// Retrieves the materials container.
		/// </summary>
		public CObjectContainer<CMaterial> Materials
		{
			get
			{
				return _Materials ?? (_Materials = new CObjectContainer<CMaterial>(this));
			}
		}

		/// <summary>
		/// Retrieves the sequences container.
		/// </summary>
		public CObjectContainer<CSequence> Sequences
		{
			get
			{
				return _Sequences ?? (_Sequences = new CObjectContainer<CSequence>(this));
			}
		}

		/// <summary>
		/// Retrieves the textures container.
		/// </summary>
		public CObjectContainer<CTexture> Textures
		{
			get
			{
				return _Textures ?? (_Textures = new CObjectContainer<CTexture>(this));
			}
		}

		/// <summary>
		/// Retrieves the texture animations container.
		/// </summary>
		public CObjectContainer<CTextureAnimation> TextureAnimations
		{
			get
			{
				return _TextureAnimations ?? (_TextureAnimations = new CObjectContainer<CTextureAnimation>(this));
			}
		}

		/// <summary>
		/// Retrieves the nodes container.
		/// </summary>
		public CNodeContainer Nodes
		{
			get
			{
				return _Nodes ?? (_Nodes = new CNodeContainer(this));
			}
		}

		/// <summary>
		/// Retrieves the metadata document. Metadata is used to store custom (hidden)
		/// data which is normally not in the model. Metadata is not handled by the
		/// undo/redo command chain.
		/// </summary>
		public System.Xml.XmlDocument MetaData
		{
			get
			{
				if(_MetaData == null)
				{
					_MetaData = new System.Xml.XmlDocument();
					_MetaData.AppendChild(_MetaData.CreateElement("meta"));
				}

				return _MetaData;
			}
		}

		/// <summary>
		/// Retrieves the root element of the metadata document. This should always
		/// be "meta".
		/// </summary>
		public System.Xml.XmlElement MetaDataRoot
		{
			get
			{
				if(_MetaData == null)
				{
					_MetaData = new System.Xml.XmlDocument();
					_MetaData.AppendChild(_MetaData.CreateElement("meta"));
				}

				return _MetaData.DocumentElement;
			}
		}

		private object _Tag = null;

		private int _Version = CConstants.DefaultModelVersion;
		private int _BlendTime = CConstants.DefaultModelBlendTime;
		private string _Name = "";
		private string _AnimationFile = "";
		private Primitives.CExtent _Extent = CConstants.DefaultExtent;

		private CObjectContainer<CAttachment> _Attachments = null;
		private CObjectContainer<CBone> _Bones = null;
		private CObjectContainer<CCollisionShape> _CollisionShapes = null;
		private CObjectContainer<CEvent> _Events = null;
		private CObjectContainer<CHelper> _Helpers = null;
		private CObjectContainer<CLight> _Lights = null;
		private CObjectContainer<CParticleEmitter> _ParticleEmitters = null;
		private CObjectContainer<CParticleEmitter2> _ParticleEmitters2 = null;
		private CObjectContainer<CRibbonEmitter> _RibbonEmitters = null;

		private CObjectContainer<CCamera> _Cameras = null;
		private CObjectContainer<CGeoset> _Geosets = null;
		private CObjectContainer<CGeosetAnimation> _GeosetAnimations = null;
		private CObjectContainer<CGlobalSequence> _GlobalSequences = null;
		private CObjectContainer<CMaterial> _Materials = null;
		private CObjectContainer<CSequence> _Sequences = null;
		private CObjectContainer<CTexture> _Textures = null;
		private CObjectContainer<CTextureAnimation> _TextureAnimations = null;

		private CNodeContainer _Nodes = null;
		private System.Xml.XmlDocument _MetaData = null;

		internal Command.CCommandGroup CommandGroup = null;
	}
}
