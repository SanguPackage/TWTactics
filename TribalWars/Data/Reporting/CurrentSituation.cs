#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TribalWars.Data.Villages;
using TribalWars.Data.Resources;
using System.Xml;
using TribalWars.Data.Buildings;
using TribalWars.Data.Units;
using System.Globalization;
#endregion

namespace TribalWars.Data.Reporting
{
    /// <summary>
    /// Represents the current estimated situation of a village (DEFENDER)
    /// </summary>
    /// <remarks>The attacker side of the 'report' is empty</remarks>
    public class CurrentSituation
    {
        //TODO: this is not a Report.
        // there could be some common base (for display in the grid etc)
        // but CurrentSituation does not have an Attacker...
        // it does have a list of villages it has support in, current attacks
        // incomings, support from other villages etc...

        #region Fields
        private Village _village;
        private Resource _resources;
        private float _loyalty;
        private VillageBuildings _buildings;
        private VillageDefense _defense;

        internal DateTime _villageDate;
        internal DateTime _loyaltyDate;
        internal DateTime _resourcesDate;
        internal DateTime _defenseDate;
        internal DateTime _buildingsDate;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the defenses of the village
        /// </summary>
        public VillageDefense Defense
        {
            get { return _defense; }
        }

        /// <summary>
        /// Gets the buildings in the village
        /// </summary>
        public VillageBuildings Buildings
        {
            get { return _buildings; }
        }

        /// <summary>
        /// Gets the village
        /// </summary>
        public Village Village
        {
            get { return _village; }
        }

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
        public DateTime BuildingsDate
        {
            get { return _buildingsDate; }
        }

        /// <summary>
        /// Gets the last time defense levels were updated
        /// </summary>
        public DateTime DefenseDate
        {
            get { return _defenseDate; }
        }

        /// <summary>
        /// Gets the last known resource level
        /// </summary>
        public Resource Resources
        {
            get { return _resources; }
        }

