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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeConverterForm));
			this.timeConverterCalculatorControl1 = new TribalWars.Controls.TimeConverter.TimeConverterCalculatorControl();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// timeConverterCalculatorControl1
			// 
			resources.ApplyResources(this.timeConverterCalculatorControl1, "timeConverterCalculatorControl1");
			this.timeConverterCalculatorControl1.BackColor = System.Drawing.Color.Transparent;
			this.timeConverterCalculatorControl1.Name = "timeConverterCalculatorControl1";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// TimeConverterForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.timeConverterCalculatorControl1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TimeConverterForm";
			this.ShowIcon = false;
			this.TopMost = true;
			this.ResumeLayout(false);

        }

        #endregion

        private Controls.TimeConverter.TimeConverterCalculatorControl timeConverterCalculatorControl1;
        private System.Windows.Forms.Label label1;
    }
}