using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using TribalWars.Controls.Finders;
using TribalWars.Villages;
using TribalWars.Worlds.Events.Impls;

namespace TribalWars.Controls.Common.ToolStripControlHostWrappers
{
    /// <summary>
    /// Wrapper for a VillageTextBox for use in a ToolStrip
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripVillageTextBox : ToolStripControlHost
    {
        #region Events
        public event EventHandler<VillageEventArgs> VillageSelected;
        public event EventHandler<PlayerEventArgs> PlayerSelected;
        public event EventHandler<TribeEventArgs> TribeSelected;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the underlying VillageTextBox
        /// </summary>
        public VillagePlayerTribeSelector PlayerTribeSelector
        {
            get { return Control as VillagePlayerTribeSelector; }
        }

        /// <summary>
        /// Gets or sets the selected Village
        /// </summary>
        [Browsable(false)]
        public Village Village
        {
            get { return PlayerTribeSelector.Village; }
            set { PlayerTribeSelector.SetVillage(value, false); }
        }

        /// <summary>
        /// Gets or sets the selected Player
        /// </summary>
        [Browsable(false)]
        public Player Player
        {
            get { return PlayerTribeSelector.Player; }
            set { PlayerTribeSelector.SetPlayer(value, false); }
        }

        /// <summary>
        /// Gets or sets the selected Tribe
        /// </summary>
        [Browsable(false)]
        public Tribe Tribe
        {
            get { return PlayerTribeSelector.Tribe; }
            set { PlayerTribeSelector.SetTribe(value, false); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allows entering tribe tags
        /// </summary>
        [DefaultValue(false)]
        public bool AllowTribe
        {
            get { return PlayerTribeSelector.AllowTribe; }
            set { PlayerTribeSelector.AllowTribe = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allows entering players
        /// </summary>
        [DefaultValue(false)]
        public bool AllowPlayer
        {
            get { return PlayerTribeSelector.AllowPlayer; }
            set { PlayerTribeSelector.AllowPlayer = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allow entering village coordinates
        /// </summary>
        [DefaultValue(true)]
        public bool AllowVillage
        {
            get { return PlayerTribeSelector.AllowVillage; }
            set { PlayerTribeSelector.AllowVillage = value; }
        }

        [DefaultValue(true)]
        public bool ShowImage
        {
            get { return PlayerTribeSelector.ShowImage; }
            set { PlayerTribeSelector.ShowImage = value; }
        }

        /// <summary>
        /// Allow entering coordinates that do not map to a village
        /// </summary>
        [DefaultValue(false)]
        public bool AllowCoordinates
        {
            get { return PlayerTribeSelector.AllowCoordinates; }
            set { PlayerTribeSelector.AllowCoordinates = value; }
        }
        #endregion

        #region Constructors
        public ToolStripVillageTextBox()
            : base(new VillagePlayerTribeSelector())
        {
            AutoSize = false;
            //Text = string.Empty;
            ToolTipText = string.Empty;

            PlayerTribeSelector.Width = 55;
            PlayerTribeSelector.Text = string.Empty;
        }
        #endregion

        #region Overriden Methods
        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);

            // Add the event.
            PlayerTribeSelector.VillageSelected += control_VillageSelected;
            PlayerTribeSelector.TribeSelected += TextBox_TribeSelected;
            PlayerTribeSelector.PlayerSelected += TextBox_PlayerSelected;
        }

        private void TextBox_PlayerSelected(object sender, PlayerEventArgs e)
        {
            if (PlayerSelected != null) PlayerSelected(sender, e);
        }

        private void TextBox_TribeSelected(object sender, TribeEventArgs e)
        {
            if (TribeSelected != null) TribeSelected(sender, e);
        }

        private void control_VillageSelected(object sender, VillageEventArgs e)
        {
            if (VillageSelected != null) VillageSelected(sender, e);
        }

        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);

            // Remove the event.
            PlayerTribeSelector.VillageSelected -= control_VillageSelected;
            PlayerTribeSelector.TribeSelected -= TextBox_TribeSelected;
            PlayerTribeSelector.PlayerSelected -= TextBox_PlayerSelected;
        }
        #endregion
    }
}