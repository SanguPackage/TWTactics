using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TribalWars.Villages;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;

namespace TribalWars.Controls.AttackPlan
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
                DistanceContainer.RowCount = 1;
            }
        }

        public DateTime AttackDate
        {
            get { return Date.Value; }
            set { Date.Value = value; }
        }

        public MapDistanceCollectionControl Collection
        {
            get { return _parent; }
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
        private void Date_ValueChanged(object sender, EventArgs e)
        {
            CalculateVariable();
        }

        private void Player_DoubleClick(object sender, EventArgs e)
        {
            if (_target.HasPlayer)
            {
                World.Default.Map.EventPublisher.SelectVillages(this, _target.Player, VillageTools.PinPoint);
            }
        }

        private void Tribe_DoubleClick(object sender, EventArgs e)
        {
            if (_target.HasTribe)
            {
                World.Default.Map.EventPublisher.SelectVillages(this, _target.Player.Tribe, VillageTools.PinPoint);
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

        public void CalculateVariable()
        {
            foreach (Control ctl in DistanceContainer.Controls)
            {
                var distanceControl = ctl as MapDistanceVillageControl;
                if (distanceControl != null) distanceControl.CalculateVariable();
            }
        }
        #endregion

        #region MapDistanceVillage Control Helpers
        public MapDistanceVillageControl AddVillage(Village village)
        {
            //Timer.Enabled = true;

            DistanceContainer.RowStyles[DistanceContainer.RowCount - 1] = new RowStyle(SizeType.Absolute, 60F);
            DistanceContainer.RowCount++;
            DistanceContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var ctl = new MapDistanceVillageControl(_unitImageList, village, this, DistanceContainer.RowCount - 1);
            DistanceContainer.Controls.Add(ctl, 0, DistanceContainer.RowCount - 2);

            World.Default.Map.Invalidate(false);

            return ctl;
        }

        public void Remove(MapDistanceVillageControl villageControl)
        {
            if (villageControl.Row == DistanceContainer.RowCount - 1)
            {
                villageControl.Dispose();
                DistanceContainer.Controls.Remove(villageControl);
                if (DistanceContainer.RowCount > 1)
                {
                    DistanceContainer.RowCount--;
                }
                DistanceContainer.RowStyles[DistanceContainer.RowCount - 1] = new RowStyle(SizeType.AutoSize);
            }
            else
            {
                for (int i = villageControl.Row - 1; i < DistanceContainer.RowCount - 2; i++)
                {
                    var mdv1 = DistanceContainer.Controls[i] as MapDistanceVillageControl;
                    var mdv2 = DistanceContainer.Controls[i + 1] as MapDistanceVillageControl;
                    if (mdv1 != null)
                    {
                        mdv1.SetVillage(mdv2.Village, mdv2.UnitSelectedIndex);
                    }

                    if (i == DistanceContainer.RowCount - 3)
                    {
                        mdv2.Dispose();
                        DistanceContainer.Controls.Remove(mdv2);

                        if (DistanceContainer.RowCount > 1)
                        {
                            DistanceContainer.RowCount--;
                        }
                        DistanceContainer.RowStyles[DistanceContainer.RowCount - 1] = new RowStyle(SizeType.AutoSize);
                    }
                }
            }
        }
        #endregion

        public string GetPlan(bool bbCodes)
        {
            return GetPlan(bbCodes, true);
        }

        public string GetPlan(bool bbCodes, bool standAlone)
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
                for (int i = 0; i < DistanceContainer.RowCount - 1; i++)
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
            for (int i = 0; i < DistanceContainer.RowCount - 1; i++)
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
            for (int i = 0; i < DistanceContainer.RowCount - 1; i++)
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
            // Bug here...
            _parent.Remove(this);
            for (int i = DistanceContainer.RowCount - 2; i >= 0 ; i--)
            {
                var mdv = DistanceContainer.Controls[i] as MapDistanceVillageControl;
                mdv.Dispose();
                DistanceContainer.Controls.Remove(mdv);
            }
            Dispose();
        }

        public void Clear()
        {
            for (int i = DistanceContainer.RowCount - 2; i >= 0; i--)
            {
                var mdv = DistanceContainer.Controls[i] as MapDistanceVillageControl;
                mdv.Dispose();
                DistanceContainer.Controls.Remove(mdv);
                DistanceContainer.RowStyles.Remove(DistanceContainer.RowStyles[i]);
            }
        }
    }
}
