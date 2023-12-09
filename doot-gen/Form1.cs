using doot_gen.doot_gen;
using doot_gen.util;
using Newtonsoft.Json;
using Optional.Unsafe;
using RingingBloom;
using RingingBloom.Common;
using RingingBloom.WWiseTypes;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;
namespace doot_gen
{
    struct Config
    {
        public string consolePath;
        public string projectPath;
        public string gamePath;
        public string? bnkextrPath;
        public string? vgmstreamPath;
    }

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

        private string consolePath
        {
            get { return labelWwiseConsole.Text; }
            set { labelWwiseConsole.Text = value; }
        }
        private string projectPath
        {
            get { return labelWwiseProject.Text; }
            set { labelWwiseProject.Text = value; }
        }

        private string gamePath
        {
            get { return labelGameFiles.Text; }
            set
            {
                string soundPath = value + "\\natives\\STM\\Sound\\Wwise\\";
                hornSelection.SelectedItem = null;
                hornSelection.Items.Clear();
                avaibleHorns.Clear();

                if (Directory.Exists(soundPath))
                {
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
                labelGameFiles.Text = value;
            }
        }

        // bnkextrPath
        private string? bnkextrPath
        {
            get { return audioMenuBNKSelect.Text == "Select exe" ? null : audioMenuBNKSelect.Text; }
            set { audioMenuBNKSelect.Text = value == null ? "Select exe" : value; }
        }
        private string? vgmstreamPath
        {
            get { return audioMenuVgmSelect.Text == "Select exe" ? null : audioMenuVgmSelect.Text; }
            set { audioMenuVgmSelect.Text = value == null ? "Select exe" : value; }
        }

        public Form1()
        {
            InitializeComponent();
            this.Text = "MH DootGen@" + Version.String();
            hornSelection.AutoCompleteMode = AutoCompleteMode.Suggest;
            hornSelection.AutoCompleteSource = AutoCompleteSource.CustomSource;

            openReplacementFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            openReplacementFileDialog.Filter = "Soundfile (*.wav)|*.wav";
            openReplacementFileDialog.Title = "Select Audio Replacement File";

            ToggleModEditVisiblity(false);

            if (File.Exists(configFile))
            {
                LoadConfig();
            }
            else
            {
                ResetConfig();
            }
        }

        void LoadConfig()
        {
            Debug.WriteLine("Loading config from " + configFile);
            StreamReader stream = File.OpenText(configFile);
            Config config = JsonConvert.DeserializeObject<Config>(stream.ReadToEnd());
            stream.Close();

            consolePath = config.consolePath;
            projectPath = config.projectPath;
            gamePath = config.gamePath;
            bnkextrPath = config.bnkextrPath;
            vgmstreamPath = config.vgmstreamPath;
        }
        void SaveConfig()
        {
            Debug.WriteLine("Saving config to " + configFile);
            Config config;
            config.consolePath = consolePath;
            config.projectPath = projectPath;
            config.gamePath = gamePath;
            config.bnkextrPath = bnkextrPath;
            config.vgmstreamPath = vgmstreamPath;
            string json = JsonConvert.SerializeObject(config);
            StreamWriter file = File.CreateText(configFile);
            file.Write(json);
            file.Close();
        }

        void ResetConfig()
        {
            Debug.WriteLine("Config reset");
            consolePath = "N/A";
            projectPath = "N/A";
            gamePath = "N/A";
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
                consolePath = dialog.FileName;
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
                projectPath = dialog.FileName;
            }
        }

        private void SelectGameFiles_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Extracted Game";
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
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
                gamePath = dialog.SelectedPath;
            }
        }

        private void ConfigMenuReset_Click(object sender, EventArgs e)
        {
            ResetConfig();
        }

        private void ConfigMenuSave_Click(object sender, EventArgs e)
        {
            SaveConfig();
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
                SaveConfig();
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
                LoadConfig();
            }
        }

        private void ConfigMenuReload_Click(object sender, EventArgs e)
        {
            LoadConfig();
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
            if (hornSelection.SelectedItem is HornWrapper)
            {
                ToggleModEditVisiblity(true);
                string soundPath = gamePath + "\\natives\\STM\\Sound\\Wwise\\";
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
            if (!File.Exists(consolePath))
            {
                MessageBox.Show(
                            "Console Path needed!",
                            "Current path not vaild!\n" +
                            consolePath,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                return;
            }
            if (!File.Exists(projectPath))
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

        private void modEditTable_Paint(object sender, PaintEventArgs e)
        {

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
                vgmstreamPath = dialog.FileName;
            }
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
                bnkextrPath = dialog.FileName;
            }
        }
        private void audioMenuBNKGitHub_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "\"https://github.com/eXpl0it3r/bnkextr/releases/tag/2.0\"");
        }

        private void audioMenuVgmGithub_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "\"https://github.com/vgmstream/vgmstream/releases/tag/r1879\"");

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

