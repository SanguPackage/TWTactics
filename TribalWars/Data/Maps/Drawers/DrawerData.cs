#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
#endregion

namespace TribalWars.Data.Maps.Drawers
{
    /// <summary>
    /// Holds the data for creating a DrawerBase
    /// for any DisplayType
    /// </summary>
    public class DrawerData
    {
        #region Fields
        private string _shape;
        private string _icon;
        private object _extraDrawerInfo;
        private object _value;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets which shapedrawer to use
        /// </summary>
        public string ShapeDrawer
        {
            get { return _shape; }
            set { _shape = value; }
        }

        /// <summary>
        /// Gets or sets which icondrawer to use
        /// </summary>
        public string IconDrawer
        {
            get { return _icon; }
            set { _icon = value; }
        }

        /// <summary>
        /// Extra info for creating the DrawerBase that
        /// is not present in the MarkerGroup.
        /// When it is the same colors for every MarkerGroup.
        /// </summary>
        /// <remarks>
        /// For example a color for a BorderDrawer based on the
        /// village type, ...
        /// </remarks>
        public object ExtraDrawerInfo
        {
            get { return _extraDrawerInfo; }
            set { _extraDrawerInfo = value; }
        }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion

        #region Constructors
        public DrawerData(string shape, string icon)
        {
            _shape = shape;
            _icon = icon;
        }

        public DrawerData(string shape, string icon, object extra)
        {
            _shape = shape;
            _icon = icon;
            _extraDrawerInfo = extra;
        }

        public DrawerData(string shape, string icon, object extra, object value)
        {
            _shape = shape;
            _icon = icon;
            _extraDrawerInfo = extra;
            _value = value;
        }

        public DrawerData(DrawerData data)
        {
            _shape = data.ShapeDrawer;
            _icon = data.IconDrawer;
            _extraDrawerInfo = data.ExtraDrawerInfo;
            _value = data.Value;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return string.Format("Shape:{0},Icon:{1}", _shape, _icon);
        }
        #endregion
    }
}
