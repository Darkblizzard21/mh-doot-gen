using doot_gen.doot_gen;
using doot_gen.util;
using Optional.Unsafe;
using RingingBloom;
using RingingBloom.Common;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace doot_gen
{

    struct HornWrapper
    {
        public HornWrapper(Horns horn) { this.horn = horn; }
        public Horns horn;
        public override string ToString() { return horn.ToString() + ": " + horn.GetHornName(); }

    }

    public partial class Form1 : Form
    {
        private string configFile = Directory.GetCurrentDirectory() + @"\config.json";
        private List<Horns> avaibleHorns = new();

        private string? currentBankName = null;
        private Dictionary<string, BankData> bankFiles = new();

        private OpenFileDialog openReplacementFileDialog = new OpenFileDialog();
        private Config config = new Config();

        public Form1()
        {
            InitializeComponent();
            this.Text = "MH DootGen@" + Version.String();
            hornSelection.AutoCompleteMode = AutoCompleteMode.Suggest;
            hornSelection.AutoCompleteSource = AutoCompleteSource.CustomSource;

            openReplacementFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            openReplacementFileDialog.Filter = "Soundfile (*.wav)|*.wav";
            openReplacementFileDialog.Title = "Select Audio Replacement File";

            // Set up ui actions
            config.SetUIAction(ConfigPath.Console, (bool hasValue, string value) =>
            {
                if (hasValue)
                {
                    labelWwiseConsole.Text = value;
                }
                else
                {
                    labelWwiseConsole.Text = "N/A";
                }
            });
            config.SetUIAction(ConfigPath.Project, (bool hasValue, string value) =>
            {
                if (hasValue)
                {
                    labelWwiseProject.Text = value;
                }
                else
                {
                    labelWwiseProject.Text = "N/A";
                }
            });
            config.SetUIAction(ConfigPath.GameFiles, (bool hasValue, string value) =>
            {
                hornSelection.SelectedItem = null;
                hornSelection.Items.Clear();
                avaibleHorns.Clear();

                labelGameFiles.Text = hasValue ? value : "N/A";
                if (hasValue)
                {
                    string soundPath = value + "\\natives\\STM\\Sound\\Wwise\\";
                    if (!Directory.Exists(soundPath)) return;
                    avaibleHorns = HornExtensions.allHorns.Where(horn =>
                    {
                        return horn.ExistingHornFiles(soundPath).Any(); ;
                    }).ToList();
                    // Update List
                    foreach (var horn in avaibleHorns)
                    {
                        HornWrapper wrapper = new(horn);
                        hornSelection.Items.Add(wrapper);
                        hornSelection.AutoCompleteCustomSource.Add(wrapper.ToString());
                    }
                }
                else
                {
                    hornSelection.Items.Add("Select GamePath before selecting Horn");
                }
            });
            config.SetUIAction(ConfigPath.BnkExtr, (bool hasValue, string value) =>
            {
                if (hasValue)
                {
                    audioMenuBNKSelect.Text = value;
                }
                else
                {
                    audioMenuBNKSelect.Text = "Select exe";
                }
            });
            config.SetUIAction(ConfigPath.VGMStream, (bool hasValue, string value) =>
            {
                if (hasValue)
                {
                    audioMenuVgmSelect.Text = value;
                }
                else
                {
                    audioMenuVgmSelect.Text = "Select exe";
                }
            });

            ToggleModEditVisiblity(false);

            if (File.Exists(configFile))
            {
                config.LoadPathsFrom(configFile);
            }
        }

        private void SelectConsole_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"C:\Program Files (x86)\Audiokinetic";
            dialog.Filter = "Executable (*.exe)|*.exe";
            dialog.Title = "Select WwiseConsole.exe";
            dialog.CheckFileExists = true;
            DialogResult res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                Debug.WriteLine("Dialog OK - Console is now: " + dialog.FileName);
                config.SetPath(ConfigPath.Console, dialog.FileName);
            }
        }

        private void SelectProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Documents";
            dialog.Filter = "Audiokinetic Wwise Project (*.wproj)|*.wproj";
            dialog.Title = "Select WwiseProject";
            dialog.CheckFileExists = true;
            DialogResult res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                Debug.WriteLine("Dialog OK - Project is now: " + dialog.FileName);
                config.SetPath(ConfigPath.Project, dialog.FileName);
            }
        }

        private void SelectGameFiles_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Extracted Game";
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            if (Directory.Exists(dialog.InitialDirectory + "\\re_files"))
            {
                dialog.InitialDirectory = dialog.InitialDirectory + "\\re_files";
            }
            else
            if (Directory.Exists(dialog.InitialDirectory + "\\..\\..\\..\\..\\re_files"))
            {
                dialog.InitialDirectory = dialog.InitialDirectory + "\\..\\..\\..\\..\\re_files";
            }
            DialogResult res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                string soundPath = dialog.SelectedPath + "\\natives\\STM\\Sound\\Wwise\\";
                if (!Directory.Exists(soundPath))
                {
                    MessageBox.Show(
                        "Expected Monster Hunter Rise File structure:\n"
                        + "%GAME_PATH%\\natives\\STM\\Sound\\Wwise",
                        "Invalid Folder",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }
                Debug.WriteLine("Dialog OK - GameFolder is now: " + dialog.SelectedPath);
                config.SetPath(ConfigPath.GameFiles, dialog.SelectedPath);
            }
        }

        private void ConfigMenuReset_Click(object sender, EventArgs e)
        {
            config.Reset();
        }

        private void ConfigMenuSave_Click(object sender, EventArgs e)
        {
            config.SaveTo(configFile);
        }

        private void ConfigMenuSaveTo_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = System.IO.Directory.GetParent(configFile).FullName;
            dialog.Filter = "Json (*.json)|*.json";
            dialog.Title = "Save Config";
            dialog.CheckFileExists = false;
            DialogResult res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                Debug.WriteLine("Dialog OK - ConfigFils is now: " + dialog.FileName);
                configFile = dialog.FileName;
                config.SaveTo(configFile);
            }
        }

        private void ConfigMenuLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = System.IO.Directory.GetParent(configFile).FullName;
            dialog.Filter = "Json (*.json)|*.json";
            dialog.Title = "´Load Config";
            dialog.CheckFileExists = true;
            DialogResult res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                Debug.WriteLine("Dialog OK - ConfigFils is now: " + dialog.FileName);
                configFile = dialog.FileName;
                config.LoadPathsFrom(configFile);
            }
        }

        private void ConfigMenuReload_Click(object sender, EventArgs e)
        {
            config.LoadPathsFrom(configFile);
        }

        private void HelpHornIds_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "\"https://github.com/mhvuze/MonsterHunterRiseModding/wiki/Weapon-IDs-(LS,SA,GL,DB,HH)#hunting-horn\"");
        }

        private void HelpGitHubPage_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "https://github.com/Darkblizzard21/mh-doot-gen");
        }

        private void HelpModdingWiki_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "\"https://github.com/mhvuze/MonsterHunterRiseModding/wiki\"");
        }

        private void ToggleModEditVisiblity(bool b)
        {
            fileTree.Visible = b;
            ExportModButton.Visible = b;

            oldNewArrow.Visible = b;

            sLableOldFile.Visible = b;
            labelOldFile.Visible = b;
            buttonPlayOldFile.Visible = b;

            sLabelNewFile.Visible = b;
            labelNewFile.Visible = b;
            buttonSelectNewFile.Visible = b;
            buttonPlayNewFile.Visible = b;
            buttonRemoveNewFile.Visible = b;

        }

        private void hornSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            bankFiles.Clear();
            fileTree.Nodes.Clear();

            ToggleModEditVisiblity(false);
            if (hornSelection.SelectedItem is HornWrapper
                && config.PathAvailible(ConfigPath.GameFiles))
            {
                ToggleModEditVisiblity(true);
                string soundPath = config.GetPath(ConfigPath.GameFiles) + "\\natives\\STM\\Sound\\Wwise\\";
                HornWrapper hornWrapper = (HornWrapper)hornSelection.SelectedItem;
                foreach (var file in hornWrapper.horn.ExistingHornFiles(soundPath))
                {
                    BinaryReader readFile = new BinaryReader(new FileStream(file, FileMode.Open), Encoding.ASCII);
                    NBNKFile nbnk = new NBNKFile(readFile, SupportedGames.MHRise);
                    if (nbnk.DataIndex == null) continue;
                    string fileName = Path.GetFileName(file);
                    bankFiles.Add(fileName, new BankData(fileName, nbnk));
                    readFile.Close();

                    TreeNode fileNode = new TreeNode();
                    fileNode.Text = fileName;
                    fileNode.Name = fileName;
                    fileNode.Expand();


                    nbnk.DataIndex.wemList.ForEach(x =>
                    {
                        TreeNode wemNode = new TreeNode();
                        wemNode.Text = x.name;
                        wemNode.Name = x.name;
                        wemNode.NodeFont = SystemFonts.DefaultFont;
                        fileNode.Nodes.Add(wemNode);
                    });
                    fileTree.Nodes.Add(fileNode);
                };

            }

        }

        private void ExportModButton_Click(object sender, EventArgs e)
        {
            if (!(hornSelection.SelectedItem is HornWrapper))
            {
                MessageBox.Show(
                            "No horn selected!",
                            "No horn selected!",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                return;
            }
            Horns horn = ((HornWrapper)hornSelection.SelectedItem).horn;
            if (!config.TryGetPath(ConfigPath.Console, out string consolePath) || !File.Exists(consolePath))
            {
                MessageBox.Show(
                            "Console Path needed!",
                            "Current path not vaild!\n" +
                            consolePath,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                return;
            }
            if (!config.TryGetPath(ConfigPath.Project, out string projectPath) || !File.Exists(projectPath))
            {
                MessageBox.Show(
                            "Project Path needed!",
                            "Current path not vaild!\n" +
                            projectPath,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                return;
            }

            // select file
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Output Folder";
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            DialogResult res = dialog.ShowDialog();
            if (res != DialogResult.OK)
            {
                MessageBox.Show(
                           "Output Path needed!",
                           "Output Path needed!",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Exclamation);
                return;
            }

            var cursorOverride = new CursorOverride(Cursors.WaitCursor);
            ModExporter.ExportMod(horn, bankFiles.Values, dialog.SelectedPath, consolePath, projectPath);
        }

        private void audioMenuBNKSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            dialog.Filter = "Executable (*.exe)|*.exe";
            dialog.Title = "Select bnkextr.exe";
            dialog.CheckFileExists = true;
            DialogResult res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                Debug.WriteLine("Dialog OK - bnkextr.exe is now: " + dialog.FileName);
                config.SetPath(ConfigPath.BnkExtr, dialog.FileName);
            }
        }
        private void audioMenuBNKGitHub_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "\"https://github.com/eXpl0it3r/bnkextr/releases/tag/2.0\"");
        }

        private void audioMenuBNKGitHubDownload_Click(object sender, EventArgs e)
        {
            string targetFile = Directory.GetCurrentDirectory() + "\\bnkextr.exe";
            if (!File.Exists(targetFile))
            {
                Uri url = new Uri("https://www.github.com/eXpl0it3r/bnkextr/releases/download/2.0/bnkextr.exe");
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, targetFile);
                }
            }
            config.SetPath(ConfigPath.BnkExtr, targetFile);

        }

        private void audioMenuVgmSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            dialog.Filter = "Executable (*.exe)|*.exe";
            dialog.Title = "Select vgmstream-cli.exe";
            dialog.CheckFileExists = true;
            DialogResult res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                Debug.WriteLine("Dialog OK - vgmstream-cli.exe is now: " + dialog.FileName);
                config.SetPath(ConfigPath.VGMStream, dialog.FileName);
            }
        }

        private void audioMenuVgmGithub_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "\"https://github.com/vgmstream/vgmstream/releases/tag/r1879\"");
        }

        private void audioMenuVgmGithubDownload_Click(object sender, EventArgs e)
        {
            string folder = Directory.GetCurrentDirectory() + "\\vgmstream\\";
            string targetFile = folder+ "\\vgmstream-cli.exe";
            if (!File.Exists(targetFile))
            {
                string zip = "vgmstream-win" + (System.Environment.Is64BitOperatingSystem ? "64.zip" : ".zip"); 
                Uri url = new Uri("https://www.github.com//vgmstream/vgmstream/releases/download/r1879/"+ zip);
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, zip);
                }
                System.IO.Compression.ZipFile.ExtractToDirectory(zip, folder);
                File.Delete(zip);
            }
            config.SetPath(ConfigPath.VGMStream, targetFile);
        }

        private void fileTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Allways reset current bank
            currentBankName = null;

            buttonPlayOldFile.Enabled = false;
            labelOldFile.Text = "Select file in tree view";

            labelNewFile.Text = "Select replacement file";
            buttonSelectNewFile.Enabled = false;
            buttonPlayNewFile.Enabled = false;
            buttonRemoveNewFile.Enabled = false;

            if (e.Node == null) { return; }
            if (e.Node.Level != 1) { return; }
            if (!bankFiles.TryGetValue(e.Node.Parent.Text, out BankData bnk)) { return; }
            currentBankName = e.Node.Parent.Text;

            List<string> wems = bnk.GetWemNames().Where(name => name == e.Node.Name).ToList();
            if (wems.Count == 0) { Debug.Assert(false); return; }
            labelOldFile.Text = e.Node.Name;
            bankFiles[currentBankName].SelectWem(e.Node.Name);

            buttonSelectNewFile.Enabled = true;

            if (!config.TryGetPath(ConfigPath.GameFiles, out string gamePath)
                || !config.TryGetPath(ConfigPath.BnkExtr, out string bnkextrPath)
                || !config.TryGetPath(ConfigPath.VGMStream, out string vgmstreamPath)) return;

            string soundPath = gamePath + "\\natives\\STM\\Sound\\Wwise\\";
            string bankPath = soundPath + e.Node.Parent.Text;
            string bankFolder = bankPath.Substring(0, bankPath.Length - 4) + "\\";
            string wavPath = bankFolder + e.Node.Name + ".wem.wav";

            bankFiles[currentBankName].LoadOriginalSound(gamePath, bnkextrPath, vgmstreamPath);

            buttonPlayOldFile.Enabled = bankFiles[currentBankName].CanPlaySelectedOriginalSound();

            labelNewFile.Text = bankFiles[currentBankName].selectedReplacementName;
            buttonPlayNewFile.Enabled = bankFiles[currentBankName].selectedHasReplacement;
            buttonRemoveNewFile.Enabled = bankFiles[currentBankName].selectedHasReplacement;
        }

        private void buttonPlayOldFile_Click(object sender, EventArgs e)
        {
            if (currentBankName == null) return;
            bankFiles[currentBankName].PlaySelectedOriginalSound();
        }

        private void fileTree_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonPlayOldFile_Click(sender, e);
        }

        private void buttonSelectNewFile_Click(object sender, EventArgs e)
        {
            if (currentBankName == null) return;

            DialogResult res = openReplacementFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                // select new file
                bankFiles[currentBankName].ReplaceSelected(openReplacementFileDialog.FileName);
                labelNewFile.Text = Path.GetFileName(openReplacementFileDialog.FileName);

                string currentWavName = bankFiles[currentBankName].selectedWem.ValueOrFailure();
                TreeNode node = fileTree.Nodes.Find(currentBankName, false).First().Nodes.Find(currentWavName, false).First();
                node.Text = Path.GetFileName(openReplacementFileDialog.FileName);

                buttonPlayNewFile.Enabled = true;
                buttonRemoveNewFile.Enabled = true;
            }
        }

        private void buttonPlayNewFile_Click(object sender, EventArgs e)
        {
            if (currentBankName == null) return;
            bankFiles[currentBankName].PlaySelectedReplacement();
        }

        private void buttonRemoveNewFile_Click(object sender, EventArgs e)
        {

            if (currentBankName == null) return;
            bankFiles[currentBankName].RemoveSelectedReplacement();

            string currentWavName = bankFiles[currentBankName].selectedWem.ValueOrFailure();
            TreeNode node = fileTree.Nodes.Find(currentBankName, false).First().Nodes.Find(currentWavName, false).First();
            node.Text = currentWavName;

            buttonPlayNewFile.Enabled = false;
            buttonRemoveNewFile.Enabled = false;
        }
    }
}

