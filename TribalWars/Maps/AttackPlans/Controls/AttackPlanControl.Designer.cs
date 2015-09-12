using TribalWars.Controls.Finders;
using TribalWars.Controls.TimeConverter;

namespace TribalWars.Maps.AttackPlans.Controls
{
    partial class AttackPlanControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttackPlanControl));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.Coords = new TribalWars.Controls.Finders.VillagePlayerTribeSelector();
			this.Close = new System.Windows.Forms.LinkLabel();
			this._Player = new System.Windows.Forms.Label();
			this._Village = new System.Windows.Forms.Label();
			this._Tribe = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.CommentsToggle1 = new System.Windows.Forms.Panel();
			this.AttackCountLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Date = new TribalWars.Controls.TimeConverter.TimeConverterControl();
			this.Comments = new System.Windows.Forms.TextBox();
			this.ToggleComments = new System.Windows.Forms.Button();
			this.DistanceContainer = new System.Windows.Forms.FlowLayoutPanel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.CommentsToggle1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.DistanceContainer, 0, 2);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.toolTip1.SetToolTip(this.tableLayoutPanel1, resources.GetString("tableLayoutPanel1.ToolTip"));
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Controls.Add(this.Coords);
			this.panel1.Controls.Add(this.Close);
			this.panel1.Controls.Add(this._Player);
			this.panel1.Controls.Add(this._Village);
			this.panel1.Controls.Add(this._Tribe);
			this.panel1.Name = "panel1";
			this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
			// 
			// Coords
			// 
			resources.ApplyResources(this.Coords, "Coords");
			this.Coords.BackColor = System.Drawing.Color.White;
			this.Coords.DisplayVillagePurposeImage = true;
			this.Coords.GameLocation = null;
			this.Coords.Name = "Coords";
			this.Coords.PlaceHolderText = "";
			this.Coords.ShowImage = false;
			this.toolTip1.SetToolTip(this.Coords, resources.GetString("Coords.ToolTip"));
			this.Coords.VillageSelected += new System.EventHandler<TribalWars.Worlds.Events.Impls.VillageEventArgs>(this.Coords_VillageSelected);
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
			// _Player
			// 
			resources.ApplyResources(this._Player, "_Player");
			this._Player.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this._Player.Name = "_Player";
			this.toolTip1.SetToolTip(this._Player, resources.GetString("_Player.ToolTip"));
			this._Player.DoubleClick += new System.EventHandler(this.Player_DoubleClick);
			this._Player.MouseClick += new System.Windows.Forms.MouseEventHandler(this._Player_MouseClick);
			// 
			// _Village
			// 
			resources.ApplyResources(this._Village, "_Village");
			this._Village.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this._Village.Name = "_Village";
			this.toolTip1.SetToolTip(this._Village, resources.GetString("_Village.ToolTip"));
			this._Village.DoubleClick += new System.EventHandler(this._Village_DoubleClick);
			this._Village.MouseClick += new System.Windows.Forms.MouseEventHandler(this._Village_MouseClick);
			// 
			// _Tribe
			// 
			resources.ApplyResources(this._Tribe, "_Tribe");
			this._Tribe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this._Tribe.Name = "_Tribe";
			this.toolTip1.SetToolTip(this._Tribe, resources.GetString("_Tribe.ToolTip"));
			this._Tribe.DoubleClick += new System.EventHandler(this.Tribe_DoubleClick);
			this._Tribe.MouseClick += new System.Windows.Forms.MouseEventHandler(this._Tribe_MouseClick);
			// 
			// panel3
			// 
			resources.ApplyResources(this.panel3, "panel3");
			this.panel3.Controls.Add(this.CommentsToggle1);
			this.panel3.Controls.Add(this.Comments);
			this.panel3.Controls.Add(this.ToggleComments);
			this.panel3.Name = "panel3";
			this.toolTip1.SetToolTip(this.panel3, resources.GetString("panel3.ToolTip"));
			// 
			// CommentsToggle1
			// 
			resources.ApplyResources(this.CommentsToggle1, "CommentsToggle1");
			this.CommentsToggle1.Controls.Add(this.AttackCountLabel);
			this.CommentsToggle1.Controls.Add(this.label1);
			this.CommentsToggle1.Controls.Add(this.Date);
			this.CommentsToggle1.Name = "CommentsToggle1";
			this.toolTip1.SetToolTip(this.CommentsToggle1, resources.GetString("CommentsToggle1.ToolTip"));
			// 
			// AttackCountLabel
			// 
			resources.ApplyResources(this.AttackCountLabel, "AttackCountLabel");
			this.AttackCountLabel.Name = "AttackCountLabel";
			this.toolTip1.SetToolTip(this.AttackCountLabel, resources.GetString("AttackCountLabel.ToolTip"));
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
			// 
			// Date
			// 
			resources.ApplyResources(this.Date, "Date");
			this.Date.BackColor = System.Drawing.Color.Transparent;
			this.Date.CustomFormat = "MMM, dd yyyy HH:mm:ss";
			this.Date.Name = "Date";
			this.toolTip1.SetToolTip(this.Date, resources.GetString("Date.ToolTip"));
			this.Date.Value = new System.DateTime(2008, 4, 10, 0, 26, 44, 906);
			this.Date.DateSelected += new System.EventHandler<TribalWars.Controls.TimeConverter.DateEventArgs>(this.Date_DateSelected);
			// 
			// Comments
			// 
			this.Comments.AcceptsReturn = true;
			this.Comments.AcceptsTab = true;
			resources.ApplyResources(this.Comments, "Comments");
			this.Comments.Name = "Comments";
			this.toolTip1.SetToolTip(this.Comments, resources.GetString("Comments.ToolTip"));
			this.Comments.TextChanged += new System.EventHandler(this.Comments_TextChanged);
			// 
			// ToggleComments
			// 
			resources.ApplyResources(this.ToggleComments, "ToggleComments");
			this.ToggleComments.Image = global::TribalWars.Properties.Resources.pencil1;
			this.ToggleComments.Name = "ToggleComments";
			this.toolTip1.SetToolTip(this.ToggleComments, resources.GetString("ToggleComments.ToolTip"));
			this.ToggleComments.UseVisualStyleBackColor = true;
			this.ToggleComments.Click += new System.EventHandler(this.ToggleComments_Click);
			// 
			// DistanceContainer
			// 
			resources.ApplyResources(this.DistanceContainer, "DistanceContainer");
			this.DistanceContainer.Name = "DistanceContainer";
			this.toolTip1.SetToolTip(this.DistanceContainer, resources.GetString("DistanceContainer.ToolTip"));
			// 
			// AttackPlanControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "AttackPlanControl";
			this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.CommentsToggle1.ResumeLayout(false);
			this.CommentsToggle1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label _Village;
        private System.Windows.Forms.Label _Player;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.LinkLabel Close;
        private TimeConverterControl Date;
        private VillagePlayerTribeSelector Coords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel DistanceContainer;
        private System.Windows.Forms.Label AttackCountLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label _Tribe;
        private System.Windows.Forms.Button ToggleComments;
        private System.Windows.Forms.Panel CommentsToggle1;
        private System.Windows.Forms.TextBox Comments;
    }
}
