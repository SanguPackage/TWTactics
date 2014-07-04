using System;
using System.Collections.Generic;
using System.Text;

using System.Text.RegularExpressions;

using TribalWars.Data.Buildings;
using TribalWars.Data.Units;
using TribalWars.Data.Resources;

namespace TribalWars.Data.Reporting
{
    /// <summary>
    /// Parses a report
    /// </summary>
    public class ReportParser
    {
        #region Parser
        /// <summary>
        /// Parses a copied report text
        /// </summary>
        /// <param name="input">The input string</param>
        /// <param name="options">The BBCode output options</param>
        public static Report ParseText(string input, ReportOutputOptions options)
        {
            Regex x = new Regex(ReportParser.Pattern);
            if (input.IndexOf("Block sender") > -1) input = input.Substring(input.IndexOf("Block sender"));
            Match match = x.Match(input + "\r\n");
            if (match.Success)
            {
                return ReportParser.ParseMatch(match, options);
            }
            return null;
        }

        /// <summary>
        /// Parses the Regex match
        /// </summary>
        /// <remarks>Parses the copy output</remarks>
        public static Report ParseMatch(Match match, ReportOutputOptions options)
        {
            Report report = new Report();
            report._reportOptions = options;
            report._attacker = new ReportVillage(match.Groups["you"].Value.Trim(), match.Groups["attacker"].Value.Trim());
            report._defender = new ReportVillage(match.Groups["him"].Value.Trim(), match.Groups["defender"].Value.Trim());

            DateTime testDate;
            if (DateTime.TryParse(match.Groups["date"].Value.Trim(), out testDate)) report._dateReport = testDate;
            report._luck = match.Groups["luck"].Value.Trim();
            report._morale = match.Groups["morale"].Value.Trim();
            report._winner = match.Groups["winner"].Value.Trim();

            int.TryParse(match.Groups["haul"].Value, out report._resourceHaulGot);
            int.TryParse(match.Groups["haulmax"].Value, out report._resourceHaulMax);

            report._buildings = ReportParser.GetBuildings(match.Groups["building"], out report._calculatedPoints);
            ReportParser.UpdateBuildings(report.Buildings, WorldBuildings.Default[BuildingTypes.Wall].Name, match.Groups["ramBefore"], match.Groups["ramAfter"]);
            ReportParser.UpdateBuildings(report.Buildings, match.Groups["catBuilding"].Value, match.Groups["catBefore"], match.Groups["catAfter"]);

            // put resources in a different Group when they come from the html parser :(
            report._resourcesHaul = ReportParser.GetResources(match.Groups["res"]);
            report._resourcesLeft = ReportParser.GetResources(match.Groups["resLeft"]);
            report._attack = ReportParser.GetTroops(match.Groups["atforce"], match.Groups["atloss"]);
            report._defense = ReportParser.GetTroops(match.Groups["defforce"], match.Groups["defloss"], match.Groups["defOut"]);

            if (match.Groups["loyaltyBegin"].Success)
            {
                report._loyaltyBegin = System.Convert.ToInt32(match.Groups["loyaltyBegin"].Value);
                report._loyaltyEnd = System.Convert.ToInt32(match.Groups["loyaltyEnd"].Value);
                if (report._loyaltyEnd <= 0) report._reportFlag |= ReportFlags.Nobled;
            }

            if (match.Groups["defOut"].Success) report._reportFlag |= ReportFlags.SeenOutside;
            if (match.Groups["defforce"].Success) report._reportFlag |= ReportFlags.SeenDefense;

            SetReportType(report);

            return report;
        }

