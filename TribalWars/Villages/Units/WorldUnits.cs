using System.Collections.Generic;

namespace TribalWars.Villages.Units
{
    /// <summary>
    /// A collection of all units in the current world
    /// </summary>
    public class WorldUnits : IEnumerable<Unit>
    {
        #region Fields
        private System.Windows.Forms.ImageList _list;

        private static Dictionary<UnitTypes, Unit> _units;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the ImageList with all unit images
        /// </summary>
        /// <remarks>Used in ImageListComboboxes</remarks>
        public System.Windows.Forms.ImageList ImageList
        {
            get
            {
                if (_list == null)
                {
                    _list = new System.Windows.Forms.ImageList();
                    foreach (Unit u in this)
                    {
                        _list.Images.Add(u.Image);
                    }
                }
                return _list;
            }
        }
        #endregion

        #region Indexer
        /// <summary>
        /// Gets the unit from its position in a report
        /// </summary>
        public Unit this[int position]
        {
            get
            {
                foreach (Unit u in _units.Values)
                {
                    if (u.Position == position)
                        return u;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the unit by its UnitType
        /// </summary>
        public Unit this[UnitTypes type]
        {
            get
            {
                if (_units.ContainsKey(type))
                    return _units[type];

                return null;
            }
        }
        #endregion

        #region Singleton
        static WorldUnits singleton;

        /// <summary>
        /// Gets the singleton instance
        /// </summary>
        public static WorldUnits Default
        {
            // simple enough
            get
            {
                if (singleton == null)
                {
                    singleton = new WorldUnits();
                }
                return singleton;
            }
        }
        #endregion

        #region Public Methods
        public void SetUnits(Dictionary<UnitTypes, Unit> units)
        {
            _list = null;
            _units = units;
        }
        #endregion

        #region IEnumerator Members
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets the different units in the world
        /// </summary>
        public IEnumerator<Unit> GetEnumerator()
        {
            foreach (Unit u in _units.Values)
                yield return u;
        }

        /// <summary>
        /// Gets the different unit types in the world
        /// </summary>
        public IEnumerator<UnitTypes> GetUnitTypes()
        {
            foreach (UnitTypes u in _units.Keys)
                yield return u;
        }
        #endregion
    }
}
