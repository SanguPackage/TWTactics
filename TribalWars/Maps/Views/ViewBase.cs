#region Imports
using System.Collections.Generic;
using TribalWars.Maps.Drawers;
using TribalWars.Villages;

#endregion

namespace TribalWars.Maps.Views
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

        /// <summary>
        /// A village can have one background drawer
        /// </summary>
        public bool Background
        {
            get { return !Decorator; }
        }

        /// <summary>
        /// A village can have multiple decorators
        /// enhancing a background view
        /// </summary>
        public bool Decorator { get; private set; }
        #endregion

        #region Constructors
        protected ViewBase(string name, bool decorator)
        {
            Name = name;
            Decorator = decorator;
            _drawers = new Dictionary<ViewData, DrawerData>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the drawer data for a village
        /// </summary>
        public abstract DrawerData GetDrawerData(Village village);

        /// <summary>
        /// Adds a new Drawer to the collection
        /// </summary>
        public virtual void AddDrawer(string drawerType, string drawerIcon, string drawerBonusIcon, int value, object extraValues)
        {
            var viewData = new ViewData(value, extraValues);
            _drawers.Add(viewData, new DrawerData(drawerType, drawerIcon, drawerBonusIcon, extraValues));
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
        #endregion
    }
}