        /// <summary>
        /// Parses the Regex match
        /// </summary>
        /// <remarks>Parses the Html output</remarks>
        public static Report ParseHtmlMatch(Dictionary<string, Group> match, ReportOutputOptions options)
        {
            Report report = new Report();
            report._reportOptions = options;
            report._attacker = new ReportVillage(match["you"].Value.Trim(), match["attacker"].Value.Replace("</a>", "").Trim());
            report._defender = new ReportVillage(match["him"].Value.Trim(), match["defender"].Value.Replace("</a>", "").Trim());

            DateTime testDate;
            if (DateTime.TryParse(match["date"].Value.Trim(), out testDate)) report._dateReport = testDate;
            report._luck = match["luck"].Value.Trim();
            report._morale = match["morale"].Value.Trim();
            report._winner = match["winner"].Value.Trim();

            if (match.ContainsKey("haul"))
            {
                int.TryParse(match["haul"].Value, out report._resourceHaulGot);
                int.TryParse(match["haulMax"].Value, out report._resourceHaulMax);
            }

            //Hiding place <b>(Level 4)</b><br />
            if (match.ContainsKey("buildings"))
            {
                report._buildings = ReportParser.GetBuildings(match["buildings"], out report._calculatedPoints);
            }
            if (match.ContainsKey("ramBefore")) ReportParser.UpdateBuildings(report.Buildings, WorldBuildings.Default[BuildingTypes.Wall].Name, match["ramBefore"], match["ramAfter"]);
            if (match.ContainsKey("catBuilding")) ReportParser.UpdateBuildings(report.Buildings, match["catBuilding"].Value, match["catBefore"], match["catAfter"]);

            // html strippen van res en resLeft
            if (match.ContainsKey("res"))
            {
                report._resourcesHaul = ReportParser.GetResources(match["res"]);
            }
            if (match.ContainsKey("resLeft"))
            {
                report._resourcesLeft = ReportParser.GetResources(match["resLeft"]);
            }

            report._attack = ReportParser.GetTroops(match["atforce"], match["atloss"]);
            if (match.ContainsKey("defforce"))
            {
                if (match.ContainsKey("defOut")) report._defense = ReportParser.GetTroops(match["defforce"], match["defloss"], match["defOut"]);
                else report._defense = ReportParser.GetTroops(match["defforce"], match["defloss"]);
            }

            if (match.ContainsKey("loyaltyBegin") && match["loyaltyBegin"].Success)
            {
                report._loyaltyBegin = System.Convert.ToInt32(match["loyaltyBegin"].Value);
                report._loyaltyEnd = System.Convert.ToInt32(match["loyaltyEnd"].Value);
                if (report._loyaltyEnd <= 0) report._reportFlag |= ReportFlags.Nobled;
            }

            if (match.ContainsKey("defOut") && match["defOut"].Success) report._reportFlag |= ReportFlags.SeenOutside;
            if (match.ContainsKey("defforce") && match["defforce"].Success) report._reportFlag |= ReportFlags.SeenDefense;

            SetReportType(report);

            return report;
        }

        private static Group PrepareHtmlMatch(Group group)
        {
            string input = group.Value.Replace("</td>", " ").Replace(@"<td class=""hidden"">", "").Replace("<td>", "").Replace(@"<span class=""grey"">.</span>", "");

            string pattern = @"\d* ";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);
            return match.Groups[0];
        }

        /// <summary>
        /// Figures out which type of report it is
        /// </summary>
        private static void SetReportType(Report report)
        {
            // Basically compare losses, check for scouts & nobles 
            // and set Success/Failure + fitting unit image
            int attack = 0, defense = 0, attack_lost = 0, defense_lost = 0;
            bool scout = false, scout_failed = false, noble = false, noble_failed = false;
            foreach (ReportUnit unit in report.Attack.Values)
            {
                if (unit.Unit.Type == UnitTypes.Spy)
                {
                    if (unit.AmountStart > 0)
                    {
                        scout = true;
                        if (unit.AmountEnd == 0) scout_failed = true;
                    }
                }
                else if (unit.Unit.Type == UnitTypes.Snob)
                {
                    if (unit.AmountStart > 0)
                    {
                        noble = true;
                        if (unit.AmountEnd == 0) noble_failed = true;
                    }
                }
                else
                {
                    attack += unit.AmountStart;
                    attack_lost += unit.AmountLost;
                }
            }
            foreach (ReportUnit unit in report.Defense.Values)
            {
                defense += unit.AmountStart;
                defense_lost += unit.AmountLost;
            }
            if (defense == defense_lost) report._reportFlag |= ReportFlags.Clear;
            report._reportStatus = ReportStatusses.Failure;

            if (noble)
            {
                report._reportType = ReportTypes.Noble;
                if (!noble_failed)
                {
                    if ((report._reportFlag & ReportFlags.Nobled) == 0) report._reportStatus = ReportStatusses.HalfSuccess;
                    else report._reportStatus = ReportStatusses.Success;
                }
            }
            else if (attack <= 100 && scout)
            {
                report._reportType = ReportTypes.Scout;
                if (!scout_failed) report._reportStatus = ReportStatusses.Success;
            }
            else
            {
                if (defense == defense_lost) report._reportStatus = ReportStatusses.Success;
                if (defense_lost > 0) report._reportType = ReportTypes.Attack;
                else
                {
                    if (defense == 0) report._reportType = ReportTypes.Farm;
                    else report._reportType = ReportTypes.Fake;
                }
            }
        }
        #endregion

