#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data;
using TribalWars.Data.Villages;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
using TribalWars.Controls.TWContextMenu;
using TribalWars.Controls.Display;
#endregion

namespace TribalWars.Controls.Accordeon.Details
{
    #region Enum
    /// <summary>
    /// The different views of the DetailsControl
    /// </summary>
    public enum DetailsDisplayEnum
    {
        None,
        Village,
        Player,
        Tribe
    }
    #endregion

    /// <summary>
    /// Displays details of villages, players and tribes
    /// </summary>
    public partial class DetailsControl : UserControl
    {
        #region Constants
        private const int SplitterWidth = 72;
        #endregion

        #region Fields
        private readonly ToolStripItem[] _villageContext;
        private readonly ToolStripItem[] _playerContext;
        private readonly ToolStripItem[] _tribeContext;

        private readonly Stack<DetailsCommand> _undo = new Stack<DetailsCommand>();
        private Stack<DetailsCommand> _redo = new Stack<DetailsCommand>();
        private DetailsCommand _current = new DetailsCommand();
        #endregion

        #region Constructors
        public DetailsControl()
        {
            InitializeComponent();

            ContextStrip.Items.Clear();
            _villageContext = new ToolStripItem[] { DefenseFlag, AttackFlag, FarmFlag, NobleFlag, ScoutFlag, VillageSeperator, VillageCurrentSituation };

            World.Default.Map.EventPublisher.TribeSelected += EventPublisher_TribeSelected;
            World.Default.Map.EventPublisher.PlayerSelected += EventPublisher_PlayerSelected;
            World.Default.Map.EventPublisher.VillagesSelected += EventPublisher_VillagesSelected;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Initiliaze logic
        /// </summary>
        private void DetailsControl_Load(object sender, EventArgs e)
        {
            // Value is overwritten if the splitter is moved in the constructor
            Tools.Common.MoveSplitter(DetailsGrid, SplitterWidth);
        }

        /// <summary>
        /// Return to the previous display
        /// </summary>
        private void UndoButton_Click(object sender, EventArgs e)
        {
            Undo();
        }

        /// <summary>
        /// Move forward in the display
        /// </summary>
        private void RedoButton_Click(object sender, EventArgs e)
        {
            Redo();
        }

        /// <summary>
        /// Show details propertygrid, hide comments
        /// </summary>
        private void DetailsView_Click(object sender, EventArgs e)
        {
            DetailsGrid.Visible = true;
            Comments.Visible = false;
            DetailsView.Checked = true;
            CommentsView.Checked = false;
        }

        /// <summary>
        /// Show comments, hide details propertygrid
        /// </summary>
        private void CommentsView_Click(object sender, EventArgs e)
        {
            DetailsGrid.Visible = false;
            Comments.Visible = true;
            DetailsView.Checked = false;
            CommentsView.Checked = true;
        }

        /// <summary>
        /// User entered a village in the VillageTextBox
        /// </summary>
        private void SelectedVillage_VillageSelected(object sender, Data.Events.VillageEventArgs e)
        {
            SetQuickFinder(new DetailsCommand(DetailsDisplayEnum.Village, e.SelectedVillage, e.Tool));
        }

        /// <summary>
        /// User entered a player in the VillageTextBox
        /// </summary>
        private void SelectedVillage_PlayerSelected(object sender, Data.Events.PlayerEventArgs e)
        {
            SetQuickFinder(new DetailsCommand(DetailsDisplayEnum.Player, e.SelectedPlayer, e.Tool));
        }

        /// <summary>
        /// User entered a tribe in the VillageTextBox
        /// </summary>
        private void SelectedVillage_TribeSelected(object sender, Data.Events.TribeEventArgs e)
        {
            SetQuickFinder(new DetailsCommand(DetailsDisplayEnum.Tribe, e.SelectedTribe, e.Tool));
        }

        /// <summary>
        /// Catch the world village selected event
        /// </summary>
        private void EventPublisher_VillagesSelected(object sender, Data.Events.VillagesEventArgs e)
        {
            if (!ReferenceEquals(this, sender))
            {
                switch (e.Tool)
                {
                    case VillageTools.SelectVillage:
                    case VillageTools.PinPoint:
                        SetQuickFinder(e.FirstVillage, e.Tool);
                        break;
                }
            }
        }

        /// <summary>
        /// Catch the world player selected event
        /// </summary>
        private void EventPublisher_PlayerSelected(object sender, Data.Events.PlayerEventArgs e)
        {
            switch (e.Tool)
            {
                case VillageTools.SelectVillage:
                case VillageTools.PinPoint:
                    SetQuickFinder(e.SelectedPlayer, e.Tool);
                    break;
            }
        }

        /// <summary>
        /// Catch the world tribe selected event
        /// </summary>
        private void EventPublisher_TribeSelected(object sender, Data.Events.TribeEventArgs e)
        {
            switch (e.Tool)
            {
                case VillageTools.SelectVillage:
                case VillageTools.PinPoint:
                    SetQuickFinder(e.SelectedTribe, e.Tool);
                    break;
            }
        }

        /// <summary>
        /// A row has been selected in the XPTableWrapper
        /// </summary>
        private void Table_RowSelected(object sender, EventArgs e)
        {
            //var test = sender as ITWContextMenu;
            //if (test != null)
            //{
            //    test.DisplayDetails();
            //}

            // TODO NEW: This can be refactored. it probably doesn't work right now
            if (sender is VillageTableRow)
            {
                var row = (VillageTableRow)sender;
                row.DisplayDetails();
            }
            else if (sender is PlayerTableRow)
            {
                var row = (PlayerTableRow)sender;
                row.DisplayDetails();
            }
            else if (sender is ReportTableRow)
            {
                var row = (ReportTableRow)sender;
                SpecialVillage.SetReport(row.Report);
            }
            else if (sender is TribeTableRow)
            {
                var row = (TribeTableRow)sender;
                row.DisplayDetails();
            }
        }

        /// <summary>
        /// Switch to village display
        /// </summary>
        private void ViewVillageDetails_Click(object sender, EventArgs e)
        {
            SetQuickFinder(new DetailsCommand(_current, DetailsDisplayEnum.Village));
        }

        /// <summary>
        /// Switch to player display
        /// </summary>
        private void ViewPlayerDetails_Click(object sender, EventArgs e)
        {
            SetQuickFinder(new DetailsCommand(_current, DetailsDisplayEnum.Player));
        }

        /// <summary>
        /// Switch to tribe display
        /// </summary>
        private void ViewTribeDetails_Click(object sender, EventArgs e)
        {
            SetQuickFinder(new DetailsCommand(_current, DetailsDisplayEnum.Tribe));
        }

        /// <summary>
        /// View current situation of the village
        /// </summary>
        private void VillageCurrentSituation_Click(object sender, EventArgs e)
        {
            if (_current.Village != null)
            {
                SpecialVillage.SetReport(_current.Village.Reports.CurrentSituation);
            }
        }

        /// <summary>
        /// Save comments
        /// </summary>
        private void Comments_TextChanged(object sender, EventArgs e)
        {
            _current.Village.Comments = Comments.Text;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Prepares the user control for viewing
        /// </summary>
        /// <param name="command">Encapsulates a view in the DetailsControl</param>
        public void SetQuickFinder(DetailsCommand command)
        {
            if (command.Tool == VillageTools.PinPoint)
            {
                _redo = new Stack<DetailsCommand>();
                RedoButton.Enabled = false;
                _undo.Push(_current);
                UndoButton.Enabled = true;
                UndoButton.ToolTipText = _current.Tooltip;
            }
            _current = command;

            SetQuickFinderCore();
        }

        /// <summary>
        /// Prepares the user control for village details viewing
        /// </summary>
        /// <param name="village">The village we want to see the details for</param>
        public void SetQuickFinder(Village village, VillageTools tool)
        {
            SetQuickFinder(new DetailsCommand(_current.Display, village, tool));
        }

        /// <summary>
        /// Prepares the user control for player details viewing
        /// </summary>
        /// <param name="player">The player we want to see the details for</param>
        public void SetQuickFinder(Player player, VillageTools tool)
        {
            SetQuickFinder(new DetailsCommand(_current.Display, player, tool));
        }

        /// <summary>
        /// Prepares the user control for tribe details viewing
        /// </summary>
        /// <param name="tribe">The tribe we want to see the details for</param>
        public void SetQuickFinder(Tribe tribe, VillageTools tool)
        {
            SetQuickFinder(new DetailsCommand(_current.Display, tribe, tool));
        }

        /// <summary>
        /// Changes the display without changing the
        /// current selected village, player or tribe
        /// </summary>
        public void SetLayout()
        {
            ContextStrip.Items.Clear();
            ViewVillageDetails.Enabled = _current.Village != null;
            ViewTribeDetails.Enabled = _current.Tribe != null;
            ViewPlayerDetails.Enabled = _current.Player != null;
            switch (_current.Display)
            {
                case DetailsDisplayEnum.Player:
                    if (_playerContext != null) ContextStrip.Items.AddRange(_playerContext);
                    ViewVillageDetails.Checked = false;
                    ViewPlayerDetails.Checked = true;
                    ViewTribeDetails.Checked = false;
                    SpecialVillage.Visible = false;

                    Table.DisplayVillages(_current.Player);
                    DetailsGrid.SelectedObject = new ExtendedPlayerDescriptor(_current.Player);

                    if (_current.Village != null)
                        World.Default.Map.EventPublisher.SelectVillages(null, _current.Village, VillageTools.Notes);
                    else
                        World.Default.Map.EventPublisher.SelectVillages(null, _current.Player, VillageTools.Notes);
                    break;
                case DetailsDisplayEnum.Tribe:
                    if (_tribeContext != null) ContextStrip.Items.AddRange(_tribeContext);
                    ViewVillageDetails.Checked = false;
                    ViewPlayerDetails.Checked = false;
                    ViewTribeDetails.Checked = true;
                    SpecialVillage.Visible = false;

                    Table.DisplayPlayers(_current.Tribe.Players);
                    DetailsGrid.SelectedObject = new ExtendedTribeDescriptor(_current.Tribe);

                    if (_current.Village != null)
                        World.Default.Map.EventPublisher.SelectVillages(null, _current.Village, VillageTools.Notes);
                    else if (_current.Player != null)
                        World.Default.Map.EventPublisher.SelectVillages(null, _current.Player, VillageTools.Notes);
                    else
                        World.Default.Map.EventPublisher.SelectVillages(null, _current.Tribe, VillageTools.Notes);
                    break;
                case DetailsDisplayEnum.Village:
                    if (_villageContext != null) ContextStrip.Items.AddRange(_villageContext);
                    ViewVillageDetails.Checked = true;
                    ViewPlayerDetails.Checked = false;
                    ViewTribeDetails.Checked = false;
                    SpecialVillage.Visible = true;

                    SpecialVillage.SetReport(_current.Village.Reports.CurrentSituation);
                    DetailsGrid.SelectedObject = new ExtendedVillageDescriptor(_current.Village);
                    Table.DisplayReports(_current.Village, _current.Village.Reports);
                    SetButtons(_current.Village.Type);
                    Comments.Text = _current.Village.Comments;
                    //if (!string.IsNullOrEmpty(_current.Village.Comments))


                    World.Default.Map.EventPublisher.SelectVillages(null, _current.Village, VillageTools.Notes);
                    break;
                default:
                    ViewVillageDetails.Checked = false;
                    ViewPlayerDetails.Checked = false;
                    ViewTribeDetails.Checked = false;

                    DetailsGrid.SelectedObject = null;
                    Table.Clear();
                    break;
            }
        }
        #endregion

        #region Village Flags
        /// <summary>
        /// Sets the correct village flag buttons state
        /// </summary>
        private void SetButtons(VillageType type)
        {
            AttackFlag.Checked = (type & VillageType.Attack) != 0;
            DefenseFlag.Checked = (type & VillageType.Defense) != 0;
            FarmFlag.Checked = (type & VillageType.Farm) != 0;
            NobleFlag.Checked = (type & VillageType.Noble) != 0;
            ScoutFlag.Checked = (type & VillageType.Scout) != 0;
        }

        /// <summary>
        /// Changes the flags for a village
        /// and saves it in the village report txt
        /// </summary>
        private void FlagSwitcher(VillageType type, ToolStripButton button)
        {
            if (_current.Village != null)
            {
                VillageType villageType = _current.Village.Type;
                if ((villageType & type) == 0) villageType |= type;
                else villageType &= ~type;
                button.Checked = (villageType & type) != 0;
                _current.Village.Type = villageType;
                
                World.Default.DrawMaps();
            }
        }

        private void DefenseFlag_Click(object sender, EventArgs e)
        {
            FlagSwitcher(VillageType.Defense, DefenseFlag);
        }

        private void AttackFlag_Click(object sender, EventArgs e)
        {
            FlagSwitcher(VillageType.Attack, AttackFlag);
        }

        private void ScoutFlag_Click(object sender, EventArgs e)
        {
            FlagSwitcher(VillageType.Scout, ScoutFlag);
        }

        private void NobleFlag_Click(object sender, EventArgs e)
        {
            FlagSwitcher(VillageType.Noble, NobleFlag);
        }

        private void FarmFlag_Click(object sender, EventArgs e)
        {
            FlagSwitcher(VillageType.Farm, FarmFlag);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the entire control enablement etc
        /// </summary>
        private void SetQuickFinderCore()
        {
            if (!_current.SubLayout)
            {
                switch (_current.UnderlyingDisplay)
                {
                    case DetailsDisplayEnum.Village:
                        SelectedVillage.Village = _current.Village;
                        break;
                    case DetailsDisplayEnum.Player:
                        SelectedVillage.Player = _current.Player;
                        break;
                    case DetailsDisplayEnum.Tribe:
                        SelectedVillage.Tribe = _current.Tribe;
                        break;
                    default:
                        SelectedVillage.PlayerTribeFinderTextBox.Clear();
                        break;
                }
            }
            SetLayout();
        }
        #endregion

        #region Command Pattern
        /// <summary>
        /// Go to the previous display
        /// </summary>
        public void Undo()
        {
            UndoRedo(_undo, UndoButton, _redo, RedoButton);
        }

        /// <summary>
        /// Go to the previous display
        /// after an Undo action
        /// </summary>
        public void Redo()
        {
            UndoRedo(_redo, RedoButton, _undo, UndoButton);
        }

        /// <summary>
        /// Undo or Redo
        /// </summary>
        private void UndoRedo(Stack<DetailsCommand> pop, ToolStripButton popButton, Stack<DetailsCommand> push, ToolStripButton pushButton)
        {
            if (pop.Count > 0)
            {
                DetailsCommand command = pop.Pop();
                push.Push(_current);
                pushButton.Enabled = true;
                pushButton.ToolTipText = _current.Tooltip;
                _current = command;
                SetQuickFinderCore();
            }

            // can we undo/redo more
            if (pop.Count == 0)
            {
                popButton.Enabled = false;
            }
            else
            {
                popButton.ToolTipText = pop.Peek().Tooltip;
            }

            World.Default.Map.EventPublisher.SelectVillages(this, _current.Village, VillageTools.PinPoint);
        }
        #endregion
    }
}
