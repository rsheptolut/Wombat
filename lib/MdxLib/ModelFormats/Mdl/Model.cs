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
namespace MdxLib.ModelFormats.Mdl
{
	internal sealed class CModel : CObject
	{
		private CModel()
		{
			//Empty
		}

		public void Load(CLoader Loader, Model.CModel Model)
		{
			string Tag = "";
			System.Collections.Generic.List<Primitives.CVector3> PivotPointList = new System.Collections.Generic.List<Primitives.CVector3>();

			while(true)
			{
				try
				{
					if(Loader.PeekToken() == Token.EType.MetaComment)
					{
						Tag = "metacomment";
					}
					else
					{
						Tag = Loader.ReadWord();
					}
				}
				catch(System.Exception Exception)
				{
					if(!Loader.Eof) throw Exception;
					break;
				}

				switch(Tag)
				{
					case "version": { CModelVersion.Instance.Load(Loader, Model); break; }
					case "model": { CModelInfo.Instance.Load(Loader, Model); break; }
					case "sequences": { CSequence.Instance.LoadAll(Loader, Model); break; }
					case "globalsequences": { CGlobalSequence.Instance.LoadAll(Loader, Model); break; }
					case "textures": { CTexture.Instance.LoadAll(Loader, Model); break; }
					case "materials": { CMaterial.Instance.LoadAll(Loader, Model); break; }
					case "textureanims": { CTextureAnimation.Instance.LoadAll(Loader, Model); break; }
					case "geoset": { CGeoset.Instance.LoadAll(Loader, Model); break; }
					case "geosetanim": { CGeosetAnimation.Instance.LoadAll(Loader, Model); break; }
					case "bone": { CBone.Instance.LoadAll(Loader, Model); break; }
					case "light": { CLight.Instance.LoadAll(Loader, Model); break; }
					case "helper": { CHelper.Instance.LoadAll(Loader, Model); break; }
					case "attachment": { CAttachment.Instance.LoadAll(Loader, Model); break; }
					case "pivotpoints": { CPivotPoint.Instance.Load(Loader, Model, PivotPointList); break; }
					case "particleemitter": { CParticleEmitter.Instance.LoadAll(Loader, Model); break; }
					case "particleemitter2": { CParticleEmitter2.Instance.LoadAll(Loader, Model); break; }
					case "ribbonemitter": { CRibbonEmitter.Instance.LoadAll(Loader, Model); break; }
					case "eventobject": { CEvent.Instance.LoadAll(Loader, Model); break; }
					case "camera": { CCamera.Instance.LoadAll(Loader, Model); break; }
					case "collisionshape": { CCollisionShape.Instance.LoadAll(Loader, Model); break; }
					case "metacomment": { CMetaData.Instance.Load(Loader, Model); break; }
				}
			}

			SetPivotPoints(Model, PivotPointList);
		}

		public void Save(CSaver Saver, Model.CModel Model)
		{
			System.Collections.Generic.List<Primitives.CVector3> PivotPointList = new System.Collections.Generic.List<Primitives.CVector3>();

			GetPivotPoints(Model, PivotPointList);

			CModelVersion.Instance.Save(Saver, Model);
			CModelInfo.Instance.Save(Saver, Model);
			CSequence.Instance.SaveAll(Saver, Model);
			CGlobalSequence.Instance.SaveAll(Saver, Model);
			CTexture.Instance.SaveAll(Saver, Model);
			CMaterial.Instance.SaveAll(Saver, Model);
			CTextureAnimation.Instance.SaveAll(Saver, Model);
			CGeoset.Instance.SaveAll(Saver, Model);
			CGeosetAnimation.Instance.SaveAll(Saver, Model);
			CBone.Instance.SaveAll(Saver, Model);
			CLight.Instance.SaveAll(Saver, Model);
			CHelper.Instance.SaveAll(Saver, Model);
			CAttachment.Instance.SaveAll(Saver, Model);
			CPivotPoint.Instance.Save(Saver, Model, PivotPointList);
			CParticleEmitter.Instance.SaveAll(Saver, Model);
			CParticleEmitter2.Instance.SaveAll(Saver, Model);
			CRibbonEmitter.Instance.SaveAll(Saver, Model);
			CEvent.Instance.SaveAll(Saver, Model);
			CCamera.Instance.SaveAll(Saver, Model);
			CCollisionShape.Instance.SaveAll(Saver, Model);
			CMetaData.Instance.Save(Saver, Model);
		}

		private void SetPivotPoints(Model.CModel Model, System.Collections.Generic.ICollection<Primitives.CVector3> PivotPointList)
		{
			int Index = 0;
			int NrOfNodes = Model.Nodes.Count;

			foreach(Primitives.CVector3 PivotPoint in PivotPointList)
			{
				if(Index >= NrOfNodes) break;
				Model.Nodes[Index].PivotPoint = PivotPoint;
				Index++;
			}
		}

		private void GetPivotPoints(Model.CModel Model, System.Collections.Generic.ICollection<Primitives.CVector3> PivotPointList)
		{
			foreach(Model.INode Node in Model.Nodes)
			{
				PivotPointList.Add(Node.PivotPoint);
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
