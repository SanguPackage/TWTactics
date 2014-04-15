#region Using
using System;
using System.Collections.Generic;
using System.Text;

using TribalWars.Data.Villages;
using TribalWars.Data.Players;
using TribalWars.Data.Maps;
using TribalWars.Data.Events;
using TribalWars.Data.Tribes;
#endregion

namespace TribalWars
{
    /// <summary>
    /// Defines a TW world
    /// </summary>
    public partial class World
    {
        /// <summary>
        /// A wrapper class for all World events
        /// </summary>
        public class Publisher
        {
            #region Events
            // actions done informers
            public event EventHandler<EventArgs> Loaded;
            public event EventHandler<EventArgs> SettingsLoaded;
            public event EventHandler<EventArgs> MonitorLoaded;

            // action events
            public event EventHandler<BrowserEventArgs> Browse;
            #endregion

            #region Publish Methods
            #region Action Events
            /// <summary>
            /// Publishes an event to browse to an URI
            /// </summary>
            public void BrowseUri(object sender, TribalWars.Controls.Main.Browser.DestinationEnum dest, params string[] args)
            {
                if (Browse != null)
                {
                    Browse(sender, new BrowserEventArgs(dest, args));
                }
            }
            #endregion

            #region Informative Events
            /// <summary>
            /// Publishes an event indicating new world data has loaded
            /// </summary>
            public void InformLoaded(object sender, EventArgs e)
            {
                if (Loaded != null)
                    Loaded(sender, e);
            }

            /// <summary>
            /// Publishes an event indicating a settings file has been loaded
            /// </summary>
            public void InformSettingsLoaded(object sender, EventArgs e)
            {
                if (SettingsLoaded != null)
                    SettingsLoaded(sender, e);
            }

            /// <summary>
            /// Inform us that we are done loading world data async
            /// </summary>
            public void InformMonitoringLoaded(object sender)
            {
                if (MonitorLoaded != null)
                    MonitorLoaded(sender, EventArgs.Empty);
            }
            #endregion
            #endregion
        }
    }
}
