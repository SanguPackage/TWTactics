#region Using
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using TribalWars.Controls.Common;
using Ascend.Windows.Forms;
using TribalWars.Controls.TWContextMenu;
using TribalWars.Data;
using TribalWars.Data.Maps.Displays;
using TribalWars.Data.Maps.Manipulators.Implementations;
using TribalWars.Data.Maps.Manipulators.Managers;
using TribalWars.Data.Villages;
using TribalWars.Data.Events;
#endregion

namespace TribalWars.Forms
{
    public partial class MainForm : Form
    {
        #region Fields
        private readonly ToolStripLocationChangerControl _locationChanger;
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();

            _locationChanger = new ToolStripLocationChangerControl();
            ToolStrip.Items.Add(_locationChanger);

            ToolStrip.Items.Add(new ToolStripSeparator());

            // Distance calc toolstrip
            //TribalWars.Controls.DistanceToolStrip.DistanceControlHost ctl = new TribalWars.Controls.DistanceToolStrip.DistanceControlHost();
            //ToolStrip.Items.Add(ctl);

            var ctl2 = new ToolStripTimeConverterCalculator();
            ToolStrip.Items.Add(ctl2);
        }

        /// <summary>
        /// Loading the world data, initializing the map
        /// </summary>
        private void FormMain_Load(object sender, EventArgs e)
        {
            World.Default.Map.InitializeMap(Map);
            World.Default.MiniMap.InitializeMap(MiniMap, World.Default.Map);

            World.Default.EventPublisher.Loaded += OnWorldLoaded;
            World.Default.EventPublisher.SettingsLoaded += OnWorldSettingsLoaded;
            World.Default.Map.EventPublisher.DisplayTypeChanged += EventPublisher_DisplayTypeChanged;
            World.Default.Map.EventPublisher.ManipulatorChanged += EventPublisher_ManipulatorChanged;
            World.Default.Map.EventPublisher.PolygonActivated += EventPublisher_PolygonActivated;
            World.Default.Map.EventPublisher.LocationChanged += EventPublisher_LocationChanged;
            World.Default.EventPublisher.Browse += EventPublisher_Browse;
            World.Default.Map.EventPublisher.VillagesSelected += EventPublisher_VillagesSelected;
            World.Default.Map.EventPublisher.PlayerSelected += EventPublisher_VillagesSelected;
            World.Default.Map.EventPublisher.TribeSelected += EventPublisher_VillagesSelected;

            // Auto load world
            string lastWorld = Properties.Settings.Default.LastWorld;
            string lastSettings = Properties.Settings.Default.LastSettings;
            if (!World.Default.LoadWorld(lastWorld, lastSettings))
            {
                // Here begins the wizard for creating a new world...
                using (var loadForm = new LoadWorldForm())
                {
                    loadForm.ShowDialog();
                }
                locationControl1.FocusYouControl();
            }

            Polygon.Initialize();
            ToolStripDefaultManipulator.CheckState = CheckState.Checked;
        }
        #endregion

        #region Switching Location & Map Display
        private int? _lastShapeZoom;
        private int? _lastIconZoom;
        private bool _isInShapeDisplay;

        private void ToolStripShapeDisplay_Click(object sender, EventArgs e)
        {
            World.Default.Map.ChangeDisplay(DisplayTypes.Shape, _lastShapeZoom ?? 10);
        }

        private void ToolStripIconDisplay_Click(object sender, EventArgs e)
        {
            World.Default.Map.ChangeDisplay(DisplayTypes.Icon, _lastIconZoom ?? 1);
        }
        
        private void EventPublisher_LocationChanged(object sender, MapLocationEventArgs e)
        {
            if (_isInShapeDisplay)
            {
                _lastShapeZoom = e.NewLocation.Zoom;
            }
            else
            {
                _lastIconZoom = e.NewLocation.Zoom;
            }
        }

        private void EventPublisher_DisplayTypeChanged(object sender, MapDisplayTypeEventArgs e)
        {
            ToolStripIconDisplay.CheckState = e.DisplayType == DisplayTypes.Icon ? CheckState.Checked : CheckState.Unchecked;
            ToolStripShapeDisplay.CheckState = e.DisplayType == DisplayTypes.Shape ? CheckState.Checked : CheckState.Unchecked;

            _isInShapeDisplay = e.DisplayType == DisplayTypes.Shape;
        }

