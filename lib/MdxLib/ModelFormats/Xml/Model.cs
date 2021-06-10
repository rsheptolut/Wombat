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
namespace MdxLib.ModelFormats.Xml
{
	internal sealed class CModel : CObject
	{
		private CModel()
		{
			//Empty
		}

		public void Load(CLoader Loader, System.Xml.XmlNode Node, Model.CModel Model)
		{
			Model.Version = ReadInteger(Node, "version", Model.Version);
			Model.BlendTime = ReadInteger(Node, "blend_time", Model.BlendTime);
			Model.Name = ReadString(Node, "name", Model.Name);
			Model.AnimationFile = ReadString(Node, "animation_file", Model.AnimationFile);
			Model.Extent = ReadExtent(Node, "extent", Model.Extent);

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("sequence"))
			{
				Model.CSequence Sequence = new Model.CSequence(Model);
				CSequence.Instance.Load(Loader, ChildNode, Model, Sequence);
				Model.Sequences.Add(Sequence);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("global_sequence"))
			{
				Model.CGlobalSequence GlobalSequence = new Model.CGlobalSequence(Model);
				CGlobalSequence.Instance.Load(Loader, ChildNode, Model, GlobalSequence);
				Model.GlobalSequences.Add(GlobalSequence);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("material"))
			{
				Model.CMaterial Material = new Model.CMaterial(Model);
				CMaterial.Instance.Load(Loader, ChildNode, Model, Material);
				Model.Materials.Add(Material);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("texture"))
			{
				Model.CTexture Texture = new Model.CTexture(Model);
				CTexture.Instance.Load(Loader, ChildNode, Model, Texture);
				Model.Textures.Add(Texture);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("texture_animation"))
			{
				Model.CTextureAnimation TextureAnimation = new Model.CTextureAnimation(Model);
				CTextureAnimation.Instance.Load(Loader, ChildNode, Model, TextureAnimation);
				Model.TextureAnimations.Add(TextureAnimation);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("geoset"))
			{
				Model.CGeoset Geoset = new Model.CGeoset(Model);
				CGeoset.Instance.Load(Loader, ChildNode, Model, Geoset);
				Model.Geosets.Add(Geoset);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("geoset_animation"))
			{
				Model.CGeosetAnimation GeosetAnimation = new Model.CGeosetAnimation(Model);
				CGeosetAnimation.Instance.Load(Loader, ChildNode, Model, GeosetAnimation);
				Model.GeosetAnimations.Add(GeosetAnimation);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("bone"))
			{
				Model.CBone Bone = new Model.CBone(Model);
				CBone.Instance.Load(Loader, ChildNode, Model, Bone);
				Model.Bones.Add(Bone);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("light"))
			{
				Model.CLight Light = new Model.CLight(Model);
				CLight.Instance.Load(Loader, ChildNode, Model, Light);
				Model.Lights.Add(Light);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("helper"))
			{
				Model.CHelper Helper = new Model.CHelper(Model);
				CHelper.Instance.Load(Loader, ChildNode, Model, Helper);
				Model.Helpers.Add(Helper);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("attachment"))
			{
				Model.CAttachment Attachment = new Model.CAttachment(Model);
				CAttachment.Instance.Load(Loader, ChildNode, Model, Attachment);
				Model.Attachments.Add(Attachment);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("particle_emitter"))
			{
				Model.CParticleEmitter ParticleEmitter = new Model.CParticleEmitter(Model);
				CParticleEmitter.Instance.Load(Loader, ChildNode, Model, ParticleEmitter);
				Model.ParticleEmitters.Add(ParticleEmitter);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("particle_emitter_2"))
			{
				Model.CParticleEmitter2 ParticleEmitter2 = new Model.CParticleEmitter2(Model);
				CParticleEmitter2.Instance.Load(Loader, ChildNode, Model, ParticleEmitter2);
				Model.ParticleEmitters2.Add(ParticleEmitter2);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("ribbon_emitter"))
			{
				Model.CRibbonEmitter RibbonEmitter = new Model.CRibbonEmitter(Model);
				CRibbonEmitter.Instance.Load(Loader, ChildNode, Model, RibbonEmitter);
				Model.RibbonEmitters.Add(RibbonEmitter);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("camera"))
			{
				Model.CCamera Camera = new Model.CCamera(Model);
				CCamera.Instance.Load(Loader, ChildNode, Model, Camera);
				Model.Cameras.Add(Camera);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("event"))
			{
				Model.CEvent Event = new Model.CEvent(Model);
				CEvent.Instance.Load(Loader, ChildNode, Model, Event);
				Model.Events.Add(Event);
			}

			foreach(System.Xml.XmlNode ChildNode in Node.SelectNodes("collision_shape"))
			{
				Model.CCollisionShape CollisionShape = new Model.CCollisionShape(Model);
				CCollisionShape.Instance.Load(Loader, ChildNode, Model, CollisionShape);
				Model.CollisionShapes.Add(CollisionShape);
			}

