namespace TribalWars.Controls.Finders
{
    partial class PlayerTribeDropdown
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			Janus.Windows.GridEX.GridEXLayout SelectorControl_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerTribeDropdown));
			this.SelectorControl = new Janus.Windows.GridEX.EditControls.MultiColumnCombo();
			this.SearchTypeImageList = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.SelectorControl)).BeginInit();
			this.SuspendLayout();
			// 
			// SelectorControl
			// 
			this.SelectorControl.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
			SelectorControl_DesignTimeLayout.LayoutString = resources.GetString("SelectorControl_DesignTimeLayout.LayoutString");
			this.SelectorControl.DesignTimeLayout = SelectorControl_DesignTimeLayout;
			this.SelectorControl.DisplayMember = "Value";
			this.SelectorControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SelectorControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.55F);
			this.SelectorControl.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight;
			this.SelectorControl.ImageList = this.SearchTypeImageList;
			this.SelectorControl.Location = new System.Drawing.Point(0, 0);
			this.SelectorControl.Name = "SelectorControl";
			this.SelectorControl.SelectedIndex = -1;
			this.SelectorControl.SelectedItem = null;
			this.SelectorControl.Size = new System.Drawing.Size(133, 22);
			this.SelectorControl.TabIndex = 12;
			this.SelectorControl.ValueMember = "Value";
			this.SelectorControl.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
			this.SelectorControl.ValueChanged += new System.EventHandler(this.SelectorControl_ValueChanged);
			this.SelectorControl.DropDown += new System.EventHandler(this.SelectorControl_DropDown);
			this.SelectorControl.TextChanged += new System.EventHandler(this.SelectorControl_TextChanged);
			this.SelectorControl.Enter += new System.EventHandler(this.SelectorControl_Enter);
			// 
			// SearchTypeImageList
			// 
			this.SearchTypeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SearchTypeImageList.ImageStream")));
			this.SearchTypeImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.SearchTypeImageList.Images.SetKeyName(0, "Village.jpg");
			this.SearchTypeImageList.Images.SetKeyName(1, "Player.jpg");
			this.SearchTypeImageList.Images.SetKeyName(2, "Tribe.jpg");
			// 
			// PlayerTribeDropdown
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SelectorControl);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "PlayerTribeDropdown";
			this.Size = new System.Drawing.Size(133, 22);
			this.Load += new System.EventHandler(this.VillagePlayerTribeSelector_Load);
			((System.ComponentModel.ISupportInitialize)(this.SelectorControl)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList SearchTypeImageList;
        public Janus.Windows.GridEX.EditControls.MultiColumnCombo SelectorControl;
    }
}
