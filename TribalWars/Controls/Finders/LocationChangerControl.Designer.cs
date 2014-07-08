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
            this.PlayerTribeFinderTextBox = new VillagePlayerTribeFinderTextBox();
            this.ZoomControl = new Janus.Windows.GridEX.EditControls.IntegerUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // PlayerTribeFinderTextBox
            // 
            this.PlayerTribeFinderTextBox.AllowPlayer = true;
            this.PlayerTribeFinderTextBox.AllowTribe = true;
            this.PlayerTribeFinderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerTribeFinderTextBox.BackColor = System.Drawing.Color.Red;
            this.PlayerTribeFinderTextBox.ButtonStyle = EditButtonStyle.TextButton;
            this.PlayerTribeFinderTextBox.ButtonText = "» OK «";
            this.PlayerTribeFinderTextBox.GameLocation = null;
            this.PlayerTribeFinderTextBox.Location = new System.Drawing.Point(3, 3);
            this.PlayerTribeFinderTextBox.Name = "PlayerTribeFinderTextBox";
            this.PlayerTribeFinderTextBox.ShowButton = true;
            this.PlayerTribeFinderTextBox.Size = new System.Drawing.Size(174, 20);
            this.PlayerTribeFinderTextBox.TabIndex = 0;
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
            // LocationChangerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ZoomControl);
            this.Controls.Add(this.PlayerTribeFinderTextBox);
            this.Name = "LocationChangerControl";
            this.Size = new System.Drawing.Size(222, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.GridEX.EditControls.IntegerUpDown ZoomControl;
        private System.Windows.Forms.ToolTip toolTip1;
        public VillagePlayerTribeFinderTextBox PlayerTribeFinderTextBox;

    }
}
