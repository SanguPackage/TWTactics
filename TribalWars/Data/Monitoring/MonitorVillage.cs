using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Xml;
using System.Xml.Serialization;

using TribalWars.Data.Players;
using TribalWars.Data.Villages;

namespace TribalWars.Data.Monitoring
{
    /// <summary>
    /// Represents a village in the monitored rectangle at a certain time
    /// </summary>
    public class MonitorVillage : IComparable<MonitorVillage>, IXmlSerializable
    {
        #region Fields
        private Point _location;
        private int _points;
        private string _name;
        private Player _player;
        private string _playerName;	
        private DateTime _date;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the player name of the village
        /// </summary>
        /// <remarks>We save this as players can be deleted</remarks>
        public string PlayerName
        {
            get { return _playerName; }
        }

        /// <summary>
        /// Gets the time of the data
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
        }

        /// <summary>
        /// Gets the owner of the village
        /// </summary>
        public Player Player
        {
            get { return _player; }
        }

        /// <summary>
        /// Gets the current name of the village
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the current points of the village
        /// </summary>
        public int Points
        {
            get { return _points; }
        }

        /// <summary>
        /// Gets the location of the village
        /// </summary>
        public Point Location
        {
            get { return _location; }
        }
        #endregion

        #region Constructors
        public MonitorVillage(Village village, DateTime time)
        {
            _location = village.Location;
            _points = village.Points;
            _name = village.Name;
            _player = village.Player;
            if (village.HasPlayer) _playerName = village.Player.Name;
            _date = time;
        }
        #endregion

        #region Event Handlers
        #endregion

        #region Public Methods
        #endregion

        #region Private Implementation
        #endregion

        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void ReadXml(XmlReader r)
        {
            
        }

        public void WriteXml(XmlWriter w)
        {
            
        }
        #endregion

        #region IComparable<MonitorVillage> Members
        public int CompareTo(MonitorVillage other)
        {
            if (other == null) return -1;
            return _date.CompareTo(other.Date);
        }
        #endregion
    }
}