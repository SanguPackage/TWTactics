namespace TribalWars.Forms.Small
{
    partial class ActivePlayerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivePlayerForm));
            this.OkButton = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.You = new TribalWars.Controls.Finders.PlayerTribeDropdown();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.YourMarker = new TribalWars.Maps.Markers.MarkerSettingsControl();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.YourTribeMarker = new TribalWars.Maps.Markers.MarkerSettingsControl();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton.Image = ((System.Drawing.Image)(resources.GetObject("OkButton.Image")));
            this.OkButton.Location = new System.Drawing.Point(11, 176);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(296, 46);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "OK";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.You);
            this.uiGroupBox1.Location = new System.Drawing.Point(12, 6);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(295, 50);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "You";
            // 
            // You
            // 
            this.You.AllowTribe = false;
            this.You.AutoOpenOnFocus = false;
            this.You.BackColor = System.Drawing.Color.Transparent;
            this.You.Location = new System.Drawing.Point(6, 19);
            this.You.Margin = new System.Windows.Forms.Padding(0);
            this.You.Name = "You";
            this.You.Size = new System.Drawing.Size(283, 28);
            this.You.TabIndex = 0;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.YourMarker);
            this.uiGroupBox2.Location = new System.Drawing.Point(12, 64);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(295, 50);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "Your marker";
            // 
            // YourMarker
            // 
            this.YourMarker.AllowBarbarianViews = false;
            this.YourMarker.AutoUpdateMarkers = false;
            this.YourMarker.BackColor = System.Drawing.Color.Transparent;
            this.YourMarker.CanDeactivate = false;
            this.YourMarker.DefaultExtraMarkerColor = System.Drawing.Color.Transparent;
            this.YourMarker.DefaultMarkerColor = System.Drawing.Color.Black;
            this.YourMarker.Location = new System.Drawing.Point(7, 20);
            this.YourMarker.Margin = new System.Windows.Forms.Padding(0);
            this.YourMarker.Name = "YourMarker";
            this.YourMarker.Size = new System.Drawing.Size(282, 25);
            this.YourMarker.TabIndex = 1;
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.YourTribeMarker);
            this.uiGroupBox3.Location = new System.Drawing.Point(12, 120);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(295, 50);
            this.uiGroupBox3.TabIndex = 2;
            this.uiGroupBox3.Text = "Your tribe marker";
            // 
            // YourTribeMarker
            // 
            this.YourTribeMarker.AllowBarbarianViews = false;
            this.YourTribeMarker.AutoUpdateMarkers = false;
            this.YourTribeMarker.BackColor = System.Drawing.Color.Transparent;
            this.YourTribeMarker.CanDeactivate = false;
            this.YourTribeMarker.DefaultExtraMarkerColor = System.Drawing.Color.Transparent;
            this.YourTribeMarker.DefaultMarkerColor = System.Drawing.Color.Black;
            this.YourTribeMarker.Location = new System.Drawing.Point(7, 20);
            this.YourTribeMarker.Margin = new System.Windows.Forms.Padding(0);
            this.YourTribeMarker.Name = "YourTribeMarker";
            this.YourTribeMarker.Size = new System.Drawing.Size(282, 25);
            this.YourTribeMarker.TabIndex = 2;
            // 
            // ActivePlayerForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 230);
            this.Controls.Add(this.uiGroupBox3);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActivePlayerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select active player";
            this.Load += new System.EventHandler(this.ActivePlayerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton OkButton;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Controls.Finders.PlayerTribeDropdown You;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Maps.Markers.MarkerSettingsControl YourMarker;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Maps.Markers.MarkerSettingsControl YourTribeMarker;
    }
}