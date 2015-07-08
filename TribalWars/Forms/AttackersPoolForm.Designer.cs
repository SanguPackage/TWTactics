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
			this.VillagesInAttackersPoolGroupbox = new Janus.Windows.EditControls.UIGroupBox();
			this.HelpLabel = new System.Windows.Forms.Label();
			this.ReloadButton = new System.Windows.Forms.Button();
			this.ClearButton = new System.Windows.Forms.Button();
			this.RemoveSelectedButton = new System.Windows.Forms.Button();
			this.villagesGridControl1 = new TribalWars.Controls.GridExs.VillagesGridExControl();
			((System.ComponentModel.ISupportInitialize)(this.VillagesInAttackersPoolGroupbox)).BeginInit();
			this.VillagesInAttackersPoolGroupbox.SuspendLayout();
			this.SuspendLayout();
			// 
			// VillagesInAttackersPoolGroupbox
			// 
			resources.ApplyResources(this.VillagesInAttackersPoolGroupbox, "VillagesInAttackersPoolGroupbox");
			this.VillagesInAttackersPoolGroupbox.Controls.Add(this.HelpLabel);
			this.VillagesInAttackersPoolGroupbox.Controls.Add(this.ReloadButton);
			this.VillagesInAttackersPoolGroupbox.Controls.Add(this.ClearButton);
			this.VillagesInAttackersPoolGroupbox.Controls.Add(this.RemoveSelectedButton);
			this.VillagesInAttackersPoolGroupbox.Controls.Add(this.villagesGridControl1);
			this.VillagesInAttackersPoolGroupbox.Name = "VillagesInAttackersPoolGroupbox";
			// 
			// HelpLabel
			// 
			resources.ApplyResources(this.HelpLabel, "HelpLabel");
			this.HelpLabel.Name = "HelpLabel";
			// 
			// ReloadButton
			// 
			resources.ApplyResources(this.ReloadButton, "ReloadButton");
			this.ReloadButton.Name = "ReloadButton";
			this.ReloadButton.UseVisualStyleBackColor = true;
			this.ReloadButton.Click += new System.EventHandler(this.ReloadButton_Click);
			// 
			// ClearButton
			// 
			resources.ApplyResources(this.ClearButton, "ClearButton");
			this.ClearButton.Name = "ClearButton";
			this.ClearButton.UseVisualStyleBackColor = true;
			this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
			// 
			// RemoveSelectedButton
			// 
			resources.ApplyResources(this.RemoveSelectedButton, "RemoveSelectedButton");
			this.RemoveSelectedButton.Name = "RemoveSelectedButton";
			this.RemoveSelectedButton.UseVisualStyleBackColor = true;
			this.RemoveSelectedButton.Click += new System.EventHandler(this.RemoveSelectedButton_Click);
			// 
			// villagesGridControl1
			// 
			resources.ApplyResources(this.villagesGridControl1, "villagesGridControl1");
			this.villagesGridControl1.Name = "villagesGridControl1";
			this.villagesGridControl1.ShowPlayer = false;
			// 
			// AttackersPoolForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.VillagesInAttackersPoolGroupbox);
			this.Name = "AttackersPoolForm";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.VillagesInAttackersPoolGroupbox)).EndInit();
			this.VillagesInAttackersPoolGroupbox.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private VillagesGridExControl villagesGridControl1;
        private Janus.Windows.EditControls.UIGroupBox VillagesInAttackersPoolGroupbox;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button RemoveSelectedButton;
        private System.Windows.Forms.Button ReloadButton;
        private System.Windows.Forms.Label HelpLabel;

    }
}