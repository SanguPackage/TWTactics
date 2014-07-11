using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using TribalWars.Controls.AccordeonLocation;
using TribalWars.Villages;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

namespace TribalWars.Controls.Finders
{
    /// <summary>
    /// Collapsable pane with world search options
    /// to find Villages, Players or Tribes
    /// </summary>
    public partial class FinderOptionsControl : UserControl
    {
        #region Constants
        private const int OptionsPaneSmall = 35;
        private const int OptionsPaneBig = 158;
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
        /// the search buttons are visible.
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
            }
        }

        /// <summary>
        /// Limit the results to this value
        /// </summary>
        public decimal LimitResultsValue
        {
            get { return ResultLimit.Value; }
            set { ResultLimit.Value = value; }
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
            FinderOptions options = GetFinderOptions(SearchForEnum.Players);
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
            FinderOptions options = GetFinderOptions(SearchForEnum.Tribes);
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
            FinderOptions options = GetFinderOptions(SearchForEnum.Villages);
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
            Filter.SelectedIndex = (int) FinderOptions.FinderOptionsEnum.Strongest;
            Area.SelectedIndex = (int) FinderOptions.FinderLocationEnum.VisibleMap;
            What.SelectedIndex = (int) SearchForEnum.Players;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Set what to search for (Villages, Players or Tribes)
        /// and then return the search criteria
        /// </summary>
        private FinderOptions GetFinderOptions(SearchForEnum search)
        {
            What.SelectedIndex = (int) search;
            return GetFinderOptions();
        }

        /// <summary>
        /// Create the object that holds the different search criteria
        /// </summary>
        public FinderOptions GetFinderOptions()
        {
            Debug.Assert(Area.SelectedIndex != -1, "Default value should be set by ctor");
            Debug.Assert(Filter.SelectedIndex != -1);

            var options = new FinderOptions((SearchForEnum) What.SelectedIndex);
            
            options.EvaluatedArea = (FinderOptions.FinderLocationEnum)Area.SelectedIndex;
            options.PointsBetweenEnd = (int)PointsBetweenEnd.Value;
            options.PointsBetweenStart = (int)PointsBetweenStart.Value;
            options.ResultLimit = (int)ResultLimit.Value;
            options.SearchStrategy = (FinderOptions.FinderOptionsEnum)Filter.SelectedIndex;

            options.Text = Search.Text.Trim().ToUpper(CultureInfo.InvariantCulture);
            options.Tribe = Tribe.Tribe;

            return options;
        }

        /// <summary>
        /// Make the control reflect the parameter
        /// </summary>
        public void SetFilters(FinderOptions options)
        {
            Area.SelectedIndex = (int)options.EvaluatedArea;
            Filter.SelectedIndex = (int)options.SearchStrategy;
            What.SelectedIndex = (int)options.SearchFor;
            if (options.Tribe != null)
            {
                Tribe.SetTribe(options.Tribe);
            }
            else
            {
                Tribe.SetTribe(null);
            }
        }
        #endregion
    }
}
