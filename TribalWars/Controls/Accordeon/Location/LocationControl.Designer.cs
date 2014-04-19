using TribalWars.Controls.Common;

namespace TribalWars.Controls.Accordeon.Location
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
            System.Windows.Forms.GroupBox groupBox1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationControl));
            this.cmdCenterKingdom = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtK = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._locationFinderControl1 = new TribalWars.Controls.Accordeon.Location.LocationFinderControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdDraw = new System.Windows.Forms.Button();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.StripHome = new System.Windows.Forms.ToolStripButton();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.You = new TribalWars.Controls.Common.VillagePlayerTribeFinderTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LocationHistory = new TribalWars.Controls.Accordeon.Location.LocationList();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            groupBox1.Controls.Add(this.cmdCenterKingdom);
            groupBox1.Controls.Add(this.label5);
            groupBox1.Controls.Add(this.txtK);
            groupBox1.Location = new System.Drawing.Point(97, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(126, 42);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Center on continent";
            // 
            // cmdCenterKingdom
            // 
            this.cmdCenterKingdom.Location = new System.Drawing.Point(70, 14);
            this.cmdCenterKingdom.Name = "cmdCenterKingdom";
            this.cmdCenterKingdom.Size = new System.Drawing.Size(51, 23);
            this.cmdCenterKingdom.TabIndex = 2;
            this.cmdCenterKingdom.Text = "Center";
            this.cmdCenterKingdom.UseVisualStyleBackColor = true;
            this.cmdCenterKingdom.Click += new System.EventHandler(this.cmdCenterKingdom_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "C";
            // 
            // txtK
            // 
            this.txtK.Location = new System.Drawing.Point(18, 16);
            this.txtK.Name = "txtK";
            this.txtK.Size = new System.Drawing.Size(46, 20);
            this.txtK.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this._locationFinderControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(298, 359);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _locationFinderControl1
            // 
            this._locationFinderControl1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this._locationFinderControl1, 2);
            this._locationFinderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._locationFinderControl1.Location = new System.Drawing.Point(0, 175);
            this._locationFinderControl1.Margin = new System.Windows.Forms.Padding(0);
            this._locationFinderControl1.Name = "_locationFinderControl1";
            this._locationFinderControl1.Size = new System.Drawing.Size(298, 184);
            this._locationFinderControl1.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.cmdDraw);
            this.groupBox2.Controls.Add(this.txtWidth);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtZ);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtY);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtX);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(3, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(62, 144);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Location";
            // 
            // cmdDraw
            // 
            this.cmdDraw.Image = ((System.Drawing.Image)(resources.GetObject("cmdDraw.Image")));
            this.cmdDraw.Location = new System.Drawing.Point(8, 103);
            this.cmdDraw.Margin = new System.Windows.Forms.Padding(0);
            this.cmdDraw.Name = "cmdDraw";
            this.cmdDraw.Size = new System.Drawing.Size(48, 34);
            this.cmdDraw.TabIndex = 4;
            this.cmdDraw.UseVisualStyleBackColor = true;
            this.cmdDraw.Click += new System.EventHandler(this.cmdDraw_Click);
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(21, 80);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(32, 20);
            this.txtWidth.TabIndex = 3;
            this.txtWidth.Click += new System.EventHandler(this.txtWidth_Click);
            this.txtWidth.Enter += new System.EventHandler(this.txtWidth_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "w";
            // 
            // txtZ
            // 
            this.txtZ.Location = new System.Drawing.Point(21, 58);
            this.txtZ.Name = "txtZ";
            this.txtZ.Size = new System.Drawing.Size(32, 20);
            this.txtZ.TabIndex = 2;
            this.txtZ.Click += new System.EventHandler(this.txtZ_Click);
            this.txtZ.Enter += new System.EventHandler(this.txtZ_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "z";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(21, 36);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(32, 20);
            this.txtY.TabIndex = 1;
            this.txtY.Click += new System.EventHandler(this.txtY_Click);
            this.txtY.Enter += new System.EventHandler(this.txtY_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "y";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(21, 14);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(32, 20);
            this.txtX.TabIndex = 0;
            this.txtX.Click += new System.EventHandler(this.txtX_Click);
            this.txtX.Enter += new System.EventHandler(this.txtX_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "x";
            // 
            // toolStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 2);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripHome,
            this.ProgressBar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(298, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // StripHome
            // 
            this.StripHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StripHome.Image = ((System.Drawing.Image)(resources.GetObject("StripHome.Image")));
            this.StripHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StripHome.Name = "StripHome";
            this.StripHome.Size = new System.Drawing.Size(23, 22);
            this.StripHome.Text = "Set home location";
            this.StripHome.Click += new System.EventHandler(this.StripHome_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(0, 22);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(groupBox1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Location = new System.Drawing.Point(70, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(226, 150);
            this.panel1.TabIndex = 6;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.You);
            this.groupBox4.Location = new System.Drawing.Point(97, 52);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(121, 47);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Change active player";
            // 
            // You
            // 
            this.You.AllowPlayer = true;
            this.You.AllowVillage = false;
            this.You.BackColor = System.Drawing.Color.Red;
            this.You.GameLocation = null;
            this.You.Location = new System.Drawing.Point(6, 19);
            this.You.Name = "You";
            this.You.Size = new System.Drawing.Size(109, 20);
            this.You.TabIndex = 11;
            this.You.PlayerSelected += new System.EventHandler<TribalWars.Data.Events.PlayerEventArgs>(this.You_PlayerSelected);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LocationHistory);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(87, 144);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "History";
            // 
            // LocationHistory
            // 
            this.LocationHistory.BackColor = System.Drawing.Color.Transparent;
            this.LocationHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LocationHistory.Location = new System.Drawing.Point(3, 16);
            this.LocationHistory.Margin = new System.Windows.Forms.Padding(0);
            this.LocationHistory.Name = "LocationHistory";
            this.LocationHistory.Size = new System.Drawing.Size(81, 125);
            this.LocationHistory.TabIndex = 0;
            // 
            // LocationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LocationControl";
            this.Size = new System.Drawing.Size(298, 359);
            this.Load += new System.EventHandler(this.LocationControl_Load);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdDraw;
        private System.Windows.Forms.Button cmdCenterKingdom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private LocationFinderControl _locationFinderControl1;
        private System.Windows.Forms.ToolStripButton StripHome;
        private System.Windows.Forms.GroupBox groupBox3;
        private LocationList LocationHistory;
        private VillagePlayerTribeFinderTextBox You;
        private System.Windows.Forms.GroupBox groupBox4;

    }
}
