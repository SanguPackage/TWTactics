using TribalWars.Controls.Finders;
using TribalWars.Controls.XPTables;

namespace TribalWars.Maps.Monitoring
{
    partial class MonitoringControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitoringControl));
			this.AdditionalFiltersGroupbox = new Janus.Windows.EditControls.UIGroupBox();
			this.AdditionalFilters = new TribalWars.Controls.Finders.FinderOptionsControl();
			this.ActivateAdditionalFilters = new System.Windows.Forms.CheckBox();
			this.ApplyAdditionalFilters = new System.Windows.Forms.Button();
			this.NobledVillage = new XPTable.Models.TextColumn();
			this.NobledPlayer = new XPTable.Models.TextColumn();
			this.NobledPlayerOld = new XPTable.Models.TextColumn();
			this.NobledPoints = new XPTable.Models.NumberColumn();
			this.NobledPointsOld = new XPTable.Models.NumberColumn();
			this.FiltersPremadeGroupbox = new Janus.Windows.EditControls.UIGroupBox();
			this.OptionsTree = new System.Windows.Forms.TreeView();
			this.groupBox2 = new Janus.Windows.EditControls.UIGroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox5 = new Janus.Windows.EditControls.UIGroupBox();
			this.PreviousDateList = new System.Windows.Forms.ListView();
			this.TextPrevious = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.groupBox4 = new Janus.Windows.EditControls.UIGroupBox();
			this.CurrentDataDate = new System.Windows.Forms.Label();
			this.groupBox3 = new Janus.Windows.EditControls.UIGroupBox();
			this.Table = new TribalWars.Controls.XPTables.TableWrapperControl();
			((System.ComponentModel.ISupportInitialize)(this.AdditionalFiltersGroupbox)).BeginInit();
			this.AdditionalFiltersGroupbox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.FiltersPremadeGroupbox)).BeginInit();
			this.FiltersPremadeGroupbox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupBox5)).BeginInit();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupBox4)).BeginInit();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupBox3)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// AdditionalFiltersGroupbox
			// 
			this.AdditionalFiltersGroupbox.Controls.Add(this.AdditionalFilters);
			this.AdditionalFiltersGroupbox.Controls.Add(this.ActivateAdditionalFilters);
			this.AdditionalFiltersGroupbox.Controls.Add(this.ApplyAdditionalFilters);
			resources.ApplyResources(this.AdditionalFiltersGroupbox, "AdditionalFiltersGroupbox");
			this.AdditionalFiltersGroupbox.Name = "AdditionalFiltersGroupbox";
			// 
			// AdditionalFilters
			// 
			this.AdditionalFilters.BackColor = System.Drawing.Color.Transparent;
			this.AdditionalFilters.Buttonsvisible = false;
			resources.ApplyResources(this.AdditionalFilters, "AdditionalFilters");
			this.AdditionalFilters.Expanded = true;
			this.AdditionalFilters.LimitResultsValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.AdditionalFilters.Name = "AdditionalFilters";
			// 
			// ActivateAdditionalFilters
			// 
			resources.ApplyResources(this.ActivateAdditionalFilters, "ActivateAdditionalFilters");
			this.ActivateAdditionalFilters.Name = "ActivateAdditionalFilters";
			this.ActivateAdditionalFilters.UseVisualStyleBackColor = true;
			this.ActivateAdditionalFilters.CheckedChanged += new System.EventHandler(this.ActivateAdditionalFilters_CheckedChanged);
			// 
			// ApplyAdditionalFilters
			// 
			resources.ApplyResources(this.ApplyAdditionalFilters, "ApplyAdditionalFilters");
			this.ApplyAdditionalFilters.Name = "ApplyAdditionalFilters";
			this.ApplyAdditionalFilters.UseVisualStyleBackColor = true;
			this.ApplyAdditionalFilters.Click += new System.EventHandler(this.ApplyAdditionalFilters_Click);
			// 
			// NobledVillage
			// 
			resources.ApplyResources(this.NobledVillage, "NobledVillage");
			// 
			// NobledPlayer
			// 
			resources.ApplyResources(this.NobledPlayer, "NobledPlayer");
			// 
			// NobledPlayerOld
			// 
			resources.ApplyResources(this.NobledPlayerOld, "NobledPlayerOld");
			// 
			// NobledPoints
			// 
			resources.ApplyResources(this.NobledPoints, "NobledPoints");
			// 
			// NobledPointsOld
			// 
			resources.ApplyResources(this.NobledPointsOld, "NobledPointsOld");
			// 
			// FiltersPremadeGroupbox
			// 
			this.FiltersPremadeGroupbox.Controls.Add(this.OptionsTree);
			resources.ApplyResources(this.FiltersPremadeGroupbox, "FiltersPremadeGroupbox");
			this.FiltersPremadeGroupbox.Name = "FiltersPremadeGroupbox";
			// 
			// OptionsTree
			// 
			this.OptionsTree.HideSelection = false;
			resources.ApplyResources(this.OptionsTree, "OptionsTree");
			this.OptionsTree.Name = "OptionsTree";
			this.OptionsTree.ShowLines = false;
			this.OptionsTree.ShowNodeToolTips = true;
			this.OptionsTree.ShowPlusMinus = false;
			this.OptionsTree.ShowRootLines = false;
			this.OptionsTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.OptionsTree_BeforeSelect);
			this.OptionsTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OptionsTree_NodeMouseClick);
			// 
			// groupBox2
			// 
			resources.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Controls.Add(this.tableLayoutPanel1);
			this.groupBox2.Name = "groupBox2";
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.PreviousDateList);
			resources.ApplyResources(this.groupBox5, "groupBox5");
			this.groupBox5.Name = "groupBox5";
			// 
			// PreviousDateList
			// 
			this.PreviousDateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TextPrevious});
			resources.ApplyResources(this.PreviousDateList, "PreviousDateList");
			this.PreviousDateList.FullRowSelect = true;
			this.PreviousDateList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.PreviousDateList.HideSelection = false;
			this.PreviousDateList.MultiSelect = false;
			this.PreviousDateList.Name = "PreviousDateList";
			this.PreviousDateList.ShowItemToolTips = true;
			this.PreviousDateList.UseCompatibleStateImageBehavior = false;
			this.PreviousDateList.View = System.Windows.Forms.View.List;
			this.PreviousDateList.SelectedIndexChanged += new System.EventHandler(this.PreviousDateList_SelectedIndexChanged);
			// 
			// TextPrevious
			// 
			resources.ApplyResources(this.TextPrevious, "TextPrevious");
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.CurrentDataDate);
			resources.ApplyResources(this.groupBox4, "groupBox4");
			this.groupBox4.Name = "groupBox4";
			// 
			// CurrentDataDate
			// 
			resources.ApplyResources(this.CurrentDataDate, "CurrentDataDate");
			this.CurrentDataDate.Name = "CurrentDataDate";
			// 
			// groupBox3
			// 
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Controls.Add(this.Table);
			this.groupBox3.Name = "groupBox3";
			// 
			// Table
			// 
			this.Table.AutoSelectSingleRow = true;
			this.Table.BackColor = System.Drawing.Color.Transparent;
			this.Table.DisplayType = TribalWars.Controls.XPTables.TableWrapperControl.ColumnDisplayTypeEnum.All;
			resources.ApplyResources(this.Table, "Table");
			this.Table.Name = "Table";
			this.Table.RowSelectionAction = TribalWars.Controls.XPTables.TableWrapperControl.RowSelectionActionEnum.SelectVillage;
			this.Table.VisiblePlayerFields = TribalWars.Controls.XPTables.PlayerFields.None;
			this.Table.VisibleReportFields = TribalWars.Controls.XPTables.ReportFields.None;
			this.Table.VisibleTribeFields = TribalWars.Controls.XPTables.TribeFields.None;
			// 
			// MonitoringControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.AdditionalFiltersGroupbox);
			this.Controls.Add(this.FiltersPremadeGroupbox);
			this.Controls.Add(this.groupBox2);
			this.Name = "MonitoringControl";
			((System.ComponentModel.ISupportInitialize)(this.AdditionalFiltersGroupbox)).EndInit();
			this.AdditionalFiltersGroupbox.ResumeLayout(false);
			this.AdditionalFiltersGroupbox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.FiltersPremadeGroupbox)).EndInit();
			this.FiltersPremadeGroupbox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.groupBox5)).EndInit();
			this.groupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.groupBox4)).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupBox3)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private XPTable.Models.TextColumn NobledVillage;
        private XPTable.Models.TextColumn NobledPlayer;
        private XPTable.Models.TextColumn NobledPlayerOld;
        private XPTable.Models.NumberColumn NobledPoints;
		private XPTable.Models.NumberColumn NobledPointsOld;
		private Janus.Windows.EditControls.UIGroupBox AdditionalFiltersGroupbox;
        private TableWrapperControl Table;
        private Janus.Windows.EditControls.UIGroupBox FiltersPremadeGroupbox;
        private Janus.Windows.EditControls.UIGroupBox groupBox2;
        private Janus.Windows.EditControls.UIGroupBox groupBox3;
        private System.Windows.Forms.Button ApplyAdditionalFilters;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Janus.Windows.EditControls.UIGroupBox groupBox5;
        private Janus.Windows.EditControls.UIGroupBox groupBox4;
        private System.Windows.Forms.Label CurrentDataDate;
        private System.Windows.Forms.ListView PreviousDateList;
        private System.Windows.Forms.ColumnHeader TextPrevious;
		private System.Windows.Forms.CheckBox ActivateAdditionalFilters;
		private FinderOptionsControl AdditionalFilters;
		private System.Windows.Forms.TreeView OptionsTree;

    }
}
