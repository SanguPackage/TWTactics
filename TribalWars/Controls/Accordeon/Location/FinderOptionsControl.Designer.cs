namespace TribalWars.Controls.Accordeon.Location
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinderOptionsControl));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PointsBetweenEnd = new System.Windows.Forms.NumericUpDown();
            this.ResultLimit = new System.Windows.Forms.NumericUpDown();
            this.PointsBetweenStart = new System.Windows.Forms.NumericUpDown();
            this.Location = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Filter = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdVillage = new System.Windows.Forms.Button();
            this.cmdTribe = new System.Windows.Forms.Button();
            this.cmdPlayer = new System.Windows.Forms.Button();
            this.DropDown = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Tribe = new System.Windows.Forms.TextBox();
            this.Search = new TribalWars.Controls.LabelTextBox();
            this.What = new System.Windows.Forms.ComboBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PointsBetweenEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResultLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PointsBetweenStart)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.Tribe);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.Filter);
            this.groupBox4.Controls.Add(this.Location);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.PointsBetweenEnd);
            this.groupBox4.Controls.Add(this.ResultLimit);
            this.groupBox4.Controls.Add(this.PointsBetweenStart);
            this.groupBox4.Location = new System.Drawing.Point(3, 34);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(278, 140);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Additional search criteria";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Limit results";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Points between";
            // 
            // PointsBetweenEnd
            // 
            this.PointsBetweenEnd.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PointsBetweenEnd.Location = new System.Drawing.Point(169, 71);
            this.PointsBetweenEnd.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.PointsBetweenEnd.Name = "PointsBetweenEnd";
            this.PointsBetweenEnd.Size = new System.Drawing.Size(68, 20);
            this.PointsBetweenEnd.TabIndex = 2;
            this.PointsBetweenEnd.ThousandsSeparator = true;
            // 
            // ResultLimit
            // 
            this.ResultLimit.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ResultLimit.Location = new System.Drawing.Point(96, 94);
            this.ResultLimit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ResultLimit.Name = "ResultLimit";
            this.ResultLimit.Size = new System.Drawing.Size(68, 20);
            this.ResultLimit.TabIndex = 2;
            this.ResultLimit.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // PointsBetweenStart
            // 
            this.PointsBetweenStart.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PointsBetweenStart.Location = new System.Drawing.Point(96, 71);
            this.PointsBetweenStart.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.PointsBetweenStart.Name = "PointsBetweenStart";
            this.PointsBetweenStart.Size = new System.Drawing.Size(68, 20);
            this.PointsBetweenStart.TabIndex = 2;
            this.PointsBetweenStart.ThousandsSeparator = true;
            // 
            // Location
            // 
            this.Location.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Location.FormattingEnabled = true;
            this.Location.Items.AddRange(new object[] {
            "Entire map",
            "Visible map",
            "Active rectangle",
            "Polygon"});
            this.Location.Location = new System.Drawing.Point(96, 19);
            this.Location.Name = "Location";
            this.Location.Size = new System.Drawing.Size(141, 21);
            this.Location.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Location";
            // 
            // Filter
            // 
            this.Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Filter.FormattingEnabled = true;
            this.Filter.Items.AddRange(new object[] {
            "All",
            "Strongest",
            "New abandoned villages",
            "Inactives",
            "Points lost villages",
            "Nobled villages",
            "Tribe change players"});
            this.Filter.Location = new System.Drawing.Point(96, 44);
            this.Filter.Name = "Filter";
            this.Filter.Size = new System.Drawing.Size(141, 21);
            this.Filter.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Filter";
            // 
            // cmdVillage
            // 
            this.cmdVillage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdVillage.Location = new System.Drawing.Point(206, 6);
            this.cmdVillage.Name = "cmdVillage";
            this.cmdVillage.Size = new System.Drawing.Size(50, 23);
            this.cmdVillage.TabIndex = 9;
            this.cmdVillage.Text = "Village";
            this.cmdVillage.UseVisualStyleBackColor = true;
            this.cmdVillage.Click += new System.EventHandler(this.cmdVillage_Click);
            // 
            // cmdTribe
            // 
            this.cmdTribe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTribe.Location = new System.Drawing.Point(166, 6);
            this.cmdTribe.Name = "cmdTribe";
            this.cmdTribe.Size = new System.Drawing.Size(41, 23);
            this.cmdTribe.TabIndex = 7;
            this.cmdTribe.Text = "Tribe";
            this.cmdTribe.UseVisualStyleBackColor = true;
            this.cmdTribe.Click += new System.EventHandler(this.cmdTribe_Click);
            // 
            // cmdPlayer
            // 
            this.cmdPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPlayer.Location = new System.Drawing.Point(117, 6);
            this.cmdPlayer.Name = "cmdPlayer";
            this.cmdPlayer.Size = new System.Drawing.Size(50, 23);
            this.cmdPlayer.TabIndex = 8;
            this.cmdPlayer.Text = "Player";
            this.cmdPlayer.UseVisualStyleBackColor = true;
            this.cmdPlayer.Click += new System.EventHandler(this.cmdPlayer_Click);
            // 
            // DropDown
            // 
            this.DropDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DropDown.Image = ((System.Drawing.Image)(resources.GetObject("DropDown.Image")));
            this.DropDown.Location = new System.Drawing.Point(262, 6);
            this.DropDown.Name = "DropDown";
            this.DropDown.Size = new System.Drawing.Size(22, 23);
            this.DropDown.TabIndex = 10;
            this.DropDown.UseVisualStyleBackColor = true;
            this.DropDown.Click += new System.EventHandler(this.DropDown_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Within tribe";
            // 
            // Tribe
            // 
            this.Tribe.Location = new System.Drawing.Point(96, 117);
            this.Tribe.Name = "Tribe";
            this.Tribe.Size = new System.Drawing.Size(68, 20);
            this.Tribe.TabIndex = 8;
            // 
            // Search
            // 
            this.Search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Search.LabelText = "Find";
            this.Search.Location = new System.Drawing.Point(-1, 4);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(119, 25);
            this.Search.TabIndex = 6;
            this.Search.TextBoxWidth = 75;
            // 
            // What
            // 
            this.What.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.What.FormattingEnabled = true;
            this.What.Items.AddRange(new object[] {
            "Players",
            "Tribes",
            "Villages"});
            this.What.Location = new System.Drawing.Point(119, 8);
            this.What.Name = "What";
            this.What.Size = new System.Drawing.Size(164, 21);
            this.What.TabIndex = 11;
            this.What.Visible = false;
            // 
            // FinderOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.DropDown);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.cmdVillage);
            this.Controls.Add(this.cmdTribe);
            this.Controls.Add(this.cmdPlayer);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.What);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "FinderOptionsControl";
            this.Size = new System.Drawing.Size(285, 35);
            this.Load += new System.EventHandler(this.FinderOptionsControl_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PointsBetweenEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResultLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PointsBetweenStart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox Location;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown PointsBetweenEnd;
        private System.Windows.Forms.NumericUpDown ResultLimit;
        private System.Windows.Forms.NumericUpDown PointsBetweenStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Filter;
        private System.Windows.Forms.Button DropDown;
        private LabelTextBox Search;
        private System.Windows.Forms.Button cmdVillage;
        private System.Windows.Forms.Button cmdTribe;
        private System.Windows.Forms.Button cmdPlayer;
        private System.Windows.Forms.TextBox Tribe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox What;
    }
}
