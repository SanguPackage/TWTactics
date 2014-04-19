using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TribalWars.Data;

namespace TribalWars.Controls.Accordeon.Location
{
    /// <summary>
    /// Used as history list with links of the last Map.Locations
    /// </summary>
    public partial class LocationList : UserControl
    {
        private int _maxLinks = 5;
        private readonly Dictionary<LinkLabel, Data.Maps.Location> _locations = new Dictionary<LinkLabel, Data.Maps.Location>(5);

        #region Constructors
        public LocationList()
        {
            InitializeComponent();
        }
        #endregion

        public int MaxLinks
        {
            private get { return _maxLinks; }
            set { _maxLinks = value; }
        }

        public void Add(Data.Maps.Location location, string name)
        {
            bool exists = false;
            foreach (KeyValuePair<LinkLabel, Data.Maps.Location> pair in _locations)
            {
                if (location.Equals(pair.Value))
                {
                    exists = true;
                    Panel.Controls.Remove(pair.Key);

                    // fill tmp
                    List<Control> tmpControls = Panel.Controls.Cast<Control>().ToList();
                    tmpControls.Add(pair.Key);

                    // switch
                    Panel.Controls.Clear();
                    Panel.Controls.AddRange(tmpControls.ToArray());
                    
                    break;
                }
            }
            if (!exists)
            {
                var link = new LinkLabel {Text = name, Height = 20};
                link.Click += Link_Click;
                _locations.Add(link, location);
                Panel.Controls.Add(link);
            }
            if (Panel.Controls.Count > MaxLinks)
            {
                _locations.Remove((LinkLabel)Panel.Controls[0]);
                Panel.Controls.RemoveAt(0);
            }
            Panel.Invalidate();
        }

        private void Link_Click(object sender, EventArgs e)
        {
            var key = sender as LinkLabel;
            if (key != null)
            {
                Data.Maps.Location loc = _locations[key];
                World.Default.Map.SetCenter(loc);
            }
        }
    }
}
