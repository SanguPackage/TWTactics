using TribalWars.Controls.Finders;
using TribalWars.Controls.XPTables;
using TribalWars.Worlds.Events.Impls;

namespace TribalWars.Controls.AccordeonLocation
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationFinderControl));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.MainTable = new System.Windows.Forms.TableLayoutPanel();
			this.SearchPanel = new System.Windows.Forms.Panel();
			this.Options = new TribalWars.Controls.Finders.FinderOptionsControl();
			this.Table = new TribalWars.Controls.XPTables.TableWrapperControl();
			this.groupBox1.SuspendLayout();
			this.MainTable.SuspendLayout();
			this.SearchPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Controls.Add(this.MainTable);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// MainTable
			// 
			resources.ApplyResources(this.MainTable, "MainTable");
			this.MainTable.Controls.Add(this.SearchPanel, 0, 0);
			this.MainTable.Controls.Add(this.Table, 0, 1);
			this.MainTable.Name = "MainTable";
			// 
			// SearchPanel
			// 
			resources.ApplyResources(this.SearchPanel, "SearchPanel");
			this.SearchPanel.Controls.Add(this.Options);
			this.SearchPanel.Name = "SearchPanel";
			// 
			// Options
			// 
			resources.ApplyResources(this.Options, "Options");
			this.Options.BackColor = System.Drawing.Color.Transparent;
			this.Options.Buttonsvisible = true;
			this.Options.Expanded = true;
			this.Options.LimitResultsValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.Options.Name = "Options";
			this.Options.PlayersFound += new System.EventHandler<TribalWars.Worlds.Events.Impls.PlayersEventArgs>(this.Options_PlayersFound);
			this.Options.VillagesFound += new System.EventHandler<TribalWars.Worlds.Events.Impls.VillagesEventArgs>(this.Options_VillagesFound);
			this.Options.TribesFound += new System.EventHandler<TribalWars.Worlds.Events.Impls.TribesEventArgs>(this.Options_TribesFound);
			this.Options.SizeChanged += new System.EventHandler(this.finderOptionsControl1_SizeChanged);
			// 
			// Table
			// 
			resources.ApplyResources(this.Table, "Table");
			this.Table.AutoSelectSingleRow = true;
			this.Table.BackColor = System.Drawing.Color.Transparent;
			this.Table.DisplayType = TribalWars.Controls.XPTables.TableWrapperControl.ColumnDisplayTypeEnum.Default;
			this.Table.Name = "Table";
			this.Table.RowSelectionAction = TribalWars.Controls.XPTables.TableWrapperControl.RowSelectionActionEnum.SelectVillage;
			this.Table.VisiblePlayerFields = TribalWars.Controls.XPTables.PlayerFields.None;
			this.Table.VisibleReportFields = TribalWars.Controls.XPTables.ReportFields.None;
			this.Table.VisibleTribeFields = TribalWars.Controls.XPTables.TribeFields.None;
			// 
			// LocationFinderControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "LocationFinderControl";
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
        private TableWrapperControl Table;
    }
}
