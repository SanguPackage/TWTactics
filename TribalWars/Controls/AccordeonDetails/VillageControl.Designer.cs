namespace TribalWars.Controls.AccordeonDetails
{
    partial class VillageControl
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
            this.ReportCanvas = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ReportCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportCanvas
            // 
            this.ReportCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(232)))));
            this.ReportCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportCanvas.Location = new System.Drawing.Point(0, 0);
            this.ReportCanvas.Margin = new System.Windows.Forms.Padding(0);
            this.ReportCanvas.Name = "ReportCanvas";
            this.ReportCanvas.Size = new System.Drawing.Size(121, 117);
            this.ReportCanvas.TabIndex = 1;
            this.ReportCanvas.TabStop = false;
            this.ReportCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.ReportCanvas_Paint);
            // 
            // VillageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ReportCanvas);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "VillageControl";
            this.Size = new System.Drawing.Size(121, 117);
            ((System.ComponentModel.ISupportInitialize)(this.ReportCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ReportCanvas;


    }
}
