namespace TribalWars.Controls.Common
{
    partial class TimeConverterCalculatorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeConverterCalculatorControl));
            this.TimeConverter = new TribalWars.Controls.Common.TimeConverterControl();
            this.AddTime = new System.Windows.Forms.PictureBox();
            this.ToAdd = new System.Windows.Forms.DateTimePicker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AddTime)).BeginInit();
            this.SuspendLayout();
            // 
            // TimeConverter
            // 
            this.TimeConverter.BackColor = System.Drawing.Color.Transparent;
            this.TimeConverter.CustomFormat = "MMM dd, yyyy HH:mm:ss";
            this.TimeConverter.Location = new System.Drawing.Point(0, 0);
            this.TimeConverter.Margin = new System.Windows.Forms.Padding(0);
            this.TimeConverter.Name = "TimeConverter";
            this.TimeConverter.Size = new System.Drawing.Size(165, 25);
            this.TimeConverter.TabIndex = 0;
            this.TimeConverter.Value = new System.DateTime(2008, 5, 1, 16, 8, 28, 937);
            // 
            // AddTime
            // 
            this.AddTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddTime.Image = ((System.Drawing.Image)(resources.GetObject("AddTime.Image")));
            this.AddTime.Location = new System.Drawing.Point(243, 5);
            this.AddTime.Margin = new System.Windows.Forms.Padding(0);
            this.AddTime.Name = "AddTime";
            this.AddTime.Size = new System.Drawing.Size(11, 11);
            this.AddTime.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.AddTime.TabIndex = 4;
            this.AddTime.TabStop = false;
            this.toolTip1.SetToolTip(this.AddTime, "Add the time to the date");
            this.AddTime.Click += new System.EventHandler(this.AddTime_Click);
            // 
            // ToAdd
            // 
            this.ToAdd.CustomFormat = "HH:mm:ss";
            this.ToAdd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToAdd.Location = new System.Drawing.Point(168, 0);
            this.ToAdd.Name = "ToAdd";
            this.ToAdd.ShowUpDown = true;
            this.ToAdd.Size = new System.Drawing.Size(72, 20);
            this.ToAdd.TabIndex = 5;
            this.ToAdd.Value = new System.DateTime(2008, 5, 1, 0, 0, 0, 0);
            // 
            // TimeConverterCalculatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ToAdd);
            this.Controls.Add(this.AddTime);
            this.Controls.Add(this.TimeConverter);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TimeConverterCalculatorControl";
            this.Size = new System.Drawing.Size(260, 23);
            ((System.ComponentModel.ISupportInitialize)(this.AddTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TimeConverterControl TimeConverter;
        private System.Windows.Forms.PictureBox AddTime;
        private System.Windows.Forms.DateTimePicker ToAdd;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
