namespace TribalWars.Controls
{
    partial class MapDistanceCollectionControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapDistanceCollectionControl));
            this.Collection = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AttackDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.AttackAllDropDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdClipboard = new System.Windows.Forms.ToolStripDropDownButton();
            this.cmdClipboardText = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdClipboardBBCode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdClipboardTextAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdClipboardBBCodeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSort = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdClear = new System.Windows.Forms.ToolStripButton();
            this.cmdAddVillage = new System.Windows.Forms.ToolStripButton();
            this.cmdAddTarget = new System.Windows.Forms.ToolStripButton();
            this.cmdFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdSound = new System.Windows.Forms.ToolStripButton();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.VillageInput = new TribalWars.Controls.ToolStripVillageTextBox();
            this.UnitInput = new TribalWars.Controls.ToolStripUnitsImageCombobox();
            this.AllContainer = new System.Windows.Forms.Panel();
            this.Collection.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Collection
            // 
            this.Collection.ColumnCount = 1;
            this.Collection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Collection.Controls.Add(this.toolStrip1, 0, 0);
            this.Collection.Controls.Add(this.AllContainer, 0, 1);
            this.Collection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Collection.Location = new System.Drawing.Point(0, 0);
            this.Collection.Margin = new System.Windows.Forms.Padding(0);
            this.Collection.Name = "Collection";
            this.Collection.RowCount = 2;
            this.Collection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.Collection.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Collection.Size = new System.Drawing.Size(319, 370);
            this.Collection.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AttackDropDown,
            this.cmdClipboard,
            this.cmdSort,
            this.toolStripSeparator1,
            this.cmdClear,
            this.VillageInput,
            this.cmdAddVillage,
            this.cmdAddTarget,
            this.UnitInput,
            this.cmdFind,
            this.toolStripSeparator2,
            this.cmdSound});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(319, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // AttackDropDown
            // 
            this.AttackDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AttackDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AttackAllDropDown,
            this.toolStripMenuItem2});
            this.AttackDropDown.Image = global::TribalWars.Properties.Resources.barracks;
            this.AttackDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AttackDropDown.Name = "AttackDropDown";
            this.AttackDropDown.Size = new System.Drawing.Size(29, 22);
            this.AttackDropDown.Text = "Attacks";
            // 
            // AttackAllDropDown
            // 
            this.AttackAllDropDown.Name = "AttackAllDropDown";
            this.AttackAllDropDown.Size = new System.Drawing.Size(96, 22);
            this.AttackAllDropDown.Text = "All";
            this.AttackAllDropDown.Click += new System.EventHandler(this.AttackAllDropDown_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(93, 6);
            // 
            // cmdClipboard
            // 
            this.cmdClipboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdClipboard.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdClipboardText,
            this.cmdClipboardBBCode,
            this.toolStripMenuItem1,
            this.cmdClipboardTextAll,
            this.cmdClipboardBBCodeAll});
            this.cmdClipboard.Image = ((System.Drawing.Image)(resources.GetObject("cmdClipboard.Image")));
            this.cmdClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdClipboard.Name = "cmdClipboard";
            this.cmdClipboard.Size = new System.Drawing.Size(29, 22);
            this.cmdClipboard.Text = "Clipboard";
            // 
            // cmdClipboardText
            // 
            this.cmdClipboardText.Name = "cmdClipboardText";
            this.cmdClipboardText.Size = new System.Drawing.Size(183, 22);
            this.cmdClipboardText.Text = "Text";
            this.cmdClipboardText.Click += new System.EventHandler(this.cmdClipboardText_Click);
            // 
            // cmdClipboardBBCode
            // 
            this.cmdClipboardBBCode.Name = "cmdClipboardBBCode";
            this.cmdClipboardBBCode.Size = new System.Drawing.Size(183, 22);
            this.cmdClipboardBBCode.Text = "BBCode";
            this.cmdClipboardBBCode.Click += new System.EventHandler(this.cmdClipboardBBCode_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 6);
            // 
            // cmdClipboardTextAll
            // 
            this.cmdClipboardTextAll.Name = "cmdClipboardTextAll";
            this.cmdClipboardTextAll.Size = new System.Drawing.Size(183, 22);
            this.cmdClipboardTextAll.Text = "All Attacks (Text)";
            this.cmdClipboardTextAll.Click += new System.EventHandler(this.cmdClipboardTextAll_Click);
            // 
            // cmdClipboardBBCodeAll
            // 
            this.cmdClipboardBBCodeAll.Name = "cmdClipboardBBCodeAll";
            this.cmdClipboardBBCodeAll.Size = new System.Drawing.Size(183, 22);
            this.cmdClipboardBBCodeAll.Text = "All Attacks (BBCode)";
            this.cmdClipboardBBCodeAll.Click += new System.EventHandler(this.cmdClipboardBBCodeAll_Click);
            // 
            // cmdSort
            // 
            this.cmdSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdSort.Image = ((System.Drawing.Image)(resources.GetObject("cmdSort.Image")));
            this.cmdSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSort.Name = "cmdSort";
            this.cmdSort.Size = new System.Drawing.Size(23, 22);
            this.cmdSort.Text = "Sort";
            this.cmdSort.Click += new System.EventHandler(this.cmdSort_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdClear
            // 
            this.cmdClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdClear.Image = ((System.Drawing.Image)(resources.GetObject("cmdClear.Image")));
            this.cmdClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(23, 22);
            this.cmdClear.Text = "Clear plan";
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdAddVillage
            // 
            this.cmdAddVillage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdAddVillage.Image = global::TribalWars.Properties.Resources.FlagBlue;
            this.cmdAddVillage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdAddVillage.Name = "cmdAddVillage";
            this.cmdAddVillage.Size = new System.Drawing.Size(23, 22);
            this.cmdAddVillage.Text = "A";
            this.cmdAddVillage.ToolTipText = "Attack from";
            this.cmdAddVillage.Click += new System.EventHandler(this.cmdAddVillage_Click);
            // 
            // cmdAddTarget
            // 
            this.cmdAddTarget.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdAddTarget.Image = global::TribalWars.Properties.Resources.PinSmall;
            this.cmdAddTarget.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdAddTarget.Name = "cmdAddTarget";
            this.cmdAddTarget.Size = new System.Drawing.Size(23, 22);
            this.cmdAddTarget.Text = "T";
            this.cmdAddTarget.ToolTipText = "Add target";
            this.cmdAddTarget.Click += new System.EventHandler(this.cmdAddTarget_Click);
            // 
            // cmdFind
            // 
            this.cmdFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdFind.Image = ((System.Drawing.Image)(resources.GetObject("cmdFind.Image")));
            this.cmdFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.Size = new System.Drawing.Size(23, 22);
            this.cmdFind.Text = "Find villages";
            this.cmdFind.Click += new System.EventHandler(this.cmdFind_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdSound
            // 
            this.cmdSound.Checked = true;
            this.cmdSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmdSound.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdSound.Image = ((System.Drawing.Image)(resources.GetObject("cmdSound.Image")));
            this.cmdSound.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSound.Name = "cmdSound";
            this.cmdSound.Size = new System.Drawing.Size(23, 22);
            this.cmdSound.Text = "Play warning sound";
            this.cmdSound.Click += new System.EventHandler(this.cmdSound_Click);
            // 
            // Timer
            // 
            this.Timer.Interval = 1000;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // VillageInput
            // 
            this.VillageInput.AutoSize = false;
            this.VillageInput.BackColor = System.Drawing.Color.White;
            this.VillageInput.Name = "VillageInput";
            this.VillageInput.Size = new System.Drawing.Size(50, 20);
            // 
            // UnitInput
            // 
            this.UnitInput.AutoSize = false;
            this.UnitInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.UnitInput.Name = "UnitInput";
            this.UnitInput.Size = new System.Drawing.Size(40, 22);
            // 
            // AllContainer
            // 
            this.AllContainer.AutoScroll = true;
            this.AllContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllContainer.Location = new System.Drawing.Point(0, 25);
            this.AllContainer.Margin = new System.Windows.Forms.Padding(0);
            this.AllContainer.Name = "AllContainer";
            this.AllContainer.Size = new System.Drawing.Size(319, 345);
            this.AllContainer.TabIndex = 5;
            // 
            // MapDistanceCollectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Collection);
            this.Name = "MapDistanceCollectionControl";
            this.Size = new System.Drawing.Size(319, 370);
            this.Collection.ResumeLayout(false);
            this.Collection.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Collection;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton AttackDropDown;
        private System.Windows.Forms.ToolStripMenuItem AttackAllDropDown;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripDropDownButton cmdClipboard;
        private System.Windows.Forms.ToolStripMenuItem cmdClipboardText;
        private System.Windows.Forms.ToolStripMenuItem cmdClipboardBBCode;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cmdClipboardTextAll;
        private System.Windows.Forms.ToolStripMenuItem cmdClipboardBBCodeAll;
        private System.Windows.Forms.ToolStripButton cmdSort;
        private ToolStripVillageTextBox VillageInput;
        private System.Windows.Forms.ToolStripButton cmdAddTarget;
        private System.Windows.Forms.ToolStripButton cmdAddVillage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ToolStripUnitsImageCombobox UnitInput;
        private System.Windows.Forms.ToolStripButton cmdFind;
        private System.Windows.Forms.ToolStripButton cmdClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cmdSound;
        private System.Windows.Forms.Panel AllContainer;
    }
}
