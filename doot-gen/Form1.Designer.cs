namespace doot_gen
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            SLabelWwiseConsole = new Label();
            SelectConsole = new Button();
            labelWwiseConsole = new Label();
            SLabelWWiseProject = new Label();
            SelectProject = new Button();
            labelWwiseProject = new Label();
            SLabelGameFiles = new Label();
            SelectGameFiles = new Button();
            labelGameFiles = new Label();
            ConfigMenuStrip = new MenuStrip();
            ConfigMenu = new ToolStripMenuItem();
            ConfigMenuSave = new ToolStripMenuItem();
            ConfigMenuSaveTo = new ToolStripMenuItem();
            ConfigMenuReload = new ToolStripMenuItem();
            ConfigMenuLoad = new ToolStripMenuItem();
            ConfigMenuReset = new ToolStripMenuItem();
            audioMenu = new ToolStripMenuItem();
            audioMenuBNK = new ToolStripMenuItem();
            audioMenuBNKSelect = new ToolStripMenuItem();
            audioMenuBNKGitHub = new ToolStripMenuItem();
            audioMenuBNKGitHubDownload = new ToolStripMenuItem();
            audioMenuVgm = new ToolStripMenuItem();
            audioMenuVgmSelect = new ToolStripMenuItem();
            audioMenuVgmGithub = new ToolStripMenuItem();
            HelpMenu = new ToolStripMenuItem();
            HelpGitHubPage = new ToolStripMenuItem();
            HelpHornIds = new ToolStripMenuItem();
            HelpModdingWiki = new ToolStripMenuItem();
            hornSelection = new ComboBox();
            SLabelHH = new Label();
            fileTree = new TreeView();
            ExportModButton = new Button();
            sLableOldFile = new Label();
            labelOldFile = new Label();
            buttonPlayOldFile = new Button();
            oldNewArrow = new PictureBox();
            helpProvider1 = new HelpProvider();
            sLabelNewFile = new Label();
            labelNewFile = new Label();
            buttonSelectNewFile = new Button();
            buttonPlayNewFile = new Button();
            buttonRemoveNewFile = new Button();
            audioMenuVgmGithubDownload = new ToolStripMenuItem();
            ConfigMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)oldNewArrow).BeginInit();
            SuspendLayout();
            // 
            // SLabelWwiseConsole
            // 
            SLabelWwiseConsole.AutoSize = true;
            SLabelWwiseConsole.Location = new Point(12, 31);
            SLabelWwiseConsole.Name = "SLabelWwiseConsole";
            SLabelWwiseConsole.Size = new Size(105, 15);
            SLabelWwiseConsole.TabIndex = 0;
            SLabelWwiseConsole.Text = "WwiseConsole.exe";
            // 
            // SelectConsole
            // 
            SelectConsole.Location = new Point(139, 27);
            SelectConsole.Name = "SelectConsole";
            SelectConsole.Size = new Size(75, 22);
            SelectConsole.TabIndex = 1;
            SelectConsole.Text = "Select";
            SelectConsole.UseVisualStyleBackColor = true;
            SelectConsole.Click += SelectConsole_Click;
            // 
            // labelWwiseConsole
            // 
            labelWwiseConsole.AutoSize = true;
            labelWwiseConsole.Location = new Point(220, 31);
            labelWwiseConsole.Name = "labelWwiseConsole";
            labelWwiseConsole.Size = new Size(80, 15);
            labelWwiseConsole.TabIndex = 2;
            labelWwiseConsole.Text = "SET_BY_CODE";
            // 
            // SLabelWWiseProject
            // 
            SLabelWWiseProject.AutoSize = true;
            SLabelWWiseProject.Location = new Point(12, 59);
            SLabelWWiseProject.Name = "SLabelWWiseProject";
            SLabelWWiseProject.Size = new Size(80, 15);
            SLabelWWiseProject.TabIndex = 3;
            SLabelWWiseProject.Text = "WWiseProject";
            // 
            // SelectProject
            // 
            SelectProject.Location = new Point(139, 55);
            SelectProject.Name = "SelectProject";
            SelectProject.Size = new Size(75, 23);
            SelectProject.TabIndex = 4;
            SelectProject.Text = "Select";
            SelectProject.UseVisualStyleBackColor = true;
            SelectProject.Click += SelectProject_Click;
            // 
            // labelWwiseProject
            // 
            labelWwiseProject.AutoSize = true;
            labelWwiseProject.Location = new Point(220, 59);
            labelWwiseProject.Name = "labelWwiseProject";
            labelWwiseProject.Size = new Size(80, 15);
            labelWwiseProject.TabIndex = 5;
            labelWwiseProject.Text = "SET_BY_CODE";
            // 
            // SLabelGameFiles
            // 
            SLabelGameFiles.AutoSize = true;
            SLabelGameFiles.Location = new Point(12, 88);
            SLabelGameFiles.Name = "SLabelGameFiles";
            SLabelGameFiles.Size = new Size(121, 15);
            SLabelGameFiles.TabIndex = 6;
            SLabelGameFiles.Text = "GameFiles (extracted)";
            // 
            // SelectGameFiles
            // 
            SelectGameFiles.Location = new Point(139, 84);
            SelectGameFiles.Name = "SelectGameFiles";
            SelectGameFiles.Size = new Size(75, 23);
            SelectGameFiles.TabIndex = 7;
            SelectGameFiles.Text = "Select";
            SelectGameFiles.UseVisualStyleBackColor = true;
            SelectGameFiles.Click += SelectGameFiles_Click;
            // 
            // labelGameFiles
            // 
            labelGameFiles.AutoSize = true;
            labelGameFiles.Location = new Point(220, 88);
            labelGameFiles.Name = "labelGameFiles";
            labelGameFiles.Size = new Size(80, 15);
            labelGameFiles.TabIndex = 8;
            labelGameFiles.Text = "SET_BY_CODE";
            // 
            // ConfigMenuStrip
            // 
            ConfigMenuStrip.BackColor = SystemColors.ControlDark;
            ConfigMenuStrip.Items.AddRange(new ToolStripItem[] { ConfigMenu, audioMenu, HelpMenu });
            ConfigMenuStrip.Location = new Point(0, 0);
            ConfigMenuStrip.Name = "ConfigMenuStrip";
            ConfigMenuStrip.Size = new Size(800, 24);
            ConfigMenuStrip.TabIndex = 9;
            ConfigMenuStrip.Text = "menuStrip1";
            // 
            // ConfigMenu
            // 
            ConfigMenu.DropDownItems.AddRange(new ToolStripItem[] { ConfigMenuSave, ConfigMenuSaveTo, ConfigMenuReload, ConfigMenuLoad, ConfigMenuReset });
            ConfigMenu.Name = "ConfigMenu";
            ConfigMenu.Size = new Size(55, 20);
            ConfigMenu.Text = "Config";
            // 
            // ConfigMenuSave
            // 
            ConfigMenuSave.Name = "ConfigMenuSave";
            ConfigMenuSave.Size = new Size(110, 22);
            ConfigMenuSave.Text = "Save";
            ConfigMenuSave.Click += ConfigMenuSave_Click;
            // 
            // ConfigMenuSaveTo
            // 
            ConfigMenuSaveTo.Name = "ConfigMenuSaveTo";
            ConfigMenuSaveTo.Size = new Size(110, 22);
            ConfigMenuSaveTo.Text = "SaveTo";
            ConfigMenuSaveTo.Click += ConfigMenuSaveTo_Click;
            // 
            // ConfigMenuReload
            // 
            ConfigMenuReload.Name = "ConfigMenuReload";
            ConfigMenuReload.Size = new Size(110, 22);
            ConfigMenuReload.Text = "Reload";
            ConfigMenuReload.Click += ConfigMenuReload_Click;
            // 
            // ConfigMenuLoad
            // 
            ConfigMenuLoad.Name = "ConfigMenuLoad";
            ConfigMenuLoad.Size = new Size(110, 22);
            ConfigMenuLoad.Text = "Load";
            ConfigMenuLoad.Click += ConfigMenuLoad_Click;
            // 
            // ConfigMenuReset
            // 
            ConfigMenuReset.Name = "ConfigMenuReset";
            ConfigMenuReset.Size = new Size(110, 22);
            ConfigMenuReset.Text = "Reset";
            ConfigMenuReset.Click += ConfigMenuReset_Click;
            // 
            // audioMenu
            // 
            audioMenu.DropDownItems.AddRange(new ToolStripItem[] { audioMenuBNK, audioMenuVgm });
            audioMenu.Name = "audioMenu";
            audioMenu.Size = new Size(101, 20);
            audioMenu.Text = "Audio Playback";
            // 
            // audioMenuBNK
            // 
            audioMenuBNK.DropDownItems.AddRange(new ToolStripItem[] { audioMenuBNKSelect, audioMenuBNKGitHub, audioMenuBNKGitHubDownload });
            audioMenuBNK.Name = "audioMenuBNK";
            audioMenuBNK.Size = new Size(180, 22);
            audioMenuBNK.Text = "bnkextr.exe";
            // 
            // audioMenuBNKSelect
            // 
            audioMenuBNKSelect.Name = "audioMenuBNKSelect";
            audioMenuBNKSelect.Size = new Size(187, 22);
            audioMenuBNKSelect.Text = "Select exe";
            audioMenuBNKSelect.Click += audioMenuBNKSelect_Click;
            // 
            // audioMenuBNKGitHub
            // 
            audioMenuBNKGitHub.Name = "audioMenuBNKGitHub";
            audioMenuBNKGitHub.Size = new Size(187, 22);
            audioMenuBNKGitHub.Text = "GitHub Page";
            audioMenuBNKGitHub.Click += audioMenuBNKGitHub_Click;
            // 
            // audioMenuBNKGitHubDownload
            // 
            audioMenuBNKGitHubDownload.Name = "audioMenuBNKGitHubDownload";
            audioMenuBNKGitHubDownload.Size = new Size(187, 22);
            audioMenuBNKGitHubDownload.Text = "Download for GitHub";
            audioMenuBNKGitHubDownload.Click += audioMenuBNKGitHubDownload_Click;
            // 
            // audioMenuVgm
            // 
            audioMenuVgm.DropDownItems.AddRange(new ToolStripItem[] { audioMenuVgmSelect, audioMenuVgmGithub, audioMenuVgmGithubDownload });
            audioMenuVgm.Name = "audioMenuVgm";
            audioMenuVgm.Size = new Size(180, 22);
            audioMenuVgm.Text = "vgmstream";
            // 
            // audioMenuVgmSelect
            // 
            audioMenuVgmSelect.Name = "audioMenuVgmSelect";
            audioMenuVgmSelect.Size = new Size(198, 22);
            audioMenuVgmSelect.Text = "Select exe";
            audioMenuVgmSelect.Click += audioMenuVgmSelect_Click;
            // 
            // audioMenuVgmGithub
            // 
            audioMenuVgmGithub.Name = "audioMenuVgmGithub";
            audioMenuVgmGithub.Size = new Size(198, 22);
            audioMenuVgmGithub.Text = "GitHub Page";
            audioMenuVgmGithub.Click += audioMenuVgmGithub_Click;
            // 
            // HelpMenu
            // 
            HelpMenu.DropDownItems.AddRange(new ToolStripItem[] { HelpGitHubPage, HelpHornIds, HelpModdingWiki });
            HelpMenu.Name = "HelpMenu";
            HelpMenu.Size = new Size(44, 20);
            HelpMenu.Text = "Help";
            // 
            // HelpGitHubPage
            // 
            HelpGitHubPage.Name = "HelpGitHubPage";
            HelpGitHubPage.Size = new Size(141, 22);
            HelpGitHubPage.Text = "GitHub Page";
            HelpGitHubPage.Click += HelpGitHubPage_Click;
            // 
            // HelpHornIds
            // 
            HelpHornIds.Name = "HelpHornIds";
            HelpHornIds.Size = new Size(141, 22);
            HelpHornIds.Text = "HornIds";
            HelpHornIds.Click += HelpHornIds_Click;
            // 
            // HelpModdingWiki
            // 
            HelpModdingWiki.Name = "HelpModdingWiki";
            HelpModdingWiki.Size = new Size(141, 22);
            HelpModdingWiki.Text = "ModingWiki";
            HelpModdingWiki.Click += HelpModdingWiki_Click;
            // 
            // hornSelection
            // 
            hornSelection.FormattingEnabled = true;
            hornSelection.Location = new Point(139, 113);
            hornSelection.MaximumSize = new Size(900, 0);
            hornSelection.MinimumSize = new Size(600, 0);
            hornSelection.Name = "hornSelection";
            hornSelection.Size = new Size(649, 23);
            hornSelection.TabIndex = 10;
            hornSelection.SelectedIndexChanged += hornSelection_SelectedIndexChanged;
            // 
            // SLabelHH
            // 
            SLabelHH.AutoSize = true;
            SLabelHH.Location = new Point(12, 116);
            SLabelHH.Name = "SLabelHH";
            SLabelHH.Size = new Size(81, 15);
            SLabelHH.TabIndex = 11;
            SLabelHH.Text = "Hunting Horn";
            // 
            // fileTree
            // 
            fileTree.Location = new Point(12, 144);
            fileTree.Name = "fileTree";
            fileTree.Size = new Size(288, 276);
            fileTree.TabIndex = 12;
            fileTree.AfterSelect += fileTree_AfterSelect;
            fileTree.MouseDoubleClick += fileTree_MouseDoubleClick;
            // 
            // ExportModButton
            // 
            ExportModButton.Location = new Point(632, 144);
            ExportModButton.Name = "ExportModButton";
            ExportModButton.Size = new Size(156, 23);
            ExportModButton.TabIndex = 13;
            ExportModButton.Text = "Generate and Export Mod";
            ExportModButton.UseVisualStyleBackColor = true;
            ExportModButton.Click += ExportModButton_Click;
            // 
            // sLableOldFile
            // 
            sLableOldFile.AutoSize = true;
            sLableOldFile.Location = new Point(306, 187);
            sLableOldFile.Name = "sLableOldFile";
            sLableOldFile.Size = new Size(33, 15);
            sLableOldFile.TabIndex = 17;
            sLableOldFile.Text = "OLD:";
            // 
            // labelOldFile
            // 
            labelOldFile.AutoSize = true;
            labelOldFile.Location = new Point(345, 187);
            labelOldFile.Name = "labelOldFile";
            labelOldFile.Size = new Size(120, 15);
            labelOldFile.TabIndex = 18;
            labelOldFile.Text = "Select file in tree view";
            // 
            // buttonPlayOldFile
            // 
            buttonPlayOldFile.Enabled = false;
            buttonPlayOldFile.Location = new Point(345, 246);
            buttonPlayOldFile.Name = "buttonPlayOldFile";
            buttonPlayOldFile.Size = new Size(140, 23);
            buttonPlayOldFile.TabIndex = 19;
            buttonPlayOldFile.Text = "Play";
            buttonPlayOldFile.UseVisualStyleBackColor = true;
            buttonPlayOldFile.Click += buttonPlayOldFile_Click;
            // 
            // oldNewArrow
            // 
            oldNewArrow.ErrorImage = (Image)resources.GetObject("oldNewArrow.ErrorImage");
            oldNewArrow.Image = (Image)resources.GetObject("oldNewArrow.Image");
            oldNewArrow.ImageLocation = "";
            oldNewArrow.InitialImage = (Image)resources.GetObject("oldNewArrow.InitialImage");
            oldNewArrow.Location = new Point(491, 217);
            oldNewArrow.Name = "oldNewArrow";
            oldNewArrow.Size = new Size(94, 81);
            oldNewArrow.SizeMode = PictureBoxSizeMode.Zoom;
            oldNewArrow.TabIndex = 20;
            oldNewArrow.TabStop = false;
            // 
            // sLabelNewFile
            // 
            sLabelNewFile.AutoSize = true;
            sLabelNewFile.Location = new Point(587, 187);
            sLabelNewFile.Name = "sLabelNewFile";
            sLabelNewFile.Size = new Size(36, 15);
            sLabelNewFile.TabIndex = 21;
            sLabelNewFile.Text = "NEW:";
            // 
            // labelNewFile
            // 
            labelNewFile.AutoSize = true;
            labelNewFile.Location = new Point(629, 187);
            labelNewFile.Name = "labelNewFile";
            labelNewFile.Size = new Size(80, 15);
            labelNewFile.TabIndex = 22;
            labelNewFile.Text = "SET_BY_CODE";
            // 
            // buttonSelectNewFile
            // 
            buttonSelectNewFile.Enabled = false;
            buttonSelectNewFile.Location = new Point(591, 217);
            buttonSelectNewFile.Name = "buttonSelectNewFile";
            buttonSelectNewFile.Size = new Size(140, 23);
            buttonSelectNewFile.TabIndex = 23;
            buttonSelectNewFile.Text = "Select";
            buttonSelectNewFile.UseVisualStyleBackColor = true;
            buttonSelectNewFile.Click += buttonSelectNewFile_Click;
            // 
            // buttonPlayNewFile
            // 
            buttonPlayNewFile.Enabled = false;
            buttonPlayNewFile.Location = new Point(591, 246);
            buttonPlayNewFile.Name = "buttonPlayNewFile";
            buttonPlayNewFile.Size = new Size(140, 23);
            buttonPlayNewFile.TabIndex = 24;
            buttonPlayNewFile.Text = "Play";
            buttonPlayNewFile.UseVisualStyleBackColor = true;
            buttonPlayNewFile.Click += buttonPlayNewFile_Click;
            // 
            // buttonRemoveNewFile
            // 
            buttonRemoveNewFile.Enabled = false;
            buttonRemoveNewFile.Location = new Point(591, 275);
            buttonRemoveNewFile.Name = "buttonRemoveNewFile";
            buttonRemoveNewFile.Size = new Size(140, 23);
            buttonRemoveNewFile.TabIndex = 25;
            buttonRemoveNewFile.Text = "Remove";
            buttonRemoveNewFile.UseVisualStyleBackColor = true;
            buttonRemoveNewFile.Click += buttonRemoveNewFile_Click;
            // 
            // audioMenuVgmGithubDownload
            // 
            audioMenuVgmGithubDownload.Name = "audioMenuVgmGithubDownload";
            audioMenuVgmGithubDownload.Size = new Size(198, 22);
            audioMenuVgmGithubDownload.Text = "Download from GitHub";
            audioMenuVgmGithubDownload.Click += audioMenuVgmGithubDownload_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonRemoveNewFile);
            Controls.Add(buttonPlayNewFile);
            Controls.Add(buttonSelectNewFile);
            Controls.Add(labelNewFile);
            Controls.Add(sLabelNewFile);
            Controls.Add(oldNewArrow);
            Controls.Add(buttonPlayOldFile);
            Controls.Add(labelOldFile);
            Controls.Add(sLableOldFile);
            Controls.Add(ExportModButton);
            Controls.Add(fileTree);
            Controls.Add(SLabelHH);
            Controls.Add(hornSelection);
            Controls.Add(labelGameFiles);
            Controls.Add(SelectGameFiles);
            Controls.Add(SLabelGameFiles);
            Controls.Add(labelWwiseProject);
            Controls.Add(SelectProject);
            Controls.Add(SLabelWWiseProject);
            Controls.Add(labelWwiseConsole);
            Controls.Add(SelectConsole);
            Controls.Add(SLabelWwiseConsole);
            Controls.Add(ConfigMenuStrip);
            MainMenuStrip = ConfigMenuStrip;
            Name = "Form1";
            Text = "Form1";
            ConfigMenuStrip.ResumeLayout(false);
            ConfigMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)oldNewArrow).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label SLabelWwiseConsole;
        private Button SelectConsole;
        private Label labelWwiseConsole;
        private Label SLabelWWiseProject;
        private Button SelectProject;
        private Label labelWwiseProject;
        private Label SLabelGameFiles;
        private Button SelectGameFiles;
        private Label labelGameFiles;
        private MenuStrip ConfigMenuStrip;
        private ToolStripMenuItem ConfigMenu;
        private ToolStripMenuItem ConfigMenuSave;
        private ToolStripMenuItem ConfigMenuLoad;
        private ToolStripMenuItem ConfigMenuReset;
        private ToolStripMenuItem ConfigMenuSaveTo;
        private ToolStripMenuItem ConfigMenuReload;
        private ToolStripMenuItem HelpMenu;
        private ToolStripMenuItem HelpGitHubPage;
        private ToolStripMenuItem HelpHornIds;
        private ComboBox hornSelection;
        private ToolStripMenuItem HelpModdingWiki;
        private Label SLabelHH;
        private TreeView fileTree;
        private Button ExportModButton;
        private ToolStripMenuItem audioMenu;
        private ToolStripMenuItem audioMenuBNK;
        private ToolStripMenuItem audioMenuBNKSelect;
        private ToolStripMenuItem audioMenuBNKGitHub;
        private ToolStripMenuItem audioMenuVgm;
        private ToolStripMenuItem audioMenuVgmSelect;
        private ToolStripMenuItem audioMenuVgmGithub;
        private Label sLableOldFile;
        private Label labelOldFile;
        private Button buttonPlayOldFile;
        private PictureBox oldNewArrow;
        private HelpProvider helpProvider1;
        private Label sLabelNewFile;
        private Label labelNewFile;
        private Button buttonSelectNewFile;
        private Button buttonPlayNewFile;
        private Button buttonRemoveNewFile;
        private ToolStripMenuItem audioMenuBNKGitHubDownload;
        private ToolStripMenuItem audioMenuVgmGithubDownload;
    }
}
