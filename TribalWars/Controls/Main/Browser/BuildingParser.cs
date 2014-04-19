using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TribalWars.Data;
using TribalWars.Data.Reporting;
using System.Text.RegularExpressions;
using TribalWars.Data.Villages;
using TribalWars.Tools.Parsers;
using TribalWars.Translations;

namespace TribalWars.Controls.Main.Browser
{
    /// <summary>
    /// Parser for the Production overview page
    /// </summary>
    public class BuildingParser : IBrowserParser
    {
        #region Constants
        #endregion

        #region Fields
        private Regex _documentRegex;
        #endregion

        #region IBrowserParser Members
        /// <summary>
        /// Gets the pattern for analysing the url
        /// </summary>
        public Regex UrlRegex
        {
            get { return null; }
        }

        /// <summary>
        /// Checks if the current page is handled by the 
        /// </summary>
        /// <param name="url">The browser Uri</param>
        public bool Handles(string url)
        {
            return url.IndexOf("screen=overview_villages&mode=buildings") > -1;
        }

        /// <summary>
        /// Handles the parsing of the document
        /// </summary>
        /// <param name="document">The document Html</param>
        /// <param name="serverTime">Time the page was generated</param>
        public bool Handle(string document, DateTime serverTime)
        {
            //&amp;screen=overview_villages&amp;mode=buildings&amp;order=stable&amp;dir=asc">
            int index = document.IndexOf(@"&amp;screen=overview_villages&amp;mode=buildings&amp;order=stable");
            if (index == -1) return false;

            string pattern = GetDocumentPattern();
            _documentRegex = new Regex(pattern, RegexOptions.Multiline);
            MatchCollection matches = _documentRegex.Matches(document, index);

            var ownVillages = World.Default.You.Player.Villages.ToDictionary(vil => vil.Id);
            if (matches.Count > 0)
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    Match match = matches[i];
                    if (match.Success)
                    {
                        HandleMatch(ownVillages, match, serverTime);
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Parses the details from the village match
        /// </summary>
        /// <param name="match">Match with village production details</param>
        /// <param name="serverTime">Time the page was generated</param>
        private void HandleMatch(Dictionary<int, Village> ownVillages, Match match, DateTime serverTime)
        {
            int villageId;
            if (CommonParsers.ParseInt(match.Groups["id"].Value, out villageId))
            {
                Village vil = ownVillages[villageId];
                CurrentSituation situation = vil.Reports.CurrentSituation;

                int tempInt;
                if (CommonParsers.ParseInt(match.Groups["points"].Value, out tempInt))
                    vil.Points = tempInt;

                // groups: warehouse, population / farm

                //System.Diagnostics.Debug.Print(vil.ToString());
                string pattern = string.Format(@"\<img src="".*\.png(\?1)?"" title=""({0}|{1}|{2})"" alt="""" /\>(?<res>(\d*<span class=""grey""\>\.\</span\>)?\d*)\s*", TWWords.Wood, TWWords.Clay, TWWords.Iron);
                var res = new Regex(pattern);
                MatchCollection resMatches = res.Matches(match.Groups["res"].Value);
                if (resMatches.Count == 3)
                {
                    if (CommonParsers.ParseInt(resMatches[0].Groups["res"].Value, out tempInt))
                        situation.Resources.Wood = tempInt;
                    if (CommonParsers.ParseInt(resMatches[1].Groups["res"].Value, out tempInt))
                        situation.Resources.Clay = tempInt;
                    if (CommonParsers.ParseInt(resMatches[2].Groups["res"].Value, out tempInt))
                        situation.Resources.Iron = tempInt;

                    situation.ResourcesDate = serverTime;
                }

           

                vil.Reports.Save();
            }
        }

        /// <summary>
        /// Builds the pattern for matching document input
        /// </summary>
        private string GetDocumentPattern()
        {
            var pattern = new StringBuilder();

            //<tr class="row_a">
            //    <td class="nowrap">	<span id="label_95233">
            //    <a href="/game.php?village=95233&amp;&amp;screen=main">
            //        <span id="label_text_95233">.Bledap (775|365) K37</span>	</a>
            	
            //    <a href="javascript:editToggle('label_95233', 'edit_95233')"><img src="/graphic/rename.png" alt="Rename" title="Rename" /></a>
            //</span>
            //<span id="edit_95233" style="display:none">
            //    <input id="edit_input_95233" size="" value=".Bledap"/> 
            //    <input type="button" value="OK" onclick="editSubmitNew('label_95233', 'label_text_95233', 'edit_95233', 'edit_input_95233', '/game.php?village=95233&amp;&amp;screen=main&amp;ajax=change_name');"/>
            //</span>	</td>
            //    <td>9<span class="grey">.</span>936</td>
            //            <td>20</td>
            //            <td>25</td>
            //            <td>20</td>
            //            <td>1</td>
            //            <td>3</td>
            //            <td>20</td>
            //            <td>1</td>
            //            <td>20</td>
            //            <td>30</td>
            //            <td>30</td>
            //            <td>30</td>
            //            <td>30</td>
            //            <td>30</td>
            //            <td>10</td>
            //            <td>20</td>
            //        <td></td>
            //</tr>

            pattern.Append(@"\<tr style=""white-space:nowrap"" class=""nowrap row_(a|b)""\>\s*");
            pattern.Append(@"\<td\>\s*");
            pattern.Append(@"\<span id=""label_(?<id>\d*)""\>\s*");
            pattern.Append(@"\<a href=""game\.php\?village=\d*&amp;screen=overview&amp;""\>\s*");
            pattern.Append(@"\<span id=""label_text_\d*"">.*\</span\>\s*");
            pattern.Append(@"\</a\>\s*");
            pattern.AppendFormat(@"\<a href=""javascript:editToggle\('label_\d*', 'edit_\d*'\)""\>\<img src="".*\.png(\?1)?"" alt=""{0}"" title=""{0}"" /\>\</a\>\s*", TWWords.Rename);
            pattern.Append(@"\</span\>\s*");
            pattern.Append(@"\<span id=""edit_\d*"" style=""display:none""\>\s*");
            pattern.Append(@"\<input id=""edit_input_\d*"" size="""" value="".*""/\>\s*");
            pattern.Append(@"\<input type=""button"" value=""OK"" onclick=""editSubmitNew\('label_\d*', 'label_text_\d*', 'edit_\d*', 'edit_input_\d*', '/game\.php\?village=\d*&amp;&amp;screen=main&amp;ajax=change_name'\);""/\>\s*");
            pattern.Append(@"\</span\>\s*");
            pattern.Append(@"\</td\>\s*");

            return pattern.ToString();
        }
        #endregion
    }
}
