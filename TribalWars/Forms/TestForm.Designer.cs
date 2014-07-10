namespace TribalWars.Forms
{
    partial class TestForm
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
            Janus.Windows.GridEX.GridEXLayout multiColumnCombo1_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.multiColumnCombo1 = new Janus.Windows.GridEX.EditControls.MultiColumnCombo();
            this.SearchTypeImageList = new System.Windows.Forms.ImageList(this.components);
            this.CloseButton = new Janus.Windows.EditControls.UIButton();
            this.villagePlayerTribeSelector1 = new TribalWars.Controls.Finders.PlayerTribeDropdown();
            this.villagePlayerTribeSelectorOld1 = new TribalWars.Controls.Finders.VillagePlayerTribeSelector();
            this.editBox1 = new Janus.Windows.GridEX.EditControls.EditBox();
            ((System.ComponentModel.ISupportInitialize)(this.multiColumnCombo1)).BeginInit();
            this.SuspendLayout();
            // 
            // multiColumnCombo1
            // 
            this.multiColumnCombo1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
            multiColumnCombo1_DesignTimeLayout.LayoutString = resources.GetString("multiColumnCombo1_DesignTimeLayout.LayoutString");
            this.multiColumnCombo1.DesignTimeLayout = multiColumnCombo1_DesignTimeLayout;
            this.multiColumnCombo1.DisplayMember = "Text";
            this.multiColumnCombo1.ImageList = this.SearchTypeImageList;
            this.multiColumnCombo1.Location = new System.Drawing.Point(63, 23);
            this.multiColumnCombo1.Name = "multiColumnCombo1";
            this.multiColumnCombo1.SelectedIndex = -1;
            this.multiColumnCombo1.SelectedItem = null;
            this.multiColumnCombo1.Size = new System.Drawing.Size(230, 18);
            this.multiColumnCombo1.TabIndex = 11;
            this.multiColumnCombo1.ValueMember = "Value";
            // 
            // SearchTypeImageList
            // 
            this.SearchTypeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SearchTypeImageList.ImageStream")));
            this.SearchTypeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.SearchTypeImageList.Images.SetKeyName(0, "Village.jpg");
            this.SearchTypeImageList.Images.SetKeyName(1, "Player.jpg");
            this.SearchTypeImageList.Images.SetKeyName(2, "Tribe.jpg");
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(349, 12);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(242, 46);
            this.CloseButton.TabIndex = 14;
            this.CloseButton.Text = "OK";
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // villagePlayerTribeSelector1
            // 
            this.villagePlayerTribeSelector1.AllowPlayer = true;
            this.villagePlayerTribeSelector1.AllowTribe = true;
            this.villagePlayerTribeSelector1.GameLocation = null;
            this.villagePlayerTribeSelector1.Location = new System.Drawing.Point(134, 194);
            this.villagePlayerTribeSelector1.Margin = new System.Windows.Forms.Padding(0);
            this.villagePlayerTribeSelector1.Name = "villagePlayerTribeSelector1";
            this.villagePlayerTribeSelector1.Size = new System.Drawing.Size(399, 29);
            this.villagePlayerTribeSelector1.TabIndex = 15;
            // 
            // villagePlayerTribeSelectorOld1
            // 
            this.villagePlayerTribeSelectorOld1.BackColor = System.Drawing.Color.Red;
            this.villagePlayerTribeSelectorOld1.GameLocation = null;
            this.villagePlayerTribeSelectorOld1.Location = new System.Drawing.Point(63, 60);
            this.villagePlayerTribeSelectorOld1.Name = "villagePlayerTribeSelectorOld1";
            this.villagePlayerTribeSelectorOld1.PlaceHolderText = "";
            this.villagePlayerTribeSelectorOld1.Size = new System.Drawing.Size(181, 20);
            this.villagePlayerTribeSelectorOld1.TabIndex = 16;
            // 
            // editBox1
            // 
            this.editBox1.Image = ((System.Drawing.Image)(resources.GetObject("editBox1.Image")));
            this.editBox1.Location = new System.Drawing.Point(134, 114);
            this.editBox1.Name = "editBox1";
            this.editBox1.Size = new System.Drawing.Size(100, 24);
            this.editBox1.TabIndex = 17;
            // 
            // TestForm
            // 
            this.AcceptButton = this.CloseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(616, 418);
            this.Controls.Add(this.editBox1);
            this.Controls.Add(this.villagePlayerTribeSelectorOld1);
            this.Controls.Add(this.villagePlayerTribeSelector1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.multiColumnCombo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TestForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TEST FORM";
            this.Load += new System.EventHandler(this.ActivePlayerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.multiColumnCombo1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIButton CloseButton;
        private Janus.Windows.GridEX.EditControls.MultiColumnCombo multiColumnCombo1;
        private System.Windows.Forms.ImageList SearchTypeImageList;
        private Controls.Finders.PlayerTribeDropdown villagePlayerTribeSelector1;
        private Controls.Finders.VillagePlayerTribeSelector villagePlayerTribeSelectorOld1;
        private Janus.Windows.GridEX.EditControls.EditBox editBox1;
    }
}