using TribalWars.Browsers.Control;
using TribalWars.Controls.AccordeonDetails;
using TribalWars.Controls.AccordeonLocation;
using TribalWars.Maps.AttackPlans.Controls;
using TribalWars.Maps.Controls;
using TribalWars.Maps.Markers;
using TribalWars.Maps.Monitoring;
using TribalWars.Maps.Polygons;

namespace TribalWars.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.FormSplitter = new System.Windows.Forms.SplitContainer();
			this.LeftSplitter = new System.Windows.Forms.SplitContainer();
			this.MiniMap = new TribalWars.Maps.Controls.MiniMapControl();
			this.LeftNavigation = new Ascend.Windows.Forms.NavigationPane();
			this.LeftNavigation_Location = new Ascend.Windows.Forms.NavigationPanePage();
			this.locationControl1 = new TribalWars.Controls.AccordeonLocation.LocationControl();
			this.LeftNavigation_QuickFind = new Ascend.Windows.Forms.NavigationPanePage();
			this.detailsControl1 = new TribalWars.Controls.AccordeonDetails.DetailsControl();
			this.LeftNavigation_Markers = new Ascend.Windows.Forms.NavigationPanePage();
			this.markersContainerControl1 = new TribalWars.Maps.Markers.MarkersControl();
			this.LeftNavigation_Distance = new Ascend.Windows.Forms.NavigationPanePage();
			this._attackPlan = new TribalWars.Maps.AttackPlans.Controls.AttackPlanCollectionControl();
			this.Tabs = new Janus.Windows.UI.Tab.UITab();
			this.TabsMap = new Janus.Windows.UI.Tab.UITabPage();
			this.Map = new TribalWars.Maps.Controls.MapControl();
			this.TabsBrowser = new Janus.Windows.UI.Tab.UITabPage();
			this.browserControl1 = new TribalWars.Browsers.Control.BrowserControl();
			this.TabsPolygon = new Janus.Windows.UI.Tab.UITabPage();
			this.Polygon = new TribalWars.Maps.Polygons.PolygonControl();
			this.TabsMonitoring = new Janus.Windows.UI.Tab.UITabPage();
			this.monitoringControl1 = new TribalWars.Maps.Monitoring.MonitoringControl();
			this.FormToolbarContainer = new System.Windows.Forms.ToolStripContainer();
			this.ToolStrip = new System.Windows.Forms.ToolStrip();
			this.ToolstripButtonCreateWorld = new System.Windows.Forms.ToolStripButton();
			this.ToolStripOpen = new System.Windows.Forms.ToolStripButton();
			this.ToolStripDownload = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripSettings = new System.Windows.Forms.ToolStripDropDownButton();
			this.ToolStripSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripHome = new System.Windows.Forms.ToolStripButton();
			this.ToolStripActiveRectangle = new System.Windows.Forms.ToolStripButton();
			this.ToolStripDraw = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripIconDisplay = new System.Windows.Forms.ToolStripButton();
			this.ToolStripShapeDisplay = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripDefaultManipulator = new System.Windows.Forms.ToolStripButton();
			this.ToolStripPolygonManipulator = new System.Windows.Forms.ToolStripButton();
			this.ToolStripAttackManipulator = new System.Windows.Forms.ToolStripButton();
			this.ToolStripChurchManipulator = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLocationChangerControl1 = new TribalWars.Controls.Common.ToolStripControlHostWrappers.ToolStripLocationChangerControl();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripProgramSettings = new System.Windows.Forms.ToolStripButton();
			this.ToolStripAbout = new System.Windows.Forms.ToolStripButton();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.PickColor = new System.Windows.Forms.ColorDialog();
			this.Status = new System.Windows.Forms.StatusStrip();
			this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.StatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
			this.StatusDataTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.StatusXY = new System.Windows.Forms.ToolStripStatusLabel();
			this.StatusVillage = new System.Windows.Forms.ToolStripStatusLabel();
			this.StatusPlayer = new System.Windows.Forms.ToolStripStatusLabel();
			this.StatusTribe = new System.Windows.Forms.ToolStripStatusLabel();
			this.StatusSettings = new System.Windows.Forms.ToolStripStatusLabel();
			this.StatusWorld = new System.Windows.Forms.ToolStripStatusLabel();
			this.StatusServerTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
			this.ServerTimeTimer = new System.Windows.Forms.Timer(this.components);
			this.GeneralTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.MenuBar = new System.Windows.Forms.MenuStrip();
			this.MenuToolstrip = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuFileNew = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuFileLoadWorld = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuFileWorldDownload = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuFileSaveSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuFileSaveSettingsAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuFileSetActivePlayer = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuFileSynchronizeTime = new System.Windows.Forms.ToolStripMenuItem();
			this.setLanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SetLanguage_English = new System.Windows.Forms.ToolStripMenuItem();
			this.SetLanguage_Dutch = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuMapMonitoringArea = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuMapSetHomeLocation = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuMapIconDisplay = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuMapShapeDisplay = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuMapInteractionDefault = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuMapInteractionPolygon = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuMapInteractionPlanAttacks = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuMapSelectPane0 = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuMapSelectPane1 = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuMapSelectPane2 = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuMapSelectPane3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuMapScreenshot = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuMapSeeScreenshots = new System.Windows.Forms.ToolStripMenuItem();
			this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuWindowsManageYourVillages = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuWindowsImportVillageCoordinates = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
			this.MenuWindowsManageYourAttackersPool = new System.Windows.Forms.ToolStripMenuItem();
			this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuWindowsAddTimes = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuHelpReportBug = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
			this.VillageTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.FormSplitter)).BeginInit();
			this.FormSplitter.Panel1.SuspendLayout();
			this.FormSplitter.Panel2.SuspendLayout();
			this.FormSplitter.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.LeftSplitter)).BeginInit();
			this.LeftSplitter.Panel1.SuspendLayout();
			this.LeftSplitter.Panel2.SuspendLayout();
			this.LeftSplitter.SuspendLayout();
			this.LeftNavigation.SuspendLayout();
			this.LeftNavigation_Location.SuspendLayout();
			this.LeftNavigation_QuickFind.SuspendLayout();
			this.LeftNavigation_Markers.SuspendLayout();
			this.LeftNavigation_Distance.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Tabs)).BeginInit();
			this.Tabs.SuspendLayout();
			this.TabsMap.SuspendLayout();
			this.TabsBrowser.SuspendLayout();
			this.TabsPolygon.SuspendLayout();
			this.TabsMonitoring.SuspendLayout();
			this.FormToolbarContainer.ContentPanel.SuspendLayout();
			this.FormToolbarContainer.TopToolStripPanel.SuspendLayout();
			this.FormToolbarContainer.SuspendLayout();
			this.ToolStrip.SuspendLayout();
			this.Status.SuspendLayout();
			this.MenuBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// FormSplitter
			// 
			this.FormSplitter.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.FormSplitter, "FormSplitter");
			this.FormSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.FormSplitter.Name = "FormSplitter";
			// 
			// FormSplitter.Panel1
			// 
			this.FormSplitter.Panel1.BackColor = System.Drawing.Color.Transparent;
			this.FormSplitter.Panel1.Controls.Add(this.LeftSplitter);
			resources.ApplyResources(this.FormSplitter.Panel1, "FormSplitter.Panel1");
			// 
			// FormSplitter.Panel2
			// 
			this.FormSplitter.Panel2.BackColor = System.Drawing.Color.Transparent;
			this.FormSplitter.Panel2.Controls.Add(this.Tabs);
			resources.ApplyResources(this.FormSplitter.Panel2, "FormSplitter.Panel2");
			this.FormSplitter.TabStop = false;
			// 
			// LeftSplitter
			// 
			resources.ApplyResources(this.LeftSplitter, "LeftSplitter");
			this.LeftSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.LeftSplitter.Name = "LeftSplitter";
			// 
			// LeftSplitter.Panel1
			// 
			this.LeftSplitter.Panel1.Controls.Add(this.MiniMap);
			// 
			// LeftSplitter.Panel2
			// 
			this.LeftSplitter.Panel2.Controls.Add(this.LeftNavigation);
			this.LeftSplitter.TabStop = false;
			// 
			// MiniMap
			// 
			this.MiniMap.BackColor = System.Drawing.Color.Green;
			resources.ApplyResources(this.MiniMap, "MiniMap");
			this.MiniMap.Name = "MiniMap";
			// 
			// LeftNavigation
			// 
			this.LeftNavigation.ButtonActiveGradientHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(225)))), ((int)(((byte)(155)))));
			this.LeftNavigation.ButtonActiveGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
			this.LeftNavigation.ButtonBorderColor = System.Drawing.SystemColors.MenuHighlight;
			this.LeftNavigation.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.LeftNavigation.ButtonForeColor = System.Drawing.SystemColors.ControlText;
			this.LeftNavigation.ButtonGradientHighColor = System.Drawing.SystemColors.ButtonHighlight;
			this.LeftNavigation.ButtonGradientLowColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.LeftNavigation.ButtonHighlightGradientHighColor = System.Drawing.Color.White;
			this.LeftNavigation.ButtonHighlightGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
			this.LeftNavigation.CaptionBorderColor = System.Drawing.SystemColors.ActiveCaption;
			this.LeftNavigation.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
			this.LeftNavigation.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.LeftNavigation.CaptionGradientHighColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.LeftNavigation.CaptionGradientLowColor = System.Drawing.SystemColors.ActiveCaption;
			this.LeftNavigation.Controls.Add(this.LeftNavigation_Location);
			this.LeftNavigation.Controls.Add(this.LeftNavigation_QuickFind);
			this.LeftNavigation.Controls.Add(this.LeftNavigation_Markers);
			this.LeftNavigation.Controls.Add(this.LeftNavigation_Distance);
			this.LeftNavigation.Cursor = System.Windows.Forms.Cursors.Default;
			resources.ApplyResources(this.LeftNavigation, "LeftNavigation");
			this.LeftNavigation.FooterGradientHighColor = System.Drawing.SystemColors.ButtonHighlight;
			this.LeftNavigation.FooterGradientLowColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.LeftNavigation.FooterHeight = 30;
			this.LeftNavigation.FooterHighlightGradientHighColor = System.Drawing.Color.White;
			this.LeftNavigation.FooterHighlightGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
			this.LeftNavigation.Name = "LeftNavigation";
			this.LeftNavigation.NavigationPages.AddRange(new Ascend.Windows.Forms.NavigationPanePage[] {
            this.LeftNavigation_Location,
            this.LeftNavigation_QuickFind,
            this.LeftNavigation_Markers,
            this.LeftNavigation_Distance});
			this.LeftNavigation.VisibleButtonCount = 0;
			// 
			// LeftNavigation_Location
			// 
			this.LeftNavigation_Location.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(225)))), ((int)(((byte)(155)))));
			this.LeftNavigation_Location.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
			resources.ApplyResources(this.LeftNavigation_Location, "LeftNavigation_Location");
			this.LeftNavigation_Location.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.LeftNavigation_Location.ButtonForeColor = System.Drawing.SystemColors.ControlText;
			this.LeftNavigation_Location.Controls.Add(this.locationControl1);
			this.LeftNavigation_Location.GradientHighColor = System.Drawing.SystemColors.ButtonHighlight;
			this.LeftNavigation_Location.GradientLowColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.LeftNavigation_Location.HighlightGradientHighColor = System.Drawing.Color.White;
			this.LeftNavigation_Location.HighlightGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
			this.LeftNavigation_Location.Image = ((System.Drawing.Image)(resources.GetObject("LeftNavigation_Location.Image")));
			this.LeftNavigation_Location.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LeftNavigation_Location.ImageFooter = null;
			this.LeftNavigation_Location.ImageIndex = -1;
			this.LeftNavigation_Location.ImageIndexFooter = -1;
			this.LeftNavigation_Location.ImageKey = "";
			this.LeftNavigation_Location.ImageKeyFooter = "";
			this.LeftNavigation_Location.ImageList = null;
			this.LeftNavigation_Location.ImageListFooter = null;
			this.LeftNavigation_Location.Key = "LeftNavigation_Location";
			this.LeftNavigation_Location.Name = "LeftNavigation_Location";
			this.LeftNavigation_Location.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LeftNavigation_Location.ToolTipText = null;
			// 
			// locationControl1
			// 
			resources.ApplyResources(this.locationControl1, "locationControl1");
			this.locationControl1.BackColor = System.Drawing.Color.Transparent;
			this.locationControl1.Name = "locationControl1";
			// 
			// LeftNavigation_QuickFind
			// 
			this.LeftNavigation_QuickFind.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(225)))), ((int)(((byte)(155)))));
			this.LeftNavigation_QuickFind.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
			resources.ApplyResources(this.LeftNavigation_QuickFind, "LeftNavigation_QuickFind");
			this.LeftNavigation_QuickFind.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.LeftNavigation_QuickFind.ButtonForeColor = System.Drawing.SystemColors.ControlText;
			this.LeftNavigation_QuickFind.Controls.Add(this.detailsControl1);
			this.LeftNavigation_QuickFind.GradientHighColor = System.Drawing.SystemColors.ButtonHighlight;
			this.LeftNavigation_QuickFind.GradientLowColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.LeftNavigation_QuickFind.HighlightGradientHighColor = System.Drawing.Color.White;
			this.LeftNavigation_QuickFind.HighlightGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
			this.LeftNavigation_QuickFind.Image = ((System.Drawing.Image)(resources.GetObject("LeftNavigation_QuickFind.Image")));
			this.LeftNavigation_QuickFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LeftNavigation_QuickFind.ImageFooter = null;
			this.LeftNavigation_QuickFind.ImageIndex = -1;
			this.LeftNavigation_QuickFind.ImageIndexFooter = -1;
			this.LeftNavigation_QuickFind.ImageKey = "";
			this.LeftNavigation_QuickFind.ImageKeyFooter = "";
			this.LeftNavigation_QuickFind.ImageList = null;
			this.LeftNavigation_QuickFind.ImageListFooter = null;
			this.LeftNavigation_QuickFind.Key = "LeftNavigation_QuickFind";
			this.LeftNavigation_QuickFind.Name = "LeftNavigation_QuickFind";
			this.LeftNavigation_QuickFind.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LeftNavigation_QuickFind.ToolTipText = null;
			// 
			// detailsControl1
			// 
			resources.ApplyResources(this.detailsControl1, "detailsControl1");
			this.detailsControl1.BackColor = System.Drawing.Color.Transparent;
			this.detailsControl1.Name = "detailsControl1";
			// 
			// LeftNavigation_Markers
			// 
			this.LeftNavigation_Markers.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(225)))), ((int)(((byte)(155)))));
			this.LeftNavigation_Markers.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
			resources.ApplyResources(this.LeftNavigation_Markers, "LeftNavigation_Markers");
			this.LeftNavigation_Markers.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.LeftNavigation_Markers.ButtonForeColor = System.Drawing.SystemColors.ControlText;
			this.LeftNavigation_Markers.Controls.Add(this.markersContainerControl1);
			this.LeftNavigation_Markers.GradientHighColor = System.Drawing.SystemColors.ButtonHighlight;
			this.LeftNavigation_Markers.GradientLowColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.LeftNavigation_Markers.HighlightGradientHighColor = System.Drawing.Color.White;
			this.LeftNavigation_Markers.HighlightGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
			this.LeftNavigation_Markers.Image = ((System.Drawing.Image)(resources.GetObject("LeftNavigation_Markers.Image")));
			this.LeftNavigation_Markers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LeftNavigation_Markers.ImageFooter = null;
			this.LeftNavigation_Markers.ImageIndex = -1;
			this.LeftNavigation_Markers.ImageIndexFooter = -1;
			this.LeftNavigation_Markers.ImageKey = "";
			this.LeftNavigation_Markers.ImageKeyFooter = "";
			this.LeftNavigation_Markers.ImageList = null;
			this.LeftNavigation_Markers.ImageListFooter = null;
			this.LeftNavigation_Markers.Key = "LeftNavigation_Markers";
			this.LeftNavigation_Markers.Name = "LeftNavigation_Markers";
			this.LeftNavigation_Markers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LeftNavigation_Markers.ToolTipText = null;
			// 
			// markersContainerControl1
			// 
			resources.ApplyResources(this.markersContainerControl1, "markersContainerControl1");
			this.markersContainerControl1.BackColor = System.Drawing.Color.Transparent;
			this.markersContainerControl1.Name = "markersContainerControl1";
			// 
			// LeftNavigation_Distance
			// 
			this.LeftNavigation_Distance.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(225)))), ((int)(((byte)(155)))));
			this.LeftNavigation_Distance.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
			resources.ApplyResources(this.LeftNavigation_Distance, "LeftNavigation_Distance");
			this.LeftNavigation_Distance.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.LeftNavigation_Distance.ButtonForeColor = System.Drawing.SystemColors.ControlText;
			this.LeftNavigation_Distance.Controls.Add(this._attackPlan);
			this.LeftNavigation_Distance.GradientHighColor = System.Drawing.SystemColors.ButtonHighlight;
			this.LeftNavigation_Distance.GradientLowColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.LeftNavigation_Distance.HighlightGradientHighColor = System.Drawing.Color.White;
			this.LeftNavigation_Distance.HighlightGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
			this.LeftNavigation_Distance.Image = ((System.Drawing.Image)(resources.GetObject("LeftNavigation_Distance.Image")));
			this.LeftNavigation_Distance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LeftNavigation_Distance.ImageFooter = null;
			this.LeftNavigation_Distance.ImageIndex = -1;
			this.LeftNavigation_Distance.ImageIndexFooter = -1;
			this.LeftNavigation_Distance.ImageKey = "";
			this.LeftNavigation_Distance.ImageKeyFooter = "";
			this.LeftNavigation_Distance.ImageList = null;
			this.LeftNavigation_Distance.ImageListFooter = null;
			this.LeftNavigation_Distance.Key = "LeftNavigation_Distance";
			this.LeftNavigation_Distance.Name = "LeftNavigation_Distance";
			this.LeftNavigation_Distance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LeftNavigation_Distance.ToolTipText = null;
			// 
			// _attackPlan
			// 
			resources.ApplyResources(this._attackPlan, "_attackPlan");
			this._attackPlan.Name = "_attackPlan";
			// 
			// Tabs
			// 
			resources.ApplyResources(this.Tabs, "Tabs");
			this.Tabs.InputFocusTab = this.TabsMap;
			this.Tabs.Name = "Tabs";
			this.Tabs.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.TabsMap,
            this.TabsBrowser,
            this.TabsPolygon,
            this.TabsMonitoring});
			// 
			// TabsMap
			// 
			this.TabsMap.Controls.Add(this.Map);
			resources.ApplyResources(this.TabsMap, "TabsMap");
			this.TabsMap.Key = "Map";
			this.TabsMap.Name = "TabsMap";
			this.TabsMap.TabStop = true;
			// 
			// Map
			// 
			this.Map.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.Map, "Map");
			this.Map.Name = "Map";
			// 
			// TabsBrowser
			// 
			this.TabsBrowser.Controls.Add(this.browserControl1);
			resources.ApplyResources(this.TabsBrowser, "TabsBrowser");
			this.TabsBrowser.Key = "TWStats";
			this.TabsBrowser.Name = "TabsBrowser";
			this.TabsBrowser.TabStop = true;
			// 
			// browserControl1
			// 
			this.browserControl1.ActiveVillage = 0;
			resources.ApplyResources(this.browserControl1, "browserControl1");
			this.browserControl1.GameBrowser = false;
			this.browserControl1.Name = "browserControl1";
			// 
			// TabsPolygon
			// 
			this.TabsPolygon.Controls.Add(this.Polygon);
			resources.ApplyResources(this.TabsPolygon, "TabsPolygon");
			this.TabsPolygon.Key = "Polygon";
			this.TabsPolygon.Name = "TabsPolygon";
			this.TabsPolygon.TabStop = true;
			// 
			// Polygon
			// 
			this.Polygon.BackColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.Polygon, "Polygon");
			this.Polygon.Name = "Polygon";
			// 
			// TabsMonitoring
			// 
			this.TabsMonitoring.Controls.Add(this.monitoringControl1);
			resources.ApplyResources(this.TabsMonitoring, "TabsMonitoring");
			this.TabsMonitoring.Key = "Monitoring";
			this.TabsMonitoring.Name = "TabsMonitoring";
			this.TabsMonitoring.TabStop = true;
			// 
			// monitoringControl1
			// 
			this.monitoringControl1.BackColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.monitoringControl1, "monitoringControl1");
			this.monitoringControl1.Name = "monitoringControl1";
			// 
			// FormToolbarContainer
			// 
			// 
			// FormToolbarContainer.ContentPanel
			// 
			this.FormToolbarContainer.ContentPanel.Controls.Add(this.FormSplitter);
			this.FormToolbarContainer.ContentPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			resources.ApplyResources(this.FormToolbarContainer.ContentPanel, "FormToolbarContainer.ContentPanel");
			resources.ApplyResources(this.FormToolbarContainer, "FormToolbarContainer");
			this.FormToolbarContainer.Name = "FormToolbarContainer";
			// 
			// FormToolbarContainer.TopToolStripPanel
			// 
			this.FormToolbarContainer.TopToolStripPanel.Controls.Add(this.ToolStrip);
			// 
			// ToolStrip
			// 
			resources.ApplyResources(this.ToolStrip, "ToolStrip");
			this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolstripButtonCreateWorld,
            this.ToolStripOpen,
            this.ToolStripDownload,
            this.toolStripSeparator6,
            this.ToolStripSettings,
            this.ToolStripSave,
            this.toolStripSeparator4,
            this.ToolStripHome,
            this.ToolStripActiveRectangle,
            this.ToolStripDraw,
            this.toolStripSeparator5,
            this.ToolStripIconDisplay,
            this.ToolStripShapeDisplay,
            this.toolStripSeparator3,
            this.ToolStripDefaultManipulator,
            this.ToolStripPolygonManipulator,
            this.ToolStripAttackManipulator,
            this.ToolStripChurchManipulator,
            this.toolStripSeparator,
            this.toolStripLocationChangerControl1,
            this.toolStripSeparator7,
            this.ToolStripProgramSettings,
            this.ToolStripAbout});
			this.ToolStrip.Name = "ToolStrip";
			// 
			// ToolstripButtonCreateWorld
			// 
			this.ToolstripButtonCreateWorld.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.ToolstripButtonCreateWorld, "ToolstripButtonCreateWorld");
			this.ToolstripButtonCreateWorld.Name = "ToolstripButtonCreateWorld";
			this.ToolstripButtonCreateWorld.Click += new System.EventHandler(this.ToolstripButtonCreateWorld_Click);
			// 
			// ToolStripOpen
			// 
			this.ToolStripOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.ToolStripOpen, "ToolStripOpen");
			this.ToolStripOpen.Name = "ToolStripOpen";
			this.ToolStripOpen.Click += new System.EventHandler(this.ToolStripOpen_Click);
			// 
			// ToolStripDownload
			// 
			this.ToolStripDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.ToolStripDownload, "ToolStripDownload");
			this.ToolStripDownload.Name = "ToolStripDownload";
			this.ToolStripDownload.Click += new System.EventHandler(this.MenuFileWorldDownload_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
			// 
			// ToolStripSettings
			// 
			this.ToolStripSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			resources.ApplyResources(this.ToolStripSettings, "ToolStripSettings");
			this.ToolStripSettings.Name = "ToolStripSettings";
			// 
			// ToolStripSave
			// 
			this.ToolStripSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.ToolStripSave, "ToolStripSave");
			this.ToolStripSave.Name = "ToolStripSave";
			this.ToolStripSave.Click += new System.EventHandler(this.ToolStripSave_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
			// 
			// ToolStripHome
			// 
			this.ToolStripHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripHome.Image = global::TribalWars.Properties.Resources.Home2;
			resources.ApplyResources(this.ToolStripHome, "ToolStripHome");
			this.ToolStripHome.Name = "ToolStripHome";
			this.ToolStripHome.Click += new System.EventHandler(this.ToolStripHome_Click);
			// 
			// ToolStripActiveRectangle
			// 
			this.ToolStripActiveRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.ToolStripActiveRectangle, "ToolStripActiveRectangle");
			this.ToolStripActiveRectangle.Name = "ToolStripActiveRectangle";
			this.ToolStripActiveRectangle.Click += new System.EventHandler(this.ToolStripActiveRectangle_Click);
			// 
			// ToolStripDraw
			// 
			this.ToolStripDraw.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.ToolStripDraw, "ToolStripDraw");
			this.ToolStripDraw.Name = "ToolStripDraw";
			this.ToolStripDraw.Click += new System.EventHandler(this.ToolStripDraw_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
			// 
			// ToolStripIconDisplay
			// 
			this.ToolStripIconDisplay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripIconDisplay.Image = global::TribalWars.Properties.Resources.Village;
			resources.ApplyResources(this.ToolStripIconDisplay, "ToolStripIconDisplay");
			this.ToolStripIconDisplay.Name = "ToolStripIconDisplay";
			this.ToolStripIconDisplay.Tag = "ChangeHighlight";
			this.ToolStripIconDisplay.Click += new System.EventHandler(this.ToolStripIconDisplay_Click);
			// 
			// ToolStripShapeDisplay
			// 
			this.ToolStripShapeDisplay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripShapeDisplay.Image = global::TribalWars.Properties.Resources.shapes;
			resources.ApplyResources(this.ToolStripShapeDisplay, "ToolStripShapeDisplay");
			this.ToolStripShapeDisplay.Name = "ToolStripShapeDisplay";
			this.ToolStripShapeDisplay.Tag = "ChangeHighlight";
			this.ToolStripShapeDisplay.Click += new System.EventHandler(this.ToolStripShapeDisplay_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
			// 
			// ToolStripDefaultManipulator
			// 
			this.ToolStripDefaultManipulator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.ToolStripDefaultManipulator, "ToolStripDefaultManipulator");
			this.ToolStripDefaultManipulator.Name = "ToolStripDefaultManipulator";
			this.ToolStripDefaultManipulator.Tag = "ChangeHighlight";
			this.ToolStripDefaultManipulator.Click += new System.EventHandler(this.ToolStripDefaultManipulator_Click);
			// 
			// ToolStripPolygonManipulator
			// 
			this.ToolStripPolygonManipulator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.ToolStripPolygonManipulator, "ToolStripPolygonManipulator");
			this.ToolStripPolygonManipulator.Name = "ToolStripPolygonManipulator";
			this.ToolStripPolygonManipulator.Tag = "ChangeHighlight";
			this.ToolStripPolygonManipulator.Click += new System.EventHandler(this.ToolStripPolygonManipulator_Click);
			// 
			// ToolStripAttackManipulator
			// 
			this.ToolStripAttackManipulator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.ToolStripAttackManipulator, "ToolStripAttackManipulator");
			this.ToolStripAttackManipulator.Name = "ToolStripAttackManipulator";
			this.ToolStripAttackManipulator.Tag = "ChangeHighlight";
			this.ToolStripAttackManipulator.Click += new System.EventHandler(this.ToolStripAttackManipulator_Click);
			// 
			// ToolStripChurchManipulator
			// 
			this.ToolStripChurchManipulator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.ToolStripChurchManipulator, "ToolStripChurchManipulator");
			this.ToolStripChurchManipulator.Name = "ToolStripChurchManipulator";
			this.ToolStripChurchManipulator.Click += new System.EventHandler(this.ToolStripChurchManipulator_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
			// 
			// toolStripLocationChangerControl1
			// 
			resources.ApplyResources(this.toolStripLocationChangerControl1, "toolStripLocationChangerControl1");
			this.toolStripLocationChangerControl1.BackColor = System.Drawing.Color.Transparent;
			this.toolStripLocationChangerControl1.Name = "toolStripLocationChangerControl1";
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
			// 
			// ToolStripProgramSettings
			// 
			this.ToolStripProgramSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.ToolStripProgramSettings, "ToolStripProgramSettings");
			this.ToolStripProgramSettings.Name = "ToolStripProgramSettings";
			this.ToolStripProgramSettings.Click += new System.EventHandler(this.ToolStripProgramSettings_Click);
			// 
			// ToolStripAbout
			// 
			this.ToolStripAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.ToolStripAbout, "ToolStripAbout");
			this.ToolStripAbout.Name = "ToolStripAbout";
			this.ToolStripAbout.Click += new System.EventHandler(this.MenuHelpAbout_Click);
			// 
			// Status
			// 
			this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressBar,
            this.StatusMessage,
            this.StatusDataTime,
            this.StatusXY,
            this.StatusVillage,
            this.StatusPlayer,
            this.StatusTribe,
            this.StatusSettings,
            this.StatusWorld,
            this.StatusServerTime,
            this.toolStripStatusLabel5});
			resources.ApplyResources(this.Status, "Status");
			this.Status.Name = "Status";
			this.Status.ShowItemToolTips = true;
			// 
			// ProgressBar
			// 
			this.ProgressBar.Name = "ProgressBar";
			resources.ApplyResources(this.ProgressBar, "ProgressBar");
			// 
			// StatusMessage
			// 
			this.StatusMessage.Name = "StatusMessage";
			resources.ApplyResources(this.StatusMessage, "StatusMessage");
			this.StatusMessage.Spring = true;
			// 
			// StatusDataTime
			// 
			resources.ApplyResources(this.StatusDataTime, "StatusDataTime");
			this.StatusDataTime.AutoToolTip = true;
			this.StatusDataTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.StatusDataTime.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StatusDataTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.StatusDataTime.Name = "StatusDataTime";
			// 
			// StatusXY
			// 
			resources.ApplyResources(this.StatusXY, "StatusXY");
			this.StatusXY.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.StatusXY.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StatusXY.Name = "StatusXY";
			// 
			// StatusVillage
			// 
			resources.ApplyResources(this.StatusVillage, "StatusVillage");
			this.StatusVillage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.StatusVillage.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StatusVillage.Name = "StatusVillage";
			// 
			// StatusPlayer
			// 
			resources.ApplyResources(this.StatusPlayer, "StatusPlayer");
			this.StatusPlayer.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.StatusPlayer.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StatusPlayer.Name = "StatusPlayer";
			// 
			// StatusTribe
			// 
			resources.ApplyResources(this.StatusTribe, "StatusTribe");
			this.StatusTribe.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.StatusTribe.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StatusTribe.Name = "StatusTribe";
			// 
			// StatusSettings
			// 
			this.StatusSettings.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.StatusSettings.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StatusSettings.Name = "StatusSettings";
			this.StatusSettings.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			resources.ApplyResources(this.StatusSettings, "StatusSettings");
			// 
			// StatusWorld
			// 
			this.StatusWorld.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.StatusWorld.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StatusWorld.Name = "StatusWorld";
			resources.ApplyResources(this.StatusWorld, "StatusWorld");
			// 
			// StatusServerTime
			// 
			resources.ApplyResources(this.StatusServerTime, "StatusServerTime");
			this.StatusServerTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.StatusServerTime.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.StatusServerTime.Name = "StatusServerTime";
			this.StatusServerTime.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			// 
			// toolStripStatusLabel5
			// 
			resources.ApplyResources(this.toolStripStatusLabel5, "toolStripStatusLabel5");
			this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
			// 
			// ServerTimeTimer
			// 
			this.ServerTimeTimer.Enabled = true;
			this.ServerTimeTimer.Interval = 1000;
			this.ServerTimeTimer.Tick += new System.EventHandler(this.ServerTimeTimer_Tick);
			// 
			// MenuBar
			// 
			this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuToolstrip,
            this.mapToolStripMenuItem,
            this.windowsToolStripMenuItem,
            this.helpToolStripMenuItem});
			resources.ApplyResources(this.MenuBar, "MenuBar");
			this.MenuBar.Name = "MenuBar";
			// 
			// MenuToolstrip
			// 
			this.MenuToolstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFileNew,
            this.MenuFileLoadWorld,
            this.toolStripMenuItem4,
            this.MenuFileWorldDownload,
            this.toolStripSeparator1,
            this.MenuFileSaveSettings,
            this.MenuFileSaveSettingsAs,
            this.toolStripSeparator2,
            this.MenuFileSetActivePlayer,
            this.MenuFileSynchronizeTime,
            this.setLanguageToolStripMenuItem,
            this.toolStripMenuItem1,
            this.MenuFileExit});
			this.MenuToolstrip.Name = "MenuToolstrip";
			resources.ApplyResources(this.MenuToolstrip, "MenuToolstrip");
			// 
			// MenuFileNew
			// 
			resources.ApplyResources(this.MenuFileNew, "MenuFileNew");
			this.MenuFileNew.Name = "MenuFileNew";
			this.MenuFileNew.Click += new System.EventHandler(this.MenuFileNew_Click);
			// 
			// MenuFileLoadWorld
			// 
			resources.ApplyResources(this.MenuFileLoadWorld, "MenuFileLoadWorld");
			this.MenuFileLoadWorld.Name = "MenuFileLoadWorld";
			this.MenuFileLoadWorld.Click += new System.EventHandler(this.MenuFileLoadWorld_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
			// 
			// MenuFileWorldDownload
			// 
			resources.ApplyResources(this.MenuFileWorldDownload, "MenuFileWorldDownload");
			this.MenuFileWorldDownload.Name = "MenuFileWorldDownload";
			this.MenuFileWorldDownload.Click += new System.EventHandler(this.MenuFileWorldDownload_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			// 
			// MenuFileSaveSettings
			// 
			resources.ApplyResources(this.MenuFileSaveSettings, "MenuFileSaveSettings");
			this.MenuFileSaveSettings.Name = "MenuFileSaveSettings";
			this.MenuFileSaveSettings.Click += new System.EventHandler(this.MenuFileSaveSettings_Click);
			// 
			// MenuFileSaveSettingsAs
			// 
			this.MenuFileSaveSettingsAs.Name = "MenuFileSaveSettingsAs";
			resources.ApplyResources(this.MenuFileSaveSettingsAs, "MenuFileSaveSettingsAs");
			this.MenuFileSaveSettingsAs.Click += new System.EventHandler(this.MenuFileSaveSettingsAs_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			// 
			// MenuFileSetActivePlayer
			// 
			this.MenuFileSetActivePlayer.Image = global::TribalWars.Properties.Resources.Player;
			this.MenuFileSetActivePlayer.Name = "MenuFileSetActivePlayer";
			resources.ApplyResources(this.MenuFileSetActivePlayer, "MenuFileSetActivePlayer");
			this.MenuFileSetActivePlayer.Click += new System.EventHandler(this.MenuFileSetActivePlayer_Click);
			// 
			// MenuFileSynchronizeTime
			// 
			this.MenuFileSynchronizeTime.Name = "MenuFileSynchronizeTime";
			resources.ApplyResources(this.MenuFileSynchronizeTime, "MenuFileSynchronizeTime");
			this.MenuFileSynchronizeTime.Click += new System.EventHandler(this.MenuFileSynchronizeTime_Click);
			// 
			// setLanguageToolStripMenuItem
			// 
			this.setLanguageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetLanguage_English,
            this.SetLanguage_Dutch});
			this.setLanguageToolStripMenuItem.Name = "setLanguageToolStripMenuItem";
			resources.ApplyResources(this.setLanguageToolStripMenuItem, "setLanguageToolStripMenuItem");
			// 
			// SetLanguage_English
			// 
			this.SetLanguage_English.Name = "SetLanguage_English";
			resources.ApplyResources(this.SetLanguage_English, "SetLanguage_English");
			this.SetLanguage_English.Click += new System.EventHandler(this.SetLanguage_English_Click);
			// 
			// SetLanguage_Dutch
			// 
			this.SetLanguage_Dutch.Name = "SetLanguage_Dutch";
			resources.ApplyResources(this.SetLanguage_Dutch, "SetLanguage_Dutch");
			this.SetLanguage_Dutch.Click += new System.EventHandler(this.SetLanguage_Dutch_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			// 
			// MenuFileExit
			// 
			this.MenuFileExit.Name = "MenuFileExit";
			resources.ApplyResources(this.MenuFileExit, "MenuFileExit");
			this.MenuFileExit.Click += new System.EventHandler(this.MenuFileExit_Click);
			// 
			// mapToolStripMenuItem
			// 
			this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuMapMonitoringArea,
            this.MenuMapSetHomeLocation,
            this.toolStripMenuItem8,
            this.MenuMapIconDisplay,
            this.MenuMapShapeDisplay,
            this.toolStripMenuItem6,
            this.MenuMapInteractionDefault,
            this.MenuMapInteractionPolygon,
            this.MenuMapInteractionPlanAttacks,
            this.toolStripMenuItem5,
            this.MenuMapSelectPane0,
            this.MenuMapSelectPane1,
            this.MenuMapSelectPane2,
            this.MenuMapSelectPane3,
            this.toolStripMenuItem2,
            this.MenuMapScreenshot,
            this.MenuMapSeeScreenshots});
			this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
			resources.ApplyResources(this.mapToolStripMenuItem, "mapToolStripMenuItem");
			// 
			// MenuMapMonitoringArea
			// 
			resources.ApplyResources(this.MenuMapMonitoringArea, "MenuMapMonitoringArea");
			this.MenuMapMonitoringArea.Name = "MenuMapMonitoringArea";
			this.MenuMapMonitoringArea.Click += new System.EventHandler(this.ToolStripActiveRectangle_Click);
			// 
			// MenuMapSetHomeLocation
			// 
			this.MenuMapSetHomeLocation.Image = global::TribalWars.Properties.Resources.Home2;
			resources.ApplyResources(this.MenuMapSetHomeLocation, "MenuMapSetHomeLocation");
			this.MenuMapSetHomeLocation.Name = "MenuMapSetHomeLocation";
			this.MenuMapSetHomeLocation.Click += new System.EventHandler(this.MenuMapSetHomeLocation_Click);
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
			// 
			// MenuMapIconDisplay
			// 
			this.MenuMapIconDisplay.Image = global::TribalWars.Properties.Resources.Village;
			this.MenuMapIconDisplay.Name = "MenuMapIconDisplay";
			resources.ApplyResources(this.MenuMapIconDisplay, "MenuMapIconDisplay");
			this.MenuMapIconDisplay.Click += new System.EventHandler(this.ToolStripIconDisplay_Click);
			// 
			// MenuMapShapeDisplay
			// 
			this.MenuMapShapeDisplay.Image = global::TribalWars.Properties.Resources.shapes;
			this.MenuMapShapeDisplay.Name = "MenuMapShapeDisplay";
			resources.ApplyResources(this.MenuMapShapeDisplay, "MenuMapShapeDisplay");
			this.MenuMapShapeDisplay.Click += new System.EventHandler(this.ToolStripShapeDisplay_Click);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
			// 
			// MenuMapInteractionDefault
			// 
			this.MenuMapInteractionDefault.Checked = true;
			this.MenuMapInteractionDefault.CheckState = System.Windows.Forms.CheckState.Checked;
			this.MenuMapInteractionDefault.Image = global::TribalWars.Properties.Resources.DefaultInteraction;
			this.MenuMapInteractionDefault.Name = "MenuMapInteractionDefault";
			resources.ApplyResources(this.MenuMapInteractionDefault, "MenuMapInteractionDefault");
			this.MenuMapInteractionDefault.Click += new System.EventHandler(this.MenuMapInteractionDefault_Click);
			// 
			// MenuMapInteractionPolygon
			// 
			this.MenuMapInteractionPolygon.Image = global::TribalWars.Properties.Resources.Polygon;
			this.MenuMapInteractionPolygon.Name = "MenuMapInteractionPolygon";
			resources.ApplyResources(this.MenuMapInteractionPolygon, "MenuMapInteractionPolygon");
			this.MenuMapInteractionPolygon.Click += new System.EventHandler(this.MenuMapInteractionPolygon_Click);
			// 
			// MenuMapInteractionPlanAttacks
			// 
			this.MenuMapInteractionPlanAttacks.Image = global::TribalWars.Properties.Resources.barracks;
			this.MenuMapInteractionPlanAttacks.Name = "MenuMapInteractionPlanAttacks";
			resources.ApplyResources(this.MenuMapInteractionPlanAttacks, "MenuMapInteractionPlanAttacks");
			this.MenuMapInteractionPlanAttacks.Click += new System.EventHandler(this.MenuMapInteractionPlanAttacks_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
			// 
			// MenuMapSelectPane0
			// 
			resources.ApplyResources(this.MenuMapSelectPane0, "MenuMapSelectPane0");
			this.MenuMapSelectPane0.Name = "MenuMapSelectPane0";
			this.MenuMapSelectPane0.Tag = "0";
			this.MenuMapSelectPane0.Click += new System.EventHandler(this.MenuMapSelectPane_Click);
			// 
			// MenuMapSelectPane1
			// 
			resources.ApplyResources(this.MenuMapSelectPane1, "MenuMapSelectPane1");
			this.MenuMapSelectPane1.Name = "MenuMapSelectPane1";
			this.MenuMapSelectPane1.Tag = "1";
			this.MenuMapSelectPane1.Click += new System.EventHandler(this.MenuMapSelectPane_Click);
			// 
			// MenuMapSelectPane2
			// 
			resources.ApplyResources(this.MenuMapSelectPane2, "MenuMapSelectPane2");
			this.MenuMapSelectPane2.Name = "MenuMapSelectPane2";
			this.MenuMapSelectPane2.Tag = "2";
			this.MenuMapSelectPane2.Click += new System.EventHandler(this.MenuMapSelectPane_Click);
			// 
			// MenuMapSelectPane3
			// 
			resources.ApplyResources(this.MenuMapSelectPane3, "MenuMapSelectPane3");
			this.MenuMapSelectPane3.Name = "MenuMapSelectPane3";
			this.MenuMapSelectPane3.Tag = "3";
			this.MenuMapSelectPane3.Click += new System.EventHandler(this.MenuMapSelectPane_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
			// 
			// MenuMapScreenshot
			// 
			this.MenuMapScreenshot.Name = "MenuMapScreenshot";
			resources.ApplyResources(this.MenuMapScreenshot, "MenuMapScreenshot");
			this.MenuMapScreenshot.Click += new System.EventHandler(this.MenuMapScreenshot_Click);
			// 
			// MenuMapSeeScreenshots
			// 
			this.MenuMapSeeScreenshots.Name = "MenuMapSeeScreenshots";
			resources.ApplyResources(this.MenuMapSeeScreenshots, "MenuMapSeeScreenshots");
			this.MenuMapSeeScreenshots.Click += new System.EventHandler(this.MenuMapSeeScreenshots_Click);
			// 
			// windowsToolStripMenuItem
			// 
			this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuWindowsManageYourVillages,
            this.MenuWindowsImportVillageCoordinates,
            this.toolStripMenuItem7,
            this.MenuWindowsManageYourAttackersPool,
            this.otherToolStripMenuItem});
			this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
			resources.ApplyResources(this.windowsToolStripMenuItem, "windowsToolStripMenuItem");
			// 
			// MenuWindowsManageYourVillages
			// 
			this.MenuWindowsManageYourVillages.Name = "MenuWindowsManageYourVillages";
			resources.ApplyResources(this.MenuWindowsManageYourVillages, "MenuWindowsManageYourVillages");
			this.MenuWindowsManageYourVillages.Click += new System.EventHandler(this.MenuWindowsManageYourVillages_Click);
			// 
			// MenuWindowsImportVillageCoordinates
			// 
			this.MenuWindowsImportVillageCoordinates.Name = "MenuWindowsImportVillageCoordinates";
			resources.ApplyResources(this.MenuWindowsImportVillageCoordinates, "MenuWindowsImportVillageCoordinates");
			this.MenuWindowsImportVillageCoordinates.Click += new System.EventHandler(this.MenuWindowsImportVillageCoordinates_Click);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
			// 
			// MenuWindowsManageYourAttackersPool
			// 
			this.MenuWindowsManageYourAttackersPool.Image = global::TribalWars.Properties.Resources.star;
			this.MenuWindowsManageYourAttackersPool.Name = "MenuWindowsManageYourAttackersPool";
			resources.ApplyResources(this.MenuWindowsManageYourAttackersPool, "MenuWindowsManageYourAttackersPool");
			this.MenuWindowsManageYourAttackersPool.Click += new System.EventHandler(this.MenuWindowsManageYourAttackersPool_Click);
			// 
			// otherToolStripMenuItem
			// 
			this.otherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuWindowsAddTimes});
			this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
			resources.ApplyResources(this.otherToolStripMenuItem, "otherToolStripMenuItem");
			// 
			// MenuWindowsAddTimes
			// 
			this.MenuWindowsAddTimes.Name = "MenuWindowsAddTimes";
			resources.ApplyResources(this.MenuWindowsAddTimes, "MenuWindowsAddTimes");
			this.MenuWindowsAddTimes.Click += new System.EventHandler(this.MenuWindowsAddTimes_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuHelpReportBug,
            this.MenuHelpAbout});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
			// 
			// MenuHelpReportBug
			// 
			resources.ApplyResources(this.MenuHelpReportBug, "MenuHelpReportBug");
			this.MenuHelpReportBug.Name = "MenuHelpReportBug";
			this.MenuHelpReportBug.Click += new System.EventHandler(this.MenuHelpReportBug_Click);
			// 
			// MenuHelpAbout
			// 
			this.MenuHelpAbout.Name = "MenuHelpAbout";
			resources.ApplyResources(this.MenuHelpAbout, "MenuHelpAbout");
			this.MenuHelpAbout.Click += new System.EventHandler(this.MenuHelpAbout_Click);
			// 
			// BottomToolStripPanel
			// 
			resources.ApplyResources(this.BottomToolStripPanel, "BottomToolStripPanel");
			this.BottomToolStripPanel.Name = "BottomToolStripPanel";
			this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			// 
			// TopToolStripPanel
			// 
			resources.ApplyResources(this.TopToolStripPanel, "TopToolStripPanel");
			this.TopToolStripPanel.Name = "TopToolStripPanel";
			this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			// 
			// RightToolStripPanel
			// 
			resources.ApplyResources(this.RightToolStripPanel, "RightToolStripPanel");
			this.RightToolStripPanel.Name = "RightToolStripPanel";
			this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			// 
			// LeftToolStripPanel
			// 
			resources.ApplyResources(this.LeftToolStripPanel, "LeftToolStripPanel");
			this.LeftToolStripPanel.Name = "LeftToolStripPanel";
			this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			// 
			// ContentPanel
			// 
			this.ContentPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			resources.ApplyResources(this.ContentPanel, "ContentPanel");
			// 
			// VillageTooltip
			// 
			this.VillageTooltip.AutoPopDelay = 10000;
			this.VillageTooltip.InitialDelay = 500;
			this.VillageTooltip.IsBalloon = true;
			this.VillageTooltip.ReshowDelay = 100;
			this.VillageTooltip.ShowAlways = true;
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "sets";
			resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
			this.saveFileDialog1.RestoreDirectory = true;
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.FormToolbarContainer);
			this.Controls.Add(this.Status);
			this.Controls.Add(this.MenuBar);
			this.MainMenuStrip = this.MenuBar;
			this.Name = "MainForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.FormSplitter.Panel1.ResumeLayout(false);
			this.FormSplitter.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.FormSplitter)).EndInit();
			this.FormSplitter.ResumeLayout(false);
			this.LeftSplitter.Panel1.ResumeLayout(false);
			this.LeftSplitter.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.LeftSplitter)).EndInit();
			this.LeftSplitter.ResumeLayout(false);
			this.LeftNavigation.ResumeLayout(false);
			this.LeftNavigation_Location.ResumeLayout(false);
			this.LeftNavigation_QuickFind.ResumeLayout(false);
			this.LeftNavigation_Markers.ResumeLayout(false);
			this.LeftNavigation_Distance.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Tabs)).EndInit();
			this.Tabs.ResumeLayout(false);
			this.TabsMap.ResumeLayout(false);
			this.TabsBrowser.ResumeLayout(false);
			this.TabsPolygon.ResumeLayout(false);
			this.TabsMonitoring.ResumeLayout(false);
			this.FormToolbarContainer.ContentPanel.ResumeLayout(false);
			this.FormToolbarContainer.TopToolStripPanel.ResumeLayout(false);
			this.FormToolbarContainer.TopToolStripPanel.PerformLayout();
			this.FormToolbarContainer.ResumeLayout(false);
			this.FormToolbarContainer.PerformLayout();
			this.ToolStrip.ResumeLayout(false);
			this.ToolStrip.PerformLayout();
			this.Status.ResumeLayout(false);
			this.Status.PerformLayout();
			this.MenuBar.ResumeLayout(false);
			this.MenuBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ColorDialog PickColor;
        private System.Windows.Forms.StatusStrip Status;
        private System.Windows.Forms.Timer ServerTimeTimer;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.ToolTip GeneralTooltip;
        private System.Windows.Forms.ToolTip VillageTooltip;
        private System.Windows.Forms.MenuStrip MenuBar;
        internal System.Windows.Forms.ToolStripStatusLabel StatusMessage;
        private System.Windows.Forms.ToolStripStatusLabel StatusServerTime;
        internal System.Windows.Forms.ToolStripStatusLabel StatusXY;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripButton ToolStripOpen;
        private System.Windows.Forms.ToolStripButton ToolStripSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton ToolStripDraw;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.SplitContainer FormSplitter;
        private System.Windows.Forms.ToolStripContainer FormToolbarContainer;
        private System.Windows.Forms.ToolStripMenuItem MenuToolstrip;
        private System.Windows.Forms.ToolStripMenuItem MenuFileNew;
        private System.Windows.Forms.ToolStripMenuItem MenuFileLoadWorld;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSaveSettings;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSaveSettingsAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuFileExit;
        private Ascend.Windows.Forms.NavigationPanePage LeftNavigation_Markers;
        private Ascend.Windows.Forms.NavigationPanePage LeftNavigation_QuickFind;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuMapScreenshot;
        internal System.Windows.Forms.ToolStripStatusLabel StatusPlayer;
        internal System.Windows.Forms.ToolStripStatusLabel StatusTribe;
        internal System.Windows.Forms.ToolStripStatusLabel StatusVillage;
        private System.Windows.Forms.ToolStripDropDownButton ToolStripSettings;
        //private Controls.YouTreeold YouTree;
        private Ascend.Windows.Forms.NavigationPanePage LeftNavigation_Distance;
        private AttackPlanCollectionControl _attackPlan;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem MenuFileWorldDownload;
        private Ascend.Windows.Forms.NavigationPanePage LeftNavigation_Location;
        //private TribalWars.Controls.Accordeon.Location.LocationControl locationControl1;
        private MarkersControl markersContainerControl1;
        private MapControl Map;
        private PolygonControl Polygon;
        //private TribalWars.Controls.Accordeon.Details.DetailsControl QuickDetails;
        //private TribalWars.Controls.Main.Monitoring.MonitoringControl monitoringControl1;
        private BrowserControl browserControl1;
        private System.Windows.Forms.ToolStripButton ToolStripDownload;
        private System.Windows.Forms.ToolStripButton ToolStripHome;
        internal System.Windows.Forms.ToolStripStatusLabel StatusSettings;
        internal System.Windows.Forms.ToolStripStatusLabel StatusWorld;
        private Janus.Windows.UI.Tab.UITab Tabs;
        private Janus.Windows.UI.Tab.UITabPage TabsMap;
        private Janus.Windows.UI.Tab.UITabPage TabsBrowser;
        private Janus.Windows.UI.Tab.UITabPage TabsPolygon;
        private Janus.Windows.UI.Tab.UITabPage TabsMonitoring;
        private System.Windows.Forms.ToolStripButton ToolStripIconDisplay;
        private System.Windows.Forms.ToolStripButton ToolStripShapeDisplay;
        private MiniMapControl MiniMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton ToolStripDefaultManipulator;
        private System.Windows.Forms.ToolStripButton ToolStripPolygonManipulator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private Ascend.Windows.Forms.NavigationPane LeftNavigation;
        private DetailsControl detailsControl1;
        private LocationControl locationControl1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem MenuMapSeeScreenshots;
        private MonitoringControl monitoringControl1;
        private System.Windows.Forms.ToolStripButton ToolstripButtonCreateWorld;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton ToolStripActiveRectangle;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSynchronizeTime;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton ToolStripProgramSettings;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSetActivePlayer;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuHelpAbout;
        private System.Windows.Forms.ToolStripButton ToolStripAbout;
        private System.Windows.Forms.ToolStripMenuItem MenuMapSetHomeLocation;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripButton ToolStripAttackManipulator;
        private System.Windows.Forms.ToolStripMenuItem MenuMapIconDisplay;
        private System.Windows.Forms.ToolStripMenuItem MenuMapShapeDisplay;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem MenuMapSelectPane0;
        private System.Windows.Forms.ToolStripMenuItem MenuMapSelectPane1;
        private System.Windows.Forms.ToolStripMenuItem MenuMapSelectPane2;
        private System.Windows.Forms.ToolStripMenuItem MenuMapSelectPane3;
        private System.Windows.Forms.ToolStripMenuItem MenuHelpReportBug;
        private System.Windows.Forms.SplitContainer LeftSplitter;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuWindowsManageYourVillages;
        private System.Windows.Forms.ToolStripMenuItem MenuWindowsImportVillageCoordinates;
        private System.Windows.Forms.ToolStripButton ToolStripChurchManipulator;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem MenuMapInteractionDefault;
        private System.Windows.Forms.ToolStripMenuItem MenuMapInteractionPolygon;
        private System.Windows.Forms.ToolStripMenuItem MenuMapInteractionPlanAttacks;
        private System.Windows.Forms.ToolStripStatusLabel StatusDataTime;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuWindowsAddTimes;
        private System.Windows.Forms.ToolStripMenuItem MenuMapMonitoringArea;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private Controls.Common.ToolStripControlHostWrappers.ToolStripLocationChangerControl toolStripLocationChangerControl1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem MenuWindowsManageYourAttackersPool;
		private System.Windows.Forms.ToolStripMenuItem setLanguageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SetLanguage_English;
		private System.Windows.Forms.ToolStripMenuItem SetLanguage_Dutch;
    }
}

