using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TribalWars.Villages.Buildings;
using TribalWars.Villages.Resources;
using TribalWars.Villages.Units;

namespace TribalWars.Data.Reporting
{
    /// <summary>
    /// Generates a BBCode report
    /// </summary>
    public class ReportBbCodeOutput
    {
        #region Fields
        private readonly Report _report;
        #endregion

        #region Constructors
        public ReportBbCodeOutput(Report report)
        {
            _report = report;
        }

        /// <summary>
        /// Generates the standard BBCode output for the
        /// given report
        /// </summary>
        public static string Generate(Report report)
        {
            var output = new ReportBbCodeOutput(report);
            switch (report.ReportType)
            {
                case ReportTypes.Scout:
                    return output.BbCodeScout();
                default:
                    return output.BbCodeStandard();
            }
        }
        #endregion

        #region Output Types
        public string BbCodeStandard()
        {
            // players
            var str = new StringBuilder(200);
            PrintPlayers(str);

            if (_report.ReportDate.HasValue)
            {
                str.Append(Environment.NewLine);
                TimeSpan span = World.Default.ServerTime - _report.ReportDate.Value;
                if (span.TotalHours < 1) str.Append(string.Format("Date: {0}", _report.ReportDate.Value));
                else str.Append(string.Format("Date: {0} ({1} hours ago)", _report.ReportDate.Value, Math.Round(span.TotalHours)));
            }

            // resources
            if (_report.ReportOptions.ResourceDetails || true) str.Append(PrintResources());

            // Troops
            if (_report.ReportOptions.AttackingTroops) str.Append(PrintTroops("Attacker:", _report.Attack, true));
            if (_report.ReportOptions.DefendingTroops) str.Append(PrintTroops("Defender:", _report.Defense, false));

            // print free space
            if (_report.ReportOptions.People)
            {
                string space = Environment.NewLine + Resource.FaceBBCodeString;
                space += "[b]" + Report.GetTotalPeople(_report.Defense, TotalPeople.End).ToString("#,0") + "[/b]";
                if (_report.Buildings != null && _report.Buildings.Count > 2)
                {
                    int farmBuildings = _report.GetTotalPeopleInBuildings();

                    if (farmBuildings > 0) space += string.Format(" + {0:#,0}", farmBuildings);

                    if (_report.Buildings.ContainsKey(BuildingTypes.Farm))
                    {
                        ReportBuilding farm = _report.Buildings[BuildingTypes.Farm];
                        int totalFarm = farm.GetTotalProduction();
                        //space += string.Format(" + {0:#,0}", farmBuildings);
                        space = string.Format("{1} ({0:#,0} free)", totalFarm - farmBuildings - Report.GetTotalPeople(_report.Defense, TotalPeople.End), space);
                    }
                }
                if (space.Length > 0)
                {
                    if (Report.GetTotalPeople(_report.Defense, TotalPeople.Start) == 0) str.Append(Environment.NewLine + Environment.NewLine + "[b]Defender:[/b]");
                    str.Append(space);
                }
            }

            // Buildings
            if (_report.ReportOptions.Buildings) str.Append(PrintBuildings("Buildings:", _report.Buildings));

            return str.ToString();
        }

        public string BbCodeScout()
        {
            // players
            var str = new StringBuilder(200);
            PrintPlayers(str);

            if (_report.ReportDate.HasValue)
            {
                str.Append(Environment.NewLine);
                TimeSpan span = World.Default.ServerTime - _report.ReportDate.Value;
                if (span.TotalHours < 1) str.Append(string.Format("Date: {0}", _report.ReportDate.Value));
                else str.Append(string.Format("Date: {0} ({1} hours ago)", _report.ReportDate.Value, Math.Round(span.TotalHours)));
            }

            // Troops
            str.Append(PrintTroops("Defender:", _report.Defense, false));

            // Buildings
            str.Append(PrintBuildings("Buildings:", _report.Buildings));

            return str.ToString();
        }
        #endregion

