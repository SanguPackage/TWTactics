using TribalWars.Controls.GridExs;

namespace TribalWars.Forms
{
    partial class AttackersPoolForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttackersPoolForm));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.ClearButton = new System.Windows.Forms.Button();
            this.RemoveSelectedButton = new System.Windows.Forms.Button();
            this.ReloadButton = new System.Windows.Forms.Button();
            this.villagesGridControl1 = new TribalWars.Controls.GridExs.VillagesGridExControl();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.ReloadButton);
            this.uiGroupBox1.Controls.Add(this.ClearButton);
            this.uiGroupBox1.Controls.Add(this.RemoveSelectedButton);
            this.uiGroupBox1.Controls.Add(this.villagesGridControl1);
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(9, 12);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(441, 468);
            this.uiGroupBox1.TabIndex = 4;
            this.uiGroupBox1.Text = "Villages currently in your attackers pool";
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(299, 392);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(136, 70);
            this.ClearButton.TabIndex = 2;
            this.ClearButton.Text = "Clear attackers pool";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // RemoveSelectedButton
            // 
            this.RemoveSelectedButton.Location = new System.Drawing.Point(151, 392);
            this.RemoveSelectedButton.Name = "RemoveSelectedButton";
            this.RemoveSelectedButton.Size = new System.Drawing.Size(142, 70);
            this.RemoveSelectedButton.TabIndex = 1;
            this.RemoveSelectedButton.Text = "Remove selected villages from pool";
            this.RemoveSelectedButton.UseVisualStyleBackColor = true;
            this.RemoveSelectedButton.Click += new System.EventHandler(this.RemoveSelectedButton_Click);
            // 
            // ReloadButton
            // 
            this.ReloadButton.Location = new System.Drawing.Point(3, 392);
            this.ReloadButton.Name = "ReloadButton";
            this.ReloadButton.Size = new System.Drawing.Size(142, 70);
            this.ReloadButton.TabIndex = 3;
            this.ReloadButton.Text = "Reload attackers pool";
            this.ReloadButton.UseVisualStyleBackColor = true;
            this.ReloadButton.Click += new System.EventHandler(this.ReloadButton_Click);
            // 
            // villagesGridControl1
            // 
            this.villagesGridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.villagesGridControl1.Location = new System.Drawing.Point(3, 16);
            this.villagesGridControl1.Margin = new System.Windows.Forms.Padding(0);
            this.villagesGridControl1.Name = "villagesGridControl1";
            this.villagesGridControl1.ShowPlayer = false;
            this.villagesGridControl1.Size = new System.Drawing.Size(432, 373);
            this.villagesGridControl1.TabIndex = 0;
            // 
            // AttackersPoolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 492);
            this.Controls.Add(this.uiGroupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AttackersPoolForm";
            this.Text = "Manage your attackers pool";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private VillagesGridExControl villagesGridControl1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button RemoveSelectedButton;
        private System.Windows.Forms.Button ReloadButton;

    }
}