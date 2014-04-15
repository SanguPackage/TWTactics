using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using TribalWars.Data.Villages;
using TribalWars.Data.Units;

namespace TribalWars.Controls
{
    public partial class MapDistanceVillageControl : UserControl
    {
        #region Fields
        private Village village;
        public MapDistanceControl TargetControl;

        private Unit Unit;
        private TimeSpan TravelTime;
        public double TimeLeftBeforeSendTotalSeconds;
        public int Row;
        #endregion

        #region Properties
        public Village Village
        {
            get { return village; }
            set
            {
                village = value;
                _Village.Text = village.Name;
                Coords.Text = value.LocationString;
            }
        }

        public int UnitSelectedIndex
        {
            get { return UnitBox.SelectedIndex; }
            set { UnitBox.SelectedIndex = value; }
        }
        #endregion

        #region Constructors
        public MapDistanceVillageControl(ImageList list, Village village, MapDistanceControl parent, int row)
        {
            InitializeComponent();

            UnitBox.ImageList = list;
            Village = village;
            TargetControl = parent;

            UnitBox.SelectedIndex = WorldUnits.Default[UnitTypes.Ram].Position;

            Row = row;
        }
        #endregion

        #region Public Methods
        public void SetVillage(Village village, int unitPosition)
        {
            Village = village;
            UnitBox.SelectedIndex = unitPosition;
            Calculate();
        }

        public void Calculate()
        {
            int i = UnitBox.SelectedIndex;
            Unit = WorldUnits.Default[i];
            if (Unit != null)
            {
                TravelTime = Village.TravelTime(TargetControl.Target, Village, Unit);
                DateRequired.Text = TravelTime.ToString();
                CalculateVariable();
            }
        }

        public void CalculateVariable()
        {
            if (Unit != null)
            {
                DateTime serverTime = World.Default.ServerTime;
                DateSend.Text = Tools.Common.GetPrettyDate(TargetControl.AttackDate - TravelTime);

                DateNow.Text = Tools.Common.GetPrettyDate(serverTime + TravelTime);
                TimeSpan TimeLeftBeforeSend = TargetControl.AttackDate - serverTime - TravelTime;
                TimeLeftBeforeSendTotalSeconds = TimeLeftBeforeSend.TotalSeconds;
                if (TimeLeftBeforeSend.TotalSeconds >= 0)
                {
                    DateLeft.ForeColor = Color.Black;
                    DateLeft.Text = "";
                }
                else
                {
                    DateLeft.ForeColor = Color.Red;
                    DateLeft.Text = "-";
                    TimeLeftBeforeSend = TimeLeftBeforeSend.Duration();
                }
                if (TimeLeftBeforeSend.Days != 0) DateLeft.Text += string.Format("{0}.", TimeLeftBeforeSend.Days);
                DateLeft.Text += string.Format("{0}:{1}:{2}", TimeLeftBeforeSend.Hours.ToString().PadLeft(2, '0'), TimeLeftBeforeSend.Minutes.ToString().PadLeft(2, '0'), TimeLeftBeforeSend.Seconds.ToString().PadLeft(2, '0'));
            }
        }
        #endregion

        #region EventHandlers
        private void Unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void Close_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TargetControl.Remove(this);
        }

        private void Village_DoubleClick(object sender, EventArgs e)
        {
            if (Village != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(this, Village, VillageTools.PinPoint);
            }
        }

        private void Coords_TextChanged(object sender, EventArgs e)
        {
            //if (Coords.Valid)
            //{
                Village vil = Coords.Village;
                if (vil != Village)
                {
                    Village = vil;
                }
            //}
        }
        #endregion

        #region TextOutput
        public string ToString(bool BBCodes, Village target)
        {
            Calculate();
            StringBuilder str = new StringBuilder();
            if (Village != null)
            {
                DateTime serverTime = World.Default.ServerTime;

                if (target != null)
                {
                    if (!BBCodes) str.AppendLine("Attack " + target.ToString());
                    else str.AppendLine("Attack " + target.BBCode());
                }
                if (BBCodes)
                {
                    str.AppendLine(string.Format("{0} from {1}", Unit.BBCodeImage, Village.BBCode()));
                    str.AppendLine("Send on: [b]" + DateSend.Text + "[/b]");
                }
                else
                {
                    str.AppendLine(string.Format("{0} from {1}", Unit.Name, Village.ToString()));
                    str.AppendLine("Send on: " + DateSend.Text);
                }
                if (target == null) str.AppendLine("Estimated time left: " + DateLeft.Text);
                str.AppendLine();
            }

            return str.ToString();
        }

        public override string ToString()
        {
            return ToString(false, null);
        }
        #endregion
    }
}