        private void Map_MouseMoved(MouseEventArgs e, Point mapLocation, Village village, Point activeLocation, Point activeVillage)
        {
            StatusXY.Text = string.Format("{0}|{1}", activeLocation.X, activeLocation.Y);
            if (village == null)
            {
                StatusPlayer.Text = string.Empty;
                StatusTribe.Text = string.Empty;
                StatusVillage.Text = string.Empty;
            }
            else
            {
                StatusPlayer.Text = village.HasPlayer ? string.Format("{0} (#{1})", village.Player.Name, village.Player.Rank) : string.Empty;
                StatusTribe.Text = village.HasTribe ? village.Player.Tribe.ToString() : string.Empty;
                StatusVillage.Text = village.ToString();
            }
        }
        #endregion

        #region Manipulators
        private void EventPublisher_PolygonActivated(object sender, PolygonEventArgs e)
        {
            Tabs.SelectedTab = TabsPolygon;
        }

        private void EventPublisher_ManipulatorChanged(object sender, ManipulatorEventArgs e)
        {
            ToolStripPolygonManipulator.CheckState = e.ManipulatorType == ManipulatorManagerTypes.Polygon ? CheckState.Checked : CheckState.Unchecked;
            ToolStripDefaultManipulator.CheckState = e.ManipulatorType == ManipulatorManagerTypes.Default ? CheckState.Checked : CheckState.Unchecked;
        }

