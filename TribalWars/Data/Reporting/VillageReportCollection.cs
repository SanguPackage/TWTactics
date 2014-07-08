using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Globalization;
using TribalWars.Villages;

namespace TribalWars.Data.Reporting
{
    /// <summary>
    /// Wrapper for a collection of Reports with the same defending village
    /// Plus a wrapper of the estimated current situation
    /// </summary>
    public class VillageReportCollection : Collection<Report>, IXmlSerializable
    {
        #region Fields
        private CurrentSituation _currentSituation;
        private bool _reportsLoaded;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current (assumed) situation
        /// of the village
        /// </summary>
        public CurrentSituation CurrentSituation
        {
            get { return _currentSituation; }
        }

        /// <summary>
        /// Gets a value indicating whether the existing reports have been loaded
        /// </summary>
        public bool ReportsLoaded
        {
            get { return _reportsLoaded; }
        }

        /// <summary>
        /// Gets the village location
        /// </summary>
        public Point VillageLocation
        {
            get { return _currentSituation.Village.Location; }
        }

        /// <summary>
        /// Gets the location of the saved reports
        /// </summary>
        public string File
        {
            get
            {
                return string.Format("{0}{1}-{2}.txt", World.Default.Structure.CurrentWorldReportsDirectory, _currentSituation.Village.X, _currentSituation.Village.Y);
            }
        }
        #endregion

        #region Constructors
        public VillageReportCollection(Village village)
        {
            Load(village);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Loads the existing reports for the village
        /// </summary>
        public void Load(Village village)
        {
            if (!_reportsLoaded)
            {
                _reportsLoaded = true;
                _currentSituation = new CurrentSituation(village);
                if (System.IO.File.Exists(File))
                {
                    var sets = new XmlReaderSettings();
                    sets.IgnoreWhitespace = true;
                    sets.CloseInput = true;
                    using (XmlReader r = XmlReader.Create(File, sets))
                    {
                        ReadXml(r);
                    }
                }
            }
        }

        /// <summary>
        /// Saves the report collection
        /// </summary>
        public void Save()
        {
            if (_reportsLoaded)
            {
                var sets = new XmlWriterSettings();
                sets.Indent = true;
                sets.IndentChars = " ";
                using (XmlWriter w = XmlWriter.Create(File, sets))
                {
                    WriteXml(w);
                }
            }
        }

        /// <summary>
        /// Saves only the user comments
        /// </summary>
        public void SaveComments()
        {
            if (!new FileInfo(File).Exists)
            {
                Save();
            }
            else
            {
                var xdoc = XDocument.Load(File);
                xdoc.Descendants("Comments").First().Value = _currentSituation.Village.Comments;
                xdoc.Save(File);
            }
        }

        /// <summary>
        /// Saves a report in the correct Collections
        /// and updates the current situation of the villages
        /// </summary>
        public static void Save(Report report)
        {
            Village vil = report.Defender.Village;
            if (vil != null)
            {
                vil.Reports.CurrentSituation.UpdateDefender(report);
                vil.Reports.Add(report);
                vil.Reports.Save();
            }

            vil = report.Attacker.Village;
            if (vil != null)
            {
                vil.Reports.Add(report);
                vil.Reports.CurrentSituation.UpdateAttacker(report);
                vil.Reports.Save();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Cancel the addition if we already have this report
        /// </summary>
        protected override void InsertItem(int index, Report item)
        {
            bool addReport = true;
            foreach (Report report in Items)
            {
                if (report.Equals(item))
                    addReport = false;
            }

            if (addReport) base.InsertItem(index, item);
        }
        #endregion

        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader r)
        {
            _currentSituation.ReadXml(r);
            r.ReadStartElement();
            while (r.IsStartElement("Report"))
            {
                var report = new Report();
                report.ReadXml(r);
                Add(report);
            }
        }

        public void WriteXml(XmlWriter w)
        {
            w.WriteStartDocument();
            _currentSituation.WriteXml(w);

            w.WriteStartElement("Reports");
            foreach (Report report in Items)
            {
                report.WriteXml(w);
            }
            w.WriteEndElement();

            w.WriteEndDocument();
        }
        #endregion
    }
}