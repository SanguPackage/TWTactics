using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using TribalWars.Browsers.Parsers;
using TribalWars.Worlds;

namespace TribalWars.Browsers.Control
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

            Browser.CanGoForwardChanged += Browser_CanGoForwardChanged;
            Browser.CanGoBackChanged += Browser_CanGoBackChanged;

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
                    case DestinationEnum.TwStatsPlayer:
                        WebBrowser.Navigate(string.Format(World.Default.Settings.TwStats.Player, e.Arguments[0]));
                        break;
                    case DestinationEnum.TwStatsTribe:
                        WebBrowser.Navigate(string.Format(World.Default.Settings.TwStats.Tribe, e.Arguments[0]));
                        break;
                    case DestinationEnum.TwStatsVillage:
                        WebBrowser.Navigate(string.Format(World.Default.Settings.TwStats.Village, e.Arguments[0]));
                        break;

                    case DestinationEnum.GuestPlayer:
                        WebBrowser.Navigate(string.Format(World.Default.Settings.GuestPlayerLink, e.Arguments[0]));
                        break;

                    case DestinationEnum.GuestTribe:
                        WebBrowser.Navigate(string.Format(World.Default.Settings.GuestTribeLink, e.Arguments[0]));
                        break;
                }
            }
            else
            {
                // TW Game links
                switch (e.Destination)
                {
                    case DestinationEnum.Overview:
                        WebBrowser.Navigate(string.Format(World.Default.Settings.GameLink, e.Arguments[0], "overview"));
                        break;
                    case DestinationEnum.InfoVillage:
                        WebBrowser.Navigate(string.Format(World.Default.Settings.GameLink, _activeVillage.ToString(CultureInfo.InvariantCulture), "info_village&id=" + e.Arguments[0]));
                        break;
                }
            }
        }

        private void OnSettingsLoaded(object sender, EventArgs e)
        {
            if (World.Default.PlayerSelected && _gameBrowser)
            {
                var anyVillage = World.Default.You.Villages.FirstOrDefault();
                _activeVillage = anyVillage == null ? 0 : anyVillage.Id;
            }
            else
            {
                _activeVillage = 0;
            }
            if (_gameBrowser)
            {
                // state pattern :p
                Browser.Navigate(World.Default.Settings.Server);
                _parsers = new List<IBrowserParser>();
                _parsers.Add(new HtmlReportParser());
                _parsers.Add(new ResourcesParser());
                _parsers.Add(new VillageInfoHandler());
                //_parsers.Add(new BuildingParser());
                _parsers.Add(new TroopsParser());
            }
            else
            {
                Browser.Navigate(World.Default.Settings.TwStats.Default);
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
                Browser.Navigate(World.Default.Settings.Server);
        }

        private void HomeStatsButton_Click(object sender, EventArgs e)
        {
            if (World.Default.HasLoaded)
            {
                Browser.Navigate(World.Default.Settings.TwStats.Default);
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
                    _activeVillage = Convert.ToInt32(match.Groups["village"].Value);

                    // Get server time
                    DateTime serverTime;
                    Tools.Parsers.CommonParsers.ServerTime(Browser.DocumentText, out serverTime);

                    // parse the document
                    ParseResultLabel.Text = string.Empty;
                    foreach (IBrowserParser parser in _parsers)
                    {
                        if (parser.Handles(url))
                        {
                            if (parser.Handle(Browser.DocumentText, serverTime))
                            {
                                ParseResultLabel.Text = "Success";
                                ParseResultLabel.BackColor = Color.Green;
                            }
                            else
                            {
                                ParseResultLabel.Text = "Failure";
                                ParseResultLabel.BackColor = Color.Red;
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
