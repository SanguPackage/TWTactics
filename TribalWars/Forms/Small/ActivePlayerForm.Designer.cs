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
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.groupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.YourTribeMarker = new TribalWars.Maps.Markers.MarkerSettingsControl();
            this.YourMarker = new TribalWars.Maps.Markers.MarkerSettingsControl();
            this.You = new TribalWars.Controls.Finders.PlayerTribeDropdown();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton.Image = ((System.Drawing.Image)(resources.GetObject("OkButton.Image")));
            this.OkButton.Location = new System.Drawing.Point(4, 264);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(663, 46);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "OK";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.You);
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(8, 148);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(295, 57);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Select yourself";
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.YourMarker);
            this.uiGroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Location = new System.Drawing.Point(309, 148);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(358, 50);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "Select the  marker for your villages";
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.YourTribeMarker);
            this.uiGroupBox3.Location = new System.Drawing.Point(309, 204);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(358, 50);
            this.uiGroupBox3.TabIndex = 2;
            this.uiGroupBox3.Text = "Select the marker for players in your tribe";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 129);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.Text = "Why do you want to know who I am?";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 110);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(309, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(356, 129);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.Text = "How do these crazy markers work?";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(350, 110);
            this.label2.TabIndex = 0;
            this.label2.Text = resources.GetString("label2.Text");
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
            this.YourTribeMarker.Size = new System.Drawing.Size(348, 25);
            this.YourTribeMarker.TabIndex = 2;
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
            this.YourMarker.Size = new System.Drawing.Size(348, 25);
            this.YourMarker.TabIndex = 1;
            // 
            // You
            // 
            this.You.AllowTribe = false;
            this.You.AutoOpenOnFocus = false;
            this.You.BackColor = System.Drawing.Color.Transparent;
            this.You.Location = new System.Drawing.Point(5, 27);
            this.You.Margin = new System.Windows.Forms.Padding(0);
            this.You.Name = "You";
            this.You.Size = new System.Drawing.Size(283, 31);
            this.You.TabIndex = 0;
            // 
            // ActivePlayerForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 322);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.uiGroupBox3);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActivePlayerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select active player";
            this.Load += new System.EventHandler(this.ActivePlayerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
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
        private Janus.Windows.EditControls.UIGroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIGroupBox groupBox2;
        private System.Windows.Forms.Label label2;
    }
}