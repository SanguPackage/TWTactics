#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Globalization;
using TribalWars.Villages;
using TribalWars.Villages.Buildings;
using TribalWars.Villages.Resources;
using TribalWars.Villages.Units;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Browsers.Reporting
{
    /// <summary>
    /// Represents the current estimated situation of a village (DEFENDER)
    /// </summary>
    /// <remarks>The attacker side of the 'report' is empty</remarks>
    public class CurrentSituation
    {
        #region Fields
        private float _loyalty;

        private DateTime _villageDate;
        private DateTime _loyaltyDate;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the defenses of the village
        /// </summary>
        public VillageDefense Defense { get; private set; }

        /// <summary>
        /// Gets the buildings in the village
        /// </summary>
        public VillageBuildings Buildings { get; private set; }

        /// <summary>
        /// Gets the village
        /// </summary>
        public Village Village { get; private set; }

        /// <summary>
        /// Gets the loyalty
        /// </summary>
        public string LoyaltyString
        {
            get { return _loyaltyDate == DateTime.MinValue ? "???" : _loyalty.ToString("0"); }
        }

        /// <summary>
        /// Gets the last time building levels were updated
        /// </summary>
        public DateTime BuildingsDate { get; internal set; }

        /// <summary>
        /// Gets the last time defense levels were updated
        /// </summary>
        public DateTime DefenseDate { get; internal set; }

        /// <summary>
        /// Gets the last known resource level
        /// </summary>
        public Resource Resources { get; private set; }

        /// <summary>
        /// Gets the last time resource levels were updated
        /// </summary>
        public DateTime ResourcesDate { private get; set; }

        /// <summary>
        /// Gets or sets the last known loyalty level
        /// </summary>
        public float Loyalty
        {
            get { return _loyalty; }
            set
            {
                _loyalty = value;
                _loyaltyDate = World.Default.ServerTime;
            }
        }

        /// <summary>
        /// Gets the last time loyalty info was gathered
        /// </summary>
        public DateTime LoyaltyDate
        {
            get { return _loyaltyDate; }
        }

        /// <summary>
        /// Gets the last time village info was gathered
        /// </summary>
        /// <remarks>This is usually the date of the last report</remarks>
        public DateTime VillageDate
        {
            get { return _villageDate; }
        }
        #endregion

        #region Constructors
        public CurrentSituation(Village village)
        {
            Village = village;
            Buildings = new VillageBuildings();
            Defense = new VillageDefense();
            Resources = new Resource();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Paints the report on a canvas
        /// </summary>
        public void Paint(Graphics g)
        {
            Update();

            if (_villageDate == DateTime.MinValue)
            {
                var format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                g.DrawString("No estimated situation available.", SystemFonts.DefaultFont, Brushes.Black, new RectangleF(10, 10, g.VisibleClipBounds.Width - 10, 50), format);
            }
            else
            {
                g.TranslateTransform(10, 10);

                // resources
                float x = 5;
                const int resourceYOffset = 3;
                const int resourceXOffset = 5;
                const int imageXOffset = 20;
                var borderPen = new Pen(Color.Black, 2);
                const int imageYOffset = 20;
                const int imageYTextOffset = 5;
                if (ResourcesDate != DateTime.MinValue)
                {
                    g.DrawImage(ResourceImages.wood, x, resourceYOffset);
                    g.DrawString(Resources.WoodString, SystemFonts.DefaultFont, Brushes.Black, x + imageXOffset, resourceYOffset + imageYTextOffset);

                    x += g.MeasureString(Resources.WoodString, SystemFonts.DefaultFont).Width;
                    x += imageXOffset + resourceXOffset;
                    g.DrawImage(ResourceImages.clay, x, resourceYOffset);
                    g.DrawString(Resources.ClayString, SystemFonts.DefaultFont, Brushes.Black, x + imageXOffset, resourceYOffset + imageYTextOffset);

                    x += g.MeasureString(Resources.ClayString, SystemFonts.DefaultFont).Width;
                    x += imageXOffset + resourceXOffset;
                    g.DrawImage(ResourceImages.iron, x, resourceYOffset);
                    g.DrawString(Resources.IronString, SystemFonts.DefaultFont, Brushes.Black, x + imageXOffset, resourceYOffset + imageYTextOffset);

                    x += g.MeasureString(Resources.IronString, SystemFonts.DefaultFont).Width;
                    x += imageXOffset + resourceXOffset;

                    g.DrawRectangle(borderPen, 0, 0, x, resourceYOffset + imageYOffset + 3);
                }

                // loyalty
                if (LoyaltyDate != DateTime.MinValue)
                {
                    g.DrawString(LoyaltyString, SystemFonts.DefaultFont, Brushes.Black, x + 15, resourceYOffset + imageYTextOffset);
                    float width = g.MeasureString(LoyaltyString, SystemFonts.DefaultFont).Width;
                    g.DrawRectangle(borderPen, x + 10, 0, width + 10, resourceYOffset + imageYOffset + 3);
                }

                g.TranslateTransform(0, resourceYOffset + imageYOffset + 10);

                // buildings
                float y = 5;
                int column = 0;
                int xAdd = 0;
                if (BuildingsDate != DateTime.MinValue)
                {
                    string dateString = string.Format("Date: {0}", Tools.Common.GetPrettyDate(BuildingsDate, true));
                    g.DrawString(dateString, SystemFonts.DefaultFont, Brushes.Black,  5, 5);
                    y += g.MeasureString(dateString, SystemFonts.DefaultFont).Height + 5;
                    foreach (KeyValuePair<Building, int> building in Buildings)
                    {
                        column++;
                        g.DrawImage(building.Key.Image, 5 + xAdd, y);
                        g.DrawString(building.Value.ToString(CultureInfo.InvariantCulture), SystemFonts.DefaultFont, Brushes.Black, 5 + imageXOffset + xAdd, y + 5);
                        if (column % 2 == 0)
                        {
                            y += 20;
                            xAdd = 0;
                        }
                        else
                        {
                            xAdd = 60;
                        }
                    }
                    if (column % 2 == 1) y += 20;
                    g.DrawRectangle(borderPen, 0, 0, 2 * 60 + 5, y);   // 60 == xAdd

                    g.TranslateTransform(2 * 60 + 12, 0);
                }

                // troops
                if (DefenseDate != DateTime.MinValue)
                {
                    column = 0;
                    xAdd = 0;
                    y = 5;
                    string dateString = string.Format("Date: {0}", Tools.Common.GetPrettyDate(DefenseDate, true));
                    g.DrawString(dateString, SystemFonts.DefaultFont, Brushes.Black, 5, 5);
                    y += g.MeasureString(dateString, SystemFonts.DefaultFont).Height + 5;
                    foreach (KeyValuePair<UnitTypes, int> unit in Defense.OwnTroops)
                    {
                        column++;
                        g.DrawImage(WorldUnits.Default[unit.Key].Image, 5 + xAdd, y);
                        g.DrawString(Tools.Common.GetPrettyNumber(unit.Value), SystemFonts.DefaultFont, Brushes.Black, 5 + imageXOffset + xAdd, y + 5);
                        if (column % 2 == 0)
                        {
                            y += 20;
                            xAdd = 0;
                        }
                        else
                        {
                            xAdd = 60;
                        }
                    }
                    if (column % 2 == 1) y += 20;
                    g.DrawRectangle(borderPen, 0, 0, 2 * xAdd + 5, y);
                }
            }
        }

        /// <summary>
        /// Updates the estimated report when a newer report is added
        /// </summary>
        public void UpdateDefender(Report report)
        {
            if (ResourcesDate < report.Date)
            {
                // Update resources
                if (report.ResourceHaulGot < report.ResourceHaulMax)
                {
                    // All resources taken
                    Resources = new Resource();
                    ResourcesDate = report.Date;
                }
                else if (report.ResourcesLeft.Set)
                {
                    // Espionage resource
                    Resources = report.ResourcesLeft.Clone();
                    ResourcesDate = report.Date;
                }
                else if (report.ResourcesHaul.Set && Resources.Set)
                {
                    // Subtract haul from stock
                    Update();
                    if (Resources.Wood < report.ResourcesHaul.Wood)
                    {
                        // If barbarian + we have building info
                        // We need to inform the user that the buildings have been upgraded?
                    }

                    Resources.Wood -= report.ResourcesHaul.Wood;
                    Resources.Clay -= report.ResourcesHaul.Clay;
                    Resources.Iron -= report.ResourcesHaul.Iron;
                }
            }
            if (BuildingsDate < report.Date && report.Buildings.Count > 0)
            {
                // Update buildings
                foreach (ReportBuilding building in report.Buildings.Values)
                {
                    Buildings[building.Building.Type] = building.Level;
                    //if (_buildings[building.Building.Type] .ContainsKey(building.Building.Type)) _buildings[building.Building.Type].Level = building.Level;
                    //else _buildings.Add(building.Building.Type, building.Clone());
                }
                BuildingsDate = report.Date;
            }
            if (DefenseDate < report.Date && (report.ReportFlag & ReportFlags.SeenDefense) != 0)
            {
                // Update defenses
                foreach (ReportUnit unit in report.Defense.Values)
                {
                    Defense.OwnTroops[unit.Unit.Type] = unit.AmountEnd;
                    //if (_defense.ContainsKey(unit.Unit.Type)) _defense[unit.Unit.Type] = unit.Clone();
                    //else _defense.Add(unit.Unit.Type, unit.Clone());

                    //_defense[unit.Unit.Type].AmountStart -= _defense[unit.Unit.Type].AmountLost;
                }
                DefenseDate = report.Date;

                // Mark the village as a farm / to noble
                if ((report.ReportFlag & ReportFlags.Clear) > 0)
                {
                    if (report.Defender.Village.Player == null) report.Defender.Village.Type |= VillageType.Farm;
                }
            }

            // loyalty changes
            if (_loyaltyDate < report.Date && report.LoyaltyBegin != report.LoyaltyEnd)
            {
                _loyalty = report.LoyaltyEnd;
                _loyaltyDate = report.Date;
                if (_loyalty <= 0)
                {
                    Village.Nobled(report.Attacker.Village.Player);
                }
            }
            if (_villageDate < report.Date) _villageDate = report.Date;
        }

        /// <summary>
        /// Updates the estimated report when a newer report is added
        /// </summary>
        public void UpdateAttacker(Report report)
        {
            Update();
            if (DefenseDate == DateTime.MinValue || DefenseDate < report.Date)
            {
                foreach (ReportUnit unit in report.Attack.Values)
                {
                    if (Defense.OwnTroops[unit.Unit.Type] >= unit.AmountLost)
                        Defense.OwnTroops[unit.Unit.Type] -= unit.AmountLost;
                    else
                        Defense.OwnTroops[unit.Unit.Type] = 0;
                }
                DefenseDate = report.Date;
            }
            if (_villageDate < report.Date) _villageDate = report.Date;
        }

        /// <summary>
        /// Makes assumptions about the village progress
        /// </summary>
        public void Update()
        {
            if (VillageDate != DateTime.MinValue && VillageDate < World.Default.ServerTime.AddMinutes(-30))
            {
                if (Building.HasResourceInfo(Buildings))
                {
                    // Update resources
                    TimeSpan elapsed = World.Default.ServerTime - VillageDate;
                    //TimeSpan elapsed = new DateTime( 2008, 9, 7, 15, 29, 0) - VillageDate;
                    Resources.Wood += (int)Math.Floor(elapsed.TotalSeconds * WorldBuildings.Default[BuildingTypes.TimberCamp].GetTotalProduction(Buildings[BuildingTypes.TimberCamp]) / 3600);
                    Resources.Clay += (int)Math.Floor(elapsed.TotalSeconds * WorldBuildings.Default[BuildingTypes.ClayPit].GetTotalProduction(Buildings[BuildingTypes.ClayPit]) / 3600);
                    Resources.Iron += (int)Math.Floor(elapsed.TotalSeconds * WorldBuildings.Default[BuildingTypes.IronMine].GetTotalProduction(Buildings[BuildingTypes.IronMine]) / 3600);
                    int warehouse = WorldBuildings.Default[BuildingTypes.Warehouse].GetTotalProduction(Buildings[BuildingTypes.Warehouse]);
                    if (Resources.Wood > warehouse) Resources.Wood = warehouse;
                    if (Resources.Clay > warehouse) Resources.Clay = warehouse;
                    if (Resources.Iron > warehouse) Resources.Iron = warehouse;
                }

                // Update loyalty
                if (_loyaltyDate != DateTime.MinValue && _loyalty < 100)
                {
                    TimeSpan elapsed = World.Default.ServerTime - VillageDate;
                    _loyalty += World.Default.Speed * elapsed.Hours;
                    _loyaltyDate = _loyaltyDate.AddHours(elapsed.Hours);
                    if (_loyalty > 100) _loyalty = 100;
                }

                _villageDate = World.Default.ServerTime;
            }
        }

        /// <summary>
        /// Sets the type of the village based on the troops
        /// inside the village
        /// </summary>
        public void GuessVillageType()
        {
            if (Village.Reports.CurrentSituation != null)
            {
                int def = 0;
                int off = 0;
                bool flagNoble = false;
                bool flagScout = false;

                CurrentSituation current = Village.Reports.CurrentSituation;
                foreach (KeyValuePair<UnitTypes, int> pair in current.Defense.OwnTroops)
                {
                    Unit unit = WorldUnits.Default[pair.Key];
                    int value = pair.Value;
                    if (value > 0)
                    {
                        if (unit.Offense) off += value * unit.Cost.People;
                        else def += value * unit.Cost.People;

                        if (unit.Type == UnitTypes.Snob) flagNoble = true;
                        if (unit.Type == UnitTypes.Spy && value > 2000) flagScout = true;
                    }
                }

                if (off + def < 200)
                {
                    Village.Type = VillageType.Farm;
                }
                else
                {
                    if (def > off * 1.2) Village.Type = VillageType.Defense;
                    else if (off > def * 1.2) Village.Type = VillageType.Attack;
                    else Village.Type = VillageType.Attack | VillageType.Defense;
                }
                if (flagNoble) Village.Type |= VillageType.Noble;
                if (flagScout) Village.Type |= VillageType.Scout;
            }
        }

        /// <summary>
        /// Updates the troop levels of the village
        /// </summary>
        /// <param name="ownForce">Your own troops home</param>
        /// <param name="thereForce">The total amount of troops in the village</param>
        /// <param name="awayForce">Your own troops in other villages</param>
        /// <param name="movingForce">Your own troops moving towards another village</param>
        public void UpdateTroops(Dictionary<UnitTypes, int> ownForce, Dictionary<UnitTypes, int> thereForce, Dictionary<UnitTypes, int> awayForce, Dictionary<UnitTypes, int> movingForce)
        {
            Defense.Clear();
            foreach (UnitTypes key in awayForce.Keys)
            {
                Defense.OtherDefenses[key] = thereForce[key] - ownForce[key];
                Defense.OwnTroops[key] = ownForce[key] + awayForce[key] + movingForce[key];
                Defense.OutTroops[key] = awayForce[key] + movingForce[key];
            }

            _villageDate = World.Default.ServerTime;
            DefenseDate = _villageDate;
            GuessVillageType();
        }
        #endregion

        #region IXmlSerializable Members
        public void ReadXml(XmlReader r)
        {
            r.MoveToContent();
            r.ReadStartElement();

            // Info
            _villageDate = Convert.ToDateTime(r.GetAttribute(0), CultureInfo.InvariantCulture);
            r.ReadStartElement();
            _loyalty = Convert.ToSingle(r.GetAttribute(0), CultureInfo.InvariantCulture);
            _loyaltyDate = Convert.ToDateTime(r.GetAttribute(1), CultureInfo.InvariantCulture);
            r.ReadStartElement();

            // Comments
            if (r.IsStartElement("Comments"))
            {
                if (!r.IsEmptyElement)
                {
                    Village.SetComments(r.ReadElementString("Comments"));
                }
                else
                {
                    r.Read();
                }
            }

            // resources
            DateTime? tempDate;
            Resources.ReadXml(r, out tempDate);
            if (tempDate.HasValue) ResourcesDate = tempDate.Value;

            // defense
            if (r.HasAttributes)
                DefenseDate = Convert.ToDateTime(r.GetAttribute(0), CultureInfo.InvariantCulture);

            r.Read();
            if (r.IsStartElement("Unit"))
            {
                while (r.IsStartElement("Unit"))
                {
                    string typeDesc = r.GetAttribute(0);
                    if (Enum.IsDefined(typeof(UnitTypes), typeDesc))
                    {
                        var type = (UnitTypes)Enum.Parse(typeof(UnitTypes), typeDesc);
                        Defense.OwnTroops[type] = Convert.ToInt32(r.GetAttribute("OwnTroops"));
                        Defense.OutTroops[type] = Convert.ToInt32(r.GetAttribute("OutTroops"));
                        Defense.OtherDefenses[type] = Convert.ToInt32(r.GetAttribute("OtherDefenses"));
                    }
                    r.Read();
                }
                r.ReadEndElement();
            }
            
            // buildings
            Buildings.Clear();
            if (r.HasAttributes)
                BuildingsDate = Convert.ToDateTime(r.GetAttribute(0), CultureInfo.InvariantCulture);

            r.Read();
            if (r.IsStartElement("Building"))
            {
                while (r.IsStartElement("Building"))
                {
                    string typeDesc = r.GetAttribute(0);
                    if (Enum.IsDefined(typeof(BuildingTypes), typeDesc))
                    {
                        var type = (BuildingTypes)Enum.Parse(typeof(BuildingTypes), typeDesc);
                        int level = Convert.ToInt32(r.GetAttribute(1));
                        Buildings[type] = level;
                    }
                    r.Read();
                }
                r.ReadEndElement();
            }     

            r.ReadEndElement();
            // Info
        }

        public void WriteXml(XmlWriter w)
        {
            w.WriteStartElement("Village");
            w.WriteAttributeString("X", Village.X.ToString(CultureInfo.InvariantCulture));
            w.WriteAttributeString("Y", Village.Y.ToString(CultureInfo.InvariantCulture));

            // Info
            w.WriteStartElement("Info");
            w.WriteAttributeString("Date", _villageDate.ToString(CultureInfo.InvariantCulture));

            w.WriteStartElement("Loyalty");
            w.WriteAttributeString("Level", _loyalty.ToString(CultureInfo.InvariantCulture));
            w.WriteAttributeString("Date", _loyaltyDate.ToString(CultureInfo.InvariantCulture));
            w.WriteEndElement();

            // Comments
            w.WriteElementString("Comments", Village.Comments);

            Resources.WriteXml(w, ResourcesDate);

            w.WriteStartElement("Defense");
            if (DefenseDate != DateTime.MinValue)
                w.WriteAttributeString("Date", DefenseDate.ToString(CultureInfo.InvariantCulture));
            foreach (UnitTypes key in Defense)
            {
                w.WriteStartElement("Unit");
                w.WriteAttributeString("Type", key.ToString());
                w.WriteAttributeString("OwnTroops", Defense.OwnTroops[key].ToString(CultureInfo.InvariantCulture));
                w.WriteAttributeString("OutTroops", Defense.OutTroops[key].ToString(CultureInfo.InvariantCulture));
                w.WriteAttributeString("OtherDefenses", Defense.OtherDefenses[key].ToString(CultureInfo.InvariantCulture));
                w.WriteEndElement();
            }
            w.WriteEndElement();


            w.WriteStartElement("Buildings");
            if (BuildingsDate != DateTime.MinValue)
                w.WriteAttributeString("Date", BuildingsDate.ToString(CultureInfo.InvariantCulture));
            foreach (KeyValuePair<Building, int> build in Buildings)
            {
                w.WriteStartElement("Building");
                w.WriteAttributeString("Type", build.Key.Type.ToString());
                w.WriteAttributeString("Level", build.Value.ToString(CultureInfo.InvariantCulture));
                w.WriteEndElement();
            }
            w.WriteEndElement();
            
            w.WriteEndElement();
            // Info
        }
        #endregion

        #region Buildings
        /// <summary>
        /// Represents the building levels in a village
        /// </summary>
        public class VillageBuildings : IEnumerable<KeyValuePair<Building, int>>
        {
            #region Fields
            private readonly Dictionary<BuildingTypes, ReportBuilding> _buildings;
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets the building level
            /// </summary>
            public int this[BuildingTypes type]
            {
                get
                {
                    if (!_buildings.ContainsKey(type)) return 0;
                    return _buildings[type].Level;
                }
                set
                {
                    if (!_buildings.ContainsKey(type)) _buildings.Add(type, new ReportBuilding(WorldBuildings.Default[type], value));
                    else _buildings[type].Level = value;
                }
            }
            #endregion

            #region Constructors
            public VillageBuildings()
            {
                _buildings = new Dictionary<BuildingTypes, ReportBuilding>();
                foreach (Building build in WorldBuildings.Default)
                {
                    _buildings.Add(build.Type, new ReportBuilding(build, 0));
                }
            }
            #endregion

            #region Public Methods
            /// <summary>
            /// Clears the building information
            /// </summary>
            public void Clear()
            {
                foreach (ReportBuilding build in _buildings.Values)
                {
                    build.Level = 0;
                }
            }
            #endregion

            #region IEnumerable<KeyValuePair<BuildingTypes,int>> Members
            public IEnumerator<KeyValuePair<Building, int>> GetEnumerator()
            {
                foreach (ReportBuilding build in _buildings.Values)
                {
                    yield return new KeyValuePair<Building, int>(build.Building, build.Level);
                }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            #endregion
        }
        #endregion

        #region Units
        /// <summary>
        /// Represents the building levels in a village
        /// </summary>
        public class VillageDefense : IEnumerable<UnitTypes>
        {
            #region Properties
            /// <summary>
            /// Gets the total own troops
            /// </summary>
            public Units OwnTroops { get; private set; }

            /// <summary>
            /// Gets the own troops not at home
            /// </summary>
            public Units OutTroops { get; private set; }

            /// <summary>
            /// Gets the own troops not at home
            /// </summary>
            public Units OtherDefenses { get; private set; }
            #endregion

            #region Constructors
            public VillageDefense()
            {
                OwnTroops = new Units();
                OutTroops = new Units();
                OtherDefenses = new Units();
            }
            #endregion

            #region Public Methods
            /// <summary>
            /// Clears all unit information
            /// </summary>
            public void Clear()
            {
                OwnTroops = new Units();
                OutTroops = new Units();
                OtherDefenses = new Units();
            }
            #endregion

            #region IEnumerable Members
            public IEnumerator<UnitTypes> GetEnumerator()
            {
                foreach (KeyValuePair<UnitTypes, int> pair in OwnTroops)
                    yield return pair.Key;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            #endregion

            #region Units
            /// <summary>
            /// Represents all the unit types with amounts
            /// </summary>
            public class Units : IEnumerable<KeyValuePair<UnitTypes, int>>
            {
                #region Fields
                private readonly Dictionary<UnitTypes, int> _troops;
                #endregion

                #region Properties
                /// <summary>
                /// Gets or sets the amount of units
                /// </summary>
                public int this[UnitTypes type]
                {
                    get
                    {
                        if (!_troops.ContainsKey(type)) return 0;
                        return _troops[type];
                    }
                    set
                    {
                        if (!_troops.ContainsKey(type)) _troops.Add(type, value);
                        else _troops[type] = value;
                    }
                }
                #endregion

                #region Constructors
                public Units()
                {
                    _troops = new Dictionary<UnitTypes, int>();
                }
                #endregion

                #region IEnumerable Members
                public IEnumerator<KeyValuePair<UnitTypes, int>> GetEnumerator()
                {
                    foreach (KeyValuePair<UnitTypes, int> pair in _troops)
                        yield return pair;
                }

                System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }
                #endregion
            }
            #endregion
        }
        #endregion
    }
}
