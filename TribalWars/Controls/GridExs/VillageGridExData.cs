using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TribalWars.Maps;
using TribalWars.Villages;

namespace TribalWars.Controls.GridExs
{
    /// <summary>
    /// The data for a villagerow in the <see cref="VillagesGridControl"/>
    /// </summary>
    public class VillageGridExData
    {
        private Map _map;
        private Point _location;

        public Village Village { get; set; }

        public bool Visible
        {
            get { return true; }
        }

        public string Coordinates { get; set; }

        public string Name { get; set; }

        public int Kingdom { get; set; }

        public VillageType Type { get; set; }

        public Image TypeImage
        {
            get { return Type.GetImage(true); }
        }

        public int Points { get; set; }

        public VillageGridExData(Map map, Village village)
        {
            _map = map;
            _location = village.Location;
            Village = village;
            Coordinates = village.LocationString;
            Name = village.Name;
            Kingdom = village.Kingdom;
            Type = village.Type;
            Points = village.Points;
        }
    }
}
