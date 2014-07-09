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
            this.MarkerActive = new Janus.Windows.EditControls.UICheckBox();
            this.MarkerActivePanel = new System.Windows.Forms.Panel();
            this.MarkerView = new Janus.Windows.EditControls.UIComboBox();
            this.MarkerExtraColor = new Janus.Windows.EditControls.UIColorButton();
            this.MarkerColor = new Janus.Windows.EditControls.UIColorButton();
            this.label1 = new System.Windows.Forms.Label();
            this.MarkerActivePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MarkerActive
            // 
            this.MarkerActive.Checked = true;
            this.MarkerActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MarkerActive.Location = new System.Drawing.Point(36, 6);
            this.MarkerActive.Name = "MarkerActive";
            this.MarkerActive.Size = new System.Drawing.Size(16, 16);
            this.MarkerActive.TabIndex = 0;
            this.MarkerActive.ToolTipText = "Activate or disable the marker";
            this.MarkerActive.CheckedChanged += new System.EventHandler(this.MarkerActive_CheckedChanged);
            // 
            // MarkerActivePanel
            // 
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mark";
            // 
            // MarkerSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MarkerActivePanel);
            this.Controls.Add(this.MarkerActive);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MarkerSettingsControl";
            this.Size = new System.Drawing.Size(299, 25);
            this.Load += new System.EventHandler(this.MarkersContainerControl_Load);
            this.MarkerActivePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UICheckBox MarkerActive;
        private System.Windows.Forms.Panel MarkerActivePanel;
        private Janus.Windows.EditControls.UIColorButton MarkerExtraColor;
        private Janus.Windows.EditControls.UIColorButton MarkerColor;
        private Janus.Windows.EditControls.UIComboBox MarkerView;
        private System.Windows.Forms.Label label1;

    }
}
