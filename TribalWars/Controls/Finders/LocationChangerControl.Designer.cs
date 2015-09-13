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
            this.SelectorControl = new TribalWars.Controls.Finders.VillagePlayerTribeSelector();
            this.SuspendLayout();
            // 
            // ZoomControl
            // 
            this.ZoomControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomControl.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.ZoomControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.ZoomControl.Location = new System.Drawing.Point(94, 3);
            this.ZoomControl.Maximum = 50;
            this.ZoomControl.Minimum = 1;
            this.ZoomControl.Name = "ZoomControl";
            this.ZoomControl.Size = new System.Drawing.Size(42, 24);
            this.ZoomControl.TabIndex = 1;
			this.toolTip1.SetToolTip(this.ZoomControl, ControlsRes.LocationChangerControl_ZoomControl_Tooltip);
            this.ZoomControl.Value = 1;
            this.ZoomControl.ValueChanged += new System.EventHandler(this.ZoomControl_ValueChanged);
            // 
            // SelectorControl
            // 
            this.SelectorControl.AllowCoordinates = true;
            this.SelectorControl.AllowKingdom = true;
            this.SelectorControl.AllowPlayer = true;
            this.SelectorControl.AllowTribe = true;
            this.SelectorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectorControl.BackColor = System.Drawing.Color.White;
            this.SelectorControl.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.SelectorControl.ButtonImage = ((System.Drawing.Image)(resources.GetObject("SelectorControl.ButtonImage")));
            this.SelectorControl.ButtonImageSize = new System.Drawing.Size(18, 18);
            this.SelectorControl.ButtonStyle = Janus.Windows.GridEX.EditControls.EditButtonStyle.Image;
            this.SelectorControl.ForeColor = System.Drawing.SystemColors.GrayText;
            this.SelectorControl.GameLocation = null;
            this.SelectorControl.Location = new System.Drawing.Point(3, 3);
            this.SelectorControl.Name = "SelectorControl";
            this.SelectorControl.PlaceHolderText = "";
            this.SelectorControl.ShowButton = true;
            this.SelectorControl.Size = new System.Drawing.Size(85, 24);
            this.SelectorControl.TabIndex = 0;
            this.SelectorControl.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // LocationChangerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ZoomControl);
            this.Controls.Add(this.SelectorControl);
            this.Name = "LocationChangerControl";
            this.Size = new System.Drawing.Size(139, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.GridEX.EditControls.IntegerUpDown ZoomControl;
        public VillagePlayerTribeSelector SelectorControl;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}
