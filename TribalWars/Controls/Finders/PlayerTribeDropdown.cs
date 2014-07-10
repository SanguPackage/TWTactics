using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using TribalWars.Maps;
using TribalWars.Villages;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

namespace TribalWars.Controls.Finders
{
    public partial class PlayerTribeDropdown : UserControl
    {
        #region Constants
        private const string PropertyGridCategory = "Tribal Wars";

        private readonly static Color BadInput = Color.Red;
        private readonly static Color GoodInput = Color.Green;
        private readonly static Color NeutralInput = Color.White;
        #endregion

        #region Events
        public event EventHandler<VillageEventArgs> VillageSelected;
        public event EventHandler<PlayerEventArgs> PlayerSelected;
        public event EventHandler<TribeEventArgs> TribeSelected;
        #endregion

        #region Fields
        private Map _map;
        private readonly ToolTip _tooltip;
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
                return World.Default.GetCoordinates(SelectorControl.Text);
            }
            set
            {
                if (value.HasValue)
                {
                    SelectorControl.Text = string.Format("{0}|{1}", value.Value.X, value.Value.Y);
                }
                else
                {
                    SelectorControl.Text = string.Empty;
                }
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
        #endregion

        #region Constructors
        public PlayerTribeDropdown()
        {
            InitializeComponent();
            AllowVillage = true;

            _tooltip = new ToolTip
            {
                Active = true,
                IsBalloon = true
            };
        }

        public void Initialize(Map map)
        {
            _map = map;
        }
        #endregion

        #region Event Handlers
        

        private void VillagePlayerTribeSelector_Load(object sender, EventArgs e)
        {
            SelectorControl.DropDownList.FormattingRow += DropDownList_FormattingRow;
        }

        private void DropDownList_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.Record)
            {
                var data = (VillagePlayerTribeRow) e.Row.DataRow;

                e.Row.Cells["Visible"].Image = data.VisibleImage;
                e.Row.Cells["Image"].ImageIndex = data.ImageIndex;

                e.Row.Cells["Value"].ToolTipText = ""; // TODO: add tooltip
            }
        }

        private void SelectorControl_TextChanged(object sender, EventArgs e)
        {
            if (_handleTextChanged)
            {
                if (SelectorControl.Text.Length > 0)
                {
                    SelectorControl.DroppedDown = true;
                }

                bool found = false;
                if (AllowVillage)
                {
                    Village vil = World.Default.GetVillage(SelectorControl.Text);
                    if (vil != null)
                    {
                        found = true;
                        SetVillage(vil, true);
                    }
                }
                if (AllowPlayer && !found)
                {
                    Player ply = World.Default.GetPlayer(SelectorControl.Text);
                    if (ply != null)
                    {
                        found = true;
                        SetPlayer(ply, true);
                    }
                }
                if (AllowTribe && !found)
                {
                    Tribe tribe = World.Default.GetTribe(SelectorControl.Text);
                    if (tribe != null)
                    {
                        found = true;
                        SetTribe(tribe, true);
                    }
                }

                if (!found && SelectorControl.BackColor != BadInput)
                {
                    SelectorControl.BackColor = BadInput;
                    _tooltip.ToolTipTitle = string.Empty;
                    _tooltip.SetToolTip(this, GetEmptyTooltip());

                    Tribe = null;
                    Village = null;
                    Player = null;
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Clears the input box
        /// </summary>
        public void EmptyTextBox(bool raiseEvent)
        {
            Village = null;
            Player = null;
            Tribe = null;
            SelectorControl.BackColor = NeutralInput;

            _tooltip.ToolTipTitle = string.Empty;
            _tooltip.SetToolTip(this, GetEmptyTooltip());

            if (string.IsNullOrEmpty(SelectorControl.Text))
                return;

            bool oldHandleText = _handleTextChanged;
            _handleTextChanged = raiseEvent;

            SelectorControl.Text = string.Empty;

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
            Player = null;
            Tribe = null;

            if (village != null)
            {
                if (SelectorControl.Text != village.LocationString)
                {
                    _handleTextChanged = false;
                    SelectorControl.Text = village.LocationString;
                    _handleTextChanged = true;
                }

                SelectorControl.BackColor = GoodInput;
                _tooltip.ToolTipTitle = village.Tooltip.Title;
                _tooltip.SetToolTip(this, village.Tooltip.Text);

                if (raiseEvent && VillageSelected != null)
                {
                    VillageSelected(this, new VillageEventArgs(village, VillageTools.PinPoint));
                }
                // TODO: button code
                /*else if (_showButton && _map != null)
                {
                    _map.SetCenter(village.Location);
                    _map.EventPublisher.SelectVillages(this, village, new VillageCommand(VillageTools.PinPoint));
                }*/
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
            Village = null;
            Player = player;
            Tribe = null;

            if (player != null)
            {
                if (SelectorControl.Text != player.Name)
                {
                    _handleTextChanged = false;
                    SelectorControl.Text = player.Name;
                    _handleTextChanged = true;
                }

                SelectorControl.SelectAll();
                SelectorControl.BackColor = GoodInput;
                _tooltip.ToolTipTitle = player.Name;
                _tooltip.SetToolTip(this, player.Tooltip);
                if (raiseEvent && PlayerSelected != null)
                {
                    PlayerSelected(this, new PlayerEventArgs(player, VillageTools.PinPoint));
                }
                // TODO: button code
                //else if (_showButton && _map != null)
                //{
                //    _map.SetCenter(player);
                //    _map.EventPublisher.SelectVillages(this, player, VillageTools.PinPoint);
                //}
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
            Village = null;
            Player = null;
            Tribe = tribe;

            if (tribe != null)
            {
                if (SelectorControl.Text != tribe.Tag)
                {
                    _handleTextChanged = false;
                    SelectorControl.Text = tribe.Tag;
                    _handleTextChanged = true;
                }

                SelectorControl.SelectAll();
                SelectorControl.BackColor = GoodInput;
                _tooltip.ToolTipTitle = tribe.Tag;
                _tooltip.SetToolTip(this, tribe.Tooltip);
                if (raiseEvent && TribeSelected != null)
                {
                    TribeSelected(this, new TribeEventArgs(tribe, VillageTools.PinPoint));
                }
                // TODO: button code
                //else if (_showButton && _map != null)
                //{
                //    _map.SetCenter(tribe);
                //    _map.EventPublisher.SelectVillages(this, tribe, VillageTools.PinPoint);
                //}
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

        private void SelectorControl_DropDown(object sender, EventArgs e)
        {
            SelectorControl.DataSource = World.Default.Cache.GetPlayersAndTribes(AllowPlayer, AllowTribe);
        }

        private void SelectorControl_DropDownHide(object sender, ComboDropDownHideEventArgs e)
        {

        }

        private void SelectorControl_ValueChanged(object sender, EventArgs e)
        {

        }

        

        

    }
}


