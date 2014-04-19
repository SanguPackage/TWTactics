using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data.Maps;

namespace TribalWars.Controls
{
    /// <summary>
    /// Used as history list with links of the last Map.Locations
    /// </summary>
    public partial class LocationList : UserControl
    {
        private int _MaxLinks = 5;
        private Dictionary<LinkLabel, Location> Locations = new Dictionary<LinkLabel, Location>(5);

        #region Constructors
        public LocationList()
        {
            InitializeComponent();
        }
        #endregion

        public int MaxLinks
        {
            get { return _MaxLinks; }
            set { _MaxLinks = value; }
        }

        public void Add(Location location, string name)
        {
            bool exists = false;
            foreach (KeyValuePair<LinkLabel, Location> pair in Locations)
            {
                if (location.Equals(pair.Value))
                {
                    exists = true;
                    Panel.Controls.Remove(pair.Key);

                    // fill tmp
                    List<Control> tmpControls = new List<Control>();
                    foreach (Control control in Panel.Controls)
                        tmpControls.Add(control);
                    tmpControls.Add(pair.Key);

                    // switch
                    Panel.Controls.Clear();
                    Panel.Controls.AddRange(tmpControls.ToArray());
                    
                    break;
                }
            }
            if (!exists)
            {
                LinkLabel link = new LinkLabel();
                link.Text = name;
                link.Height = 20;
                link.Click += new EventHandler(Link_Click);
                Locations.Add(link, location);
                Panel.Controls.Add(link);
            }
            if (Panel.Controls.Count > MaxLinks)
            {
                Locations.Remove((LinkLabel)Panel.Controls[0]);
                Panel.Controls.RemoveAt(0);
            }
            Panel.Invalidate();
        }

        private void Link_Click(object sender, EventArgs e)
        {
            if (sender is LinkLabel)
            {
                Location loc = Locations[(LinkLabel)sender];
                World.Default.Map.SetCenter(loc);
            }
        }
    }
}
