using TribalWars.Controls.Common;

namespace TribalWars.Controls.AttackPlan
{
    partial class MapDistanceControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Coords = new VillagePlayerTribeFinderTextBox();
            this.Close = new System.Windows.Forms.LinkLabel();
            this._Player = new System.Windows.Forms.Label();
            this._Village = new System.Windows.Forms.Label();
            this._Tribe = new System.Windows.Forms.Label();
            this.DistanceContainer = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.InfoText = new System.Windows.Forms.ComboBox();
            this.Date = new TimeConverterControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.DistanceContainer, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(274, 431);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Coords);
            this.panel1.Controls.Add(this.Close);
            this.panel1.Controls.Add(this._Player);
            this.panel1.Controls.Add(this._Village);
            this.panel1.Controls.Add(this._Tribe);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 40);
            this.panel1.TabIndex = 0;
            // 
            // Coords
            // 
            this.Coords.BackColor = System.Drawing.Color.White;
            this.Coords.Location = new System.Drawing.Point(6, 3);
            this.Coords.Name = "Coords";
            this.Coords.Size = new System.Drawing.Size(50, 20);
            this.Coords.TabIndex = 14;
            // 
            // Close
            // 
            this.Close.ActiveLinkColor = System.Drawing.Color.Black;
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Close.AutoSize = true;
            this.Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Close.DisabledLinkColor = System.Drawing.Color.Black;
            this.Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close.LinkColor = System.Drawing.Color.Black;
            this.Close.Location = new System.Drawing.Point(256, 3);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(15, 16);
            this.Close.TabIndex = 13;
            this.Close.TabStop = true;
            this.Close.Text = "x";
            this.Close.VisitedLinkColor = System.Drawing.Color.Black;
            this.Close.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Close_LinkClicked);
            // 
            // _Player
            // 
            this._Player.AutoSize = true;
            this._Player.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Player.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this._Player.Location = new System.Drawing.Point(3, 23);
            this._Player.Name = "_Player";
            this._Player.Size = new System.Drawing.Size(53, 16);
            this._Player.TabIndex = 11;
            this._Player.Text = "Player";
            this._Player.DoubleClick += new System.EventHandler(this.Player_DoubleClick);
            // 
            // _Village
            // 
            this._Village.AutoSize = true;
            this._Village.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Village.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this._Village.Location = new System.Drawing.Point(57, 4);
            this._Village.Name = "_Village";
            this._Village.Size = new System.Drawing.Size(95, 16);
            this._Village.TabIndex = 10;
            this._Village.Text = "Villagename";
            // 
            // _Tribe
            // 
            this._Tribe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._Tribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Tribe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this._Tribe.Location = new System.Drawing.Point(201, 23);
            this._Tribe.Name = "_Tribe";
            this._Tribe.Size = new System.Drawing.Size(70, 17);
            this._Tribe.TabIndex = 12;
            this._Tribe.Text = "Tag";
            this._Tribe.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._Tribe.DoubleClick += new System.EventHandler(this.Tribe_DoubleClick);
            // 
            // DistanceContainer
            // 
            this.DistanceContainer.AutoScroll = true;
            this.DistanceContainer.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.DistanceContainer.ColumnCount = 1;
            this.DistanceContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.DistanceContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DistanceContainer.Location = new System.Drawing.Point(0, 70);
            this.DistanceContainer.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.DistanceContainer.Name = "DistanceContainer";
            this.DistanceContainer.RowCount = 1;
            this.DistanceContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 363F));
            this.DistanceContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 363F));
            this.DistanceContainer.Size = new System.Drawing.Size(274, 360);
            this.DistanceContainer.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.InfoText);
            this.panel3.Controls.Add(this.Date);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 40);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(274, 30);
            this.panel3.TabIndex = 4;
            // 
            // InfoText
            // 
            this.InfoText.FormattingEnabled = true;
            this.InfoText.Items.AddRange(new object[] {
            "BEFORE",
            "EXACTLY AT",
            "AFTER"});
            this.InfoText.Location = new System.Drawing.Point(1, 4);
            this.InfoText.Name = "InfoText";
            this.InfoText.Size = new System.Drawing.Size(106, 21);
            this.InfoText.TabIndex = 2;
            this.InfoText.Text = "EXACTLY AT";
            // 
            // Date
            // 
            this.Date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Date.BackColor = System.Drawing.Color.Transparent;
            this.Date.CustomFormat = "MMM, dd yyyy HH:mm:ss";
            this.Date.Location = new System.Drawing.Point(107, 4);
            this.Date.Margin = new System.Windows.Forms.Padding(0);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(165, 25);
            this.Date.TabIndex = 1;
            this.Date.Value = new System.DateTime(2008, 4, 10, 0, 26, 44, 906);
            // 
            // MapDistanceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MapDistanceControl";
            this.Size = new System.Drawing.Size(274, 431);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label _Village;
        private System.Windows.Forms.Label _Tribe;
        private System.Windows.Forms.Label _Player;
        private System.Windows.Forms.TableLayoutPanel DistanceContainer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.LinkLabel Close;
        private TimeConverterControl Date;
        private System.Windows.Forms.ComboBox InfoText;
        private VillagePlayerTribeFinderTextBox Coords;
    }
}
