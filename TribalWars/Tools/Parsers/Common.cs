using System;
using System.Text.RegularExpressions;
using TribalWars.Data;

namespace TribalWars.Tools.Parsers
{
    /// <summary>
    /// Common RegEx methods
    /// </summary>
    public static class CommonParsers
    {
        #region Patterns
        private const string DatePattern = @"(?<date>(\w{3} \d{1,2}, \d{4})|today at|tomorrow at|((?<dateDay>\d{1,2})\.(?<dateMonth>\d{2})\. at)) (?<time>\d{1,2}:\d{1,2})(?<seconds>:\d{2})?";
        private const string ServerTimePattern = @"\<span id=""serverTime""\>(?<hour>\d{1,2}):(?<minute>\d{1,2}):(?<second>\d{1,2})\</span\>";
        #endregion

        #region Fields
        private static Regex _dateRegex;
        private static Regex _serverTimeRegex;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Regex for parsing TW Dates
        /// </summary>
        public static Regex DateRegex
        {
            get
            {
                if (_dateRegex == null)
                {
                    _dateRegex = new Regex(DatePattern, RegexOptions.Compiled);
                }
                return _dateRegex;
            }
        }

        /// <summary>
        /// Gets the Regex for parsing TW Server time
        /// </summary>
        public static Regex ServerTimeRegex
        {
            get
            {
                if (_serverTimeRegex == null)
                {
                    _serverTimeRegex = new Regex(ServerTimePattern, RegexOptions.Compiled);
                }
                return _serverTimeRegex;
            }
        }
        #endregion

        /// <summary>
        /// Parses a TW string to an int
        /// </summary>
        public static bool ParseInt(string input, out int output)
        {
            input = input.Replace(@"<span class=""grey"">.</span>", "");
            return int.TryParse(input, out output);
        }

        /// <summary>
        /// Parses any TW Date to a DateTime
        /// </summary>
        /// <param name="input">String date input</param>
        /// <param name="value">Output parsed DateTime</param>
        public static bool Date(string input, out DateTime value)
        {
            value = DateTime.MinValue;
            Match match = DateRegex.Match(input);
            if (match.Success)
            {
                string date = string.Empty;
                string datePart = match.Groups["date"].Value;
                if (datePart == "today at")
                    date = World.Default.ServerTime.ToString("MMM dd, yyyy", System.Globalization.CultureInfo.InvariantCulture);
                else if (datePart == "tomorrow at")
                    date = World.Default.ServerTime.AddDays(1).ToString("MMM dd, yyyy");
                else
                {
                    if (match.Groups["dateMonth"].Success)
                    {
                        // on 12.04. at 03:05
                        string tempDate = string.Format("{0}{1}{2}", World.Default.ServerTime.Year.ToString(), match.Groups["dateMonth"].Value, match.Groups["dateDay"].Value);
                        DateTime tempRealDate;
                        if (!DateTime.TryParseExact(tempDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out tempRealDate))
                            return false;

                        date = tempRealDate.ToString("MMM dd, yyyy");
                    }
                    else
                    {
                        //MMM, dd yyyy HH:mm:ss
                        date = datePart;
                    }
                }
                date += " " + match.Groups["time"].Value;
                if (match.Groups["seconds"] != null && match.Groups["seconds"].Value.Length != 0) date += match.Groups["seconds"].Value;
                else date += ":00";

                if (DateTime.TryParseExact(date, "MMM dd, yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out value))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Parses the current server time on a TW page
        /// </summary>
        /// <param name="input">String date input</param>
        /// <param name="value">Output parsed DateTime</param>
        public static bool ServerTime(string input, out DateTime value)
        {
            value = DateTime.MinValue;
            Match match = ServerTimeRegex.Match(input);
            if (match.Success)
            {
                int hour, minute, second;
                if (int.TryParse(match.Groups["hour"].Value, out hour) && int.TryParse(match.Groups["minute"].Value, out minute) && int.TryParse(match.Groups["second"].Value, out second))
                {
                    value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, second);
                    return true;
                }
            }
            return false;
        }
    }
}