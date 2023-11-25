using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace doot_gen
{
    struct Config
    {
        public string consolePath;
        public string projectPath;
        public string gamePath;
    }

    struct HornWrapper
    {
        public HornWrapper(Horns horn) {this.horn = horn;}
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
                if (Directory.Exists(soundPath))
                {
                    avaibleHorns = HornExtensions.allHorns.Where(horn =>
                    {
                        return horn.ExistingHornFiles(soundPath).Any(); ;
                    }).ToList();
                    // Update List
                    hornSelection.SelectedItem = null;
                    hornSelection.Items.Clear();
                    foreach (var horn in avaibleHorns)
                    {
                        hornSelection.Items.Add(new HornWrapper(horn));
                    }
                    labelGameFiles.Text = value;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            this.Text = "MH DootGen@" + VERSION_MAJOR + "." + VERSION_MINOR + "." + VERSION_PATCH;
            hornSelection.AutoCompleteMode = AutoCompleteMode.Suggest;

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
            Config config = JsonConvert.DeserializeObject<Config>(File.OpenText(configFile).ReadToEnd());
            consolePath = config.consolePath;
            projectPath = config.projectPath;
            gamePath = config.gamePath;
        }
        void SaveConfig()
        {
            Debug.WriteLine("Saving config to " + configFile);
            Config config;
            config.consolePath = consolePath;
            config.projectPath = projectPath;
            config.gamePath = gamePath;
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
            hornSelection.SelectedItem = null;
            hornSelection.Items.Clear();
            hornSelection.Items.Add("None");
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
    }
}
