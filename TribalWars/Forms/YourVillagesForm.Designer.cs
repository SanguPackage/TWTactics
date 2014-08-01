namespace TribalWars.Forms
{
    partial class YourVillagesForm
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
            this.villagesGridControl1 = new TribalWars.Controls.VillagesGridControl();
            this.SuspendLayout();
            // 
            // villagesGridControl1
            // 
            this.villagesGridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.villagesGridControl1.Location = new System.Drawing.Point(9, 9);
            this.villagesGridControl1.Margin = new System.Windows.Forms.Padding(0);
            this.villagesGridControl1.Name = "villagesGridControl1";
            this.villagesGridControl1.Size = new System.Drawing.Size(441, 521);
            this.villagesGridControl1.TabIndex = 0;
            // 
            // YourVillagesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 539);
            this.Controls.Add(this.villagesGridControl1);
            this.Name = "YourVillagesForm";
            this.Text = "Manage your villages";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.VillagesGridControl villagesGridControl1;

    }
}