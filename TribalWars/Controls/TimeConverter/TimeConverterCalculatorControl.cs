using System;
using System.Windows.Forms;

namespace TribalWars.Controls.TimeConverter
{
    /// <summary>
    /// Adds addings time functionality to the TimeConverterControl
    /// </summary>
    public partial class TimeConverterCalculatorControl : UserControl
    {
        #region Constructors
        public TimeConverterCalculatorControl()
        {
            InitializeComponent();
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            TimeConverter.Value = DateTime.Today;
        }

        #region Event Handlers
        /// <summary>
        /// Adds the time to the original date value
        /// </summary>
        private void AddTime_Click(object sender, EventArgs e)
        {
            if (ToAdd.Value.Hour == 0 && ToAdd.Value.Minute == 0 && ToAdd.Value.Second == 0)
            {
                MessageBox.Show("Specify the time in the right box (format: HH:MM:SS (hours, minutes, seconds)) to be added to the time in the box left." 
                    + Environment.NewLine 
                    + "This can be handy when you need to calculate the time to send your troops.");
            }
            else
            {
                var span = new TimeSpan(ToAdd.Value.Hour, ToAdd.Value.Minute, ToAdd.Value.Second);
                TimeConverter.Value = TimeConverter.Value.Add(span);
            }
        }
        #endregion
    }
}
