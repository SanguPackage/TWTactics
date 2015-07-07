namespace TribalWars.Forms
{
    partial class NewWorldForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewWorldForm));
			this.IconList = new System.Windows.Forms.ImageList(this.components);
			this.btnNewWorld = new Janus.Windows.EditControls.UIButton();
			this.CreateANewWorldGroupbox = new System.Windows.Forms.GroupBox();
			this.worldLabel = new System.Windows.Forms.Label();
			this.serverLabel = new System.Windows.Forms.Label();
			this.Servers = new System.Windows.Forms.ComboBox();
			this.AvailableWorlds = new System.Windows.Forms.ComboBox();
			this.OpenFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.CreateANewWorldGroupbox.SuspendLayout();
			this.SuspendLayout();
			// 
			// IconList
			// 
			this.IconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconList.ImageStream")));
			this.IconList.TransparentColor = System.Drawing.Color.Transparent;
			this.IconList.Images.SetKeyName(0, "123.ico");
			this.IconList.Images.SetKeyName(1, "150.ico");
			this.IconList.Images.SetKeyName(2, "116.ico");
			// 
			// btnNewWorld
			// 
			resources.ApplyResources(this.btnNewWorld, "btnNewWorld");
			this.btnNewWorld.Name = "btnNewWorld";
			this.btnNewWorld.Click += new System.EventHandler(this.btnNewWorld_Click);
			// 
			// CreateANewWorldGroupbox
			// 
			resources.ApplyResources(this.CreateANewWorldGroupbox, "CreateANewWorldGroupbox");
			this.CreateANewWorldGroupbox.Controls.Add(this.worldLabel);
			this.CreateANewWorldGroupbox.Controls.Add(this.serverLabel);
			this.CreateANewWorldGroupbox.Controls.Add(this.Servers);
			this.CreateANewWorldGroupbox.Controls.Add(this.btnNewWorld);
			this.CreateANewWorldGroupbox.Controls.Add(this.AvailableWorlds);
			this.CreateANewWorldGroupbox.Name = "CreateANewWorldGroupbox";
			this.CreateANewWorldGroupbox.TabStop = false;
			// 
			// worldLabel
			// 
			resources.ApplyResources(this.worldLabel, "worldLabel");
			this.worldLabel.Name = "worldLabel";
			// 
			// serverLabel
			// 
			resources.ApplyResources(this.serverLabel, "serverLabel");
			this.serverLabel.Name = "serverLabel";
			// 
			// Servers
			// 
			resources.ApplyResources(this.Servers, "Servers");
			this.Servers.DisplayMember = "ServerUrl";
			this.Servers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Servers.FormattingEnabled = true;
			this.Servers.Name = "Servers";
			this.Servers.SelectedIndexChanged += new System.EventHandler(this.Servers_SelectedIndexChanged);
			// 
			// AvailableWorlds
			// 
			resources.ApplyResources(this.AvailableWorlds, "AvailableWorlds");
			this.AvailableWorlds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.AvailableWorlds.FormattingEnabled = true;
			this.AvailableWorlds.Name = "AvailableWorlds";
			// 
			// OpenFolder
			// 
			resources.ApplyResources(this.OpenFolder, "OpenFolder");
			// 
			// NewWorldForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CreateANewWorldGroupbox);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewWorldForm";
			this.ShowIcon = false;
			this.Load += new System.EventHandler(this.LoadWorldForm_Load);
			this.CreateANewWorldGroupbox.ResumeLayout(false);
			this.CreateANewWorldGroupbox.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList IconList;
        private System.Windows.Forms.GroupBox CreateANewWorldGroupbox;
        private System.Windows.Forms.FolderBrowserDialog OpenFolder;
        private Janus.Windows.EditControls.UIButton btnNewWorld;
        private System.Windows.Forms.ComboBox AvailableWorlds;
        private System.Windows.Forms.ComboBox Servers;
        private System.Windows.Forms.Label worldLabel;
        private System.Windows.Forms.Label serverLabel;


    }
}