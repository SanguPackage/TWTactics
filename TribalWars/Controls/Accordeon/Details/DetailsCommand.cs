using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Villages;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;

namespace TribalWars.Controls.Accordeon.Details
{
    /// <summary>
    /// Represents an action in the DetailsControl
    /// </summary>
    public class DetailsCommand
    {
        #region Fields
        private DetailsDisplayEnum _display = DetailsDisplayEnum.None;
        private DetailsDisplayEnum _underlyingDisplay = DetailsDisplayEnum.None;

        private Village _village;
        private Player _player;
        private Tribe _tribe;

        private VillageTools _tool;

        private bool _subLayout;
        private string _tooltip;	
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tool for this command
        /// </summary>
        public VillageTools Tool
        {
            get { return _tool; }
        }

        /// <summary>
        /// Gets the tooltip for this command
        /// </summary>
        public string Tooltip
        {
            get { return _tooltip; }
        }

        /// <summary>
        /// Gets the currently active village
        /// </summary>
        public Village Village
        {
            get { return _village; }
        }

        /// <summary>
        /// Gets the currently active player
        /// </summary>
        public Player Player
        {
            get { return _player; }
        }

        /// <summary>
        /// Gets the currently active tribe
        /// </summary>
        public Tribe Tribe
        {
            get { return _tribe; }
        }

        /// <summary>
        /// Gets the current display
        /// </summary>
        public DetailsDisplayEnum Display
        {
            get { return _display; }
        }

        /// <summary>
        /// Gets a value indicating whether
        /// the command will change the entire
        /// control display or not
        /// </summary>
        public bool SubLayout
        {
            get { return _subLayout; }
        }

        /// <summary>
        /// Gets what was originally selected
        /// ignoring the current display
        /// </summary>
        /// <remarks>Used to set the VillageTextBox value</remarks>
        public DetailsDisplayEnum UnderlyingDisplay
        {
            get { return _underlyingDisplay; }
        }
        #endregion

        #region Constructors
        public DetailsCommand()
        {
            _tooltip = string.Empty;
        }

        public DetailsCommand(DetailsDisplayEnum display, Village village, VillageTools tool)
        {
            _tooltip = village.ToString();
            _display = display;
            if (_display == DetailsDisplayEnum.None) _display = DetailsDisplayEnum.Village;
            else if (_display == DetailsDisplayEnum.Player && !village.HasPlayer) _display = DetailsDisplayEnum.Village;
            else if (_display == DetailsDisplayEnum.Tribe && !village.HasTribe) _display = DetailsDisplayEnum.Village;
            _underlyingDisplay = DetailsDisplayEnum.Village;
            _village = village;
            _tool = tool;
            if (village != null)
            {
                if (village.HasPlayer)
                {
                    _player = village.Player;
                    if (_player.HasTribe)
                    {
                        _tribe = _player.Tribe;
                    }
                }
            }
        }

        public DetailsCommand(DetailsDisplayEnum display, Player player, VillageTools tool)
        {
            _tooltip = player.ToString();
            _display = display;
            if (_display == DetailsDisplayEnum.None || _display == DetailsDisplayEnum.Village) _display = DetailsDisplayEnum.Player;
            else if (_display == DetailsDisplayEnum.Tribe && !player.HasTribe) _display = DetailsDisplayEnum.Player;
            _underlyingDisplay = DetailsDisplayEnum.Player;
            _player = player;
            _tool = tool;
            if (player != null)
            {
                _tribe = player.Tribe;
            }
        }

        public DetailsCommand(DetailsDisplayEnum display, Tribe tribe, VillageTools tool)
        {
            _tooltip = tribe.ToString();
            _display = display;
            if (_display == DetailsDisplayEnum.None || _display == DetailsDisplayEnum.Village || _display == DetailsDisplayEnum.Player)
                _display = DetailsDisplayEnum.Tribe;
            _underlyingDisplay = DetailsDisplayEnum.Tribe;
            _tribe = tribe;
            _tool = tool;
        }

        /// <summary>
        /// Create a new command with the same underlying display
        /// </summary>
        /// <param name="previous">The previous command</param>
        /// <param name="display">The new display</param>
        public DetailsCommand(DetailsCommand previous, DetailsDisplayEnum display)
        {
            _tooltip = previous.Tooltip;
            _village = previous.Village;
            _player = previous.Player;
            _tribe = previous.Tribe;
            _underlyingDisplay = display; // previous.UnderlyingDisplay;
            _display = display;
            _tool = VillageTools.SelectVillage;
            _subLayout = true;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            switch (_display)
            {
                case DetailsDisplayEnum.None:
                    return "None";
                case DetailsDisplayEnum.Player:
                    return string.Format("{0} ({1})", _player.ToString(), _subLayout.ToString());
                case DetailsDisplayEnum.Tribe:
                    return string.Format("{0} ({1})", _tribe.ToString(), _subLayout.ToString());
                default:
                    return string.Format("{0} ({1})", _village.ToString(), _subLayout.ToString());
            }
        }
        #endregion
    }
}
