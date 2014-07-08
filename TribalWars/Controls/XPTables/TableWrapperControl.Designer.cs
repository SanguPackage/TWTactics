namespace TribalWars.Controls.XPTables
{
    partial class TableWrapperControl
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
            XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder1 = new XPTable.Models.DataSourceColumnBinder();
            this.Table = new XPTable.Models.Table();
            this.TableRows = new XPTable.Models.TableModel();
            ((System.ComponentModel.ISupportInitialize)(this.Table)).BeginInit();
            this.SuspendLayout();
            // 
            // Table
            // 
            this.Table.BorderColor = System.Drawing.Color.Black;
            this.Table.DataMember = null;
            this.Table.DataSourceColumnBinder = dataSourceColumnBinder1;
            this.Table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Table.EnableToolTips = true;
            this.Table.FullRowSelect = true;
            this.Table.GridLines = XPTable.Models.GridLines.Both;
            this.Table.GridLineStyle = XPTable.Models.GridLineStyle.Dash;
            this.Table.Location = new System.Drawing.Point(0, 0);
            this.Table.Margin = new System.Windows.Forms.Padding(0);
            this.Table.MultiSelect = true;
            this.Table.Name = "Table";
            this.Table.ShowSelectionRectangle = false;
            this.Table.Size = new System.Drawing.Size(150, 150);
            this.Table.TabIndex = 0;
            this.Table.TableModel = this.TableRows;
            this.Table.Text = "table1";
            this.Table.ToolTipShowAlways = true;
            this.Table.UnfocusedBorderColor = System.Drawing.Color.Black;
            this.Table.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TableControl_MouseClick);
            this.Table.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TableControl_MouseDoubleClick);
            // 
            // TableWrapperControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Table);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TableWrapperControl";
            ((System.ComponentModel.ISupportInitialize)(this.Table)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private XPTable.Models.Table Table;
        private XPTable.Models.TableModel TableRows;
    }
}
