using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TribalWars.Villages.Units
{
    /// <summary>
    /// A collection of all units in the current world
    /// </summary>
    public class WorldUnits : IEnumerable<Unit>
    {
        #region Fields
        private static Dictionary<UnitTypes, Unit> _units;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the ImageList with all unit images
        /// </summary>
        /// <remarks>Used in ImageListComboboxes</remarks>
        public ImageList ImageList
        {
            get { return GetImageList(false); }
        }

        public ImageList GetImageList(bool addEmpty)
        {
            var list = new ImageList();
            if (addEmpty)
            {
                list.Images.Add(new Bitmap(18, 18));
            }
            foreach (Unit u in this)
            {
                list.Images.Add(u.Image);
            }
            return list;
        }
        #endregion

        #region Indexer
        /// <summary>
        /// Gets the unit from its position in a report
        /// </summary>
        public Unit this[int position]
        {
            get { return _units.Values.FirstOrDefault(u => u.Position == position); }
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
