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
namespace MdxLib.ModelFormats
{
	/// <summary>
	/// Handles the MDL model format. Can load and save MDL models.
	/// </summary>
	public sealed class CMdl : IModelFormat
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CMdl()
		{
			//Empty
		}

		/// <summary>
		/// Loads a model from a stream.
		/// </summary>
		/// <param name="Name">The name of the model (only used in some error messages)</param>
		/// <param name="Stream">The stream to load from (must support reading)</param>
		/// <param name="Model">The model to load to (must be an empty model)</param>
		public void Load(string Name, System.IO.Stream Stream, Model.CModel Model)
		{
			if(!Stream.CanRead) throw new System.NotSupportedException("Unable to load \"" + Name + "\", the stream does not support reading!");
			
			Mdl.CLoader Loader = new Mdl.CLoader(Name, Stream);
			Mdl.CModel.Instance.Load(Loader, Model);
			Loader.Attacher.Attach();
		}

		/// <summary>
		/// Saves a model to a stream.
		/// </summary>
		/// <param name="Name">The name of the model (only used in some error messages)</param>
		/// <param name="Stream">The stream to save to (must support writing)</param>
		/// <param name="Model">The model to save from</param>
		public void Save(string Name, System.IO.Stream Stream, Model.CModel Model)
		{
			if(!Stream.CanWrite) throw new System.NotSupportedException("Unable to save \"" + Name + "\", the stream does not support writing!");

			Mdl.CSaver Saver = new Mdl.CSaver(Name, Stream);
			Saver.WriteWord(BuildHeader(Name));
			Mdl.CModel.Instance.Save(Saver, Model);
			Saver.WriteToStream();
		}

		private string BuildHeader(string Name)
		{
			System.Text.StringBuilder Header = new System.Text.StringBuilder();

			Header.AppendLine("//+-----------------------------------------------------------------------------");
			Header.AppendLine("//|");
			Header.AppendLine("//| " + Name.Replace("\n", "").Replace("\r", ""));
			Header.AppendLine("//| " + CConstants.HeaderFullName);
			Header.AppendLine("//| " + System.DateTime.Now.ToString(CConstants.HeaderDateFormat));
			Header.AppendLine("//| " + CConstants.HeaderUrl);
			Header.AppendLine("//|");
			Header.AppendLine("//+-----------------------------------------------------------------------------");

			return Header.ToString();
		}
	}
}
