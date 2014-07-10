using TribalWars.Controls.Finders;
using TribalWars.Worlds.Events.Impls;

namespace TribalWars.Controls.AccordeonLocation
{
    partial class LocationControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._locationFinderControl1 = new TribalWars.Controls.AccordeonLocation.LocationFinderControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.GoHome = new Janus.Windows.EditControls.UIButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this._locationFinderControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(304, 324);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _locationFinderControl1
            // 
            this._locationFinderControl1.BackColor = System.Drawing.Color.Transparent;
            this._locationFinderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._locationFinderControl1.Location = new System.Drawing.Point(2, 50);
            this._locationFinderControl1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this._locationFinderControl1.Name = "_locationFinderControl1";
            this._locationFinderControl1.Size = new System.Drawing.Size(300, 272);
            this._locationFinderControl1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(304, 50);
            this.panel1.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.GoHome);
            this.groupBox2.Location = new System.Drawing.Point(234, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(66, 43);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Set home";
            // 
            // GoHome
            // 
            this.GoHome.Image = ((System.Drawing.Image)(resources.GetObject("GoHome.Image")));
            this.GoHome.Location = new System.Drawing.Point(6, 14);
            this.GoHome.Name = "GoHome";
            this.GoHome.Size = new System.Drawing.Size(54, 23);
            this.GoHome.TabIndex = 9;
            this.GoHome.ToolTipText = "Set new home location";
            this.GoHome.Click += new System.EventHandler(this.GoHome_Click);
            // 
            // LocationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LocationControl";
            this.Size = new System.Drawing.Size(304, 324);
            this.Load += new System.EventHandler(this.LocationControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private LocationFinderControl _locationFinderControl1;
        private Janus.Windows.EditControls.UIButton GoHome;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}