        /// <summary>
        /// Gets the last time resource levels were updated
        /// </summary>
        public DateTime ResourcesDate
        {
            set { _resourcesDate = value; }
        }

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
            _village = village;
            _buildings = new VillageBuildings();
            _defense = new VillageDefense();
            _resources = new Resource();
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
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                g.DrawString("No estimated situation available.", SystemFonts.DefaultFont, Brushes.Black, new RectangleF(10, 10, g.VisibleClipBounds.Width - 10, 50), format);
            }
            else
            {
                g.TranslateTransform(10, 10);

                // resources
                float x = 5;
                int ResourceYOffset = 3;
                int ResourceXOffset = 5;
                int ImageXOffset = 20;
                Pen borderPen = new Pen(Color.Black, 2);
                int ImageYOffset = 20;
                int ImageYTextOffset = 5;
                if (_resourcesDate != DateTime.MinValue)
                {
                    g.DrawImage(TribalWars.Data.Resources.Images.wood, x, ResourceYOffset);
                    g.DrawString(_resources.WoodString, SystemFonts.DefaultFont, Brushes.Black, x + ImageXOffset, ResourceYOffset + ImageYTextOffset);

                    x += g.MeasureString(_resources.WoodString, SystemFonts.DefaultFont).Width;
                    x += ImageXOffset + ResourceXOffset;
                    g.DrawImage(TribalWars.Data.Resources.Images.clay, x, ResourceYOffset);
                    g.DrawString(_resources.ClayString, SystemFonts.DefaultFont, Brushes.Black, x + ImageXOffset, ResourceYOffset + ImageYTextOffset);

                    x += g.MeasureString(_resources.ClayString, SystemFonts.DefaultFont).Width;
                    x += ImageXOffset + ResourceXOffset;
                    g.DrawImage(TribalWars.Data.Resources.Images.iron, x, ResourceYOffset);
                    g.DrawString(_resources.IronString, SystemFonts.DefaultFont, Brushes.Black, x + ImageXOffset, ResourceYOffset + ImageYTextOffset);

                    x += g.MeasureString(_resources.IronString, SystemFonts.DefaultFont).Width;
                    x += ImageXOffset + ResourceXOffset;

                    g.DrawRectangle(borderPen, 0, 0, x, ResourceYOffset + ImageYOffset + 3);
                }

                // loyalty
                if (LoyaltyDate != DateTime.MinValue)
                {
                    g.DrawString(LoyaltyString, SystemFonts.DefaultFont, Brushes.Black, x + 15, ResourceYOffset + ImageYTextOffset);
                    float width = g.MeasureString(LoyaltyString, SystemFonts.DefaultFont).Width;
                    g.DrawRectangle(borderPen, x + 10, 0, width + 10, ResourceYOffset + ImageYOffset + 3);
                }

                g.TranslateTransform(0, ResourceYOffset + ImageYOffset + 10);

                float y = 10;
                // buildings
                y = 5;
                int column = 0;
                int xAdd = 0;
                if (BuildingsDate != DateTime.MinValue)
                {
                    string dateString = string.Format("Date: {0}", Tools.Common.GetPrettyDate(BuildingsDate, true));
                    g.DrawString(dateString, SystemFonts.DefaultFont, Brushes.Black,  5, 5);
                    y += g.MeasureString(dateString, SystemFonts.DefaultFont).Height + 5;
                    foreach (KeyValuePair<Building, int> building in this.Buildings)
                    {
                        column++;
                        g.DrawImage(building.Key.Image, 5 + xAdd, y);
                        g.DrawString(building.Value.ToString(), SystemFonts.DefaultFont, Brushes.Black, 5 + ImageXOffset + xAdd, y + 5);
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
                    foreach (KeyValuePair<UnitTypes, int> unit in this.Defense.OwnTroops)
                    {
                        column++;
                        g.DrawImage(WorldUnits.Default[unit.Key].Image, 5 + xAdd, y);
                        g.DrawString(Tools.Common.GetPrettyNumber(unit.Value), SystemFonts.DefaultFont, Brushes.Black, 5 + ImageXOffset + xAdd, y + 5);
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
            if (_resourcesDate < report.Date)
            {
                // Update resources
                if (report.ResourceHaulGot < report.ResourceHaulMax)
                {
                    // All resources taken
                    _resources = new Resource();
                    _resourcesDate = report.Date;
                }
                else if (report.ResourcesLeft.Set)
                {
                    // Espionage resource
                    _resources = report.ResourcesLeft.Clone();
                    _resourcesDate = report.Date;
                }
                else if (report.ResourcesHaul.Set && Resources.Set)
                {
                    // Subtract haul from stock
                    Update();
                    if (_resources.Wood < report.ResourcesHaul.Wood)
                    {
                        // If barbarian + we have building info
                        // We need to inform the user that the buildings have been upgraded?
                    }

                    _resources.Wood -= report.ResourcesHaul.Wood;
                    _resources.Clay -= report.ResourcesHaul.Clay;
                    _resources.Iron -= report.ResourcesHaul.Iron;
                }
            }
            if (_buildingsDate < report.Date && report.Buildings.Count > 0)
            {
                // Update buildings
                foreach (ReportBuilding building in report.Buildings.Values)
                {
                    _buildings[building.Building.Type] = building.Level;
                    //if (_buildings[building.Building.Type] .ContainsKey(building.Building.Type)) _buildings[building.Building.Type].Level = building.Level;
                    //else _buildings.Add(building.Building.Type, building.Clone());
                }
                _buildingsDate = report.Date;
            }
            if (_defenseDate < report.Date && (report.ReportFlag & ReportFlags.SeenDefense) != 0)
            {
                // Update defenses
                foreach (ReportUnit unit in report.Defense.Values)
                {
                    _defense.OwnTroops[unit.Unit.Type] = unit.AmountEnd;
                    //if (_defense.ContainsKey(unit.Unit.Type)) _defense[unit.Unit.Type] = unit.Clone();
                    //else _defense.Add(unit.Unit.Type, unit.Clone());

                    //_defense[unit.Unit.Type].AmountStart -= _defense[unit.Unit.Type].AmountLost;
                }
                _defenseDate = report.Date;

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
            if (_defenseDate == DateTime.MinValue || _defenseDate < report.Date)
            {
                foreach (ReportUnit unit in report.Attack.Values)
                {
                    if (_defense.OwnTroops[unit.Unit.Type] >= unit.AmountLost)
                        _defense.OwnTroops[unit.Unit.Type] -= unit.AmountLost;
                    else
                        _defense.OwnTroops[unit.Unit.Type] = 0;
                }
                _defenseDate = report.Date;
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
                    Resources.Wood += (int)Math.Floor(elapsed.TotalSeconds * WorldBuildings.Default[BuildingTypes.TimberCamp].GetTotalProduction(_buildings[BuildingTypes.TimberCamp]) / 3600);
                    Resources.Clay += (int)Math.Floor(elapsed.TotalSeconds * WorldBuildings.Default[BuildingTypes.ClayPit].GetTotalProduction(_buildings[BuildingTypes.ClayPit]) / 3600);
                    Resources.Iron += (int)Math.Floor(elapsed.TotalSeconds * WorldBuildings.Default[BuildingTypes.IronMine].GetTotalProduction(_buildings[BuildingTypes.IronMine]) / 3600);
                    int warehouse = WorldBuildings.Default[BuildingTypes.Warehouse].GetTotalProduction(_buildings[BuildingTypes.Warehouse]);
                    if (Resources.Wood > warehouse) Resources.Wood = warehouse;
                    if (Resources.Clay > warehouse) Resources.Clay = warehouse;
                    if (Resources.Iron > warehouse) Resources.Iron = warehouse;
                }

                // Update loyalty
                if (_loyaltyDate != DateTime.MinValue && _loyalty < 100)
                {
                    TimeSpan elapsed = World.Default.ServerTime - VillageDate;
                    _loyalty += World.Default.Speed * elapsed.Hours;
                    _loyaltyDate.AddHours(elapsed.Hours);
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

                CurrentSituation current = _village.Reports.CurrentSituation;
                foreach (KeyValuePair<UnitTypes, int> pair in current.Defense.OwnTroops)
                {
                    Unit unit = WorldUnits.Default[pair.Key];
                    int value = pair.Value;
                    if (value > 0)
                    {
                        if (unit.Offense) off += value * unit.Cost.People;
                        else def += value * unit.Cost.People;

                        if (unit.Type == UnitTypes.Nobleman) flagNoble = true;
                        if (unit.Type == UnitTypes.Scout && value > 2000) flagScout = true;
                    }
                }

                if (off + def < 200)
                {
                    _village.Type = VillageType.Farm;
                }
                else
                {
                    if (def > off * 1.2) _village.Type = VillageType.Defense;
                    else if (off > def * 1.2) _village.Type = VillageType.Attack;
                    else _village.Type = VillageType.Attack | VillageType.Defense;
                }
                if (flagNoble) _village.Type |= VillageType.Noble;
                if (flagScout) _village.Type |= VillageType.Scout;
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
            _defenseDate = _villageDate;
            GuessVillageType();
        }
        #endregion

        #region IXmlSerializable Members
        public void ReadXml(XmlReader r)
        {
            r.MoveToContent();
            r.ReadStartElement();

            // Info
            _villageDate = System.Convert.ToDateTime(r.GetAttribute(0), System.Globalization.CultureInfo.InvariantCulture);
            r.ReadStartElement();
            _loyalty = System.Convert.ToSingle(r.GetAttribute(0));
            _loyaltyDate = System.Convert.ToDateTime(r.GetAttribute(1), System.Globalization.CultureInfo.InvariantCulture);
            r.ReadStartElement();

            // Comments
            if (r.IsStartElement("Comments"))
            {
                if (!r.IsEmptyElement)
                    Village.SetComments(r.ReadElementString("Comments"));
                else
                    r.Read();
            }

            // resources
            DateTime? tempDate;
            _resources.ReadXml(r, out tempDate);
            if (tempDate.HasValue) _resourcesDate = tempDate.Value;

            // defense
            if (r.HasAttributes)
                _defenseDate = System.Convert.ToDateTime(r.GetAttribute(0), System.Globalization.CultureInfo.InvariantCulture);

            r.Read();
            if (r.IsStartElement("Unit"))
            {
                while (r.IsStartElement("Unit"))
                {
                    string typeDesc = r.GetAttribute(0);
                    if (Enum.IsDefined(typeof(UnitTypes), typeDesc))
                    {
                        UnitTypes type = (UnitTypes)Enum.Parse(typeof(UnitTypes), typeDesc);
                        _defense.OwnTroops[type] = System.Convert.ToInt32(r.GetAttribute("OwnTroops"));
                        _defense.OutTroops[type] = System.Convert.ToInt32(r.GetAttribute("OutTroops"));
                        _defense.OtherDefenses[type] = System.Convert.ToInt32(r.GetAttribute("OtherDefenses"));
                    }
                    r.Read();
                }
                r.ReadEndElement();
            }
            
            // buildings
            _buildings.Clear();
            if (r.HasAttributes)
                _buildingsDate = System.Convert.ToDateTime(r.GetAttribute(0), System.Globalization.CultureInfo.InvariantCulture);

            r.Read();
            if (r.IsStartElement("Building"))
            {
                while (r.IsStartElement("Building"))
                {
                    string typeDesc = r.GetAttribute(0);
                    if (Enum.IsDefined(typeof(BuildingTypes), typeDesc))
                    {
                        BuildingTypes type = (BuildingTypes)Enum.Parse(typeof(BuildingTypes), typeDesc);
                        int level = System.Convert.ToInt32(r.GetAttribute(1));
                        _buildings[type] = level;
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
            w.WriteAttributeString("X", Village.X.ToString());
            w.WriteAttributeString("Y", Village.Y.ToString());

            // Info
            w.WriteStartElement("Info");
            w.WriteAttributeString("Date", _villageDate.ToString(CultureInfo.InvariantCulture));

            w.WriteStartElement("Loyalty");
            w.WriteAttributeString("Level", _loyalty.ToString());
            w.WriteAttributeString("Date", _loyaltyDate.ToString(CultureInfo.InvariantCulture));
            w.WriteEndElement();

            // Comments
            w.WriteElementString("Comments", _village.Comments);

            _resources.WriteXml(w, _resourcesDate);

            w.WriteStartElement("Defense");
            if (_defenseDate != DateTime.MinValue)
                w.WriteAttributeString("Date", _defenseDate.ToString(System.Globalization.CultureInfo.InvariantCulture));
            foreach (UnitTypes key in _defense)
            {
                w.WriteStartElement("Unit");
                w.WriteAttributeString("Type", key.ToString());
                w.WriteAttributeString("OwnTroops", _defense.OwnTroops[key].ToString());
                w.WriteAttributeString("OutTroops", _defense.OutTroops[key].ToString());
                w.WriteAttributeString("OtherDefenses", _defense.OtherDefenses[key].ToString());
                w.WriteEndElement();
            }
            w.WriteEndElement();


            w.WriteStartElement("Buildings");
            if (_buildingsDate != DateTime.MinValue)
                w.WriteAttributeString("Date", _buildingsDate.ToString(System.Globalization.CultureInfo.InvariantCulture));
            foreach (KeyValuePair<Building, int> build in _buildings)
            {
                w.WriteStartElement("Building");
                w.WriteAttributeString("Type", build.Key.Type.ToString());
                w.WriteAttributeString("Level", build.Value.ToString());
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
            private Dictionary<BuildingTypes, ReportBuilding> _buildings;
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
            #region Fields
            private Units _ownHomeTroops;
            private Units _outTroops;
            private Units _otherDefenses;
            #endregion

            #region Properties
            /// <summary>
            /// Gets the total own troops
            /// </summary>
            public Units OwnTroops
            {
                get { return _ownHomeTroops; }
            }

            /// <summary>
            /// Gets the own troops not at home
            /// </summary>
            public Units OutTroops
            {
                get { return _outTroops; }
            }

            /// <summary>
            /// Gets the own troops not at home
            /// </summary>
            public Units OtherDefenses
            {
                get { return _otherDefenses; }
            }
            #endregion

            #region Constructors
            public VillageDefense()
            {
                _ownHomeTroops = new Units();
                _outTroops = new Units();
                _otherDefenses = new Units();
            }
            #endregion

            #region Public Methods
            /// <summary>
            /// Clears all unit information
            /// </summary>
            public void Clear()
            {
                _ownHomeTroops = new Units();
                _outTroops = new Units();
                _otherDefenses = new Units();
            }
            #endregion

            #region IEnumerable Members
            public IEnumerator<UnitTypes> GetEnumerator()
            {
                foreach (KeyValuePair<UnitTypes, int> pair in _ownHomeTroops)
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
                private Dictionary<UnitTypes, int> _troops;
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
