using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

using System.Windows.Forms;
using System.Reflection;
using TribalWars.Data;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
using TribalWars.Data.Villages;

namespace TribalWars.Tools
{
    /// <summary>
    /// Common shared methods
    /// </summary>
    public static class Common
    {
        #region Point Methods
        /// <summary>
        /// Returns whether the point is inside TW boundaries
        /// </summary>
        public static bool IsValidGameCoordinate(this Point p)
        {
            return p.X > 0 && p.Y > 0 && p.X < 1000 && p.Y < 1000;
        }

        /// <summary>
        /// Gets the kingdom the point is located in
        /// </summary>
        public static int Kingdom(this Point p)
        {
            Debug.Assert(p.IsValidGameCoordinate());
            return (int) (Math.Floor((double) p.X / 100) + 10 * Math.Floor((double) p.Y / 100));
        }
        #endregion

        #region Villages
        /// <summary>
        /// Gets all distinct players in the parameter
        /// </summary>
        public static IEnumerable<Player> GetPlayers(this IEnumerable<Village> villages)
        {
            return villages.Where(x => x.HasPlayer).Select(x => x.Player).Distinct();
        }

        /// <summary>
        /// Gets all distinct tribes in the parameter
        /// </summary>
        public static IEnumerable<Tribe> GetTribes(this IEnumerable<Village> villages)
        {
            return villages.Where(x => x.HasTribe).Select(x => x.Player.Tribe).Distinct();
        }
        #endregion

        #region Number Methods
        /// <summary>
        /// Formats the number with an M after the million
        /// </summary>
        public static string GetPrettyNumber(int input)
        {
            if (input < 1000000) return input.ToString("#,0");
            return string.Format("{0}M", (input / 1000).ToString("#,0000"));
        }

        /// <summary>
        /// Pretty number display with proper sorting
        /// </summary>
        /// <remarks>Currently not in use</remarks>
        public class PrettyNumber : IComparable, IComparable<PrettyNumber>
        {
            private int _number;
            public int Number
            {
                get { return _number; }
            }
            public PrettyNumber(int number)
            {
                _number = number;
            }
            public override string ToString()
            {
                return Tools.Common.GetPrettyNumber(_number);
            }
            public int CompareTo(object obj)
            {
                return CompareTo(obj as PrettyNumber);
            }
            public int CompareTo(PrettyNumber other)
            {
                if (other == null) return -1;
                return _number - other.Number;
            }
        }
        #endregion

        #region Date Methods
        /// <summary>
        /// Print for displaying WorldData dates
        /// </summary>
        public static string PrintWorldDate(this DateTime value)
        {
            return value.ToString("dd MMM yyyy HH", System.Globalization.CultureInfo.InvariantCulture) + 'h';
        }

        /// <summary>
        /// Formats a date as on the Tribal Wars server
        /// </summary>
        public static string GetPrettyDate(DateTime date)
        {
            return GetPrettyDate(date, false);
        }

        /// <summary>
        /// Formats a date as on the Tribal Wars server
        /// </summary>
        /// <param name="date">The date to format</param>
        /// <param name="shortFormat">Removes 'on'</param>
        public static string GetPrettyDate(DateTime date, bool shortFormat)
        {
            DateTime serverTime = World.Default.ServerTime;
            if (date.DayOfYear == serverTime.DayOfYear)
            {
                if (shortFormat)
                    return "today at " + date.ToShortTimeString();
                else
                    return "today at " + date.ToLongTimeString();
            }
            else if (date.DayOfYear - 1 == serverTime.DayOfYear)
                return "tomorrow at " + date.ToLongTimeString();
            else if (shortFormat)
                return string.Format("{0} at {1}", date.ToString("dd.MM."), date.ToShortTimeString());
            else
                return string.Format("on {0} at {1}", date.ToString("dd.MM."), date.ToLongTimeString());
        }

        /// <summary>
        /// Short Tribal Wars server date format
        /// </summary>
        public static string GetShortPrettyDate(DateTime date)
        {
            DateTime serverTime = World.Default.ServerTime;
            return string.Format("{0} {1}", date.ToString("dd.MM."), date.ToLongTimeString());
        }
        #endregion

        #region Change Width PropertyGrid
        public static void MoveSplitter(PropertyGrid propertyGrid, int width)
        {
            object propertyGridView = typeof(PropertyGrid).InvokeMember("gridView", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance, null, propertyGrid, null);
            propertyGridView.GetType().InvokeMember("MoveSplitterTo", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, propertyGridView, new object[] { width });
        }
        #endregion
    }
}
