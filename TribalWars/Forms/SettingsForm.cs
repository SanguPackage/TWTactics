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
