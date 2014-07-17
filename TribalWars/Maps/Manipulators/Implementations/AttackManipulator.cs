#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using TribalWars.Controls;
using TribalWars.Controls.Polygons;
using TribalWars.Maps.Displays;
using TribalWars.Maps.Manipulators.Helpers;
using TribalWars.Maps.Manipulators.Helpers.EventArgs;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Maps.Manipulators.Implementations
{
    /// <summary>
    /// Plan attacks on the map
    /// </summary>
    public class AttackManipulator : ManipulatorBase
    {
        #region Constants
        #endregion

        #region Fields
        private readonly AttackManipulatorManager _parent;
        private List<Village> _plans;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public AttackManipulator(Map map, AttackManipulatorManager parent)
            : base(map)
        {
            _parent = parent;
            _plans = new List<Village>();

            map.EventPublisher.VillagesSelected += EventPublisherOnVillagesSelected;
        }

        private void EventPublisherOnVillagesSelected(object sender, VillagesEventArgs e)
        {
            if (e.Tool == VillageTools.DistanceCalculationTarget)
            {
                _plans.Add(e.FirstVillage);
            }
            else if (e.Tool == VillageTools.DistanceCalculation)
            {
                
            }
        }
        #endregion

        #region Events
        public override void Paint(MapPaintEventArgs e)
        {
            _parent.Draw(e.Graphics);
        }

        protected internal override bool MouseDownCore(MapMouseEventArgs e)
        {
            if (e.Village != null)
            {
                if (e.MouseEventArgs.Button == MouseButtons.Left)
                {
                    World.Default.Map.EventPublisher.SelectVillages(this, e.Village, VillageTools.DistanceCalculationTarget);
                    return true;
                }
                else if (e.MouseEventArgs.Button == MouseButtons.Right && (e.Village.Player == World.Default.You || World.Default.You.Empty))
                {
                    World.Default.Map.EventPublisher.SelectVillages(this, e.Village, VillageTools.DistanceCalculation);
                    return true;
                }
            }
            return false;
        }

        protected internal override bool MouseUpCore(MapMouseEventArgs e)
        {
            
            return false;
        }

        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            return false;
        }

        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
        {
            return false;
        }
        #endregion

        #region Public Methods
        public override void Dispose()
        {
        }
        #endregion

        #region Persistence
        protected internal override void WriteXmlCore(XmlWriter w)
        {
        }

        protected internal override void ReadXmlCore(XmlReader r)
        {
        }

        /// <summary>
        /// Cleanup anything when switching worlds or settings
        /// </summary>
        protected internal override void CleanUp()
        {

        }
        #endregion

        #region Private Implementation
        #endregion
    }
}