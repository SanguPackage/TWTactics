﻿namespace TribalWars.Forms
{
    partial class TimeZoneForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeZoneForm));
            this.gbTimeZome = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TimeOffset = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ServerTime = new System.Windows.Forms.Label();
            this.LocalTime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TimeDisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.gbTimeZome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // gbTimeZome
            // 
            this.gbTimeZome.Controls.Add(this.label3);
            this.gbTimeZome.Controls.Add(this.TimeOffset);
            this.gbTimeZome.Controls.Add(this.label2);
            this.gbTimeZome.Controls.Add(this.label1);
            this.gbTimeZome.Controls.Add(this.ServerTime);
            this.gbTimeZome.Controls.Add(this.LocalTime);
            this.gbTimeZome.Controls.Add(this.label6);
            this.gbTimeZome.Controls.Add(this.label5);
            this.gbTimeZome.Location = new System.Drawing.Point(12, 12);
            this.gbTimeZome.Name = "gbTimeZome";
            this.gbTimeZome.Size = new System.Drawing.Size(242, 179);
            this.gbTimeZome.TabIndex = 13;
            this.gbTimeZome.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 69);
            this.label3.TabIndex = 8;
            this.label3.Text = "This progam needs to know what time zone you are in. Look at \'server time\' and en" +
    "sure it is the same as Tribal Wars server time. If it is not, change the \'Time A" +
    "djustment\' value until it is.\r\n";
            // 
            // TimeOffset
            // 
            this.TimeOffset.Location = new System.Drawing.Point(104, 64);
            this.TimeOffset.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.TimeOffset.Minimum = new decimal(new int[] {
            23,
            0,
            0,
            -2147483648});
            this.TimeOffset.Name = "TimeOffset";
            this.TimeOffset.Size = new System.Drawing.Size(42, 20);
            this.TimeOffset.TabIndex = 7;
            this.TimeOffset.ValueChanged += new System.EventHandler(this.TimeOffset_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "hours";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Time adjustment:";
            // 
            // ServerTime
            // 
            this.ServerTime.AutoSize = true;
            this.ServerTime.Location = new System.Drawing.Point(95, 33);
            this.ServerTime.Name = "ServerTime";
            this.ServerTime.Size = new System.Drawing.Size(10, 13);
            this.ServerTime.TabIndex = 3;
            this.ServerTime.Text = ".";
            // 
            // LocalTime
            // 
            this.LocalTime.AutoSize = true;
            this.LocalTime.Location = new System.Drawing.Point(95, 16);
            this.LocalTime.Name = "LocalTime";
            this.LocalTime.Size = new System.Drawing.Size(10, 13);
            this.LocalTime.TabIndex = 2;
            this.LocalTime.Text = ".";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Server time:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Your local time:";
            // 
            // TimeDisplayTimer
            // 
            this.TimeDisplayTimer.Enabled = true;
            this.TimeDisplayTimer.Interval = 200;
            this.TimeDisplayTimer.Tick += new System.EventHandler(this.TimeDisplayTimer_Tick);
            // 
            // TimeZoneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 201);
            this.Controls.Add(this.gbTimeZome);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimeZoneForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Synchronise Server Time";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TimeZoneForm_FormClosing);
            this.gbTimeZome.ResumeLayout(false);
            this.gbTimeZome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeOffset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTimeZome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown TimeOffset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ServerTime;
        private System.Windows.Forms.Label LocalTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer TimeDisplayTimer;
    }
}