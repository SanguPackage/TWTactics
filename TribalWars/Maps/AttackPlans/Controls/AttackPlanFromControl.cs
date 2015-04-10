using System;
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Maps.AttackPlans.EventArg;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Villages.ContextMenu;
using TribalWars.Villages.Units;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;

namespace TribalWars.Maps.AttackPlans.Controls
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
            UnitBox.SelectedIndex = attacker.SlowestUnit.Position;

            _changingAttacker = false;
            UpdateDisplay();
        }

        /// <summary>
        /// Update the text displays
        /// </summary>
        public void UpdateDisplay()
        {
            _Village.Text = Attacker.Attacker.Name;

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

        private void AttackPlanFromControl_DoubleClick(object sender, EventArgs e)
        {
            if (Attacker.Attacker != null)
            {
                World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Attack);
                World.Default.Map.SetCenter(Attacker.Attacker);
                World.Default.Map.EventPublisher.SelectVillages(this, Attacker.Attacker, VillageTools.SelectVillage);
                World.Default.Map.EventPublisher.AttackSelect(null, Attacker);
                World.Default.Map.GiveFocus();
            }
        }

        private void AttackPlanFromControl_MouseClick(object sender, MouseEventArgs e)
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
                    World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Attack);
                    World.Default.Map.EventPublisher.AttackSelect(null, Attacker);
                    World.Default.Map.GiveFocus();
                }
            }
        }

        private void Coords_TextChanged(object sender, EventArgs e)
        {
            if (!_changingAttacker && Coords.Village != null)
            {
                Attacker.Attacker = Coords.Village;
                UpdateDisplay();
                World.Default.Map.EventPublisher.AttackUpdateTarget(this, AttackUpdateEventArgs.UpdateAttackFrom(Attacker));
            }
        }
        #endregion

        #region TextOutput
        public override string ToString()
        {
            return Attacker.ToString();
        }
        #endregion        
    }
}
