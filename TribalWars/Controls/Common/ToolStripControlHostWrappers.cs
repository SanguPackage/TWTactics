using System;
using System.Drawing;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using TribalWars.Data;
using TribalWars.Data.Events;
using System.ComponentModel;
using TribalWars.Villages;
using TribalWars.Villages.Units;

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
        public VillagePlayerTribeFinderTextBox PlayerTribeFinderTextBox
        {
            get { return Control as VillagePlayerTribeFinderTextBox; }
        }

        /// <summary>
        /// Gets or sets the selected Village
        /// </summary>
        [Browsable(false)]
        public Village Village
        {
            get { return PlayerTribeFinderTextBox.Village; }
            set { PlayerTribeFinderTextBox.SetVillage(value, false); }
        }

        /// <summary>
        /// Gets or sets the selected Player
        /// </summary>
        [Browsable(false)]
        public Player Player
        {
            get { return PlayerTribeFinderTextBox.Player; }
            set { PlayerTribeFinderTextBox.SetPlayer(value, false); }
        }

        /// <summary>
        /// Gets or sets the selected Tribe
        /// </summary>
        [Browsable(false)]
        public Tribe Tribe
        {
            get { return PlayerTribeFinderTextBox.Tribe; }
            set { PlayerTribeFinderTextBox.SetTribe(value, false); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allows entering tribe tags
        /// </summary>
        [DefaultValue(false)]
        public bool AllowTribe
        {
            get { return PlayerTribeFinderTextBox.AllowTribe; }
            set { PlayerTribeFinderTextBox.AllowTribe = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allows entering players
        /// </summary>
        [DefaultValue(false)]
        public bool AllowPlayer
        {
            get { return PlayerTribeFinderTextBox.AllowPlayer; }
            set { PlayerTribeFinderTextBox.AllowPlayer = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allow entering village coordinates
        /// </summary>
        [DefaultValue(true)]
        public bool AllowVillage
        {
            get { return PlayerTribeFinderTextBox.AllowVillage; }
            set { PlayerTribeFinderTextBox.AllowVillage = value; }
        }
        #endregion

        #region Constructors
        public ToolStripVillageTextBox()
            : base(new VillagePlayerTribeFinderTextBox())
        {
            AutoSize = false;
            Text = string.Empty;
            ToolTipText = string.Empty;

            PlayerTribeFinderTextBox.Width = 50;
            PlayerTribeFinderTextBox.Text = string.Empty;
        }
        #endregion

        #region Overriden Methods
        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);

            // Add the event.
            PlayerTribeFinderTextBox.VillageSelected += control_VillageSelected;
            PlayerTribeFinderTextBox.TribeSelected += TextBox_TribeSelected;
            PlayerTribeFinderTextBox.PlayerSelected += TextBox_PlayerSelected;
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
            PlayerTribeFinderTextBox.VillageSelected -= control_VillageSelected;
            PlayerTribeFinderTextBox.TribeSelected -= TextBox_TribeSelected;
            PlayerTribeFinderTextBox.PlayerSelected -= TextBox_PlayerSelected;
        }
        #endregion
    }

    
    /// <summary>
    /// Wrapper for a LocationChangerControl for use in a ToolStrip
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
            LocationChanger.Width = 235;

            LocationChanger.PlayerTribeFinderTextBox.Text = DefaultPlaceHolderText;
            LocationChanger.PlayerTribeFinderTextBox.LostFocus += PlayerTribeFinderTextBoxOnLostFocus;
            LocationChanger.PlayerTribeFinderTextBox.GotFocus += PlayerTribeFinderTextBoxOnGotFocus;
        }

        private void PlayerTribeFinderTextBoxOnGotFocus(object sender, EventArgs e)
        {
            if (LocationChanger.PlayerTribeFinderTextBox.Text == DefaultPlaceHolderText)
            {
                LocationChanger.PlayerTribeFinderTextBox.Text = "";
            }
        }

        private void PlayerTribeFinderTextBoxOnLostFocus(object sender, EventArgs e)
        {
            if (LocationChanger.PlayerTribeFinderTextBox.Text == "")
            {
                LocationChanger.PlayerTribeFinderTextBox.Text = DefaultPlaceHolderText;
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
