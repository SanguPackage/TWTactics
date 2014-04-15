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
        private string _name;
        private Types _type;
        private Categories _category;

        // TODO: SortedDictionary en comparer meegeven
        protected Dictionary<ViewData, DrawerData> _drawers;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of the view
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets a value indicating what kind of
        /// display it is
        /// </summary>
        public Types Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets a value indicating whether this
        /// is a backgrounddrawer or a decorator
        /// </summary>
        public Categories Category
        {
            get { return _category; }
        }
        #endregion

        #region Constructors
        public ViewBase(string name, Types type, Categories category)
        {
            _name = name;
            _type = type;
            _category = category;
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
        public virtual void AddDrawer(string drawerType, string drawerIcon, int value, object extraValues)
        {
            _drawers.Add(new ViewData(value, extraValues), new DrawerData(drawerType, drawerIcon, extraValues));
        }

        /// <summary>
        /// Adds a new Drawer to the collection
        /// </summary>
        public virtual void AddDrawer(DrawerData drawer, int value)
        {
            _drawers.Add(new ViewData(value), drawer);
        }

        /// <summary>
        /// Adds a new Drawer to the collection
        /// </summary>
        public virtual void AddDrawer(DrawerData drawer, int value, object extraValues)
        {
            _drawers.Add(new ViewData(value, extraValues), drawer);
        }

        public override string ToString()
        {
            return string.Format("{0} ({1}, {2})", _name, _type.ToString(), _category.ToString());
        }
        #endregion
    }
}
