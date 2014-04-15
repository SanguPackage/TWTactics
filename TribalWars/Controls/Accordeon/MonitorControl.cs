using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TribalWars.Controls.Accordeon
{
    public partial class MonitorControl : UserControl
    {
        #region Constructors
        public MonitorControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers
        private void StartButton_Click(object sender, EventArgs e)
        {
            World.Default.Monitor.InspectCurrentData();
        }
        #endregion
    }
}
