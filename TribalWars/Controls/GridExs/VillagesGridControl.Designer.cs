namespace TribalWars.Controls
{
    partial class VillagesGridControl
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
            Janus.Windows.GridEX.GridEXLayout GridExVillage_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VillagesGridControl));
            this.GridExVillage = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.GridExVillage)).BeginInit();
            this.SuspendLayout();
            // 
            // GridExVillage
            // 
            this.GridExVillage.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.GridExVillage.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.True;
            this.GridExVillage.AlternatingColors = true;
            this.GridExVillage.AutoEdit = true;
            this.GridExVillage.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><EmptyGridInfo>Click \'" +
    "Load Polygon Data\' to load all villages inside your polygons.</EmptyGridInfo></L" +
    "ocalizableData>";
            this.GridExVillage.ColumnAutoResize = true;
            this.GridExVillage.DataMember = "VILLAGE";
            GridExVillage_DesignTimeLayout.LayoutString = resources.GetString("GridExVillage_DesignTimeLayout.LayoutString");
            this.GridExVillage.DesignTimeLayout = GridExVillage_DesignTimeLayout;
            this.GridExVillage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridExVillage.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.GridExVillage.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.GridExVillage.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.GridExVillage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.GridExVillage.GroupByBoxVisible = false;
            this.GridExVillage.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True;
            this.GridExVillage.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.GridExVillage.Location = new System.Drawing.Point(0, 0);
            this.GridExVillage.Margin = new System.Windows.Forms.Padding(0);
            this.GridExVillage.Name = "GridExVillage";
            this.GridExVillage.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelectionSameTable;
            this.GridExVillage.SettingsKey = "PolygonVillage3";
            this.GridExVillage.Size = new System.Drawing.Size(450, 228);
            this.GridExVillage.TabIndex = 2;
            this.GridExVillage.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.GridExVillage.UseGroupRowSelector = true;
            this.GridExVillage.RowDoubleClick += new Janus.Windows.GridEX.RowActionEventHandler(this.GridExVillage_RowDoubleClick);
            this.GridExVillage.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.GridExVillage_FormattingRow);
            this.GridExVillage.LoadingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.GridExVillage_LoadingRow);
            this.GridExVillage.CurrentCellChanging += new Janus.Windows.GridEX.CurrentCellChangingEventHandler(this.GridExVillage_CurrentCellChanging);
            this.GridExVillage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GridExVillage_MouseClick);
            // 
            // VillagesGridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridExVillage);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "VillagesGridControl";
            this.Size = new System.Drawing.Size(450, 228);
            this.Load += new System.EventHandler(this.VillagesGridControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridExVillage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX GridExVillage;
    }
}
