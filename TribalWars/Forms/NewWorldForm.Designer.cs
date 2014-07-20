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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Servers = new System.Windows.Forms.ComboBox();
            this.AvailableWorlds = new System.Windows.Forms.ComboBox();
            this.OpenFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2.SuspendLayout();
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
            this.btnNewWorld.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewWorld.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewWorld.Location = new System.Drawing.Point(10, 73);
            this.btnNewWorld.Name = "btnNewWorld";
            this.btnNewWorld.Size = new System.Drawing.Size(221, 43);
            this.btnNewWorld.TabIndex = 1;
            this.btnNewWorld.Text = "&Create New World";
            this.btnNewWorld.Click += new System.EventHandler(this.btnNewWorld_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.Servers);
            this.groupBox2.Controls.Add(this.btnNewWorld);
            this.groupBox2.Controls.Add(this.AvailableWorlds);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(237, 123);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create a new world";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "World:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server:";
            // 
            // Servers
            // 
            this.Servers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Servers.DisplayMember = "ServerUrl";
            this.Servers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Servers.FormattingEnabled = true;
            this.Servers.Location = new System.Drawing.Point(54, 19);
            this.Servers.Name = "Servers";
            this.Servers.Size = new System.Drawing.Size(177, 21);
            this.Servers.TabIndex = 2;
            this.Servers.SelectedIndexChanged += new System.EventHandler(this.Servers_SelectedIndexChanged);
            // 
            // AvailableWorlds
            // 
            this.AvailableWorlds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AvailableWorlds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AvailableWorlds.FormattingEnabled = true;
            this.AvailableWorlds.Location = new System.Drawing.Point(54, 46);
            this.AvailableWorlds.Name = "AvailableWorlds";
            this.AvailableWorlds.Size = new System.Drawing.Size(177, 21);
            this.AvailableWorlds.TabIndex = 2;
            // 
            // NewWorldForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 147);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewWorldForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create World";
            this.Load += new System.EventHandler(this.LoadWorldForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList IconList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FolderBrowserDialog OpenFolder;
        private Janus.Windows.EditControls.UIButton btnNewWorld;
        private System.Windows.Forms.ComboBox AvailableWorlds;
        private System.Windows.Forms.ComboBox Servers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;


    }
}