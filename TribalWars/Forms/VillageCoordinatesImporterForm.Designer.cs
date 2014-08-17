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
            this.label1 = new System.Windows.Forms.Label();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.playerTribeDropdown1 = new TribalWars.Controls.Finders.PlayerTribeDropdown();
            this.ClearTextFieldButton = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uiGroupBox1.Controls.Add(this.ClearTextFieldButton);
            this.uiGroupBox1.Controls.Add(this.VillageCoordsInputBox);
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(12, 67);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(268, 357);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Or paste village coordinates here:";
            // 
            // VillageCoordsInputBox
            // 
            this.VillageCoordsInputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VillageCoordsInputBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VillageCoordsInputBox.Location = new System.Drawing.Point(6, 20);
            this.VillageCoordsInputBox.Multiline = true;
            this.VillageCoordsInputBox.Name = "VillageCoordsInputBox";
            this.VillageCoordsInputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.VillageCoordsInputBox.Size = new System.Drawing.Size(255, 304);
            this.VillageCoordsInputBox.TabIndex = 0;
            this.VillageCoordsInputBox.TextChanged += new System.EventHandler(this.VillageCoordsInputBox_TextChanged);
            // 
            // villagesGridExControl1
            // 
            this.villagesGridExControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.villagesGridExControl1.Location = new System.Drawing.Point(3, 39);
            this.villagesGridExControl1.Margin = new System.Windows.Forms.Padding(0);
            this.villagesGridExControl1.Name = "villagesGridExControl1";
            this.villagesGridExControl1.ShowPlayer = true;
            this.villagesGridExControl1.Size = new System.Drawing.Size(384, 367);
            this.villagesGridExControl1.TabIndex = 1;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Controls.Add(this.villagesGridExControl1);
            this.uiGroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Location = new System.Drawing.Point(286, 12);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(390, 412);
            this.uiGroupBox2.TabIndex = 2;
            this.uiGroupBox2.Text = "... And manipulate the villages here:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(377, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select village rows and right click to set purpose or use them in attack plans.";
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.playerTribeDropdown1);
            this.uiGroupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox3.Location = new System.Drawing.Point(12, 13);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(268, 48);
            this.uiGroupBox3.TabIndex = 3;
            this.uiGroupBox3.Text = "Select a player here:";
            // 
            // playerTribeDropdown1
            // 
            this.playerTribeDropdown1.AllowTribe = false;
            this.playerTribeDropdown1.AutoOpenOnFocus = false;
            this.playerTribeDropdown1.Location = new System.Drawing.Point(7, 19);
            this.playerTribeDropdown1.Margin = new System.Windows.Forms.Padding(0);
            this.playerTribeDropdown1.Name = "playerTribeDropdown1";
            this.playerTribeDropdown1.Size = new System.Drawing.Size(254, 22);
            this.playerTribeDropdown1.TabIndex = 1;
            this.playerTribeDropdown1.PlayerSelected += new System.EventHandler<TribalWars.Worlds.Events.Impls.PlayerEventArgs>(this.playerTribeDropdown1_PlayerSelected);
            // 
            // ClearTextFieldButton
            // 
            this.ClearTextFieldButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearTextFieldButton.Location = new System.Drawing.Point(7, 331);
            this.ClearTextFieldButton.Name = "ClearTextFieldButton";
            this.ClearTextFieldButton.Size = new System.Drawing.Size(75, 23);
            this.ClearTextFieldButton.TabIndex = 1;
            this.ClearTextFieldButton.Text = "Clear";
            this.ClearTextFieldButton.Click += new System.EventHandler(this.ClearTextFieldButton_Click);
            // 
            // VillageCoordinatesImporterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 433);
            this.Controls.Add(this.uiGroupBox3);
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
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.EditControls.EditBox VillageCoordsInputBox;
        private Controls.GridExs.VillagesGridExControl villagesGridExControl1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Controls.Finders.PlayerTribeDropdown playerTribeDropdown1;
        private Janus.Windows.EditControls.UIButton ClearTextFieldButton;
    }
}