using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TribalWars.Browsers.Reporting;
using TribalWars.Browsers.Translations;
using System.Text.RegularExpressions;
using TribalWars.Tools.Parsers;
using TribalWars.Villages;
using TribalWars.Worlds;

namespace TribalWars.Browsers.Parsers
{
    /// <summary>
    /// Parser for the Production overview page
    /// </summary>
    public class ResourcesParser : IBrowserParser
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
            return url.IndexOf("screen=overview_villages") > -1 && url.IndexOf("mode=prod") > -1;
        }

        /// <summary>
        /// Handles the parsing of the document
        /// </summary>
        /// <param name="document">The document Html</param>
        /// <param name="serverTime">Time the page was generated</param>
        public bool Handle(string document, DateTime serverTime)
        {
            int index = document.IndexOf(string.Format("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th>", TWWords.Village, TWWords.Points, TWWords.Resources, TWWords.Warehouse, TWWords.Farm));
            if (index == -1) return false;

            string pattern = GetDocumentPattern();
            _documentRegex = new Regex(pattern, RegexOptions.Multiline);
            MatchCollection matches = _documentRegex.Matches(document, index);

            var ownVillages = World.Default.You.Villages.ToDictionary(vil => vil.Id);
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
        private void HandleMatch(Dictionary<int, Village> ownVillages, Match match, DateTime serverTime)
        {
            int villageId;
            if (CommonParsers.ParseInt(match.Groups["id"].Value, out villageId))
            {
                Village vil = null;
                if (ownVillages.ContainsKey(villageId))
                    vil = ownVillages[villageId];
                else
                {
                    // newly conquered village: change owner
                    foreach (Village village in World.Default.Villages.Values)
                    {
                        if (village.Id == villageId)
                        {
                            vil = village;
                            vil.Player = World.Default.You;
                        }
                    }
                }

                if (vil != null)
                {
                    CurrentSituation situation = vil.Reports.CurrentSituation;

                    int tempInt;
                    if (CommonParsers.ParseInt(match.Groups["points"].Value, out tempInt))
                        vil.Points = tempInt;

                    string pattern = string.Format(@"(\<img src="".*\.png(\?1)?"" title=""({0}|{1}|{2})"" alt="""" /\>(\<span class=""warn""\>)?(?<res>(\d*<span class=""grey""\>\.\</span\>)?\d*)(\</span\>)?\s*)", TWWords.Wood, TWWords.Clay, TWWords.Iron);
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
        }

        /// <summary>
        /// Builds the pattern for matching document input
        /// </summary>
        private string GetDocumentPattern()
        {
            var pattern = new StringBuilder();

            //<tr style="white-space:nowrap" class="nowrap row_a">
            //<td>
            //<span id="label_95233">
            //    <a href="game.php?village=95233&amp;screen=overview&amp;">1
            //        <span id="label_text_95233">.Bledap (775|365) K37</span>2
            //    </a>3
            //    4<a href="javascript:editToggle('label_95233', 'edit_95233')"><img src="/graphic/rename.png" alt="Rename" title="Rename" /></a>
            //</span>5
            //<span id="edit_95233" style="display:none">6
            //    <input id="edit_input_95233" size="" value=".Bledap"/>7 
            //    8<input type="button" value="OK" onclick="editSubmitNew('label_95233', 'label_text_95233', 'edit_95233', 'edit_input_95233', '/game.php?village=95233&amp;&amp;screen=main&amp;ajax=change_name');"/>
            //</span>9
            //</td>10
            //<td>9<span class="grey">.</span>936</td>
            //<td align="center">
            //    <img src="/graphic/holz.png" title="Wood" alt="" />154<span class="grey">.</span>048 <img src="/graphic/lehm.png" title="Clay" alt="" />289<span class="grey">.</span>290 <img src="/graphic/eisen.png" title="Iron" alt="" />295<span class="grey">.</span>967 </td>
            //<td>400000</td>
            //<td>23996/24000</td>
            //<td>tomorrow at 14:39<br />
        	//<img src="/graphic/buildings/barracks.png" title="Barracks - today at 22:30" alt="" /> <img src="/graphic/buildings/stable.png" title="Stable - tomorrow at 03:23" alt="" /> <img src="/graphic/buildings/stable.png" title="Stable - tomorrow at 09:14" alt="" /> <img src="/graphic/overview/down.png" title="Demolition" alt="" /><img src="/graphic/buildings/main.png" title="Village Headquarters - tomorrow at 11:51" alt="" /> <img src="/graphic/overview/down.png" title="Demolition" alt="" /><img src="/graphic/buildings/main.png" title="Village Headquarters - tomorrow at 14:09" alt="" /> <img src="/graphic/overview/down.png" title="Demolition" alt="" /><img src="/graphic/buildings/garage.png" title="Workshop - tomorrow at 14:39" alt="" /></td>
            //<td><img src="/graphic/unit/unit_spear.png" title="Spear fighter - today at 22:04" alt="" /><img src="/graphic/unit/unit_sword.png" title="Swordsman - today at 23:32" alt="" /><img src="/graphic/overview/down.png" title="Demolition" alt="" /><img src="/graphic/unit/unit_sword.png" title="Swordsman - tomorrow at 00:02" alt="" /><img src="/graphic/overview/down.png" title="Demolition" alt="" /><img src="/graphic/unit/unit_heavy.png" title="Heavy cavalry - tomorrow at 00:18" alt="" /><img src="/graphic/unit/unit_spy.png" title="Scout - tomorrow at 00:25" alt="" /><img src="/graphic/overview/down.png" title="Demolition" alt="" /></td>
            //<td><img src="/graphic/unit/unit_spear.png" title="299 - today at 22:18" alt="" /><img src="/graphic/unit/unit_heavy.png" title="76 - tomorrow at 04:17" alt="" /></td>
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

            pattern.Append(@"\<td\>(?<points>(\d*<span class=""grey""\>\.\</span\>)?\d*)\</td\>\s*");
            pattern.Append(@"\<td align=""center""\>\s*");
            pattern.AppendFormat(@"(?<res>(\<img src="".*\.png(\?1)?"" title=""({0}|{1}|{2})"" alt="""" /\>(\<span class=""warn""\>)?(\d*<span class=""grey""\>\.\</span\>)?\d*(\</span\>)?\s*){{3}})", TWWords.Wood, TWWords.Clay, TWWords.Iron);
            pattern.Append(@"\</td\>\s*");

            //	<td align="center"><img src="/graphic/holz.png" title="Wood" alt="" />70<span class="grey">.</span>965 
            //<img src="/graphic/lehm.png" title="Clay" alt="" />89<span class="grey">.</span>611 
            //<img src="/graphic/eisen.png" title="Iron" alt="" /><span class="warn">175<span class="grey">.</span>047</span> </td>

            //<span id="wood" title="3600" class="warn">400000</span>&nbsp;</td>

            pattern.Append(@"\<td\>(?<warehouse>\d*)\</td\>\s*");
            pattern.Append(@"\<td\>(?<population>\d*)/(?<farm>\d*)\</td\>\s*");
            pattern.Append(@"\<td\>\s*");
            pattern.Append(@"((?<buildingDoneDate>.*)\<br /\>\s*");

            pattern.Append(@"((?<buildings>(\<img src="".*/graphic/overview/down.png"" title=""Demolition"" alt="""" /\>\s*)?\<img src="".*/graphic/buildings/.*\.png(\?1)?"" title="".*"" alt="""" /\>\s*)*))?");
            pattern.Append(@"\</td\>\s*");
            pattern.Append(@"\<td\>\s*");
            pattern.Append(@"(?<smithy>(\<img src="".*\.png(\?1)?"" title="".*"" alt="""" /\>\s*(\<img src="".*/graphic/overview/down\.png(\?1)?"" title="".*"" alt="""" /\>)?)*)");
            pattern.Append(@"\</td\>\s*");
            pattern.Append(@"\<td\>\s*");

            pattern.Append(@"(?<training>(\<img src="".*\.png(\?1)?"" title="".*"" alt="""" /\>)*)");
            pattern.Append(@"\</td\>\s*\");
            pattern.Append(@"</tr\>\s*");

            return pattern.ToString();
        }
        #endregion
    }
}
