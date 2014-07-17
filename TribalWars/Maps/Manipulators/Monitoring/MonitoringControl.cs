using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TribalWars.Controls.AccordeonLocation;
using TribalWars.Tools;
using TribalWars.Worlds;

namespace TribalWars.Maps.Manipulators.Monitoring
{
    /// <summary>
    /// The complete monitoring tab, active in the MainForm
    /// </summary>
    public partial class MonitoringControl : UserControl
    {
        #region Constants
        private const string CurrentDateSuffix = " (Current)";
        private const string PreviousDateSuffix = " (Previous)";
        #endregion

        #region Constructors
        public MonitoringControl()
        {
            InitializeComponent();

            for (int i = 0; i <= OptionsTree.Nodes.Count - 1; i++)
                OptionsTree.Nodes[i].Expand();

            World.Default.EventPublisher.MonitorLoaded += EventPublisher_Monitor;
            World.Default.EventPublisher.Loaded += EventPublisherOnLoaded;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Execute the selected predefined search operation
        /// </summary>
        private void OptionsTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                FinderOptions options = CreateOption(e.Node.Tag.ToString());
                Table.Display(options);
            }
        }

        /// <summary>
        /// Don't allow selection of Villages, Players, Tribes
        /// </summary>
        private void OptionsTree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Parent == null)
                e.Cancel = true;
        }

        private void ActivateAdditionalFilters_CheckedChanged(object sender, System.EventArgs e)
        {
            AdditionalFilters.Enabled = ActivateAdditionalFilters.Checked;
            ApplyAdditionalFilters.Enabled = ActivateAdditionalFilters.Checked;
        }

        private void ApplyAdditionalFilters_Click(object sender, System.EventArgs e)
        {
            FinderOptions options = AdditionalFilters.GetFinderOptions();
            Table.Display(options);
        }
        
        /// <summary>
        /// Reload previous data
        /// </summary>
        private void PreviousDateList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (PreviousDateList.SelectedItems.Count == 1)
            {
                var selectedDate = (WorldDataDateInfo)PreviousDateList.SelectedItems[0].Tag;
                if (!selectedDate.IsCurrentData() && !selectedDate.IsPreviousData())
                {
                    // Reset current previous item
                    foreach (ListViewItem item in PreviousDateList.Items.OfType<ListViewItem>())
                    {
                        var info = (WorldDataDateInfo)item.Tag;
                        if (info.IsPreviousData())
                        {
                            item.BackColor = Color.Transparent;
                            item.Text = item.Text.Replace(PreviousDateSuffix, "");
                        }
                    }

                    // Load dictionary
                    PreviousDateList.Enabled = false;
                    World.Default.LoadPreviousTwSnapshot(selectedDate.Directory.Name);
                }
            }
        }
        #endregion

        #region Workers
        /// <summary>
        /// Occurs after loading a new world or current data (through LoadWorldForm)
        /// </summary>
        private void EventPublisherOnLoaded(object sender, System.EventArgs eventArgs)
        {
            Debug.Assert(World.Default.CurrentData.HasValue);
            CurrentDataDate.InvokeIfRequired(() => CurrentDataDate.Text = World.Default.CurrentData.Value.PrintWorldDate());

            // Show what the current date can be compared with
            List<WorldDataDateInfo> dirs = GetPreviousDatums();
            WorldDataDateInfo current = dirs.Single(x => x.IsCurrentData());
            PreviousDateList.InvokeIfRequired(() =>
            {
                PreviousDateList.Items.Clear();
                foreach (WorldDataDateInfo info in dirs)
                {
                    var listItem = new ListViewItem(info.Text);
                    listItem.Tag = info;
                    
                    if (info.IsCurrentData())
                    {
                        listItem.BackColor = Color.Green;
                        listItem.Text += CurrentDateSuffix;
                    }
                    else
                    {
                        listItem.ToolTipText = GetTimeDifference(current.Value - info.Value);
                    }

                    PreviousDateList.Items.Add(listItem);
                }
            });
        }

        /// <summary>
        /// Occurs when the previous world data loading is completed
        /// </summary>
        private void EventPublisher_Monitor(object sender, System.EventArgs e)
        {
            PreviousDateList.InvokeIfRequired(() =>
                {
                    foreach (ListViewItem item in PreviousDateList.Items.OfType<ListViewItem>())
                    {
                        var info = (WorldDataDateInfo)item.Tag;
                        if (info.IsPreviousData())
                        {
                            item.BackColor = Color.Green;
                            item.Text += PreviousDateSuffix;
                        }
                    }

                    PreviousDateList.Enabled = true;
                });
        }

        /// <summary>
        /// Gets all folders with data from the world
        /// </summary>
        private List<WorldDataDateInfo> GetPreviousDatums()
        {
            return 
               (from dir in Directory.GetDirectories(World.Default.Structure.CurrentWorldDataDirectory)
                let info = WorldDataDateInfo.Create(dir)
                where info != null
                orderby dir descending
                select info).ToList();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Transforms the parameter to {0} days, {1} hours
        /// </summary>
        private string GetTimeDifference(TimeSpan span)
        {
            var str = new StringBuilder();
            AppendTimeDifferenceFraction(str, span.Days, "{0} days");
            AppendTimeDifferenceFraction(str, span.Hours, "{0} hours");
            return str.ToString();
        }

        /// <summary>
        /// Helper for GetTimeDifference
        /// </summary>
        private void AppendTimeDifferenceFraction(StringBuilder str, int amount, string format)
        {
            if (amount != 0)
            {
                if (str.Length != 0)
                {
                    str.Append(", ");
                }
                str.AppendFormat(format, Math.Abs(amount));
            }
        }

        /// <summary>
        /// Create the options for the provided keyword
        /// </summary>
        /// <param name="tag">The search criteria to create description</param>
        private FinderOptions CreateOption(string tag)
        {
            FinderOptions options = null;
            switch (tag)
            {
                case "VillageNewInactive":
                    options = new FinderOptions(SearchForEnum.Villages);
                    options.EvaluatedArea = FinderOptions.FinderLocationEnum.ActiveRectangle;
                    options.SearchStrategy = FinderOptions.FinderOptionsEnum.NewInactives;
                    break;

                case "VillageLostPoints":
                    options = new FinderOptions(SearchForEnum.Villages);
                    options.EvaluatedArea = FinderOptions.FinderLocationEnum.ActiveRectangle;
                    options.SearchStrategy = FinderOptions.FinderOptionsEnum.LostPoints;
                    break;

                case "PlayerNobled":
                    options = new FinderOptions(SearchForEnum.Villages);
                    options.EvaluatedArea = FinderOptions.FinderLocationEnum.ActiveRectangle;
                    options.SearchStrategy = FinderOptions.FinderOptionsEnum.Nobled;
                    break;

                case "PlayerNoActivity":
                    options = new FinderOptions(SearchForEnum.Players);
                    options.EvaluatedArea = FinderOptions.FinderLocationEnum.ActiveRectangle;
                    options.SearchStrategy = FinderOptions.FinderOptionsEnum.Inactives;
                    break;

                case "PlayerTribeChange":
                    options = new FinderOptions(SearchForEnum.Players);
                    options.EvaluatedArea = FinderOptions.FinderLocationEnum.ActiveRectangle;
                    options.SearchStrategy = FinderOptions.FinderOptionsEnum.TribeChange;
                    break;

                case "TribeNobled":
                    options = new FinderOptions(SearchForEnum.Villages);
                    options.EvaluatedArea = FinderOptions.FinderLocationEnum.EntireMap;
                    if (World.Default.You.HasTribe)
                    {
                        options.Tribe = World.Default.You.Tribe;
                    }
                    options.SearchStrategy = FinderOptions.FinderOptionsEnum.Nobled;
                    break;

                case "TribeNoActivity":
                    options = new FinderOptions(SearchForEnum.Players);
                    options.EvaluatedArea = FinderOptions.FinderLocationEnum.EntireMap;
                    if (World.Default.You.HasTribe)
                    {
                        options.Tribe = World.Default.You.Tribe;
                    }
                    options.SearchStrategy = FinderOptions.FinderOptionsEnum.Inactives;
                    break;

                case "TribeLostPoints":
                    options = new FinderOptions(SearchForEnum.Players);
                    options.EvaluatedArea = FinderOptions.FinderLocationEnum.EntireMap;
                    if (World.Default.You.HasTribe)
                    {
                        options.Tribe = World.Default.You.Tribe;
                    }
                    options.SearchStrategy = FinderOptions.FinderOptionsEnum.LostPoints;
                    break;

                case "TribePlayers":
                    options = new FinderOptions(SearchForEnum.Players);
                    options.EvaluatedArea = FinderOptions.FinderLocationEnum.EntireMap;
                    if (World.Default.You.HasTribe)
                    {
                        options.Tribe = World.Default.You.Tribe;
                    }
                    options.SearchStrategy = FinderOptions.FinderOptionsEnum.All;
                    break;

                default:
                    Debug.Assert(false, "No other options");
                    break;
            }

            AdditionalFilters.SetFilters(options);
            if (ActivateAdditionalFilters.Checked)
            {
                return AdditionalFilters.GetFinderOptions();
            }
            else
            {
                return options;
            }
        }
        #endregion 

        #region Private classes
        /// <summary>
        /// Helper class for the previous data selector control
        /// </summary>
        private class WorldDataDateInfo
        {
            public DirectoryInfo Directory { get; private set; }
            public DateTime Value { get; private set; }
            public string Text
            {
                get { return Value.PrintWorldDate(); }
            }

            private WorldDataDateInfo(DirectoryInfo dir, DateTime date)
            {
                Directory = dir;
                Value = date;
            }

            public bool IsCurrentData()
            {
                Debug.Assert(World.Default.CurrentData.HasValue);
                return Value == World.Default.CurrentData.Value;
            }

            public bool IsPreviousData()
            {
                if (World.Default.PreviousData == null)
                {
                    return false;
                }
                return Value == World.Default.PreviousData.Value;
            }

            public static WorldDataDateInfo Create(string directoryName)
            {
                var worldDataDirectory = new DirectoryInfo(directoryName);

                DateTime test;
                if (DateTime.TryParseExact(worldDataDirectory.Name, "yyyyMMddHH", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out test))
                {
                    var info = new WorldDataDateInfo(worldDataDirectory, test);
                    return info;
                }
                return null;
            }

            public override string ToString()
            {
                return string.Format("{0}", Text);
            }
        }
        #endregion
    }
}
