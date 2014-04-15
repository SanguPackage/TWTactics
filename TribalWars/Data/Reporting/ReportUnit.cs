using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Serialization;
using System.Xml;

using TribalWars.Data.Units;

namespace TribalWars.Data.Reporting
{
    /// <summary>
    /// Adds report info to a unit
    /// </summary>
    public class ReportUnit : IXmlSerializable, IEquatable<ReportUnit>
    {
        #region Fields
        private int _AmountStart = 0;
        private int _AmountLost = 0;
        private int _AmountOut = 0;
        private Unit _unit;	
        #endregion

        #region Properties
        /// <summary>
        /// Gets the unit
        /// </summary>
        public Unit Unit
        {
            get { return _unit; }
        }

        /// <summary>
        /// Gets or sets the starting amount of units
        /// </summary>
        public int AmountStart
        {
            get { return _AmountStart; }
            set
            {
                _AmountStart = value;
                if (value < 0) _AmountStart = 0;
            }
        }

        /// <summary>
        /// Gets or sets the amount of units lost in battle
        /// </summary>
        public int AmountLost
        {
            get { return _AmountLost; }
            set { _AmountLost = value; }
        }

        /// <summary>
        /// Gets or sets the amount of units outside the village
        /// </summary>
        /// <remarks>Defender only</remarks>
        public int AmountOut
        {
            get { return _AmountOut; }
            set { _AmountOut = value; }
        }

        /// <summary>
        /// Gets the amount of units left home at the end of the battle
        /// </summary>
        /// <remarks>Start - Lost</remarks>
        public int AmountEnd
        {
            get { return AmountStart - AmountLost; }
        }

        /// <summary>
        /// Gets the total amount of units left
        /// </summary>
        /// <remarks>Start - Lost + Out</remarks>
        public int AmountEndPlusOut
        {
            get { return AmountStart - AmountLost + AmountOut; }
        }

        /// <summary>
        /// Gets the total amount of start units
        /// </summary>
        /// <remarks>Start + Out</remarks>
        public int AmountTotal
        {
            get { return AmountStart + AmountOut; }
        }
        #endregion

        #region Constructors
        public ReportUnit(Unit unit)
        {
            _unit = unit;
        }

        public ReportUnit(Unit unit, int amount)
        {
            _AmountStart = amount;
            _unit = unit;
        }

        public ReportUnit(Unit unit, int amount, int amountOut)
        {
            _AmountStart = amount;
            _AmountOut = amountOut;
            _unit = unit;
        }

        public ReportUnit(Unit unit, int amount, int amountOut, int amountLost)
        {
            _AmountStart = amount;
            _AmountOut = amountOut;
            _AmountLost = amountLost;
            _unit = unit;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return string.Format("{0} ({1} - {2})", this.Unit.Name, _AmountStart.ToString("#,0"), _AmountLost.ToString("#,0"));
        }

        public override int GetHashCode()
        {
            return (_AmountStart + _AmountLost + _AmountOut + _unit.GetHashCode()).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ReportUnit);
        }

