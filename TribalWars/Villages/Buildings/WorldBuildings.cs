#region Using
using System.Collections.Generic;
using System.Linq;

#endregion

namespace TribalWars.Villages.Buildings
{
    /// <summary>
    /// A collection of all buildings in the current world
    /// </summary>
    public class WorldBuildings : IEnumerable<Building>
    {
        #region Fields
        private static Dictionary<BuildingTypes, Building> _buildings;
        #endregion

        #region Indexer
        /// <summary>
        /// Gets the building from the name
        /// </summary>
        public Building this[string name]
        {
            get
            {
                return _buildings.Values.Where(x => x.Name == name).Select(x => _buildings[x.Type]).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the building from the building type
        /// </summary>
        public Building this[BuildingTypes name]
        {
            get
            {
                if (_buildings.ContainsKey(name))
                    return _buildings[name];

                return null;
            }
        }
        #endregion

        #region Singleton
        private static WorldBuildings _singleton;

        /// <summary>
        /// Gets the singleton instance
        /// </summary>
        public static WorldBuildings Default
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = new WorldBuildings();
                }
                return _singleton;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the world buildings dictionary
        /// </summary>
        public void SetBuildings(Dictionary<BuildingTypes, Building> buildings)
        {
            _buildings = buildings;
        }
        #endregion

        #region IEnumerator Members
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Building> GetEnumerator()
        {
            foreach (Building b in _buildings.Values)
                yield return b;
        }
        #endregion
    }
}
