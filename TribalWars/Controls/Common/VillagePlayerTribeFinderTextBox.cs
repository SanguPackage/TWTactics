#region Using
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Data;
using TribalWars.Data.Villages;
using TribalWars.Data.Events;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
using Janus.Windows.GridEX.EditControls;
using TribalWars.Data.Maps;
using TribalWars.Tools;

#endregion

namespace TribalWars.Controls.Common
{
    /// <summary>
    /// Extended TextBox that accepts Village coordinates, player names and tribe tags
    /// </summary>
    public class VillagePlayerTribeFinderTextBox : EditBox
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
        private readonly ToolTip _tooltip;

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
        public Village Village { get; private set; }

        /// <summary>
        /// Gets or sets the active player
        /// </summary>
        [Browsable(false)]
        public Player Player { get; private set; }

        /// <summary>
        /// Gets or sets the active tribe
        /// </summary>
        [Browsable(false)]
        public Tribe Tribe { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allows entering tribe tags
        /// </summary>
        [Category(PropertyGridCategory), DefaultValue(false)]
        public bool AllowTribe { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allows entering players
        /// </summary>
        [Category(PropertyGridCategory), DefaultValue(false)]
        public bool AllowPlayer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allow entering village coordinates
        /// </summary>
        [Category(PropertyGridCategory), DefaultValue(true)]
        public bool AllowVillage { get; set; }

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
                    ButtonStyle = EditButtonStyle.TextButton;
                    ButtonText = "» OK «";
                }
                else
                {
                    ButtonStyle = EditButtonStyle.NoButton;
                }
                _showButton = value;
            }
        }
        #endregion

        #region Constructors
        public VillagePlayerTribeFinderTextBox()
        {
            AllowVillage = true;
            Width = 50;
            _tooltip = WinForms.CreateTooltip();
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

                    Tribe = null;
                    Village = null;
                    Player = null;
                }
            }
            base.OnTextChanged(e);
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
            else
            {
                _tooltip.ToolTipTitle = string.Empty;
                _tooltip.SetToolTip(this, GetEmptyTooltip());
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Clears the input box
        /// </summary>
        public void EmptyTextBox(bool raiseEvent)
        {
            if (string.IsNullOrEmpty(Text))
                return;

            bool oldHandleText = _handleTextChanged;
            _handleTextChanged = raiseEvent;

            Village = null;
            Player = null;
            Tribe = null;
            Text = string.Empty;
            BackColor = Color.White;
            _tooltip.ToolTipTitle = string.Empty;
            _tooltip.SetToolTip(this, GetEmptyTooltip());

            _handleTextChanged = oldHandleText;
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
            Village = village;
            if (village != null)
            {
                if (Text != village.LocationString)
                {
                    _handleTextChanged = false;
                    Text = village.LocationString;
                    _handleTextChanged = true;
                }

                BackColor = Color.Green;
                _tooltip.ToolTipTitle = village.Tooltip.Title;
                _tooltip.SetToolTip(this, village.Tooltip.Text);

                if (raiseEvent && VillageSelected != null)
                {
                    VillageSelected(this, new VillageEventArgs(village, VillageTools.PinPoint));
                }
                else if (_showButton && _map != null)
                {
                    _map.SetCenter(village.Location);
                    _map.EventPublisher.SelectVillages(this, village, new VillageCommand(VillageTools.PinPoint));
                }
            }
            else
            {
                EmptyTextBox(raiseEvent);
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
            Player = player;
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
                {
                    PlayerSelected(this, new PlayerEventArgs(player, VillageTools.PinPoint));
                }
                else if (_showButton && _map != null)
                {
                    _map.SetCenter(player);
                    _map.EventPublisher.SelectVillages(this, player, VillageTools.PinPoint);
                }
            }
            else
            {
                EmptyTextBox(raiseEvent);
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
            Tribe = tribe;
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
                {
                    TribeSelected(this, new TribeEventArgs(tribe, VillageTools.PinPoint));
                }
                else if (_showButton && _map != null)
                {
                    _map.SetCenter(tribe);
                    _map.EventPublisher.SelectVillages(this, tribe, VillageTools.PinPoint);
                }
            }
            else
            {
                EmptyTextBox(raiseEvent);
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