        private void ToolStripDefaultManipulator_Click(object sender, EventArgs e)
        {
            World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Default);
        }

        private void ToolStripPolygonManipulator_Click(object sender, EventArgs e)
        {
            World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Polygon);
        }
        #endregion

        #region Settings Loading & Saving
        private void OnWorldSettingsLoaded(object sender, EventArgs e)
        {
            StatusSettings.Text = World.Default.SettingsName;

            World w = World.Default;
            if (w.SettingsName != null)
            {
                StatusWorld.Text = w.Name;

                // Fill settings contextmenu
                ToolStripSettings.DropDownItems.Clear();
                string[] settingFiles = Directory.GetFiles(w.Structure.CurrentWorldSettingsDirectory, "*" + World.InternalStructure.SettingsExtensionString);
                foreach (string setting in settingFiles)
                {
                    var settingInfo = new FileInfo(setting);
                    var itm = new ToolStripMenuItem(settingInfo.Name);
                    ToolStripSettings.DropDownItems.Add(itm);
                    itm.Click += Settings_Click;
                    if (settingInfo.Name == w.SettingsName)
                    {
                        itm.Checked = true;
                    }
                }
            }

            Map.Invalidate();
        }

        private void OnWorldLoaded(object sender, EventArgs e)
        {
            _locationChanger.LocationChanger.Initialize(World.Default.Map);

            World.Default.Map.Manipulators.AddMouseMoved(Map_MouseMoved);
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            var selected = sender as ToolStripMenuItem;
            if (selected != null)
            {
                foreach (ToolStripMenuItem itm in ToolStripSettings.DropDownItems)
                {
                    itm.Checked = false;
                }
                selected.Checked = true;
                if (World.Default.LoadSettings(selected.Text))
                {
                    Properties.Settings.Default.LastSettings = selected.Text;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void MenuFileSaveSettings_Click(object sender, EventArgs e)
        {
            if (World.Default.HasLoaded)
            {
                World.Default.SaveSettings();
            }
        }

        private void MenuFileSaveSettingsAs_Click(object sender, EventArgs e)
        {
            if (World.Default.HasLoaded)
            {
                saveFileDialog1.InitialDirectory = World.Default.Structure.CurrentWorldSettingsDirectory;
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string settingsName = new FileInfo(saveFileDialog1.FileName).Name;
                    World.Default.SaveSettings(settingsName);
                    World.Default.LoadWorld(World.Default.Structure.CurrentWorldDirectory, World.Default.SettingsName);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (World.Default.HasLoaded)
            {
#if !DEBUG
                DialogResult result = MessageBox.Show("Save settings before quitting?", "Quit?", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (result == DialogResult.Yes)
                {
                    World.Default.SaveSettings();
                }
#endif
            }
        }
        #endregion

        #region Menu & Toolstrip Handlers
        private void MenuFileNew_Click(object sender, EventArgs e)
        {
            var frm = new LoadWorldForm();
            frm.ShowDialog();
        }

        private void MenuFileLoadWorld_Click(object sender, EventArgs e)
        {
            var frm = new LoadWorldForm();
            frm.ShowDialog();
        }

        private void MenuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuFileWorldDownload_Click(object sender, EventArgs e)
        {
            World.Default.Structure.Download();
            World.Default.LoadWorld(World.Default.Structure.CurrentWorldDirectory, World.Default.SettingsName);
        }

        private void MenuFileSynchronizeTime_Click(object sender, EventArgs e)
        {
            using (var timeSetter = new TimeZoneForm(World.Default))
            {
                timeSetter.ShowDialog();
            }
        }

        private void ToolStripOpen_Click(object sender, EventArgs e)
        {
            var x = new LoadWorldForm();
            x.ShowDialog();
        }

        private void ToolStripSave_Click(object sender, EventArgs e)
        {
            if (World.Default.HasLoaded)
            {
                World.Default.SaveSettings();
            }
        }

        private void ToolStripDraw_Click(object sender, EventArgs e)
        {
            if (World.Default.HasLoaded)
            {
                World.Default.DrawMaps();
            }
        }

        private void ToolStripHome_Click(object sender, EventArgs e)
        {
            if (World.Default.HasLoaded)
            {
                World.Default.Map.GoHome();
            }
        }

        private void ToolStripActiveRectangle_Click(object sender, EventArgs e)
        {
            if (World.Default.HasLoaded)
            {
                if (ToolStripActiveRectangle.Checked)
                {
                    ToolStripActiveRectangle.Checked = false;
                    World.Default.Map.Manipulators.CurrentManipulator.RemoveFullControlManipulator();
                }
                else
                {
                    ToolStripActiveRectangle.Checked = true;

                    var setActiveRectangle = new ActiveRectangleManipulator(World.Default.Map, (bool newRectangleSet) =>
                        {
                            ToolStripActiveRectangle.Checked = false;
                            if (newRectangleSet)
                            {
                                Tabs.SelectedTab = TabsMonitoring;
                            }
                        });
                    World.Default.Map.Manipulators.CurrentManipulator.SetFullControlManipulator(setActiveRectangle);
                }
            }
        }
        #endregion

        #region Small functionality
        private void ServerTimeTimer_Tick(object sender, EventArgs e)
        {
            StatusServerTime.Text = World.Default.ServerTime.ToLongTimeString();
        }

        private void MenuMapScreenshot_Click(object sender, EventArgs e)
        {
            int i = 0;
            string lFile = string.Format(@"{0}TW{1:yyyyMMdd}-", World.Default.Structure.CurrentWorldScreenshotDirectory, DateTime.Now);
            while (File.Exists(lFile + i.ToString(CultureInfo.InvariantCulture) + ".png")) i++;
            lFile += i.ToString(CultureInfo.InvariantCulture) + ".png";

            World.Default.Map.Control.Screenshot(lFile);

            StatusMessage.Text = "Screenshot saved as " + lFile;
        }

        private void MenuMapSeeScreenshots_Click(object sender, EventArgs e)
        {
            Process.Start(World.Default.Structure.CurrentWorldScreenshotDirectory);
        }

        private void EventPublisher_Browse(object sender, BrowserEventArgs e)
        {
            Tabs.SelectedTab = TabsBrowser;
        }

        private void EventPublisher_VillagesSelected(object sender, VillagesEventArgs e)
        {
            if (e.Tool == VillageTools.PinPoint)
            {
                if (sender is string && (string)sender == VillageContextMenu.OnDetailsHack)
                {
                    var pane = GetNavigationPane(NavigationPanes.Details);
                    LeftNavigation.SelectNavigationPage(pane.Key);

                    Tabs.SelectedTab = TabsMap;
                }
            }
        }
        #endregion

        #region General Pane Stuff
        private enum NavigationPanes
        {
            Location = 0,
            Details,
            Markers,
            Attack
        }

        private NavigationPanePage GetNavigationPane(NavigationPanes pane)
        {
            return LeftNavigation.NavigationPages[(int)pane];
        }
        #endregion
    }
}
