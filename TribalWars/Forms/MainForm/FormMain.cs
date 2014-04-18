#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using TribalWars.Tools;
using Ascend.Windows.Forms;

using TribalWars.Data.Villages;
using TribalWars.Data.Maps;
using TribalWars.Data.Tribes;
using TribalWars.Data.Players;
using TribalWars.Data.Events;
//using JanusExtension.GridEx;
#endregion

namespace TribalWars
{
    public partial class FormMain : Form
    {
        #region Fields
        private readonly TribalWars.Controls.ToolStripLocationChangerControl _locationChanger;
        #endregion

        #region Constructor
        public FormMain()
        {
            InitializeComponent();

            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            _locationChanger = new TribalWars.Controls.ToolStripLocationChangerControl();
            ToolStrip.Items.Add(_locationChanger);

            // Distance calc toolstrip
            //TribalWars.Controls.DistanceToolStrip.DistanceControlHost ctl = new TribalWars.Controls.DistanceToolStrip.DistanceControlHost();
            //ToolStrip.Items.Add(ctl);

            var ctl2 = new TribalWars.Controls.ToolStripTimeConverterCalculator();
            ToolStrip.Items.Add(ctl2);
        }
        #endregion

        /// <summary>
        /// Loading the world data, initializing the map
        /// </summary>
        private void FormMain_Load(object sender, EventArgs e)
        {
            World.Default.Map.InitializeMap(Map);
            World.Default.MiniMap.InitializeMap(MiniMap, World.Default.Map);

            World.Default.EventPublisher.Loaded += new EventHandler<EventArgs>(OnWorldLoaded);
            World.Default.EventPublisher.SettingsLoaded += new EventHandler<EventArgs>(OnWorldSettingsLoaded);
            World.Default.Map.EventPublisher.DisplayTypeChanged += new EventHandler<MapDisplayTypeEventArgs>(EventPublisher_DisplayTypeChanged);
            World.Default.Map.EventPublisher.ManipulatorChanged += new EventHandler<ManipulatorEventArgs>(EventPublisher_ManipulatorChanged);
            World.Default.Map.EventPublisher.PolygonActivated += new EventHandler<PolygonEventArgs>(EventPublisher_PolygonActivated);

            // Auto load world
            string lastWorld = TribalWars.Properties.Settings.Default.LastWorld;
            string lastSettings = TribalWars.Properties.Settings.Default.LastSettings;
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

        private void EventPublisher_PolygonActivated(object sender, PolygonEventArgs e)
        {
            Tabs.SelectedIndex = 4;
        }

        private void EventPublisher_ManipulatorChanged(object sender, ManipulatorEventArgs e)
        {
            ToolStripPolygonManipulator.CheckState = e.ManipulatorType == TribalWars.Data.Maps.Manipulators.ManipulatorManagerTypes.Polygon ? CheckState.Checked : CheckState.Unchecked;
            ToolStripDefaultManipulator.CheckState = e.ManipulatorType == TribalWars.Data.Maps.Manipulators.ManipulatorManagerTypes.Default ? CheckState.Checked : CheckState.Unchecked;
        }

        private void EventPublisher_DisplayTypeChanged(object sender, MapDisplayTypeEventArgs e)
        {
            ToolStripIconDisplay.CheckState = e.DisplayType == TribalWars.Data.Maps.Displays.DisplayTypes.Icon ? CheckState.Checked : CheckState.Unchecked;
            ToolStripShapeDisplay.CheckState = e.DisplayType == TribalWars.Data.Maps.Displays.DisplayTypes.Shape ? CheckState.Checked : CheckState.Unchecked;
        }

        private void OnWorldSettingsLoaded(object sender, EventArgs e)
        {
            StatusSettings.Text = World.Default.SettingsName;
            // TODO: this cannot be in the FormMain code but needs to be 
            // in the Map class!
            // additional maps will not work due this
            // TODO: the CacheSpecialMarkers needs to be in the MarkerGroups
            // so it is done only once!
            // now we have identical "CacheSpecialMarkers" for map & minimap
            World.Default.Map.Display.DisplayManager.CacheSpecialMarkers();
            World.Default.MiniMap.Display.DisplayManager.CacheSpecialMarkers();
            Map.Invalidate();
        }

        private void OnWorldLoaded(object sender, EventArgs e)
        {
            // Available settings for the world
            World w = World.Default;
            _locationChanger.LocationChanger.Initialize(w.Map);
            if (w.SettingsName != null)
            {
                StatusWorld.Text = w.Name;

                // Fill settings contextmenu
                ToolStripSettings.DropDownItems.Clear();
                string[] settingFiles = Directory.GetFiles(w.Structure.CurrentWorldSettingsDirectory, World.InternalStructure.SettingsWildcardString);
                foreach (string setting in settingFiles)
                {
                    var settingInfo = new FileInfo(setting);
                    var itm = new ToolStripMenuItem(settingInfo.Name);
                    ToolStripSettings.DropDownItems.Add(itm);
                    itm.Click += new EventHandler(Settings_Click);
                    if (settingInfo.Name == w.SettingsName)
                    {
                        itm.Checked = true;
                    }
                }
            }
            World.Default.Map.Manipulators.AddMouseMoved(Map_MouseMoved);
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
                //WorldSettings.ReadSettings(selected.Text, this.MapCon);

                // Save last setting
                TribalWars.Properties.Settings.Default.LastSettings = selected.Text;
                TribalWars.Properties.Settings.Default.Save();
            }
        }

