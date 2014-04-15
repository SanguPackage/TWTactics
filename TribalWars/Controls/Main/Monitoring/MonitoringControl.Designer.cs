namespace TribalWars.Controls.Main.Monitoring
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("New inactive");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Lost points");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Nobled");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Villages", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("No activity");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Tribe change");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Players", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("All players");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Nobled");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("No activity");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Lost points");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Tribe", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Filter");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Custom", new System.Windows.Forms.TreeNode[] {
            treeNode13});
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.OptionsTree = new System.Windows.Forms.TreeView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.PreviousDate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CurrentDate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PreviousDateList = new System.Windows.Forms.ListBox();
            this.NobledVillage = new XPTable.Models.TextColumn();
            this.NobledPlayer = new XPTable.Models.TextColumn();
            this.NobledPlayerOld = new XPTable.Models.TextColumn();
            this.NobledPoints = new XPTable.Models.NumberColumn();
            this.NobledPointsOld = new XPTable.Models.NumberColumn();
            this.OptionsGroupBox = new TribalWars.Controls.Accordeon.Location.FinderOptionsControl();
            this.Table = new TribalWars.Controls.Display.TableWrapperControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(719, 552);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(100, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(619, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.OptionsTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(97, 527);
            this.splitContainer1.SplitterDistance = 179;
            this.splitContainer1.TabIndex = 3;
            // 
            // OptionsTree
            // 
            this.OptionsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OptionsTree.Indent = 15;
            this.OptionsTree.Location = new System.Drawing.Point(0, 0);
            this.OptionsTree.Margin = new System.Windows.Forms.Padding(0);
            this.OptionsTree.Name = "OptionsTree";
            treeNode1.Name = "Node2";
            treeNode1.Tag = "VillageNewInactive";
            treeNode1.Text = "New inactive";
            treeNode1.ToolTipText = "Villages that have become abandoned";
            treeNode2.Name = "Node3";
            treeNode2.Tag = "VillageLostPoints";
            treeNode2.Text = "Lost points";
            treeNode2.ToolTipText = "Villages that lost points";
            treeNode3.Name = "Node8";
            treeNode3.Tag = "PlayerNobled";
            treeNode3.Text = "Nobled";
            treeNode3.ToolTipText = "Villages that have been nobled";
            treeNode4.Name = "Node0";
            treeNode4.Text = "Villages";
            treeNode5.Name = "Node7";
            treeNode5.Tag = "PlayerNoActivity";
            treeNode5.Text = "No activity";
            treeNode5.ToolTipText = "Players that have not increased in points";
            treeNode6.Name = "Node10";
            treeNode6.Tag = "PlayerTribeChange";
            treeNode6.Text = "Tribe change";
            treeNode6.ToolTipText = "Players that have changed tribes";
            treeNode7.Name = "Node6";
            treeNode7.Text = "Players";
            treeNode8.Name = "Node2";
            treeNode8.Tag = "TribePlayers";
            treeNode8.Text = "All players";
            treeNode8.ToolTipText = "Get a list of all players";
            treeNode9.Name = "Node1";
            treeNode9.Tag = "TribeNobled";
            treeNode9.Text = "Nobled";
            treeNode9.ToolTipText = "See who has nobled or was nobled in your tribe";
            treeNode10.Name = "Node2";
            treeNode10.Tag = "TribeNoActivity";
            treeNode10.Text = "No activity";
            treeNode10.ToolTipText = "See who has not grown in your tribe.";
            treeNode11.Name = "Node3";
            treeNode11.Tag = "TribeLostPoints";
            treeNode11.Text = "Lost points";
            treeNode11.ToolTipText = "See who has lost points in your tribe.";
            treeNode12.Name = "Node0";
            treeNode12.Text = "Tribe";
            treeNode13.Name = "Node1";
            treeNode13.Tag = "Filter";
            treeNode13.Text = "Filter";
            treeNode14.Name = "Node0";
            treeNode14.Text = "Custom";
            this.OptionsTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode7,
            treeNode12,
            treeNode14});
            this.OptionsTree.ShowNodeToolTips = true;
            this.OptionsTree.ShowPlusMinus = false;
            this.OptionsTree.ShowRootLines = false;
            this.OptionsTree.Size = new System.Drawing.Size(97, 179);
            this.OptionsTree.TabIndex = 1;
            this.OptionsTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OptionsTree_NodeMouseClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.PreviousDate);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.CurrentDate);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.label6);
            this.flowLayoutPanel1.Controls.Add(this.label7);
            this.flowLayoutPanel1.Controls.Add(this.label8);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(97, 344);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Comparing";
            // 
            // PreviousDate
            // 
            this.PreviousDate.AutoSize = true;
            this.PreviousDate.Location = new System.Drawing.Point(3, 19);
            this.PreviousDate.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.PreviousDate.Name = "PreviousDate";
            this.PreviousDate.Size = new System.Drawing.Size(53, 13);
            this.PreviousDate.TabIndex = 1;
            this.PreviousDate.Text = "Unknown";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "and";
            // 
            // CurrentDate
            // 
            this.CurrentDate.AutoSize = true;
            this.CurrentDate.Location = new System.Drawing.Point(3, 51);
            this.CurrentDate.Name = "CurrentDate";
            this.CurrentDate.Size = new System.Drawing.Size(53, 13);
            this.CurrentDate.TabIndex = 3;
            this.CurrentDate.Text = "Unknown";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 52);
            this.label2.TabIndex = 0;
            this.label2.Text = "mark you, your tribe and your personal allies in special color";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 52);
            this.label6.TabIndex = 3;
            this.label6.Text = "use one of the map options and jump to the map aswell...";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 65);
            this.label7.TabIndex = 4;
            this.label7.Text = "specify people that need watching and warn when they change";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 233);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 65);
            this.label8.TabIndex = 5;
            this.label8.Text = "display rectangle && left clicked village + player on map (yellow + white)";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(103, 28);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel3);
            this.splitContainer2.Panel1MinSize = 200;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.Table);
            this.splitContainer2.Size = new System.Drawing.Size(613, 521);
            this.splitContainer2.SplitterDistance = 200;
            this.splitContainer2.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 291F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.PreviousDateList, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(613, 200);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OptionsGroupBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(203, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 194);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // PreviousDateList
            // 
            this.PreviousDateList.Enabled = false;
            this.PreviousDateList.FormattingEnabled = true;
            this.PreviousDateList.Location = new System.Drawing.Point(494, 3);
            this.PreviousDateList.Name = "PreviousDateList";
            this.PreviousDateList.Size = new System.Drawing.Size(116, 186);
            this.PreviousDateList.TabIndex = 5;
            this.PreviousDateList.SelectedIndexChanged += new System.EventHandler(this.PreviousDateList_SelectedIndexChanged);
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
            // OptionsGroupBox
            // 
            this.OptionsGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.OptionsGroupBox.Buttonsvisible = false;
            this.OptionsGroupBox.Expanded = true;
            this.OptionsGroupBox.Location = new System.Drawing.Point(-1, 14);
            this.OptionsGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.OptionsGroupBox.Name = "OptionsGroupBox";
            this.OptionsGroupBox.Size = new System.Drawing.Size(285, 178);
            this.OptionsGroupBox.TabIndex = 0;
            // 
            // Table
            // 
            this.Table.BackColor = System.Drawing.Color.Transparent;
            this.Table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Table.DisplayType = TribalWars.Controls.Display.TableWrapperControl.ColumnDisplayTypeEnum.All;
            this.Table.Location = new System.Drawing.Point(0, 0);
            this.Table.Margin = new System.Windows.Forms.Padding(0);
            this.Table.Name = "Table";
            this.Table.Size = new System.Drawing.Size(613, 317);
            this.Table.TabIndex = 0;
            // 
            // MonitoringControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MonitoringControl";
            this.Size = new System.Drawing.Size(719, 552);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private XPTable.Models.TextColumn NobledVillage;
        private XPTable.Models.TextColumn NobledPlayer;
        private XPTable.Models.TextColumn NobledPlayerOld;
        private XPTable.Models.NumberColumn NobledPoints;
        private XPTable.Models.NumberColumn NobledPointsOld;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView OptionsTree;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label PreviousDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label CurrentDate;
        private TribalWars.Controls.Accordeon.Location.FinderOptionsControl OptionsGroupBox;
        private System.Windows.Forms.ListBox PreviousDateList;
        private TribalWars.Controls.Display.TableWrapperControl Table;

    }
}
