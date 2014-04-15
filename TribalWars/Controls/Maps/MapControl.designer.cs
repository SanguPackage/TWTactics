namespace TribalWars.Controls.Maps
{
    partial class MapControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapControl));
            this.TableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.YRuler = new System.Windows.Forms.Panel();
            this.XRuler = new System.Windows.Forms.Panel();
            this.MapToolStripRuler = new System.Windows.Forms.PictureBox();
            this.MapPicture = new TribalWars.Controls.Maps.ScrollableMapControl();
            this.VillageTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.PickColor = new System.Windows.Forms.ColorDialog();
            this.MapLinesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MapToolStripRosterContinent = new System.Windows.Forms.ToolStripMenuItem();
            this.MapToolStripRosterProvince = new System.Windows.Forms.ToolStripMenuItem();
            this.MapHideMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.abandonedVillagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.villagesWithoutTribeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.villagesWithTribeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapToolStripRuler)).BeginInit();
            this.MapLinesMenu.SuspendLayout();
            this.MapHideMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayout
            // 
            this.TableLayout.ColumnCount = 2;
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayout.Controls.Add(this.YRuler, 0, 1);
            this.TableLayout.Controls.Add(this.XRuler, 1, 0);
            this.TableLayout.Controls.Add(this.MapToolStripRuler, 0, 0);
            this.TableLayout.Controls.Add(this.MapPicture, 1, 1);
            this.TableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayout.Location = new System.Drawing.Point(0, 0);
            this.TableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.TableLayout.Name = "TableLayout";
            this.TableLayout.RowCount = 2;
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayout.Size = new System.Drawing.Size(121, 117);
            this.TableLayout.TabIndex = 6;
            // 
            // YRuler
            // 
            this.YRuler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YRuler.Location = new System.Drawing.Point(0, 20);
            this.YRuler.Margin = new System.Windows.Forms.Padding(0);
            this.YRuler.Name = "YRuler";
            this.YRuler.Size = new System.Drawing.Size(20, 111);
            this.YRuler.TabIndex = 7;
            this.YRuler.Paint += new System.Windows.Forms.PaintEventHandler(this.YRuler_Paint);
            // 
            // XRuler
            // 
            this.XRuler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XRuler.Location = new System.Drawing.Point(20, 0);
            this.XRuler.Margin = new System.Windows.Forms.Padding(0);
            this.XRuler.Name = "XRuler";
            this.XRuler.Size = new System.Drawing.Size(122, 20);
            this.XRuler.TabIndex = 6;
            this.XRuler.Paint += new System.Windows.Forms.PaintEventHandler(this.XRuler_Paint);
            // 
            // MapToolStripRuler
            // 
            this.MapToolStripRuler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapToolStripRuler.Image = ((System.Drawing.Image)(resources.GetObject("MapToolStripRuler.Image")));
            this.MapToolStripRuler.Location = new System.Drawing.Point(3, 3);
            this.MapToolStripRuler.Name = "MapToolStripRuler";
            this.MapToolStripRuler.Size = new System.Drawing.Size(14, 14);
            this.MapToolStripRuler.TabIndex = 0;
            this.MapToolStripRuler.TabStop = false;
            // 
            // MapPicture
            // 
            this.MapPicture.BackColor = System.Drawing.Color.Green;
            this.MapPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapPicture.Location = new System.Drawing.Point(20, 20);
            this.MapPicture.Margin = new System.Windows.Forms.Padding(0);
            this.MapPicture.Name = "MapPicture";
            this.MapPicture.Size = new System.Drawing.Size(122, 111);
            this.MapPicture.TabIndex = 8;
            // 
            // VillageTooltip
            // 
            this.VillageTooltip.IsBalloon = true;
            // 
            // MapLinesMenu
            // 
            this.MapLinesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MapToolStripRosterContinent,
            this.MapToolStripRosterProvince});
            this.MapLinesMenu.Name = "MapLinesMenu";
            this.MapLinesMenu.Size = new System.Drawing.Size(133, 48);
            // 
            // MapToolStripRosterContinent
            // 
            this.MapToolStripRosterContinent.Checked = true;
            this.MapToolStripRosterContinent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MapToolStripRosterContinent.Name = "MapToolStripRosterContinent";
            this.MapToolStripRosterContinent.Size = new System.Drawing.Size(132, 22);
            this.MapToolStripRosterContinent.Text = "&Continent";
            // 
            // MapToolStripRosterProvince
            // 
            this.MapToolStripRosterProvince.Name = "MapToolStripRosterProvince";
            this.MapToolStripRosterProvince.Size = new System.Drawing.Size(132, 22);
            this.MapToolStripRosterProvince.Text = "&Province";
            // 
            // MapHideMenu
            // 
            this.MapHideMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abandonedVillagesToolStripMenuItem,
            this.villagesWithoutTribeToolStripMenuItem,
            this.villagesWithTribeToolStripMenuItem});
            this.MapHideMenu.Name = "MapHideMenu";
            this.MapHideMenu.Size = new System.Drawing.Size(185, 70);
            // 
            // abandonedVillagesToolStripMenuItem
            // 
            this.abandonedVillagesToolStripMenuItem.Name = "abandonedVillagesToolStripMenuItem";
            this.abandonedVillagesToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.abandonedVillagesToolStripMenuItem.Text = "Abandoned villages";
            // 
            // villagesWithoutTribeToolStripMenuItem
            // 
            this.villagesWithoutTribeToolStripMenuItem.Name = "villagesWithoutTribeToolStripMenuItem";
            this.villagesWithoutTribeToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.villagesWithoutTribeToolStripMenuItem.Text = "Villages without tribe";
            // 
            // villagesWithTribeToolStripMenuItem
            // 
            this.villagesWithTribeToolStripMenuItem.Name = "villagesWithTribeToolStripMenuItem";
            this.villagesWithTribeToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.villagesWithTribeToolStripMenuItem.Text = "Villages with tribe";
            // 
            // MapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.TableLayout);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MapControl";
            this.Size = new System.Drawing.Size(121, 117);
            this.TableLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MapToolStripRuler)).EndInit();
            this.MapLinesMenu.ResumeLayout(false);
            this.MapHideMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip VillageTooltip;
        private System.Windows.Forms.ColorDialog PickColor;
        private System.Windows.Forms.ContextMenuStrip MapLinesMenu;
        private System.Windows.Forms.ToolStripMenuItem MapToolStripRosterContinent;
        private System.Windows.Forms.ToolStripMenuItem MapToolStripRosterProvince;
        private System.Windows.Forms.ContextMenuStrip MapHideMenu;
        private System.Windows.Forms.ToolStripMenuItem abandonedVillagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem villagesWithoutTribeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem villagesWithTribeToolStripMenuItem;
        protected System.Windows.Forms.TableLayoutPanel TableLayout;
        protected System.Windows.Forms.Panel YRuler;
        protected System.Windows.Forms.PictureBox MapToolStripRuler;
        protected System.Windows.Forms.Panel XRuler;
        protected ScrollableMapControl MapPicture;
    }
}
