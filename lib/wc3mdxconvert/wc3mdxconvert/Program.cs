using MdxLib.Model;
using MdxLib.ModelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wc3mdxconvert
{
    class Program
    {
        static void WriteUsage()
        {
            Console.WriteLine("Usage: ");
            Console.WriteLine("wc3mdxconvert <srcfile> <format>|<destfile");
            Console.WriteLine("Where format can be: mdx, mdl (default), xml");
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0 || args[0] == "/?" || args[0] == "-h" || args[0] == "-?" || args[0] == "/h")
                {
                    WriteUsage();
                    return;
                }
                string SrcFile = args[0];
                string DestFormat = "mdl";
                string DestFile = "";
                if (args.Length > 1)
                    DestFile = args[1];
                DestFormat = Path.GetExtension(DestFile).ToLower();
                if (DestFormat == "")
                {
                    DestFormat = DestFile;
                    DestFile = Path.ChangeExtension(SrcFile, DestFormat);
                }
                if (!File.Exists(SrcFile))
                    throw new ApplicationException(String.Format("File {0} does not exist.", SrcFile));

                if (DestFormat != "xml" && DestFormat != "mdx" && DestFormat != "mdl")
                    throw new ApplicationException(String.Format("Format {0} is not supported.", DestFormat));

                var Model = new CModel();
                

                using (var ModelFS = new FileStream(SrcFile, FileMode.Open, FileAccess.Read))
                {
                    IModelFormat ModelFormat;
                    if (Path.GetExtension(SrcFile).ToLower() == ".mdx")
                        ModelFormat = new CMdx();
                    else if (Path.GetExtension(SrcFile).ToLower() == ".mdl")
                        ModelFormat = new CMdl();
                    else
                        if (Path.GetExtension(SrcFile).ToLower() == ".xml")
                            ModelFormat = new CXml();
                        else
                            throw new Exception("Unsupported model format. File: " + SrcFile);

                    ModelFormat.Load(SrcFile, ModelFS, Model);
                }

                using (var ModelFS = new FileStream(DestFile, FileMode.Create, FileAccess.ReadWrite))
                {
                    IModelFormat ModelFormat;
                    if (DestFormat == "mdx")
                        ModelFormat = new CMdx();
                    else if (DestFormat == "mdl")
                        ModelFormat = new CMdl();
                    else if (DestFormat == "xml")
                        ModelFormat = new CXml();
                    else
                        throw new Exception("Unsupported model format: " + DestFormat);

                    ModelFormat.Save(Model.Name, ModelFS, Model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine("-------------------");
                Console.WriteLine(ex.Message);
                Console.WriteLine("-------------------");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("-------------------");
                return;
            }
        }


    }
}
