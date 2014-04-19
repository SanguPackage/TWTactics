namespace TribalWars.Controls
{
    partial class LabelTextBox
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._Label = new System.Windows.Forms.Label();
            this._TextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this._Label);
            this.flowLayoutPanel1.Controls.Add(this._TextBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(56, 25);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // _Label
            // 
            this._Label.AutoSize = true;
            this._Label.Location = new System.Drawing.Point(3, 5);
            this._Label.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this._Label.Name = "_Label";
            this._Label.Size = new System.Drawing.Size(12, 13);
            this._Label.TabIndex = 5;
            this._Label.Text = "x";
            // 
            // _TextBox
            // 
            this._TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._TextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this._TextBox.Location = new System.Drawing.Point(21, 3);
            this._TextBox.Name = "_TextBox";
            this._TextBox.Size = new System.Drawing.Size(32, 20);
            this._TextBox.TabIndex = 4;
            this._TextBox.Leave += new System.EventHandler(this._TextBox_Leave);
            this._TextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this._TextBox_MouseDown);
            // 
            // LabelTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "LabelTextBox";
            this.Size = new System.Drawing.Size(56, 25);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label _Label;
        private System.Windows.Forms.TextBox _TextBox;

    }
}
