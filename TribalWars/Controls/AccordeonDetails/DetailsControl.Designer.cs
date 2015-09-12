using TribalWars.Controls.Common;
using TribalWars.Controls.Common.ToolStripControlHostWrappers;
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
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.DetailsGrid = new System.Windows.Forms.PropertyGrid();
			this.CommentsPanel = new System.Windows.Forms.Panel();
			this.CommentsLabel = new System.Windows.Forms.Label();
			this.Comments = new System.Windows.Forms.TextBox();
			this.Table = new TribalWars.Controls.XPTables.TableWrapperControl();
			this.QuickFinderLayout = new System.Windows.Forms.TableLayoutPanel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.SelectedVillage = new TribalWars.Controls.Common.ToolStripControlHostWrappers.ToolStripVillageTextBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.UndoButton = new System.Windows.Forms.ToolStripButton();
			this.RedoButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.ViewVillageDetails = new System.Windows.Forms.ToolStripButton();
			this.ViewPlayerDetails = new System.Windows.Forms.ToolStripButton();
			this.ViewTribeDetails = new System.Windows.Forms.ToolStripButton();
			this.ContextStripPanel = new System.Windows.Forms.Panel();
			this.ContextStrip = new System.Windows.Forms.ToolStrip();
			this.AttackFlag = new System.Windows.Forms.ToolStripButton();
			this.CatapultFlag = new System.Windows.Forms.ToolStripButton();
			this.DefenseFlag = new System.Windows.Forms.ToolStripButton();
			this.NobleFlag = new System.Windows.Forms.ToolStripButton();
			this.ScoutFlag = new System.Windows.Forms.ToolStripButton();
			this.FarmFlag = new System.Windows.Forms.ToolStripButton();
			this.VillageSeperator = new System.Windows.Forms.ToolStripSeparator();
			this.VillageCurrentSituation = new System.Windows.Forms.ToolStripButton();
			this.MarkPlayerOrTribe = new TribalWars.Maps.Markers.MarkerSettingsControl();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.CommentsPanel.SuspendLayout();
			this.QuickFinderLayout.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.ContextStripPanel.SuspendLayout();
			this.ContextStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer2
			// 
			resources.ApplyResources(this.splitContainer2, "splitContainer2");
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			resources.ApplyResources(this.splitContainer2.Panel1, "splitContainer2.Panel1");
			this.splitContainer2.Panel1.Controls.Add(this.DetailsGrid);
			// 
			// splitContainer2.Panel2
			// 
			resources.ApplyResources(this.splitContainer2.Panel2, "splitContainer2.Panel2");
			this.splitContainer2.Panel2.Controls.Add(this.CommentsPanel);
			this.splitContainer2.Panel2.Controls.Add(this.Table);
			// 
			// DetailsGrid
			// 
			resources.ApplyResources(this.DetailsGrid, "DetailsGrid");
			this.DetailsGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.DetailsGrid.Name = "DetailsGrid";
			this.DetailsGrid.ToolbarVisible = false;
			// 
			// CommentsPanel
			// 
			resources.ApplyResources(this.CommentsPanel, "CommentsPanel");
			this.CommentsPanel.Controls.Add(this.CommentsLabel);
			this.CommentsPanel.Controls.Add(this.Comments);
			this.CommentsPanel.Name = "CommentsPanel";
			// 
			// CommentsLabel
			// 
			resources.ApplyResources(this.CommentsLabel, "CommentsLabel");
			this.CommentsLabel.Name = "CommentsLabel";
			// 
			// Comments
			// 
			this.Comments.AcceptsReturn = true;
			resources.ApplyResources(this.Comments, "Comments");
			this.Comments.Name = "Comments";
			// 
			// Table
			// 
			resources.ApplyResources(this.Table, "Table");
			this.Table.AutoSelectSingleRow = false;
			this.Table.BackColor = System.Drawing.Color.Transparent;
			this.Table.DisplayType = TribalWars.Controls.XPTables.TableWrapperControl.ColumnDisplayTypeEnum.Custom;
			this.Table.Name = "Table";
			this.Table.RowSelectionAction = TribalWars.Controls.XPTables.TableWrapperControl.RowSelectionActionEnum.RaiseSelectEvent;
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
			// 
			// QuickFinderLayout
			// 
			resources.ApplyResources(this.QuickFinderLayout, "QuickFinderLayout");
			this.QuickFinderLayout.Controls.Add(this.splitContainer2, 0, 2);
			this.QuickFinderLayout.Controls.Add(this.toolStrip1, 0, 0);
			this.QuickFinderLayout.Controls.Add(this.ContextStripPanel, 0, 1);
			this.QuickFinderLayout.Name = "QuickFinderLayout";
			// 
			// toolStrip1
			// 
			resources.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectedVillage,
            this.toolStripSeparator2,
            this.UndoButton,
            this.RedoButton,
            this.toolStripSeparator3,
            this.ViewVillageDetails,
            this.ViewPlayerDetails,
            this.ViewTribeDetails});
			this.toolStrip1.Name = "toolStrip1";
			// 
			// SelectedVillage
			// 
			resources.ApplyResources(this.SelectedVillage, "SelectedVillage");
			this.SelectedVillage.AllowPlayer = true;
			this.SelectedVillage.AllowTribe = true;
			this.SelectedVillage.BackColor = System.Drawing.Color.White;
			this.SelectedVillage.ForeColor = System.Drawing.SystemColors.WindowText;
			this.SelectedVillage.Name = "SelectedVillage";
			this.SelectedVillage.Player = null;
			this.SelectedVillage.Tribe = null;
			this.SelectedVillage.Village = null;
			this.SelectedVillage.VillageSelected += new System.EventHandler<TribalWars.Worlds.Events.Impls.VillageEventArgs>(this.SelectedVillage_VillageSelected);
			this.SelectedVillage.PlayerSelected += new System.EventHandler<TribalWars.Worlds.Events.Impls.PlayerEventArgs>(this.SelectedVillage_PlayerSelected);
			this.SelectedVillage.TribeSelected += new System.EventHandler<TribalWars.Worlds.Events.Impls.TribeEventArgs>(this.SelectedVillage_TribeSelected);
			// 
			// toolStripSeparator2
			// 
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			// 
			// UndoButton
			// 
			resources.ApplyResources(this.UndoButton, "UndoButton");
			this.UndoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.UndoButton.Name = "UndoButton";
			this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
			// 
			// RedoButton
			// 
			resources.ApplyResources(this.RedoButton, "RedoButton");
			this.RedoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.RedoButton.Name = "RedoButton";
			this.RedoButton.Click += new System.EventHandler(this.RedoButton_Click);
			// 
			// toolStripSeparator3
			// 
			resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			// 
			// ViewVillageDetails
			// 
			resources.ApplyResources(this.ViewVillageDetails, "ViewVillageDetails");
			this.ViewVillageDetails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ViewVillageDetails.Image = global::TribalWars.Properties.Resources.Village;
			this.ViewVillageDetails.Name = "ViewVillageDetails";
			this.ViewVillageDetails.Click += new System.EventHandler(this.ViewVillageDetails_Click);
			// 
			// ViewPlayerDetails
			// 
			resources.ApplyResources(this.ViewPlayerDetails, "ViewPlayerDetails");
			this.ViewPlayerDetails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ViewPlayerDetails.Image = global::TribalWars.Properties.Resources.Player;
			this.ViewPlayerDetails.Name = "ViewPlayerDetails";
			this.ViewPlayerDetails.Click += new System.EventHandler(this.ViewPlayerDetails_Click);
			// 
			// ViewTribeDetails
			// 
			resources.ApplyResources(this.ViewTribeDetails, "ViewTribeDetails");
			this.ViewTribeDetails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ViewTribeDetails.Image = global::TribalWars.Properties.Resources.Tribe;
			this.ViewTribeDetails.Name = "ViewTribeDetails";
			this.ViewTribeDetails.Click += new System.EventHandler(this.ViewTribeDetails_Click);
			// 
			// ContextStripPanel
			// 
			resources.ApplyResources(this.ContextStripPanel, "ContextStripPanel");
			this.ContextStripPanel.Controls.Add(this.ContextStrip);
			this.ContextStripPanel.Controls.Add(this.MarkPlayerOrTribe);
			this.ContextStripPanel.Name = "ContextStripPanel";
			// 
			// ContextStrip
			// 
			resources.ApplyResources(this.ContextStrip, "ContextStrip");
			this.ContextStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AttackFlag,
            this.CatapultFlag,
            this.DefenseFlag,
            this.NobleFlag,
            this.ScoutFlag,
            this.FarmFlag,
            this.VillageSeperator,
            this.VillageCurrentSituation});
			this.ContextStrip.Name = "ContextStrip";
			// 
			// AttackFlag
			// 
			resources.ApplyResources(this.AttackFlag, "AttackFlag");
			this.AttackFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AttackFlag.Name = "AttackFlag";
			this.AttackFlag.Click += new System.EventHandler(this.AttackFlag_Click);
			// 
			// CatapultFlag
			// 
			resources.ApplyResources(this.CatapultFlag, "CatapultFlag");
			this.CatapultFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CatapultFlag.Image = global::TribalWars.Properties.Resources.catapult;
			this.CatapultFlag.Name = "CatapultFlag";
			this.CatapultFlag.Click += new System.EventHandler(this.CatapultFlag_Click);
			// 
			// DefenseFlag
			// 
			resources.ApplyResources(this.DefenseFlag, "DefenseFlag");
			this.DefenseFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DefenseFlag.Image = global::TribalWars.Properties.Resources.Defense;
			this.DefenseFlag.Name = "DefenseFlag";
			this.DefenseFlag.Click += new System.EventHandler(this.DefenseFlag_Click);
			// 
			// NobleFlag
			// 
			resources.ApplyResources(this.NobleFlag, "NobleFlag");
			this.NobleFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.NobleFlag.Image = global::TribalWars.Properties.Resources.nobleman;
			this.NobleFlag.Name = "NobleFlag";
			this.NobleFlag.Click += new System.EventHandler(this.NobleFlag_Click);
			// 
			// ScoutFlag
			// 
			resources.ApplyResources(this.ScoutFlag, "ScoutFlag");
			this.ScoutFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ScoutFlag.Image = global::TribalWars.Properties.Resources.scout;
			this.ScoutFlag.Name = "ScoutFlag";
			this.ScoutFlag.Click += new System.EventHandler(this.ScoutFlag_Click);
			// 
			// FarmFlag
			// 
			resources.ApplyResources(this.FarmFlag, "FarmFlag");
			this.FarmFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.FarmFlag.Image = global::TribalWars.Properties.Resources.farm;
			this.FarmFlag.Name = "FarmFlag";
			this.FarmFlag.Click += new System.EventHandler(this.FarmFlag_Click);
			// 
			// VillageSeperator
			// 
			resources.ApplyResources(this.VillageSeperator, "VillageSeperator");
			this.VillageSeperator.Name = "VillageSeperator";
			// 
			// VillageCurrentSituation
			// 
			resources.ApplyResources(this.VillageCurrentSituation, "VillageCurrentSituation");
			this.VillageCurrentSituation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.VillageCurrentSituation.Name = "VillageCurrentSituation";
			this.VillageCurrentSituation.Click += new System.EventHandler(this.VillageCurrentSituation_Click);
			// 
			// MarkPlayerOrTribe
			// 
			resources.ApplyResources(this.MarkPlayerOrTribe, "MarkPlayerOrTribe");
			this.MarkPlayerOrTribe.AllowBarbarianViews = false;
			this.MarkPlayerOrTribe.AutoUpdateMarkers = true;
			this.MarkPlayerOrTribe.BackColor = System.Drawing.Color.White;
			this.MarkPlayerOrTribe.CanDeactivate = true;
			this.MarkPlayerOrTribe.DefaultExtraMarkerColor = System.Drawing.Color.Transparent;
			this.MarkPlayerOrTribe.DefaultMarkerColor = System.Drawing.Color.Black;
			this.MarkPlayerOrTribe.Name = "MarkPlayerOrTribe";
			// 
			// DetailsControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.QuickFinderLayout);
			this.Name = "DetailsControl";
			this.Load += new System.EventHandler(this.DetailsControl_Load);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.CommentsPanel.ResumeLayout(false);
			this.CommentsPanel.PerformLayout();
			this.QuickFinderLayout.ResumeLayout(false);
			this.QuickFinderLayout.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton ViewPlayerDetails;
        private System.Windows.Forms.ToolStripButton ViewTribeDetails;
        private System.Windows.Forms.ToolStripButton ViewVillageDetails;
        private ToolStripVillageTextBox SelectedVillage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private TableWrapperControl Table;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid DetailsGrid;
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
        private System.Windows.Forms.ToolStripSeparator VillageSeperator;
        private System.Windows.Forms.ToolStripButton VillageCurrentSituation;
        private Maps.Markers.MarkerSettingsControl MarkPlayerOrTribe;
        private System.Windows.Forms.ToolStripButton CatapultFlag;
        private System.Windows.Forms.TextBox Comments;
        private System.Windows.Forms.Panel CommentsPanel;
        private System.Windows.Forms.Label CommentsLabel;
    }
}
