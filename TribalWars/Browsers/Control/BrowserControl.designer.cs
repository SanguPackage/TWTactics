namespace TribalWars.Browsers.Control
{
    partial class BrowserControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserControl));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.BackButton = new System.Windows.Forms.ToolStripButton();
			this.ForwardButton = new System.Windows.Forms.ToolStripButton();
			this.StopButton = new System.Windows.Forms.ToolStripButton();
			this.RefreshButton = new System.Windows.Forms.ToolStripButton();
			this.HomeButton = new System.Windows.Forms.ToolStripButton();
			this.HomeStatsButton = new System.Windows.Forms.ToolStripButton();
			this.Browser = new System.Windows.Forms.WebBrowser();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.Url = new System.Windows.Forms.TextBox();
			this.GoButton = new System.Windows.Forms.Button();
			this.ParseResultLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.Browser, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// toolStrip1
			// 
			resources.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackButton,
            this.ForwardButton,
            this.StopButton,
            this.RefreshButton,
            this.HomeButton,
            this.HomeStatsButton});
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			// 
			// BackButton
			// 
			resources.ApplyResources(this.BackButton, "BackButton");
			this.BackButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BackButton.Name = "BackButton";
			this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
			// 
			// ForwardButton
			// 
			resources.ApplyResources(this.ForwardButton, "ForwardButton");
			this.ForwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ForwardButton.Name = "ForwardButton";
			this.ForwardButton.Click += new System.EventHandler(this.ForwardButton_Click);
			// 
			// StopButton
			// 
			resources.ApplyResources(this.StopButton, "StopButton");
			this.StopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.StopButton.Name = "StopButton";
			this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
			// 
			// RefreshButton
			// 
			resources.ApplyResources(this.RefreshButton, "RefreshButton");
			this.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.RefreshButton.Name = "RefreshButton";
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// HomeButton
			// 
			resources.ApplyResources(this.HomeButton, "HomeButton");
			this.HomeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.HomeButton.Name = "HomeButton";
			this.HomeButton.Click += new System.EventHandler(this.HomeButton_Click);
			// 
			// HomeStatsButton
			// 
			resources.ApplyResources(this.HomeStatsButton, "HomeStatsButton");
			this.HomeStatsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.HomeStatsButton.Name = "HomeStatsButton";
			this.HomeStatsButton.Click += new System.EventHandler(this.HomeStatsButton_Click);
			// 
			// Browser
			// 
			resources.ApplyResources(this.Browser, "Browser");
			this.Browser.Name = "Browser";
			this.Browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.Browser_DocumentCompleted);
			this.Browser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.Browser_Navigating);
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.Url);
			this.panel1.Controls.Add(this.GoButton);
			this.panel1.Controls.Add(this.ParseResultLabel);
			this.panel1.Name = "panel1";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Name = "label1";
			// 
			// Url
			// 
			resources.ApplyResources(this.Url, "Url");
			this.Url.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.Url.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
			this.Url.Name = "Url";
			this.Url.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Url_KeyDown);
			// 
			// GoButton
			// 
			resources.ApplyResources(this.GoButton, "GoButton");
			this.GoButton.FlatAppearance.BorderSize = 0;
			this.GoButton.Name = "GoButton";
			this.GoButton.UseVisualStyleBackColor = true;
			this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
			// 
			// ParseResultLabel
			// 
			resources.ApplyResources(this.ParseResultLabel, "ParseResultLabel");
			this.ParseResultLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.ParseResultLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ParseResultLabel.Name = "ParseResultLabel";
			// 
			// BrowserControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "BrowserControl";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BackButton;
        private System.Windows.Forms.ToolStripButton ForwardButton;
        private System.Windows.Forms.ToolStripButton StopButton;
        private System.Windows.Forms.ToolStripButton RefreshButton;
        private System.Windows.Forms.ToolStripButton HomeButton;
        private System.Windows.Forms.ToolStripButton HomeStatsButton;
        private System.Windows.Forms.WebBrowser Browser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Url;
        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.Label ParseResultLabel;

    }
}
