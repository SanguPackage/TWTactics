using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using TribalWars.Villages;

namespace TribalWars.Data.Reporting
{
    /// <summary>
    /// Keeps information of the status of the village at the time of the report
    /// </summary>
    public class ReportVillage : IXmlSerializable
    {
        #region Fields
        private Village _village;
        private int _x;
        private int _y;
        private int _points;
        private string _tribeString;
        private string _playerString;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current data on the village
        /// </summary>
        public Village Village
        {
            get { return _village; }
        }

        /// <summary>
        /// Gets the X coordinate of the village
        /// </summary>
        public int X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the Y coordinate of the village
        /// </summary>
        public int Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets the points of the village at the time of the report
        /// </summary>
        public int Points
        {
            get { return _points; }
        }

        /// <summary>
        /// Gets the Tribe string representation of the village
        /// at the time of the report
        /// </summary>
        public string TribeString
        {
            get { return _tribeString; }
        }

        /// <summary>
        /// Gets the Player string representation of the village
        /// at the time of the report
        /// </summary>
        public string PlayerString
        {
            get { return _playerString; }
        }

        /// <summary>
        /// Gets the X|Y coordinates of the village
        /// </summary>
        public string VillageString
        {
            get { return string.Format("{0}|{1}", X, Y); }
        }
        #endregion

        #region Constructors
        public ReportVillage(string vil, string player)
        {
            _playerString = player;

            Regex reg = new Regex(@"\((?<x>\d+)\|(?<y>\d+)\)");
            Match match = reg.Match(vil);
            if (match.Success)
            {
                _x = System.Convert.ToInt32(match.Groups["x"].Value);
                _y = System.Convert.ToInt32(match.Groups["y"].Value);

                if (World.Default.Map != null)
                {
                    System.Drawing.Point pt = new System.Drawing.Point(X, Y);
                    if (World.Default.Villages.ContainsKey(pt))
                    {
                        _village = World.Default.Villages[pt];
                        _points = Village.Points;
                        if (Village.HasTribe) _tribeString = Village.Player.Tribe.Tag;
                    }
                }
            }
        }

        public ReportVillage(Village village)
        {
            _village = village;
            _x = village.X;
            _y = village.Y;
            //if (village.HasTribe) _tribeString = village.Player.Tribe.Tag;
            //_points = village.Points;
            //if (village.HasPlayer) _playerString = village.Player.Name;
        }

        internal ReportVillage()
        {

        }
        #endregion

        #region BBCode
        public override string ToString()
        {
            string str = string.Format("[player]{0}[/player]", PlayerString);
            str += Environment.NewLine + string.Format("[village]{0}[/village]", VillageString);
            return str;
        }

        public string BBCode()
        {
            if (Village == null)
            {
                string str = string.Empty;
                if (PlayerString.Length > 0) str += PlayerString;
                if (VillageString.Length > 0) str += Environment.NewLine + PlayerString;
                if (str.Length == 0) return "Unknown";
                return str;
            }
            else
            {
                if (Village.HasPlayer) return Village.Player.BbCode() + Environment.NewLine + Village.BbCode();
                return Village.BbCode();
            }
        }

        public string BBCodeExtended()
        {
            if (Village == null) return ToString() + Environment.NewLine;
            return Village.BbCodeExtended();
        }
        #endregion










        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader r)
        {
            r.Read();
            _x = System.Convert.ToInt32(Tools.XmlHelper.ReadXmlElement(r, "X"));
            _y = System.Convert.ToInt32(Tools.XmlHelper.ReadXmlElement(r, "Y"));
            if (World.Default.Villages != null)
            {
                if (World.Default.Villages.ContainsKey(new System.Drawing.Point(X, Y)))
                    _village = World.Default.Villages[new System.Drawing.Point(X, Y)];
            }
            _points = System.Convert.ToInt32(Tools.XmlHelper.ReadXmlElement(r, "Points"));
            _playerString = Tools.XmlHelper.ReadXmlElement(r, "Player");
            _tribeString = Tools.XmlHelper.ReadXmlElement(r, "Tribe");
            r.Read();
        }

        public void WriteXml(System.Xml.XmlWriter w)
        {
            w.WriteStartElement("Village");
            w.WriteElementString("X", X.ToString());
            w.WriteElementString("Y", Y.ToString());
            w.WriteElementString("Points", Points.ToString());
            w.WriteElementString("Player", PlayerString);
            w.WriteElementString("Tribe", TribeString);
            w.WriteEndElement();
        }
        #endregion
    }
}
