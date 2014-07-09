using TribalWars.Controls.Common;
using TribalWars.Controls.XPTables;
using TribalWars.Worlds.Events.Impls;

namespace TribalWars.Controls.AccordeonDetails
{
    partial class DetailsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailsControl));
            this.QuickFinderLayout = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.Comments = new System.Windows.Forms.TextBox();
            this.DetailsGrid = new System.Windows.Forms.PropertyGrid();
            this.Table = new TribalWars.Controls.XPTables.TableWrapperControl();
            this.SpecialVillage = new TribalWars.Controls.AccordeonDetails.VillageControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SelectedVillage = new TribalWars.Controls.Common.ToolStripVillageTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.UndoButton = new System.Windows.Forms.ToolStripButton();
            this.RedoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.DetailsView = new System.Windows.Forms.ToolStripButton();
            this.CommentsView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewVillageDetails = new System.Windows.Forms.ToolStripButton();
            this.ViewPlayerDetails = new System.Windows.Forms.ToolStripButton();
            this.ViewTribeDetails = new System.Windows.Forms.ToolStripButton();
            this.ContextStripPanel = new System.Windows.Forms.Panel();
            this.MarkPlayerOrTribe = new TribalWars.Maps.Markers.MarkerSettingsControl();
            this.ContextStrip = new System.Windows.Forms.ToolStrip();
            this.AttackFlag = new System.Windows.Forms.ToolStripButton();
            this.DefenseFlag = new System.Windows.Forms.ToolStripButton();
            this.NobleFlag = new System.Windows.Forms.ToolStripButton();
            this.ScoutFlag = new System.Windows.Forms.ToolStripButton();
            this.FarmFlag = new System.Windows.Forms.ToolStripButton();
            this.VillageSeperator = new System.Windows.Forms.ToolStripSeparator();
            this.VillageCurrentSituation = new System.Windows.Forms.ToolStripButton();
            this.QuickFinderLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.ContextStripPanel.SuspendLayout();
            this.ContextStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // QuickFinderLayout
            // 
            this.QuickFinderLayout.ColumnCount = 1;
            this.QuickFinderLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.QuickFinderLayout.Controls.Add(this.splitContainer2, 0, 2);
            this.QuickFinderLayout.Controls.Add(this.toolStrip1, 0, 0);
            this.QuickFinderLayout.Controls.Add(this.ContextStripPanel, 0, 1);
            this.QuickFinderLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuickFinderLayout.Location = new System.Drawing.Point(0, 0);
            this.QuickFinderLayout.Margin = new System.Windows.Forms.Padding(0);
            this.QuickFinderLayout.Name = "QuickFinderLayout";
            this.QuickFinderLayout.RowCount = 3;
            this.QuickFinderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.QuickFinderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.QuickFinderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.QuickFinderLayout.Size = new System.Drawing.Size(267, 297);
            this.QuickFinderLayout.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 50);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.Comments);
            this.splitContainer2.Panel1.Controls.Add(this.DetailsGrid);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.Table);
            this.splitContainer2.Panel2.Controls.Add(this.SpecialVillage);
            this.splitContainer2.Size = new System.Drawing.Size(267, 247);
            this.splitContainer2.SplitterDistance = 122;
            this.splitContainer2.TabIndex = 0;
            // 
            // Comments
            // 
            this.Comments.AcceptsReturn = true;
            this.Comments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Comments.Location = new System.Drawing.Point(0, 0);
            this.Comments.Multiline = true;
            this.Comments.Name = "Comments";
            this.Comments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Comments.Size = new System.Drawing.Size(267, 122);
            this.Comments.TabIndex = 1;
            this.Comments.Visible = false;
            this.Comments.TextChanged += new System.EventHandler(this.Comments_TextChanged);
            // 
            // DetailsGrid
            // 
            this.DetailsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsGrid.HelpVisible = false;
            this.DetailsGrid.Location = new System.Drawing.Point(0, 0);
            this.DetailsGrid.Margin = new System.Windows.Forms.Padding(0);
            this.DetailsGrid.Name = "DetailsGrid";
            this.DetailsGrid.Size = new System.Drawing.Size(267, 122);
            this.DetailsGrid.TabIndex = 0;
            this.DetailsGrid.ToolbarVisible = false;
            // 
            // Table
            // 
            this.Table.AutoSelectSingleRow = false;
            this.Table.BackColor = System.Drawing.Color.Transparent;
            this.Table.DisplayType = TribalWars.Controls.XPTables.TableWrapperControl.ColumnDisplayTypeEnum.Custom;
            this.Table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Table.Location = new System.Drawing.Point(0, 0);
            this.Table.Margin = new System.Windows.Forms.Padding(0);
            this.Table.Name = "Table";
            this.Table.RowSelectionAction = TribalWars.Controls.XPTables.TableWrapperControl.RowSelectionActionEnum.RaiseSelectEvent;
            this.Table.Size = new System.Drawing.Size(267, 121);
            this.Table.TabIndex = 0;
            this.Table.VisiblePlayerFields = ((TribalWars.Controls.XPTables.PlayerFields)(((((TribalWars.Controls.XPTables.PlayerFields.Name | TribalWars.Controls.XPTables.PlayerFields.Points) 
            | TribalWars.Controls.XPTables.PlayerFields.PointsDifference) 
            | TribalWars.Controls.XPTables.PlayerFields.Villages) 
            | TribalWars.Controls.XPTables.PlayerFields.VillagesDifference)));
            this.Table.VisibleReportFields = ((TribalWars.Controls.XPTables.ReportFields)((((((TribalWars.Controls.XPTables.ReportFields.Type | TribalWars.Controls.XPTables.ReportFields.Status) 
            | TribalWars.Controls.XPTables.ReportFields.Village) 
            | TribalWars.Controls.XPTables.ReportFields.Player) 
            | TribalWars.Controls.XPTables.ReportFields.Date) 
            | TribalWars.Controls.XPTables.ReportFields.Flag)));
            this.Table.VisibleTribeFields = ((TribalWars.Controls.XPTables.TribeFields)((((((TribalWars.Controls.XPTables.TribeFields.Tag | TribalWars.Controls.XPTables.TribeFields.Name) 
            | TribalWars.Controls.XPTables.TribeFields.Players) 
            | TribalWars.Controls.XPTables.TribeFields.Points) 
            | TribalWars.Controls.XPTables.TribeFields.Villages) 
            | TribalWars.Controls.XPTables.TribeFields.Rank)));
            this.Table.VisibleVillageFields = ((TribalWars.Controls.XPTables.VillageFields)((((((TribalWars.Controls.XPTables.VillageFields.Type | TribalWars.Controls.XPTables.VillageFields.Coordinates) 
            | TribalWars.Controls.XPTables.VillageFields.Name) 
            | TribalWars.Controls.XPTables.VillageFields.Points) 
            | TribalWars.Controls.XPTables.VillageFields.PointsDifference) 
            | TribalWars.Controls.XPTables.VillageFields.HasReport)));
            this.Table.RowSelected += new System.EventHandler<System.EventArgs>(this.Table_RowSelected);
            // 
            // SpecialVillage
            // 
            this.SpecialVillage.BackColor = System.Drawing.Color.Transparent;
            this.SpecialVillage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SpecialVillage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpecialVillage.Location = new System.Drawing.Point(0, 0);
            this.SpecialVillage.Margin = new System.Windows.Forms.Padding(0);
            this.SpecialVillage.Name = "SpecialVillage";
            this.SpecialVillage.Size = new System.Drawing.Size(267, 121);
            this.SpecialVillage.TabIndex = 0;
            this.SpecialVillage.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectedVillage,
            this.toolStripSeparator2,
            this.UndoButton,
            this.RedoButton,
            this.toolStripSeparator3,
            this.DetailsView,
            this.CommentsView,
            this.toolStripSeparator1,
            this.ViewVillageDetails,
            this.ViewPlayerDetails,
            this.ViewTribeDetails});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(267, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SelectedVillage
            // 
            this.SelectedVillage.AllowPlayer = true;
            this.SelectedVillage.AllowTribe = true;
            this.SelectedVillage.AutoSize = false;
            this.SelectedVillage.BackColor = System.Drawing.Color.White;
            this.SelectedVillage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SelectedVillage.ForeColor = System.Drawing.SystemColors.WindowText;
            this.SelectedVillage.Name = "SelectedVillage";
            this.SelectedVillage.Player = null;
            this.SelectedVillage.Size = new System.Drawing.Size(80, 23);
            this.SelectedVillage.Tribe = null;
            this.SelectedVillage.Village = null;
            this.SelectedVillage.VillageSelected += new System.EventHandler<TribalWars.Worlds.Events.Impls.VillageEventArgs>(this.SelectedVillage_VillageSelected);
            this.SelectedVillage.PlayerSelected += new System.EventHandler<TribalWars.Worlds.Events.Impls.PlayerEventArgs>(this.SelectedVillage_PlayerSelected);
            this.SelectedVillage.TribeSelected += new System.EventHandler<TribalWars.Worlds.Events.Impls.TribeEventArgs>(this.SelectedVillage_TribeSelected);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // UndoButton
            // 
            this.UndoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoButton.Enabled = false;
            this.UndoButton.Image = ((System.Drawing.Image)(resources.GetObject("UndoButton.Image")));
            this.UndoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(23, 22);
            this.UndoButton.ToolTipText = "Undo";
            this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // RedoButton
            // 
            this.RedoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RedoButton.Enabled = false;
            this.RedoButton.Image = ((System.Drawing.Image)(resources.GetObject("RedoButton.Image")));
            this.RedoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RedoButton.Name = "RedoButton";
            this.RedoButton.Size = new System.Drawing.Size(23, 22);
            this.RedoButton.ToolTipText = "Redo";
            this.RedoButton.Click += new System.EventHandler(this.RedoButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // DetailsView
            // 
            this.DetailsView.Checked = true;
            this.DetailsView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DetailsView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DetailsView.Image = ((System.Drawing.Image)(resources.GetObject("DetailsView.Image")));
            this.DetailsView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DetailsView.Name = "DetailsView";
            this.DetailsView.Size = new System.Drawing.Size(23, 22);
            this.DetailsView.Text = "toolStripButton2";
            this.DetailsView.ToolTipText = "General details";
            this.DetailsView.Click += new System.EventHandler(this.DetailsView_Click);
            // 
            // CommentsView
            // 
            this.CommentsView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CommentsView.Image = ((System.Drawing.Image)(resources.GetObject("CommentsView.Image")));
            this.CommentsView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CommentsView.Name = "CommentsView";
            this.CommentsView.Size = new System.Drawing.Size(23, 22);
            this.CommentsView.Text = "toolStripButton1";
            this.CommentsView.ToolTipText = "Custom comments";
            this.CommentsView.Click += new System.EventHandler(this.CommentsView_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ViewVillageDetails
            // 
            this.ViewVillageDetails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewVillageDetails.Enabled = false;
            this.ViewVillageDetails.Image = global::TribalWars.Properties.Resources.Village;
            this.ViewVillageDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewVillageDetails.Name = "ViewVillageDetails";
            this.ViewVillageDetails.Size = new System.Drawing.Size(23, 22);
            this.ViewVillageDetails.ToolTipText = "View village details";
            this.ViewVillageDetails.Click += new System.EventHandler(this.ViewVillageDetails_Click);
            // 
            // ViewPlayerDetails
            // 
            this.ViewPlayerDetails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewPlayerDetails.Enabled = false;
            this.ViewPlayerDetails.Image = global::TribalWars.Properties.Resources.Player;
            this.ViewPlayerDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewPlayerDetails.Name = "ViewPlayerDetails";
            this.ViewPlayerDetails.Size = new System.Drawing.Size(23, 22);
            this.ViewPlayerDetails.ToolTipText = "View player details";
            this.ViewPlayerDetails.Click += new System.EventHandler(this.ViewPlayerDetails_Click);
            // 
            // ViewTribeDetails
            // 
            this.ViewTribeDetails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewTribeDetails.Enabled = false;
            this.ViewTribeDetails.Image = global::TribalWars.Properties.Resources.Tribe;
            this.ViewTribeDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewTribeDetails.Name = "ViewTribeDetails";
            this.ViewTribeDetails.Size = new System.Drawing.Size(23, 20);
            this.ViewTribeDetails.ToolTipText = "View tribe details";
            this.ViewTribeDetails.Click += new System.EventHandler(this.ViewTribeDetails_Click);
            // 
            // ContextStripPanel
            // 
            this.ContextStripPanel.Controls.Add(this.MarkPlayerOrTribe);
            this.ContextStripPanel.Controls.Add(this.ContextStrip);
            this.ContextStripPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContextStripPanel.Location = new System.Drawing.Point(0, 25);
            this.ContextStripPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ContextStripPanel.Name = "ContextStripPanel";
            this.ContextStripPanel.Size = new System.Drawing.Size(267, 25);
            this.ContextStripPanel.TabIndex = 8;
            // 
            // MarkPlayerOrTribe
            // 
            this.MarkPlayerOrTribe.BackColor = System.Drawing.Color.White;
            this.MarkPlayerOrTribe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MarkPlayerOrTribe.Location = new System.Drawing.Point(0, 0);
            this.MarkPlayerOrTribe.Margin = new System.Windows.Forms.Padding(0);
            this.MarkPlayerOrTribe.Name = "MarkPlayerOrTribe";
            this.MarkPlayerOrTribe.Size = new System.Drawing.Size(267, 25);
            this.MarkPlayerOrTribe.TabIndex = 1;
            this.MarkPlayerOrTribe.Visible = false;
            // 
            // ContextStrip
            // 
            this.ContextStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContextStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AttackFlag,
            this.DefenseFlag,
            this.NobleFlag,
            this.ScoutFlag,
            this.FarmFlag,
            this.VillageSeperator,
            this.VillageCurrentSituation});
            this.ContextStrip.Location = new System.Drawing.Point(0, 0);
            this.ContextStrip.Name = "ContextStrip";
            this.ContextStrip.Size = new System.Drawing.Size(267, 25);
            this.ContextStrip.TabIndex = 2;
            this.ContextStrip.Text = "ContextStrip";
            // 
            // AttackFlag
            // 
            this.AttackFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AttackFlag.Image = ((System.Drawing.Image)(resources.GetObject("AttackFlag.Image")));
            this.AttackFlag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AttackFlag.Name = "AttackFlag";
            this.AttackFlag.Size = new System.Drawing.Size(23, 22);
            this.AttackFlag.ToolTipText = "Mark this village as offensive";
            this.AttackFlag.Click += new System.EventHandler(this.AttackFlag_Click);
            // 
            // DefenseFlag
            // 
            this.DefenseFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DefenseFlag.Image = global::TribalWars.Properties.Resources.Defense;
            this.DefenseFlag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DefenseFlag.Name = "DefenseFlag";
            this.DefenseFlag.Size = new System.Drawing.Size(23, 22);
            this.DefenseFlag.ToolTipText = "Mark this village as defensive";
            this.DefenseFlag.Click += new System.EventHandler(this.DefenseFlag_Click);
            // 
            // NobleFlag
            // 
            this.NobleFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NobleFlag.Image = global::TribalWars.Properties.Resources.nobleman;
            this.NobleFlag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NobleFlag.Name = "NobleFlag";
            this.NobleFlag.Size = new System.Drawing.Size(23, 22);
            this.NobleFlag.ToolTipText = "Mark this village for nobles";
            this.NobleFlag.Click += new System.EventHandler(this.NobleFlag_Click);
            // 
            // ScoutFlag
            // 
            this.ScoutFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ScoutFlag.Image = global::TribalWars.Properties.Resources.scout;
            this.ScoutFlag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ScoutFlag.Name = "ScoutFlag";
            this.ScoutFlag.Size = new System.Drawing.Size(23, 22);
            this.ScoutFlag.ToolTipText = "Mark this village for scouts";
            this.ScoutFlag.Click += new System.EventHandler(this.ScoutFlag_Click);
            // 
            // FarmFlag
            // 
            this.FarmFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FarmFlag.Image = global::TribalWars.Properties.Resources.farm;
            this.FarmFlag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FarmFlag.Name = "FarmFlag";
            this.FarmFlag.Size = new System.Drawing.Size(23, 22);
            this.FarmFlag.ToolTipText = "Mark this village as a farm";
            this.FarmFlag.Click += new System.EventHandler(this.FarmFlag_Click);
            // 
            // VillageSeperator
            // 
            this.VillageSeperator.Name = "VillageSeperator";
            this.VillageSeperator.Size = new System.Drawing.Size(6, 25);
            this.VillageSeperator.Visible = false;
            // 
            // VillageCurrentSituation
            // 
            this.VillageCurrentSituation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.VillageCurrentSituation.Image = ((System.Drawing.Image)(resources.GetObject("VillageCurrentSituation.Image")));
            this.VillageCurrentSituation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.VillageCurrentSituation.Name = "VillageCurrentSituation";
            this.VillageCurrentSituation.Size = new System.Drawing.Size(23, 22);
            this.VillageCurrentSituation.ToolTipText = "View the current estimated status";
            this.VillageCurrentSituation.Visible = false;
            this.VillageCurrentSituation.Click += new System.EventHandler(this.VillageCurrentSituation_Click);
            // 
            // DetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.QuickFinderLayout);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DetailsControl";
            this.Size = new System.Drawing.Size(267, 297);
            this.Load += new System.EventHandler(this.DetailsControl_Load);
            this.QuickFinderLayout.ResumeLayout(false);
            this.QuickFinderLayout.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ContextStripPanel.ResumeLayout(false);
            this.ContextStripPanel.PerformLayout();
            this.ContextStrip.ResumeLayout(false);
            this.ContextStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel QuickFinderLayout;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ViewPlayerDetails;
        private System.Windows.Forms.ToolStripButton ViewTribeDetails;
        private System.Windows.Forms.ToolStripButton ViewVillageDetails;
        private ToolStripVillageTextBox SelectedVillage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private TableWrapperControl Table;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid DetailsGrid;
        private VillageControl SpecialVillage;
        private System.Windows.Forms.Panel ContextStripPanel;
        private System.Windows.Forms.ToolStrip ContextStrip;
        private System.Windows.Forms.ToolStripButton DefenseFlag;
        private System.Windows.Forms.ToolStripButton AttackFlag;
        private System.Windows.Forms.ToolStripButton ScoutFlag;
        private System.Windows.Forms.ToolStripButton NobleFlag;
        private System.Windows.Forms.ToolStripButton FarmFlag;
        private System.Windows.Forms.ToolStripButton UndoButton;
        private System.Windows.Forms.ToolStripButton RedoButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton CommentsView;
        private System.Windows.Forms.ToolStripButton DetailsView;
        private System.Windows.Forms.TextBox Comments;
        private System.Windows.Forms.ToolStripSeparator VillageSeperator;
        private System.Windows.Forms.ToolStripButton VillageCurrentSituation;
        private Maps.Markers.MarkerSettingsControl MarkPlayerOrTribe;
    }
}
