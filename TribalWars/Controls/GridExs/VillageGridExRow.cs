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
    /// The data for a villagerow in the <see cref="VillagesGridExControl"/>
    /// </summary>
    public class VillageGridExRow
    {
        private readonly Map _map;

        public Village Village { get; set; }

        public bool Visible
        {
            get { return _map.Display.IsVisible(Village); }
        }

        public string Coordinates { get; set; }

        public string Name { get; set; }

        public int Kingdom { get; set; }

        public int Type { get; set; }

        public string Player
        {
            get { return Village.Player == null ? "" : Village.Player.Name; }
        }

        public Image TypeImage
        {
            get { return Village.Type.GetImage(true); }
        }

        public int Points { get; set; }

        public VillageGridExRow(Map map, Village village)
        {
            _map = map;
            Village = village;
            Coordinates = village.LocationString;
            Name = village.Name;
            Kingdom = village.Kingdom;
            Type = (int)village.Type;
            Points = village.Points;
        }
    }
}
