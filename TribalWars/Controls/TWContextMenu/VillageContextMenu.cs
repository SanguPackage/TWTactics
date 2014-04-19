#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data;
using TribalWars.Data.Villages;
using TribalWars.Data.Maps.Manipulators;
using TribalWars.Data.Maps;
using Janus.Windows.UI.CommandBars;
using System.Drawing;
using Janus.Windows.UI;
#endregion

namespace TribalWars.Controls.TWContextMenu
{
    /// <summary>
    /// ContextMenu with general Village operations
    /// </summary>
    public class VillageContextMenu : IContextMenu
    {
        #region Fields
        private Village _village;

        private UIContextMenu _menu;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the village 
        /// </summary>
        public Village Village
        {
            get { return _village; }
            set { _village = value; }
        }
        #endregion

        #region Constructors
        public VillageContextMenu()
        {
            UICommand visit = new UICommand(ContextMenuKeys.Village.VISIT, "Visit", CommandType.Command);
            visit.Click += OnVillageOverview;

            UICommand pinpoint = new UICommand(ContextMenuKeys.Village.PINPOINT, "Pinpoint", CommandType.Command);
            pinpoint.Click += OnPinpoint;

            UICommand clipboard = new UICommand(ContextMenuKeys.Village.CLIPBOARD_TEXT, "Clipboard", CommandType.Command);
            clipboard.Click += OnBBCode;

            UICommand mark = new UICommand(ContextMenuKeys.Village.MARK, "Mark", CommandType.Command);
            mark.Click += OnMark;

            UICommand player = new UICommand(ContextMenuKeys.Village.PLAYER, "Player", CommandType.Command);
            //player.Commands.AddRange(World.Default.PlayerContextMenu);

            UICommand tribe = new UICommand(ContextMenuKeys.Village.TRIBE, "Tribe", CommandType.Command);

            _menu = new UIContextMenu("DefaultVillageContextMenu");
            _menu.Commands.AddRange(new UICommand[] {
                visit,
                pinpoint,
                clipboard,
                mark,
                new UICommand(ContextMenuKeys.Village.PLAYERSEPERATOR, string.Empty, CommandType.Separator),
                player,
                tribe
                });
            _menu.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Shows the ContextMenuStrip
        /// </summary>
        public void Show(Control control, System.Drawing.Point position, Village target)
        {
            _village = target;
            if (_village.HasPlayer)
            {
                _menu.Commands[ContextMenuKeys.Village.PLAYER].Text = _village.Player.Name;
                _menu.Commands[ContextMenuKeys.Village.PLAYER].ToolTipText = _village.Player.Tooltip;
                if (_village.HasTribe)
                {
                    _menu.Commands[ContextMenuKeys.Village.TRIBE].Text = _village.Player.Tribe.Tag;
                    _menu.Commands[ContextMenuKeys.Village.TRIBE].ToolTipText = _village.Player.Tribe.Tooltip;
                }
            }
            _menu.Commands[ContextMenuKeys.Village.TRIBE].Visible = _village.HasTribe ? InheritableBoolean.True : InheritableBoolean.False;
            _menu.Commands[ContextMenuKeys.Village.PLAYER].Visible = _village.HasPlayer ? InheritableBoolean.True : InheritableBoolean.False;
            _menu.Commands[ContextMenuKeys.Village.PLAYERSEPERATOR].Visible = _village.HasPlayer ? InheritableBoolean.True : InheritableBoolean.False;

            _menu.Show(control, position);
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Browses to the target village
        /// </summary>
        private void OnVillageOverview(object sender, CommandEventArgs e)
        {
            if (Village != null)
            {
                World.Default.EventPublisher.BrowseUri(null, TribalWars.Controls.Main.Browser.DestinationEnum.InfoVillage, Village.Id.ToString());
            }
        }

        /// <summary>
        /// Puts the pinpoint on the target village
        /// </summary>
        private void OnPinpoint(object sender, CommandEventArgs e)
        {
            if (Village != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(null, Village, VillageTools.PinPoint);
            }
        }

        /// <summary>
        /// Pinpoints and centers the target village
        /// </summary>
        private void OnCenter(object sender, CommandEventArgs e)
        {
            if (Village != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(null, Village, VillageTools.PinPoint);
                World.Default.Map.SetCenter(Data.Maps.Display.GetSpan(Village));
            }
        }

        /// <summary>
        /// Start a new marker for the target village
        /// </summary>
        private void OnMark(object sender, CommandEventArgs e)
        {
            if (Village != null)
            {
                //World.Default.EventPublisher.Publish(null, Village, VillageTools.Default);
            }
        }

        /// <summary>
        /// Create BB code for the village and put on clipboard
        /// </summary>
        private void OnBBCode(object sender, CommandEventArgs e)
        {
            try
            {
                if (Village != null)
                {
                    string text = Village.LocationString;
                    Clipboard.SetText(text);
                }
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        /// Open quick details for the village
        /// </summary>
        private void OnDetails(object sender, CommandEventArgs e)
        {
            if (Village != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(null, Village, VillageTools.SelectVillage);
            }
        }

        /// <summary>
        /// Puts the bullseye on the target village
        /// </summary>
        /// <remarks>Bullseye is used for distance calculation</remarks>
        private void OnDistance(object sender, CommandEventArgs e)
        {
            if (Village != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(null, Village, VillageTools.Distance);
            }
        }
        #endregion
    }
}
