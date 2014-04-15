using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using TribalWars.Data.Villages;

namespace TribalWars.Controls
{
    public partial class MapDistanceControl : UserControl
    {
        #region Fields
        private Village _Target;
        private MapDistanceCollectionControl _Parent;
        private ImageList UnitImageList;
        #endregion

        #region Properties
        public Village Target
        {
            get { return _Target; }
            set
            {
                _Target = value;
                if (value != null)
                {
                    Coords.Text = value.LocationString;
                    _Village.Text = value.Name;
                    if (value.HasPlayer)
                    {
                        _Player.Text = value.Player.ToString();
                    }
                    else
                    {
                        _Player.Text = "";
                    }
                    if (value.HasTribe)
                    {
                        _Tribe.Text = value.Player.Tribe.ToString();
                    }
                    else
                    {
                        _Tribe.Text = "";
                    }
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
            get { return _Parent; }
        }
        #endregion

        #region Constructors
        public MapDistanceControl(MapDistanceCollectionControl parent, ImageList imageList)
        {
            InitializeComponent();

            _Parent = parent;
            UnitImageList = imageList;
            Date.Value = World.Default.ServerTime.AddHours(8);
        }
        #endregion

        #region EventHandlers
        private void Date_ValueChanged(object sender, EventArgs e)
        {
            CalculateVariable();
        }

        private void Player_DoubleClick(object sender, EventArgs e)
        {
            if (_Target.HasPlayer)
            {
                World.Default.Map.EventPublisher.SelectVillages(this, _Target.Player, VillageTools.PinPoint);
            }
        }

        private void Tribe_DoubleClick(object sender, EventArgs e)
        {
            if (_Target.HasTribe)
            {
                World.Default.Map.EventPublisher.SelectVillages(this, _Target.Player.Tribe, VillageTools.PinPoint);
            }
        }
        #endregion

        #region Calculate Times
        public void Calculate()
        {
            foreach (Control ctl in DistanceContainer.Controls)
            {
                MapDistanceVillageControl distanceControl = ctl as MapDistanceVillageControl;
                if (distanceControl != null) distanceControl.Calculate();
            }
        }

        public void CalculateVariable()
        {
            foreach (Control ctl in DistanceContainer.Controls)
            {
                MapDistanceVillageControl distanceControl = ctl as MapDistanceVillageControl;
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

            MapDistanceVillageControl ctl = new MapDistanceVillageControl(UnitImageList, village, this, DistanceContainer.RowCount - 1);
            DistanceContainer.Controls.Add(ctl, 0, DistanceContainer.RowCount - 2);

            //World.Default.EventPublisher.DoPaint(false);

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
                    MapDistanceVillageControl mdv1 = DistanceContainer.Controls[i] as MapDistanceVillageControl;
                    MapDistanceVillageControl mdv2 = DistanceContainer.Controls[i + 1] as MapDistanceVillageControl;
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

        public string GetPlan(bool BBCodes)
        {
            return GetPlan(BBCodes, true);
        }

        public string GetPlan(bool BBCodes, bool standAlone)
        {
            Sort();

            if (_Target != null)
            {
                StringBuilder str = new StringBuilder();
                if (standAlone)
                {
                    str.AppendLine("*** Attack Plan ***");
                    if (!BBCodes)
                    {
                        str.AppendLine(_Target.ToString());
                    }
                    else
                    {
                        str.AppendLine(_Target.BBCode());
                    }
                    str.AppendLine();

                    str.AppendLine("Arrival time: " + Date.Value.ToString(Date.CustomFormat));
                    str.AppendLine("Current time: " + World.Default.ServerTime.ToString(Date.CustomFormat));
                    str.AppendLine();
                }
                for (int i = 0; i < DistanceContainer.RowCount - 1; i++)
                {
                    MapDistanceVillageControl mdv = DistanceContainer.Controls[i] as MapDistanceVillageControl;
                    if (mdv != null)
                    {
                        str.Append(mdv.ToString(BBCodes, standAlone ? null : _Target));
                    }
                }
                return str.ToString().Trim();
            }
            return string.Empty;
        }

        public List<MapDistanceVillageComparor> GetVillageList()
        {
            List<MapDistanceVillageComparor> list = new List<MapDistanceVillageComparor>();
            for (int i = 0; i < DistanceContainer.RowCount - 1; i++)
            {
                MapDistanceVillageControl mdv = DistanceContainer.Controls[i] as MapDistanceVillageControl;
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
                MapDistanceVillageControl mdv = DistanceContainer.Controls[i] as MapDistanceVillageControl;
                if (mdv != null)
                {
                    mdv.SetVillage(list[i].Village, list[i].UnitSelectedIndex);
                }
            }
        }

        private void Close_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _Parent.Remove(this);
            for (int i = DistanceContainer.RowCount - 2; i >= 0 ; i--)
            {
                MapDistanceVillageControl mdv = DistanceContainer.Controls[i] as MapDistanceVillageControl;
                mdv.Dispose();
                DistanceContainer.Controls.Remove(mdv);
            }
            this.Dispose();
        }

        public void Clear()
        {
            for (int i = DistanceContainer.RowCount - 2; i >= 0; i--)
            {
                MapDistanceVillageControl mdv = DistanceContainer.Controls[i] as MapDistanceVillageControl;
                mdv.Dispose();
                DistanceContainer.Controls.Remove(mdv);
                DistanceContainer.RowStyles.Remove(DistanceContainer.RowStyles[i]);
            }
        }
    }
}
