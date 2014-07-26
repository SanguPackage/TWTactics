namespace TribalWars.Forms.NotUsed
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.CancelBtn = new Janus.Windows.EditControls.UIButton();
            this.OkButton = new Janus.Windows.EditControls.UIButton();
            this.uiTab1 = new Janus.Windows.UI.Tab.UITab();
            this.uiTabPage1 = new Janus.Windows.UI.Tab.UITabPage();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.ConnectedProxyGroupbox = new Janus.Windows.EditControls.UIGroupBox();
            this.editBox2 = new Janus.Windows.GridEX.EditControls.EditBox();
            this.editBox1 = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ConnectedProxy = new Janus.Windows.EditControls.UIRadioButton();
            this.ConnectedDirect = new Janus.Windows.EditControls.UIRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).BeginInit();
            this.uiTab1.SuspendLayout();
            this.uiTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConnectedProxyGroupbox)).BeginInit();
            this.ConnectedProxyGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(496, 410);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Location = new System.Drawing.Point(415, 410);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "OK";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // uiTab1
            // 
            this.uiTab1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiTab1.InputFocusTab = this.uiTabPage1;
            this.uiTab1.Location = new System.Drawing.Point(12, 12);
            this.uiTab1.Name = "uiTab1";
            this.uiTab1.Size = new System.Drawing.Size(559, 392);
            this.uiTab1.TabIndex = 0;
            this.uiTab1.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.uiTabPage1});
            // 
            // uiTabPage1
            // 
            this.uiTabPage1.Controls.Add(this.uiGroupBox1);
            this.uiTabPage1.Key = "Network";
            this.uiTabPage1.Location = new System.Drawing.Point(1, 21);
            this.uiTabPage1.Name = "uiTabPage1";
            this.uiTabPage1.Size = new System.Drawing.Size(555, 368);
            this.uiTabPage1.TabStop = true;
            this.uiTabPage1.Text = "Network";
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.uiGroupBox1.Controls.Add(this.ConnectedProxyGroupbox);
            this.uiGroupBox1.Controls.Add(this.ConnectedProxy);
            this.uiGroupBox1.Controls.Add(this.ConnectedDirect);
            this.uiGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(549, 362);
            this.uiGroupBox1.TabIndex = 0;
            // 
            // ConnectedProxyGroupbox
            // 
            this.ConnectedProxyGroupbox.Controls.Add(this.editBox2);
            this.ConnectedProxyGroupbox.Controls.Add(this.editBox1);
            this.ConnectedProxyGroupbox.Controls.Add(this.label2);
            this.ConnectedProxyGroupbox.Controls.Add(this.label1);
            this.ConnectedProxyGroupbox.Enabled = false;
            this.ConnectedProxyGroupbox.Location = new System.Drawing.Point(8, 86);
            this.ConnectedProxyGroupbox.Name = "ConnectedProxyGroupbox";
            this.ConnectedProxyGroupbox.Size = new System.Drawing.Size(363, 208);
            this.ConnectedProxyGroupbox.TabIndex = 2;
            this.ConnectedProxyGroupbox.Text = "Proxy settings";
            // 
            // editBox2
            // 
            this.editBox2.Location = new System.Drawing.Point(308, 23);
            this.editBox2.Name = "editBox2";
            this.editBox2.Size = new System.Drawing.Size(47, 20);
            this.editBox2.TabIndex = 2;
            // 
            // editBox1
            // 
            this.editBox1.Location = new System.Drawing.Point(59, 20);
            this.editBox1.Name = "editBox1";
            this.editBox1.Size = new System.Drawing.Size(207, 20);
            this.editBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address:";
            // 
            // ConnectedProxy
            // 
            this.ConnectedProxy.Location = new System.Drawing.Point(15, 61);
            this.ConnectedProxy.Name = "ConnectedProxy";
            this.ConnectedProxy.Size = new System.Drawing.Size(248, 23);
            this.ConnectedProxy.TabIndex = 1;
            this.ConnectedProxy.Text = "I connect with the internet through a proxy";
            this.ConnectedProxy.CheckedChanged += new System.EventHandler(this.ConnectedProxy_CheckedChanged);
            // 
            // ConnectedDirect
            // 
            this.ConnectedDirect.Checked = true;
            this.ConnectedDirect.Location = new System.Drawing.Point(15, 32);
            this.ConnectedDirect.Name = "ConnectedDirect";
            this.ConnectedDirect.Size = new System.Drawing.Size(248, 23);
            this.ConnectedDirect.TabIndex = 0;
            this.ConnectedDirect.TabStop = true;
            this.ConnectedDirect.Text = "I am connected directly to the internet";
            this.ConnectedDirect.CheckedChanged += new System.EventHandler(this.ConnectedDirect_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(583, 439);
            this.Controls.Add(this.uiTab1);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.CancelBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).EndInit();
            this.uiTab1.ResumeLayout(false);
            this.uiTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ConnectedProxyGroupbox)).EndInit();
            this.ConnectedProxyGroupbox.ResumeLayout(false);
            this.ConnectedProxyGroupbox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton CancelBtn;
        private Janus.Windows.EditControls.UIButton OkButton;
        private Janus.Windows.UI.Tab.UITab uiTab1;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox ConnectedProxyGroupbox;
        private Janus.Windows.EditControls.UIRadioButton ConnectedProxy;
        private Janus.Windows.EditControls.UIRadioButton ConnectedDirect;
        private Janus.Windows.GridEX.EditControls.EditBox editBox2;
        private Janus.Windows.GridEX.EditControls.EditBox editBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}