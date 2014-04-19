using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data;
using TribalWars.Data.Events;
using System.Globalization;
using TribalWars.Data.Players;
using TribalWars.Data.Villages;

namespace TribalWars.Controls.Accordeon.Location
{
    /// <summary>
    /// Collapsable pane with world search options
    /// </summary>
    public partial class FinderOptionsControl : UserControl
    {
        #region Constants
        private const int OptionsPaneSmall = 35;
        private const int OptionsPaneBig = 178;
        #endregion

        #region Fields
        private bool _buttonsVisible;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating wether
        /// the additional search options are visible
        /// </summary>
        public bool Expanded
        {
            get { return Height == OptionsPaneBig; }
            set { Height = value ? OptionsPaneBig : OptionsPaneSmall; }
        }

        /// <summary>
        /// Gets or sets a value indicating wether
        /// the search buttons are visible
        /// </summary>
        public bool Buttonsvisible
        {
            get { return _buttonsVisible; }
            set
            {
                _buttonsVisible = value;
                DropDown.Visible = value;
                cmdPlayer.Visible = value;
                cmdTribe.Visible = value;
                cmdVillage.Visible = value;
                What.Visible = !value;
                What.SelectedIndex = 0;
            }
        }
        #endregion

        #region Events
        public event EventHandler<PlayersEventArgs> PlayersFound;
        public event EventHandler<VillagesEventArgs> VillagesFound;
        public event EventHandler<TribesEventArgs> TribesFound;
        #endregion

        #region Constructors
        public FinderOptionsControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Show extra search options
        /// </summary>
        private void DropDown_Click(object sender, EventArgs e)
        {
            Height = (Height == OptionsPaneBig) ? OptionsPaneSmall : OptionsPaneBig;
        }

        /// <summary>
        /// Displays all players that match the input
        /// </summary>
        private void cmdPlayer_Click(object sender, EventArgs e)
        {
            FinderOptions options = LoadFinderOptions();
            if (World.Default.HasLoaded)
            {
                List<Player> list = options.PlayerMatches().ToList();
                if (PlayersFound != null)
                {
                    PlayersFound(this, new PlayersEventArgs(list, VillageTools.Default));
                }
            }
        }

        /// <summary>
        /// Displays all tribes that match the input
        /// </summary>
        private void cmdTribe_Click(object sender, EventArgs e)
        {
            FinderOptions options = LoadFinderOptions();
            if (World.Default.HasLoaded)
            {
                var list = options.TribeMatches().ToList();
                if (TribesFound != null)
                {
                    TribesFound(this, new TribesEventArgs(list, VillageTools.Default));
                }
            }
        }

        /// <summary>
        /// Displays all villages that match the input
        /// </summary>
        private void cmdVillage_Click(object sender, EventArgs e)
        {
            FinderOptions options = LoadFinderOptions();
            if (World.Default.HasLoaded)
            {
                // Use the VillageTextBox RegEx to match for (X|Y) village input
                Village vil = World.Default.GetVillage(options.Text);
                if (vil != null)
                {
                    World.Default.Map.EventPublisher.SelectVillages(null, vil, VillageTools.PinPoint);
                    if (VillagesFound != null)
                    {
                        VillagesFound(this, new VillagesEventArgs(vil, VillageTools.Default));
                    }
                }
                else
                {
                    // Loop all villages in the world to match the input
                    var list = options.VillageMatches().ToList();
                    if (VillagesFound != null)
                    {
                        VillagesFound(this, new VillagesEventArgs(list, VillageTools.Default));
                    }
                }
            }
        }

        private void FinderOptionsControl_Load(object sender, EventArgs e)
        {
            What.SelectedIndex = 0;
        }

        /// <summary>
        /// Create the object that holds the different search criteria
        /// </summary>
        public FinderOptions LoadFinderOptions()
        {
            var options = new FinderOptions();
            if (Location.SelectedIndex == -1)
                options.Evaluate = FinderOptions.FinderLocationEnum.EntireMap;
            else
                options.Evaluate = (FinderOptions.FinderLocationEnum)Location.SelectedIndex;
            options.PointsBetweenEnd = (int)PointsBetweenEnd.Value;
            options.PointsBetweenStart = (int)PointsBetweenStart.Value;
            options.ResultLimit = (int)ResultLimit.Value;
            if (Filter.SelectedIndex == -1)
                options.Options = FinderOptions.FinderOptionsEnum.All;
            else
                options.Options = (FinderOptions.FinderOptionsEnum)Filter.SelectedIndex;

            switch (What.SelectedIndex)
            {
                case 1:
                    options.SearchFor = SearchForEnum.Tribes;
                    break;
                case 2:
                    options.SearchFor = SearchForEnum.Villages;
                    break;
                default:
                    options.SearchFor = SearchForEnum.Players;
                    break;
            }

            options.Text = Search.Text.Trim().ToUpper(CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(Tribe.Text) && World.Default.Tribes.ContainsKey(Tribe.Text.ToUpper(CultureInfo.InvariantCulture)))
                options.Tribe = World.Default.Tribes[Tribe.Text.ToUpper(CultureInfo.InvariantCulture)];
            return options;
        }
        #endregion
    }
}
