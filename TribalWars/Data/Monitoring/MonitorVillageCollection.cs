using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Drawing;
using System.Collections.ObjectModel;

using System.Xml;
using System.Xml.Serialization;

namespace TribalWars.Data.Monitoring
{
    /// <summary>
    /// Represents a village over time
    /// </summary>
    public class MonitorVillageCollection : Collection<MonitorVillage>
    {
        #region Fields
        private Point _location;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the location of the village
        /// </summary>
        public Point Location
        {
            get { return _location; }
        }
        #endregion

        #region Constructors
        #endregion

        #region Event Handlers
        #endregion

        #region Public Methods
        public void Save()
        {
            string path = World.Default.Structure.CurrentWorldMonitorDirectory;
            string file = string.Format("{0}|{1}.txt", Location.X.ToString(), Location.Y.ToString());
            if (File.Exists(path + file))
            {

            }
        }
        #endregion

        #region Private Implementation
        #endregion
    }
}