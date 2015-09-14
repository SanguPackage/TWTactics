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
			this.ProxyPort = new Janus.Windows.GridEX.EditControls.EditBox();
			this.ProxyAddress = new Janus.Windows.GridEX.EditControls.EditBox();
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
			resources.ApplyResources(this.CancelBtn, "CancelBtn");
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// OkButton
			// 
			resources.ApplyResources(this.OkButton, "OkButton");
			this.OkButton.Name = "OkButton";
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// uiTab1
			// 
			resources.ApplyResources(this.uiTab1, "uiTab1");
			this.uiTab1.InputFocusTab = this.uiTabPage1;
			this.uiTab1.Name = "uiTab1";
			this.uiTab1.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.uiTabPage1});
			// 
			// uiTabPage1
			// 
			this.uiTabPage1.Controls.Add(this.uiGroupBox1);
			this.uiTabPage1.Key = "Network";
			resources.ApplyResources(this.uiTabPage1, "uiTabPage1");
			this.uiTabPage1.Name = "uiTabPage1";
			this.uiTabPage1.TabStop = true;
			// 
			// uiGroupBox1
			// 
			this.uiGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.uiGroupBox1.Controls.Add(this.ConnectedProxyGroupbox);
			this.uiGroupBox1.Controls.Add(this.ConnectedProxy);
			this.uiGroupBox1.Controls.Add(this.ConnectedDirect);
			resources.ApplyResources(this.uiGroupBox1, "uiGroupBox1");
			this.uiGroupBox1.Name = "uiGroupBox1";
			// 
			// ConnectedProxyGroupbox
			// 
			this.ConnectedProxyGroupbox.Controls.Add(this.ProxyPort);
			this.ConnectedProxyGroupbox.Controls.Add(this.ProxyAddress);
			this.ConnectedProxyGroupbox.Controls.Add(this.label2);
			this.ConnectedProxyGroupbox.Controls.Add(this.label1);
			resources.ApplyResources(this.ConnectedProxyGroupbox, "ConnectedProxyGroupbox");
			this.ConnectedProxyGroupbox.Name = "ConnectedProxyGroupbox";
			// 
			// ProxyPort
			// 
			resources.ApplyResources(this.ProxyPort, "ProxyPort");
			this.ProxyPort.Name = "ProxyPort";
			// 
			// ProxyAddress
			// 
			resources.ApplyResources(this.ProxyAddress, "ProxyAddress");
			this.ProxyAddress.Name = "ProxyAddress";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// ConnectedProxy
			// 
			resources.ApplyResources(this.ConnectedProxy, "ConnectedProxy");
			this.ConnectedProxy.Name = "ConnectedProxy";
			this.ConnectedProxy.CheckedChanged += new System.EventHandler(this.ConnectedProxy_CheckedChanged);
			// 
			// ConnectedDirect
			// 
			this.ConnectedDirect.Checked = true;
			resources.ApplyResources(this.ConnectedDirect, "ConnectedDirect");
			this.ConnectedDirect.Name = "ConnectedDirect";
			this.ConnectedDirect.TabStop = true;
			this.ConnectedDirect.CheckedChanged += new System.EventHandler(this.ConnectedDirect_CheckedChanged);
			// 
			// SettingsForm
			// 
			this.AcceptButton = this.OkButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CancelBtn;
			this.Controls.Add(this.uiTab1);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.CancelBtn);
			this.MaximizeBox = false;
			this.Name = "SettingsForm";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SettingsForm_Load);
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
        private Janus.Windows.GridEX.EditControls.EditBox ProxyPort;
        private Janus.Windows.GridEX.EditControls.EditBox ProxyAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}