namespace TribalWars.Maps.Markers
{
    partial class MarkersControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkersControl));
			Janus.Windows.GridEX.GridEXLayout MarkersGrid_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
			Janus.Windows.Common.Layouts.JanusLayoutReference MarkersGrid_DesignTimeLayout_Reference_0 = new Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column6.ButtonImage");
			this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
			this.RefreshMarkersButton = new Janus.Windows.EditControls.UIButton();
			this.MarkersGrid = new Janus.Windows.GridEX.GridEX();
			this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
			this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
			this.AbandonedMarker = new TribalWars.Maps.Markers.MarkerSettingsControl();
			this.EnemyMarker = new TribalWars.Maps.Markers.MarkerSettingsControl();
			((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
			this.uiGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MarkersGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
			this.uiGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
			this.uiGroupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// uiGroupBox1
			// 
			resources.ApplyResources(this.uiGroupBox1, "uiGroupBox1");
			this.uiGroupBox1.Controls.Add(this.RefreshMarkersButton);
			this.uiGroupBox1.Controls.Add(this.MarkersGrid);
			this.uiGroupBox1.Name = "uiGroupBox1";
			// 
			// RefreshMarkersButton
			// 
			resources.ApplyResources(this.RefreshMarkersButton, "RefreshMarkersButton");
			this.RefreshMarkersButton.Icon = ((System.Drawing.Icon)(resources.GetObject("RefreshMarkersButton.Icon")));
			this.RefreshMarkersButton.Name = "RefreshMarkersButton";
			this.RefreshMarkersButton.Click += new System.EventHandler(this.RefreshMarkersButton_Click);
			// 
			// MarkersGrid
			// 
			resources.ApplyResources(this.MarkersGrid, "MarkersGrid");
			this.MarkersGrid.ColumnAutoResize = true;
			MarkersGrid_DesignTimeLayout_Reference_0.Instance = ((object)(resources.GetObject("MarkersGrid_DesignTimeLayout_Reference_0.Instance")));
			MarkersGrid_DesignTimeLayout.LayoutReferences.AddRange(new Janus.Windows.Common.Layouts.JanusLayoutReference[] {
            MarkersGrid_DesignTimeLayout_Reference_0});
			resources.ApplyResources(MarkersGrid_DesignTimeLayout, "MarkersGrid_DesignTimeLayout");
			this.MarkersGrid.DesignTimeLayout = MarkersGrid_DesignTimeLayout;
			this.MarkersGrid.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
			this.MarkersGrid.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
			this.MarkersGrid.GroupByBoxVisible = false;
			this.MarkersGrid.Name = "MarkersGrid";
			this.MarkersGrid.SettingsKey = "MarkersGrid2";
			this.MarkersGrid.UpdateMode = Janus.Windows.GridEX.UpdateMode.CellUpdate;
			this.MarkersGrid.DeletingRecord += new Janus.Windows.GridEX.RowActionCancelEventHandler(this.MarkersGrid_DeletingRecord);
			this.MarkersGrid.GetNewRow += new Janus.Windows.GridEX.GetNewRowEventHandler(this.MarkersGrid_GetNewRow);
			this.MarkersGrid.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.MarkersGrid_FormattingRow);
			this.MarkersGrid.UpdatingCell += new Janus.Windows.GridEX.UpdatingCellEventHandler(this.MarkersGrid_UpdatingCell);
			this.MarkersGrid.AddingRecord += new System.ComponentModel.CancelEventHandler(this.MarkersGrid_AddingRecord);
			this.MarkersGrid.ColumnButtonClick += new Janus.Windows.GridEX.ColumnActionEventHandler(this.MarkersGrid_ColumnButtonClick);
			this.MarkersGrid.InitCustomEdit += new Janus.Windows.GridEX.InitCustomEditEventHandler(this.MarkersGrid_InitCustomEdit);
			this.MarkersGrid.EndCustomEdit += new Janus.Windows.GridEX.EndCustomEditEventHandler(this.MarkersGrid_EndCustomEdit);
			// 
			// uiGroupBox2
			// 
			resources.ApplyResources(this.uiGroupBox2, "uiGroupBox2");
			this.uiGroupBox2.Controls.Add(this.EnemyMarker);
			this.uiGroupBox2.Name = "uiGroupBox2";
			// 
			// uiGroupBox3
			// 
			resources.ApplyResources(this.uiGroupBox3, "uiGroupBox3");
			this.uiGroupBox3.Controls.Add(this.AbandonedMarker);
			this.uiGroupBox3.Name = "uiGroupBox3";
			// 
			// AbandonedMarker
			// 
			resources.ApplyResources(this.AbandonedMarker, "AbandonedMarker");
			this.AbandonedMarker.AllowBarbarianViews = true;
			this.AbandonedMarker.AutoUpdateMarkers = true;
			this.AbandonedMarker.BackColor = System.Drawing.Color.Transparent;
			this.AbandonedMarker.CanDeactivate = false;
			this.AbandonedMarker.DefaultExtraMarkerColor = System.Drawing.Color.Transparent;
			this.AbandonedMarker.DefaultMarkerColor = System.Drawing.Color.Olive;
			this.AbandonedMarker.Name = "AbandonedMarker";
			// 
			// EnemyMarker
			// 
			resources.ApplyResources(this.EnemyMarker, "EnemyMarker");
			this.EnemyMarker.AllowBarbarianViews = false;
			this.EnemyMarker.AutoUpdateMarkers = true;
			this.EnemyMarker.BackColor = System.Drawing.Color.Transparent;
			this.EnemyMarker.CanDeactivate = false;
			this.EnemyMarker.DefaultExtraMarkerColor = System.Drawing.Color.Transparent;
			this.EnemyMarker.DefaultMarkerColor = System.Drawing.Color.Red;
			this.EnemyMarker.Name = "EnemyMarker";
			// 
			// MarkersControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.uiGroupBox3);
			this.Controls.Add(this.uiGroupBox2);
			this.Controls.Add(this.uiGroupBox1);
			this.Name = "MarkersControl";
			this.Load += new System.EventHandler(this.MarkersControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
			this.uiGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MarkersGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
			this.uiGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
			this.uiGroupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.GridEX MarkersGrid;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private MarkerSettingsControl EnemyMarker;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private MarkerSettingsControl AbandonedMarker;
        private Janus.Windows.EditControls.UIButton RefreshMarkersButton;
    }
}
