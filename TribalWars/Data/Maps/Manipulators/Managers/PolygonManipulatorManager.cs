#region Using
using System.Collections.Generic;
using System.Xml;
using Janus.Windows.UI;
using Janus.Windows.UI.CommandBars;
using TribalWars.Controls.TWContextMenu;
using TribalWars.Controls;
using TribalWars.Data.Maps.Manipulators.Implementations;
using TribalWars.Data.Villages;
using TribalWars.Data.Maps.Manipulators.Helpers;
#endregion

namespace TribalWars.Data.Maps.Manipulators.Managers
{
    /// <summary>
    /// The managing polygonmanipulator
    /// </summary>
    public class PolygonManipulatorManager : DefaultManipulatorManager
    {
        #region Fields
        private readonly BbCodeManipulator _bbCode;
        #endregion

        #region Constructors
        public PolygonManipulatorManager(Map map)
            : base(map)
        {
            // Active manipulators
            _bbCode = new BbCodeManipulator(map, this);
            _manipulators.Add(_bbCode);

            MapMover.RightClickToMove = false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads state from stream
        /// </summary>
        protected internal override void ReadXmlCore(XmlReader r)
        {
            _bbCode.ReadXmlCore(r);
        }

        /// <summary>
        /// Saves state to stream
        /// </summary>
        protected internal override void WriteXmlCore(XmlWriter w)
        {
            _bbCode.WriteXmlCore(w);
        }

        public override IContextMenu GetContextMenu(System.Drawing.Point location, Village village)
        {
            return new PolygonContextMenu(_bbCode);
        }

        /// <summary>
        /// Gets all villages that are in the polygons
        /// </summary>
        public IEnumerable<Village> GetAllPolygonVillages()
        {
            foreach (Polygon poly in _bbCode.Polygons)
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
            return _bbCode.Polygons;
        }
        #endregion
    }
}
