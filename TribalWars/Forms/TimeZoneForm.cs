using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data;

namespace TribalWars.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TimeZoneForm : Form
    {
        #region Fields
        private readonly World _world;
        #endregion

        #region Constructors
        public TimeZoneForm()
        {
            InitializeComponent();
        }

        public TimeZoneForm(World world)
        {
            _world = world;
            InitializeComponent();
        }
        #endregion

        private void TimeDisplayTimer_Tick(object sender, EventArgs e)
        {
            LocalTime.Text = DateTime.Now.ToString("MMMM dd yyyy HH:mm:ss");
            ServerTime.Text = _world.ServerTime.ToString("MMMM dd yyyy HH:mm:ss");
        }

        private void TimeOffset_ValueChanged(object sender, EventArgs e)
        {
            _world.ServerOffset = new TimeSpan((int)TimeOffset.Value, 0, 0);
        }

        private void TimeZoneForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _world.SaveSettings();
        }
    }
}
