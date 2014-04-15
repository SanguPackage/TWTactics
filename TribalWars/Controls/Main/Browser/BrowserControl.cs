using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using TribalWars.Data.Events;

namespace TribalWars.Controls.Main.Browser
{
    public partial class BrowserControl : UserControl
    {
        #region Constants
        private const string _activeVillagePattern = @"\.php\?village=(?<village>\d*)&";
        #endregion

        #region Fields
        private static Regex _activeVillageRegEx;

        private int _activeVillage;
        private bool _gameBrowser;
        private List<IBrowserParser> _parsers;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the WebBrowser control
        /// </summary>
        public WebBrowser WebBrowser
        {
            get { return Browser; }
        }

        /// <summary>
        /// Gets or sets the village we are currently logged into
        /// </summary>
        public int ActiveVillage
        {
            get { return _activeVillage; }
            set { _activeVillage = value; }
        }

        /// <summary>
        /// Gets the RegEx pattern for recognizing
        /// the currently active village
        /// </summary>
        public static string ActiveVillagePattern
        {
            get { return _activeVillagePattern; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this browser
        /// is used for playing a village
        /// </summary>
        public bool GameBrowser
        {
            get { return _gameBrowser; }
            set { _gameBrowser = value; }
        }
        #endregion

        #region Constructor
        public BrowserControl()
        {
            InitializeComponent();

            Browser.CanGoForwardChanged += new EventHandler(Browser_CanGoForwardChanged);
            Browser.CanGoBackChanged += new EventHandler(Browser_CanGoBackChanged);

            World.Default.EventPublisher.Browse += OnBrowseEvent;
            World.Default.EventPublisher.SettingsLoaded += OnSettingsLoaded;

            _activeVillageRegEx = new Regex(ActiveVillagePattern);
            _parsers = new List<IBrowserParser>();
        }
        #endregion

        #region EventHandlers
        private void OnBrowseEvent(object sender, BrowserEventArgs e)
        {
            if (!_gameBrowser)
            {
                switch (e.Destination)
                {
                    case DestinationEnum.TWStatsPlayer:
                        WebBrowser.Navigate(string.Format(World.Default.TWStats.Player, e.Arguments[0]));
                        break;
                    case DestinationEnum.TWStatsTribe:
                        WebBrowser.Navigate(string.Format(World.Default.TWStats.Tribe, e.Arguments[0]));
                        break;
                    case DestinationEnum.TWStatsVillage:
                        WebBrowser.Navigate(string.Format(World.Default.TWStats.Village, e.Arguments[0]));
                        break;
                }
            }
            else
            {
                // TW Game links
                switch (e.Destination)
                {
                    case DestinationEnum.Overview:
                        WebBrowser.Navigate(string.Format(World.Default.GameLink, e.Arguments[0], "overview"));
                        break;
                    case DestinationEnum.Info_Village:
                        WebBrowser.Navigate(string.Format(World.Default.GameLink, _activeVillage.ToString(), "info_village&id=" + e.Arguments[0]));
                        break;
                }
            }
        }

        private void OnSettingsLoaded(object sender, EventArgs e)
        {
            if (World.Default.PlayerSelected)
            {
                _activeVillage = World.Default.You.Player.Villages[0].ID;
            }
            else
            {
                _activeVillage = 0;
            }
            if (_gameBrowser)
            {
                // state pattern :p
                Browser.Navigate(World.Default.Server);
                _parsers = new List<IBrowserParser>();
                _parsers.Add(new HtmlReportParser());
                _parsers.Add(new ResourcesParser());
                _parsers.Add(new VillageInfoHandler());
                //_parsers.Add(new BuildingParser());
                _parsers.Add(new TroopsParser());
            }
            else
            {
                Browser.Navigate(World.Default.TWStats.Default);
            }
        }
        #endregion

        #region Buttons
        private void BackButton_Click(object sender, EventArgs e)
        {
            Browser.GoBack();
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            Browser.GoForward();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Browser.Stop();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            Browser.Refresh();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            if (World.Default.HasLoaded)
                Browser.Navigate(World.Default.Server);
        }

        private void HomeStatsButton_Click(object sender, EventArgs e)
        {
            if (World.Default.HasLoaded)
            {
                Browser.Navigate(World.Default.TWStats.Default);
            }
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            Browser.Navigate(Url.Text);
        }

        private void Url_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                GoButton.PerformClick();
        }
        #endregion

        #region Browser Events
        private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = e.Url.ToString();
            if (!url.ToUpper().StartsWith("JAVASCRIPT"))
            {
                Url.Text = url;
                StopButton.Enabled = true;
            }
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string url = Url.Text;
            StopButton.Enabled = false;
            BackButton.Enabled = Browser.CanGoBack;
            ForwardButton.Enabled = Browser.CanGoForward;

            if (_gameBrowser)
            {
                // Change the active village
                Match match = _activeVillageRegEx.Match(url);
                if (match.Success)
                {
                    _activeVillage = System.Convert.ToInt32(match.Groups["village"].Value);

                    // Get server time
                    DateTime serverTime;
                    Tools.Parsers.CommonParsers.ServerTime(Browser.DocumentText, out serverTime);

                    // parse the document
                    this.ParseResultLabel.Text = string.Empty;
                    foreach (IBrowserParser parser in _parsers)
                    {
                        if (parser.Handles(url))
                        {
                            if (parser.Handle(Browser.DocumentText, serverTime))
                            {
                                this.ParseResultLabel.Text = "Success";
                                this.ParseResultLabel.BackColor = Color.Green;
                            }
                            else
                            {
                                this.ParseResultLabel.Text = "Failure";
                                this.ParseResultLabel.BackColor = Color.Red;
                            }
                        }
                    }
                    if (ParseResultLabel.Text.Length == 0)
                        ParseResultLabel.BackColor = Color.Transparent;
                }
            }
        }

        private void Browser_CanGoBackChanged(object sender, EventArgs e)
        {
            BackButton.Enabled = Browser.CanGoBack;
        }

        private void Browser_CanGoForwardChanged(object sender, EventArgs e)
        {
            ForwardButton.Enabled = Browser.CanGoForward;
        }
        #endregion
    }
}
