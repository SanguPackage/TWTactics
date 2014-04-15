namespace TribalWars.Controls
{
    partial class ParserControl
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
            this.Output = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CheckPeople = new System.Windows.Forms.CheckBox();
            this.CheckBuildings = new System.Windows.Forms.CheckBox();
            this.CheckDefenseTroops = new System.Windows.Forms.CheckBox();
            this.CheckFarming = new System.Windows.Forms.CheckBox();
            this.CheckAttackTroops = new System.Windows.Forms.CheckBox();
            this.CheckResources = new System.Windows.Forms.CheckBox();
            this.CheckDefender = new System.Windows.Forms.CheckBox();
            this.CheckAttacker = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Output
            // 
            this.Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Output.Location = new System.Drawing.Point(3, 28);
            this.Output.Multiline = true;
            this.Output.Name = "Output";
            this.Output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Output.Size = new System.Drawing.Size(550, 571);
            this.Output.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.Output, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(695, 602);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(559, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.39597F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.60403F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(133, 596);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CheckPeople);
            this.groupBox1.Controls.Add(this.CheckBuildings);
            this.groupBox1.Controls.Add(this.CheckDefenseTroops);
            this.groupBox1.Controls.Add(this.CheckFarming);
            this.groupBox1.Controls.Add(this.CheckAttackTroops);
            this.groupBox1.Controls.Add(this.CheckResources);
            this.groupBox1.Controls.Add(this.CheckDefender);
            this.groupBox1.Controls.Add(this.CheckAttacker);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 205);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output";
            // 
            // CheckPeople
            // 
            this.CheckPeople.AutoSize = true;
            this.CheckPeople.Location = new System.Drawing.Point(6, 157);
            this.CheckPeople.Name = "CheckPeople";
            this.CheckPeople.Size = new System.Drawing.Size(59, 17);
            this.CheckPeople.TabIndex = 0;
            this.CheckPeople.Text = "People";
            this.CheckPeople.UseVisualStyleBackColor = true;
            this.CheckPeople.CheckedChanged += new System.EventHandler(this.CheckPeople_CheckedChanged);
            // 
            // CheckBuildings
            // 
            this.CheckBuildings.AutoSize = true;
            this.CheckBuildings.Checked = true;
            this.CheckBuildings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBuildings.Location = new System.Drawing.Point(6, 180);
            this.CheckBuildings.Name = "CheckBuildings";
            this.CheckBuildings.Size = new System.Drawing.Size(68, 17);
            this.CheckBuildings.TabIndex = 0;
            this.CheckBuildings.Text = "Buildings";
            this.CheckBuildings.UseVisualStyleBackColor = true;
            this.CheckBuildings.CheckedChanged += new System.EventHandler(this.CheckBuildings_CheckedChanged);
            // 
            // CheckDefenseTroops
            // 
            this.CheckDefenseTroops.AutoSize = true;
            this.CheckDefenseTroops.Checked = true;
            this.CheckDefenseTroops.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckDefenseTroops.Location = new System.Drawing.Point(6, 134);
            this.CheckDefenseTroops.Name = "CheckDefenseTroops";
            this.CheckDefenseTroops.Size = new System.Drawing.Size(107, 17);
            this.CheckDefenseTroops.TabIndex = 0;
            this.CheckDefenseTroops.Text = "Defending troops";
            this.CheckDefenseTroops.UseVisualStyleBackColor = true;
            this.CheckDefenseTroops.CheckedChanged += new System.EventHandler(this.CheckDefenseTroops_CheckedChanged);
            // 
            // CheckFarming
            // 
            this.CheckFarming.AutoSize = true;
            this.CheckFarming.Location = new System.Drawing.Point(6, 88);
            this.CheckFarming.Name = "CheckFarming";
            this.CheckFarming.Size = new System.Drawing.Size(96, 17);
            this.CheckFarming.TabIndex = 0;
            this.CheckFarming.Text = "Farming details";
            this.CheckFarming.UseVisualStyleBackColor = true;
            this.CheckFarming.CheckedChanged += new System.EventHandler(this.CheckFarming_CheckedChanged);
            // 
            // CheckAttackTroops
            // 
            this.CheckAttackTroops.AutoSize = true;
            this.CheckAttackTroops.Checked = true;
            this.CheckAttackTroops.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckAttackTroops.Location = new System.Drawing.Point(6, 111);
            this.CheckAttackTroops.Name = "CheckAttackTroops";
            this.CheckAttackTroops.Size = new System.Drawing.Size(103, 17);
            this.CheckAttackTroops.TabIndex = 0;
            this.CheckAttackTroops.Text = "Attacking troops";
            this.CheckAttackTroops.UseVisualStyleBackColor = true;
            this.CheckAttackTroops.CheckedChanged += new System.EventHandler(this.CheckAttackTroops_CheckedChanged);
            // 
            // CheckResources
            // 
            this.CheckResources.AutoSize = true;
            this.CheckResources.Location = new System.Drawing.Point(6, 65);
            this.CheckResources.Name = "CheckResources";
            this.CheckResources.Size = new System.Drawing.Size(107, 17);
            this.CheckResources.TabIndex = 0;
            this.CheckResources.Text = "Resource Details";
            this.CheckResources.UseVisualStyleBackColor = true;
            // 
            // CheckDefender
            // 
            this.CheckDefender.AutoSize = true;
            this.CheckDefender.Checked = true;
            this.CheckDefender.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckDefender.Location = new System.Drawing.Point(6, 42);
            this.CheckDefender.Name = "CheckDefender";
            this.CheckDefender.Size = new System.Drawing.Size(106, 17);
            this.CheckDefender.TabIndex = 0;
            this.CheckDefender.Text = "Defending player";
            this.CheckDefender.UseVisualStyleBackColor = true;
            this.CheckDefender.CheckedChanged += new System.EventHandler(this.CheckDefender_CheckedChanged);
            // 
            // CheckAttacker
            // 
            this.CheckAttacker.AutoSize = true;
            this.CheckAttacker.Checked = true;
            this.CheckAttacker.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckAttacker.Location = new System.Drawing.Point(6, 19);
            this.CheckAttacker.Name = "CheckAttacker";
            this.CheckAttacker.Size = new System.Drawing.Size(102, 17);
            this.CheckAttacker.TabIndex = 0;
            this.CheckAttacker.Text = "Attacking player";
            this.CheckAttacker.UseVisualStyleBackColor = true;
            this.CheckAttacker.CheckedChanged += new System.EventHandler(this.CheckAttacker_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(556, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ParserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ParserControl";
            this.Size = new System.Drawing.Size(695, 602);
            this.Load += new System.EventHandler(this.ReportParser_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox CheckBuildings;
        private System.Windows.Forms.CheckBox CheckDefenseTroops;
        private System.Windows.Forms.CheckBox CheckAttackTroops;
        private System.Windows.Forms.CheckBox CheckResources;
        private System.Windows.Forms.CheckBox CheckDefender;
        private System.Windows.Forms.CheckBox CheckAttacker;
        private System.Windows.Forms.CheckBox CheckFarming;
        private System.Windows.Forms.CheckBox CheckPeople;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}
