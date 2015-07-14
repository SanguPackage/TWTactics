namespace TribalWars.Maps.Markers
{
    partial class MarkerSettingsControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkerSettingsControl));
			this.MarkerActive = new Janus.Windows.EditControls.UICheckBox();
			this.MarkerActivePanel = new System.Windows.Forms.Panel();
			this.MarkerView = new Janus.Windows.EditControls.UIComboBox();
			this.MarkerExtraColor = new Janus.Windows.EditControls.UIColorButton();
			this.MarkerColor = new Janus.Windows.EditControls.UIColorButton();
			this.BilliardMarkerPicturebox = new System.Windows.Forms.PictureBox();
			this.MarkerActivePanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BilliardMarkerPicturebox)).BeginInit();
			this.SuspendLayout();
			// 
			// MarkerActive
			// 
			resources.ApplyResources(this.MarkerActive, "MarkerActive");
			this.MarkerActive.Checked = true;
			this.MarkerActive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.MarkerActive.Name = "MarkerActive";
			this.MarkerActive.ShowFocusRectangle = false;
			this.MarkerActive.CheckedChanged += new System.EventHandler(this.MarkerActive_CheckedChanged);
			// 
			// MarkerActivePanel
			// 
			resources.ApplyResources(this.MarkerActivePanel, "MarkerActivePanel");
			this.MarkerActivePanel.Controls.Add(this.MarkerView);
			this.MarkerActivePanel.Controls.Add(this.MarkerExtraColor);
			this.MarkerActivePanel.Controls.Add(this.MarkerColor);
			this.MarkerActivePanel.Name = "MarkerActivePanel";
			// 
			// MarkerView
			// 
			resources.ApplyResources(this.MarkerView, "MarkerView");
			this.MarkerView.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
			this.MarkerView.Name = "MarkerView";
			this.MarkerView.SelectedValueChanged += new System.EventHandler(this.MarkerView_SelectedValueChanged);
			// 
			// MarkerExtraColor
			// 
			resources.ApplyResources(this.MarkerExtraColor, "MarkerExtraColor");
			// 
			// 
			// 
			this.MarkerExtraColor.ColorPicker.AccessibleDescription = resources.GetString("MarkerExtraColor.ColorPicker.AccessibleDescription");
			this.MarkerExtraColor.ColorPicker.AccessibleName = resources.GetString("MarkerExtraColor.ColorPicker.AccessibleName");
			this.MarkerExtraColor.ColorPicker.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("MarkerExtraColor.ColorPicker.Anchor")));
			this.MarkerExtraColor.ColorPicker.AutomaticButtonText = resources.GetString("MarkerExtraColor.ColorPicker.AutomaticButtonText");
			this.MarkerExtraColor.ColorPicker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MarkerExtraColor.ColorPicker.BackgroundImage")));
			this.MarkerExtraColor.ColorPicker.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("MarkerExtraColor.ColorPicker.BackgroundImageLayout")));
			this.MarkerExtraColor.ColorPicker.BorderStyle = Janus.Windows.UI.BorderStyle.None;
			this.MarkerExtraColor.ColorPicker.Columns = ((int)(resources.GetObject("MarkerExtraColor.ColorPicker.Columns")));
			this.MarkerExtraColor.ColorPicker.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("MarkerExtraColor.ColorPicker.Dock")));
			this.MarkerExtraColor.ColorPicker.Font = ((System.Drawing.Font)(resources.GetObject("MarkerExtraColor.ColorPicker.Font")));
			this.MarkerExtraColor.ColorPicker.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("MarkerExtraColor.ColorPicker.ImeMode")));
			this.MarkerExtraColor.ColorPicker.Location = ((System.Drawing.Point)(resources.GetObject("MarkerExtraColor.ColorPicker.Location")));
			this.MarkerExtraColor.ColorPicker.MaximumSize = ((System.Drawing.Size)(resources.GetObject("MarkerExtraColor.ColorPicker.MaximumSize")));
			this.MarkerExtraColor.ColorPicker.MoreColorsButtonText = resources.GetString("MarkerExtraColor.ColorPicker.MoreColorsButtonText");
			this.MarkerExtraColor.ColorPicker.Name = "";
			this.MarkerExtraColor.ColorPicker.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("MarkerExtraColor.ColorPicker.RightToLeft")));
			this.MarkerExtraColor.ColorPicker.ShowAutomaticButton = ((bool)(resources.GetObject("MarkerExtraColor.ColorPicker.ShowAutomaticButton")));
			this.MarkerExtraColor.ColorPicker.ShowMoreColorsButton = ((bool)(resources.GetObject("MarkerExtraColor.ColorPicker.ShowMoreColorsButton")));
			this.MarkerExtraColor.ColorPicker.ShowToolTips = ((bool)(resources.GetObject("MarkerExtraColor.ColorPicker.ShowToolTips")));
			this.MarkerExtraColor.ColorPicker.Size = ((System.Drawing.Size)(resources.GetObject("MarkerExtraColor.ColorPicker.Size")));
			this.MarkerExtraColor.ColorPicker.TabIndex = ((int)(resources.GetObject("MarkerExtraColor.ColorPicker.TabIndex")));
			this.MarkerExtraColor.Name = "MarkerExtraColor";
			this.MarkerExtraColor.SelectedColorChanged += new System.EventHandler(this.MarkerExtraColor_SelectedColorChanged);
			// 
			// MarkerColor
			// 
			resources.ApplyResources(this.MarkerColor, "MarkerColor");
			// 
			// 
			// 
			this.MarkerColor.ColorPicker.AccessibleDescription = resources.GetString("MarkerColor.ColorPicker.AccessibleDescription");
			this.MarkerColor.ColorPicker.AccessibleName = resources.GetString("MarkerColor.ColorPicker.AccessibleName");
			this.MarkerColor.ColorPicker.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("MarkerColor.ColorPicker.Anchor")));
			this.MarkerColor.ColorPicker.AutomaticButtonText = resources.GetString("MarkerColor.ColorPicker.AutomaticButtonText");
			this.MarkerColor.ColorPicker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MarkerColor.ColorPicker.BackgroundImage")));
			this.MarkerColor.ColorPicker.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("MarkerColor.ColorPicker.BackgroundImageLayout")));
			this.MarkerColor.ColorPicker.BorderStyle = Janus.Windows.UI.BorderStyle.None;
			this.MarkerColor.ColorPicker.Columns = ((int)(resources.GetObject("MarkerColor.ColorPicker.Columns")));
			this.MarkerColor.ColorPicker.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("MarkerColor.ColorPicker.Dock")));
			this.MarkerColor.ColorPicker.Font = ((System.Drawing.Font)(resources.GetObject("MarkerColor.ColorPicker.Font")));
			this.MarkerColor.ColorPicker.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("MarkerColor.ColorPicker.ImeMode")));
			this.MarkerColor.ColorPicker.Location = ((System.Drawing.Point)(resources.GetObject("MarkerColor.ColorPicker.Location")));
			this.MarkerColor.ColorPicker.MaximumSize = ((System.Drawing.Size)(resources.GetObject("MarkerColor.ColorPicker.MaximumSize")));
			this.MarkerColor.ColorPicker.MoreColorsButtonText = resources.GetString("MarkerColor.ColorPicker.MoreColorsButtonText");
			this.MarkerColor.ColorPicker.Name = "";
			this.MarkerColor.ColorPicker.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("MarkerColor.ColorPicker.RightToLeft")));
			this.MarkerColor.ColorPicker.ShowAutomaticButton = ((bool)(resources.GetObject("MarkerColor.ColorPicker.ShowAutomaticButton")));
			this.MarkerColor.ColorPicker.ShowMoreColorsButton = ((bool)(resources.GetObject("MarkerColor.ColorPicker.ShowMoreColorsButton")));
			this.MarkerColor.ColorPicker.ShowToolTips = ((bool)(resources.GetObject("MarkerColor.ColorPicker.ShowToolTips")));
			this.MarkerColor.ColorPicker.Size = ((System.Drawing.Size)(resources.GetObject("MarkerColor.ColorPicker.Size")));
			this.MarkerColor.ColorPicker.TabIndex = ((int)(resources.GetObject("MarkerColor.ColorPicker.TabIndex")));
			this.MarkerColor.Name = "MarkerColor";
			this.MarkerColor.SelectedColorChanged += new System.EventHandler(this.MarkerColor_SelectedColorChanged);
			// 
			// BilliardMarkerPicturebox
			// 
			resources.ApplyResources(this.BilliardMarkerPicturebox, "BilliardMarkerPicturebox");
			this.BilliardMarkerPicturebox.Name = "BilliardMarkerPicturebox";
			this.BilliardMarkerPicturebox.TabStop = false;
			// 
			// MarkerSettingsControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.BilliardMarkerPicturebox);
			this.Controls.Add(this.MarkerActivePanel);
			this.Controls.Add(this.MarkerActive);
			this.Name = "MarkerSettingsControl";
			this.Load += new System.EventHandler(this.MarkerSettingsControl_Load);
			this.MarkerActivePanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.BilliardMarkerPicturebox)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UICheckBox MarkerActive;
        private System.Windows.Forms.Panel MarkerActivePanel;
        private Janus.Windows.EditControls.UIColorButton MarkerExtraColor;
        private Janus.Windows.EditControls.UIColorButton MarkerColor;
        private Janus.Windows.EditControls.UIComboBox MarkerView;
        private System.Windows.Forms.PictureBox BilliardMarkerPicturebox;

    }
}
