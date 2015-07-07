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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VillageCoordinatesImporterForm));
			this.PasteVillageCoordinatesGroupbox = new Janus.Windows.EditControls.UIGroupBox();
			this.ClearTextFieldButton = new Janus.Windows.EditControls.UIButton();
			this.VillageCoordsInputBox = new Janus.Windows.GridEX.EditControls.EditBox();
			this.villagesGridExControl1 = new TribalWars.Controls.GridExs.VillagesGridExControl();
			this.ManipulateVillagesGroupbox = new Janus.Windows.EditControls.UIGroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SelectAPlayGroupbox = new Janus.Windows.EditControls.UIGroupBox();
			this.playerTribeDropdown1 = new TribalWars.Controls.Finders.PlayerTribeDropdown();
			((System.ComponentModel.ISupportInitialize)(this.PasteVillageCoordinatesGroupbox)).BeginInit();
			this.PasteVillageCoordinatesGroupbox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ManipulateVillagesGroupbox)).BeginInit();
			this.ManipulateVillagesGroupbox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SelectAPlayGroupbox)).BeginInit();
			this.SelectAPlayGroupbox.SuspendLayout();
			this.SuspendLayout();
			// 
			// PasteVillageCoordinatesGroupbox
			// 
			resources.ApplyResources(this.PasteVillageCoordinatesGroupbox, "PasteVillageCoordinatesGroupbox");
			this.PasteVillageCoordinatesGroupbox.Controls.Add(this.ClearTextFieldButton);
			this.PasteVillageCoordinatesGroupbox.Controls.Add(this.VillageCoordsInputBox);
			this.PasteVillageCoordinatesGroupbox.Name = "PasteVillageCoordinatesGroupbox";
			// 
			// ClearTextFieldButton
			// 
			resources.ApplyResources(this.ClearTextFieldButton, "ClearTextFieldButton");
			this.ClearTextFieldButton.Name = "ClearTextFieldButton";
			this.ClearTextFieldButton.Click += new System.EventHandler(this.ClearTextFieldButton_Click);
			// 
			// VillageCoordsInputBox
			// 
			resources.ApplyResources(this.VillageCoordsInputBox, "VillageCoordsInputBox");
			this.VillageCoordsInputBox.Multiline = true;
			this.VillageCoordsInputBox.Name = "VillageCoordsInputBox";
			this.VillageCoordsInputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.VillageCoordsInputBox.TextChanged += new System.EventHandler(this.VillageCoordsInputBox_TextChanged);
			// 
			// villagesGridExControl1
			// 
			resources.ApplyResources(this.villagesGridExControl1, "villagesGridExControl1");
			this.villagesGridExControl1.Name = "villagesGridExControl1";
			this.villagesGridExControl1.ShowPlayer = true;
			// 
			// ManipulateVillagesGroupbox
			// 
			resources.ApplyResources(this.ManipulateVillagesGroupbox, "ManipulateVillagesGroupbox");
			this.ManipulateVillagesGroupbox.Controls.Add(this.label1);
			this.ManipulateVillagesGroupbox.Controls.Add(this.villagesGridExControl1);
			this.ManipulateVillagesGroupbox.Name = "ManipulateVillagesGroupbox";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// SelectAPlayGroupbox
			// 
			resources.ApplyResources(this.SelectAPlayGroupbox, "SelectAPlayGroupbox");
			this.SelectAPlayGroupbox.Controls.Add(this.playerTribeDropdown1);
			this.SelectAPlayGroupbox.Name = "SelectAPlayGroupbox";
			// 
			// playerTribeDropdown1
			// 
			resources.ApplyResources(this.playerTribeDropdown1, "playerTribeDropdown1");
			this.playerTribeDropdown1.AllowTribe = false;
			this.playerTribeDropdown1.AutoOpenOnFocus = false;
			this.playerTribeDropdown1.Name = "playerTribeDropdown1";
			this.playerTribeDropdown1.PlayerSelected += new System.EventHandler<TribalWars.Worlds.Events.Impls.PlayerEventArgs>(this.playerTribeDropdown1_PlayerSelected);
			// 
			// VillageCoordinatesImporterForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SelectAPlayGroupbox);
			this.Controls.Add(this.ManipulateVillagesGroupbox);
			this.Controls.Add(this.PasteVillageCoordinatesGroupbox);
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "VillageCoordinatesImporterForm";
			this.TopMost = true;
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.VillageCoordinatesImporterForm_HelpButtonClicked);
			((System.ComponentModel.ISupportInitialize)(this.PasteVillageCoordinatesGroupbox)).EndInit();
			this.PasteVillageCoordinatesGroupbox.ResumeLayout(false);
			this.PasteVillageCoordinatesGroupbox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ManipulateVillagesGroupbox)).EndInit();
			this.ManipulateVillagesGroupbox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SelectAPlayGroupbox)).EndInit();
			this.SelectAPlayGroupbox.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox PasteVillageCoordinatesGroupbox;
        private Janus.Windows.GridEX.EditControls.EditBox VillageCoordsInputBox;
        private Controls.GridExs.VillagesGridExControl villagesGridExControl1;
        private Janus.Windows.EditControls.UIGroupBox ManipulateVillagesGroupbox;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIGroupBox SelectAPlayGroupbox;
        private Controls.Finders.PlayerTribeDropdown playerTribeDropdown1;
        private Janus.Windows.EditControls.UIButton ClearTextFieldButton;
    }
}