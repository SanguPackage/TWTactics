#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Text.RegularExpressions;
using TribalWars.Data.Villages;
using TribalWars.Data.Events;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
using System.Globalization;
using Janus.Windows.GridEX.EditControls;
using TribalWars.Data.Maps;
#endregion

namespace TribalWars.Controls
{
    /// <summary>
    /// Extended TextBox that accepts Village coordinates, player names and tribe tags
    /// </summary>
    public class VillagePlayerTribeFinderTextBox : MaskedEditBox
    {
        #region Constants
        private const string PropertyGridCategory = "Tribal Wars";
        #endregion

        #region Events
        public event EventHandler<VillageEventArgs> VillageSelected;
        public event EventHandler<PlayerEventArgs> PlayerSelected;
        public event EventHandler<TribeEventArgs> TribeSelected;
        #endregion

        #region Fields
        private Map _map;
        private readonly ToolTip _tooltip = new ToolTip();

        private bool _allowVillage = true;
        private bool _allowPlayer;
        private bool _allowTribe;

        private Village _village;
        private Player _player;
        private Tribe _tribe;

        private bool _showButton;

        private bool _handleTextChanged = true;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the game location if coordinates are filled in
        /// </summary>
        public Point? GameLocation
        {
            get
            {
                return World.Default.GetCoordinates(Text);
            }
            set
            {
                if (value.HasValue)
                    Text = string.Format("{0}|{1}", value.Value.X, value.Value.Y);
                else
                    Text = string.Empty;
            }
        }

        /// <summary>
        /// Gets the selected Village
        /// </summary>
        [Browsable(false)]
        public Village Village
        {
            get
            {
                return _village;
            }
        }

        /// <summary>
        /// Gets or sets the active player
        /// </summary>
        [Browsable(false)]
        public Player Player
        {
            get
            {
                return _player;
            }
        }

