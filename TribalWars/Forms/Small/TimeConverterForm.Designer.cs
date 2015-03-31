namespace TribalWars.Forms.Small
{
    partial class TimeConverterForm
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
            this.timeConverterCalculatorControl1 = new TribalWars.Controls.TimeConverter.TimeConverterCalculatorControl();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timeConverterCalculatorControl1
            // 
            this.timeConverterCalculatorControl1.BackColor = System.Drawing.Color.Transparent;
            this.timeConverterCalculatorControl1.Location = new System.Drawing.Point(9, 46);
            this.timeConverterCalculatorControl1.Margin = new System.Windows.Forms.Padding(0);
            this.timeConverterCalculatorControl1.Name = "timeConverterCalculatorControl1";
            this.timeConverterCalculatorControl1.Size = new System.Drawing.Size(260, 23);
            this.timeConverterCalculatorControl1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "Set a date and add a walking time. (Backtiming anyone?)";
            // 
            // TimeConverterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 73);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeConverterCalculatorControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimeConverterForm";
            this.ShowIcon = false;
            this.Text = "Time adder";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TimeConverter.TimeConverterCalculatorControl timeConverterCalculatorControl1;
        private System.Windows.Forms.Label label1;
    }
}