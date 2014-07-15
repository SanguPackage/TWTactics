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
    /// <summary>
    /// Select player or tribe with dropdownlist
    /// </summary>
    public partial class PlayerTribeDropdown : UserControl
    {
        #region Constants
        private const string PropertyGridCategory = "Tribal Wars";

        private readonly static Color BadInput = Color.Red;
        private static readonly Color GoodInput = VillagePlayerTribeSelector.GoodInput;
        private readonly static Color NeutralInput = Color.White;
        #endregion

        #region Events
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
        [Category(PropertyGridCategory), DefaultValue(true)]
        public bool AllowTribe { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the textbox allows entering players
        /// </summary>
        [Category(PropertyGridCategory), DefaultValue(true)]
        public bool AllowPlayer { get; set; }

        public bool AutoOpenOnFocus { get; set; }
        #endregion

        #region Constructors
        public PlayerTribeDropdown()
        {
            InitializeComponent();
            AllowPlayer = true;
            AllowTribe = true;
            
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

                if (data.Visible)
                {
                    e.Row.Cells["Visible"].Image = Properties.Resources.Visible;
                }
                
                e.Row.Cells["Image"].ImageIndex = data.ImageIndex;
                e.Row.Cells["Value"].ToolTipText = data.Tooltip;
            }
        }

        private void SelectorControl_TextChanged(object sender, EventArgs e)
        {
            if (_handleTextChanged)
            {
                bool found = false;
                if (SelectorControl.Text.Length > 0 && string.IsNullOrEmpty(SelectorControl.SelectedText))
                {
                    if (AllowPlayer)
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
                }

                if (!found && SelectorControl.BackColor != BadInput)
                {
                    if (SelectorControl.Text.Length == 0)
                    {
                        SelectorControl.BackColor = NeutralInput;
                    }
                    else
                    {
                        SelectorControl.BackColor = BadInput;
                        _tooltip.ToolTipTitle = string.Empty;
                        _tooltip.SetToolTip(this, GetEmptyTooltip());
                    }

                    SelectorControl.Image = null;
                    Tribe = null;
                    Player = null;
                }
            }
        }

        private void SetDataSource()
        {
            if (SelectorControl.DataSource == null)
            {
                SelectorControl.DataSource = World.Default.Cache.GetPlayersAndTribes(AllowPlayer, AllowTribe);
            }
        }

        private void SelectorControl_ValueChanged(object sender, EventArgs e)
        {
            if (_handleTextChanged)
            {
                var selected = SelectorControl.SelectedItem as VillagePlayerTribeRow;
                if (selected != null)
                {
                    if (selected.IsPlayer)
                    {
                        Player ply = World.Default.GetPlayer(SelectorControl.Text);
                        if (ply != null)
                        {
                            SetPlayer(ply, true);
                        }
                    }
                    else if (selected.IsTribe)
                    {
                        var ply = World.Default.GetTribe(SelectorControl.Text);
                        if (ply != null)
                        {
                            SetTribe(ply, true);
                        }
                    }
                }
                else
                {
                    Player = null;
                    Tribe = null;
                }
            }
        }

        private void SelectorControl_Enter(object sender, EventArgs e)
        {
            if (AutoOpenOnFocus)
            {
                SelectorControl.DroppedDown = true;
            }
        }

        private void SelectorControl_DropDown(object sender, EventArgs e)
        {
            SetDataSource();
        }
        #endregion

        #region Public Methods
        public void SortOnText()
        {
            SelectorControl.DropDownList.SortKeys.RemoveAt(0);
            SelectorControl.DropDownList.SortKeys.Add("Rank");
        }

        /// <summary>
        /// Clears the input box
        /// </summary>
        public void EmptyTextBox(bool raiseEvent)
        {
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
        /// Sets a player in the box
        /// </summary>
        public void SetPlayer(Player player)
        {
            SetPlayer(player, false);
        }

        /// <summary>
        /// Sets a player in the box
        /// </summary>
        private void SetPlayer(Player player, bool raiseEvent)
        {
            Player = player;
            Tribe = null;

            if (player != null)
            {
                if (SelectorControl.Text != player.Name)
                {
                    _handleTextChanged = false;
                    SetDataSource();
                    SelectorControl.Text = player.Name;
                    _handleTextChanged = true;
                }

                SelectorControl.SelectionStart = Text.Length;
                SelectorControl.BackColor = GoodInput;
                _tooltip.ToolTipTitle = player.Name;
                _tooltip.SetToolTip(this, player.Tooltip);
                if (raiseEvent && PlayerSelected != null)
                {
                    PlayerSelected(this, new PlayerEventArgs(player, VillageTools.PinPoint));
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
        private void SetTribe(Tribe tribe, bool raiseEvent)
        {
            Player = null;
            Tribe = tribe;

            if (tribe != null)
            {
                if (SelectorControl.Text != tribe.Tag)
                {
                    _handleTextChanged = false;
                    SetDataSource();
                    SelectorControl.Text = tribe.Tag;
                    _handleTextChanged = true;
                }

                SelectorControl.SelectionStart = Text.Length;
                SelectorControl.BackColor = GoodInput;
                _tooltip.ToolTipTitle = tribe.Tag;
                _tooltip.SetToolTip(this, tribe.Tooltip);
                if (raiseEvent && TribeSelected != null)
                {
                    TribeSelected(this, new TribeEventArgs(tribe, VillageTools.PinPoint));
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
            if (AllowPlayer) str += ", player name";
            if (AllowTribe) str += ", tribe tag";
            if (str != string.Empty)
                return "Enter" + str.Substring(1);

            return string.Empty;
        }
        #endregion
    }
}