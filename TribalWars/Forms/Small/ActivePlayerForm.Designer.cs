namespace TribalWars.Forms.Small
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
			this.OkButton = new Janus.Windows.EditControls.UIButton();
			this.SelectYourselfGroupbox = new Janus.Windows.EditControls.UIGroupBox();
			this.You = new TribalWars.Controls.Finders.PlayerTribeDropdown();
			this.SelectYourVillagesMarkerGroupbox = new Janus.Windows.EditControls.UIGroupBox();
			this.YourMarker = new TribalWars.Maps.Markers.MarkerSettingsControl();
			this.SelectYourTribeMarkersGroupbox = new Janus.Windows.EditControls.UIGroupBox();
			this.YourTribeMarker = new TribalWars.Maps.Markers.MarkerSettingsControl();
			this.WhySetMeGroupbox = new Janus.Windows.EditControls.UIGroupBox();
			this.WhySetMeLabel = new System.Windows.Forms.Label();
			this.HowDoMarkersWorkGroupbox = new Janus.Windows.EditControls.UIGroupBox();
			this.HowToMarkersWorkLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.SelectYourselfGroupbox)).BeginInit();
			this.SelectYourselfGroupbox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SelectYourVillagesMarkerGroupbox)).BeginInit();
			this.SelectYourVillagesMarkerGroupbox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SelectYourTribeMarkersGroupbox)).BeginInit();
			this.SelectYourTribeMarkersGroupbox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.WhySetMeGroupbox)).BeginInit();
			this.WhySetMeGroupbox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.HowDoMarkersWorkGroupbox)).BeginInit();
			this.HowDoMarkersWorkGroupbox.SuspendLayout();
			this.SuspendLayout();
			// 
			// OkButton
			// 
			resources.ApplyResources(this.OkButton, "OkButton");
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.OkButton.Image = ((System.Drawing.Image)(resources.GetObject("OkButton.Image")));
			this.OkButton.Name = "OkButton";
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// SelectYourselfGroupbox
			// 
			resources.ApplyResources(this.SelectYourselfGroupbox, "SelectYourselfGroupbox");
			this.SelectYourselfGroupbox.Controls.Add(this.You);
			this.SelectYourselfGroupbox.Name = "SelectYourselfGroupbox";
			// 
			// You
			// 
			resources.ApplyResources(this.You, "You");
			this.You.AllowTribe = false;
			this.You.AutoOpenOnFocus = false;
			this.You.BackColor = System.Drawing.Color.Transparent;
			this.You.Name = "You";
			// 
			// SelectYourVillagesMarkerGroupbox
			// 
			resources.ApplyResources(this.SelectYourVillagesMarkerGroupbox, "SelectYourVillagesMarkerGroupbox");
			this.SelectYourVillagesMarkerGroupbox.Controls.Add(this.YourMarker);
			this.SelectYourVillagesMarkerGroupbox.Name = "SelectYourVillagesMarkerGroupbox";
			// 
			// YourMarker
			// 
			resources.ApplyResources(this.YourMarker, "YourMarker");
			this.YourMarker.AllowBarbarianViews = false;
			this.YourMarker.AutoUpdateMarkers = false;
			this.YourMarker.BackColor = System.Drawing.Color.Transparent;
			this.YourMarker.CanDeactivate = false;
			this.YourMarker.DefaultExtraMarkerColor = System.Drawing.Color.Transparent;
			this.YourMarker.DefaultMarkerColor = System.Drawing.Color.Black;
			this.YourMarker.Name = "YourMarker";
			// 
			// SelectYourTribeMarkersGroupbox
			// 
			resources.ApplyResources(this.SelectYourTribeMarkersGroupbox, "SelectYourTribeMarkersGroupbox");
			this.SelectYourTribeMarkersGroupbox.Controls.Add(this.YourTribeMarker);
			this.SelectYourTribeMarkersGroupbox.Name = "SelectYourTribeMarkersGroupbox";
			// 
			// YourTribeMarker
			// 
			resources.ApplyResources(this.YourTribeMarker, "YourTribeMarker");
			this.YourTribeMarker.AllowBarbarianViews = false;
			this.YourTribeMarker.AutoUpdateMarkers = false;
			this.YourTribeMarker.BackColor = System.Drawing.Color.Transparent;
			this.YourTribeMarker.CanDeactivate = false;
			this.YourTribeMarker.DefaultExtraMarkerColor = System.Drawing.Color.Transparent;
			this.YourTribeMarker.DefaultMarkerColor = System.Drawing.Color.Black;
			this.YourTribeMarker.Name = "YourTribeMarker";
			// 
			// WhySetMeGroupbox
			// 
			resources.ApplyResources(this.WhySetMeGroupbox, "WhySetMeGroupbox");
			this.WhySetMeGroupbox.Controls.Add(this.WhySetMeLabel);
			this.WhySetMeGroupbox.Name = "WhySetMeGroupbox";
			// 
			// WhySetMeLabel
			// 
			resources.ApplyResources(this.WhySetMeLabel, "WhySetMeLabel");
			this.WhySetMeLabel.Name = "WhySetMeLabel";
			// 
			// HowDoMarkersWorkGroupbox
			// 
			resources.ApplyResources(this.HowDoMarkersWorkGroupbox, "HowDoMarkersWorkGroupbox");
			this.HowDoMarkersWorkGroupbox.Controls.Add(this.HowToMarkersWorkLabel);
			this.HowDoMarkersWorkGroupbox.Name = "HowDoMarkersWorkGroupbox";
			// 
			// HowToMarkersWorkLabel
			// 
			resources.ApplyResources(this.HowToMarkersWorkLabel, "HowToMarkersWorkLabel");
			this.HowToMarkersWorkLabel.Name = "HowToMarkersWorkLabel";
			// 
			// ActivePlayerForm
			// 
			this.AcceptButton = this.OkButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.HowDoMarkersWorkGroupbox);
			this.Controls.Add(this.WhySetMeGroupbox);
			this.Controls.Add(this.SelectYourTribeMarkersGroupbox);
			this.Controls.Add(this.SelectYourVillagesMarkerGroupbox);
			this.Controls.Add(this.SelectYourselfGroupbox);
			this.Controls.Add(this.OkButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ActivePlayerForm";
			this.ShowIcon = false;
			this.Load += new System.EventHandler(this.ActivePlayerForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.SelectYourselfGroupbox)).EndInit();
			this.SelectYourselfGroupbox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SelectYourVillagesMarkerGroupbox)).EndInit();
			this.SelectYourVillagesMarkerGroupbox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SelectYourTribeMarkersGroupbox)).EndInit();
			this.SelectYourTribeMarkersGroupbox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.WhySetMeGroupbox)).EndInit();
			this.WhySetMeGroupbox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.HowDoMarkersWorkGroupbox)).EndInit();
			this.HowDoMarkersWorkGroupbox.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton OkButton;
        private Janus.Windows.EditControls.UIGroupBox SelectYourselfGroupbox;
        private Controls.Finders.PlayerTribeDropdown You;
        private Janus.Windows.EditControls.UIGroupBox SelectYourVillagesMarkerGroupbox;
        private Maps.Markers.MarkerSettingsControl YourMarker;
        private Janus.Windows.EditControls.UIGroupBox SelectYourTribeMarkersGroupbox;
        private Maps.Markers.MarkerSettingsControl YourTribeMarker;
        private Janus.Windows.EditControls.UIGroupBox WhySetMeGroupbox;
        private System.Windows.Forms.Label WhySetMeLabel;
        private Janus.Windows.EditControls.UIGroupBox HowDoMarkersWorkGroupbox;
        private System.Windows.Forms.Label HowToMarkersWorkLabel;
    }
}