namespace TribalWars.Forms
{
    partial class VillageCoordinatesImporterForm
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
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.VillageCoordsInputBox = new Janus.Windows.GridEX.EditControls.EditBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.VillageCoordsInputBox);
            this.uiGroupBox1.Location = new System.Drawing.Point(12, 12);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(673, 412);
            this.uiGroupBox1.TabIndex = 0;
            // 
            // VillageCoordsInputBox
            // 
            this.VillageCoordsInputBox.Location = new System.Drawing.Point(6, 20);
            this.VillageCoordsInputBox.Multiline = true;
            this.VillageCoordsInputBox.Name = "VillageCoordsInputBox";
            this.VillageCoordsInputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.VillageCoordsInputBox.Size = new System.Drawing.Size(255, 133);
            this.VillageCoordsInputBox.TabIndex = 0;
            // 
            // VillageCoordinatesImporterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 560);
            this.Controls.Add(this.uiGroupBox1);
            this.Name = "VillageCoordinatesImporterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import village coordinates";
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.EditControls.EditBox VillageCoordsInputBox;
    }
}