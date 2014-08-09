#region Using
using System.Collections.Generic;
using System.Xml;
using TribalWars.Controls;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Villages;

#endregion

namespace TribalWars.Maps.Polygons
{
    /// <summary>
    /// The managing polygonmanipulator
    /// </summary>
    public class PolygonManipulatorManager : DefaultManipulatorManager
    {
        #region Fields
        private readonly PolygonDrawerManipulator _polygonDrawer;
        #endregion

        #region Constructors
        public PolygonManipulatorManager(Map map)
            : base(map)
        {
            // Active manipulators
            _polygonDrawer = new PolygonDrawerManipulator(map, this);
            AddManipulator(_polygonDrawer);

            MapMover.RightClickToMove = false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads state from stream
        /// </summary>
        protected override void ReadXmlCore(XmlReader r)
        {
            _polygonDrawer.ReadXmlCore(r);
        }

        /// <summary>
        /// Saves state to stream
        /// </summary>
        protected override void WriteXmlCore(XmlWriter w)
        {
            _polygonDrawer.WriteXmlCore(w);
        }

        public override IContextMenu GetContextMenu(System.Drawing.Point location, Village village)
        {
            return new NoPolygonContextMenu(_polygonDrawer);
        }

        /// <summary>
        /// Gets all villages that are in the polygons
        /// </summary>
        public IEnumerable<Village> GetAllPolygonVillages()
        {
            foreach (Polygon poly in _polygonDrawer.Polygons)
            {
                foreach (Village village in poly.GetVillages())
                {
                    yield return village;
                }
            }
        }

        /// <summary>
        /// Gets all defined polygons
        /// </summary>
        public List<Polygon> GetAllPolygons()
        {
            return _polygonDrawer.Polygons;
        }
        #endregion
    }
}