        #region Buildings
        /// <summary>
        /// Builds the village buildings dictionary
        /// </summary>
        /// <param name="group">The RegEx group containing the buildings</param>
        /// <param name="points">Out: Calculated points for the building list</param>
        private static Dictionary<BuildingTypes, ReportBuilding> GetBuildings(Group group, out int points)
        {
            points = 0;
            Dictionary<BuildingTypes, ReportBuilding> build = new Dictionary<BuildingTypes, ReportBuilding>();

            string pattern = @"^(?<name>.*) (\<b\>)?\(Level\s*(?<level>\d*)\)(\</b\>\<br /\>)?\s*$";
            Regex x = new Regex(pattern);
            for (int i = 0; i < group.Captures.Count; i++)
            {
                Match match = x.Match(group.Captures[i].Value.Trim());
                if (match.Success)
                {
                    string name = match.Groups["name"].Value.Trim();
                    int level;
                    int.TryParse(match.Groups["level"].Value, out level);
                    ReportBuilding vilbuild = new ReportBuilding(WorldBuildings.Default[name], level);
                    build.Add(vilbuild.Building.Type, vilbuild);
                    points += vilbuild.GetTotalPoints();
                }
            }
            return build;
        }

        /// <summary>
        /// Updates a building level after rams or catapults
        /// </summary>
        /// <param name="buildings">The buildings list</param>
        /// <param name="target">The target building</param>
        /// <param name="beforeLevel">The RegEx group with the original building level</param>
        /// <param name="afterLevel">The RegEx group with the updated building level</param>
        private static void UpdateBuildings(Dictionary<BuildingTypes, ReportBuilding> buildings, string target, Group beforeLevel, Group afterLevel)
        {
            // update buildings with cata/ram data
            int before = 0, after = 0;
            if (target.Length > 0 && int.TryParse(beforeLevel.Value, out before) && int.TryParse(afterLevel.Value, out after))
            {
                BuildingTypes build = Building.GetBuildingFromName(target);
                if (buildings.ContainsKey(build))
                {
                    buildings[build].Level = after;
                    buildings[build].OriginalLevel = before;
                }
                else
                {
                    buildings.Add(build, new ReportBuilding(WorldBuildings.Default[build], after, before));
                }
            }
        }
        #endregion

        #region Troops
        /// <summary>
        /// Gets the troop amounts for the attacker
        /// </summary>
        /// <param name="groupAmount">The RegEx group with home troops</param>
        /// <param name="groupLost">The RegEx group with lost troops</param>
        private static Dictionary<UnitTypes, ReportUnit> GetTroops(Group groupAmount, Group groupLost)
        {
            return GetTroops(groupAmount, groupLost, null);
        }

        /// <summary>
        /// Gets the troop amounts
        /// </summary>
        /// <param name="groupAmount">The RegEx group with home troops</param>
        /// <param name="groupLost">The RegEx group with lost troops</param>
        /// <param name="groupOut">The RegEx group with the out troops</param>
        private static Dictionary<UnitTypes, ReportUnit> GetTroops(Group groupAmount, Group groupLost, Group groupOut)
        {
            Dictionary<UnitTypes, ReportUnit> troops = new Dictionary<UnitTypes, ReportUnit>();
            for (int i = 0; i < groupAmount.Captures.Count; i++)
            {
                if (WorldUnits.Default[i] != null)
                {
                    ReportUnit unit = new ReportUnit(WorldUnits.Default[i]);
                    int val = 0;
                    if (GetTroopCount(groupAmount.Captures[i].Value, out val))
                    {
                        unit.AmountStart = val;
                        if (GetTroopCount(groupLost.Captures[i].Value, out val))
                        {
                            unit.AmountLost = val;
                        }
                        if (groupOut != null && groupOut.Captures.Count > 0 && GetTroopCount(groupOut.Captures[i].Value, out val))
                        {
                            unit.AmountOut = val;
                        }
                        troops.Add(unit.Unit.Type, unit);
                    }
                }
            }
            return troops;
        }

        /// <summary>
        /// Removes the noise from a number
        /// </summary>
        public static bool GetTroopCount(string input, out int val)
        {
            if (input.StartsWith("<td"))
            {
                if (input == "<td class=\"hidden\">0</td>")
                    val = 0;
                else
                {
                    input = input.Substring(4);
                    val = System.Convert.ToInt32(input.Substring(0, input.Length - 5));
                }
                return true;
            }
            else
            {
                return int.TryParse(input.Trim().Replace(".", ""), out val);
            }
        }
        #endregion

