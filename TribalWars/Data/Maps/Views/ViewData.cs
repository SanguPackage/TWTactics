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
    public sealed class ViewData
    {
        #region Properties
        /// <summary>
        /// Gets the extra values if necessary for determinating
        /// the DrawerBase for a certain village
        /// </summary>
        public object ExtraValues { get; private set; }

        /// <summary>
        /// Gets the value that decides which DrawerBase to use
        /// for a certain village
        /// </summary>
        public int Value { get; private set; }
        #endregion

        #region Constructors
        public ViewData(int value)
        {
            Value = value;
        }

        public ViewData(int value, object extraValues)
        {
            Value = value;
            ExtraValues = extraValues;
        }
        #endregion

        #region Public Methods
        public override int GetHashCode()
        {
            if (ExtraValues != null) return Value.GetHashCode() + ExtraValues.GetHashCode();
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return Equals(obj as ViewData);
        }

        public bool Equals(ViewData other)
        {
            return Value == other.Value && ExtraValues == other.ExtraValues;
        }

        public override string ToString()
        {
            if (ExtraValues == null) return string.Format("ViewData: {0}", Value.ToString());
            return string.Format("ViewData: {0} ({1})", Value.ToString(), ExtraValues.ToString());
        }
        #endregion
    }
}
