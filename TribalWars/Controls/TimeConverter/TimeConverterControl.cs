using System;
using System.Windows.Forms;
using TribalWars.Worlds;

namespace TribalWars.Controls.TimeConverter
{
    /// <summary>
    /// Extended DateTimePicker for parsing Tribal Wars formatted dates
    /// </summary>
    public partial class TimeConverterControl : UserControl
    {
        #region Constructors
        public TimeConverterControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the DateTime value
        /// </summary>
        public DateTime Value
        {
            get { return Date.Value; }
            set { Date.Value = value; }
        }

        /// <summary>
        /// Gets or sets the custom DateTime format
        /// </summary>
        public string CustomFormat
        {
            get { return Date.CustomFormat; }
            set { Date.CustomFormat = value; }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Parses the current text on the clipboard and puts the date it in the DateTimePicker
        /// </summary>
        private void ParseClipboard_Click(object sender, EventArgs e)
        {
            string input = Clipboard.GetText().Trim();

            if (input.Length > 0 && World.Default.HasLoaded)
            {
                DateTime date;
                if (Tools.Parsers.CommonParsers.Date(input, out date))
                {
                    Value = date;
                }
                else
                {
                    MessageBox.Show("Copy a Tribal Wars date to paste the date here.");
                }
            }
        }
        #endregion
    }
}
