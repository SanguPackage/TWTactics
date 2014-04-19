using System;
using System.Text.RegularExpressions;
using TribalWars.Data;
using TribalWars.Data.Villages;
using System.Drawing;

namespace TribalWars.Controls.Main.Browser
{
    /// <summary>
    /// Makes the program aware a village has been selected
    /// </summary>
    public class VillageInfoHandler : IBrowserParser
    {
        #region Fields
        private Regex _regex;
        private const string DocumentPattern = @"\<tr\>\<td width=""80""\>Coordinates:\</td\>\<td\>(?<x>\d*)\|(?<y>\d*)\</td\>\</tr\>";
        #endregion

        #region Properties
        /// <summary>
        /// Not Implemented
        /// </summary>
        public Regex UrlRegex
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        /// <summary>
        /// Gets the regex to parse the document string
        /// </summary>
        public Regex Regex
        {
            get
            {
                if (_regex == null)
                {
                    _regex = new Regex(DocumentPattern, RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Multiline);
                }
                return _regex;
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Checks if the current page is handled by the parser
        /// </summary>
        /// <param name="url">The browser Uri</param>
        public bool Handles(string url)
        {
            return url.IndexOf("screen=info_village") > -1;
        }

        /// <summary>
        /// Handles the parsing of the document
        /// </summary>
        /// <param name="document">The document Html</param>
        /// <param name="serverTime">The time the page was generated</param>
        public bool Handle(string document, DateTime serverTime)
        {
            Match match = Regex.Match(document);
            if (match.Success)
            {
                var vil = new Point(Convert.ToInt32(match.Groups["x"].Value), Convert.ToInt32(match.Groups["y"].Value));
                if (World.Default.Villages.ContainsKey(vil))
                {
                    Village village = World.Default.Villages[vil];
                    World.Default.Map.EventPublisher.SelectVillages(null, village, VillageTools.SelectVillage);
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
