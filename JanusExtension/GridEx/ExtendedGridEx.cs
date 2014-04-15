#region Using
using System;
using System.Collections.Generic;
using System.Text;
using Janus.Windows.GridEX;
using System.Drawing;
#endregion

namespace JanusExtension.GridEx
{
    /// <summary>
    /// Extended GridEx
    /// </summary>
    public class ExtendedGridEx : GridEX
    {
        #region Fields
        
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ExtendedGridEx()
            : this(true)
        {

        }

        public ExtendedGridEx(bool isEnabled)
        {
            VisualStyle = VisualStyle.Office2003;
            AcceptsEscape = false;
            AlternatingColors = true;
            AlternatingRowFormatStyle.BackColor = Color.FromArgb(247, 251, 238);
            //ColumnAutoResize = True
            GridLines = GridLines.Both;
            GroupByBoxVisible = false;
            GroupRowFormatStyle.BackColor = Color.FromArgb(189, 217, 120);
            GroupRowFormatStyle.BackColorGradient = Color.FromArgb(247, 251, 238);
            HideSelection = HideSelection.Highlight;
            SelectedFormatStyle.BackColor = Color.FromArgb(69, 103, 153);
            FocusCellFormatStyle.BackColor = Color.FromArgb(69, 103, 153);
            FocusCellFormatStyle.ForeColor = SystemColors.HighlightText;
            CellToolTip = CellToolTip.TruncatedText;
            TabKeyBehavior = TabKeyBehavior.ColumnNavigation;
            EnterKeyBehavior = EnterKeyBehavior.None;

            //BuiltInTexts(Janus.Windows.GridEX.GridEXBuiltInText.CalendarTodayButton) = "Vandaag";
            //BuiltInTexts(Janus.Windows.GridEX.GridEXBuiltInText.CalendarNoneButton) = "Geen";

            if (!isEnabled)
            {
                BackColor = Constants.ReadOnlyColor;
                RowFormatStyle.BackColor = Constants.ReadOnlyColor;
                AlternatingRowFormatStyle.BackColor = Constants.ReadOnlyColor;
            }
            else
            {
                ResetBackColor();
                RowFormatStyle.BackColor = Color.White;
            }
            TabStop = isEnabled;
        }
        #endregion
    }
}


///// <summary>
///// Het vertalen van Engelse teksten
///// </summary>
///// <param name="calendar">De Janus CalendarCombo die vertaald moet worden</param>
//public static void SetLayoutJanusCalendar(Janus.Windows.CalendarCombo.CalendarCombo calendar)
//{
//    calendar.TodayButtonText = My.Resources.MsgDateTimeToday;
//    calendar.NullButtonText = My.Resources.MsgDateTimeNone;
//}
///// <summary>
///// Het vertalen van Engelse teksten
///// </summary>
//public static void SetLayoutJanusCalendar(GridEX gridEx)
//{

//}
