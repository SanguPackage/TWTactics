#region Imports
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Villages;
#endregion

namespace TribalWars.Data.Maps.Views
{
    /// <summary>
    /// The base class for a representation (or view)
    /// of the map
    /// </summary>
    /// <remarks>Views are used to display a Marker on the map</remarks>
    public abstract class ViewBase
    {
        #region Fields
        protected readonly Dictionary<ViewData, DrawerData> _drawers;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of the view
        /// </summary>
        public string Name { get; private set; }
        #endregion

        #region Constructors
        protected ViewBase(string name)
        {
            Name = name;
            _drawers = new Dictionary<ViewData, DrawerData>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the drawer data for a village
        /// </summary>
        public abstract DrawerData GetDrawer(Village village);

        /// <summary>
        /// Adds a new Drawer to the collection
        /// </summary>
        public virtual void AddDrawer(string drawerType, string drawerIcon, string drawerBonusIcon, int value, object extraValues)
        {
            _drawers.Add(new ViewData(value, extraValues), new DrawerData(drawerType, drawerIcon, drawerBonusIcon, extraValues));
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
        #endregion
    }
}
