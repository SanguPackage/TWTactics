using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TribalWars.Forms
{
    /// <summary>
    /// CURRENTLY NOT IN USE
    /// 
    /// Change settings for the program
    /// 
    /// There is some layout for changing proxy
    /// but not implemented (uses built in .NET method)
    /// </summary>
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }

        private void ConnectedDirect_CheckedChanged(object sender, EventArgs e)
        {
            ConnectedProxyGroupbox.Enabled = false;
        }

        private void ConnectedProxy_CheckedChanged(object sender, EventArgs e)
        {
            ConnectedProxyGroupbox.Enabled = true;
        }
    }
}
