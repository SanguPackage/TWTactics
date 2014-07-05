using System;
using System.Windows.Forms;

namespace TribalWars.Forms
{
    /// <summary>
    /// Select the difference (in hours) between local system time
    /// and TW server time.
    /// </summary>
    public partial class TimeZoneForm : Form
    {
        #region Properties
        /// <summary>
        /// Gets the selected offset between local and server
        /// </summary>
        public TimeSpan ServerOffset { get; private set; }
        #endregion

        #region Constructors
        public TimeZoneForm()
        {
            InitializeComponent();
            ServerOffset = new TimeSpan();
        }
        #endregion

        private void TimeDisplayTimer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            LocalTime.Text = now.ToString("dd MMMM yyyy HH:mm:ss");
            ServerTime.Text = now.Add(ServerOffset).ToString("dd MMMM yyyy HH:mm:ss");
        }

        private void TimeOffset_ValueChanged(object sender, EventArgs e)
        {
            ServerOffset = new TimeSpan((int)TimeOffset.Value, 0, 0);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
