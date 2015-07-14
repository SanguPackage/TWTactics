using TribalWars.Controls.GridExs;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YourVillagesForm));
			this.villagesGridControl1 = new TribalWars.Controls.GridExs.VillagesGridExControl();
			this.SetPurposeOfYourVillagesGroupbox = new Janus.Windows.EditControls.UIGroupBox();
			this.Explain2Label = new System.Windows.Forms.Label();
			this.Explain1Label = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.SetPurposeOfYourVillagesGroupbox)).BeginInit();
			this.SetPurposeOfYourVillagesGroupbox.SuspendLayout();
			this.SuspendLayout();
			// 
			// villagesGridControl1
			// 
			resources.ApplyResources(this.villagesGridControl1, "villagesGridControl1");
			this.villagesGridControl1.Name = "villagesGridControl1";
			this.villagesGridControl1.ShowPlayer = false;
			// 
			// SetPurposeOfYourVillagesGroupbox
			// 
			resources.ApplyResources(this.SetPurposeOfYourVillagesGroupbox, "SetPurposeOfYourVillagesGroupbox");
			this.SetPurposeOfYourVillagesGroupbox.Controls.Add(this.Explain2Label);
			this.SetPurposeOfYourVillagesGroupbox.Controls.Add(this.villagesGridControl1);
			this.SetPurposeOfYourVillagesGroupbox.Controls.Add(this.Explain1Label);
			this.SetPurposeOfYourVillagesGroupbox.Name = "SetPurposeOfYourVillagesGroupbox";
			// 
			// Explain2Label
			// 
			resources.ApplyResources(this.Explain2Label, "Explain2Label");
			this.Explain2Label.Name = "Explain2Label";
			// 
			// Explain1Label
			// 
			resources.ApplyResources(this.Explain1Label, "Explain1Label");
			this.Explain1Label.Name = "Explain1Label";
			// 
			// YourVillagesForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SetPurposeOfYourVillagesGroupbox);
			this.Name = "YourVillagesForm";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.SetPurposeOfYourVillagesGroupbox)).EndInit();
			this.SetPurposeOfYourVillagesGroupbox.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private VillagesGridExControl villagesGridControl1;
        private Janus.Windows.EditControls.UIGroupBox SetPurposeOfYourVillagesGroupbox;
        private System.Windows.Forms.Label Explain2Label;
        private System.Windows.Forms.Label Explain1Label;

    }
}