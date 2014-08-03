#region Imports
using System.Xml.Linq;
#endregion

namespace TribalWars.Maps.Drawing.Views
{
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
