#region Using
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using TribalWars.Controls.Common;
using Ascend.Windows.Forms;
using TribalWars.Data;
using TribalWars.Data.Maps.Displays;
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

            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            _locationChanger = new ToolStripLocationChangerControl();
            ToolStrip.Items.Add(_locationChanger);

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

            // Auto load world
            string lastWorld = Properties.Settings.Default.LastWorld;
            string lastSettings = Properties.Settings.Default.LastSettings;
            if (!World.Default.LoadWorld(lastWorld, lastSettings))
            {
                // Here begins the wizard for creating a new world...
                var loadForm = new LoadWorldForm();
                loadForm.ShowDialog();
                locationControl1.FocusYouControl();
            }

            Polygon.Initialize();
            ToolStripDefaultManipulator.CheckState = CheckState.Checked;
        }
        #endregion

        #region Switching Location & Map Display
        private int? _lastShapeZoom;
        private bool _isInShapeDisplay;

        private void ToolStripShapeDisplay_Click(object sender, EventArgs e)
        {
            World.Default.Map.ChangeDisplay(DisplayTypes.Shape, _lastShapeZoom ?? 10);
        }

        private void ToolStripIconDisplay_Click(object sender, EventArgs e)
        {
            World.Default.Map.ChangeDisplay(Data.Maps.Displays.DisplayTypes.Icon, 1);
        }
        
        private void EventPublisher_LocationChanged(object sender, MapLocationEventArgs e)
        {
            if (_isInShapeDisplay)
            {
                _lastShapeZoom = e.NewLocation.Zoom;
            }
        }

        private void EventPublisher_DisplayTypeChanged(object sender, MapDisplayTypeEventArgs e)
        {
            ToolStripIconDisplay.CheckState = e.DisplayType == DisplayTypes.Icon ? CheckState.Checked : CheckState.Unchecked;
            ToolStripShapeDisplay.CheckState = e.DisplayType == DisplayTypes.Shape ? CheckState.Checked : CheckState.Unchecked;

            _isInShapeDisplay = e.DisplayType == DisplayTypes.Shape;
            if (_isInShapeDisplay)
            {
                //_lastShapeZoom = e.Location.Zoom;
            }
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
            Tabs.SelectedIndex = 4;
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
            World w = World.Default;
            _locationChanger.LocationChanger.Initialize(w.Map);

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
                World.Default.Map.SetCenter(World.Default.Map.Location);
            }
        }

        private void ToolStripHome_Click(object sender, EventArgs e)
        {
            if (World.Default.HasLoaded)
            {
                World.Default.Map.GoHome();
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
        #endregion

        #region General Pane Stuff
        internal enum NavigationPanes
        {
            Location = 0,
            Details,
            Markers,
            Attack
        }

        internal NavigationPanePage GetNavigationPane(NavigationPanes pane)
        {
            return LeftNavigation.NavigationPages[(int)pane];
        }
        #endregion
    }
}
