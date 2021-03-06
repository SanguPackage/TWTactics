using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using TribalWars.Villages.Buildings;
using TribalWars.Villages.Resources;
using TribalWars.Villages.Units;
using TribalWars.Worlds;

namespace TribalWars.Browsers.Reporting
{
    /// <summary>
    /// Represents a Tribal Wars attack report
    /// </summary>
    public class Report : IComparable<Report>, IEquatable<Report>, IXmlSerializable
    {
        #region Fields
        // players
        internal ReportVillage _defender;
        internal ReportVillage _attacker;

        // report date
        internal DateTime? _dateReport;
        internal DateTime _dateCopied;

        // General stuff
        internal string _luck;
        internal string _morale;
        internal string _winner;

        internal float _loyaltyBegin;
        internal int _loyaltyEnd;

        // resources
        internal int _resourceHaulGot;                 // stole this much resources
        internal int _resourceHaulMax;                 // max resources your troops could captures
        internal Resource _resourcesHaul;              // Actual haul
        internal Resource _resourcesLeft;              // Left in the village (if scouts went along)

        // identifies the report
        internal ReportTypes _reportType;
        internal ReportStatusses _reportStatus;
        internal ReportFlags _reportFlag;

        // output options
        internal ReportOutputOptions _reportOptions;

        // Troops
        internal Dictionary<UnitTypes, ReportUnit> _attack = new Dictionary<UnitTypes, ReportUnit>();
        internal Dictionary<UnitTypes, ReportUnit> _defense = new Dictionary<UnitTypes, ReportUnit>();

        internal Dictionary<BuildingTypes, ReportBuilding> _buildings;
        internal int _calculatedPoints;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the defending village
        /// </summary>
        public ReportVillage Defender
        {
            get { return _defender; }
        }

        /// <summary>
        /// Gets the attacking village
        /// </summary>
        public ReportVillage Attacker
        {
            get { return _attacker; }
        }

        /// <summary>
        /// Gets the report date or the copy date if the latter is unknown
        /// </summary>
        public DateTime Date
        {
            get
            {
                if (_dateReport.HasValue) return _dateReport.Value;
                return _dateCopied;
            }
        }

        /// <summary>
        /// Gets the report date
        /// </summary>
        public DateTime? ReportDate
        {
            get
            {
                return _dateReport;
            }
        }

        /// <summary>
        /// Gets the luck factor
        /// </summary>
        public string Luck
        {
            get { return _luck; }
        }

        /// <summary>
        /// Gets the morale
        /// </summary>
        public string Morale
        {
            get { return _morale; }
        }

        /// <summary>
        /// Gets the winner (attacker/defender)
        /// </summary>
        public string Winner
        {
            get { return _winner; }
        }

        /// <summary>
        /// Gets the loyalty before the attack
        /// </summary>
        public float LoyaltyBegin
        {
            get { return _loyaltyBegin; }
        }

        /// <summary>
        /// Gets the loyalty after the attack
        /// </summary>
        public int LoyaltyEnd
        {
            get { return _loyaltyEnd; }
        }

        /// <summary>
        /// Gets the total haul
        /// </summary>
        public int ResourceHaulGot
        {
            get { return _resourceHaulGot; }
        }

        /// <summary>
        /// Gets the maximum haul
        /// </summary>
        /// <remarks>If Got is different from Max the village is emptied</remarks>
        public int ResourceHaulMax
        {
            get { return _resourceHaulMax; }
        }

        /// <summary>
        /// Gets the haul details
        /// </summary>
        public Resource ResourcesHaul
        {
            get { return _resourcesHaul; }
        }

        /// <summary>
        /// The amount of resources left in the village
        /// </summary>
        /// <remarks>When a scout was sent with the attack</remarks>
        public Resource ResourcesLeft
        {
            get { return _resourcesLeft; }
        }

        /// <summary>
        /// Gets the report type
        /// </summary>
        public ReportTypes ReportType
        {
            get { return _reportType; }
        }

        /// <summary>
        /// Gets the winner of the attack
        /// </summary>
        public ReportStatusses ReportStatus
        {
            get { return _reportStatus; }
        }

        /// <summary>
        /// Get additional info on the attack
        /// </summary>
        public ReportFlags ReportFlag
        {
            get { return _reportFlag; }
        }

        /// <summary>
        /// Gets the output options
        /// </summary>
        public ReportOutputOptions ReportOptions
        {
            get { return _reportOptions; }
        }

        /// <summary>
        /// Gets the attack army
        /// </summary>
        public Dictionary<UnitTypes, ReportUnit> Attack
        {
            get { return _attack; }
        }

