using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TribalWars.Villages;
using TribalWars.Villages.ContextMenu;
using TribalWars.Villages.Units;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    public partial class MapDistanceControl : UserControl
    {
        #region Fields
        private Village _target;
        private readonly MapDistanceCollectionControl _parent;
        private readonly ImageList _unitImageList;
        #endregion

        #region Properties
        public Village Target
        {
            get { return _target; }
            set
            {
                _target = value;
                if (value != null)
                {
                    Coords.Text = value.LocationString;
                    _Village.Text = value.Name;
                    _Player.Text = value.HasPlayer ? value.Player.ToString() : "";
                    _Tribe.Text = value.HasTribe ? value.Player.Tribe.ToString() : "";
                }
                DistanceContainer.Controls.Clear();
            }
        }

        public DateTime AttackDate
        {
            get { return Date.Value; }
            set { Date.Value = value; }
        }
        #endregion

        #region Constructors
        public MapDistanceControl(MapDistanceCollectionControl parent, ImageList imageList)
        {
            InitializeComponent();

            _parent = parent;
            _unitImageList = imageList;
            Date.Value = World.Default.Settings.ServerTime.AddHours(8);
        }
        #endregion

        #region EventHandlers
        private void _Village_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var cm = new VillageContextMenu(World.Default.Map, _target);
                cm.Show(_Village, e.Location);
            }
            else if (e.Button == MouseButtons.Left)
            {
                World.Default.Map.EventPublisher.SelectVillages(null, _target, VillageTools.PinPoint);
            }
        }

        private void _Village_DoubleClick(object sender, EventArgs e)
        {
            World.Default.Map.EventPublisher.SelectVillages(null, _target, VillageTools.PinPoint);
            World.Default.Map.SetCenter(_target.Location);
        }

        private void _Player_MouseClick(object sender, MouseEventArgs e)
        {
            if (_target.HasPlayer)
            {
                if (e.Button == MouseButtons.Right)
                {
                    var cm = new PlayerContextMenu(World.Default.Map, _target.Player, false);
                    cm.Show(_Player, e.Location);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    World.Default.Map.EventPublisher.SelectVillages(null, _target.Player, VillageTools.PinPoint);
                }
            }
        }

        private void Player_DoubleClick(object sender, EventArgs e)
        {
            if (_target.HasPlayer)
            {
                World.Default.Map.EventPublisher.SelectVillages(this, _target.Player, VillageTools.PinPoint);
                World.Default.Map.SetCenter(_target.Player);
            }
        }

        private void _Tribe_MouseClick(object sender, MouseEventArgs e)
        {
            if (_target.HasTribe)
            {
                if (e.Button == MouseButtons.Right)
                {
                    var cm = new TribeContextMenu(World.Default.Map, _target.Player.Tribe);
                    cm.Show(_Tribe, e.Location);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    World.Default.Map.EventPublisher.SelectVillages(null, _target.Player.Tribe, VillageTools.PinPoint);
                }
            }
        }

        private void Tribe_DoubleClick(object sender, EventArgs e)
        {
            if (_target.HasTribe)
            {
                World.Default.Map.EventPublisher.SelectVillages(this, _target.Player.Tribe, VillageTools.PinPoint);
                World.Default.Map.SetCenter(_target.Player.Tribe);
            }
        }
        #endregion

        #region Calculate Times
        public void Calculate()
        {
            foreach (Control ctl in DistanceContainer.Controls)
            {
                var distanceControl = ctl as MapDistanceVillageControl;
                if (distanceControl != null) distanceControl.Calculate();
            }
        }
        #endregion

        #region MapDistanceVillage Control Helpers
        public MapDistanceVillageControl AddVillage(Village village, Unit slowestUnit = null)
        {
            var ctl = new MapDistanceVillageControl(_unitImageList, village, this);
            if (slowestUnit != null)
            {
                ctl.UnitSelectedIndex = slowestUnit.Position;
            }
            DistanceContainer.Controls.Add(ctl);

            World.Default.Map.Invalidate(false);

            return ctl;
        }

        public void Remove(MapDistanceVillageControl villageControl)
        {
            DistanceContainer.Controls.Remove(villageControl);
        }
        #endregion

        public string GetPlan(bool bbCodes)
        {
            return GetPlan(bbCodes, true);
        }

        private string GetPlan(bool bbCodes, bool standAlone)
        {
            Sort();

            if (_target != null)
            {
                var str = new StringBuilder();
                if (standAlone)
                {
                    str.AppendLine("*** Attack Plan ***");
                    str.AppendLine(!bbCodes ? _target.ToString() : _target.BbCode());
                    str.AppendLine();

                    str.AppendLine("Arrival time: " + Date.Value.ToString(Date.CustomFormat));
                    str.AppendLine("Current time: " + World.Default.Settings.ServerTime.ToString(Date.CustomFormat));
                    str.AppendLine();
                }
                for (int i = 0; i < DistanceContainer.Controls.Count - 1; i++)
                {
                    var mdv = DistanceContainer.Controls[i] as MapDistanceVillageControl;
                    if (mdv != null)
                    {
                        str.Append(mdv.ToString(bbCodes, standAlone ? null : _target));
                    }
                }
                return str.ToString().Trim();
            }
            return string.Empty;
        }

        public List<MapDistanceVillageComparor> GetVillageList()
        {
            var list = new List<MapDistanceVillageComparor>();
            for (int i = 0; i < DistanceContainer.Controls.Count - 1; i++)
            {
                var mdv = DistanceContainer.Controls[i] as MapDistanceVillageControl;
                if (mdv != null) list.Add(new MapDistanceVillageComparor(mdv, mdv.TimeLeftBeforeSendTotalSeconds));
            }
            return list;
        }

        public void Sort()
        {
            List<MapDistanceVillageComparor> list = GetVillageList();
            list.Sort();
            for (int i = 0; i < DistanceContainer.Controls.Count - 1; i++)
            {
                var mdv = DistanceContainer.Controls[i] as MapDistanceVillageControl;
                if (mdv != null)
                {
                    mdv.SetVillage(list[i].Village, list[i].UnitSelectedIndex);
                }
            }
        }

        private void Close_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _parent.Remove(this);
            DistanceContainer.Controls.Clear();
            Dispose();
        }

        public void Clear()
        {
            DistanceContainer.Controls.Clear();
        }

        public AttackPlanInfo GetPlanInfo()
        {
            var plan = new AttackPlanInfo
                {
                    Target = _target,
                    ArrivalTime = AttackDate
                };

            for (int i = 0; i < DistanceContainer.Controls.Count - 1; i++)
            {
                var mdv = DistanceContainer.Controls[i] as MapDistanceVillageControl;
                if (mdv != null)
                {
                    plan.Attacks.Add(new AttackPlanFrom
                        {
                            Attacker = mdv.Village,
                            SlowestUnit = WorldUnits.Default[mdv.UnitSelectedIndex]
                        });
                }
            }

            return plan;
        }
    }
}
