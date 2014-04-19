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
        #region Properties
        /// <summary>
        /// Gets the building
        /// </summary>
        public Building Building { get; private set; }

        /// <summary>
        /// Gets or sets the current level
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the level before the report
        /// </summary>
        /// <remarks>Only relevant when rams or catapults were used</remarks>
        public int OriginalLevel { get; set; }
        #endregion

        #region Constructors
        /*public ReportBuilding(Building build)
        {

        }*/

        public ReportBuilding(Building build, int level)
        {
            Level = level;
            Building = build;
        }

        public ReportBuilding(Building build, int level, int origLevel)
        {
            Level = level;
            OriginalLevel = origLevel;
            Building = build;
        }
        #endregion

        #region BBCodes
        public override string ToString()
        {
            return string.Format("{0} ({1}", Building.Name, Level);
        }

        public string BbCode()
        {
            string str =  string.Format("{0} {1}", Building.BbCodeImage, Level);
            if (OriginalLevel != 0) str += string.Format(" (-{0})", OriginalLevel - Level);
            return str;
        }

        public string BbCodeExtended(int warehouse, int stock)
        {
            // warehouse & stock overload are only for economy buildings
            string str = string.Empty;
            if (Building.Production != null && Building.GetTotalProduction(Level) > 0)
            {
                str += string.Format(Building.ProductionImage, Building.GetTotalProduction(Level));
                str = string.Format("{0}", str);
                if (warehouse > 0)
                {
                    str += string.Format("|{0}hours", Math.Round((double)(warehouse - stock) / Building.GetTotalProduction(Level)));
                }
                return str;
            }
            return BbCode();
        }

        public virtual string BbCodeForResource(int warehouse, int stock)
        {
            string str = string.Empty;
            if (Building.Production != null && Building.GetTotalProduction(Level) > 0)
            {
                str += string.Format("{0:#,0}", Building.GetTotalProduction(Level));
                str = string.Format("{0}", str);
                if (warehouse > 0)
                {
                    str += string.Format("|{0}hours", Math.Round((double)(warehouse - stock) / Building.GetTotalProduction(Level)));
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
            return Building.GetTotalPeople(Level);
        }

        /// <summary>
        /// Gets the total points for the building level
        /// </summary>
        public int GetTotalPoints()
        {
            return Building.GetTotalPoints(Level);
        }

        /// <summary>
        /// Production for Clay Pit, Timber Camp, Iron Mine
        /// For Warehouse and Farm it is total space
        /// For the wall it is the defense
        /// </summary>
        public int GetTotalProduction()
        {
            return Building.GetTotalProduction(Level);
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
            w.WriteAttributeString("Level", Level.ToString());
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
            return new ReportBuilding(Building, Level, OriginalLevel);
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