        #region Resources
        /// <summary>
        /// Parses the RegEx resources group
        /// </summary>
        /// <param name="group">The RegEx group containing the resources details</param>
        /// <returns>The Resource object</returns>
        /// <remarks>If there are only two resources, the method will always assume it is wood and clay</remarks>
        private static Resource GetResources(Group group)
        {
            Resource res = new Resource();
            if (group.Captures.Count > 3)
            {
                for (int i = 0; i < group.Captures.Count; i += 2)
                {
                    int got = 0;
                    if (int.TryParse(group.Captures[i + 1].Value.Trim().Replace(".", ""), out got))
                    switch (group.Captures[i].Value)
                    {
                        case "Wood":
                            res.Wood = got;
                            break;
                        case "Iron":
                            res.Iron = got;
                            break;
                        case "Clay":
                            res.Clay = got;
                            break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < group.Captures.Count; i++)
                {
                    int got = 0;
                    if (group.Captures[i].Value.IndexOf("<img") > -1)
                    {
                        //<img src="/graphic/holz.png" title="Wood" alt="" />16<span class="grey">.</span>910 
                        Match match = Regex.Match(group.Captures[i].Value, @"\<img src="".*\.png(\?1)?"" title=""(?<res>\w*)"" alt="""" />(?<val>(\d*\<span class=""grey""\>\.\</span\>)?\d*)");
                        if (match.Success && int.TryParse(match.Groups["val"].Value.Replace("<span class=\"grey\">.</span>", ""), out got))
                        {
                            if (match.Groups["res"].Value == Translations.TWWords.Wood) res.Wood = got;
                            else if (match.Groups["res"].Value == Translations.TWWords.Iron) res.Iron = got;
                            else if (match.Groups["res"].Value == Translations.TWWords.Clay) res.Clay = got;
                        }
                    }
                    else
                    {
                        if (int.TryParse(group.Captures[i].Value.Trim().Replace(".", ""), out got))
                        {
                            if (i == 0)
                                res.Wood = got;
                            else if (i == 1)
                                res.Clay = got;
                            else
                                res.Iron = got;
                        }
                    }
                }
            }
            return res;
        }
        #endregion

        #region Pattern
        private static string _pattern;

        /// <summary>
        /// Gets the pattern used to recognize TW report data
        /// </summary>
        public static string Pattern
        {
            get
            {
                //if (_Pattern != null) return _Pattern;
                #region Examples
                // opera
                //Subject	 Laoujin attacks Plunder me  	
                //Sent	Dec 15,2007 21:36	
                //The defender has won
                //Luck (from attacker's point of view)
                //-23.6%		

                //Morale: 100%

                //Attacker:	Laoujin
                //-------------------------------------------

                // internet exploder
                //Subject  Laoujin attacks Khu_SAK     
                //Sent Dec 16,2007 02:13 
                //The defender has won
                //Luck (from attacker's point of view)

                //9.1% 
                //Morale: 90% 

                //Attacker: 

                //--------------------------------------------

                //Units outside of village:							
                //0	0	0	0	0	0	0	0	0

                //Haul:	10.851 10.887 5.882 	27620/27620	
                //Damage by rams:	The wall has been damaged and downgraded from level 11 to level 5
                //Damage by catapult bombardment:	The Barracks has been damaged and downgraded from level13 to level9

                //Defender's troops, that were in transit

                //0	0	0	0	0	0	0	0	0	
                //Haul:		0/114850	
                //Change of Loyalty	Loyalty loss from 32 to -2



                //Subject	  RedDevill attacks Village Hidden Amongst The Rocks  	
                //Sent	Jan 01,2008 19:11	
                //forwarded on:	Jan 01,2008 21:54	
                //forwarded by:	RedDevill

                // if I would just make it work for opera, this would've cost me alot less time :p
                #endregion



                // useless crap

                //forwarded on:	Jan 24,2008 23:12	
                //forwarded by:	LordMarash

                // stringbuilder anyone?

                StringBuilder pattern = new StringBuilder();

                pattern.Append(@"(Sent\s*(?<date>.*)\s*)?");
                pattern.Append(@"(forwarded on.*\s*)?");
                pattern.Append(@"(forwarded by.*\s*)?");
                pattern.Append(@"(The\s*(?<winner>\b(attacker|defender)\b) has won\s*");
                pattern.Append(@"Luck \(from attacker's point of view\)\s*");
                pattern.Append(@"(?<luck>-?\d{1,3}(\.\d{1,2})?%)");
                pattern.Append(@"\s*(Misfortune\s*luck\s*)?Morale:\s*(?<morale>\S*%)\s*)?");

                // Attacker
                pattern.Append(@"(Attacker:\s*(?<attacker>.*)\r\s*)?");
                pattern.Append(@"(Village:\s*(?<you>.*)\r\s*)?");
                pattern.Append(@"(Quantity:\s*(?<atforce>[\*xX\-\.\d]*\s*){0,12}\r\s*)?");
                pattern.Append(@"(Losses:\s*(?<atloss>[\*xX\-\.\d]*\s*){0,12}\r\s*)?");

                // Defender
                pattern.Append(@"(Defender:)?\s*(?<defender>.*)\r\s*");
                pattern.Append(@"(Village:)?\s*(?<him>.*)\r\s*");

                pattern.Append(@"((Quantity:\s*(?<defforce>[\*Xx\-\.\d]*\s*){0,12}\r\s*");
                pattern.Append(@"Losses:\s*(?<defloss>[\*xX\-\.\d]*\s*){0,12}\r\s*)");
                pattern.Append(@"|(None of your troops have returned\. No information about the strength of your enemy's army could be collected\.\s*))");

                // Other stuff
                pattern.Append(@"(Espionage\s*Resources\s*scouted:\s*((?<resLeft>[\.\d]*\s*){0,3}|none)\s*Buildings:\s*");
                pattern.Append(@"(?<building>.*\s*\(Level\s*\d+\)\s*)*\r\s*)?");

                pattern.Append(@"(Units outside of village:\s*");
                pattern.Append(@"(?<defOut>[\*xX\.\d]*\s*){0,12}\r\s*)?");

                pattern.Append(@"(\s*Haul:\s*(?<res>[\.\d]*\s*){0,3}\s*(?<haul>\d*)/(?<haulmax>\d*)\s*)?");

                pattern.Append(@"(Damage by rams:\s*The (w|W)all has been damaged and downgraded from level\s?(?<ramBefore>\d+) to level\s?(?<ramAfter>\d{1,2})\s*)?");
                pattern.Append(@"(Damage by catapult bombardment:\s*The (?<catBuilding>\w*) has been damaged and downgraded from level\s?(?<catBefore>\d{1,2}) to level\s?(?<catAfter>\d{1,2})\s*)?");

                pattern.Append(@"(Units outside of village:\s*");
                pattern.Append(@"(?<unitsOutside>[\*xX\-\.\d]*\s*){0,12}\r\s*)?");

                pattern.Append(@"(Defender's troops, that were in transit\s*");
                pattern.Append(@"(?<forcetransit>[\*xX\-\.\d]*\s*){0,12}\r\s*)?");

                //pattern.Append(@"(Defender's troops in other villages\s*";

                /*
                 * Subject	  ryanthegreat attacks Mehry City 07A  
                    Sent	Feb 03,2008 03:22
                    forwarded on:	Apr 03,2008 23:30
                    forwarded by:	ryanthegreat
                    The attacker has won
                    Luck (from attacker's point of view)-11.4%							
                    	
                    Morale: 100%

                    Attacker:	ryanthegreat
                    Village:	016 Pidgey (442|529) K54
                    									
                    Quantity:	0	0	100	0	0	0	0	0	1
                    Losses:	0	0	0	0	0	0	0	0	0


                    Defender:	GAAJE THE GREAT
                    Village:	035 Clefairy (448|521) K54
                    									
                    Quantity:	0	0	0	0	0	0	0	0	0
                    Losses:	0	0	0	0	0	0	0	0	0




                    Defender's troops, that were in transit								
                    0	0	0	0	0	0	0	0	0

                    Defender's troops in other villages									
                    M-Slaughterhouse (442|516) K54	0	0	200	0	498	0	0	0	0
                    010 Caterpie (448|526) K54	0	0	458	0	0	0	0	0	0
                    006 Charizard (444|523) K54	0	200	100	0	0	0	0	0	0
                    Haul:		0/1000
                    Change of Loyalty	Loyalty loss from 15 to -8


                    Paste troops quantities into simulator

                    Paste quantities of surviving troops into simulator*/

                pattern.Append(@"(Change of Loyalty\s*");
                pattern.Append(@"Loyalty loss from (?<loyaltyBegin>\d{1,3}) to (?<loyaltyEnd>-?\d{1,2})\s*)?");





                _pattern = pattern.ToString();

                return _pattern;
            }
        }
        #endregion
    }
}
