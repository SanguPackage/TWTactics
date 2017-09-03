namespace TribalWars.Forms
{
    partial class LoadWorldForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadWorldForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ExistingWorldsGroupbox = new System.Windows.Forms.GroupBox();
            this.Worlds = new System.Windows.Forms.TreeView();
            this.IconList = new System.Windows.Forms.ImageList(this.components);
            this.btnLoad = new Janus.Windows.EditControls.UIButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.AvailableSettingsGroupbox = new System.Windows.Forms.GroupBox();
            this.WorldSettings = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OpenFolder = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ExistingWorldsGroupbox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.AvailableSettingsGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.ExistingWorldsGroupbox);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.TabStop = false;
            // 
            // ExistingWorldsGroupbox
            // 
            resources.ApplyResources(this.ExistingWorldsGroupbox, "ExistingWorldsGroupbox");
            this.ExistingWorldsGroupbox.Controls.Add(this.Worlds);
            this.ExistingWorldsGroupbox.Controls.Add(this.btnLoad);
            this.ExistingWorldsGroupbox.Name = "ExistingWorldsGroupbox";
            this.ExistingWorldsGroupbox.TabStop = false;
            // 
            // Worlds
            // 
            resources.ApplyResources(this.Worlds, "Worlds");
            this.Worlds.HideSelection = false;
            this.Worlds.ImageList = this.IconList;
            this.Worlds.Name = "Worlds";
            this.Worlds.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Worlds_AfterSelect);
            this.Worlds.DoubleClick += new System.EventHandler(this.Worlds_DoubleClick);
            // 
            // IconList
            // 
            this.IconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconList.ImageStream")));
            this.IconList.TransparentColor = System.Drawing.Color.Transparent;
            this.IconList.Images.SetKeyName(0, "123.ico");
            this.IconList.Images.SetKeyName(1, "150.ico");
            this.IconList.Images.SetKeyName(2, "116.ico");
            // 
            // btnLoad
            // 
            resources.ApplyResources(this.btnLoad, "btnLoad");
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.AvailableSettingsGroupbox, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // AvailableSettingsGroupbox
            // 
            resources.ApplyResources(this.AvailableSettingsGroupbox, "AvailableSettingsGroupbox");
            this.AvailableSettingsGroupbox.Controls.Add(this.WorldSettings);
            this.AvailableSettingsGroupbox.Name = "AvailableSettingsGroupbox";
            this.AvailableSettingsGroupbox.TabStop = false;
            // 
            // WorldSettings
            // 
            resources.ApplyResources(this.WorldSettings, "WorldSettings");
            this.WorldSettings.BackgroundImageTiled = true;
            this.WorldSettings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.WorldSettings.FullRowSelect = true;
            this.WorldSettings.GridLines = true;
            this.WorldSettings.HideSelection = false;
            this.WorldSettings.Name = "WorldSettings";
            this.WorldSettings.UseCompatibleStateImageBehavior = false;
            this.WorldSettings.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // OpenFolder
            // 
            resources.ApplyResources(this.OpenFolder, "OpenFolder");
            // 
            // LoadWorldForm
            // 
            this.AcceptButton = this.btnLoad;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadWorldForm";
            this.Load += new System.EventHandler(this.LoadWorldForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ExistingWorldsGroupbox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.AvailableSettingsGroupbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList IconList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIButton btnLoad;
        private System.Windows.Forms.TreeView Worlds;
        private System.Windows.Forms.ListView WorldSettings;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.FolderBrowserDialog OpenFolder;
        private System.Windows.Forms.GroupBox ExistingWorldsGroupbox;
        private System.Windows.Forms.GroupBox AvailableSettingsGroupbox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;


    }
}