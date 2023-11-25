using System.Diagnostics;
using System.Windows.Forms;

namespace doot_gen
{
    public partial class Form1 : Form
    {
        const int VERSION_MAJOR = 0;
        const int VERSION_MINOR = 1;
        const int VERSION_PATCH = 0;

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

        public Form1()
        {
            InitializeComponent();
            this.Text = "MH DootGen@" + VERSION_MAJOR + "." + VERSION_MINOR + "." + VERSION_PATCH;

            consolePath = "N/A";
            projectPath = "N/A";
        }

        private void SelectConsole_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"C:\Program Files (x86)\Audiokinetic";
            dialog.Filter = "Executable (*.exe)|*.exe";
            dialog.Title = "Select WwiseConsole.exe";
            DialogResult res = dialog.ShowDialog();
            if(res == DialogResult.OK)
            {
                Debug.WriteLine("Dialog OK - Console is now: " + dialog.FileName);
                consolePath = dialog.FileName;
            }
        }

        private void SelectProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)+ @"\Documents";
            dialog.Filter = "Audiokinetic Wwise Project (*.wproj)|*.wproj";
            dialog.Title = "Select WwiseProject";
            DialogResult res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                Debug.WriteLine("Dialog OK - Project is now: " + dialog.FileName);
                projectPath = dialog.FileName;
            }
        }
    }
}
