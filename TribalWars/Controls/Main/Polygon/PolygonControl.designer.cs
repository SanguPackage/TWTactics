namespace TribalWars.Controls.Main.Polygon
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PolygonControl));
            Janus.Windows.GridEX.GridEXLayout GridExPolygon_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.ButtonGenerate = new Janus.Windows.EditControls.UIButton();
            this.LoadPolygonData = new Janus.Windows.EditControls.UIButton();
            this.VisibleImageList = new System.Windows.Forms.ImageList(this.components);
            this.GridExPolygonShowFieldChooser = new Janus.Windows.EditControls.UIButton();
            this.GridExPolygon = new Janus.Windows.GridEX.GridEX();
            this.polygonDataSet1 = new TribalWars.Controls.PolygonDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.GridExPolygon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.polygonDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonGenerate
            // 
            this.ButtonGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonGenerate.Location = new System.Drawing.Point(3, 54);
            this.ButtonGenerate.Name = "ButtonGenerate";
            this.ButtonGenerate.Size = new System.Drawing.Size(85, 45);
            this.ButtonGenerate.TabIndex = 1;
            this.ButtonGenerate.Text = "&Generate BBCodes";
            this.ButtonGenerate.ToolTipText = "Put BBCodes for all checked visible village rows to the clipboard.";
            this.ButtonGenerate.Click += new System.EventHandler(this.ButtonGenerate_Click);
            // 
            // LoadPolygonData
            // 
            this.LoadPolygonData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadPolygonData.Location = new System.Drawing.Point(3, 3);
            this.LoadPolygonData.Name = "LoadPolygonData";
            this.LoadPolygonData.Size = new System.Drawing.Size(85, 45);
            this.LoadPolygonData.TabIndex = 1;
            this.LoadPolygonData.Text = "Load Polygon Data";
            this.LoadPolygonData.ToolTipText = "Load (or reload) all villages in your drawn polygons.";
            this.LoadPolygonData.Click += new System.EventHandler(this.LoadPolygonData_Click);
            // 
            // VisibleImageList
            // 
            this.VisibleImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("VisibleImageList.ImageStream")));
            this.VisibleImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.VisibleImageList.Images.SetKeyName(0, "Visible");
            // 
            // GridExPolygonShowFieldChooser
            // 
            this.GridExPolygonShowFieldChooser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GridExPolygonShowFieldChooser.Location = new System.Drawing.Point(691, 12);
            this.GridExPolygonShowFieldChooser.Name = "GridExPolygonShowFieldChooser";
            this.GridExPolygonShowFieldChooser.Size = new System.Drawing.Size(110, 23);
            this.GridExPolygonShowFieldChooser.TabIndex = 2;
            this.GridExPolygonShowFieldChooser.TabStop = false;
            this.GridExPolygonShowFieldChooser.Text = "Show/Hide Columns";
            this.GridExPolygonShowFieldChooser.Click += new System.EventHandler(this.GridExPolygonShowFieldChooser_Click);
            // 
            // GridExPolygon
            // 
            this.GridExPolygon.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.GridExPolygon.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.True;
            this.GridExPolygon.AlternatingColors = true;
            this.GridExPolygon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridExPolygon.AutoEdit = true;
            this.GridExPolygon.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><EmptyGridInfo>Click \'" +
    "Load Polygon Data\' to load all villages inside your polygons.</EmptyGridInfo></L" +
    "ocalizableData>";
            this.GridExPolygon.ColumnAutoResize = true;
            this.GridExPolygon.DataMember = "VILLAGE";
            this.GridExPolygon.DataSource = this.polygonDataSet1;
            GridExPolygon_DesignTimeLayout.LayoutString = resources.GetString("GridExPolygon_DesignTimeLayout.LayoutString");
            this.GridExPolygon.DesignTimeLayout = GridExPolygon_DesignTimeLayout;
            this.GridExPolygon.DynamicFiltering = true;
            this.GridExPolygon.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.GridExPolygon.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.GridExPolygon.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.GridExPolygon.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True;
            this.GridExPolygon.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.GridExPolygon.ImageList = this.VisibleImageList;
            this.GridExPolygon.Location = new System.Drawing.Point(94, 3);
            this.GridExPolygon.Name = "GridExPolygon";
            this.GridExPolygon.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelectionSameTable;
            this.GridExPolygon.Size = new System.Drawing.Size(732, 357);
            this.GridExPolygon.TabIndex = 0;
            this.GridExPolygon.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.GridExPolygon.UseGroupRowSelector = true;
            this.GridExPolygon.RowCheckStateChanged += new Janus.Windows.GridEX.RowCheckStateChangeEventHandler(this.GridExPolygon_RowCheckStateChanged);
            this.GridExPolygon.RowDoubleClick += new Janus.Windows.GridEX.RowActionEventHandler(this.GridExPolygon_RowDoubleClick);
            this.GridExPolygon.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.GridExPolygon_FormattingRow);
            this.GridExPolygon.GroupsChanging += new Janus.Windows.GridEX.GroupsChangingEventHandler(this.GridExPolygon_GroupsChanging);
            this.GridExPolygon.CurrentCellChanging += new Janus.Windows.GridEX.CurrentCellChangingEventHandler(this.GridExPolygon_CurrentCellChanging);
            // 
            // polygonDataSet1
            // 
            this.polygonDataSet1.DataSetName = "PolygonDataSet";
            this.polygonDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PolygonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.GridExPolygonShowFieldChooser);
            this.Controls.Add(this.LoadPolygonData);
            this.Controls.Add(this.ButtonGenerate);
            this.Controls.Add(this.GridExPolygon);
            this.Name = "PolygonControl";
            this.Size = new System.Drawing.Size(829, 363);
            ((System.ComponentModel.ISupportInitialize)(this.GridExPolygon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.polygonDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX GridExPolygon;
        private PolygonDataSet polygonDataSet1;
        private Janus.Windows.EditControls.UIButton ButtonGenerate;
        private Janus.Windows.EditControls.UIButton LoadPolygonData;
        private System.Windows.Forms.ImageList VisibleImageList;
        private Janus.Windows.EditControls.UIButton GridExPolygonShowFieldChooser;

    }
}
