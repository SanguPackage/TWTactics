#region Imports
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TribalWars.Maps.Displays;
using TribalWars.Maps.Drawers;
using TribalWars.Maps.Markers;
using TribalWars.Villages;
using TribalWars.WorldTemplate;

#endregion

namespace TribalWars.Maps.Views
{
    public interface IView
    {
        string Name { get; set; }

        string Type { get; set; }

        void ReadDrawerXml(XElement drawer);

        object[] WriteDrawerXml();
    }

    /// <summary>
    /// The base class for a representation (or view)
    /// of the map
    /// </summary>
    /// <remarks>Views are used to display a Marker on the map</remarks>
    public abstract class ViewBase : IView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the view
        /// </summary>
        public string Name { get; set; }

        public string Type { get; set; }
        #endregion

        #region Constructors
        protected ViewBase(string name, string type)
        {
            Name = name;
            Type = type;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a new Drawer to the collection
        /// </summary>
        public abstract void ReadDrawerXml(XElement drawer);

        public abstract object[] WriteDrawerXml();

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
        #endregion
    }
}
