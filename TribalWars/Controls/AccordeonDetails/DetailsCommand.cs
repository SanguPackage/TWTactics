using TribalWars.Villages;
using TribalWars.Worlds.Events;

namespace TribalWars.Controls.AccordeonDetails
{
    /// <summary>
    /// Represents an action in the DetailsControl
    /// </summary>
    public class DetailsCommand
    {
        #region Properties
        /// <summary>
        /// Gets the tool for this command
        /// </summary>
        public VillageTools Tool { get; private set; }

        /// <summary>
        /// Gets the tooltip for this command
        /// </summary>
        public string Tooltip { get; private set; }

        /// <summary>
        /// Gets the currently active village
        /// </summary>
        public Village Village { get; private set; }

        /// <summary>
        /// Gets the currently active player
        /// </summary>
        public Player Player { get; private set; }

        /// <summary>
        /// Gets the currently active tribe
        /// </summary>
        public Tribe Tribe { get; private set; }

        /// <summary>
        /// Gets the current display
        /// </summary>
        public DetailsDisplayEnum Display { get; private set; }

        /// <summary>
        /// Gets a value indicating whether
        /// the command will change the entire
        /// control display or not
        /// </summary>
        public bool SubLayout { get; private set; }

        /// <summary>
        /// Gets what was originally selected
        /// ignoring the current display
        /// </summary>
        /// <remarks>Used to set the VillageTextBox value</remarks>
        public DetailsDisplayEnum UnderlyingDisplay { get; private set; }
        #endregion

        #region Constructors
        public DetailsCommand()
        {
            UnderlyingDisplay = DetailsDisplayEnum.None;
            Display = DetailsDisplayEnum.None;
            Tooltip = string.Empty;
        }

        public DetailsCommand(DetailsDisplayEnum display, Village village, VillageTools tool)
        {
            Tooltip = village.ToString();
            Display = display;
            if (Display == DetailsDisplayEnum.None) Display = DetailsDisplayEnum.Village;
            else if (Display == DetailsDisplayEnum.Player && !village.HasPlayer) Display = DetailsDisplayEnum.Village;
            else if (Display == DetailsDisplayEnum.Tribe && !village.HasTribe) Display = DetailsDisplayEnum.Village;
            UnderlyingDisplay = DetailsDisplayEnum.Village;
            Village = village;
            Tool = tool;
            if (village.HasPlayer)
            {
                Player = village.Player;
                if (Player.HasTribe)
                {
                    Tribe = Player.Tribe;
                }
            }
        }

        public DetailsCommand(DetailsDisplayEnum display, Player player, VillageTools tool)
        {
            Tooltip = player.ToString();
            Display = display;
            if (Display == DetailsDisplayEnum.None || Display == DetailsDisplayEnum.Village) Display = DetailsDisplayEnum.Player;
            else if (Display == DetailsDisplayEnum.Tribe && !player.HasTribe) Display = DetailsDisplayEnum.Player;
            UnderlyingDisplay = DetailsDisplayEnum.Player;
            Player = player;
            Tool = tool;
            Tribe = player.Tribe;
        }

        public DetailsCommand(DetailsDisplayEnum display, Tribe tribe, VillageTools tool)
        {
            Tooltip = tribe.ToString();
            Display = display;
            if (Display == DetailsDisplayEnum.None || Display == DetailsDisplayEnum.Village || Display == DetailsDisplayEnum.Player)
                Display = DetailsDisplayEnum.Tribe;
            UnderlyingDisplay = DetailsDisplayEnum.Tribe;
            Tribe = tribe;
            Tool = tool;
        }

        /// <summary>
        /// Create a new command with the same underlying display
        /// </summary>
        /// <param name="previous">The previous command</param>
        /// <param name="display">The new display</param>
        public DetailsCommand(DetailsCommand previous, DetailsDisplayEnum display)
        {
            Tooltip = previous.Tooltip;
            Village = previous.Village;
            Player = previous.Player;
            Tribe = previous.Tribe;
            UnderlyingDisplay = display;
            Display = display;
            Tool = VillageTools.SelectVillage;
            SubLayout = true;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            switch (Display)
            {
                case DetailsDisplayEnum.None:
                    return "None";
                case DetailsDisplayEnum.Player:
                    return string.Format("{0} ({1})", Player, SubLayout);
                case DetailsDisplayEnum.Tribe:
                    return string.Format("{0} ({1})", Tribe, SubLayout);
                default:
                    return string.Format("{0} ({1})", Village, SubLayout);
            }
        }
        #endregion
    }
}
