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
namespace MdxLib.ModelFormats.Mdx
{
	internal sealed class CModel
	{
		private CModel()
		{
			//Empty
		}

		public void Load(CLoader Loader, Model.CModel Model)
		{
			System.Collections.Generic.List<Primitives.CVector3> PivotPointList = new System.Collections.Generic.List<Primitives.CVector3>();

			Loader.ExpectTag("MDLX");

			while(true)
			{
				string Tag = "";

				try
				{
					if(Loader.Eof()) break;
					Tag = Loader.ReadTag();
				}
				catch(System.IO.EndOfStreamException)
				{
					break;
				}

				switch(Tag)
				{
					case "VERS": { CModelVersion.Instance.Load(Loader, Model); break; }
					case "MODL": { CModelInfo.Instance.Load(Loader, Model); break; }
					case "SEQS": { CSequence.Instance.LoadAll(Loader, Model); break; }
					case "GLBS": { CGlobalSequence.Instance.LoadAll(Loader, Model); break; }
					case "TEXS": { CTexture.Instance.LoadAll(Loader, Model); break; }
					case "MTLS": { CMaterial.Instance.LoadAll(Loader, Model); break; }
					case "TXAN": { CTextureAnimation.Instance.LoadAll(Loader, Model); break; }
					case "GEOS": { CGeoset.Instance.LoadAll(Loader, Model); break; }
					case "GEOA": { CGeosetAnimation.Instance.LoadAll(Loader, Model); break; }
					case "BONE": { CBone.Instance.LoadAll(Loader, Model); break; }
					case "LITE": { CLight.Instance.LoadAll(Loader, Model); break; }
					case "HELP": { CHelper.Instance.LoadAll(Loader, Model); break; }
					case "ATCH": { CAttachment.Instance.LoadAll(Loader, Model); break; }
					case "PIVT": { CPivotPoint.Instance.Load(Loader, Model, PivotPointList); break; }
					case "PREM": { CParticleEmitter.Instance.LoadAll(Loader, Model); break; }
					case "PRE2": { CParticleEmitter2.Instance.LoadAll(Loader, Model); break; }
					case "RIBB": { CRibbonEmitter.Instance.LoadAll(Loader, Model); break; }
					case "EVTS": { CEvent.Instance.LoadAll(Loader, Model); break; }
					case "CAMS": { CCamera.Instance.LoadAll(Loader, Model); break; }
					case "CLID": { CCollisionShape.Instance.LoadAll(Loader, Model); break; }
					case "META": { CMetaData.Instance.Load(Loader, Model); break; }

					default:
					{
						Loader.Skip(Loader.ReadInt32());
						break;
					}
				}
			}

			SetPivotPoints(Model, PivotPointList);
		}

		public void Save(CSaver Saver, Model.CModel Model, string Info)
		{
			System.Collections.Generic.List<Primitives.CVector3> PivotPointList = new System.Collections.Generic.List<Primitives.CVector3>();

			GetPivotPoints(Model, PivotPointList);
			Saver.WriteTag("MDLX");

			Saver.WriteTag("INFO");
			Saver.PushLocation();
			Saver.WriteString(Info, Info.Length);
			Saver.PopExclusiveLocation();

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
