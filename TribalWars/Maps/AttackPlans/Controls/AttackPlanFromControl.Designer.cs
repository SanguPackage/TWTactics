using TribalWars.Controls.Common;
using TribalWars.Controls.Finders;

namespace TribalWars.Maps.AttackPlans.Controls
{
    partial class AttackPlanFromControl
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
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Close = new System.Windows.Forms.LinkLabel();
            this.Coords = new TribalWars.Controls.Finders.VillagePlayerTribeSelector();
            this.UnitBox = new TribalWars.Controls.Common.ImageCombobox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _Village
            // 
            this._Village.AutoSize = true;
            this._Village.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Village.Location = new System.Drawing.Point(81, 6);
            this._Village.Name = "_Village";
            this._Village.Size = new System.Drawing.Size(75, 13);
            this._Village.TabIndex = 0;
            this._Village.Text = "Villagename";
            this._Village.DoubleClick += new System.EventHandler(this.AttackPlanFromControl_DoubleClick);
            this._Village.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AttackPlanFromControl_MouseClick);
            // 
            // DateRequired
            // 
            this.DateRequired.Location = new System.Drawing.Point(94, 23);
            this.DateRequired.Name = "DateRequired";
            this.DateRequired.Size = new System.Drawing.Size(70, 18);
            this.DateRequired.TabIndex = 3;
            this.DateRequired.Text = "01:33:45";
            this.toolTip1.SetToolTip(this.DateRequired, "Travel time");
            this.DateRequired.DoubleClick += new System.EventHandler(this.AttackPlanFromControl_DoubleClick);
            this.DateRequired.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AttackPlanFromControl_MouseClick);
            // 
            // DateLeft
            // 
            this.DateLeft.Location = new System.Drawing.Point(95, 41);
            this.DateLeft.Name = "DateLeft";
            this.DateLeft.Size = new System.Drawing.Size(70, 18);
            this.DateLeft.TabIndex = 3;
            this.DateLeft.Text = "1.22:10:15";
            this.toolTip1.SetToolTip(this.DateLeft, "Time left (before sending to arrive at specified date)");
            this.DateLeft.DoubleClick += new System.EventHandler(this.AttackPlanFromControl_DoubleClick);
            this.DateLeft.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AttackPlanFromControl_MouseClick);
            // 
            // DateNow
            // 
            this.DateNow.Location = new System.Drawing.Point(185, 23);
            this.DateNow.Name = "DateNow";
            this.DateNow.Size = new System.Drawing.Size(129, 18);
            this.DateNow.TabIndex = 3;
            this.DateNow.Text = "tomorrow at 24:55:45";
            this.DateNow.DoubleClick += new System.EventHandler(this.AttackPlanFromControl_DoubleClick);
            this.DateNow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AttackPlanFromControl_MouseClick);
            // 
            // DateSend
            // 
            this.DateSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateSend.Location = new System.Drawing.Point(185, 41);
            this.DateSend.Name = "DateSend";
            this.DateSend.Size = new System.Drawing.Size(128, 18);
            this.DateSend.TabIndex = 3;
            this.DateSend.Text = "tomorrow at 19:00:00";
            this.toolTip1.SetToolTip(this.DateSend, "Time to send (to arrive at specified date)");
            this.DateSend.DoubleClick += new System.EventHandler(this.AttackPlanFromControl_DoubleClick);
            this.DateSend.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AttackPlanFromControl_MouseClick);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::TribalWars.Properties.Resources.place;
            this.pictureBox3.Location = new System.Drawing.Point(169, 40);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(17, 17);
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox3, "Time to send (to arrive at specified date)");
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::TribalWars.Properties.Resources.time1;
            this.pictureBox2.Location = new System.Drawing.Point(78, 39);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(18, 18);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox2, "Time left (before sending to arrive at specified date)");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TribalWars.Properties.Resources.speed;
            this.pictureBox1.Location = new System.Drawing.Point(79, 20);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(17, 17);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Travel time");
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
            // Coords
            // 
            this.Coords.BackColor = System.Drawing.Color.White;
            this.Coords.DisplayVillagePurposeImage = true;
            this.Coords.GameLocation = null;
            this.Coords.Location = new System.Drawing.Point(2, 4);
            this.Coords.Name = "Coords";
            this.Coords.PlaceHolderText = "";
            this.Coords.Size = new System.Drawing.Size(75, 20);
            this.Coords.TabIndex = 5;
            this.Coords.TextChanged += new System.EventHandler(this.Coords_TextChanged);
            // 
            // UnitBox
            // 
            this.UnitBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.UnitBox.DisplayMember = "ItemData";
            this.UnitBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.UnitBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UnitBox.FormattingEnabled = true;
            this.UnitBox.ItemHeight = 20;
            this.UnitBox.Location = new System.Drawing.Point(2, 30);
            this.UnitBox.MaxDropDownItems = 9;
            this.UnitBox.Name = "UnitBox";
            this.UnitBox.Size = new System.Drawing.Size(75, 26);
            this.UnitBox.TabIndex = 2;
            this.UnitBox.SelectedIndexChanged += new System.EventHandler(this.Unit_SelectedIndexChanged);
            // 
            // AttackPlanFromControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Coords);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.DateSend);
            this.Controls.Add(this.DateNow);
            this.Controls.Add(this.DateLeft);
            this.Controls.Add(this.DateRequired);
            this.Controls.Add(this.UnitBox);
            this.Controls.Add(this._Village);
            this.Margin = new System.Windows.Forms.Padding(1, 0, 0, 1);
            this.Name = "AttackPlanFromControl";
            this.Size = new System.Drawing.Size(311, 60);
            this.DoubleClick += new System.EventHandler(this.AttackPlanFromControl_DoubleClick);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AttackPlanFromControl_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private VillagePlayerTribeSelector Coords;
        private System.Windows.Forms.LinkLabel Close;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}
