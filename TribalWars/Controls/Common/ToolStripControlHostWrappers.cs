using System;
using System.Drawing;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.ComponentModel;
using TribalWars.Controls.Finders;
using TribalWars.Controls.TimeConverter;
using TribalWars.Villages;
using TribalWars.Villages.Units;
using TribalWars.Worlds;
using TribalWars.Worlds.Events.Impls;

namespace TribalWars.Controls.Common
{
    /// <summary>
    /// Wrapper for a Unit ImageCombobox for use in a ToolStrip
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripUnitsImageCombobox : ToolStripControlHost
    {
        #region Properties
        /// <summary>
        /// Gets the underlying ImageCombobox
        /// </summary>
        public ImageCombobox Combobox
        {
            get { return Control as ImageCombobox; }
        }

        /// <summary>
        /// Gets the currently selected unit
        /// </summary>
        public Unit Unit
        {
            get
            {
                if (World.Default.HasLoaded)
                {
                    int i = Combobox.SelectedIndex;
                    return WorldUnits.Default[i];
                }
                return null;
            }
        }
        #endregion

        #region Constructors
        public ToolStripUnitsImageCombobox()
            : base(new ImageCombobox())
        {
            AutoSize = false;
            Text = string.Empty;
            ToolTipText = string.Empty;
        }
        #endregion
    }

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
        public VillagePlayerTribeSelectorOld PlayerTribeSelectorOld
        {
            get { return Control as VillagePlayerTribeSelectorOld; }
        }

        /// <summary>
        /// Gets or sets the selected Village
        /// </summary>
        [Browsable(false)]
        public Village Village
        {
            get { return PlayerTribeSelectorOld.Village; }
            set { PlayerTribeSelectorOld.SetVillage(value, false); }
        }

        /// <summary>
        /// Gets or sets the selected Player
        /// </summary>
        [Browsable(false)]
        public Player Player
        {
            get { return PlayerTribeSelectorOld.Player; }
            set { PlayerTribeSelectorOld.SetPlayer(value, false); }
        }

        /// <summary>
        /// Gets or sets the selected Tribe
        /// </summary>
        [Browsable(false)]
        public Tribe Tribe
        {
            get { return PlayerTribeSelectorOld.Tribe; }
            set { PlayerTribeSelectorOld.SetTribe(value, false); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allows entering tribe tags
        /// </summary>
        [DefaultValue(false)]
        public bool AllowTribe
        {
            get { return PlayerTribeSelectorOld.AllowTribe; }
            set { PlayerTribeSelectorOld.AllowTribe = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allows entering players
        /// </summary>
        [DefaultValue(false)]
        public bool AllowPlayer
        {
            get { return PlayerTribeSelectorOld.AllowPlayer; }
            set { PlayerTribeSelectorOld.AllowPlayer = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allow entering village coordinates
        /// </summary>
        [DefaultValue(true)]
        public bool AllowVillage
        {
            get { return PlayerTribeSelectorOld.AllowVillage; }
            set { PlayerTribeSelectorOld.AllowVillage = value; }
        }
        #endregion

        #region Constructors
        public ToolStripVillageTextBox()
            : base(new VillagePlayerTribeSelectorOld())
        {
            AutoSize = false;
            Text = string.Empty;
            ToolTipText = string.Empty;

            PlayerTribeSelectorOld.Width = 50;
            PlayerTribeSelectorOld.Text = string.Empty;
        }
        #endregion

        #region Overriden Methods
        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);

            // Add the event.
            PlayerTribeSelectorOld.VillageSelected += control_VillageSelected;
            PlayerTribeSelectorOld.TribeSelected += TextBox_TribeSelected;
            PlayerTribeSelectorOld.PlayerSelected += TextBox_PlayerSelected;
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
            PlayerTribeSelectorOld.VillageSelected -= control_VillageSelected;
            PlayerTribeSelectorOld.TribeSelected -= TextBox_TribeSelected;
            PlayerTribeSelectorOld.PlayerSelected -= TextBox_PlayerSelected;
        }
        #endregion
    }

    
    /// <summary>
    /// Wrapper for a LocationChangerControl for use in a ToolStrip.
    /// Also provides the placeholder text for the VillagePlayerTribeSelector.
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripLocationChangerControl : ToolStripControlHost
    {
        private const string DefaultPlaceHolderText = "Search village, player, tribe...";

        #region Properties
        /// <summary>
        /// Gets the underlying LocationChangerControl
        /// </summary>
        public LocationChangerControl LocationChanger
        {
            get { return Control as LocationChangerControl; }
        }
        #endregion

        #region Constructors
        public ToolStripLocationChangerControl()
            : base(new LocationChangerControl())
        {
            AutoSize = false;
            ToolTipText = string.Empty;
            LocationChanger.Width = 400;

            LocationChanger.PlayerTribeSelectorOld.Text = DefaultPlaceHolderText;
            LocationChanger.PlayerTribeSelectorOld.LostFocus += PlayerTribeSelectorOldOnLostFocus;
            LocationChanger.PlayerTribeSelectorOld.GotFocus += PlayerTribeSelectorOldOnGotFocus;

            LocationChanger.VillagePlayerTribeSelector.Text = DefaultPlaceHolderText;
            LocationChanger.VillagePlayerTribeSelector.LostFocus += PlayerTribeSelectorOldOnLostFocus;
            LocationChanger.VillagePlayerTribeSelector.GotFocus += PlayerTribeSelectorOldOnGotFocus;
        }

        private void PlayerTribeSelectorOldOnGotFocus(object sender, EventArgs e)
        {
            // TODO: this still working with the old:
            if (LocationChanger.PlayerTribeSelectorOld.Text == DefaultPlaceHolderText)
            {
                LocationChanger.PlayerTribeSelectorOld.Text = "";
            }

            if (LocationChanger.VillagePlayerTribeSelector.Text == DefaultPlaceHolderText)
            {
                LocationChanger.VillagePlayerTribeSelector.Text = "";
            }
        }

        private void PlayerTribeSelectorOldOnLostFocus(object sender, EventArgs e)
        {
            if (LocationChanger.PlayerTribeSelectorOld.Text == "")
            {
                LocationChanger.PlayerTribeSelectorOld.Text = DefaultPlaceHolderText;
            }

            if (LocationChanger.VillagePlayerTribeSelector.Text == "")
            {
                LocationChanger.VillagePlayerTribeSelector.Text = DefaultPlaceHolderText;
            }
        }
        #endregion
    }


    /// <summary>
    /// Wrapper for a TimeConverterCalculatorControl for use in a ToolStrip
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripTimeConverterCalculator : ToolStripControlHost
    {
        #region Properties
        /// <summary>
        /// Gets the underlying TimeConverterCalculatorControl
        /// </summary>
        public TimeConverterCalculatorControl TimeConverterCalculator
        {
            get { return Control as TimeConverterCalculatorControl; }
        }
        #endregion

        #region Constructors
        public ToolStripTimeConverterCalculator()
            : base(new TimeConverterCalculatorControl())
        {
            AutoSize = false;
            Text = string.Empty;
            ToolTipText = string.Empty;
        }
        #endregion
    }
}