        private void SaveSettings()
        {
            string settings = @"\settings.txt";

            DialogResult result = 
                MessageBox.Show(
                "Save settings?" + Environment.NewLine 
                + "Pick Yes to save settings for this TW data." + Environment.NewLine
                + "Pick No to save settings for all data for this world (One dictory up)" + Environment.NewLine
                + "Pick Cancel to not save your settings"
                , "Saving"
                , MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Cancel)
            {
                string file;
                if (result == DialogResult.Yes) file = folderBrowserDialog1.SelectedPath + settings;
                else
                {
                    DirectoryInfo info = Directory.GetParent(folderBrowserDialog1.SelectedPath);
                    file = info.FullName + settings;
                }

                //World.WorldSettings sets = new World.WorldSettings();
                //sets.X = txtX.Text;
                //sets.Y = txtY.Text;
                //sets.Zoom = txtZ.Text;
                //sets.Width = txtWidth.Text;

                //sets.HideAbandoned = chkHideAbandoned.Checked;
                //sets.HideAllyWith = chkHideWithAlly.Checked;
                //sets.HideAllyWithout = chkHideAllyless.Checked;
                //sets.LinesContinent = chkContinent.Checked;
                //sets.LinesProvince = chkProvince.Checked;
                //sets.MeMark = chkMarkMe.Checked;

                //if (Map != null)
                //{
                //    sets.MeAlly = Map.MeAlly;
                //    sets.MeTribe = Map.MeTribe;
                //    sets.MeVillage = Map.MeVillage;

                //    sets.SetMarkers(Map.MarkTribe, Map.MarkAlly);
                //}

                /*using (FileStream s = new FileStream(file, FileMode.Create))
                {
                    BinaryFormatter b = new BinaryFormatter();
                    b.Serialize(s, sets);
                }*/
            }
        }

        #region Timers
        private void ServerTimeTimer_Tick(object sender, EventArgs e)
        {
            StatusServerTime.Text = World.Default.ServerTime.ToLongTimeString();
        }
        #endregion

        #region Menu
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

        private void MenuMapScreenshot_Click(object sender, EventArgs e)
        {
            int i = 0;
            string lFile = string.Format(@"{0}\TW{1:yyyyMMdd}-", World.Default.Structure.CurrentWorldScreenshotDirectory, DateTime.Now);
            while (File.Exists(lFile + i.ToString() + ".png")) i++;
            lFile += i.ToString() + ".png";

            World.Default.Map.Control.Screenshot(lFile);
        }

        private void MenuFileWorldDownload_Click(object sender, EventArgs e)
        {
            World.Default.Structure.Download();
            World.Default.LoadWorld(World.Default.Structure.CurrentWorldDirectory, World.Default.SettingsName);
        }
        #endregion

        #region ToolStrips
        private void ToolStripOpen_Click(object sender, EventArgs e)
        {
            var x = new LoadWorldForm();
            x.ShowDialog();
        }

        private void ToolStripSave_Click(object sender, EventArgs e)
        {
            if (World.Default.HasLoaded)
            {
                World.Default.SaveWorld();
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
                World.Default.Map.SetCenter();
            }
        }

        private void ToolStripIconDisplay_Click(object sender, EventArgs e)
        {
            World.Default.Map.ChangeDisplay(TribalWars.Data.Maps.Displays.DisplayTypes.Icon);
        }

        private void ToolStripShapeDisplay_Click(object sender, EventArgs e)
        {
            World.Default.Map.ChangeDisplay(TribalWars.Data.Maps.Displays.DisplayTypes.Shape);
        }

        private void ToolStripDefaultManipulator_Click(object sender, EventArgs e)
        {
            World.Default.Map.Manipulators.SetManipulator(TribalWars.Data.Maps.Manipulators.ManipulatorManagerTypes.Default);
        }

        private void ToolStripPolygonManipulator_Click(object sender, EventArgs e)
        {
            World.Default.Map.Manipulators.SetManipulator(TribalWars.Data.Maps.Manipulators.ManipulatorManagerTypes.Polygon);
        }
        #endregion

        #region General Pane Stuff
        internal enum NavigationPanes
        {
            Location = 0,
            QuickFinder,
            Markers,
            Attack,
            You,
            YourTribe,
            Paint
        }

        internal NavigationPanePage GetNavigationPane(NavigationPanes pane)
        {
            return this.LeftNavigation.NavigationPages[(int)pane];
        }
        #endregion

        private void MenuFileSaveSettings_Click(object sender, EventArgs e)
        {

        }

        private void MenuFileSaveSettingsAs_Click(object sender, EventArgs e)
        {

        }
    }
}
