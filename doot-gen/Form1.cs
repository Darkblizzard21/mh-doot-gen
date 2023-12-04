using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RingingBloom;
using RingingBloom.Common;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

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
        const int VERSION_MAJOR = 0;
        const int VERSION_MINOR = 1;
        const int VERSION_PATCH = 0;

        private string configFile = Directory.GetCurrentDirectory() + @"\config.json";
        private List<Horns> avaibleHorns = new();
        private Dictionary<string, NBNKFile> bankFiles = new();
        private System.Media.SoundPlayer? currentWavPlayer = null;
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
            this.Text = "MH DootGen@" + VERSION_MAJOR + "." + VERSION_MINOR + "." + VERSION_PATCH;
            hornSelection.AutoCompleteMode = AutoCompleteMode.Suggest;
            hornSelection.AutoCompleteSource = AutoCompleteSource.CustomSource;

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
                    bankFiles.Add(fileName, nbnk);
                    readFile.Close();

                    TreeNode fileNode = new TreeNode();
                    fileNode.Text = fileName;
                    fileNode.Expand();


                    nbnk.DataIndex.wemList.ForEach(x =>
                    {
                        fileNode.Nodes.Add(x.name);
                    });
                    fileTree.Nodes.Add(fileNode);
                };

            }

        }

        private void ExportModButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                        "This function still needs to be implemented",
                        "Not Implemented!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
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
            currentWavPlayer = null;
            buttonPlayOldFile.Enabled = false;
            labelOldFile.Text = "Select file in tree view";

            if (e.Node == null) { return; }
            if (e.Node.Level != 1) { return; }
            if (!bankFiles.TryGetValue(e.Node.Parent.Text, out NBNKFile file)) { return; }

            List<(Wem, int)> wems = file.DataIndex.wemList.Zip(Enumerable.Range(0, file.DataIndex.wemList.Count)).Where(wem => wem.First.name == e.Node.Text).ToList();
            if (wems.Count == 0) { Debug.Assert(false); return; }
            Wem wem = wems.First().Item1;
            labelOldFile.Text = e.Node.Text;

            string soundPath = gamePath + "\\natives\\STM\\Sound\\Wwise\\";
            string bankPath = soundPath + e.Node.Parent.Text;
            string bankFolder = bankPath.Substring(0, bankPath.Length - 4) + "\\";
            string wavPath = bankFolder + e.Node.Text + ".wem.wav";

            if (bnkextrPath != null && vgmstreamPath != null)
            {

                // extract file if not there
                if (!File.Exists(wavPath))
                {
                    Cursor prev = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
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
                    Cursor.Current = prev;
                }

            }

            if (File.Exists(wavPath))
            {
                currentWavPlayer = new System.Media.SoundPlayer(wavPath);
                buttonPlayOldFile.Enabled = true;
            }
        }

        private void buttonPlayOldFile_Click(object sender, EventArgs e)
        {
            if (currentWavPlayer == null) return;
            currentWavPlayer.Play();
        }

        private void fileTree_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonPlayOldFile_Click(sender, e);
        }
    }
}

