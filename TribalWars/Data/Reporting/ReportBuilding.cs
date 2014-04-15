using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.Serialization;

using TribalWars.Data.Buildings;

namespace TribalWars.Data.Reporting
{
    /// <summary>
    /// Adds report info to a building
    /// </summary>
    public class ReportBuilding : IXmlSerializable
    {
        #region Fields
        private int _level;
        private int _originalLevel;
        private Building _building;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the building
        /// </summary>
        public Building Building
        {
            get { return _building; }
        }

        /// <summary>
        /// Gets or sets the current level
        /// </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        /// <summary>
        /// Gets or sets the level before the report
        /// </summary>
        /// <remarks>Only relevant when rams or catapults were used</remarks>
        public int OriginalLevel
        {
            get { return _originalLevel; }
            set { _originalLevel = value; }
        }
        #endregion

        #region Constructors
        /*public ReportBuilding(Building build)
        {

        }*/

        public ReportBuilding(Building build, int level)
        {
            _level = level;
            _building = build;
        }

        public ReportBuilding(Building build, int level, int origLevel)
        {
            _level = level;
            _originalLevel = origLevel;
            _building = build;
        }
        #endregion

        #region BBCodes
        public override string ToString()
        {
            return string.Format("{0} ({1}", this.Building.Name, this._level);
        }

        public string BBCode()
        {
            string str =  string.Format("{0} {1}", Building.BBCodeImage, _level);
            if (_originalLevel != 0) str += string.Format(" (-{0})", _originalLevel - _level);
            return str;
        }

        public string BBCodeExtended(int warehouse, int stock)
        {
            // warehouse & stock overload are only for economy buildings
            string str = string.Empty;
            if (Building.Production != null && Building.GetTotalProduction(_level) > 0)
            {
                str += string.Format(Building._productionImage, Building.GetTotalProduction(_level));
                str = string.Format("{0}", str);
                if (warehouse > 0)
                {
                    str += string.Format("|{0}hours", Math.Round((double)(warehouse - stock) / Building.GetTotalProduction(_level)));
                }
                return str;
            }
            return BBCode();
        }

        public virtual string BBCodeForResource(int warehouse, int stock)
        {
            string str = string.Empty;
            if (Building.Production != null && _building.GetTotalProduction(_level) > 0)
            {
                str += string.Format("{0:#,0}", _building.GetTotalProduction(_level));
                str = string.Format("{0}", str);
                if (warehouse > 0)
                {
                    str += string.Format("|{0}hours", Math.Round((double)(warehouse - stock) / _building.GetTotalProduction(_level)));
                }
                str += "";
            }
            return str;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the total farm space required for the level of the building
        /// </summary>
        public int GetTotalPeople()
        {
            return _building.GetTotalPeople(_level);
        }

        /// <summary>
        /// Gets the total points for the building level
        /// </summary>
        public int GetTotalPoints()
        {
            return _building.GetTotalPoints(_level);
        }

        /// <summary>
        /// Production for Clay Pit, Timber Camp, Iron Mine
        /// For Warehouse and Farm it is total space
        /// For the wall it is the defense
        /// </summary>
        public int GetTotalProduction()
        {
            return _building.GetTotalProduction(_level);
        }
        #endregion

        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader r)
        {
            throw new NotImplementedException("");
        }

        public void WriteXml(XmlWriter w)
        {
            w.WriteStartElement("Building");
            w.WriteAttributeString("Type", Building.Type.ToString());
            w.WriteAttributeString("Level", _level.ToString());
            w.WriteEndElement();
        }

        public static void WriteXmlList(XmlWriter w, Dictionary<BuildingTypes, ReportBuilding> list)
        {
            ReportBuilding.WriteXmlList(w, list, null);
        }

        public static void WriteXmlList(XmlWriter w, Dictionary<BuildingTypes, ReportBuilding> list, DateTime? date)
        {
            w.WriteStartElement("Buildings");
            if (date.HasValue)
                w.WriteAttributeString("Date", date.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            foreach (ReportBuilding build in list.Values)
            {
                build.WriteXml(w);
            }
            w.WriteEndElement();
        }

        public static Dictionary<BuildingTypes, ReportBuilding> LoadXmlList(XmlReader r, out DateTime? date)
        {
            Dictionary<BuildingTypes, ReportBuilding> list = new Dictionary<BuildingTypes, ReportBuilding>();
            if (r.HasAttributes)
                date = System.Convert.ToDateTime(r.GetAttribute(0), System.Globalization.CultureInfo.InvariantCulture);
            else
                date = null;

            r.Read();
            r.MoveToContent();
            if (r.IsStartElement("Building"))
            {
                while (r.IsStartElement("Building"))
                {
                    string typeDesc = r.GetAttribute(0);
                    if (Enum.IsDefined(typeof(BuildingTypes), typeDesc))
                    {
                        BuildingTypes type = (BuildingTypes)Enum.Parse(typeof(BuildingTypes), typeDesc);
                        int level = System.Convert.ToInt32(r.GetAttribute(1));
                        ReportBuilding build = new ReportBuilding(WorldBuildings.Default[type], level);
                        list.Add(type, build);
                    }
                    r.Read();
                    r.Read();
                }
                r.Read();
                r.MoveToContent();
            }

            return list;
        }
        #endregion

        #region ICloneable Members
        public ReportBuilding Clone()
        {
            return new ReportBuilding(_building, _level, _originalLevel);
        }

        public static Dictionary<BuildingTypes, ReportBuilding> Clone(Dictionary<BuildingTypes, ReportBuilding> originalList)
        {
            Dictionary<BuildingTypes, ReportBuilding> newList = new Dictionary<BuildingTypes, ReportBuilding>();
            foreach (KeyValuePair<BuildingTypes, ReportBuilding> pair in originalList)
            {
                newList.Add(pair.Key, new ReportBuilding(pair.Value.Building, pair.Value.Level, pair.Value.OriginalLevel));
            }
            return newList;
        }
        #endregion
    }
}