        /// <summary>
        /// Gets or sets the active tribe
        /// </summary>
        [Browsable(false)]    
        public Tribe Tribe
        {
            get
            {
                return _tribe;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allows entering tribe tags
        /// </summary>
        [Category(PropertyGridCategory), DefaultValue(false)]
        public bool AllowTribe
        {
            get { return _allowTribe; }
            set { _allowTribe = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allows entering players
        /// </summary>
        [Category(PropertyGridCategory), DefaultValue(false)]
        public bool AllowPlayer
        {
            get { return _allowPlayer; }
            set { _allowPlayer = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allow entering village coordinates
        /// </summary>
        [Category(PropertyGridCategory), DefaultValue(true)]
        public bool AllowVillage
        {
            get { return _allowVillage; }
            set { _allowVillage = value; }
        }

        /// <summary>
        /// Shows or hides the location changer button
        /// </summary>
        [Category(PropertyGridCategory), DefaultValue(false)]
        public bool ShowButton
        {
            get { return _showButton; }
            set
            {
                if (value)
                {
                    base.ButtonStyle = EditButtonStyle.TextButton;
                    base.ButtonText = "» OK «";
                }
                else
                {
                    base.ButtonStyle = EditButtonStyle.NoButton;
                }
                _showButton = value;
            }
        }
        #endregion

        #region Constructors
        public VillagePlayerTribeFinderTextBox()
        {
            Width = 50;
            Text = string.Empty;
            _tooltip.Active = true;
            _tooltip.IsBalloon = true;
        }

        public void Initialize(Map map)
        {
            _map = map;
        }
        #endregion

        #region Event Handlers
        protected override void OnTextChanged(EventArgs e)
        {
            if (_handleTextChanged)
            {
                bool found = false;
                if (AllowVillage)
                {
                    Village vil = World.Default.GetVillage(Text);
                    if (vil != null)
                    {
                        found = true;
                        SetVillage(vil, true);
                    }
                }
                if (AllowPlayer && !found)
                {
                    Player ply = World.Default.GetPlayer(Text);
                    if (ply != null)
                    {
                        found = true;
                        SetPlayer(ply, true);
                    }
                }
                if (AllowTribe && !found)
                {
                    Tribe tribe = World.Default.GetTribe(Text);
                    if (tribe != null)
                    {
                        found = true;
                        SetTribe(tribe, true);
                    }
                }

                if (!found && BackColor != Color.Red)
                {
                    BackColor = Color.Red;
                    _tooltip.ToolTipTitle = string.Empty;
                    _tooltip.SetToolTip(this, GetEmptyTooltip());
                }
            }
            base.OnTextChanged(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            this.SelectAll();
            base.OnEnter(e);
        }

        protected override void OnButtonClick(EditButton editButton)
        {
            Point? point = World.Default.GetCoordinates(Text);
            if (point.HasValue)
            {
                if (_showButton && _map != null)
                {
                    _map.SetCenter(point.Value);
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Clears the input box
        /// </summary>
        public void EmptyTextBox()
        {
            _village = null;
            _player = null;
            _tribe = null;
            Text = string.Empty;
            BackColor = Color.White;
            _tooltip.ToolTipTitle = string.Empty;
            _tooltip.SetToolTip(this, GetEmptyTooltip());
        }

        /// <summary>
        /// Sets a village in the box
        /// </summary>
        public void SetVillage(Village village)
        {
            SetVillage(village, false);
        }

        /// <summary>
        /// Sets a village in the box
        /// </summary>
        public void SetVillage(Village village, bool raiseEvent)
        {
            _village = village;
            if (village != null)
            {
                if (Text != village.LocationString)
                {
                    _handleTextChanged = false;
                    Text = village.LocationString;
                    _handleTextChanged = true;
                }

                BackColor = Color.Green;
                _tooltip.ToolTipTitle = village.ToString();
                _tooltip.SetToolTip(this, village.Tooltip);

                if (raiseEvent && VillageSelected != null)
                    VillageSelected(this, new VillageEventArgs(village, VillageTools.SelectVillage));
                else if (_showButton && _map != null)
                {
                    _map.SetCenter(village);
                    _map.EventPublisher.SelectVillages(this, village, new VillageCommand(VillageTools.SelectVillage));
                }
            }
            else
            {
                EmptyTextBox();
            }
        }

        /// <summary>
        /// Sets a player in the box
        /// </summary>
        public void SetPlayer(Player player)
        {
            SetPlayer(player, false);
        }

        /// <summary>
        /// Sets a player in the box
        /// </summary>
        public void SetPlayer(Player player, bool raiseEvent)
        {
            _player = player;
            if (player != null)
            {
                if (Text != player.Name)
                {
                    _handleTextChanged = false;
                    Text = player.Name;
                    _handleTextChanged = true;
                }

                SelectionStart = Text.Length;
                BackColor = Color.Green;
                _tooltip.ToolTipTitle = player.Name;
                _tooltip.SetToolTip(this, player.Tooltip);
                if (raiseEvent && PlayerSelected != null)
                    PlayerSelected(this, new PlayerEventArgs(player, VillageTools.PinPoint));
                else if (_showButton && _map != null)
                {
                    _map.SetCenter(Data.Maps.Display.GetSpan(player));
                    _map.EventPublisher.SelectVillages(this, player, VillageTools.PinPoint);
                }
            }
            else
            {
                EmptyTextBox();
            }
        }

        /// <summary>
        /// Sets a tribe in the box
        /// </summary>
        public void SetTribe(Tribe tribe)
        {
            SetTribe(tribe, false);
        }

        /// <summary>
        /// Sets a tribe in the box
        /// </summary>
        public void SetTribe(Tribe tribe, bool raiseEvent)
        {
            _tribe = tribe;
            if (tribe != null)
            {
                if (Text != tribe.Tag)
                {
                    _handleTextChanged = false;
                    Text = tribe.Tag;
                    _handleTextChanged = true;
                }

                SelectionStart = Text.Length;
                BackColor = Color.Green;
                _tooltip.ToolTipTitle = tribe.Tag;
                _tooltip.SetToolTip(this, tribe.Tooltip);
                if (raiseEvent && TribeSelected != null)
                    TribeSelected(this, new TribeEventArgs(tribe, VillageTools.SelectVillage));
                else if (_showButton && _map != null)
                {
                    _map.SetCenter(Data.Maps.Display.GetSpan(tribe));
                    _map.EventPublisher.SelectVillages(this, tribe, VillageTools.SelectVillage);
                }
            }
            else
            {
                EmptyTextBox();
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Gets the tooltip with info what the user can enter in the textbox
        /// </summary>
        private string GetEmptyTooltip()
        {
            string str = "";
            if (AllowVillage) str += ", world coordinates";
            if (AllowPlayer) str += ", player name";
            if (AllowTribe) str += ", tribe tag";
            if (str != string.Empty)
                return "Enter" + str.Substring(1);

            return string.Empty;
        }
        #endregion
    }
}

