using TribalWars.Controls.Finders;
using TribalWars.Worlds.Events.Impls;

namespace TribalWars.Controls.AccordeonLocation
{
    partial class LocationControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PlayerTribeSelectorButton = new Janus.Windows.EditControls.UIButton();
            this._locationFinderControl1 = new TribalWars.Controls.AccordeonLocation.LocationFinderControl();
            this.PlayerTribeSelector = new TribalWars.Controls.Finders.PlayerTribeDropdown();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this._locationFinderControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(304, 324);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PlayerTribeSelectorButton);
            this.groupBox1.Controls.Add(this.PlayerTribeSelector);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 44);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search players and tribes";
            // 
            // PlayerTribeSelectorButton
            // 
            this.PlayerTribeSelectorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerTribeSelectorButton.Image = global::TribalWars.Properties.Resources.teleport;
            this.PlayerTribeSelectorButton.Location = new System.Drawing.Point(264, 15);
            this.PlayerTribeSelectorButton.Name = "PlayerTribeSelectorButton";
            this.PlayerTribeSelectorButton.Size = new System.Drawing.Size(28, 24);
            this.PlayerTribeSelectorButton.TabIndex = 1;
            this.PlayerTribeSelectorButton.ToolTipText = "Center and pinpoint or right click for more options";
            this.PlayerTribeSelectorButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PlayerTribeSelectorButton_MouseClick);
            // 
            // _locationFinderControl1
            // 
            this._locationFinderControl1.BackColor = System.Drawing.Color.Transparent;
            this._locationFinderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._locationFinderControl1.Location = new System.Drawing.Point(2, 50);
            this._locationFinderControl1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this._locationFinderControl1.Name = "_locationFinderControl1";
            this._locationFinderControl1.Size = new System.Drawing.Size(300, 272);
            this._locationFinderControl1.TabIndex = 7;
            // 
            // PlayerTribeSelector
            // 
            this.PlayerTribeSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerTribeSelector.AutoOpenOnFocus = false;
            this.PlayerTribeSelector.Location = new System.Drawing.Point(3, 16);
            this.PlayerTribeSelector.Margin = new System.Windows.Forms.Padding(0);
            this.PlayerTribeSelector.Name = "PlayerTribeSelector";
            this.PlayerTribeSelector.Size = new System.Drawing.Size(258, 22);
            this.PlayerTribeSelector.TabIndex = 0;
            // 
            // LocationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LocationControl";
            this.Size = new System.Drawing.Size(304, 324);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LocationFinderControl _locationFinderControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private PlayerTribeDropdown PlayerTribeSelector;
        private Janus.Windows.EditControls.UIButton PlayerTribeSelectorButton;

    }
}
