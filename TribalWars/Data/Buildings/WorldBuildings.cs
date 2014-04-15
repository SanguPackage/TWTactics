#region Using
using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.IO;
#endregion

namespace TribalWars.Data.Buildings
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
                foreach (Building building in _buildings.Values)
                {
                    if (building.Name == name)
                        return _buildings[building.Type];
                }
                return null;
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
                else
                    return null;
            }
        }
        #endregion

        #region Singleton
        static WorldBuildings singleton;

        /// <summary>
        /// Gets the singleton instance
        /// </summary>
        public static WorldBuildings Default
        {
            // simple enough
            // using a simpleton because there
            // is no such thing as a static indexer
            // TODO: heh heh wrong singleton implementation :)
            // The private ctor is also missing
            get
            {
                if (singleton == null)
                {
                    singleton = new WorldBuildings();
                }
                return singleton;
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
