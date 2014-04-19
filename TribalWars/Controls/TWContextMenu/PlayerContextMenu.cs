#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data;
using TribalWars.Data.Players;

using TribalWars.Data.Maps;
#endregion

namespace TribalWars.Controls.TWContextMenu
{
    /// <summary>
    /// ContextMenu with general Player operations
    /// </summary>
    public class PlayerContextMenu : ContextMenuStrip
    {
        #region Fields
        private Player _player;

        private ToolStripSeparator _playerSeperator;
        private ToolStripMenuItem _tribeMenu;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the player 
        /// </summary>
        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }
        #endregion

        #region Constructors
        public PlayerContextMenu()
            : base()
        {
            Items.Add("BBCode", null, OnBBCode);
            //Items.Add("Pinpoint", null, OnPinpoint);
            //Items.Add("Pinpoint && Center", null, OnCenter);
            Items.Add("Details", null, OnDetails);
            //Items.Add("Mark", null, OnMark);
            Items.Add("Operation", null, OnBBCodeOperation);
            Items.Add(new ToolStripSeparator());
            Items.Add("TWStats", null, OnTWStats);

            // Show these menus when required
            _playerSeperator = new ToolStripSeparator();
            _tribeMenu = new ToolStripMenuItem("Tribe");
            Items.Add(_playerSeperator);
            Items.Add(_tribeMenu);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Shows the ContextMenuStrip
        /// </summary>
        public void Show(Control control, System.Drawing.Point position, Player target)
        {
            _player = target;
            if (_player.HasTribe)
            {
                _tribeMenu.Text = _player.Tribe.Tag;
                //World.Default.Map.Manipulators.CurrentManipulator.TribeContextMenu.Tribe = _player.Tribe;
                //if (_tribeMenu.DropDown.Items.Count == 0) _tribeMenu.DropDown = World.Default.Map.Manipulators.CurrentManipulator.TribeContextMenu;
            }
            _tribeMenu.Visible = _player.HasTribe;
            _playerSeperator.Visible = _player.HasTribe;

            Show(control, position);
        }

        /// <summary>
        /// Sets the player for the ContextMenu
        /// </summary>
        /// <param name="player">The player to show </param>
        /// <param name="showTribeMenu">Show or hide the TribeContextMenu</param>
        public void SetPlayer(Player player, bool showTribeMenu)
        {
            _player = player;
            showTribeMenu = showTribeMenu && player.HasTribe;
            if (showTribeMenu)
            {
                _tribeMenu.Text = player.Tribe.Tag;
                //_tribeMenu.DropDown = World.Default.Map.Manipulators.CurrentManipulator.TribeContextMenu;
                //World.Default.Map.Manipulators.CurrentManipulator.TribeContextMenu.Tribe = player.Tribe;
            }
            _playerSeperator.Visible = showTribeMenu;
            _tribeMenu.Visible = showTribeMenu;
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Puts the pinpoint on the target player
        /// </summary>
        private void OnPinpoint(object sender, EventArgs e)
        {
            if (_player != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(null, _player, VillageTools.PinPoint);
            }
        }

        /// <summary>
        /// Pinpoints and centers the target player
        /// </summary>
        private void OnCenter(object sender, EventArgs e)
        {
            if (_player != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(null, _player, VillageTools.PinPoint);
                World.Default.Map.SetCenter(Data.Maps.Display.GetSpan(_player));
            }
        }

        /// <summary>
        /// Start a new marker for the target player
        /// </summary>
        private void OnMark(object sender, EventArgs e)
        {
            if (_player != null)
            {
                //World.Default.EventPublisher.Publish(null, _player, VillageTools.PinPoint);
            }
        }

        /// <summary>
        /// Put target player BBCoded on clipboard
        /// </summary>
        private void OnBBCode(object sender, EventArgs e)
        {
            if (_player != null)
            {
                try
                {
                    Clipboard.SetText(_player.BBCode());
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// Put target player operation BBCoded on clipboard
        /// </summary>
        private void OnBBCodeOperation(object sender, EventArgs e)
        {
            if (_player != null)
            {
                try
                {
                    Clipboard.SetText(_player.BbCodeMatt()); 
                }
                catch (Exception)
                {

                }
                
            }
        }

        /// <summary>
        /// Browse to TWStats for the target player
        /// </summary>
        private void OnTWStats(object sender, EventArgs e)
        {
            if (_player != null)
            {
                World.Default.EventPublisher.BrowseUri(null, TribalWars.Controls.Main.Browser.DestinationEnum.TwStatsPlayer, _player.Id.ToString());
            }
        }

        /// <summary>
        /// Open quick details for the player
        /// </summary>
        private void OnDetails(object sender, EventArgs e)
        {
            if (_player != null)
            {
                World.Default.Map.EventPublisher.SelectPlayer(null, _player, VillageTools.SelectVillage);
            }
        }
        #endregion
    }
}
