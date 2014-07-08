namespace TribalWars.Forms
{
    partial class ActivePlayerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivePlayerForm));
            this.multiColumnCombo1 = new Janus.Windows.GridEX.EditControls.MultiColumnCombo();
            this.SearchTypeImageList = new System.Windows.Forms.ImageList(this.components);
            this.CloseButton = new Janus.Windows.EditControls.UIButton();
            this.uiColorPicker1 = new Janus.Windows.EditControls.UIColorPicker();
            this.uiColorButton1 = new Janus.Windows.EditControls.UIColorButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.multiColumnCombo1)).BeginInit();
            this.SuspendLayout();
            // 
            // multiColumnCombo1
            // 
            multiColumnCombo1_DesignTimeLayout.LayoutString = resources.GetString("multiColumnCombo1_DesignTimeLayout.LayoutString");
            this.multiColumnCombo1.DesignTimeLayout = multiColumnCombo1_DesignTimeLayout;
            this.multiColumnCombo1.DisplayMember = "Text";
            this.multiColumnCombo1.ImageList = this.SearchTypeImageList;
            this.multiColumnCombo1.Location = new System.Drawing.Point(26, 12);
            this.multiColumnCombo1.Name = "multiColumnCombo1";
            this.multiColumnCombo1.SelectedIndex = -1;
            this.multiColumnCombo1.SelectedItem = null;
            this.multiColumnCombo1.Size = new System.Drawing.Size(230, 20);
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
            // uiColorPicker1
            // 
            this.uiColorPicker1.Location = new System.Drawing.Point(72, 148);
            this.uiColorPicker1.Name = "uiColorPicker1";
            this.uiColorPicker1.Size = new System.Drawing.Size(152, 149);
            this.uiColorPicker1.TabIndex = 12;
            this.uiColorPicker1.Text = "uiColorPicker1";
            // 
            // uiColorButton1
            // 
            // 
            // 
            // 
            this.uiColorButton1.ColorPicker.BorderStyle = Janus.Windows.UI.BorderStyle.None;
            this.uiColorButton1.ColorPicker.Location = new System.Drawing.Point(0, 0);
            this.uiColorButton1.ColorPicker.Name = "";
            this.uiColorButton1.ColorPicker.Size = new System.Drawing.Size(100, 100);
            this.uiColorButton1.ColorPicker.TabIndex = 0;
            this.uiColorButton1.Location = new System.Drawing.Point(318, 184);
            this.uiColorButton1.Name = "uiColorButton1";
            this.uiColorButton1.Size = new System.Drawing.Size(75, 23);
            this.uiColorButton1.TabIndex = 15;
            this.uiColorButton1.Text = "uiColorButton1";
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            // 
            // ActivePlayerForm
            // 
            this.AcceptButton = this.CloseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(616, 418);
            this.Controls.Add(this.uiColorButton1);
            this.Controls.Add(this.uiColorPicker1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.multiColumnCombo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ActivePlayerForm";
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
        private Janus.Windows.EditControls.UIColorPicker uiColorPicker1;
        private Janus.Windows.EditControls.UIColorButton uiColorButton1;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}