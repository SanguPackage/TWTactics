namespace TribalWars.Controls.Accordeon.Location
{
    partial class LocationFinderControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MainTable = new System.Windows.Forms.TableLayoutPanel();
            this.SearchPanel = new System.Windows.Forms.Panel();
            this.Options = new TribalWars.Controls.Accordeon.Location.FinderOptionsControl();
            this.Table = new TribalWars.Controls.Display.TableWrapperControl();
            this.groupBox1.SuspendLayout();
            this.MainTable.SuspendLayout();
            this.SearchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MainTable);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 180);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Find players, tribes or villages";
            // 
            // MainTable
            // 
            this.MainTable.ColumnCount = 1;
            this.MainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTable.Controls.Add(this.SearchPanel, 0, 0);
            this.MainTable.Controls.Add(this.Table, 0, 1);
            this.MainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTable.Location = new System.Drawing.Point(3, 16);
            this.MainTable.Margin = new System.Windows.Forms.Padding(0);
            this.MainTable.Name = "MainTable";
            this.MainTable.RowCount = 2;
            this.MainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.MainTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTable.Size = new System.Drawing.Size(290, 161);
            this.MainTable.TabIndex = 3;
            // 
            // SearchPanel
            // 
            this.SearchPanel.Controls.Add(this.Options);
            this.SearchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchPanel.Location = new System.Drawing.Point(0, 0);
            this.SearchPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(290, 35);
            this.SearchPanel.TabIndex = 3;
            // 
            // Options
            // 
            this.Options.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Options.BackColor = System.Drawing.Color.Transparent;
            this.Options.Buttonsvisible = true;
            this.Options.Expanded = false;
            this.Options.LimitResultsValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.Options.Location = new System.Drawing.Point(0, 0);
            this.Options.Margin = new System.Windows.Forms.Padding(0);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(285, 35);
            this.Options.TabIndex = 0;
            this.Options.PlayersFound += new System.EventHandler<TribalWars.Data.Events.PlayersEventArgs>(this.Options_PlayersFound);
            this.Options.VillagesFound += new System.EventHandler<TribalWars.Data.Events.VillagesEventArgs>(this.Options_VillagesFound);
            this.Options.TribesFound += new System.EventHandler<TribalWars.Data.Events.TribesEventArgs>(this.Options_TribesFound);
            this.Options.SizeChanged += new System.EventHandler(this.finderOptionsControl1_SizeChanged);
            // 
            // Table
            // 
            this.Table.AutoSelectSingleRow = true;
            this.Table.BackColor = System.Drawing.Color.Transparent;
            this.Table.DisplayType = TribalWars.Controls.Display.TableWrapperControl.ColumnDisplayTypeEnum.Default;
            this.Table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Table.Location = new System.Drawing.Point(0, 35);
            this.Table.Margin = new System.Windows.Forms.Padding(0);
            this.Table.Name = "Table";
            this.Table.RowSelectionAction = TribalWars.Controls.Display.TableWrapperControl.RowSelectionActionEnum.SelectVillage;
            this.Table.Size = new System.Drawing.Size(290, 126);
            this.Table.TabIndex = 4;
            this.Table.VisiblePlayerFields = TribalWars.Controls.Display.PlayerFields.None;
            this.Table.VisibleReportFields = TribalWars.Controls.Display.ReportFields.None;
            this.Table.VisibleTribeFields = TribalWars.Controls.Display.TribeFields.None;
            // 
            // LocationFinderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "LocationFinderControl";
            this.Size = new System.Drawing.Size(296, 180);
            this.groupBox1.ResumeLayout(false);
            this.MainTable.ResumeLayout(false);
            this.SearchPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel MainTable;
        private System.Windows.Forms.Panel SearchPanel;
        private FinderOptionsControl Options;
        private TribalWars.Controls.Display.TableWrapperControl Table;
    }
}
