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
            this.EnemyMarker = new TribalWars.Maps.Markers.MarkerSettingsControl();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.AbandonedMarker = new TribalWars.Maps.Markers.MarkerSettingsControl();
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
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.RefreshMarkersButton);
            this.uiGroupBox1.Controls.Add(this.MarkersGrid);
            this.uiGroupBox1.Location = new System.Drawing.Point(2, 103);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(318, 264);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Markers";
            // 
            // RefreshMarkersButton
            // 
            this.RefreshMarkersButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshMarkersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshMarkersButton.Icon = ((System.Drawing.Icon)(resources.GetObject("RefreshMarkersButton.Icon")));
            this.RefreshMarkersButton.ImageSize = new System.Drawing.Size(64, 64);
            this.RefreshMarkersButton.Location = new System.Drawing.Point(3, 210);
            this.RefreshMarkersButton.Name = "RefreshMarkersButton";
            this.RefreshMarkersButton.Size = new System.Drawing.Size(312, 48);
            this.RefreshMarkersButton.TabIndex = 1;
            this.RefreshMarkersButton.Text = "Refresh";
            this.RefreshMarkersButton.ToolTipText = "The marker list is not updated when you alter the markers from the context menu. " +
    "Click this button to refresh the marker list.";
            this.RefreshMarkersButton.Click += new System.EventHandler(this.RefreshMarkersButton_Click);
            // 
            // MarkersGrid
            // 
            this.MarkersGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MarkersGrid.ColumnAutoResize = true;
            MarkersGrid_DesignTimeLayout_Reference_0.Instance = ((object)(resources.GetObject("MarkersGrid_DesignTimeLayout_Reference_0.Instance")));
            MarkersGrid_DesignTimeLayout.LayoutReferences.AddRange(new Janus.Windows.Common.Layouts.JanusLayoutReference[] {
            MarkersGrid_DesignTimeLayout_Reference_0});
            MarkersGrid_DesignTimeLayout.LayoutString = resources.GetString("MarkersGrid_DesignTimeLayout.LayoutString");
            this.MarkersGrid.DesignTimeLayout = MarkersGrid_DesignTimeLayout;
            this.MarkersGrid.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.MarkersGrid.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.MarkersGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.MarkersGrid.GroupByBoxVisible = false;
            this.MarkersGrid.Location = new System.Drawing.Point(3, 22);
            this.MarkersGrid.Name = "MarkersGrid";
            this.MarkersGrid.SettingsKey = "MarkersGrid2";
            this.MarkersGrid.Size = new System.Drawing.Size(312, 182);
            this.MarkersGrid.TabIndex = 0;
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
            this.uiGroupBox2.Controls.Add(this.EnemyMarker);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox2.Location = new System.Drawing.Point(2, 2);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(318, 46);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "Change enemy marker";
            // 
            // EnemyMarker
            // 
            this.EnemyMarker.AllowBarbarianViews = false;
            this.EnemyMarker.AutoUpdateMarkers = true;
            this.EnemyMarker.BackColor = System.Drawing.Color.Transparent;
            this.EnemyMarker.CanDeactivate = false;
            this.EnemyMarker.DefaultExtraMarkerColor = System.Drawing.Color.Transparent;
            this.EnemyMarker.DefaultMarkerColor = System.Drawing.Color.Red;
            this.EnemyMarker.Location = new System.Drawing.Point(6, 16);
            this.EnemyMarker.Margin = new System.Windows.Forms.Padding(0);
            this.EnemyMarker.Name = "EnemyMarker";
            this.EnemyMarker.Size = new System.Drawing.Size(307, 25);
            this.EnemyMarker.TabIndex = 0;
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.AbandonedMarker);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox3.Location = new System.Drawing.Point(2, 48);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(318, 49);
            this.uiGroupBox3.TabIndex = 1;
            this.uiGroupBox3.Text = "Change abandoned villages marker";
            // 
            // AbandonedMarker
            // 
            this.AbandonedMarker.AllowBarbarianViews = true;
            this.AbandonedMarker.AutoUpdateMarkers = true;
            this.AbandonedMarker.BackColor = System.Drawing.Color.Transparent;
            this.AbandonedMarker.CanDeactivate = false;
            this.AbandonedMarker.DefaultExtraMarkerColor = System.Drawing.Color.Transparent;
            this.AbandonedMarker.DefaultMarkerColor = System.Drawing.Color.Olive;
            this.AbandonedMarker.Location = new System.Drawing.Point(6, 16);
            this.AbandonedMarker.Margin = new System.Windows.Forms.Padding(0);
            this.AbandonedMarker.Name = "AbandonedMarker";
            this.AbandonedMarker.Size = new System.Drawing.Size(307, 25);
            this.AbandonedMarker.TabIndex = 0;
            // 
            // MarkersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uiGroupBox3);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.Name = "MarkersControl";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(322, 369);
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
