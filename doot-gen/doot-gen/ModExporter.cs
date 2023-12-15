using doot_gen.util;
using Optional;
using Optional.Unsafe;
using RingingBloom.WWiseTypes;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;

namespace doot_gen.doot_gen
{
    static internal class ModExporter
    {


        public static void ExportMod(Horns horn, IEnumerable<BankData> bankEnum, string outputFolder, string wwiseConsolePath, string wwiseProjectPath)
        {
            ExportMod(horn.Some(), bankEnum, outputFolder, wwiseConsolePath, wwiseProjectPath);
        }


        public static void ExportMod(IEnumerable<BankData> bankEnum, string outputFolder, string wwiseConsolePath, string wwiseProjectPath)
        {
            ExportMod(Option.None<Horns>(), bankEnum, outputFolder, wwiseConsolePath, wwiseProjectPath);
        }

        private static void ExportMod(Option<Horns> horn, IEnumerable<BankData> bankEnum, string outputFolder, string wwiseConsolePath, string wwiseProjectPath)
        {
            Logger.Info("Starting Exporting Mod:");
            var banks = bankEnum.ToList();
            // convert files to wem
            string outputPath = Directory.GetCurrentDirectory() + "/tmp/";
            string sourcesPath = Directory.GetCurrentDirectory() + "/tmp/Wems.wsources";

            Logger.Info("Collecting WSources");
            WSource.MakeWSource(sourcesPath, banks.SelectMany(bank => bank.GetReplacementFilePaths()).ToHashSet().ToList());

            Logger.Info("Started conversion process ({0})", wwiseConsolePath);
            var argsBuilder = new StringBuilder();
            argsBuilder.Append("convert-external-source ");
            argsBuilder.AppendFormat("\"{0}\" ", wwiseProjectPath);
            argsBuilder.AppendFormat("--output \"{0}\" ", outputPath);
            argsBuilder.AppendFormat("--source-file \"{0}\" ", sourcesPath);
            Process process = Process.Start(wwiseConsolePath, argsBuilder.ToString());
            process.WaitForExit();

            // create banks
            outputPath += "Windows/";
            string modPath = outputPath + "mod\\";
            string modSoundPath = modPath + "natives\\STM\\Sound\\Wwise";
            if (horn.HasValue && !Directory.Exists(modSoundPath))
            { 
                Logger.Info("Creating Directory {0}", modSoundPath);
                Directory.CreateDirectory(modSoundPath);
            }

            Logger.Info("Started exporting bnks");
            banks.ForEach(bnk => bnk.ExportReplacementsTo(horn.HasValue ? modSoundPath : outputFolder, outputPath));
            if (!horn.HasValue)
            {
                Logger.Info("Exported Mod successfull without horn!");
                return;
            }
            // create infos
            {
                Logger.Info("Creating Modinfo");
                string modinfo = modPath + "modinfo.ini";
                if (File.Exists(modinfo))
                {
                    File.Delete(modinfo);
                }

                using (FileStream fs = File.Create(modinfo))
                {
                    // Add some text to file

                    byte[] name = new UTF8Encoding(true).GetBytes("name=HH " + horn.ValueOrFailure().GetHornName() + " Sound Replacement\n");
                    fs.Write(name, 0, name.Length);
                    byte[] description = new UTF8Encoding(true).GetBytes("description=Swaps the melodies of this Hunting Horn\n");
                    fs.Write(description, 0, description.Length);
                    byte[] author = new UTF8Encoding(true).GetBytes("author=Darkblizzard21/mh-doot-gen\n");
                    fs.Write(author, 0, author.Length);
                    byte[] category = new UTF8Encoding(true).GetBytes("category=Sounds\n");
                    fs.Write(category, 0, category.Length);
                }
            }
            // copy image //todo add auto generated images
            {
                Logger.Info("Adding Image through copy");
                string imgDest = modPath + "screenshot.png";
                string imgSrc = Directory.GetCurrentDirectory() + "\\mhdootgen.png";
                if (!File.Exists(imgSrc)) { imgSrc = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\mhdootgen.png"; }
                if (File.Exists(imgSrc))
                {
                    File.Copy(imgSrc, imgDest, true);
                }
            }
            // zip together & save zip
            string zipPath = "HH " + horn.ValueOrFailure().GetHornName() + " Sound Replacement.zip";
            zipPath.Replace(' ', '-');
            zipPath = Path.Combine(outputFolder ,zipPath);
            Logger.Info("Zipping Mod to {0}", zipPath);
            if (File.Exists(zipPath)) File.Delete(zipPath);
            ZipFile.CreateFromDirectory(modPath, zipPath);
            // clear tmp files
            Logger.Info("Clearing temporary files");
            File.Delete(sourcesPath);
            Directory.Delete(outputPath, true);
            Logger.Info("Exported Mod successfull with horn!");
        }
    }
}
