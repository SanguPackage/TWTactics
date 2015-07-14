namespace TribalWars.Forms.Small
{
    partial class TimeZoneForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeZoneForm));
			this.gbTimeZome = new Janus.Windows.EditControls.UIGroupBox();
			this.ExplainationLabel = new System.Windows.Forms.Label();
			this.TimeOffset = new System.Windows.Forms.NumericUpDown();
			this.SecondsLabel = new System.Windows.Forms.Label();
			this.TimeAdjustmentLabel = new System.Windows.Forms.Label();
			this.ServerTime = new System.Windows.Forms.Label();
			this.LocalTime = new System.Windows.Forms.Label();
			this.ServerTimeLabel = new System.Windows.Forms.Label();
			this.YourLocalTimeLabel = new System.Windows.Forms.Label();
			this.TimeDisplayTimer = new System.Windows.Forms.Timer(this.components);
			this.CloseButton = new Janus.Windows.EditControls.UIButton();
			((System.ComponentModel.ISupportInitialize)(this.gbTimeZome)).BeginInit();
			this.gbTimeZome.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TimeOffset)).BeginInit();
			this.SuspendLayout();
			// 
			// gbTimeZome
			// 
			resources.ApplyResources(this.gbTimeZome, "gbTimeZome");
			this.gbTimeZome.Controls.Add(this.ExplainationLabel);
			this.gbTimeZome.Controls.Add(this.TimeOffset);
			this.gbTimeZome.Controls.Add(this.SecondsLabel);
			this.gbTimeZome.Controls.Add(this.TimeAdjustmentLabel);
			this.gbTimeZome.Controls.Add(this.ServerTime);
			this.gbTimeZome.Controls.Add(this.LocalTime);
			this.gbTimeZome.Controls.Add(this.ServerTimeLabel);
			this.gbTimeZome.Controls.Add(this.YourLocalTimeLabel);
			this.gbTimeZome.Name = "gbTimeZome";
			// 
			// ExplainationLabel
			// 
			resources.ApplyResources(this.ExplainationLabel, "ExplainationLabel");
			this.ExplainationLabel.Name = "ExplainationLabel";
			// 
			// TimeOffset
			// 
			resources.ApplyResources(this.TimeOffset, "TimeOffset");
			this.TimeOffset.Increment = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.TimeOffset.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
			this.TimeOffset.Minimum = new decimal(new int[] {
            86400,
            0,
            0,
            -2147483648});
			this.TimeOffset.Name = "TimeOffset";
			this.TimeOffset.ValueChanged += new System.EventHandler(this.TimeOffset_ValueChanged);
			// 
			// SecondsLabel
			// 
			resources.ApplyResources(this.SecondsLabel, "SecondsLabel");
			this.SecondsLabel.Name = "SecondsLabel";
			// 
			// TimeAdjustmentLabel
			// 
			resources.ApplyResources(this.TimeAdjustmentLabel, "TimeAdjustmentLabel");
			this.TimeAdjustmentLabel.Name = "TimeAdjustmentLabel";
			// 
			// ServerTime
			// 
			resources.ApplyResources(this.ServerTime, "ServerTime");
			this.ServerTime.Name = "ServerTime";
			// 
			// LocalTime
			// 
			resources.ApplyResources(this.LocalTime, "LocalTime");
			this.LocalTime.Name = "LocalTime";
			// 
			// ServerTimeLabel
			// 
			resources.ApplyResources(this.ServerTimeLabel, "ServerTimeLabel");
			this.ServerTimeLabel.Name = "ServerTimeLabel";
			// 
			// YourLocalTimeLabel
			// 
			resources.ApplyResources(this.YourLocalTimeLabel, "YourLocalTimeLabel");
			this.YourLocalTimeLabel.Name = "YourLocalTimeLabel";
			// 
			// TimeDisplayTimer
			// 
			this.TimeDisplayTimer.Enabled = true;
			this.TimeDisplayTimer.Interval = 200;
			this.TimeDisplayTimer.Tick += new System.EventHandler(this.TimeDisplayTimer_Tick);
			// 
			// CloseButton
			// 
			resources.ApplyResources(this.CloseButton, "CloseButton");
			this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseButton.Image")));
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// TimeZoneForm
			// 
			this.AcceptButton = this.CloseButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CloseButton;
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.gbTimeZome);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TimeZoneForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			((System.ComponentModel.ISupportInitialize)(this.gbTimeZome)).EndInit();
			this.gbTimeZome.ResumeLayout(false);
			this.gbTimeZome.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.TimeOffset)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox gbTimeZome;
        private System.Windows.Forms.Label ExplainationLabel;
        private System.Windows.Forms.NumericUpDown TimeOffset;
        private System.Windows.Forms.Label SecondsLabel;
        private System.Windows.Forms.Label TimeAdjustmentLabel;
        private System.Windows.Forms.Label ServerTime;
        private System.Windows.Forms.Label LocalTime;
        private System.Windows.Forms.Label ServerTimeLabel;
        private System.Windows.Forms.Label YourLocalTimeLabel;
        private System.Windows.Forms.Timer TimeDisplayTimer;
        private Janus.Windows.EditControls.UIButton CloseButton;
    }
}