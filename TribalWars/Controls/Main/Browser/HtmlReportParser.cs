using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TribalWars.Data.Reporting;
using TribalWars.Translations;

namespace TribalWars.Controls.Main.Browser
{
    /// <summary>
    /// Parses the document of the browser
    /// </summary>
    public class HtmlReportParser : IBrowserParser
    {
        #region Constants
        private const RegexOptions regexOptions = RegexOptions.Multiline | RegexOptions.Compiled;
        #endregion

        #region Fields
        private Regex _reportCoreRegex;
        private Regex _reportEspionageRegex;
        private Regex _reportRestRegex;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the pattern for analysing the url
        /// </summary>
        public Regex UrlRegex
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the pattern for analysing the document
        /// </summary>
        public Regex ReportCoreRegex
        {
            get { return _reportCoreRegex ?? (_reportCoreRegex = new Regex(GetReportCorePattern(), regexOptions)); }
        }

        /// <summary>
        /// Gets the pattern for analysing the document
        /// </summary>
        public Regex ReportEspionageRegex
        {
            get {
                return _reportEspionageRegex ??
                       (_reportEspionageRegex = new Regex(GetReportEspionagePattern(), regexOptions));
            }
        }

        /// <summary>
        /// Gets the pattern for analysing the document
        /// </summary>
        public Regex ReportRestRegex
        {
            get { return _reportRestRegex ?? (_reportRestRegex = new Regex(GetReportRestPattern(), regexOptions)); }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Checks if the current page is handled by the 
        /// </summary>
        /// <param name="url">The browser Uri</param>
        public bool Handles(string url)
        {
            return url.IndexOf("screen=report") > -1;
        }

        /// <summary>
        /// Handles the parsing of the document
        /// </summary>
        /// <param name="document">The document Html</param>
        /// <param name="serverTime">Time the page was generated</param>
        public bool Handle(string document, DateTime serverTime)
        {
            int index = document.IndexOf(string.Format("<th width=\"140\">{0}</th>", TWWords.ReportSubject));
            if (index == -1) return false;
            document = document.Substring(index);

            var matches = new Dictionary<string, Group>();
            if (HandleReportCore(matches, document))
            {
                bool testEspionage = HandleReportEspionage(matches, document);
                bool testRest = HandleReportRest(matches, document);

                var options = new ReportOutputOptions();
                Report report = ReportParser.ParseHtmlMatch(matches, options);
                VillageReportCollection.Save(report);
                return true;
            }
           
            return false;
        }

        private bool HandleReportCore(Dictionary<string, Group> matches, string input)
        {
            _reportCoreRegex = new Regex(GetReportCorePattern(), regexOptions);
            Match match = ReportCoreRegex.Match(input);
            if (match.Success)
            {
                matches.Add("date", match.Groups["date"]);
                matches.Add("luck", match.Groups["luck"]);
                matches.Add("morale", match.Groups["morale"]);
                matches.Add("winner", match.Groups["winner"]);
                matches.Add("you", match.Groups["you"]);
                matches.Add("attacker", match.Groups["attacker"]);
                matches.Add("him", match.Groups["him"]);
                matches.Add("defender", match.Groups["defender"]);
                matches.Add("atforce", match.Groups["atforce"]);
                matches.Add("defforce", match.Groups["defforce"]);
                matches.Add("atloss", match.Groups["atloss"]);
                matches.Add("defloss", match.Groups["defloss"]);
                return true;
            }
            return false;
        }

        private bool HandleReportEspionage(Dictionary<string, Group> matches, string input)
        {
            _reportEspionageRegex = new Regex(GetReportEspionagePattern(), regexOptions);
            Match match = ReportEspionageRegex.Match(input);
            if (match.Success)
            {
                matches.Add("resLeft", match.Groups["resLeft"]);
                matches.Add("buildings", match.Groups["buildings"]);
                matches.Add("unitsOutside", match.Groups["unitsOutside"]);
                return true;
            }
            return false;
        }

        private bool HandleReportRest(Dictionary<string, Group> matches, string input)
        {
            _reportRestRegex = new Regex(GetReportPatternOutside(), regexOptions);
            Match match = ReportRestRegex.Match(input); 
            if (match.Success)
            {
                matches.Add("forcetransit", match.Groups["forcetransit"]);
                matches.Add("forceSupport", match.Groups["forceSupport"]);
            }

            _reportRestRegex = new Regex(GetReportPatternHaul(), regexOptions);
            match = ReportRestRegex.Match(input);
            if (match.Success)
            {
                matches.Add("res", match.Groups["res"]);
                matches.Add("haul", match.Groups["haul"]);
                matches.Add("haulMax", match.Groups["haulMax"]);
            }

            _reportRestRegex = new Regex(GetReportPatternRam(), regexOptions);
            match = ReportRestRegex.Match(input);
            if (match.Success)
            {
                matches.Add("ramBefore", match.Groups["ramBefore"]);
                matches.Add("ramAfter", match.Groups["ramAfter"]);
            }

            _reportRestRegex = new Regex(GetReportPatternCata(), regexOptions);
            match = ReportRestRegex.Match(input);
            if (match.Success)
            {
                matches.Add("catBuilding", match.Groups["catBuilding"]);
                matches.Add("catBefore", match.Groups["catBefore"]);
                matches.Add("catAfter", match.Groups["catAfter"]);
            }

            _reportRestRegex = new Regex(GetReportPatternLoyalty(), regexOptions);
            match = ReportRestRegex.Match(input);
            if (match.Success)
            {
                matches.Add("loyaltyBegin", match.Groups["loyaltyBegin"]);
                matches.Add("loyaltyEnd", match.Groups["loyaltyEnd"]);
            }
            return true;
        }

        private string GetReportPatternCata()
        {
            var pattern = new StringBuilder();
            pattern.Append(string.Format(@"\<tr\>\<th\>{0}:\</th\>\s*", TWWords.ReportCatas));
            pattern.Append(string.Format(@"\<td colspan=""2""\>({2} )?(?<catBuilding>\w*) {0}\s?\<b\>(?<catBefore>\d*)\</b\> {1}\s?\<b\>(?<catAfter>\d*)\</b\>\</td\>\</tr\>\s*", TWWords.ReportCatasPre, TWWords.ReportCatasBetween, TWWords.The));
            return pattern.ToString();
        }

        private string GetReportPatternLoyalty()
        {
            var pattern = new StringBuilder();
            pattern.Append(string.Format(@"\<tr\>\<th\>{0}\</th\>\s*", TWWords.ReportLoyalty));
            pattern.Append(string.Format(@"\<td colspan=""2""\>{0} \<b\>(?<loyaltyBegin>\d*)\</b\> {1} \<b\>(?<loyaltyEnd>-?\d*)\</b\>\</td\>\</tr\>", TWWords.ReportLoyaltyPre, TWWords.ReportLoyaltyBetween));
            return pattern.ToString();
        }

        private string GetReportPatternRam()
        {
            var pattern = new StringBuilder();
            pattern.Append(string.Format(@"\<tr\>\<th\>{0}:\</th\>\s*", TWWords.ReportRams));
            pattern.Append(string.Format(@"\<td colspan=""2""\>{0} \<b\>(?<ramBefore>\d*)\</b\> {1} \<b\>(?<ramAfter>\d*)\</b\>\</td\>\</tr\>\s*", TWWords.ReportRamsPre, TWWords.ReportRamsBetween));
            return pattern.ToString();
        }

        private string GetReportPatternHaul()
        {
            var pattern = new StringBuilder();
            pattern.Append(@"\<table width=""100%"" style=""border: 1px solid #DED3B9""\>\s*");
            pattern.Append(string.Format(@"\<tr\>\<th\>{0}:\</th\>\s*\<td width=""220""\>", TWWords.ReportHaul));
            pattern.Append(string.Format(@"(?<res>\<img src="".*\.png(\?1)?"" title=""({0}|{1}|{2})"" alt="""" /\>(\d*\<span class=""grey""\>\.\</span\>)?\d*\s*){{0,3}}\</td\>\s*", TWWords.Wood, TWWords.Clay, TWWords.Iron));
            pattern.Append(@"\<td\>(?<haul>\d*)/(?<haulMax>\d*)\</td\>\s*\</tr\>\s*");
            return pattern.ToString();
        }

        private string GetReportPatternOutside()
        {
            var pattern = new StringBuilder();

            pattern.Append(string.Format(@"\<h4\>{0}\</h4\>\s*", TWWords.ReportUnitsInTransit));
            pattern.Append(@"\<table\>\s*");
            pattern.Append(@"\<tr\>(\<th width=""35""\>\<img src="".*\.png(\?1)?"" title="".*"" alt="""" /\>\</th\>)+\</tr\>\s*");
            pattern.Append(@"\<tr\>(?<forcetransit>\<td( class=""hidden"")?\>\d*\</td\>)*\s*\</tr\>\s*");
            pattern.Append(@"\</table\>\s*");

            pattern.Append(@"(");
            pattern.Append(string.Format(@"\<h4\>{0}\</h4\>\s*", TWWords.ReportUnitsOtherVillages));
            pattern.Append(@"\<table\>\s*");
            pattern.Append(@"\<tr\>\<th width=""200""\>\</th\>(\<th width=""15""\>\<img src="".*\.png(\?1)?"" title="".*"" alt="""" /\>\</th\>)+\</tr\>\s*");
            pattern.Append(string.Format(@"(?<forceSupport>\<tr\>\<td\>\<a href=""/game\.php\?village=\d*&amp;screen=info_village&amp;id=\d*""\>.* \(\d*\|\d*\) {0}\d{{1,2}}\</a\>\</td\>", TWWords.ContinentAbreviation));
            pattern.Append(@"(\<td( class=""hidden"")?\>\d*\</td\>)*\</tr\>\s*)*");
            pattern.Append(@"\</table\>\s*");
            pattern.Append(@")?");

            return pattern.ToString();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Builds the pattern for matching document input
        /// </summary>
        private string GetReportCorePattern()
        {
            #region Example
            //<tr><td>Sent</td><td>Jun 29,2008 16:30</td></tr>
            //<tr><td colspan="2" valign="top" height="160" style="border: solid 1px black; padding: 4px;">

            //<h3>The defender has won</h3>

            //<h4>Luck (from attacker's point of view)</h4>
            //<table><tr>
            //    <td><b>-4.7%</b></td>
            //    <td><img src="/graphic/rabe.png" alt="Misfortune" /></td>

            //<td>
            //<table style="border: 1px solid black;" cellspacing="0" cellpadding="0">
            //<tr>
            //    <td width="40.53732019" height="12"></td>
            //    <td width="9.46267981" style="background-image:url(/graphic/balken_pech.png);"></td>
            //    <td width="2" style="background-color:rgb(0, 0, 0)"></td>
            //    <td width="0" style="background-image:url(/graphic/balken_glueck.png);"></td>
            //    <td width="50"></td>
            //</tr>
            //</table>
            //</td>

            //    <td><img src="/graphic/klee_grau.png" alt="luck" /></td>

            //</tr>
            //</table>

            //<table>
            //<tr><td><h4>Morale: 62%</h4></td></tr>
            //</table>
            //<br />


            //<table width="100%" style="border: 1px solid #DED3B9">
            //<tr><th width="100">Attacker:</th><th><a href="/game.php?village=99271&amp;screen=info_player&amp;id=1318012">Laoujin</a></th></tr>
            //<tr><td>Village:</td><td><a href="/game.php?village=99271&amp;screen=info_village&amp;id=86899">iier II (761|360) K37</a></td></tr>
            //<tr><td colspan="2">
            //    <table class="vis">
            //        <tr class="center">
            //            <td></td>
            //            <td width="35"><img src="/graphic/unit/unit_spear.png" title="Spear fighter" alt="" /></td><td width="35"><img src="/graphic/unit/unit_sword.png" title="Swordsman" alt="" /></td><td width="35"><img src="/graphic/unit/unit_axe.png" title="Axeman" alt="" /></td><td width="35"><img src="/graphic/unit/unit_spy.png" title="Scout" alt="" /></td><td width="35"><img src="/graphic/unit/unit_light.png" title="Light cavalry" alt="" /></td><td width="35"><img src="/graphic/unit/unit_heavy.png" title="Heavy cavalry" alt="" /></td><td width="35"><img src="/graphic/unit/unit_ram.png" title="Ram" alt="" /></td><td width="35"><img src="/graphic/unit/unit_catapult.png" title="Catapult" alt="" /></td><td width="35"><img src="/graphic/unit/unit_snob.png" title="Nobleman" alt="" /></td>
            //        </tr>
            //        <tr class="center">
            //            <td>Quantity:</td>
            //                        <td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td>650</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td>
            //                    </tr>
            //        <tr class="center">
            //            <td>Losses:</td>
            //                        <td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td>40</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td>
            //                    </tr>
            //    </table>
            //</td></tr>
            //</table><br />



            //<table width="100%" style="border: 1px solid #DED3B9">
            //<tr><th width="100">Defender:</th><th>PrinceHector (deleted)</th></tr>
            //<tr><td>Village:</td><td><a href="/game.php?village=99271&amp;screen=info_village&amp;id=90425">Jaheira The Harper III (765|350) K37</a></td></tr>
            //<tr><td colspan="2">
            //    <table class="vis">
            //        <tr class="center">
            //            <td></td>
            //            <td width="35"><img src="/graphic/unit/unit_spear.png" title="Spear fighter" alt="" /></td><td width="35"><img src="/graphic/unit/unit_sword.png" title="Swordsman" alt="" /></td><td width="35"><img src="/graphic/unit/unit_axe.png" title="Axeman" alt="" /></td><td width="35"><img src="/graphic/unit/unit_spy.png" title="Scout" alt="" /></td><td width="35"><img src="/graphic/unit/unit_light.png" title="Light cavalry" alt="" /></td><td width="35"><img src="/graphic/unit/unit_heavy.png" title="Heavy cavalry" alt="" /></td><td width="35"><img src="/graphic/unit/unit_ram.png" title="Ram" alt="" /></td><td width="35"><img src="/graphic/unit/unit_catapult.png" title="Catapult" alt="" /></td><td width="35"><img src="/graphic/unit/unit_snob.png" title="Nobleman" alt="" /></td>
            //        </tr>
            //        <tr class="center">
            //            <td>Quantity:</td>
            //                        <td>7</td><td class="hidden">0</td><td class="hidden">0</td><td>200</td><td>656</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td>
            //                    </tr>
            //        <tr class="center">
            //            <td>Losses:</td>
            //                        <td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td>
            //                    </tr>
            //    </table>
            //</td></tr>
            //</table><br /><br />
            #endregion

            var pattern = new StringBuilder();

            // Date, luck, morale, ...
            pattern.Append(string.Format(@"\<tr\>\<td\>{0}\</td\>\<td\>(?<date>.*)\</td\>\</tr\>", TWWords.ReportSent));
            pattern.Append(@"[\s\S]*");
            pattern.Append(string.Format(@"\<h3\>{0}\s*(?<winner>({1}|{2})) {3}\</h3\>", TWWords.The, TWWords.ReportAttackerLCase, TWWords.ReportDefenderLCase, TWWords.ReportHasWon));
            pattern.Append(@"[\s\S]*");
            pattern.Append(@"\s*\<td( style=""padding:0;"")?\>\<b\>(?<luck>-?\d{1,3}(\.\d{1,2})?%)\</b\>\</td\>");
            pattern.Append(@"[\s\S]*");
            pattern.Append(string.Format(@"\<tr\>\<td\>\<h4\>{0}: (?<morale>\S*%)\</h4\>\</td\>\</tr\>", TWWords.Morale));
            pattern.Append(@"[\s\S]*");

            // Attacker
            pattern.Append(@"\<table width=""100%"" style=""border: 1px solid #DED3B9""\>\s*");
            pattern.Append(string.Format(@"\<tr\>\<th width=""100""\>{0}:\</th\>\<th\>(\<a href=""/game\.php\?village=\d*&amp;screen=info_player&amp;id=\d*""\>)?(?<attacker>.*)(\</a\>)?\</th\>\</tr\>\s*", TWWords.ReportAttacker));
            pattern.Append(string.Format(@"\<tr\>\<td\>{0}:\</td\>\<td\>\<a href=""/game\.php\?village=\d*&amp;screen=info_village&amp;id=\d*""\>(?<you>.*)\</a\>\</td\>\</tr\>\s*", TWWords.Village));
            pattern.Append(@"\<tr\>\<td colspan=""2""\>\s*");
            pattern.Append(@"\<table class=""vis""\>\s*");
            pattern.Append(@"\<tr class=""center""\>\s*");
            pattern.Append(@"\<td\>\</td\>\s*");
            pattern.Append(@"(\<td width=""35""\>\<img src="".*\.png(\?1)?"" title="".*"" alt="""" /\>\</td\>)+\s*");
            pattern.Append(@"\</tr\>\s*");
            pattern.Append(@"\<tr class=""center""\>\s*");
            pattern.Append(string.Format(@"\<td\>{0}:\</td\>\s*", TWWords.ReportQuantity));
            pattern.Append(@"(?<atforce>\<td( class=""hidden"")?\>\d*\</td\>)*\s*\</tr\>\s*");
            pattern.Append(@"\<tr class=""center""\>\s*");
            pattern.Append(string.Format(@"\<td\>{0}:\</td\>\s*", TWWords.ReportLosses));
            pattern.Append(@"(?<atloss>\<td( class=""hidden"")?\>\d*\</td\>)*\s*\</tr\>\s*");
            pattern.Append(@"[\s\S]*");

            //<tr><td>Village:</td><td><a href="/game.php?village=85946&amp;screen=info_village&amp;id=86973">002 K27 A New Begining (703|284) K27</a></td></tr>
            //<tr><td colspan="2">
            //    <p>None of your troops have returned. No information about the strength of your enemy's army could be collected.</p>
            //</td></tr>
            //</table><br /><br />

            // Defender
            pattern.Append(string.Format(@"\<tr\>\<th width=""100""\>{0}:\</th\>\<th\>(\<a href=""/game\.php\?village=\d*&amp;screen=info_player&amp;id=\d*""\>)?(?<defender>.*)(\</a\>)?\</th\>\</tr\>\s*", TWWords.ReportDefender));
            pattern.Append(string.Format(@"\<tr\>\<td\>{0}:\</td\>\<td\>\<a href=""/game\.php\?village=\d*&amp;screen=info_village&amp;id=\d*""\>(?<him>.*)\</a\>\</td\>\</tr\>\s*", TWWords.Village));
            pattern.Append(@"\<tr\>\<td colspan=""2""\>\s*");

            pattern.Append(string.Format(@"((\<p\>{0}\</p\>)|(", TWWords.ReportNoDefense.Replace(".", @"\.")));
            pattern.Append(@"\<table class=""vis""\>\s*");
            pattern.Append(@"\<tr class=""center""\>\s*");
            pattern.Append(@"\<td\>\</td\>\s*");
            pattern.Append(@"(\<td width=""35""\>\<img src="".*\.png(\?1)?"" title="".*"" alt="""" /\>\</td\>)+\s*");
            pattern.Append(@"\</tr\>\s*");
            pattern.Append(@"\<tr class=""center""\>\s*");
            pattern.Append(string.Format(@"\<td\>{0}:\</td\>\s*", TWWords.ReportQuantity));
            pattern.Append(@"(?<defforce>\<td( class=""hidden"")?\>\d*\</td\>)*\s*\</tr\>\s*");
            pattern.Append(@"\<tr class=""center""\>\s*");
            pattern.Append(string.Format(@"\<td\>{0}:\</td\>\s*", TWWords.ReportLosses));
            pattern.Append(@"(?<defloss>\<td( class=""hidden"")?\>\d*\</td\>)*\s*\</tr\>\s*");
            pattern.Append(@"))");

            return pattern.ToString();
        }

        private string GetReportEspionagePattern()
        {
            var pattern = new StringBuilder();

            #region Example
            //<h4>Espionage</h4>
            //<table style="border: 1px solid #DED3B9">
            //<tr><th>Resources scouted:</th><td><img src="/graphic/holz.png" title="Wood" alt="" />3<span class="grey">.</span>094 <img src="/graphic/lehm.png" title="Clay" alt="" />7<span class="grey">.</span>776 <img src="/graphic/eisen.png" title="Iron" alt="" />14<span class="grey">.</span>861 </td></tr>
            //    <tr><th>Buildings:</th><td>
            //        Village Headquarters <b>(Level 20)</b><br />
            //        Barracks <b>(Level 10)</b><br />
            //        Stable <b>(Level 14)</b><br />
            //        Workshop <b>(Level 2)</b><br />
            //        Academy <b>(Level 1)</b><br />
            //        Smithy <b>(Level 20)</b><br />
            //        Rally point <b>(Level 1)</b><br />
            //        Market <b>(Level 10)</b><br />
            //        Timber camp <b>(Level 25)</b><br />
            //        Clay pit <b>(Level 25)</b><br />
            //        Iron mine <b>(Level 25)</b><br />
            //        Farm <b>(Level 22)</b><br />
            //        Warehouse <b>(Level 22)</b><br />
            //        Hiding place <b>(Level 10)</b><br />
            //        Wall <b>(Level 15)</b><br />
            //        </td></tr>
            //    <tr><th colspan="2">Units outside of village:</th></tr>
            //    <tr><td colspan="2">
            //        <table>
            //        <tr><th width="35"><img src="/graphic/unit/unit_spear.png" title="Spear fighter" alt="" /></th><th width="35"><img src="/graphic/unit/unit_sword.png" title="Swordsman" alt="" /></th><th width="35"><img src="/graphic/unit/unit_axe.png" title="Axeman" alt="" /></th><th width="35"><img src="/graphic/unit/unit_spy.png" title="Scout" alt="" /></th><th width="35"><img src="/graphic/unit/unit_light.png" title="Light cavalry" alt="" /></th><th width="35"><img src="/graphic/unit/unit_heavy.png" title="Heavy cavalry" alt="" /></th><th width="35"><img src="/graphic/unit/unit_ram.png" title="Ram" alt="" /></th><th width="35"><img src="/graphic/unit/unit_catapult.png" title="Catapult" alt="" /></th><th width="35"><img src="/graphic/unit/unit_snob.png" title="Nobleman" alt="" /></th></tr>
            //        <tr><td>588</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td></tr>
            //        </table>
            //    </td></tr>
            #endregion

            // Other stuff
            pattern.Append(string.Format(@"\<h4\>{0}\</h4\>\s*", TWWords.ReportEspionage));
            pattern.Append(@"\<table style=""border: 1px solid #DED3B9""\>\s*");
            pattern.Append(string.Format(@"\<tr\>\<th\>{0}:\</th\>\<td\>", TWWords.ReportResourcesScouted));
            pattern.Append(string.Format(@"({0}|(?<resLeft>\<img src="".*\.png(\?1)?"" title=""({1}|{2}|{3})"" alt="""" /\>(\d*<span class=""grey""\>\.\</span\>)?\d*\s*){1,3})", TWWords.ReportResourcesScoutedNone, TWWords.Wood, TWWords.Clay, TWWords.Iron));
            pattern.Append(@"\</td\>\</tr\>\s*");


            pattern.Append(string.Format(@"(\<tr\>\<th\>{0}:\</th\>\<td\>\s*", TWWords.Buildings));
            pattern.Append(string.Format(@"(?<buildings>.* \<b\>\({0} \d*\)\</b\>\<br /\>\s*)*", TWWords.BuildingLevel));
            pattern.Append(@"\</td\>\</tr\>\s*)?");


            pattern.Append(string.Format(@"(\<tr\>\<th colspan=""2""\>{0}:\</th\>\</tr\>\s*", TWWords.ReportUnitsOutside));
            pattern.Append(@"\<tr\>\<td colspan=""2""\>\s*");
            pattern.Append(@"\<table\>\s*");
            pattern.Append(@"\<tr\>(\<th width=""35""\>\<img src="".*\.png(\?1)?"" title="".*"" alt="""" /\>\</th\>)*\</tr\>\s*");
            pattern.Append(@"\<tr\>(?<unitsOutside>(\<td( class=""hidden"")?\>\d*\</td\>)*)\</tr\>\s*");
            pattern.Append(@"\</table\>)?");

            return pattern.ToString();
        }

        private string GetReportRestPattern()
        {
            var pattern = new StringBuilder();

            pattern.Append(@"(");
            pattern.Append(string.Format(@"\<h4\>{0}\</h4\>\s*", TWWords.ReportUnitsInTransit));
            pattern.Append(@"\<table\>\s*");
            pattern.Append(@"\<tr\>(\<th width=""35""\>\<img src="".*\.png(\?1)?"" title="".*"" alt="""" /\>\</th\>)+\</tr\>\s*");
            pattern.Append(@"\<tr\>(?<forcetransit>\<td( class=""hidden"")?\>\d*\</td\>)*\s*\</tr\>\s*");
            pattern.Append(@"\</table\>\s*");

            pattern.Append(@"(");
            pattern.Append(string.Format(@"\<h4\>{0}\</h4\>\s*", TWWords.ReportUnitsOtherVillages));
            pattern.Append(@"\<table\>\s*");
            pattern.Append(@"\<tr\>\<th width=""200""\>\</th\>(\<th width=""15""\>\<img src="".*\.png(\?1)?"" title="".*"" alt="""" /\>\</th\>)+\</tr\>\s*");
            pattern.Append(string.Format(@"(?<forceSupport>\<tr\>\<td\>\<a href=""/game\.php\?village=\d*&amp;screen=info_village&amp;id=\d*""\>.* \(\d*\|\d*\) {0}\d{{1,2}}\</a\>\</td\>", TWWords.ContinentAbreviation));
            pattern.Append(@"(\<td( class=""hidden"")?\>\d*\</td\>)*\</tr\>\s*)*");
            pattern.Append(@"\</table\>\s*");
            pattern.Append(@")?");
            pattern.Append(@")?");

            // Haul
            pattern.Append(@"(\<table width=""100%"" style=""border: 1px solid #DED3B9""\>\s*");
            pattern.Append(string.Format(@"\<tr\>\<th\>{0}:\</th\>\s*\<td width=""220""\>", TWWords.ReportHaul));
            pattern.Append(string.Format(@"(?<res>\<img src="".*\.png(\?1)?"" title=""({0}|{1}|{2})"" alt="""" /\>(\d*\<span class=""grey""\>\.\</span\>)?\d*\s*){{0,3}}\</td\>\s*)?", TWWords.Wood, TWWords.Clay, TWWords.Iron));
            pattern.Append(@"\<td\>(?<haul>\d*)/(?<haulMax>\d*)\</td\>\s*\</tr\>\s*)?");
            
            pattern.Append(string.Format(@"(\<tr\>\<th\>{0}:\</th\>\s*", TWWords.ReportRams));
            pattern.Append(string.Format(@"\<td colspan=""2""\>{0} \<b\>(?<ramBefore>\d*)\</b\> {1} \<b\>(?<ramAfter>\d*)\</b\>\</td\>\</tr\>\s*", TWWords.ReportRamsPre, TWWords.ReportRamsBetween));
            
            pattern.Append(string.Format(@"(\<tr\>\<th\>{0}:\</th\>\s*", TWWords.ReportCatas));
            pattern.Append(string.Format(@"\<td colspan=""2""\>({2} )?(?<catBuilding>\w*) {0}\s?\<b\>(?<catBefore>\d*)\</b\> {1}\s?\<b\>(?<catAfter>\d*)\</b\>\</td\>\</tr\>\s*)?", TWWords.ReportCatasPre, TWWords.ReportCatasBetween, TWWords.The));
            
            pattern.Append(string.Format(@"(\<tr\>\<th\>{0}\</th\>\s*", TWWords.ReportLoyalty));
            pattern.Append(string.Format(@"\<td colspan=""2""\>{0} \<b\>(?<loyaltyBegin>\d*)\</b\> {1} \<b\>(?<loyaltyEnd>-?\d*)\</b\>\</td\>\</tr\>)?", TWWords.ReportLoyaltyPre, TWWords.ReportLoyaltyBetween));
            
            return pattern.ToString();
        }
        #endregion
    }
}
