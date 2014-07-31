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
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("New inactive");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Lost points");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Nobled");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Villages", new System.Windows.Forms.TreeNode[] {
            treeNode25,
            treeNode26,
            treeNode27});
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("No activity");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Tribe change");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Players", new System.Windows.Forms.TreeNode[] {
            treeNode29,
            treeNode30});
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Players");
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Nobled");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("No activity");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Lost points");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("Your Tribe", new System.Windows.Forms.TreeNode[] {
            treeNode32,
            treeNode33,
            treeNode34,
            treeNode35});
            this.OptionsTree = new System.Windows.Forms.TreeView();
            this.AdditionalFiltersGroupbox = new Janus.Windows.EditControls.UIGroupBox();
            this.ApplyAdditionalFilters = new System.Windows.Forms.Button();
            this.NobledVillage = new XPTable.Models.TextColumn();
            this.NobledPlayer = new XPTable.Models.TextColumn();
            this.NobledPlayerOld = new XPTable.Models.TextColumn();
            this.NobledPoints = new XPTable.Models.NumberColumn();
            this.NobledPointsOld = new XPTable.Models.NumberColumn();
            this.FiltersPremadeGroupbox = new Janus.Windows.EditControls.UIGroupBox();
            this.groupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox5 = new Janus.Windows.EditControls.UIGroupBox();
            this.PreviousDateList = new System.Windows.Forms.ListView();
            this.TextPrevious = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new Janus.Windows.EditControls.UIGroupBox();
            this.CurrentDataDate = new System.Windows.Forms.Label();
            this.groupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.Table = new TableWrapperControl();
            this.AdditionalFilters = new FinderOptionsControl();
            this.ActivateAdditionalFilters = new System.Windows.Forms.CheckBox();
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
            // OptionsTree
            // 
            this.OptionsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OptionsTree.HideSelection = false;
            this.OptionsTree.Indent = 15;
            this.OptionsTree.Location = new System.Drawing.Point(3, 16);
            this.OptionsTree.Margin = new System.Windows.Forms.Padding(0);
            this.OptionsTree.Name = "OptionsTree";
            treeNode25.Name = "Node2";
            treeNode25.Tag = "VillageNewInactive";
            treeNode25.Text = "New inactive";
            treeNode25.ToolTipText = "Villages that have become abandoned";
            treeNode26.Name = "Node3";
            treeNode26.Tag = "VillageLostPoints";
            treeNode26.Text = "Lost points";
            treeNode26.ToolTipText = "Villages that lost points";
            treeNode27.Name = "Node8";
            treeNode27.Tag = "PlayerNobled";
            treeNode27.Text = "Nobled";
            treeNode27.ToolTipText = "Villages that have been nobled";
            treeNode28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            treeNode28.Name = "Node0";
            treeNode28.Text = "Villages";
            treeNode29.Name = "Node7";
            treeNode29.Tag = "PlayerNoActivity";
            treeNode29.Text = "No activity";
            treeNode29.ToolTipText = "Players that have not increased in points";
            treeNode30.Name = "Node10";
            treeNode30.Tag = "PlayerTribeChange";
            treeNode30.Text = "Tribe change";
            treeNode30.ToolTipText = "Players that have changed tribes";
            treeNode31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            treeNode31.Name = "Node6";
            treeNode31.Text = "Players";
            treeNode32.Name = "Node2";
            treeNode32.Tag = "TribePlayers";
            treeNode32.Text = "Players";
            treeNode32.ToolTipText = "Get a list of all players";
            treeNode33.Name = "Node1";
            treeNode33.Tag = "TribeNobled";
            treeNode33.Text = "Nobled";
            treeNode33.ToolTipText = "See who has nobled or was nobled in your tribe";
            treeNode34.Name = "Node2";
            treeNode34.Tag = "TribeNoActivity";
            treeNode34.Text = "No activity";
            treeNode34.ToolTipText = "See who has not grown in your tribe.";
            treeNode35.Name = "Node3";
            treeNode35.Tag = "TribeLostPoints";
            treeNode35.Text = "Lost points";
            treeNode35.ToolTipText = "See who has lost points in your tribe.";
            treeNode36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            treeNode36.Name = "Node0";
            treeNode36.Text = "Your Tribe";
            this.OptionsTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode28,
            treeNode31,
            treeNode36});
            this.OptionsTree.ShowNodeToolTips = true;
            this.OptionsTree.ShowPlusMinus = false;
            this.OptionsTree.ShowRootLines = false;
            this.OptionsTree.Size = new System.Drawing.Size(141, 222);
            this.OptionsTree.TabIndex = 1;
            this.OptionsTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.OptionsTree_BeforeSelect);
            this.OptionsTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OptionsTree_NodeMouseClick);
            // 
            // AdditionalFiltersGroupbox
            // 
            this.AdditionalFiltersGroupbox.Controls.Add(this.ActivateAdditionalFilters);
            this.AdditionalFiltersGroupbox.Controls.Add(this.ApplyAdditionalFilters);
            this.AdditionalFiltersGroupbox.Controls.Add(this.AdditionalFilters);
            this.AdditionalFiltersGroupbox.Location = new System.Drawing.Point(156, 3);
            this.AdditionalFiltersGroupbox.Name = "AdditionalFiltersGroupbox";
            this.AdditionalFiltersGroupbox.Size = new System.Drawing.Size(286, 241);
            this.AdditionalFiltersGroupbox.TabIndex = 0;
            this.AdditionalFiltersGroupbox.Text = "      Custom filter";
            // 
            // ApplyAdditionalFilters
            // 
            this.ApplyAdditionalFilters.Enabled = false;
            this.ApplyAdditionalFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplyAdditionalFilters.Location = new System.Drawing.Point(3, 200);
            this.ApplyAdditionalFilters.Name = "ApplyAdditionalFilters";
            this.ApplyAdditionalFilters.Size = new System.Drawing.Size(276, 35);
            this.ApplyAdditionalFilters.TabIndex = 2;
            this.ApplyAdditionalFilters.Text = "Apply additional filters";
            this.ApplyAdditionalFilters.UseVisualStyleBackColor = true;
            this.ApplyAdditionalFilters.Click += new System.EventHandler(this.ApplyAdditionalFilters_Click);
            // 
            // NobledVillage
            // 
            this.NobledVillage.Text = "Village";
            // 
            // NobledPlayer
            // 
            this.NobledPlayer.Text = "Player";
            // 
            // NobledPlayerOld
            // 
            this.NobledPlayerOld.Text = "Previous owner";
            // 
            // NobledPoints
            // 
            this.NobledPoints.Format = "#,0";
            this.NobledPoints.Text = "Points";
            // 
            // NobledPointsOld
            // 
            this.NobledPointsOld.Format = "#,0";
            this.NobledPointsOld.Text = "Difference";
            // 
            // FiltersPremadeGroupbox
            // 
            this.FiltersPremadeGroupbox.Controls.Add(this.OptionsTree);
            this.FiltersPremadeGroupbox.Location = new System.Drawing.Point(3, 3);
            this.FiltersPremadeGroupbox.Name = "FiltersPremadeGroupbox";
            this.FiltersPremadeGroupbox.Size = new System.Drawing.Size(147, 241);
            this.FiltersPremadeGroupbox.TabIndex = 1;
            this.FiltersPremadeGroupbox.Text = "Premade filters";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Location = new System.Drawing.Point(3, 250);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 349);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.Text = "Select dates";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(141, 330);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.PreviousDateList);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 53);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(135, 274);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.Text = "with \"previous\"";
            // 
            // PreviousDateList
            // 
            this.PreviousDateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TextPrevious});
            this.PreviousDateList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviousDateList.Enabled = false;
            this.PreviousDateList.FullRowSelect = true;
            this.PreviousDateList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.PreviousDateList.HideSelection = false;
            this.PreviousDateList.Location = new System.Drawing.Point(3, 16);
            this.PreviousDateList.MultiSelect = false;
            this.PreviousDateList.Name = "PreviousDateList";
            this.PreviousDateList.ShowItemToolTips = true;
            this.PreviousDateList.Size = new System.Drawing.Size(129, 255);
            this.PreviousDateList.TabIndex = 0;
            this.PreviousDateList.UseCompatibleStateImageBehavior = false;
            this.PreviousDateList.View = System.Windows.Forms.View.List;
            this.PreviousDateList.SelectedIndexChanged += new System.EventHandler(this.PreviousDateList_SelectedIndexChanged);
            // 
            // TextPrevious
            // 
            this.TextPrevious.Text = "";
            this.TextPrevious.Width = 200;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.CurrentDataDate);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(135, 44);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.Text = "Comparing \"current\"";
            // 
            // CurrentDataDate
            // 
            this.CurrentDataDate.AutoSize = true;
            this.CurrentDataDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentDataDate.Location = new System.Drawing.Point(7, 20);
            this.CurrentDataDate.Name = "CurrentDataDate";
            this.CurrentDataDate.Size = new System.Drawing.Size(77, 13);
            this.CurrentDataDate.TabIndex = 0;
            this.CurrentDataDate.Text = "Current date";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.Table);
            this.groupBox3.Location = new System.Drawing.Point(155, 250);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(442, 349);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.Text = "Results";
            // 
            // Table
            // 
            this.Table.AutoSelectSingleRow = true;
            this.Table.BackColor = System.Drawing.Color.Transparent;
            this.Table.DisplayType = TableWrapperControl.ColumnDisplayTypeEnum.All;
            this.Table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Table.Location = new System.Drawing.Point(3, 16);
            this.Table.Margin = new System.Windows.Forms.Padding(0);
            this.Table.Name = "Table";
            this.Table.RowSelectionAction = TableWrapperControl.RowSelectionActionEnum.SelectVillage;
            this.Table.Size = new System.Drawing.Size(436, 330);
            this.Table.TabIndex = 0;
            this.Table.VisiblePlayerFields = PlayerFields.None;
            this.Table.VisibleReportFields = ReportFields.None;
            this.Table.VisibleTribeFields = TribeFields.None;
            // 
            // AdditionalFilters
            // 
            this.AdditionalFilters.BackColor = System.Drawing.Color.Transparent;
            this.AdditionalFilters.Buttonsvisible = false;
            this.AdditionalFilters.Enabled = false;
            this.AdditionalFilters.Expanded = false;
            this.AdditionalFilters.LimitResultsValue = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.AdditionalFilters.Location = new System.Drawing.Point(-1, 14);
            this.AdditionalFilters.Margin = new System.Windows.Forms.Padding(0);
            this.AdditionalFilters.Name = "AdditionalFilters";
            this.AdditionalFilters.Size = new System.Drawing.Size(285, 187);
            this.AdditionalFilters.TabIndex = 0;
            // 
            // ActivateAdditionalFilters
            // 
            this.ActivateAdditionalFilters.AutoSize = true;
            this.ActivateAdditionalFilters.Location = new System.Drawing.Point(12, 0);
            this.ActivateAdditionalFilters.Name = "ActivateAdditionalFilters";
            this.ActivateAdditionalFilters.Size = new System.Drawing.Size(15, 14);
            this.ActivateAdditionalFilters.TabIndex = 8;
            this.ActivateAdditionalFilters.UseVisualStyleBackColor = true;
            this.ActivateAdditionalFilters.CheckedChanged += new System.EventHandler(this.ActivateAdditionalFilters_CheckedChanged);
            // 
            // MonitoringControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.AdditionalFiltersGroupbox);
            this.Controls.Add(this.FiltersPremadeGroupbox);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MonitoringControl";
            this.Size = new System.Drawing.Size(601, 602);
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
        private System.Windows.Forms.TreeView OptionsTree;
        private Janus.Windows.EditControls.UIGroupBox AdditionalFiltersGroupbox;
        private FinderOptionsControl AdditionalFilters;
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

    }
}
