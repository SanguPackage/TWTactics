using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.Serialization;

using System.IO;
using System.Drawing;

using TribalWars.Data.Villages;
using TribalWars.Data.Players;

namespace TribalWars.Data.Monitoring
{
    /// <summary>
    /// Collects information on players to monitor progress
    /// </summary>
    public class Monitor : IXmlSerializable
    {
        #region Fields
        private Rectangle _rectangle;
        private List<MonitorPlayer> _players;
        private MonitorPlayer _abandon;

        private List<Player> _markedPlayers;
        private List<string> _markedDead;

        private Dictionary<Point, MonitorVillageCollection> _villages;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the rectangle in which villages are being monitored
        /// </summary>
        public Rectangle ActiveRectangle
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        /// <summary>
        /// Gets the villages in the monitored rectangle
        /// </summary>
        public Dictionary<Point, MonitorVillageCollection> Villages
        {
            get { return _villages; }
        }
        #endregion

        // TODO: need new event: downloaded
        // also start explictly for old data
        // do async in case there is a big rectangle :)
        //World.Default.EventPublisher.Loaded += new EventHandler<EventArgs>(EventPublisher_Loaded);

        #region Actual Monitoring
        /// <summary>
        /// Loads existing monitor data
        /// </summary>
        private void LoadData()
        {
            //if (_players == null)
            //{
            //    _players = new List<MonitorVillage>();
            //    foreach (string file in Directory.GetFiles(World.Default.Structure.CurrentWorldMonitorDirectory))
            //    {
            //        MonitorPlayer monitor = MonitorPlayer.Load(file);
            //        if (monitor != null)
            //            _players.Add(monitor);
            //    }
            //}
        }

        /// <summary>
        /// Updates the monitored rectangle
        /// </summary>
        public void InspectCurrentData()
        {
            LoadData();

            // List all villages in the rectangle
            _villages = new Dictionary<Point, MonitorVillageCollection>();
            foreach (Village village in World.Default.Villages.Values)
            {
                if (_rectangle.Contains(village.Location))
                {
                    //_villages.Add(village.Location, new MonitorVillageCollection(village));
                }
            }

            // Create a report on these guys

        }

        private void InspectCurrentDataCore(IAsyncResult result)
        {
            // todo Async for later
        }
        #endregion

        #region Saving
        /// <summary>
        /// Saves the data to disc
        /// </summary>
        public void Save()
        {
            foreach (MonitorVillageCollection collection in _villages.Values)
            {
                collection.Save();
            }
        }
        #endregion

        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader r)
        {
            r.ReadStartElement("Monitor");

            // Read monitor rectangle
            int x = System.Convert.ToInt32(r.ReadElementString("X"));
            int y = System.Convert.ToInt32(r.ReadElementString("Y"));
            int width = System.Convert.ToInt32(r.ReadElementString("Width"));
            int height = System.Convert.ToInt32(r.ReadElementString("Height"));
            _rectangle = new Rectangle(x, y, width, height);

            // Read marked players
            _markedPlayers = new List<Player>();
            r.ReadStartElement("Watch");
            r.Read();
            while (r.IsStartElement("Player"))
            {
                string player = r.ReadElementString("Player");
                string playerKey = player.ToUpper();
                if (World.Default.Players.ContainsKey(playerKey))
                {
                    _markedPlayers.Add(World.Default.Players[playerKey]);
                }
                else
                {
                    //_markedDead.Add(player);
                }
            }
            r.ReadEndElement();

            r.ReadEndElement();
        }

        public void WriteXml(XmlWriter w)
        {
            w.WriteStartElement("Monitor");

            // Write monitor rectangle
            w.WriteElementString("X", _rectangle.X.ToString());
            w.WriteElementString("Y", _rectangle.Y.ToString());
            w.WriteElementString("Width", _rectangle.Width.ToString());
            w.WriteElementString("Height", _rectangle.Height.ToString());

            // Write marked players
            w.WriteStartElement("Watch");
            foreach (Player player in _markedPlayers)
            {
                w.WriteElementString("Player", player.Name);
            }
            w.WriteEndElement();

            w.WriteEndElement();
        }
        #endregion
    }
}
