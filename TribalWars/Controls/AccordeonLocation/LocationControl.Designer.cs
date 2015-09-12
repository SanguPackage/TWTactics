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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationControl));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._locationFinderControl1 = new TribalWars.Controls.AccordeonLocation.LocationFinderControl();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.PlayerTribeSelectorButton = new Janus.Windows.EditControls.UIButton();
			this.PlayerTribeSelector = new TribalWars.Controls.Finders.PlayerTribeDropdown();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this._locationFinderControl1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// _locationFinderControl1
			// 
			resources.ApplyResources(this._locationFinderControl1, "_locationFinderControl1");
			this._locationFinderControl1.BackColor = System.Drawing.Color.Transparent;
			this._locationFinderControl1.Name = "_locationFinderControl1";
			// 
			// groupBox1
			// 
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Controls.Add(this.PlayerTribeSelectorButton);
			this.groupBox1.Controls.Add(this.PlayerTribeSelector);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// PlayerTribeSelectorButton
			// 
			resources.ApplyResources(this.PlayerTribeSelectorButton, "PlayerTribeSelectorButton");
			this.PlayerTribeSelectorButton.Image = global::TribalWars.Properties.Resources.teleport;
			this.PlayerTribeSelectorButton.Name = "PlayerTribeSelectorButton";
			this.PlayerTribeSelectorButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PlayerTribeSelectorButton_MouseClick);
			// 
			// PlayerTribeSelector
			// 
			resources.ApplyResources(this.PlayerTribeSelector, "PlayerTribeSelector");
			this.PlayerTribeSelector.AutoOpenOnFocus = false;
			this.PlayerTribeSelector.Name = "PlayerTribeSelector";
			// 
			// LocationControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "LocationControl";
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
