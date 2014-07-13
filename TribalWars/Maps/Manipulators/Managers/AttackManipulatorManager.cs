#region Using
using System.Collections.Generic;
using System.Xml;
using TribalWars.Controls;
using TribalWars.Controls.Polygons;
using TribalWars.Maps.Manipulators.Helpers;
using TribalWars.Maps.Manipulators.Implementations;
using TribalWars.Villages;

#endregion

namespace TribalWars.Maps.Manipulators.Managers
{
    /// <summary>
    /// The managing attackmanipulator
    /// </summary>
    public class AttackManipulatorManager : DefaultManipulatorManager
    {
        #region Fields
        private readonly AttackManipulator _attacker;
        #endregion

        #region Constructors
        public AttackManipulatorManager(Map map)
            : base(map)
        {
            // Active manipulators
            _attacker = new AttackManipulator(map, this);
            _manipulators.Add(_attacker);

            MapMover.RightClickToMove = false;
        }
        #endregion

        #region Methods
        public void AddTarget(Village village)
        {
            _attacker.AddTarget(village);
        }

        

        //protected internal override void ReadXmlCore(XmlReader r)
        //{
        //    _bbCode.ReadXmlCore(r);
        //}

        //protected internal override void WriteXmlCore(XmlWriter w)
        //{
        //    _bbCode.WriteXmlCore(w);
        //}

        //public override IContextMenu GetContextMenu(System.Drawing.Point location, Village village)
        //{
        //    return new NoPolygonContextMenu(_bbCode);
        //}

        ///// <summary>
        ///// Gets all villages that are in the polygons
        ///// </summary>
        //public IEnumerable<Village> GetAllPolygonVillages()
        //{
        //    foreach (Polygon poly in _bbCode.Polygons)
        //    {
        //        foreach (Village village in poly.GetVillages())
        //        {
        //            yield return village;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets all defined polygons
        ///// </summary>
        //public List<Polygon> GetAllPolygons()
        //{
        //    return _bbCode.Polygons;
        //}
        #endregion
    }
}
