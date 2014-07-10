using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using TribalWars.Controls.Common;

namespace TribalWars.Controls.Finders
{
    partial class LocationChangerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationChangerControl));
            this.ZoomControl = new Janus.Windows.GridEX.EditControls.IntegerUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.TeleportButton = new Janus.Windows.EditControls.UIButton();
            this.VillagePlayerTribeSelector = new TribalWars.Controls.Finders.VillagePlayerTribeSelector();
            this.PlayerTribeSelectorOld = new TribalWars.Controls.Finders.VillagePlayerTribeSelectorOld();
            this.SuspendLayout();
            // 
            // ZoomControl
            // 
            this.ZoomControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomControl.Location = new System.Drawing.Point(183, 3);
            this.ZoomControl.Maximum = 50;
            this.ZoomControl.Minimum = 1;
            this.ZoomControl.Name = "ZoomControl";
            this.ZoomControl.Size = new System.Drawing.Size(36, 20);
            this.ZoomControl.TabIndex = 1;
            this.toolTip1.SetToolTip(this.ZoomControl, "Change map zoom level");
            this.ZoomControl.Value = 1;
            this.ZoomControl.ValueChanged += new System.EventHandler(this.ZoomControl_ValueChanged);
            // 
            // TeleportButton
            // 
            this.TeleportButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TeleportButton.Appearance = Janus.Windows.UI.Appearance.Flat;
            this.TeleportButton.Image = global::TribalWars.Properties.Resources.teleport;
            this.TeleportButton.Location = new System.Drawing.Point(157, 2);
            this.TeleportButton.Margin = new System.Windows.Forms.Padding(0);
            this.TeleportButton.Name = "TeleportButton";
            this.TeleportButton.Size = new System.Drawing.Size(25, 25);
            this.TeleportButton.TabIndex = 3;
            this.TeleportButton.Click += new System.EventHandler(this.TeleportButton_Click);
            // 
            // VillagePlayerTribeSelector
            // 
            this.VillagePlayerTribeSelector.AllowPlayer = true;
            this.VillagePlayerTribeSelector.AllowTribe = true;
            this.VillagePlayerTribeSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VillagePlayerTribeSelector.GameLocation = null;
            this.VillagePlayerTribeSelector.Location = new System.Drawing.Point(4, 3);
            this.VillagePlayerTribeSelector.Margin = new System.Windows.Forms.Padding(0);
            this.VillagePlayerTribeSelector.Name = "VillagePlayerTribeSelector";
            this.VillagePlayerTribeSelector.Size = new System.Drawing.Size(150, 25);
            this.VillagePlayerTribeSelector.TabIndex = 2;
            // 
            // PlayerTribeSelectorOld
            // 
            this.PlayerTribeSelectorOld.AllowPlayer = true;
            this.PlayerTribeSelectorOld.AllowTribe = true;
            this.PlayerTribeSelectorOld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerTribeSelectorOld.BackColor = System.Drawing.Color.Red;
            this.PlayerTribeSelectorOld.ButtonImage = ((System.Drawing.Image)(resources.GetObject("PlayerTribeSelectorOld.ButtonImage")));
            this.PlayerTribeSelectorOld.ButtonImageSize = new System.Drawing.Size(15, 15);
            this.PlayerTribeSelectorOld.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Image;
            this.PlayerTribeSelectorOld.ButtonText = "» OK «";
            this.PlayerTribeSelectorOld.GameLocation = null;
            this.PlayerTribeSelectorOld.Location = new System.Drawing.Point(3, 3);
            this.PlayerTribeSelectorOld.Name = "PlayerTribeSelectorOld";
            this.PlayerTribeSelectorOld.ShowButton = true;
            this.PlayerTribeSelectorOld.Size = new System.Drawing.Size(134, 23);
            this.PlayerTribeSelectorOld.TabIndex = 0;
            this.PlayerTribeSelectorOld.Visible = false;
            // 
            // LocationChangerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.TeleportButton);
            this.Controls.Add(this.ZoomControl);
            this.Controls.Add(this.VillagePlayerTribeSelector);
            this.Controls.Add(this.PlayerTribeSelectorOld);
            this.Name = "LocationChangerControl";
            this.Size = new System.Drawing.Size(222, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.GridEX.EditControls.IntegerUpDown ZoomControl;
        public VillagePlayerTribeSelectorOld PlayerTribeSelectorOld;
        private Janus.Windows.EditControls.UIButton TeleportButton;
        private System.Windows.Forms.ToolTip toolTip1;
        public VillagePlayerTribeSelector VillagePlayerTribeSelector;

    }
}
