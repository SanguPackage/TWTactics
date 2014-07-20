namespace TribalWars.Controls.TimeConverter
{
    partial class TimeConverterControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeConverterControl));
            this.Date = new System.Windows.Forms.DateTimePicker();
            this.DateText = new System.Windows.Forms.TextBox();
            this.ParseClipboard = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ParseClipboard)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Date
            // 
            this.Date.CustomFormat = "MMM dd, yyyy HH:mm:ss";
            this.Date.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date.Location = new System.Drawing.Point(0, 0);
            this.Date.Name = "Date";
            this.Date.ShowUpDown = true;
            this.Date.Size = new System.Drawing.Size(153, 20);
            this.Date.TabIndex = 2;
            this.Date.ValueChanged += new System.EventHandler(this.Date_ValueChanged);
            // 
            // DateText
            // 
            this.DateText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateText.Location = new System.Drawing.Point(0, 0);
            this.DateText.Name = "DateText";
            this.DateText.Size = new System.Drawing.Size(153, 20);
            this.DateText.TabIndex = 4;
            this.DateText.Visible = false;
            // 
            // ParseClipboard
            // 
            this.ParseClipboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ParseClipboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParseClipboard.Image = ((System.Drawing.Image)(resources.GetObject("ParseClipboard.Image")));
            this.ParseClipboard.Location = new System.Drawing.Point(0, 0);
            this.ParseClipboard.Margin = new System.Windows.Forms.Padding(0);
            this.ParseClipboard.Name = "ParseClipboard";
            this.ParseClipboard.Size = new System.Drawing.Size(12, 25);
            this.ParseClipboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ParseClipboard.TabIndex = 3;
            this.ParseClipboard.TabStop = false;
            this.ParseClipboard.Click += new System.EventHandler(this.ParseClipboard_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.ParseClipboard, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(165, 25);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Date);
            this.panel1.Controls.Add(this.DateText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(12, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 25);
            this.panel1.TabIndex = 4;
            // 
            // TimeConverterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TimeConverterControl";
            this.Size = new System.Drawing.Size(165, 25);
            ((System.ComponentModel.ISupportInitialize)(this.ParseClipboard)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker Date;
        private System.Windows.Forms.PictureBox ParseClipboard;
        private System.Windows.Forms.TextBox DateText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}
