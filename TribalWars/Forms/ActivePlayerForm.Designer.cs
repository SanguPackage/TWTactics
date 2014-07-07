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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivePlayerForm));
            this.gbTimeZome = new System.Windows.Forms.GroupBox();
            this.You = new TribalWars.Controls.Common.VillagePlayerTribeFinderTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CloseButton = new Janus.Windows.EditControls.UIButton();
            this.gbTimeZome.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTimeZome
            // 
            this.gbTimeZome.Controls.Add(this.You);
            this.gbTimeZome.Controls.Add(this.label3);
            this.gbTimeZome.Location = new System.Drawing.Point(12, 12);
            this.gbTimeZome.Name = "gbTimeZome";
            this.gbTimeZome.Size = new System.Drawing.Size(242, 234);
            this.gbTimeZome.TabIndex = 13;
            this.gbTimeZome.TabStop = false;
            // 
            // You
            // 
            this.You.AllowPlayer = true;
            this.You.AllowVillage = false;
            this.You.BackColor = System.Drawing.Color.Red;
            this.You.GameLocation = null;
            this.You.Location = new System.Drawing.Point(19, 20);
            this.You.Name = "You";
            this.You.Size = new System.Drawing.Size(207, 20);
            this.You.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 140);
            this.label3.TabIndex = 8;
            this.label3.Text = "\r\n";
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseButton.Image")));
            this.CloseButton.Location = new System.Drawing.Point(12, 249);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(242, 46);
            this.CloseButton.TabIndex = 14;
            this.CloseButton.Text = "OK";
            // 
            // ActivePlayerForm
            // 
            this.AcceptButton = this.CloseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(264, 304);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.gbTimeZome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActivePlayerForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Your Player Name";
            this.gbTimeZome.ResumeLayout(false);
            this.gbTimeZome.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTimeZome;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton CloseButton;
        private Controls.Common.VillagePlayerTribeFinderTextBox You;
    }
}