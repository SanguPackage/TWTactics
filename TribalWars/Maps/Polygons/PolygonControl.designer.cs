namespace TribalWars.Maps.Polygons
{
    partial class PolygonControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PolygonControl));
			Janus.Windows.GridEX.GridEXLayout GridExVillage_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
			Janus.Windows.GridEX.GridEXLayout GridExPolygon_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
			this.ButtonGenerate = new Janus.Windows.EditControls.UIButton();
			this.LoadPolygonData = new Janus.Windows.EditControls.UIButton();
			this.GridExVillageShowFieldChooser = new Janus.Windows.EditControls.UIButton();
			this.GridExVillage = new Janus.Windows.GridEX.GridEX();
			this.polygonDataSet1 = new TribalWars.Maps.Polygons.PolygonDataSet();
			this.ModusPolygon = new Janus.Windows.EditControls.UIButton();
			this.GeneratorActions = new Janus.Windows.EditControls.UIGroupBox();
			this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
			this.ModusVillage = new Janus.Windows.EditControls.UIButton();
			this.GridExPolygon = new Janus.Windows.GridEX.GridEX();
			this.CurrentModusGroupbox = new Janus.Windows.EditControls.UIGroupBox();
			((System.ComponentModel.ISupportInitialize)(this.GridExVillage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.polygonDataSet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GeneratorActions)).BeginInit();
			this.GeneratorActions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
			this.uiGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GridExPolygon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CurrentModusGroupbox)).BeginInit();
			this.CurrentModusGroupbox.SuspendLayout();
			this.SuspendLayout();
			// 
			// ButtonGenerate
			// 
			resources.ApplyResources(this.ButtonGenerate, "ButtonGenerate");
			this.ButtonGenerate.Name = "ButtonGenerate";
			this.ButtonGenerate.Click += new System.EventHandler(this.ButtonGenerate_Click);
			// 
			// LoadPolygonData
			// 
			resources.ApplyResources(this.LoadPolygonData, "LoadPolygonData");
			this.LoadPolygonData.Name = "LoadPolygonData";
			this.LoadPolygonData.Click += new System.EventHandler(this.LoadPolygonData_Click);
			// 
			// GridExVillageShowFieldChooser
			// 
			resources.ApplyResources(this.GridExVillageShowFieldChooser, "GridExVillageShowFieldChooser");
			this.GridExVillageShowFieldChooser.Name = "GridExVillageShowFieldChooser";
			this.GridExVillageShowFieldChooser.TabStop = false;
			this.GridExVillageShowFieldChooser.Click += new System.EventHandler(this.GridExVillageShowFieldChooser_Click);
			// 
			// GridExVillage
			// 
			resources.ApplyResources(this.GridExVillage, "GridExVillage");
			this.GridExVillage.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
			this.GridExVillage.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.True;
			this.GridExVillage.AlternatingColors = true;
			this.GridExVillage.AutoEdit = true;
			this.GridExVillage.ColumnAutoResize = true;
			this.GridExVillage.DataMember = "VILLAGE";
			this.GridExVillage.DataSource = this.polygonDataSet1;
			resources.ApplyResources(GridExVillage_DesignTimeLayout, "GridExVillage_DesignTimeLayout");
			this.GridExVillage.DesignTimeLayout = GridExVillage_DesignTimeLayout;
			this.GridExVillage.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
			this.GridExVillage.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
			this.GridExVillage.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
			this.GridExVillage.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True;
			this.GridExVillage.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
			this.GridExVillage.Name = "GridExVillage";
			this.GridExVillage.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelectionSameTable;
			this.GridExVillage.SettingsKey = "PolygonVillage3";
			this.GridExVillage.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
			this.GridExVillage.UseGroupRowSelector = true;
			this.GridExVillage.RowCheckStateChanged += new Janus.Windows.GridEX.RowCheckStateChangeEventHandler(this.GridExVillage_RowCheckStateChanged);
			this.GridExVillage.RowDoubleClick += new Janus.Windows.GridEX.RowActionEventHandler(this.GridExVillage_RowDoubleClick);
			this.GridExVillage.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.GridExVillage_FormattingRow);
			this.GridExVillage.GroupsChanging += new Janus.Windows.GridEX.GroupsChangingEventHandler(this.GridExVillage_GroupsChanging);
			this.GridExVillage.CurrentCellChanging += new Janus.Windows.GridEX.CurrentCellChangingEventHandler(this.GridExVillage_CurrentCellChanging);
			this.GridExVillage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridExVillage_KeyDown);
			this.GridExVillage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GridExVillage_MouseClick);
			// 
			// polygonDataSet1
			// 
			this.polygonDataSet1.DataSetName = "PolygonDataSet";
			this.polygonDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// ModusPolygon
			// 
			resources.ApplyResources(this.ModusPolygon, "ModusPolygon");
			this.ModusPolygon.Name = "ModusPolygon";
			this.ModusPolygon.Click += new System.EventHandler(this.ModusPolygon_Click);
			// 
			// GeneratorActions
			// 
			resources.ApplyResources(this.GeneratorActions, "GeneratorActions");
			this.GeneratorActions.Controls.Add(this.ButtonGenerate);
			this.GeneratorActions.Controls.Add(this.LoadPolygonData);
			this.GeneratorActions.Name = "GeneratorActions";
			// 
			// uiGroupBox1
			// 
			resources.ApplyResources(this.uiGroupBox1, "uiGroupBox1");
			this.uiGroupBox1.Controls.Add(this.ModusVillage);
			this.uiGroupBox1.Controls.Add(this.ModusPolygon);
			this.uiGroupBox1.Name = "uiGroupBox1";
			// 
			// ModusVillage
			// 
			resources.ApplyResources(this.ModusVillage, "ModusVillage");
			this.ModusVillage.Name = "ModusVillage";
			this.ModusVillage.Click += new System.EventHandler(this.ModusVillage_Click);
			// 
			// GridExPolygon
			// 
			resources.ApplyResources(this.GridExPolygon, "GridExPolygon");
			this.GridExPolygon.AlternatingColors = true;
			resources.ApplyResources(GridExPolygon_DesignTimeLayout, "GridExPolygon_DesignTimeLayout");
			this.GridExPolygon.DesignTimeLayout = GridExPolygon_DesignTimeLayout;
			this.GridExPolygon.GroupByBoxVisible = false;
			this.GridExPolygon.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
			this.GridExPolygon.Name = "GridExPolygon";
			this.GridExPolygon.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
			this.GridExPolygon.SettingsKey = "PolygonManage";
			// 
			// CurrentModusGroupbox
			// 
			resources.ApplyResources(this.CurrentModusGroupbox, "CurrentModusGroupbox");
			this.CurrentModusGroupbox.Controls.Add(this.GridExVillageShowFieldChooser);
			this.CurrentModusGroupbox.Controls.Add(this.GridExVillage);
			this.CurrentModusGroupbox.Controls.Add(this.GridExPolygon);
			this.CurrentModusGroupbox.Name = "CurrentModusGroupbox";
			// 
			// PolygonControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.CurrentModusGroupbox);
			this.Controls.Add(this.uiGroupBox1);
			this.Controls.Add(this.GeneratorActions);
			this.Name = "PolygonControl";
			this.Load += new System.EventHandler(this.PolygonControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.GridExVillage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.polygonDataSet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GeneratorActions)).EndInit();
			this.GeneratorActions.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
			this.uiGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GridExPolygon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CurrentModusGroupbox)).EndInit();
			this.CurrentModusGroupbox.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX GridExVillage;
        private PolygonDataSet polygonDataSet1;
        private Janus.Windows.EditControls.UIButton ButtonGenerate;
        private Janus.Windows.EditControls.UIButton LoadPolygonData;
        private Janus.Windows.EditControls.UIButton GridExVillageShowFieldChooser;
        private Janus.Windows.EditControls.UIButton ModusPolygon;
        private Janus.Windows.EditControls.UIGroupBox GeneratorActions;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIButton ModusVillage;
        private Janus.Windows.GridEX.GridEX GridExPolygon;
        private Janus.Windows.EditControls.UIGroupBox CurrentModusGroupbox;

    }
}
