using System;
using System.Windows.Forms;

namespace TribalWars.Forms.NotUsed
{
    /// <summary>
    /// Change settings for the program
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
	        Properties.Settings.Default.Proxy = !ConnectedDirect.Checked;
	        Properties.Settings.Default.ProxyAddress = ProxyAddress.Text;
			Properties.Settings.Default.ProxyPort = int.Parse(ProxyPort.Text);
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

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			ConnectedDirect.Checked = !Properties.Settings.Default.Proxy;
			ConnectedProxy.Checked = Properties.Settings.Default.Proxy;
			ProxyAddress.Text = Properties.Settings.Default.ProxyAddress;
			ProxyPort.Text = Properties.Settings.Default.ProxyPort.ToString();
		}
    }
}
