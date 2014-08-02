using TribalWars.Controls.Common;

namespace TribalWars.Maps.AttackPlans.Controls
{
    partial class AttackPlanCollectionControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttackPlanCollectionControl));
            this.Collection = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AttackDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.cmdClipboard = new System.Windows.Forms.ToolStripDropDownButton();
            this.cmdClipboardText = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdClipboardBBCode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdClipboardTextAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdClipboardBBCodeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSort = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdClear = new System.Windows.Forms.ToolStripButton();
            this.VillageInput = new TribalWars.Controls.Common.ToolStripVillageTextBox();
            this.cmdAddVillage = new System.Windows.Forms.ToolStripButton();
            this.cmdAddTarget = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.UnitInput = new TribalWars.Controls.Common.ToolStripUnitsImageCombobox();
            this.cmdFind = new System.Windows.Forms.ToolStripButton();
            this.cmdFindPool = new System.Windows.Forms.ToolStripButton();
            this.AllPlans = new System.Windows.Forms.Panel();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.Collection.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Collection
            // 
            this.Collection.ColumnCount = 1;
            this.Collection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Collection.Controls.Add(this.toolStrip1, 0, 0);
            this.Collection.Controls.Add(this.AllPlans, 0, 1);
            this.Collection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Collection.Location = new System.Drawing.Point(0, 0);
            this.Collection.Margin = new System.Windows.Forms.Padding(0);
            this.Collection.Name = "Collection";
            this.Collection.RowCount = 2;
            this.Collection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.Collection.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Collection.Size = new System.Drawing.Size(319, 95);
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
            this.toolStripSeparator2,
            this.UnitInput,
            this.cmdFind,
            this.cmdFindPool});
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
            this.AttackDropDown.Image = global::TribalWars.Properties.Resources.barracks;
            this.AttackDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AttackDropDown.Name = "AttackDropDown";
            this.AttackDropDown.Size = new System.Drawing.Size(29, 22);
            this.AttackDropDown.Text = "Select an existing attack plan";
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
            this.cmdClipboard.Text = "Export attack plans to clipboard";
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
            this.cmdSort.Text = "Sort attacks on travel time";
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
            this.cmdClear.Text = "Remove all attacks on currently selected plan";
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // VillageInput
            // 
            this.VillageInput.AutoSize = false;
            this.VillageInput.BackColor = System.Drawing.Color.White;
            this.VillageInput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.VillageInput.ForeColor = System.Drawing.SystemColors.WindowText;
            this.VillageInput.Name = "VillageInput";
            this.VillageInput.Player = null;
            this.VillageInput.ShowImage = false;
            this.VillageInput.Size = new System.Drawing.Size(60, 23);
            this.VillageInput.Tribe = null;
            this.VillageInput.Village = null;
            // 
            // cmdAddVillage
            // 
            this.cmdAddVillage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdAddVillage.Image = global::TribalWars.Properties.Resources.FlagBlue;
            this.cmdAddVillage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdAddVillage.Name = "cmdAddVillage";
            this.cmdAddVillage.Size = new System.Drawing.Size(23, 22);
            this.cmdAddVillage.Text = "A";
            this.cmdAddVillage.ToolTipText = "Attack current target from selected village";
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
            this.cmdAddTarget.ToolTipText = "Add target village";
            this.cmdAddTarget.Click += new System.EventHandler(this.cmdAddTarget_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // UnitInput
            // 
            this.UnitInput.AutoSize = false;
            this.UnitInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
            this.UnitInput.Name = "UnitInput";
            this.UnitInput.Size = new System.Drawing.Size(40, 24);
            // 
            // cmdFind
            // 
            this.cmdFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdFind.Image = ((System.Drawing.Image)(resources.GetObject("cmdFind.Image")));
            this.cmdFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.Size = new System.Drawing.Size(23, 22);
            this.cmdFind.Text = "Find villages";
            this.cmdFind.ToolTipText = "Find villages that can still reach the target at the arrival time at the given sp" +
    "eed";
            this.cmdFind.Click += new System.EventHandler(this.cmdFind_Click);
            // 
            // cmdFindPool
            // 
            this.cmdFindPool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdFindPool.Image = ((System.Drawing.Image)(resources.GetObject("cmdFindPool.Image")));
            this.cmdFindPool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdFindPool.Name = "cmdFindPool";
            this.cmdFindPool.Size = new System.Drawing.Size(23, 20);
            this.cmdFindPool.ToolTipText = "Find villages inside the attackers pool that can still reach the target at the ar" +
    "rival time at the given speed. Add villages to the attack pool in the \'Manage yo" +
    "ur villages\' Window.";
            this.cmdFindPool.Click += new System.EventHandler(this.cmdFindPool_Click);
            // 
            // AllPlans
            // 
            this.AllPlans.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AllPlans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllPlans.Location = new System.Drawing.Point(0, 25);
            this.AllPlans.Margin = new System.Windows.Forms.Padding(0);
            this.AllPlans.Name = "AllPlans";
            this.AllPlans.Size = new System.Drawing.Size(319, 70);
            this.AllPlans.TabIndex = 5;
            // 
            // Timer
            // 
            this.Timer.Interval = 1000;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // AttackPlanCollectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.Collection);
            this.Name = "AttackPlanCollectionControl";
            this.Size = new System.Drawing.Size(319, 95);
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
        private System.Windows.Forms.Panel AllPlans;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cmdFindPool;
    }
}
