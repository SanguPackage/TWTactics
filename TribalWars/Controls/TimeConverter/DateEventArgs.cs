using System;

namespace TribalWars.Controls.TimeConverter
{
    public class DateEventArgs : EventArgs
    {
        public DateTime SelectedDate { get; private set; }

        public DateEventArgs(DateTime selectedDate)
        {
            SelectedDate = selectedDate;
        }
    }
}