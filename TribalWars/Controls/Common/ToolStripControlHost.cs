using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using TribalWars.Data.Villages;
using TribalWars.Data.Units;
using TribalWars.Data.Events;
using TribalWars.Data.Tribes;
using TribalWars.Data.Players;
using System.ComponentModel;
using TribalWars.Controls.Common;

namespace TribalWars.Controls
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
        public VillageTextBox TextBox
        {
            get { return Control as VillageTextBox; }
        }

        /// <summary>
        /// Gets or sets the selected Village
        /// </summary>
        [Browsable(false)]
        public Village Village
        {
            get { return TextBox.Village; }
            set { TextBox.SetVillage(value, false); }
        }

        /// <summary>
        /// Gets or sets the selected Player
        /// </summary>
        [Browsable(false)]
        public Player Player
        {
            get { return TextBox.Player; }
            set { TextBox.SetPlayer(value, false); }
        }

        /// <summary>
        /// Gets or sets the selected Tribe
        /// </summary>
        [Browsable(false)]
        public Tribe Tribe
        {
            get { return TextBox.Tribe; }
            set { TextBox.SetTribe(value, false); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allows entering tribe tags
        /// </summary>
        [DefaultValue(false)]
        public bool AllowTribe
        {
            get { return TextBox.AllowTribe; }
            set { TextBox.AllowTribe = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allows entering players
        /// </summary>
        [DefaultValue(false)]
        public bool AllowPlayer
        {
            get { return TextBox.AllowPlayer; }
            set { TextBox.AllowPlayer = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allow entering village coordinates
        /// </summary>
        [DefaultValue(true)]
        public bool AllowVillage
        {
            get { return TextBox.AllowVillage; }
            set { TextBox.AllowVillage = value; }
        }
        #endregion

        #region Constructors
        public ToolStripVillageTextBox()
            : base(new VillageTextBox())
        {
            AutoSize = false;
            Text = string.Empty;
            ToolTipText = string.Empty;

            TextBox.Width = 50;
            TextBox.Text = string.Empty;
        }
        #endregion

        #region Overriden Methods
        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);

            // Add the event.
            TextBox.VillageSelected += new EventHandler<VillageEventArgs>(control_VillageSelected);
            TextBox.TribeSelected += new EventHandler<TribeEventArgs>(TextBox_TribeSelected);
            TextBox.PlayerSelected += new EventHandler<PlayerEventArgs>(TextBox_PlayerSelected);
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
            TextBox.VillageSelected -= new EventHandler<VillageEventArgs>(control_VillageSelected);
            TextBox.TribeSelected -= new EventHandler<TribeEventArgs>(TextBox_TribeSelected);
            TextBox.PlayerSelected -= new EventHandler<PlayerEventArgs>(TextBox_PlayerSelected);
        }
        #endregion
    }

    
    /// <summary>
    /// Wrapper for a LocationChangerControl for use in a ToolStrip
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripLocationChangerControl : ToolStripControlHost
    {
        #region Events
        #endregion

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
            Text = string.Empty;
            ToolTipText = string.Empty;

            LocationChanger.Width = 200;
        }
        #endregion

        #region Overriden Methods
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
