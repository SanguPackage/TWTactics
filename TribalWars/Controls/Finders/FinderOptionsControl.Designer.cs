using TribalWars.Controls.Common;

namespace TribalWars.Controls.Finders
{
    partial class FinderOptionsControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinderOptionsControl));
			this.WithinTribeLabel = new System.Windows.Forms.Label();
			this.FilterLabel = new System.Windows.Forms.Label();
			this.AreaLabel = new System.Windows.Forms.Label();
			this.Filter = new System.Windows.Forms.ComboBox();
			this.Area = new System.Windows.Forms.ComboBox();
			this.LimitResultsLabel = new System.Windows.Forms.Label();
			this.PointsBetweenLabel = new System.Windows.Forms.Label();
			this.PointsBetweenEnd = new System.Windows.Forms.NumericUpDown();
			this.ResultLimit = new System.Windows.Forms.NumericUpDown();
			this.PointsBetweenStart = new System.Windows.Forms.NumericUpDown();
			this.cmdVillage = new System.Windows.Forms.Button();
			this.cmdTribe = new System.Windows.Forms.Button();
			this.cmdPlayer = new System.Windows.Forms.Button();
			this.DropDown = new System.Windows.Forms.Button();
			this.What = new System.Windows.Forms.ComboBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label6 = new System.Windows.Forms.Label();
			this.Tribe = new TribalWars.Controls.Finders.PlayerTribeDropdown();
			this.Search = new TribalWars.Controls.Common.LabelTextBox();
			((System.ComponentModel.ISupportInitialize)(this.PointsBetweenEnd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ResultLimit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PointsBetweenStart)).BeginInit();
			this.SuspendLayout();
			// 
			// WithinTribeLabel
			// 
			resources.ApplyResources(this.WithinTribeLabel, "WithinTribeLabel");
			this.WithinTribeLabel.Name = "WithinTribeLabel";
			this.toolTip1.SetToolTip(this.WithinTribeLabel, resources.GetString("WithinTribeLabel.ToolTip"));
			// 
			// FilterLabel
			// 
			resources.ApplyResources(this.FilterLabel, "FilterLabel");
			this.FilterLabel.Name = "FilterLabel";
			this.toolTip1.SetToolTip(this.FilterLabel, resources.GetString("FilterLabel.ToolTip"));
			// 
			// AreaLabel
			// 
			resources.ApplyResources(this.AreaLabel, "AreaLabel");
			this.AreaLabel.Name = "AreaLabel";
			this.toolTip1.SetToolTip(this.AreaLabel, resources.GetString("AreaLabel.ToolTip"));
			// 
			// Filter
			// 
			resources.ApplyResources(this.Filter, "Filter");
			this.Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Filter.FormattingEnabled = true;
			this.Filter.Items.AddRange(new object[] {
            resources.GetString("Filter.Items"),
            resources.GetString("Filter.Items1"),
            resources.GetString("Filter.Items2"),
            resources.GetString("Filter.Items3"),
            resources.GetString("Filter.Items4"),
            resources.GetString("Filter.Items5"),
            resources.GetString("Filter.Items6")});
			this.Filter.Name = "Filter";
			this.toolTip1.SetToolTip(this.Filter, resources.GetString("Filter.ToolTip"));
			// 
			// Area
			// 
			resources.ApplyResources(this.Area, "Area");
			this.Area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Area.FormattingEnabled = true;
			this.Area.Items.AddRange(new object[] {
            resources.GetString("Area.Items"),
            resources.GetString("Area.Items1"),
            resources.GetString("Area.Items2"),
            resources.GetString("Area.Items3")});
			this.Area.Name = "Area";
			this.toolTip1.SetToolTip(this.Area, resources.GetString("Area.ToolTip"));
			// 
			// LimitResultsLabel
			// 
			resources.ApplyResources(this.LimitResultsLabel, "LimitResultsLabel");
			this.LimitResultsLabel.Name = "LimitResultsLabel";
			this.toolTip1.SetToolTip(this.LimitResultsLabel, resources.GetString("LimitResultsLabel.ToolTip"));
			// 
			// PointsBetweenLabel
			// 
			resources.ApplyResources(this.PointsBetweenLabel, "PointsBetweenLabel");
			this.PointsBetweenLabel.Name = "PointsBetweenLabel";
			this.toolTip1.SetToolTip(this.PointsBetweenLabel, resources.GetString("PointsBetweenLabel.ToolTip"));
			// 
			// PointsBetweenEnd
			// 
			resources.ApplyResources(this.PointsBetweenEnd, "PointsBetweenEnd");
			this.PointsBetweenEnd.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.PointsBetweenEnd.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.PointsBetweenEnd.Name = "PointsBetweenEnd";
			this.toolTip1.SetToolTip(this.PointsBetweenEnd, resources.GetString("PointsBetweenEnd.ToolTip"));
			// 
			// ResultLimit
			// 
			resources.ApplyResources(this.ResultLimit, "ResultLimit");
			this.ResultLimit.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.ResultLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.ResultLimit.Name = "ResultLimit";
			this.toolTip1.SetToolTip(this.ResultLimit, resources.GetString("ResultLimit.ToolTip"));
			this.ResultLimit.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// PointsBetweenStart
			// 
			resources.ApplyResources(this.PointsBetweenStart, "PointsBetweenStart");
			this.PointsBetweenStart.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.PointsBetweenStart.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.PointsBetweenStart.Name = "PointsBetweenStart";
			this.toolTip1.SetToolTip(this.PointsBetweenStart, resources.GetString("PointsBetweenStart.ToolTip"));
			// 
			// cmdVillage
			// 
			resources.ApplyResources(this.cmdVillage, "cmdVillage");
			this.cmdVillage.Name = "cmdVillage";
			this.toolTip1.SetToolTip(this.cmdVillage, resources.GetString("cmdVillage.ToolTip"));
			this.cmdVillage.UseVisualStyleBackColor = true;
			this.cmdVillage.Click += new System.EventHandler(this.cmdVillage_Click);
			// 
			// cmdTribe
			// 
			resources.ApplyResources(this.cmdTribe, "cmdTribe");
			this.cmdTribe.Name = "cmdTribe";
			this.toolTip1.SetToolTip(this.cmdTribe, resources.GetString("cmdTribe.ToolTip"));
			this.cmdTribe.UseVisualStyleBackColor = true;
			this.cmdTribe.Click += new System.EventHandler(this.cmdTribe_Click);
			// 
			// cmdPlayer
			// 
			resources.ApplyResources(this.cmdPlayer, "cmdPlayer");
			this.cmdPlayer.Name = "cmdPlayer";
			this.toolTip1.SetToolTip(this.cmdPlayer, resources.GetString("cmdPlayer.ToolTip"));
			this.cmdPlayer.UseVisualStyleBackColor = true;
			this.cmdPlayer.Click += new System.EventHandler(this.cmdPlayer_Click);
			// 
			// DropDown
			// 
			resources.ApplyResources(this.DropDown, "DropDown");
			this.DropDown.Name = "DropDown";
			this.toolTip1.SetToolTip(this.DropDown, resources.GetString("DropDown.ToolTip"));
			this.DropDown.UseVisualStyleBackColor = true;
			this.DropDown.Click += new System.EventHandler(this.DropDown_Click);
			// 
			// What
			// 
			resources.ApplyResources(this.What, "What");
			this.What.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.What.FormattingEnabled = true;
			this.What.Items.AddRange(new object[] {
            resources.GetString("What.Items"),
            resources.GetString("What.Items1"),
            resources.GetString("What.Items2")});
			this.What.Name = "What";
			this.toolTip1.SetToolTip(this.What, resources.GetString("What.ToolTip"));
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
			// 
			// Tribe
			// 
			resources.ApplyResources(this.Tribe, "Tribe");
			this.Tribe.AllowPlayer = false;
			this.Tribe.AutoOpenOnFocus = false;
			this.Tribe.BackColor = System.Drawing.Color.Transparent;
			this.Tribe.Name = "Tribe";
			this.toolTip1.SetToolTip(this.Tribe, resources.GetString("Tribe.ToolTip"));
			// 
			// Search
			// 
			resources.ApplyResources(this.Search, "Search");
			this.Search.LabelText = "Zoek";
			this.Search.Name = "Search";
			this.Search.TextBoxWidth = 70;
			this.toolTip1.SetToolTip(this.Search, resources.GetString("Search.ToolTip"));
			// 
			// FinderOptionsControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.label6);
			this.Controls.Add(this.Tribe);
			this.Controls.Add(this.DropDown);
			this.Controls.Add(this.WithinTribeLabel);
			this.Controls.Add(this.Search);
			this.Controls.Add(this.FilterLabel);
			this.Controls.Add(this.cmdVillage);
			this.Controls.Add(this.AreaLabel);
			this.Controls.Add(this.cmdTribe);
			this.Controls.Add(this.Filter);
			this.Controls.Add(this.cmdPlayer);
			this.Controls.Add(this.Area);
			this.Controls.Add(this.LimitResultsLabel);
			this.Controls.Add(this.PointsBetweenLabel);
			this.Controls.Add(this.What);
			this.Controls.Add(this.PointsBetweenEnd);
			this.Controls.Add(this.PointsBetweenStart);
			this.Controls.Add(this.ResultLimit);
			this.Name = "FinderOptionsControl";
			this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
			this.Load += new System.EventHandler(this.FinderOptionsControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.PointsBetweenEnd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ResultLimit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PointsBetweenStart)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Area;
        private System.Windows.Forms.Label LimitResultsLabel;
        private System.Windows.Forms.Label PointsBetweenLabel;
        private System.Windows.Forms.NumericUpDown PointsBetweenEnd;
        private System.Windows.Forms.NumericUpDown ResultLimit;
        private System.Windows.Forms.NumericUpDown PointsBetweenStart;
        private System.Windows.Forms.Label FilterLabel;
        private System.Windows.Forms.Label AreaLabel;
        private System.Windows.Forms.ComboBox Filter;
        private System.Windows.Forms.Button DropDown;
        private LabelTextBox Search;
        private System.Windows.Forms.Button cmdVillage;
        private System.Windows.Forms.Button cmdTribe;
        private System.Windows.Forms.Button cmdPlayer;
        private System.Windows.Forms.Label WithinTribeLabel;
        private PlayerTribeDropdown Tribe;
        private System.Windows.Forms.ComboBox What;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label6;
    }
}
