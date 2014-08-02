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
            this.villagesGridExControl1 = new TribalWars.Controls.GridExs.VillagesGridExControl();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uiGroupBox1.Controls.Add(this.VillageCoordsInputBox);
            this.uiGroupBox1.Location = new System.Drawing.Point(12, 12);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(268, 412);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Paste village coordinates here:";
            // 
            // VillageCoordsInputBox
            // 
            this.VillageCoordsInputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VillageCoordsInputBox.Location = new System.Drawing.Point(6, 20);
            this.VillageCoordsInputBox.Multiline = true;
            this.VillageCoordsInputBox.Name = "VillageCoordsInputBox";
            this.VillageCoordsInputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.VillageCoordsInputBox.Size = new System.Drawing.Size(255, 386);
            this.VillageCoordsInputBox.TabIndex = 0;
            this.VillageCoordsInputBox.TextChanged += new System.EventHandler(this.VillageCoordsInputBox_TextChanged);
            // 
            // villagesGridExControl1
            // 
            this.villagesGridExControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.villagesGridExControl1.Location = new System.Drawing.Point(3, 20);
            this.villagesGridExControl1.Margin = new System.Windows.Forms.Padding(0);
            this.villagesGridExControl1.Name = "villagesGridExControl1";
            this.villagesGridExControl1.Size = new System.Drawing.Size(384, 386);
            this.villagesGridExControl1.TabIndex = 1;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox2.Controls.Add(this.villagesGridExControl1);
            this.uiGroupBox2.Location = new System.Drawing.Point(286, 12);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(390, 412);
            this.uiGroupBox2.TabIndex = 2;
            this.uiGroupBox2.Text = "... And manipulate the villages here:";
            // 
            // VillageCoordinatesImporterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 433);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VillageCoordinatesImporterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import village coordinates";
            this.TopMost = true;
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.VillageCoordinatesImporterForm_HelpButtonClicked);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.EditControls.EditBox VillageCoordsInputBox;
        private Controls.GridExs.VillagesGridExControl villagesGridExControl1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
    }
}