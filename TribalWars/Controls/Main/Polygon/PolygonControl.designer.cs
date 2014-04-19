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
            Janus.Windows.GridEX.GridEXLayout GridExPolygon_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PolygonControl));
            this.ButtonGenerate = new Janus.Windows.EditControls.UIButton();
            this.GridExPolygon = new Janus.Windows.GridEX.GridEX();
            this.polygonDataSet1 = new TribalWars.Controls.PolygonDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.GridExPolygon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.polygonDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonGenerate
            // 
            this.ButtonGenerate.Location = new System.Drawing.Point(3, 3);
            this.ButtonGenerate.Name = "ButtonGenerate";
            this.ButtonGenerate.Size = new System.Drawing.Size(85, 21);
            this.ButtonGenerate.TabIndex = 1;
            this.ButtonGenerate.Text = "&Generate";
            this.ButtonGenerate.Click += new System.EventHandler(this.ButtonGenerate_Click);
            // 
            // GridExPolygon
            // 
            this.GridExPolygon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridExPolygon.ColumnAutoResize = true;
            this.GridExPolygon.DataMember = "VILLAGE";
            this.GridExPolygon.DataSource = this.polygonDataSet1;
            GridExPolygon_DesignTimeLayout.LayoutString = resources.GetString("GridExPolygon_DesignTimeLayout.LayoutString");
            this.GridExPolygon.DesignTimeLayout = GridExPolygon_DesignTimeLayout;
            this.GridExPolygon.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.GridExPolygon.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True;
            this.GridExPolygon.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.GridExPolygon.Location = new System.Drawing.Point(94, 3);
            this.GridExPolygon.Name = "GridExPolygon";
            this.GridExPolygon.Size = new System.Drawing.Size(732, 357);
            this.GridExPolygon.TabIndex = 0;
            this.GridExPolygon.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.GridExPolygon_FormattingRow);
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

    }
}
