using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using TribalWars.Villages;
using TribalWars.Villages.ContextMenu;
using TribalWars.Villages.Units;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    public partial class MapDistanceVillageControl : UserControl
    {
        #region Fields
        private Village _village;
        public readonly MapDistanceControl TargetControl;

        private Unit _unit;
        private TimeSpan _travelTime;
        public double TimeLeftBeforeSendTotalSeconds;
        #endregion

        #region Properties
        public Village Village
        {
            get { return _village; }
            private set
            {
                _village = value;
                _Village.Text = _village.Name;
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
        public MapDistanceVillageControl(ImageList list, Village village, MapDistanceControl parent)
        {
            InitializeComponent();

            UnitBox.ImageList = list;
            Village = village;
            TargetControl = parent;

            UnitBox.SelectedIndex = WorldUnits.Default[UnitTypes.Ram].Position;
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
            _unit = WorldUnits.Default[i];
            if (_unit != null)
            {
                _travelTime = Village.TravelTime(TargetControl.Target, Village, _unit);
                DateRequired.Text = _travelTime.ToString();
                CalculateVariable();
            }
        }

        private void CalculateVariable()
        {
            if (_unit != null)
            {
                DateTime serverTime = World.Default.Settings.ServerTime;
                DateSend.Text = Tools.Common.GetPrettyDate(TargetControl.AttackDate - _travelTime);

                DateNow.Text = Tools.Common.GetPrettyDate(serverTime + _travelTime);
                TimeSpan timeLeftBeforeSend = TargetControl.AttackDate - serverTime - _travelTime;
                TimeLeftBeforeSendTotalSeconds = timeLeftBeforeSend.TotalSeconds;
                if (timeLeftBeforeSend.TotalSeconds >= 0)
                {
                    DateLeft.ForeColor = Color.Black;
                    DateLeft.Text = "";
                }
                else
                {
                    DateLeft.ForeColor = Color.Red;
                    DateLeft.Text = "-";
                    timeLeftBeforeSend = timeLeftBeforeSend.Duration();
                }
                if (timeLeftBeforeSend.Days != 0) DateLeft.Text += string.Format("{0}.", timeLeftBeforeSend.Days);
                DateLeft.Text += string.Format("{0}:{1}:{2}", timeLeftBeforeSend.Hours.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'), timeLeftBeforeSend.Minutes.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'), timeLeftBeforeSend.Seconds.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
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

        private void _Village_MouseClick(object sender, MouseEventArgs e)
        {
            if (Village != null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    var cm = new VillageContextMenu(World.Default.Map, Village);
                    cm.Show(_Village, e.Location);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    World.Default.Map.EventPublisher.SelectVillages(null, Village, VillageTools.PinPoint);
                }
            }
        }

        private void Coords_TextChanged(object sender, EventArgs e)
        {
            Village vil = Coords.Village;
            if (vil != null)
            {
                Village = vil;
            }
        }
        #endregion

        #region TextOutput
        public string ToString(bool bbCodes, Village target)
        {
            Calculate();
            var str = new StringBuilder();
            if (Village != null)
            {
                if (target != null)
                {
                    if (!bbCodes) str.AppendLine("Attack " + target);
                    else str.AppendLine("Attack " + target.BbCode());
                }
                if (bbCodes)
                {
                    str.AppendLine(string.Format("{0} from {1}", _unit.BbCodeImage, Village.BbCode()));
                    str.AppendLine("Send on: [b]" + DateSend.Text + "[/b]");
                }
                else
                {
                    str.AppendLine(string.Format("{0} from {1}", _unit.Name, Village));
                    str.AppendLine("Send on: " + DateSend.Text);
                }
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
