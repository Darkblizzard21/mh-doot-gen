﻿namespace doot_gen
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
            currentWemFile = new Label();
            ConfigMenuStrip.SuspendLayout();
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
            audioMenuBNK.DropDownItems.AddRange(new ToolStripItem[] { audioMenuBNKSelect, audioMenuBNKGitHub });
            audioMenuBNK.Name = "audioMenuBNK";
            audioMenuBNK.Size = new Size(180, 22);
            audioMenuBNK.Text = "bnkextr.exe";
            // 
            // audioMenuBNKSelect
            // 
            audioMenuBNKSelect.Name = "audioMenuBNKSelect";
            audioMenuBNKSelect.Size = new Size(180, 22);
            audioMenuBNKSelect.Text = "Select exe";
            audioMenuBNKSelect.Click += audioMenuBNKSelect_Click;
            // 
            // audioMenuBNKGitHub
            // 
            audioMenuBNKGitHub.Name = "audioMenuBNKGitHub";
            audioMenuBNKGitHub.Size = new Size(180, 22);
            audioMenuBNKGitHub.Text = "GitHub Page";
            audioMenuBNKGitHub.Click += audioMenuBNKGitHub_Click;
            // 
            // audioMenuVgm
            // 
            audioMenuVgm.DropDownItems.AddRange(new ToolStripItem[] { audioMenuVgmSelect, audioMenuVgmGithub });
            audioMenuVgm.Name = "audioMenuVgm";
            audioMenuVgm.Size = new Size(180, 22);
            audioMenuVgm.Text = "vgmstream";
            // 
            // audioMenuVgmSelect
            // 
            audioMenuVgmSelect.Name = "audioMenuVgmSelect";
            audioMenuVgmSelect.Size = new Size(180, 22);
            audioMenuVgmSelect.Text = "Select exe";
            audioMenuVgmSelect.Click += audioMenuVgmSelect_Click;
            // 
            // audioMenuVgmGithub
            // 
            audioMenuVgmGithub.Name = "audioMenuVgmGithub";
            audioMenuVgmGithub.Size = new Size(180, 22);
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
            fileTree.Size = new Size(288, 294);
            fileTree.TabIndex = 12;
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
            // currentWemFile
            // 
            currentWemFile.AutoSize = true;
            currentWemFile.Location = new Point(306, 148);
            currentWemFile.Name = "currentWemFile";
            currentWemFile.Size = new Size(161, 15);
            currentWemFile.TabIndex = 15;
            currentWemFile.Text = "Select a WEM in the tree view";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(currentWemFile);
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
        private Label currentWemFile;
        private ToolStripMenuItem audioMenu;
        private ToolStripMenuItem audioMenuBNK;
        private ToolStripMenuItem audioMenuBNKSelect;
        private ToolStripMenuItem audioMenuBNKGitHub;
        private ToolStripMenuItem audioMenuVgm;
        private ToolStripMenuItem audioMenuVgmSelect;
        private ToolStripMenuItem audioMenuVgmGithub;
    }
}
