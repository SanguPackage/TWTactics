#region Imports
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TribalWars.Data.Maps.Views
{
    /// <summary>
    /// Holds the data connected to a DrawerBase
    /// </summary>
    public class ViewData
    {
        #region Fields
        private int _value;
        private object _extraValues;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the extra values if necessary for determinating
        /// the DrawerBase for a certain village
        /// </summary>
        public object ExtraValues
        {
            get { return _extraValues; }
        }

        /// <summary>
        /// Gets the value that decides which DrawerBase to use
        /// for a certain village
        /// </summary>
        public int Value
        {
            get { return _value; }
        }
        #endregion

        #region Constructors
        public ViewData(int value)
        {
            _value = value;
        }

        public ViewData(int value, object extraValues)
        {
            _value = value;
            _extraValues = extraValues;
        }
        #endregion

        #region Public Methods
        public override int GetHashCode()
        {
            if (_extraValues != null) return _value.GetHashCode() + _extraValues.GetHashCode();
            return _value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return Equals(obj as ViewData);
        }

        public bool Equals(ViewData other)
        {
            return _value == other.Value && _extraValues == other.ExtraValues;
        }

        public override string ToString()
        {
            if (_extraValues == null) return string.Format("ViewData: {0}", _value.ToString());
            return string.Format("ViewData: {0} ({1})", _value.ToString(), _extraValues.ToString());
        }
        #endregion
    }
}
