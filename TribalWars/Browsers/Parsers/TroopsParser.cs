#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Browsers.Reporting;
using System.Text.RegularExpressions;
using TribalWars.Villages;
using TribalWars.Villages.Units;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Browsers.Parsers
{
    /// <summary>
    /// Parser for the Troops overview page
    /// </summary>
    public class TroopsParser : IBrowserParser
    {
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
            return url.IndexOf("screen=overview_villages") > -1 && url.IndexOf("mode=units") > -1;
        }

        /// <summary>
        /// Handles the parsing of the document
        /// </summary>
        /// <param name="document">The document Html</param>
        /// <param name="serverTime">Time the page was generated</param>
        public bool Handle(string document, DateTime serverTime)
        {
            int index = document.IndexOf("&amp;screen=overview_villages&amp;mode=units&amp;units_type=support_detail");
            if (index == -1) return false;

            string pattern = GetDocumentPattern();
            _documentRegex = new Regex(pattern, RegexOptions.Multiline);
            MatchCollection matches = _documentRegex.Matches(document, index);

            var ownVillages = new Dictionary<int, Village>();
            /*foreach (Village vil in World.Default.You.Player.Villages)
            {
                ownVillages.Add(vil.ID, vil);
            }*/

            //System.Diagnostics.Debug.Print(DateTime.Now.ToShortTimeString() + " " + matches.Count.ToString());
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
            Village vil = World.Default.GetVillage(match.Groups["village"].Value);
            if (vil != null)
            {
                /*if (vil.Player == World.Default.You.Player && vil.PreviousVillageDetails != null && vil.PreviousVillageDetails.Player != World.Default.You.Player)
                {
                    vil.Nobled(World.Default.You.Player);
                }*/

                CurrentSituation current = vil.Reports.CurrentSituation;

                /*int tempInt;
                if (CommonParsers.ParseInt(match.Groups["points"].Value, out tempInt))
                    vil.Points = tempInt;*/

                /*string pattern = string.Format(@"(\<img src=""/graphic/(holz|lehm|eisen)\.png(\?1)?"" title=""({0}|{1}|{2})"" alt="""" /\>(\<span class=""warn""\>)?(?<res>(\d*<span class=""grey""\>\.\</span\>)?\d*)(\</span\>)?\s*)", TWWords.Wood, TWWords.Clay, TWWords.Iron);
                Regex res = new Regex(pattern);
                MatchCollection resMatches = res.Matches(match.Groups["res"].Value);
                if (resMatches.Count == 3)
                {
                    if (CommonParsers.ParseInt(resMatches[0].Groups["res"].Value, out tempInt))
                        situation.Resources.Wood = tempInt;
                    if (CommonParsers.ParseInt(resMatches[1].Groups["res"].Value, out tempInt))
                        situation.Resources.Clay = tempInt;
                    if (CommonParsers.ParseInt(resMatches[2].Groups["res"].Value, out tempInt))
                        situation.Resources.Iron = tempInt;

                    situation._resourcesDate = serverTime;
                }
                */

                
                Dictionary<UnitTypes, int> ownForce = GetTroops(match.Groups["ownforce"]);
                Dictionary<UnitTypes, int> thereForce = GetTroops(match.Groups["thereforce"]);
                Dictionary<UnitTypes, int> awayForce = GetTroops(match.Groups["awayforce"]);
                Dictionary<UnitTypes, int> movingForce = GetTroops(match.Groups["movingforce"]);
                current.UpdateTroops(ownForce, thereForce, awayForce, movingForce);
                vil.Reports.Save();
            }
        }

        /// <summary>
        /// Gets the troop amounts
        /// </summary>
        /// <param name="troops">The RegEx group with troops</param>
        private static Dictionary<UnitTypes, int> GetTroops(Group troops)
        {
            var outTroops = new Dictionary<UnitTypes, int>();
            for (int i = 0; i < troops.Captures.Count; i++)
            {
                if (WorldUnits.Default[i] != null)
                {
                    int val;
                    if (ReportParser.GetTroopCount(troops.Captures[i].Value, out val))
                    {
                        outTroops.Add(WorldUnits.Default[i].Type, val);
                    }
                }
            }
            return outTroops;
        }

        /// <summary>
        /// Builds the pattern for matching document input
        /// </summary>
        private string GetDocumentPattern()
        {
            var pattern = new StringBuilder();

            //            <tr>
            //    <td rowspan="5" valign="top">	
            //        <span id="label_11">
            //        <a href="game.php?village=11&amp;screen=overview&amp;">
            //            <span id="label_text_11">Gesmurft (467|511) C54</span>
            //        </a>

            //    <a href="javascript:editToggle('label_11', 'edit_11')"><img src="/graphic/rename.png?1" alt="herbenoemen" title="herbenoemen" /></a>
            //</span>
            //<span id="edit_11" style="display:none">
            //    <input id="edit_input_11" size="" value=".*"/> 
            //    <input type="button" value="OK" onclick="editSubmitNew('label_11', 'label_text_11', 'edit_11', 'edit_input_11', '/game.php?village=11&amp;&amp;screen=main&amp;ajax=change_name');"/>
            //</span>	</td>
            //    </tr>

            pattern.Append(@"\<tr\>\s*");
            pattern.Append(@"\<td rowspan=""5"" valign=""top""\>\s*");
            pattern.Append(@"\<span id=""label_(?<id>\d*)""\>\s*");
            pattern.Append(@"\<a href=""game\.php\?village=\d*&amp;screen=overview&amp;""\>\s*");
            pattern.Append(@"\<span id=""label_text_\d*""\>.*\((?<village>\d{1,3}\|\d{1,3})\) .\d{1,2}\</span\>\s*");
            pattern.Append(@"\</a\>\s*");
            pattern.Append(@"\<a href=""javascript:editToggle\('label_\d*', 'edit_\d*'\)""\>\<img\ssrc="".*\.png(\?1)?"" alt="".*"" title="".*"" /\>\</a\>\s*");
            pattern.Append(@"\</span>\s*");
            pattern.Append(@"\<span id=""edit_\d*"" style=""display:none""\>\s*");
            pattern.Append(@"\<input id=""edit_input_\d*"" size="""" value="".*""/\>\s*");
            pattern.Append(@"\<input type=""button"" value=""OK"" onclick=""editSubmitNew\('label_\d*', 'label_text_\d*', 'edit_\d*', 'edit_input_\d*', '/game\.php\?village=\d*&amp;&amp;screen=main&amp;ajax=change_name'\);""/\>\s*");
            pattern.Append(@"\</span\>\s*\</td\>\s*");
            pattern.Append(@"\</tr\>\s*");

            //            <tr class="units_home">
            //        <td>eigen</td>
            //        <td>4652</td><td>4925</td><td class="hidden">0</td><td>2890</td><td class="hidden">0</td><td>900</td><td>6</td><td class="hidden">0</td><td class="hidden">0</td>
            //        <td><a href="game.php?village=11&amp;screen=place&amp;">Bevelen</a></td>
            //        </tr>

            pattern.Append(@"\<tr class=""units_home""\>\s*");
            pattern.Append(@"\<td\>.*\</td\>\s*");
            pattern.Append(@"(?<ownforce>\<td( class=""hidden"")?\>\d*\</td\>)*\s*");
            pattern.Append(@"\<td\>\<a href=""game\.php\?village=\d*&amp;screen=place&amp;""\>.*\</a\>\</td\>\s*");
            pattern.Append(@"\</tr\>\s*");

            //            <tr  class="units_there">
            //        <td>in het dorp</td>
            //        <td>4652</td><td>4925</td><td class="hidden">0</td><td>2890</td><td class="hidden">0</td><td>900</td><td>6</td><td class="hidden">0</td><td class="hidden">0</td>
            //                    <td rowspan="2"><a href="game.php?village=11&amp;screen=place&amp;mode=units">Troepen</a></td>
            //                </tr>

            pattern.Append(@"\<tr\s{1,2}class=""units_there""\>\s*");
            pattern.Append(@"\<td\>.*\</td\>\s*");
            pattern.Append(@"(?<thereforce>\<td( class=""hidden"")?\>\d*\</td\>)*\s*");
            pattern.Append(@"\<td rowspan=""2""\>\<a href=""game\.php\?village=\d*&amp;screen=place&amp;mode=units""\>.*\</a\>\</td\>\s*");
            pattern.Append(@"\</tr\>\s*");

            //        <tr class="units_away">
            //        <td>elders</td>
            //        <td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td>
            //                </tr>

            pattern.Append(@"\<tr class=""units_away""\>\s*");
            pattern.Append(@"\<td\>.*\</td\>\s*");
            pattern.Append(@"(?<awayforce>\<td( class=""hidden"")?\>\d*\</td\>)*\s*");
            pattern.Append(@"\</tr\>\s*");

            //            <tr class="units_moving">
            //        <td>op pad</td>
            //        <td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td>
            //        <td><a href="game.php?village=11&amp;screen=place&amp;">Bevelen</a></td>
            //        </tr>

            pattern.Append(@"\<tr class=""units_moving""\>\s*");
            pattern.Append(@"\<td\>.*\</td\>\s*");
            pattern.Append(@"(?<movingforce>\<td( class=""hidden"")?\>\d*\</td\>)*\s*");
            pattern.Append(@"\<td\>\<a href=""game\.php\?village=\d*&amp;screen=place&amp;.*""\>.*\</a\>\</td\>\s*");
            pattern.Append(@"\</tr\>\s*");

            //pattern.Append(@"");


            		
            
            		
            			

            		
            








            /*pattern.Append(@"\<td\>(?<warehouse>\d*)\</td\>\s*");
            pattern.Append(@"\<td\>(?<population>\d*)/(?<farm>\d*)\</td\>\s*");
            pattern.Append(@"\<td\>\s*");
            pattern.Append(@"((?<buildingDoneDate>.*)\<br /\>\s*");

            pattern.Append(@"((?<buildings>(\<img src=""/graphic/overview/down.png"" title=""Demolition"" alt="""" /\>\s*)?\<img src=""/graphic/buildings/.*\.png(\?1)?"" title="".*"" alt="""" /\>\s*)*))?");
            pattern.Append(@"\</td\>\s*");
            pattern.Append(@"\<td\>\s*");
            pattern.Append(@"(?<smithy>(\<img src=""/graphic/unit/unit_.*\.png(\?1)?"" title="".*"" alt="""" /\>\s*(\<img src=""/graphic/overview/down\.png(\?1)?"" title="".*"" alt="""" /\>)?)*)");
            pattern.Append(@"\</td\>\s*");
            pattern.Append(@"\<td\>\s*");

            pattern.Append(@"(?<training>(\<img src=""/graphic/unit/unit_.*\.png(\?1)?"" title="".*"" alt="""" /\>)*)");
            pattern.Append(@"\</td\>\s*\");
            pattern.Append(@"</tr\>\s*");*/

            //pattern.Append(@"");

            return pattern.ToString();
        }
        #endregion
    }
}
