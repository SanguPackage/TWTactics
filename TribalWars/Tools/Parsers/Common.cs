using System;
using System.Globalization;
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
        private const string ServerTimePattern = @"\<span id=""serverTime""\>(?<hour>\d{1,2}):(?<minute>\d{1,2}):(?<second>\d{1,2})\</span\>";

        /// <summary>
        /// Holds the data to parse 
        /// </summary>
        private class DatePatternInfo
        {
            private const string DatePatternToday = "today at";
            private const string DatePatternTomorrow = "tomorrow at";
            private const string DatePatternAtTime = "at";

            private const string DutchDatePatternToday = "vandaag om";
            private const string DutchDatePatternTomorrow = "morgen om";
            private const string DutchDatePatternAtTime = "om";

            private const string DatePattern = @"(?<date>(\w{3} \d{1,2}, \d{4})|{TODAY}|{TOMORROW}|((?<dateDay>\d{1,2})\.(?<dateMonth>\d{2})\. {TIMEAT})) (?<time>\d{1,2}:\d{1,2})(?<seconds>:\d{2})?";

            private readonly Regex _pattern;

            public string Today { get; private set; }
            public string Tomorrow { get; private set; }

            private DatePatternInfo(string regexPattern, string today, string tomorrow, string timeAt)
            {
                _pattern = new Regex(regexPattern.Replace("{TODAY}", today).Replace("{TOMORROW}", tomorrow).Replace("{TIMEAT}", timeAt));
                Today = today;
                Tomorrow = tomorrow;
            }

            public Match Match(string input)
            {
                return _pattern.Match(input);
            }

            public static DatePatternInfo GetInfo(CultureInfo culture)
            {
                switch (culture.Name)
                {
                    case "nl-BE":
                    case "nl-NL":
                        return new DatePatternInfo(DatePattern, DutchDatePatternToday, DutchDatePatternTomorrow, DutchDatePatternAtTime);

                    default:
                        return new DatePatternInfo(DatePattern, DatePatternToday, DatePatternTomorrow, DatePatternAtTime);
                }
            }
        }
        
        #endregion

        #region Fields
        private static Regex _serverTimeRegex;
        #endregion

        #region Properties
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
            var patternMatcher = DatePatternInfo.GetInfo(World.Default.Culture);
            Match match = patternMatcher.Match(input);
            if (match.Success)
            {
                string date = string.Empty;
                string datePart = match.Groups["date"].Value;
                if (datePart == patternMatcher.Today)
                {
                    date = World.Default.ServerTime.ToString("MMM dd, yyyy", CultureInfo.InvariantCulture);
                }
                else if (datePart == patternMatcher.Tomorrow)
                {
                    date = World.Default.ServerTime.AddDays(1).ToString("MMM dd, yyyy");
                }
                else
                {
                    if (match.Groups["dateMonth"].Success)
                    {
                        // on 12.04. at 03:05
                        string tempDate = string.Format("{0}{1}{2}", World.Default.ServerTime.Year.ToString(), match.Groups["dateMonth"].Value, match.Groups["dateDay"].Value);
                        DateTime tempRealDate;
                        if (!DateTime.TryParseExact(tempDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out tempRealDate))
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

                if (DateTime.TryParseExact(date, "MMM dd, yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out value))
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