        #region Buildings
        private string PrintBuildings(string title, Dictionary<BuildingTypes, ReportBuilding> buildings)
        {
            string str = "";
            foreach (ReportBuilding build in buildings.Values)
            {
                str += ", " + string.Format(build.BbCode());
            }
            if (str.Length > 2)
            {
                str = string.Format("{1}{1}[b]{0}[/b]{1}{2}", title, Environment.NewLine, str.Substring(2));
                if (buildings.Count > 3) str += Environment.NewLine + string.Format("Calculated points: {0:#,0}", _report.CalculatedPoints);
            }
            return str;
        }
        #endregion

        #region Troops
        private string PrintTroops(string title, Dictionary<UnitTypes, ReportUnit> troops, bool isAttacker)
        {
            int troopsTotalLost = 0;
            string str = string.Empty;
            foreach (ReportUnit unit in troops.Values)
            {
                if (unit.AmountStart > 0 && !(isAttacker && unit.Unit.HideAttacker))
                {
                    // add attack numbers - losses
                    if (unit.AmountEnd > 0)
                    {
                        //str += Environment.NewLine;
                        str += string.Format(" {0}{1}", unit.Unit.BbCodeImage, unit.AmountStart.ToString("#,0"));
                        if (unit.AmountLost > 0)
                        {
                            str += string.Format(" ([b]{0}[/b])", unit.AmountEnd.ToString("#,0"));
                        }
                    }
                    troopsTotalLost += unit.AmountLost;
                }
            }
            if (troopsTotalLost > 0)
            {
                // print cost lost troops
                int clay = 0, iron = 0, wood = 0;
                int totalLostType = 0;
                string str2 = "";
                foreach (ReportUnit unit in troops.Values)
                {
                    if (unit.AmountStart > 0 && unit.AmountEnd == 0)
                    {
                        str2 += unit.Unit.BbCodeImage;
                        totalLostType += unit.AmountLost;
                    }
                    if (isAttacker && unit.AmountLost > 0)
                    {
                        clay += unit.Unit.Cost.Clay * unit.AmountLost;
                        wood += unit.Unit.Cost.Wood * unit.AmountLost;
                        iron += unit.Unit.Cost.Iron * unit.AmountLost;
                    }
                }
                if (str2.Length > 0) str += Environment.NewLine + string.Format("{1} {0:#,0} vanquished", totalLostType, str2);
                //if (total == totalLost) str += string.Format("[b]!!All Dead!![/b]");

                /*if (isAttacker && clay > 0)
                {
                    // add resource cost losses
                    str += Environment.NewLine + "Losses cost: ";
                    str += Resource.ClayString + clay.ToString("#,0") + " ";
                    str += Resource.WoodString + wood.ToString("#,0") + " ";
                    str += Resource.IronString + iron.ToString("#,0") + " ";
                    str += Resource.AllString + (iron + clay + wood).ToString("#,0");
                }*/
            }
            if (str.Length > 0)
            {
                str = string.Format("{1}{1}[b]{0}[/b]{1}{2}", title, Environment.NewLine, str.Trim()); // .Trim()
            }

            string unitOut = string.Empty;
            foreach (ReportUnit unit in troops.Values)
            {
                if (unit.AmountOut > 0)
                {
                    unitOut += string.Format("{1}{0} ", unit.AmountOut.ToString("#,0"), unit.Unit.BbCodeImage);
                }
            }
            if (unitOut.Length > 0) str += Environment.NewLine + "Outside: " + unitOut;
            return str;
        }
        #endregion

        #region Resources
        private string PrintResources()
        {
            string str = Environment.NewLine;
            int totalLeft = _report.ResourcesLeft.Total();
            int totalHaul = _report.ResourcesHaul.Total();
            int room = _report.GetTotalResourceRoom();
            if (room > 0)
            {
                str += Environment.NewLine + string.Format("{0} [b]{2:#,0}[/b] / {1:#,0}", _report.Buildings[BuildingTypes.Warehouse].Building.BbCodeImage, room, totalLeft);

                const string pattern = "{1} {0:#,0} ({2})";
                str += Environment.NewLine + string.Format(pattern, _report.ResourcesLeft.Wood, Resource.WoodBBCodeString, _report.Buildings[BuildingTypes.TimberCamp].BbCodeForResource(room, _report.ResourcesLeft.Wood));
                str += Environment.NewLine + string.Format(pattern, _report.ResourcesLeft.Clay, Resource.ClayBBCodeString, _report.Buildings[BuildingTypes.ClayPit].BbCodeForResource(room, _report.ResourcesLeft.Clay));
                str += Environment.NewLine + string.Format(pattern, _report.ResourcesLeft.Iron, Resource.IronBBCodeString, _report.Buildings[BuildingTypes.IronMine].BbCodeForResource(room, _report.ResourcesLeft.Iron));
            }
            else if (_report.ResourcesLeft.Total() > 0)
            {
                // normal 1 line resource print:
                str += Environment.NewLine + "Resources left: ";
                str += PrintResourcesCore(_report.ResourcesLeft, 0, " ", true);
            }
            //str += PrintResourceHaulFarmers();

            if (_report.ResourcesHaul.Total() > 0)
            {
                str += Environment.NewLine + "Resources taken: ";
                str += PrintResourcesCore(_report.ResourcesHaul, _report.ResourceHaulMax, " ", true);
            }

            if (str.Length > 0 && str != Environment.NewLine) return Environment.NewLine + Environment.NewLine + "[b]Resource Details[/b]" + Environment.NewLine + str.Trim();
            return "";
        }