			System.Xml.XmlNode MetaNode = Node.SelectSingleNode("meta");
			if((MetaNode != null) && (MetaNode.ChildNodes.Count > 0))
			{
				System.Xml.XmlNode ImportedNode = Model.MetaData.ImportNode(MetaNode, true);
				Model.MetaData.ReplaceChild(ImportedNode, Model.MetaDataRoot);
			}
		}

		public void Save(CSaver Saver, System.Xml.XmlNode Node, Model.CModel Model)
		{
			WriteInteger(Node, "version", Model.Version);
			WriteInteger(Node, "blend_time", Model.BlendTime);
			WriteString(Node, "name", Model.Name);
			WriteString(Node, "animation_file", Model.AnimationFile);
			WriteExtent(Node, "extent", Model.Extent);

			if(Model.HasSequences)
			{
				foreach(Model.CSequence Sequence in Model.Sequences)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "sequence");
					CSequence.Instance.Save(Saver, Element, Model, Sequence);
				}
			}

			if(Model.HasGlobalSequences)
			{
				foreach(Model.CGlobalSequence GlobalSequence in Model.GlobalSequences)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "global_sequence");
					CGlobalSequence.Instance.Save(Saver, Element, Model, GlobalSequence);
				}
			}

			if(Model.HasMaterials)
			{
				foreach(Model.CMaterial Material in Model.Materials)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "material");
					CMaterial.Instance.Save(Saver, Element, Model, Material);
				}
			}

			if(Model.HasTextures)
			{
				foreach(Model.CTexture Texture in Model.Textures)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "texture");
					CTexture.Instance.Save(Saver, Element, Model, Texture);
				}
			}

			if(Model.HasTextureAnimations)
			{
				foreach(Model.CTextureAnimation TextureAnimation in Model.TextureAnimations)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "texture_animation");
					CTextureAnimation.Instance.Save(Saver, Element, Model, TextureAnimation);
				}
			}

			if(Model.HasGeosets)
			{
				foreach(Model.CGeoset Geoset in Model.Geosets)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "geoset");
					CGeoset.Instance.Save(Saver, Element, Model, Geoset);
				}
			}

			if(Model.HasGeosetAnimations)
			{
				foreach(Model.CGeosetAnimation GeosetAnimation in Model.GeosetAnimations)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "geoset_animation");
					CGeosetAnimation.Instance.Save(Saver, Element, Model, GeosetAnimation);
				}
			}

			if(Model.HasBones)
			{
				foreach(Model.CBone Bone in Model.Bones)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "bone");
					CBone.Instance.Save(Saver, Element, Model, Bone);
				}
			}

			if(Model.HasLights)
			{
				foreach(Model.CLight Light in Model.Lights)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "light");
					CLight.Instance.Save(Saver, Element, Model, Light);
				}
			}

			if(Model.HasHelpers)
			{
				foreach(Model.CHelper Helper in Model.Helpers)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "helper");
					CHelper.Instance.Save(Saver, Element, Model, Helper);
				}
			}

			if(Model.HasAttachments)
			{
				foreach(Model.CAttachment Attachment in Model.Attachments)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "attachment");
					CAttachment.Instance.Save(Saver, Element, Model, Attachment);
				}
			}

			if(Model.HasParticleEmitters)
			{
				foreach(Model.CParticleEmitter ParticleEmitter in Model.ParticleEmitters)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "particle_emitter");
					CParticleEmitter.Instance.Save(Saver, Element, Model, ParticleEmitter);
				}
			}

			if(Model.HasParticleEmitters2)
			{
				foreach(Model.CParticleEmitter2 ParticleEmitter2 in Model.ParticleEmitters2)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "particle_emitter_2");
					CParticleEmitter2.Instance.Save(Saver, Element, Model, ParticleEmitter2);
				}
			}

			if(Model.HasRibbonEmitters)
			{
				foreach(Model.CRibbonEmitter RibbonEmitter in Model.RibbonEmitters)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "ribbon_emitter");
					CRibbonEmitter.Instance.Save(Saver, Element, Model, RibbonEmitter);
				}
			}

			if(Model.HasCameras)
			{
				foreach(Model.CCamera Camera in Model.Cameras)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "camera");
					CCamera.Instance.Save(Saver, Element, Model, Camera);
				}
			}

			if(Model.HasEvents)
			{
				foreach(Model.CEvent Event in Model.Events)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "event");
					CEvent.Instance.Save(Saver, Element, Model, Event);
				}
			}

			if(Model.HasCollisionShapes)
			{
				foreach(Model.CCollisionShape CollisionShape in Model.CollisionShapes)
				{
					System.Xml.XmlElement Element = AppendElement(Node, "collision_shape");
					CCollisionShape.Instance.Save(Saver, Element, Model, CollisionShape);
				}
			}

			if(Model.HasMetaData)
			{
				System.Xml.XmlNode ImportedNode = Node.OwnerDocument.ImportNode(Model.MetaDataRoot, true);
				Node.OwnerDocument.DocumentElement.AppendChild(ImportedNode);
			}
		}

		public static CModel Instance
		{
			get
			{
				return CSingleton.Instance;
			}
		}

		private static class CSingleton
		{
			static CSingleton() { }
			public static CModel Instance = new CModel();
		}
	}
}
