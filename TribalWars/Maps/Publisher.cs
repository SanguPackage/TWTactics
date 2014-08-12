#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TribalWars.Browsers.Reporting;
using TribalWars.Maps.AttackPlans;
using TribalWars.Maps.AttackPlans.EventArg;
using TribalWars.Maps.Manipulators;
using TribalWars.Maps.Manipulators.Implementations;
using TribalWars.Maps.Manipulators.Implementations.Church;
using TribalWars.Maps.Polygons;
using TribalWars.Villages;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Maps
{
    /// <summary>
    /// Raises events for a specific map
    /// </summary>
    public class Publisher
    {
        #region Fields
        private readonly Map _map;
        #endregion

        #region Events
        public event EventHandler<EventArgs> VillagesDeselected;
        public event EventHandler<VillagesEventArgs> VillagesSelected;
        public event EventHandler<PlayerEventArgs> PlayerSelected;
        public event EventHandler<TribeEventArgs> TribeSelected;
        public event EventHandler<ReportEventArgs> ReportSelected;

        public event EventHandler<PolygonEventArgs> PolygonActivated;

        public event EventHandler<MapLocationEventArgs> LocationChanged;
        public event EventHandler<ManipulatorEventArgs> ManipulatorChanged;

        public event EventHandler<AttackEventArgs> TargetAdded;
        public event EventHandler<AttackUpdateEventArgs> TargetUpdated;
        public event EventHandler<AttackEventArgs> TargetRemoved;
        public event EventHandler<AttackEventArgs> TargetSelected;

        public event EventHandler<ChurchEventArgs> ChurchChanged;
        #endregion

        #region Constructors
        public Publisher(Map map)
        {
            _map = map;
        }
        #endregion

        #region Publish Methods
        #region Church
        public void ChurchChange(Village village, int level, bool redrawMaps = true)
        {
            if (ChurchChanged != null)
            {
                var church = _map.Manipulators.ChurchManipulator.GetChurch(village);
                if (church == null)
                {
                    church = new ChurchInfo(village, level);
                }
                else
                {
                    church.ChurchLevel = level;
                }

                ChurchChanged(null, new ChurchEventArgs(church));

                if (redrawMaps)
                {
                    World.Default.DrawMaps(false);
                }
            }
        }
        #endregion

        #region Selection Events
        /// <summary>
        /// Deselect villages previously selected with SelectVillages events
        /// </summary>
        public void Deselect(object sender)
        {
            if (VillagesDeselected != null)
            {
                VillagesDeselected(sender, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Publishes an event for several villages
        /// </summary>
        public void SelectVillages(object sender, IEnumerable<Village> vil, VillageTools action)
        {
            if (VillagesSelected != null)
            {
                VillagesSelected(sender, new VillagesEventArgs(vil, action));
            }
        }

        /// <summary>
        /// Publishes an event for one village
        /// </summary>
        public void SelectVillages(object sender, Village village, VillageCommand action)
        {
            if (VillagesSelected != null)
                VillagesSelected(sender, new VillagesEventArgs(village, action));
        }

        /// <summary>
        /// Publishes an event for the villages of one player
        /// </summary>
        public void SelectVillages(object sender, Player ply, VillageTools action)
        {
            if (ply != null)
                VillagesSelected(sender, new PlayerEventArgs(ply, action));
        }

        /// <summary>
        /// Publishes an event for all the villages in one tribe
        /// </summary>
        public void SelectVillages(object sender, Tribe tribe, VillageTools action)
        {
            if (tribe != null)
                VillagesSelected(sender, new TribeEventArgs(tribe, action));
        }

        /// <summary>
        /// Publishes an event for one tribe
        /// </summary>
        public void SelectTribe(object sender, Tribe tribe, VillageTools tool)
        {
            if (TribeSelected != null)
            {
                TribeSelected(sender, new TribeEventArgs(tribe, tool));
            }
        }

        /// <summary>
        /// Publishes an event for one player
        /// </summary>
        public void SelectPlayer(object sender, Player player, VillageTools tool)
        {
            if (PlayerSelected != null)
            {
                PlayerSelected(sender, new PlayerEventArgs(player, tool));
            }
        }

        /// <summary>
        /// Publishes an event for a report
        /// </summary>
        public void SelectReport(object sender, Report report)
        {
            if (ReportSelected != null)
            {
                ReportSelected(sender, new ReportEventArgs(report));
            }
        }
        #endregion

        #region Attack Events
        public void AttackAddTarget(object sender, Village village)
        {
            DateTime arrivalTime = World.Default.Map.Manipulators.AttackManipulator.GetDefaultArrivalTime();
            AddTarget(sender, new AttackEventArgs(new AttackPlan(village, arrivalTime), null));
        }

        public void AttackUpdateTarget(object sender, AttackUpdateEventArgs e)
        {
            if (TargetUpdated != null)
                TargetUpdated(sender, e);
        }

        private void AddTarget(object sender, AttackEventArgs e)
        {
            if (TargetAdded != null)
                TargetAdded(sender, e);

            AttackSelect(sender, e);
        }

        public void AttackRemoveTarget(object sender, AttackPlan plan)
        {
            var e = new AttackEventArgs(plan, null);
            if (TargetRemoved != null)
                TargetRemoved(sender, e);
        }

        public void AttackSelect(object sender, AttackPlan plan)
        {
            var e = new AttackEventArgs(plan, null);
            AttackSelect(sender, e);
        }

        public void AttackSelect(object sender, AttackPlanFrom attacker)
        {
            var e = new AttackEventArgs(attacker.Plan, attacker);
            AttackSelect(sender, e);
        }

        private void AttackSelect(object sender, AttackEventArgs e)
        {
            if (TargetSelected != null)
                TargetSelected(sender, e);
        }
        #endregion

        #region Action Events
        // EditorBrowsableState.Never: A design error.

        /// <summary>
        /// Should be called only from one location: <see cref="Map"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal void SetMapCenter(object sender, MapLocationEventArgs e)
        {
            if (LocationChanged != null)
                LocationChanged(sender, e);
        }

        /// <summary>
        /// Should be called only from one location: <see cref="ManipulatorManagerController"/>
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ChangeManipulator(object sender, ManipulatorEventArgs e)
        {
            if (ManipulatorChanged != null)
                ManipulatorChanged(sender, e);
        }

        /// <summary>
        /// Ship villages to the Polygon control
        /// </summary>
        public void ActivatePolygon(object sender, IEnumerable<Polygon> polygons)
        {
            if (PolygonActivated != null)
                PolygonActivated(sender, new PolygonEventArgs(polygons));
        }
        #endregion
        #endregion
    }
}