        public bool Equals(ReportUnit other)
        {
            if (other == null) return false;
            return _unit.Equals(other.Unit)
                && _AmountStart == other.AmountStart
                && _AmountLost == other.AmountLost
                && _AmountOut == other.AmountOut;
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

        /// <summary>
        /// Writes the ReportUnit to Xml
        /// </summary>
        /// <param name="w">The XmlWriter object</param>
        public void WriteXml(XmlWriter w)
        {
            WriteXml(w, true);
        }

        /// <summary>
        /// Writes the ReportUnit to Xml
        /// </summary>
        /// <param name="w">The XmlWriter object</param>
        /// <param name="writeLostTroops">A value indicating whether to add the "lost" troops to the Xml output</param>
        public void WriteXml(XmlWriter w, bool writeLostTroops)
        {
            w.WriteStartElement("Unit");
            w.WriteAttributeString("Type", Unit.Type.ToString());
            if (writeLostTroops)
            {
                w.WriteElementString("Home", AmountStart.ToString());
                w.WriteElementString("Lost", _AmountLost.ToString());
            }
            else
            {
                w.WriteElementString("Home", AmountStart.ToString());
            }
            w.WriteElementString("Out", _AmountOut.ToString());
            w.WriteEndElement();
        }

        /// <summary>
        /// Writes a list of ReportUnit to Xml
        /// </summary>
        /// <param name="w">The XmlWriter to write to</param>
        /// <param name="element">The main node name</param>
        /// <param name="troops">The troops list to write away</param>
        public static void WriteXmlList(XmlWriter w, string element, Dictionary<UnitTypes, ReportUnit> troops)
        {
            ReportUnit.WriteXmlList(w, element, troops, true);
        }

        /// <summary>
        /// Writes a list of ReportUnit to Xml
        /// </summary>
        /// <param name="w">The XmlWriter to write to</param>
        /// <param name="element">The main node name</param>
        /// <param name="troops">The troops list to write away</param>
        /// <param name="writeLostTroops">A value indicating whether to add the "lost" troops to the Xml output</param>
        public static void WriteXmlList(XmlWriter w, string element, Dictionary<UnitTypes, ReportUnit> troops, bool writeLostTroops)
        {
            ReportUnit.WriteXmlList(w, element, troops, writeLostTroops, null);
        }

        /// <summary>
        /// Writes a list of ReportUnit to Xml
        /// </summary>
        /// <param name="w">The XmlWriter to write to</param>
        /// <param name="element">The main node name</param>
        /// <param name="troops">The troops list to write away</param>
        /// <param name="writeLostTroops">A value indicating whether to add the "lost" troops to the Xml output</param>
        /// <param name="date">The date of the troop info</param>
        public static void WriteXmlList(XmlWriter w, string element, Dictionary<UnitTypes, ReportUnit> troops, bool writeLostTroops, DateTime? date)
        {
            w.WriteStartElement(element);
            if (date.HasValue)
                w.WriteAttributeString("Date", date.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            foreach (ReportUnit unit in troops.Values)
            {
                unit.WriteXml(w, writeLostTroops);
            }
            w.WriteEndElement();
        }

        /// <summary>
        /// Loads a list of nodes to a list of ReportUnits
        /// </summary>
        public static Dictionary<UnitTypes, ReportUnit> LoadXmlList(XmlReader r, out DateTime? date)
        {
            Dictionary<UnitTypes, ReportUnit> list = new Dictionary<UnitTypes, ReportUnit>();
            if (r.HasAttributes)
                date = System.Convert.ToDateTime(r.GetAttribute(0), System.Globalization.CultureInfo.InvariantCulture);
            else
                date = null;

            r.Read();
            r.MoveToContent();
            if (r.IsStartElement("Unit"))
            {
                while (r.IsStartElement("Unit"))
                {
                    string typeDesc = r.GetAttribute(0);
                    if (Enum.IsDefined(typeof(UnitTypes), typeDesc))
                    {
                        UnitTypes type = (UnitTypes)Enum.Parse(typeof(UnitTypes), typeDesc);
                        r.Read();
                        //r.Read();
                        int amount = System.Convert.ToInt32(r.ReadElementString("Home"));
                        ReportUnit unit = new ReportUnit(WorldUnits.Default[type], amount);
                        //r.Read();
                        if (r.IsStartElement("Lost"))
                        {
                            int lost = System.Convert.ToInt32(r.ReadElementString("Lost"));
                            unit.AmountLost = lost;
                        }
                        int outt = System.Convert.ToInt32(r.ReadElementString("Out"));
                        unit.AmountOut = outt;
                        list.Add(type, unit);
                    }
                    r.Read();
                    //r.ReadEndElement();
                    //r.Read();
                }
                r.Read();
            }

            return list;
        }
        #endregion

        #region ICloneable Members
        public ReportUnit Clone()
        {
            return new ReportUnit(_unit, _AmountStart, _AmountOut, _AmountLost);
        }

        public static Dictionary<UnitTypes, ReportUnit> Clone(Dictionary<UnitTypes, ReportUnit> originalList)
        {
            Dictionary<UnitTypes, ReportUnit> newList = new Dictionary<UnitTypes, ReportUnit>();
            foreach (KeyValuePair<UnitTypes, ReportUnit> pair in originalList)
            {
                newList.Add(pair.Key, new ReportUnit(pair.Value.Unit, pair.Value.AmountStart, pair.Value.AmountOut, pair.Value.AmountLost));
            }
            return newList;
        }
        #endregion
    }
}