        /// <summary>
        /// Gets the defending army
        /// </summary>
        public Dictionary<UnitTypes, ReportUnit> Defense
        {
            get { return _defense; }
        }

        /// <summary>
        /// Gets the scouted buildings
        /// </summary>
        public Dictionary<BuildingTypes, ReportBuilding> Buildings
        {
            get { return _buildings; }
        }

        /// <summary>
        /// Gets the calculated points based on scouted buildings
        /// </summary>
        public int CalculatedPoints
        {
            get { return _calculatedPoints; }
        }
        #endregion

        #region Constructors
        public Report()
        {
            _dateCopied = World.Default.Settings.ServerTime;
            _resourcesHaul = new Resource();
            _resourcesLeft = new Resource();
            _defense = new Dictionary<UnitTypes, ReportUnit>();
            _buildings = new Dictionary<BuildingTypes, ReportBuilding>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the report date
        /// </summary>
        public void SetReportDate(DateTime date)
        {
            _dateReport = date;
        }

        /// <summary>
        /// Calculates the total amount of troops in the village
        /// </summary>
        /// <param name="units">The units dictionary</param>
        /// <param name="type">Calculate for what state of the battle</param>
        public static int GetTotalPeople(Dictionary<UnitTypes, ReportUnit> units, TotalPeople type)
        {
            int total = 0;
            foreach (ReportUnit unit in units.Values)
            {
                switch (type)
                {
                    case TotalPeople.Start:
                        total += unit.AmountStart * unit.Unit.Cost.People;
                        break;
                    case TotalPeople.Out:
                        total += unit.AmountOut * unit.Unit.Cost.People;
                        break;
                    case TotalPeople.Lost:
                        total += unit.AmountLost * unit.Unit.Cost.People;
                        break;
                    case TotalPeople.EndPlusOut:
                        total += unit.AmountEndPlusOut * unit.Unit.Cost.People;
                        break;
                    case TotalPeople.End:
                        total += unit.AmountEnd * unit.Unit.Cost.People;
                        break;
                }
            }
            return total;
        }

        /// <summary>
        /// Gets the population in the buildings
        /// </summary>
        public int GetTotalPeopleInBuildings()
        {
            int farmBuildings = 0;
            foreach (ReportBuilding build in Buildings.Values)
            {
                farmBuildings += build.GetTotalPeople();
            }
            return farmBuildings;
        }

        /// <summary>
        /// Gets the total resource space
        /// </summary>
        /// <remarks>This is the (warehouse - hiding place) * 3</remarks>
        public int GetTotalResourceRoom()
        {
            if (Buildings != null)
            {
                ReportBuilding warehouse = null, hide = null;
                if (Buildings.ContainsKey(BuildingTypes.Warehouse)) warehouse = Buildings[BuildingTypes.Warehouse];
                if (Buildings.ContainsKey(BuildingTypes.HidingPlace)) hide = Buildings[BuildingTypes.HidingPlace];

                if (warehouse != null)
                {
                    int room = warehouse.GetTotalProduction();
                    if (hide != null) room -= hide.GetTotalProduction();
                    return room * 3;
                }
            }
            return 0;
        }
        #endregion

        

        #region Printers
        /// <summary>
        /// Generates standard BBCode output
        /// </summary>
        public string BBCode()
        {
            return ReportBbCodeOutput.Generate(this);
        }
        #endregion

        #region Painters
        protected int ImageXOffset = 20;
        protected int ImageYOffset = 20;
        protected int ImageYTextOffset = 5;

        protected int ResourceYOffset = 10;

        protected static Pen _borderPen = new Pen(Color.Black, 2);

        /// <summary>
        /// Draws the report to a canvas
        /// </summary>
        public virtual void Paint(Graphics g)
        {
            ReportPainter.Paint(g, this);
        }
        #endregion


        #region Static Methods
        /// <summary>
        /// Returns an image corresponding with the outcome of the battle
        /// </summary>
        public static Image GetCircleImage(Report report)
        {
            switch (report.ReportStatus)
            {
                case ReportStatusses.Failure:
                    return Images.ReportRed;
                case ReportStatusses.Success:
                    if (report.ReportType == ReportTypes.Scout)
                    {
                        return Images.ReportBlue;
                    }
                    else
                    {
                        return Images.ReportGreen;
                    }
                case ReportStatusses.HalfSuccess:
                    return Images.ReportYellow;
            }
            return null;
        }

        /// <summary>
        /// Returns an image corresponding with the nature of the battle
        /// </summary>
        public static Image GetInfoImage(Report report)
        {
            switch (report.ReportType)
            {
                case ReportTypes.Attack:
                    return BuildingImages.Barracks;
                case ReportTypes.Fake:
                    return Images.Fake;
                case ReportTypes.Farm:
                    return BuildingImages.Farm;
                case ReportTypes.Noble:
                    return UnitImages.Noble;
                case ReportTypes.Scout:
                    return UnitImages.Scout;
            }
            return null;
        }
        #endregion



        #region IComparable<Report> Members
        public int CompareTo(Report other)
        {
            TimeSpan span = other.Date - Date;
            if (span.TotalSeconds > 0) return -1;
            else return 1;
        }

        public override int GetHashCode()
        {
            return Date.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Report);
        }

