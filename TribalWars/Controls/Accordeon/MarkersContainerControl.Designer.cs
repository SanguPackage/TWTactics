namespace TribalWars.Controls
{
    partial class MarkersContainerControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkersContainerControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.FilterGrid = new System.Windows.Forms.PropertyGrid();
            this.Markers = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.MarkerGroups = new System.Windows.Forms.ListView();
            this.VillagesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.VillagePinPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.VillageCenter = new System.Windows.Forms.ToolStripMenuItem();
            this.VillageUnmark = new System.Windows.Forms.ToolStripMenuItem();
            this.StripNew = new System.Windows.Forms.ToolStripButton();
            this.MarkerGroupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.GroupEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.VillagesMenu.SuspendLayout();
            this.MarkerGroupMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(309, 349);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripNew});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(309, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.FilterGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(309, 324);
            this.splitContainer1.SplitterDistance = 77;
            this.splitContainer1.TabIndex = 1;
            // 
            // FilterGrid
            // 
            this.FilterGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterGrid.HelpVisible = false;
            this.FilterGrid.Location = new System.Drawing.Point(0, 0);
            this.FilterGrid.Margin = new System.Windows.Forms.Padding(0);
            this.FilterGrid.Name = "FilterGrid";
            this.FilterGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.FilterGrid.Size = new System.Drawing.Size(309, 77);
            this.FilterGrid.TabIndex = 3;
            this.FilterGrid.ToolbarVisible = false;
            // 
            // Markers
            // 
            this.Markers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.Markers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Markers.FullRowSelect = true;
            this.Markers.GridLines = true;
            this.Markers.Location = new System.Drawing.Point(0, 0);
            this.Markers.Margin = new System.Windows.Forms.Padding(0);
            this.Markers.Name = "Markers";
            this.Markers.Size = new System.Drawing.Size(309, 89);
            this.Markers.TabIndex = 3;
            this.Markers.UseCompatibleStateImageBehavior = false;
            this.Markers.View = System.Windows.Forms.View.Details;
            this.Markers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Markers_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 115;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Points";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Rank";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 42;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Villages";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.MarkerGroups);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.Markers);
            this.splitContainer2.Size = new System.Drawing.Size(309, 243);
            this.splitContainer2.SplitterDistance = 150;
            this.splitContainer2.TabIndex = 0;
            // 
            // MarkerGroups
            // 
            this.MarkerGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MarkerGroups.FullRowSelect = true;
            this.MarkerGroups.GridLines = true;
            this.MarkerGroups.Location = new System.Drawing.Point(0, 0);
            this.MarkerGroups.Margin = new System.Windows.Forms.Padding(0);
            this.MarkerGroups.Name = "MarkerGroups";
            this.MarkerGroups.Size = new System.Drawing.Size(309, 150);
            this.MarkerGroups.TabIndex = 3;
            this.MarkerGroups.UseCompatibleStateImageBehavior = false;
            this.MarkerGroups.View = System.Windows.Forms.View.Details;
            this.MarkerGroups.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MarkerGroups_MouseClick);
            this.MarkerGroups.SelectedIndexChanged += new System.EventHandler(this.MarkerGroups_SelectedIndexChanged);
            // 
            // VillagesMenu
            // 
            this.VillagesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VillagePinPoint,
            this.VillageCenter,
            this.VillageUnmark});
            this.VillagesMenu.Name = "VillagesMenu";
            this.VillagesMenu.Size = new System.Drawing.Size(124, 70);
            // 
            // VillagePinPoint
            // 
            this.VillagePinPoint.Name = "VillagePinPoint";
            this.VillagePinPoint.Size = new System.Drawing.Size(123, 22);
            this.VillagePinPoint.Text = "&Pinpoint";
            this.VillagePinPoint.Click += new System.EventHandler(this.VillagePinPoint_Click);
            // 
            // VillageCenter
            // 
            this.VillageCenter.Name = "VillageCenter";
            this.VillageCenter.Size = new System.Drawing.Size(123, 22);
            this.VillageCenter.Text = "&Center";
            this.VillageCenter.Click += new System.EventHandler(this.VillageCenter_Click);
            // 
            // VillageUnmark
            // 
            this.VillageUnmark.Name = "VillageUnmark";
            this.VillageUnmark.Size = new System.Drawing.Size(123, 22);
            this.VillageUnmark.Text = "&Unmark";
            this.VillageUnmark.Click += new System.EventHandler(this.VillageUnmark_Click);
            // 
            // StripNew
            // 
            this.StripNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StripNew.Image = ((System.Drawing.Image)(resources.GetObject("StripNew.Image")));
            this.StripNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StripNew.Name = "StripNew";
            this.StripNew.Size = new System.Drawing.Size(68, 22);
            this.StripNew.Text = "New Marker";
            this.StripNew.Click += new System.EventHandler(this.StripNew_Click);
            // 
            // MarkerGroupMenu
            // 
            this.MarkerGroupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GroupEdit,
            this.GroupDelete});
            this.MarkerGroupMenu.Name = "MarkerGroupMenu";
            this.MarkerGroupMenu.Size = new System.Drawing.Size(117, 48);
            // 
            // GroupEdit
            // 
            this.GroupEdit.Name = "GroupEdit";
            this.GroupEdit.Size = new System.Drawing.Size(152, 22);
            this.GroupEdit.Text = "&Edit";
            this.GroupEdit.Click += new System.EventHandler(this.GroupEdit_Click);
            // 
            // GroupDelete
            // 
            this.GroupDelete.Name = "GroupDelete";
            this.GroupDelete.Size = new System.Drawing.Size(152, 22);
            this.GroupDelete.Text = "&Delete";
            this.GroupDelete.Click += new System.EventHandler(this.GroupDelete_Click);
            // 
            // MarkersContainerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MarkersContainerControl";
            this.Size = new System.Drawing.Size(309, 349);
            this.Load += new System.EventHandler(this.MarkersContainerControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.VillagesMenu.ResumeLayout(false);
            this.MarkerGroupMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid FilterGrid;
        private System.Windows.Forms.ListView Markers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView MarkerGroups;
        private System.Windows.Forms.ContextMenuStrip VillagesMenu;
        private System.Windows.Forms.ToolStripMenuItem VillagePinPoint;
        private System.Windows.Forms.ToolStripMenuItem VillageCenter;
        private System.Windows.Forms.ToolStripMenuItem VillageUnmark;
        private System.Windows.Forms.ToolStripButton StripNew;
        private System.Windows.Forms.ContextMenuStrip MarkerGroupMenu;
        private System.Windows.Forms.ToolStripMenuItem GroupEdit;
        private System.Windows.Forms.ToolStripMenuItem GroupDelete;
    }
}
