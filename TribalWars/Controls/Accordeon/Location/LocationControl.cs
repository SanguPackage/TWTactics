#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data.Maps;
using TribalWars.Data.Events;
#endregion

namespace TribalWars.Controls.Accordeon.Location
{
    public partial class LocationControl : UserControl
    {
        Data.Maps.Location StartLocation;

        public LocationControl()
        {
            InitializeComponent();
        }

        private void LocationControl_Load(object sender, EventArgs e)
        {
            World.Default.EventPublisher.SettingsLoaded += new EventHandler<EventArgs>(World_SettingsLoaded);
            // Subscribe to setting events
            World.Default.Map.EventPublisher.LocationChanged += new EventHandler<MapLocationEventArgs>(Location_Changed);
        }

        private void World_SettingsLoaded(object sender, EventArgs e)
        {
            // Stuff
            //LocationFav.Add(World.Default.Settings.Location, string.Format("Start {0}|{1}", World.Default.Settings.Location.X, World.Default.Settings.Location.Y));
            StartLocation = World.Default.Map.Location;
            txtX.Text = World.Default.Map.Location.X.ToString();
            txtY.Text = World.Default.Map.Location.Y.ToString();
            txtZ.Text = World.Default.Map.Location.Zoom.ToString();
            //txtWidth.Text = World.Default.Map.Location.Width.ToString();

            if (World.Default.PlayerSelected)
            {
                You.Text = World.Default.You.Player.Name;
                //if (World.Default.Settings.You.Player.HasTribe)
                //    YourTribe.Text = World.Default.Settings.You.Player.Tribe.Tag;
            }
        }

        private void cmdDraw_Click(object sender, EventArgs e)
        {
            int x, y, z;
            if (int.TryParse(txtX.Text, out x) && int.TryParse(txtY.Text, out y) && int.TryParse(txtZ.Text, out z))
            {
                World.Default.Map.SetCenter(x, y, z);
            }
        }

        #region Focusselect
        private void txtX_Enter(object sender, EventArgs e)
        {
            txtX.SelectAll();
        }

        private void txtY_Enter(object sender, EventArgs e)
        {
            txtY.SelectAll();
        }

        private void txtZ_Enter(object sender, EventArgs e)
        {
            txtZ.SelectAll();
        }

        private void txtWidth_Enter(object sender, EventArgs e)
        {
            txtWidth.SelectAll();
        }

        private void txtX_Click(object sender, EventArgs e)
        {
            txtX.SelectAll();
        }

        private void txtY_Click(object sender, EventArgs e)
        {
            txtY.SelectAll();
        }

        private void txtZ_Click(object sender, EventArgs e)
        {
            txtZ.SelectAll();
        }

        private void txtWidth_Click(object sender, EventArgs e)
        {
            txtWidth.SelectAll();
        }
        #endregion

        private void Location_Changed(object sender, MapLocationEventArgs e)
        {
            LocationHistory.Add(e.NewLocation, e.NewLocation.ToString());

            // Location
            txtX.Text = e.NewLocation.X.ToString();
            txtY.Text = e.NewLocation.Y.ToString();
            txtZ.Text = e.NewLocation.Zoom.ToString();
            //txtWidth.Text = e.NewLocation.Width.ToString();

            //World.Default.Map.EventPublisher.PaintMap(null);
        }

        private void cmdCenterKingdom_Click(object sender, EventArgs e)
        {
            int continent;
            if (int.TryParse(txtK.Text, out continent) && continent <= 99 && continent >= 0)
            {
                int x = continent % 10 * 100 + 50;
                int y = (continent - continent % 10) * 10 + 50;
                World.Default.Map.SetCenter(x, y);

                //txtY.Text = txtK.Text.Substring(0, 1) + "50";
                //if (txtK.Text.Length == 2) txtX.Text = txtK.Text.Substring(1, 1);
                //else txtX.Text = "";
                //txtX.Text += "50";
                //txtZ.Text = "10";
                //txtWidth.Text = "1";
                //World.Default.Map.EventPublisher.PaintMap(null);
            }
        }

        private void StripHome_Click(object sender, EventArgs e)
        {
            if (World.Default.HasLoaded)
            {
                World.Default.Map.SetCenter(StartLocation);
            }
        }

        private void You_TextChanged(object sender, EventArgs e)
        {
            if (World.Default.Players.ContainsKey(You.Text.ToUpper()))
            {
                World.Default.You.Player = World.Default.Players[You.Text.ToUpper()];
                //World.Default.You.MarkPlayer.Clear();
                //World.Default.You.MarkPlayer.Add(new PlayerMarker(World.Default.Settings.You.MarkPlayer, World.Default.You));
            }
        }
    }
}
