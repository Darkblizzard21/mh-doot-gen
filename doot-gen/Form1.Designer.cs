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
            SLabelWwiseConsole = new Label();
            SelectConsole = new Button();
            labelWwiseConsole = new Label();
            SLabelWWiseProject = new Label();
            SelectProject = new Button();
            labelWwiseProject = new Label();
            SLabelGameFiles = new Label();
            SelectGameFiles = new Button();
            labelGameFiles = new Label();
            menuStrip1 = new MenuStrip();
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
            // menuStrip1
            // 
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 9;
            menuStrip1.Text = "menuStrip1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelGameFiles);
            Controls.Add(SelectGameFiles);
            Controls.Add(SLabelGameFiles);
            Controls.Add(labelWwiseProject);
            Controls.Add(SelectProject);
            Controls.Add(SLabelWWiseProject);
            Controls.Add(labelWwiseConsole);
            Controls.Add(SelectConsole);
            Controls.Add(SLabelWwiseConsole);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
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
        private MenuStrip menuStrip1;
    }
}