        public bool Equals(Report other)
        {
            if (other == null) return false;
            if (other.Date != Date || other.Defender.Village != Defender.Village || other.Attacker.Village != Attacker.Village)
                return false;

            foreach (ReportUnit unit in _attack.Values)
            {
                if (!other._attack.ContainsKey(unit.Unit.Type) || !unit.Equals(other._attack[unit.Unit.Type]))
                    return false;
            }
            foreach (ReportUnit unit in _defense.Values)
            {
                if (!other._defense.ContainsKey(unit.Unit.Type) || !unit.Equals(other._defense[unit.Unit.Type]))
                    return false;
            }

            return true;
        }
        #endregion

        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader r)
        {
            r.MoveToContent();
            string reportDate = r.GetAttribute(0);
            DateTime reportDateTest;
            if (DateTime.TryParse(reportDate, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out reportDateTest))
                _dateReport = reportDateTest;
            _dateCopied = Convert.ToDateTime(r.GetAttribute(1), System.Globalization.CultureInfo.InvariantCulture);

            r.Read();
            _reportType = (ReportTypes)Convert.ToInt32(r.ReadElementString("Type"));
            _reportStatus = (ReportStatusses)Convert.ToInt32(r.ReadElementString("Status"));
            _reportFlag = (ReportFlags)Convert.ToInt32(r.ReadElementString("Flags"));

            r.MoveToContent();
            _loyaltyBegin = Convert.ToInt32(r.GetAttribute(0));
            _loyaltyEnd = Convert.ToInt32(r.GetAttribute(1));
            r.Read();
            r.MoveToContent();

            _attacker = new ReportVillage();
            _attacker.ReadXml(r);
            //r.ReadEndElement();
            //r.MoveToContent();

            _defender = new ReportVillage();
            _defender.ReadXml(r);
            //r.Read();
            //r.MoveToContent();
            //r.ReadStartElement();
            //r.MoveToContent();

            _resourcesHaul = new Resource();
            r.Read();
            _resourcesHaul.ReadXml(r);
            //r.ReadEndElement();
            _resourceHaulMax = Convert.ToInt32(r.ReadElementString("Max"));
            r.Read();
            r.Read();

            _resourcesLeft = new Resource();
            _resourcesLeft.ReadXml(r);
            r.Read();

            DateTime? tempDate;
            _attack = ReportUnit.LoadXmlList(r, out tempDate);

            _defense = ReportUnit.LoadXmlList(r, out tempDate);

            _buildings = ReportBuilding.LoadXmlList(r, out tempDate);

            r.ReadEndElement();
            r.Read();
        }

        public virtual void WriteXml(XmlWriter w)
        {
            w.WriteStartElement("Report");
            if (_dateReport.HasValue)
                w.WriteAttributeString("Report", _dateReport.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            else
                w.WriteAttributeString("Report", "");
            w.WriteAttributeString("Copied", _dateCopied.ToString(System.Globalization.CultureInfo.InvariantCulture));

            w.WriteElementString("Type", ((int)_reportType).ToString());
            w.WriteElementString("Status", ((int)_reportStatus).ToString());
            w.WriteElementString("Flags", ((int)_reportFlag).ToString());

            w.WriteStartElement("Loyalty");
            w.WriteAttributeString("Begin", _loyaltyBegin.ToString());
            w.WriteAttributeString("End", _loyaltyEnd.ToString());
            w.WriteEndElement();

            Attacker.WriteXml(w);
            Defender.WriteXml(w);

            w.WriteStartElement("Haul");
            _resourcesHaul.WriteXml(w);
            w.WriteElementString("Max", _resourceHaulMax.ToString());
            w.WriteEndElement();

            w.WriteStartElement("Scouted");
            _resourcesLeft.WriteXml(w);
            w.WriteEndElement();

            ReportUnit.WriteXmlList(w, "Attack", Attack, true, null);
            ReportUnit.WriteXmlList(w, "Defense", Defense, true, null);

            ReportBuilding.WriteXmlList(w, Buildings, null);

            w.WriteEndElement();
        }
        #endregion
    }
}