        protected string PrintResourceHaulFarmers()
        {
            // showMinTroops: print how many 'farmer' (spear&lc) units are needed to get all resources
            int tot = _report.ResourcesLeft.Total();
            if (tot > 0)
            {
                string str = Environment.NewLine;
                string farmer = string.Empty;
                foreach (Unit unit in WorldUnits.Default)
                {
                    if (unit.Farmer && unit.Carry > 0)
                        farmer += string.Format(", {0}{1}", unit.BbCodeImage, Math.Ceiling((double)tot / unit.Carry).ToString("#,0"));
                }
                if (farmer.Length > 0) str += string.Format("{0}", farmer.Substring(2));
                return str;
            }

            return string.Empty;
        }

        protected string PrintResourcesCore(Resource resources, int haulMax, string seperator, bool printTotal)
        {
            // print the three resources
            // if there are only two numbers (because he has 0 of some type)
            // then the icon for iron will have 0 resources even if the type
            // was wood/clay. since we can't really see the image:)
            string str = "";
            if (resources.Wood > 0) str += string.Format("{1} {0:#,0}{2}", resources.Wood, Resource.WoodBBCodeString, seperator);
            if (resources.Clay > 0) str += string.Format("{1} {0:#,0}{2}", resources.Clay, Resource.ClayBBCodeString, seperator);
            if (resources.Iron > 0) str += string.Format("{1} {0:#,0}{2}", resources.Iron, Resource.IronBBCodeString, seperator);

            if (printTotal)
            {
                int total = resources.Total();
                if (total > 0)
                {
                    str += string.Format("{0}{1:#,0}", Resource.GetImage(ResourceTypes.All), total);
                    if (resources.Total() != haulMax && haulMax > 0) str += " (All)";
                }
            }
            return str;
        }
        #endregion

        #region Players
        /// <summary>
        /// Gets BBCode of the report winner
        /// Also adds loyalty changes
        /// </summary>
        private string GetWinner()
        {
            if (_report.LoyaltyBegin > 0)
            {
                Unit snob = WorldUnits.Default[UnitTypes.Snob];
                return string.Format("{0} [b]{1}[/b] to [b]{2}[/b]", snob.BbCodeImage, _report.LoyaltyBegin.ToString(CultureInfo.InvariantCulture), _report.LoyaltyEnd.ToString(CultureInfo.InvariantCulture));
            }
            if (_report.Winner == "attacker")
                return "[b]won[/b] against";
            if (_report.Winner == "defender")
                return "[b]lost[/b] against";

            return "[b]fought[/b] against";
        }

        /// <summary>
        /// Adds village and player info to the output
        /// </summary>
        public void PrintPlayers(StringBuilder str)
        {
            if (_report.ReportType != ReportTypes.Scout)
            {
                if (_report.ReportOptions.AttackingPlayer) str.Append(_report.Attacker.BBCode());
                if (_report.ReportOptions.DefendingPlayer || _report.ReportType == ReportTypes.Noble)
                {
                    str.Append(Environment.NewLine + GetWinner());
                    str.Append(Environment.NewLine);
                }
            }
            if (_report.ReportOptions.DefendingPlayer) str.Append(_report.Defender.BBCode());
        }
        #endregion
    }
}
