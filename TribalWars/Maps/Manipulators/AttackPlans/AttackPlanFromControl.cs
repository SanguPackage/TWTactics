using System;
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Villages.ContextMenu;
using TribalWars.Villages.Units;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// Control for one attacker. 
    /// A <see cref="AttackPlanControl"/> has one of these per Attacker.
    /// </summary>
    public partial class AttackPlanFromControl : UserControl
    {
        #region Fields
        private bool _changingAttacker;
        #endregion

        #region Properties
        public AttackPlanFrom Attacker { get; private set; }
        #endregion

        #region Constructors
        public AttackPlanFromControl(ImageList list, AttackPlanFrom attacker)
        {
            InitializeComponent();

            UnitBox.ImageList = list;

            SetVillage(attacker);
        }
        #endregion

        #region Public Methods
        public void SetVillage(AttackPlanFrom attacker)
        {
            _changingAttacker = true;
            Attacker = attacker;

            Coords.SetVillage(attacker.Attacker);
            _Village.Text = Attacker.Attacker.Name;
            UnitBox.SelectedIndex = attacker.SlowestUnit.Position;

            _changingAttacker = false;
            UpdateDisplay();
        }

        /// <summary>
        /// Update the text displays
        /// </summary>
        public void UpdateDisplay()
        {
            DateRequired.Text = Attacker.TravelTime.ToString();
            DateSend.Text = Attacker.FormattedSendDate();
            DateNow.Text = Tools.Common.GetPrettyDate(World.Default.Settings.ServerTime + Attacker.TravelTime);

            TimeSpan timeLeftBeforeSend = Attacker.GetTimeLeftBeforeSendDate();
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
            DateLeft.Text += string.Format("{0}", timeLeftBeforeSend.ToString(@"hh\:mm\:ss"));
        }
        #endregion

        #region EventHandlers
        private void Unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_changingAttacker)
            {
                Attacker.SlowestUnit = WorldUnits.Default[UnitBox.SelectedIndex];
                UpdateDisplay();
            }
        }

        private void Close_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var attackEventArgs = AttackUpdateEventArgs.DeleteAttackFrom(Attacker);
            World.Default.Map.EventPublisher.AttackUpdateTarget(this, attackEventArgs);
        }

        private void Village_DoubleClick(object sender, EventArgs e)
        {
            if (Attacker.Attacker != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(this, Attacker.Attacker, VillageTools.PinPoint);
            }
        }

        private void _Village_MouseClick(object sender, MouseEventArgs e)
        {
            if (Attacker.Attacker != null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    var cm = new VillageContextMenu(World.Default.Map, Attacker.Attacker);
                    cm.Show(_Village, e.Location);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    World.Default.Map.EventPublisher.SelectVillages(null, Attacker.Attacker, VillageTools.PinPoint);
                }
            }
        }

        private void Coords_TextChanged(object sender, EventArgs e)
        {
            if (!_changingAttacker && Coords.Village != null)
            {
                Attacker.Attacker = Coords.Village;
                World.Default.Map.EventPublisher.AttackUpdateTarget(this, AttackUpdateEventArgs.UpdateAttackFrom(Attacker));
            }
        }
        #endregion

        #region TextOutput
        public override string ToString()
        {
            return Attacker.GetExport(false, true);
        }
        #endregion
    }
}
