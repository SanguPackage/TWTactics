using TribalWars.Controls.Common;
using TribalWars.Controls.Finders;

namespace TribalWars.Controls.AttackPlan
{
    partial class MapDistanceVillageControl
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
            this._Village = new System.Windows.Forms.Label();
            this.DateRequired = new System.Windows.Forms.Label();
            this.DateLeft = new System.Windows.Forms.Label();
            this.DateNow = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.DateSend = new System.Windows.Forms.Label();
            this.UnitBox = new ImageCombobox();
            this.Coords = new VillagePlayerTribeFinderTextBox();
            this.Close = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // _Village
            // 
            this._Village.AutoSize = true;
            this._Village.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Village.Location = new System.Drawing.Point(61, 7);
            this._Village.Name = "_Village";
            this._Village.Size = new System.Drawing.Size(75, 13);
            this._Village.TabIndex = 0;
            this._Village.Text = "Villagename";
            this._Village.DoubleClick += new System.EventHandler(this.Village_DoubleClick);
            // 
            // DateRequired
            // 
            this.DateRequired.Location = new System.Drawing.Point(62, 25);
            this.DateRequired.Name = "DateRequired";
            this.DateRequired.Size = new System.Drawing.Size(75, 18);
            this.DateRequired.TabIndex = 3;
            this.DateRequired.Text = "01:33:45";
            this.toolTip1.SetToolTip(this.DateRequired, "Travel time");
            // 
            // DateLeft
            // 
            this.DateLeft.Location = new System.Drawing.Point(62, 43);
            this.DateLeft.Name = "DateLeft";
            this.DateLeft.Size = new System.Drawing.Size(78, 18);
            this.DateLeft.TabIndex = 3;
            this.DateLeft.Text = "1.22:10:15";
            this.toolTip1.SetToolTip(this.DateLeft, "Time left before sending to arrive at specified date");
            // 
            // DateNow
            // 
            this.DateNow.Location = new System.Drawing.Point(167, 25);
            this.DateNow.Name = "DateNow";
            this.DateNow.Size = new System.Drawing.Size(141, 18);
            this.DateNow.TabIndex = 3;
            this.DateNow.Text = "tomorrow at 24:55:45";
            this.toolTip1.SetToolTip(this.DateNow, "Arrival time when sent NOW");
            // 
            // DateSend
            // 
            this.DateSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateSend.Location = new System.Drawing.Point(167, 43);
            this.DateSend.Name = "DateSend";
            this.DateSend.Size = new System.Drawing.Size(141, 18);
            this.DateSend.TabIndex = 3;
            this.DateSend.Text = "tomorrow at 19:00:00";
            this.toolTip1.SetToolTip(this.DateSend, "Time to send to arrive at specified date");
            // 
            // UnitBox
            // 
            this.UnitBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.UnitBox.DisplayMember = "ItemData";
            this.UnitBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.UnitBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UnitBox.FormattingEnabled = true;
            this.UnitBox.ItemHeight = 20;
            this.UnitBox.Location = new System.Drawing.Point(5, 26);
            this.UnitBox.Name = "UnitBox";
            this.UnitBox.Size = new System.Drawing.Size(51, 26);
            this.UnitBox.TabIndex = 2;
            this.UnitBox.SelectedIndexChanged += new System.EventHandler(this.Unit_SelectedIndexChanged);
            // 
            // Coords
            // 
            this.Coords.BackColor = System.Drawing.Color.White;
            this.Coords.Location = new System.Drawing.Point(5, 3);
            this.Coords.Name = "Coords";
            this.Coords.Size = new System.Drawing.Size(50, 20);
            this.Coords.TabIndex = 5;
            this.Coords.TextChanged += new System.EventHandler(this.Coords_TextChanged);
            // 
            // Close
            // 
            this.Close.ActiveLinkColor = System.Drawing.Color.Black;
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Close.AutoSize = true;
            this.Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Close.DisabledLinkColor = System.Drawing.Color.Black;
            this.Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close.LinkColor = System.Drawing.Color.Black;
            this.Close.Location = new System.Drawing.Point(296, 3);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(15, 16);
            this.Close.TabIndex = 4;
            this.Close.TabStop = true;
            this.Close.Text = "x";
            this.Close.VisitedLinkColor = System.Drawing.Color.Black;
            this.Close.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Close_LinkClicked);
            // 
            // MapDistanceVillageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.Coords);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.DateSend);
            this.Controls.Add(this.DateNow);
            this.Controls.Add(this.DateLeft);
            this.Controls.Add(this.DateRequired);
            this.Controls.Add(this.UnitBox);
            this.Controls.Add(this._Village);
            this.Margin = new System.Windows.Forms.Padding(1, 0, 0, 1);
            this.Name = "MapDistanceVillageControl";
            this.Size = new System.Drawing.Size(311, 60);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _Village;
        private ImageCombobox UnitBox;
        private System.Windows.Forms.Label DateRequired;
        private System.Windows.Forms.Label DateLeft;
        private System.Windows.Forms.Label DateNow;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label DateSend;
        private VillagePlayerTribeFinderTextBox Coords;
        private System.Windows.Forms.LinkLabel Close;
    }
}
