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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttackPlanFromControl));
			this._Village = new System.Windows.Forms.Label();
			this.DateRequired = new System.Windows.Forms.Label();
			this.DateLeft = new System.Windows.Forms.Label();
			this.DateNow = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.DateSend = new System.Windows.Forms.Label();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.UnitBox = new TribalWars.Controls.Common.ImageCombobox();
			this.Close = new System.Windows.Forms.LinkLabel();
			this.Coords = new TribalWars.Controls.Finders.VillagePlayerTribeSelector();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// _Village
			// 
			resources.ApplyResources(this._Village, "_Village");
			this._Village.Name = "_Village";
			this.toolTip1.SetToolTip(this._Village, resources.GetString("_Village.ToolTip"));
			this._Village.DoubleClick += new System.EventHandler(this.AttackPlanFromControl_DoubleClick);
			this._Village.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AttackPlanFromControl_MouseClick);
			// 
			// DateRequired
			// 
			resources.ApplyResources(this.DateRequired, "DateRequired");
			this.DateRequired.Name = "DateRequired";
			this.toolTip1.SetToolTip(this.DateRequired, resources.GetString("DateRequired.ToolTip"));
			this.DateRequired.DoubleClick += new System.EventHandler(this.AttackPlanFromControl_DoubleClick);
			this.DateRequired.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AttackPlanFromControl_MouseClick);
			// 
			// DateLeft
			// 
			resources.ApplyResources(this.DateLeft, "DateLeft");
			this.DateLeft.Name = "DateLeft";
			this.toolTip1.SetToolTip(this.DateLeft, resources.GetString("DateLeft.ToolTip"));
			this.DateLeft.DoubleClick += new System.EventHandler(this.AttackPlanFromControl_DoubleClick);
			this.DateLeft.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AttackPlanFromControl_MouseClick);
			// 
			// DateNow
			// 
			resources.ApplyResources(this.DateNow, "DateNow");
			this.DateNow.Name = "DateNow";
			this.toolTip1.SetToolTip(this.DateNow, resources.GetString("DateNow.ToolTip"));
			this.DateNow.DoubleClick += new System.EventHandler(this.AttackPlanFromControl_DoubleClick);
			this.DateNow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AttackPlanFromControl_MouseClick);
			// 
			// DateSend
			// 
			resources.ApplyResources(this.DateSend, "DateSend");
			this.DateSend.Name = "DateSend";
			this.toolTip1.SetToolTip(this.DateSend, resources.GetString("DateSend.ToolTip"));
			this.DateSend.DoubleClick += new System.EventHandler(this.AttackPlanFromControl_DoubleClick);
			this.DateSend.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AttackPlanFromControl_MouseClick);
			// 
			// pictureBox3
			// 
			resources.ApplyResources(this.pictureBox3, "pictureBox3");
			this.pictureBox3.Image = global::TribalWars.Properties.Resources.place;
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.TabStop = false;
			this.toolTip1.SetToolTip(this.pictureBox3, resources.GetString("pictureBox3.ToolTip"));
			// 
			// pictureBox2
			// 
			resources.ApplyResources(this.pictureBox2, "pictureBox2");
			this.pictureBox2.Image = global::TribalWars.Properties.Resources.time1;
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.TabStop = false;
			this.toolTip1.SetToolTip(this.pictureBox2, resources.GetString("pictureBox2.ToolTip"));
			// 
			// pictureBox1
			// 
			resources.ApplyResources(this.pictureBox1, "pictureBox1");
			this.pictureBox1.Image = global::TribalWars.Properties.Resources.speed;
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.TabStop = false;
			this.toolTip1.SetToolTip(this.pictureBox1, resources.GetString("pictureBox1.ToolTip"));
			// 
			// UnitBox
			// 
			resources.ApplyResources(this.UnitBox, "UnitBox");
			this.UnitBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
			this.UnitBox.DisplayMember = "ItemData";
			this.UnitBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.UnitBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.UnitBox.FormattingEnabled = true;
			this.UnitBox.Name = "UnitBox";
			this.toolTip1.SetToolTip(this.UnitBox, resources.GetString("UnitBox.ToolTip"));
			this.UnitBox.SelectedIndexChanged += new System.EventHandler(this.Unit_SelectedIndexChanged);
			// 
			// Close
			// 
			resources.ApplyResources(this.Close, "Close");
			this.Close.ActiveLinkColor = System.Drawing.Color.Black;
			this.Close.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Close.DisabledLinkColor = System.Drawing.Color.Black;
			this.Close.LinkColor = System.Drawing.Color.Black;
			this.Close.Name = "Close";
			this.Close.TabStop = true;
			this.toolTip1.SetToolTip(this.Close, resources.GetString("Close.ToolTip"));
			this.Close.VisitedLinkColor = System.Drawing.Color.Black;
			this.Close.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Close_LinkClicked);
			// 
			// Coords
			// 
			resources.ApplyResources(this.Coords, "Coords");
			this.Coords.BackColor = System.Drawing.Color.White;
			this.Coords.DisplayVillagePurposeImage = true;
			this.Coords.GameLocation = null;
			this.Coords.Name = "Coords";
			this.Coords.PlaceHolderText = "";
			this.toolTip1.SetToolTip(this.Coords, resources.GetString("Coords.ToolTip"));
			this.Coords.TextChanged += new System.EventHandler(this.Coords_TextChanged);
			// 
			// AttackPlanFromControl
			// 
			resources.ApplyResources(this, "$this");
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
			this.Name = "AttackPlanFromControl";
			this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
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
