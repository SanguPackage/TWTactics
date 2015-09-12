using TribalWars.Controls.Common;
using TribalWars.Controls.Common.ToolStripControlHostWrappers;

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
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.AttackDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.cmdClipboard = new System.Windows.Forms.ToolStripDropDownButton();
			this.cmdClipboardText = new System.Windows.Forms.ToolStripMenuItem();
			this.cmdClipboardBBCode = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmdClipboardTextAll = new System.Windows.Forms.ToolStripMenuItem();
			this.cmdClipboardBBCodeAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.VillageInputLabel = new System.Windows.Forms.ToolStripLabel();
			this.VillageInput = new TribalWars.Controls.Common.ToolStripControlHostWrappers.ToolStripVillageTextBox();
			this.cmdAddVillage = new System.Windows.Forms.ToolStripButton();
			this.cmdAddTarget = new System.Windows.Forms.ToolStripButton();
			this.AllPlans = new System.Windows.Forms.Panel();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.VillageTypeInput = new TribalWars.Controls.Common.ToolStripControlHostWrappers.ToolStripUnitsImageCombobox();
			this.UnitInput = new TribalWars.Controls.Common.ToolStripControlHostWrappers.ToolStripUnitsImageCombobox();
			this.cmdFind = new System.Windows.Forms.ToolStripButton();
			this.cmdFindPool = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.cmdSort = new System.Windows.Forms.ToolStripButton();
			this.cmdClear = new System.Windows.Forms.ToolStripButton();
			this.Timer = new System.Windows.Forms.Timer(this.components);
			this.Collection.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// Collection
			// 
			resources.ApplyResources(this.Collection, "Collection");
			this.Collection.Controls.Add(this.toolStrip1, 0, 0);
			this.Collection.Controls.Add(this.AllPlans, 0, 2);
			this.Collection.Controls.Add(this.toolStrip2, 0, 1);
			this.Collection.Name = "Collection";
			// 
			// toolStrip1
			// 
			resources.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.AttackDropDown,
            this.cmdClipboard,
            this.toolStripSeparator1,
            this.VillageInputLabel,
            this.VillageInput,
            this.cmdAddVillage,
            this.cmdAddTarget});
			this.toolStrip1.Name = "toolStrip1";
			// 
			// toolStripLabel3
			// 
			resources.ApplyResources(this.toolStripLabel3, "toolStripLabel3");
			this.toolStripLabel3.Name = "toolStripLabel3";
			// 
			// AttackDropDown
			// 
			resources.ApplyResources(this.AttackDropDown, "AttackDropDown");
			this.AttackDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AttackDropDown.Image = global::TribalWars.Properties.Resources.barracks;
			this.AttackDropDown.Name = "AttackDropDown";
			// 
			// cmdClipboard
			// 
			resources.ApplyResources(this.cmdClipboard, "cmdClipboard");
			this.cmdClipboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdClipboard.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdClipboardText,
            this.cmdClipboardBBCode,
            this.toolStripMenuItem1,
            this.cmdClipboardTextAll,
            this.cmdClipboardBBCodeAll});
			this.cmdClipboard.Name = "cmdClipboard";
			// 
			// cmdClipboardText
			// 
			resources.ApplyResources(this.cmdClipboardText, "cmdClipboardText");
			this.cmdClipboardText.Name = "cmdClipboardText";
			this.cmdClipboardText.Click += new System.EventHandler(this.cmdClipboardText_Click);
			// 
			// cmdClipboardBBCode
			// 
			resources.ApplyResources(this.cmdClipboardBBCode, "cmdClipboardBBCode");
			this.cmdClipboardBBCode.Name = "cmdClipboardBBCode";
			this.cmdClipboardBBCode.Click += new System.EventHandler(this.cmdClipboardBBCode_Click);
			// 
			// toolStripMenuItem1
			// 
			resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			// 
			// cmdClipboardTextAll
			// 
			resources.ApplyResources(this.cmdClipboardTextAll, "cmdClipboardTextAll");
			this.cmdClipboardTextAll.Name = "cmdClipboardTextAll";
			this.cmdClipboardTextAll.Click += new System.EventHandler(this.cmdClipboardTextAll_Click);
			// 
			// cmdClipboardBBCodeAll
			// 
			resources.ApplyResources(this.cmdClipboardBBCodeAll, "cmdClipboardBBCodeAll");
			this.cmdClipboardBBCodeAll.Name = "cmdClipboardBBCodeAll";
			this.cmdClipboardBBCodeAll.Click += new System.EventHandler(this.cmdClipboardBBCodeAll_Click);
			// 
			// toolStripSeparator1
			// 
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// VillageInputLabel
			// 
			resources.ApplyResources(this.VillageInputLabel, "VillageInputLabel");
			this.VillageInputLabel.Name = "VillageInputLabel";
			// 
			// VillageInput
			// 
			resources.ApplyResources(this.VillageInput, "VillageInput");
			this.VillageInput.BackColor = System.Drawing.Color.White;
			this.VillageInput.ForeColor = System.Drawing.SystemColors.WindowText;
			this.VillageInput.Name = "VillageInput";
			this.VillageInput.Player = null;
			this.VillageInput.ShowImage = false;
			this.VillageInput.Tribe = null;
			this.VillageInput.Village = null;
			this.VillageInput.TextChanged += new System.EventHandler(this.VillageInput_TextChanged);
			// 
			// cmdAddVillage
			// 
			resources.ApplyResources(this.cmdAddVillage, "cmdAddVillage");
			this.cmdAddVillage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdAddVillage.Image = global::TribalWars.Properties.Resources.FlagBlue;
			this.cmdAddVillage.Name = "cmdAddVillage";
			this.cmdAddVillage.Tag = "OWN_VISIBILITY";
			this.cmdAddVillage.Click += new System.EventHandler(this.cmdAddVillage_Click);
			// 
			// cmdAddTarget
			// 
			resources.ApplyResources(this.cmdAddTarget, "cmdAddTarget");
			this.cmdAddTarget.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdAddTarget.Image = global::TribalWars.Properties.Resources.PinSmall;
			this.cmdAddTarget.Name = "cmdAddTarget";
			this.cmdAddTarget.Tag = "OWN_VISIBILITY";
			this.cmdAddTarget.Click += new System.EventHandler(this.cmdAddTarget_Click);
			// 
			// AllPlans
			// 
			resources.ApplyResources(this.AllPlans, "AllPlans");
			this.AllPlans.Name = "AllPlans";
			// 
			// toolStrip2
			// 
			resources.ApplyResources(this.toolStrip2, "toolStrip2");
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.VillageTypeInput,
            this.UnitInput,
            this.cmdFind,
            this.cmdFindPool,
            this.toolStripSeparator2,
            this.cmdSort,
            this.cmdClear});
			this.toolStrip2.Name = "toolStrip2";
			// 
			// toolStripLabel1
			// 
			resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
			this.toolStripLabel1.Name = "toolStripLabel1";
			// 
			// VillageTypeInput
			// 
			resources.ApplyResources(this.VillageTypeInput, "VillageTypeInput");
			this.VillageTypeInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
			this.VillageTypeInput.Name = "VillageTypeInput";
			// 
			// UnitInput
			// 
			resources.ApplyResources(this.UnitInput, "UnitInput");
			this.UnitInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(232)))));
			this.UnitInput.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
			this.UnitInput.Name = "UnitInput";
			this.UnitInput.Click += new System.EventHandler(this.UnitInput_Click);
			// 
			// cmdFind
			// 
			resources.ApplyResources(this.cmdFind, "cmdFind");
			this.cmdFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdFind.Name = "cmdFind";
			this.cmdFind.Click += new System.EventHandler(this.cmdFind_Click);
			// 
			// cmdFindPool
			// 
			resources.ApplyResources(this.cmdFindPool, "cmdFindPool");
			this.cmdFindPool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdFindPool.Name = "cmdFindPool";
			this.cmdFindPool.Click += new System.EventHandler(this.cmdFindPool_Click);
			// 
			// toolStripSeparator2
			// 
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			// 
			// cmdSort
			// 
			resources.ApplyResources(this.cmdSort, "cmdSort");
			this.cmdSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdSort.Name = "cmdSort";
			this.cmdSort.Click += new System.EventHandler(this.cmdSort_Click);
			// 
			// cmdClear
			// 
			resources.ApplyResources(this.cmdClear, "cmdClear");
			this.cmdClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdClear.Name = "cmdClear";
			this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
			// 
			// Timer
			// 
			this.Timer.Interval = 1000;
			this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
			// 
			// AttackPlanCollectionControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Collection);
			this.Name = "AttackPlanCollectionControl";
			this.Collection.ResumeLayout(false);
			this.Collection.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton cmdFindPool;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private ToolStripUnitsImageCombobox VillageTypeInput;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel VillageInputLabel;
    }
}
