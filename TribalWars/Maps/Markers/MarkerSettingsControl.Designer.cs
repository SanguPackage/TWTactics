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
            this.MarkerActive.Checked = true;
            this.MarkerActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MarkerActive.Location = new System.Drawing.Point(32, 0);
            this.MarkerActive.Name = "MarkerActive";
            this.MarkerActive.ShowFocusRectangle = false;
            this.MarkerActive.Size = new System.Drawing.Size(16, 25);
            this.MarkerActive.TabIndex = 0;
            this.MarkerActive.ToolTipText = "Activate or disable the marker";
            this.MarkerActive.CheckedChanged += new System.EventHandler(this.MarkerActive_CheckedChanged);
            // 
            // MarkerActivePanel
            // 
            this.MarkerActivePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MarkerActivePanel.Controls.Add(this.MarkerView);
            this.MarkerActivePanel.Controls.Add(this.MarkerExtraColor);
            this.MarkerActivePanel.Controls.Add(this.MarkerColor);
            this.MarkerActivePanel.Location = new System.Drawing.Point(52, 0);
            this.MarkerActivePanel.Margin = new System.Windows.Forms.Padding(0);
            this.MarkerActivePanel.Name = "MarkerActivePanel";
            this.MarkerActivePanel.Size = new System.Drawing.Size(247, 25);
            this.MarkerActivePanel.TabIndex = 1;
            // 
            // MarkerView
            // 
            this.MarkerView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MarkerView.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.MarkerView.Location = new System.Drawing.Point(156, 3);
            this.MarkerView.Name = "MarkerView";
            this.MarkerView.Size = new System.Drawing.Size(88, 20);
            this.MarkerView.TabIndex = 2;
            this.MarkerView.SelectedValueChanged += new System.EventHandler(this.MarkerView_SelectedValueChanged);
            // 
            // MarkerExtraColor
            // 
            // 
            // 
            // 
            this.MarkerExtraColor.ColorPicker.BorderStyle = Janus.Windows.UI.BorderStyle.None;
            this.MarkerExtraColor.ColorPicker.Location = new System.Drawing.Point(0, 0);
            this.MarkerExtraColor.ColorPicker.Name = "";
            this.MarkerExtraColor.ColorPicker.Size = new System.Drawing.Size(100, 100);
            this.MarkerExtraColor.ColorPicker.TabIndex = 0;
            this.MarkerExtraColor.Location = new System.Drawing.Point(78, 2);
            this.MarkerExtraColor.Name = "MarkerExtraColor";
            this.MarkerExtraColor.Size = new System.Drawing.Size(75, 23);
            this.MarkerExtraColor.TabIndex = 1;
            this.MarkerExtraColor.ToolTipText = "Choose inner color";
            this.MarkerExtraColor.SelectedColorChanged += new System.EventHandler(this.MarkerExtraColor_SelectedColorChanged);
            // 
            // MarkerColor
            // 
            // 
            // 
            // 
            this.MarkerColor.ColorPicker.BorderStyle = Janus.Windows.UI.BorderStyle.None;
            this.MarkerColor.ColorPicker.Location = new System.Drawing.Point(0, 0);
            this.MarkerColor.ColorPicker.Name = "";
            this.MarkerColor.ColorPicker.Size = new System.Drawing.Size(100, 100);
            this.MarkerColor.ColorPicker.TabIndex = 0;
            this.MarkerColor.Location = new System.Drawing.Point(2, 2);
            this.MarkerColor.Name = "MarkerColor";
            this.MarkerColor.Size = new System.Drawing.Size(75, 23);
            this.MarkerColor.TabIndex = 0;
            this.MarkerColor.ToolTipText = "Choose main color";
            this.MarkerColor.SelectedColorChanged += new System.EventHandler(this.MarkerColor_SelectedColorChanged);
            // 
            // BilliardMarkerPicturebox
            // 
            this.BilliardMarkerPicturebox.Image = ((System.Drawing.Image)(resources.GetObject("BilliardMarkerPicturebox.Image")));
            this.BilliardMarkerPicturebox.Location = new System.Drawing.Point(5, 3);
            this.BilliardMarkerPicturebox.Name = "BilliardMarkerPicturebox";
            this.BilliardMarkerPicturebox.Size = new System.Drawing.Size(24, 22);
            this.BilliardMarkerPicturebox.TabIndex = 2;
            this.BilliardMarkerPicturebox.TabStop = false;
            // 
            // MarkerSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.BilliardMarkerPicturebox);
            this.Controls.Add(this.MarkerActivePanel);
            this.Controls.Add(this.MarkerActive);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MarkerSettingsControl";
            this.Size = new System.Drawing.Size(299, 25);
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
