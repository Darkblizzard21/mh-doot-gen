using doot_gen.util;
using Optional;
using Optional.Collections;
using Optional.Unsafe;
using RingingBloom;
using RingingBloom.Common;
using System.Diagnostics;


namespace doot_gen.doot_gen
{
    internal class BankData(string name, NBNKFile file)
    {
        struct Replacement
        {
            public Replacement(string path)
            {
                if (!File.Exists(path)) { throw new Exception("Replacement can not be created because file dose not exist."); }
                this.path = path;
                this.filename = Path.GetFileName(path);
                this.player = new System.Media.SoundPlayer(path);
            }

            public readonly string path;
            public readonly string filename;
            public readonly System.Media.SoundPlayer player;
        }

        string name = name;
        NBNKFile file = file;
        Dictionary<string, Replacement> replacements = new Dictionary<string, Replacement>();
        Dictionary<string, System.Media.SoundPlayer> originalSounds = new Dictionary<string, System.Media.SoundPlayer>();


        public Option<string> selectedWem { get; private set; }
        public bool selectedHasReplacement { get => selectedWem.FlatMap(str => replacements.GetValueOrNone(str)).HasValue; }
        public string selectedReplacementName { get => selectedWem.FlatMap(str => replacements.GetValueOrNone(str)).Map(rep => rep.filename).ValueOr("Select replacement file"); }


        public void SelectWem(string wem)
        {
            if (!GetWemNames().Contains(wem)) { throw new Exception("wem not in possible wems"); }
            selectedWem = wem.Some();
        }

        public void ReplaceSelected(string replacementFile)
        {
            if (!selectedWem.HasValue) { throw new Exception("nothing is selected"); }
            replacements[selectedWem.ValueOrFailure()] = new Replacement(replacementFile);
        }

        public void RemoveSelectedReplacement()
        {
            if (!selectedWem.HasValue) { throw new Exception("nothing is selected"); }
            selectedWem.DoIfPresent(wem => replacements.Remove(wem));

        }

        public void PlaySelectedReplacement()
        {
            selectedWem.DoIfPresent(str =>
            {
                replacements.GetValueOrNone(str).DoIfPresent(rep => rep.player.Play());
            });
        }

        public IEnumerable<string> GetWemNames()
        {
            foreach (var wem in file.DataIndex.wemList)
            {
                yield return wem.name;
            }
        }
        public IEnumerable<string> GetReplacementFilePaths()
        {
            return replacements.Values.Select(rep => rep.path).ToList();
        }

        /// <param name="exportPath">Folder where the new bnk file should be save to (name will be the same as the original bnk file).</param>
        /// <param name="workingDir"></param>
        public void ExportReplacementsTo(string exportPath, string workingDir)
        {
            replacements.ForEach(pair =>
            {
                var key = pair.Key;
                var rep = pair.Value;
                string name = rep.filename;
                string path = workingDir + name.Substring(0, name.Length - 4) + ".wem";
                if (!File.Exists(path)) { throw new Exception(path); }

                (Wem wem, int idx) oldWem = file.DataIndex.wemList.Zip(Enumerable.Range(0, file.DataIndex.wemList.Count)).Where(wem => wem.First.name == key).First();
                Wem newWem = new Wem(rep.filename, oldWem.wem.id.ToString(), new BinaryReader(File.Open(path, FileMode.Open)));
                file.DataIndex.wemList[oldWem.idx] = newWem;
            });

            string nbnkPath = Path.Combine(exportPath, name);
            file.ExportNBNK(new BinaryWriter(new FileStream(nbnkPath, FileMode.OpenOrCreate)));
        }

        // Sound functions
        public bool CanPlaySelectedOriginalSound()
        {
            return selectedWem.FlatMap(str => originalSounds.GetValueOrNone(str)).HasValue;
        }

        public void PlaySelectedOriginalSound()
        {
            selectedWem.DoIfPresent(str =>
            {
                originalSounds.GetValueOrNone(str).DoIfPresent(player => player.Play());
            });
        }


        public void LoadOriginalSound(string gamePath, string? bnkextrPath, string? vgmstreamPath)
        {
            if (bnkextrPath == null || vgmstreamPath == null) return;

            string soundPath = gamePath + "\\natives\\STM\\Sound\\Wwise\\";
            string bankPath = soundPath + name;
            string bankFolder = bankPath.Substring(0, bankPath.Length - 4) + "\\";

            var curOverride = new CursorOverride(Cursors.WaitCursor);
            foreach (var wemName in GetWemNames())
            {
                if(originalSounds.ContainsKey(wemName)) { continue; }
                string wavPath = bankFolder + wemName + ".wem.wav";
                // extract file if not there
                if (!File.Exists(wavPath))
                {
                    // extract files
                    Process process = Process.Start(bnkextrPath, bankPath);
                    process.WaitForExit();

                    // convert files
                    List<Process> processes = new List<Process>();
                    foreach (var item in Directory.EnumerateFiles(bankFolder))
                    {
                        processes.Add(Process.Start(vgmstreamPath, item));
                    };
                    processes.ForEach(p => p.WaitForExit());

                    // clean up wems
                    foreach (var item in Directory.EnumerateFiles(bankFolder).Where(f => f.EndsWith(".wem")))
                    {
                        File.Delete(item);
                    }
                }

                if (File.Exists(wavPath))
                {
                    originalSounds[wemName] = new System.Media.SoundPlayer(wavPath);
                }
            }            
        }
    }
}
