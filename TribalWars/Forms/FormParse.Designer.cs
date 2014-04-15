namespace TribalWars
{
    partial class FormParse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParse));
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.cmdParse = new System.Windows.Forms.Button();
            this.cmdParseClip = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(12, 12);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput.Size = new System.Drawing.Size(444, 250);
            this.txtInput.TabIndex = 0;
            this.txtInput.Text = resources.GetString("txtInput.Text");
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 268);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(574, 415);
            this.txtOutput.TabIndex = 1;
            // 
            // cmdParse
            // 
            this.cmdParse.Location = new System.Drawing.Point(485, 41);
            this.cmdParse.Name = "cmdParse";
            this.cmdParse.Size = new System.Drawing.Size(113, 23);
            this.cmdParse.TabIndex = 2;
            this.cmdParse.Text = "Parse";
            this.cmdParse.UseVisualStyleBackColor = true;
            this.cmdParse.Click += new System.EventHandler(this.cmdParse_Click);
            // 
            // cmdParseClip
            // 
            this.cmdParseClip.Location = new System.Drawing.Point(485, 12);
            this.cmdParseClip.Name = "cmdParseClip";
            this.cmdParseClip.Size = new System.Drawing.Size(113, 23);
            this.cmdParseClip.TabIndex = 2;
            this.cmdParseClip.Text = "Parse clipboard";
            this.cmdParseClip.UseVisualStyleBackColor = true;
            this.cmdParseClip.Click += new System.EventHandler(this.cmdParseClip_Click);
            // 
            // FormParse
            // 
            this.AcceptButton = this.cmdParseClip;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 695);
            this.Controls.Add(this.cmdParseClip);
            this.Controls.Add(this.cmdParse);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtInput);
            this.Name = "FormParse";
            this.Text = "Parser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button cmdParse;
        private System.Windows.Forms.Button cmdParseClip;
    }
}