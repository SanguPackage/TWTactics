#region Using
using System.Collections.Generic;
using System.Globalization;
using TribalWars.Maps.Manipulators.Helpers;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Controls.Polygons
{
    partial class PolygonDataSet
    {
        /// <summary>
        /// Create a new PolygonDataSet with all villages from the polygons
        /// </summary>
        public static PolygonDataSet CreateDataSet(IEnumerable<Polygon> polygons)
        {
            var ds = new PolygonDataSet();
            foreach (Polygon poly in polygons)
            {
                foreach (Village v in poly.GetVillages())
                {
                    ds.AddVillageRow(v, poly);
                }
            }
            return ds;
        }

        private void AddVillageRow(Village village, Polygon polygon)
        {
            VILLAGERow row = VILLAGE.NewVILLAGERow();
            row.Village = village;
            row.Polygon = polygon;

            row.KINGDOM = village.Kingdom.ToString(CultureInfo.InvariantCulture);
            row.LOCATION = village.LocationString;
            row.NAME = village.Name;
            row.ISVISIBLE = World.Default.Map.Display.IsVisible(village);
            if (village.HasPlayer)
            {
                row.PLAYER = village.Player.Name;
                if (village.HasTribe)
                {
                    row.TRIBE = village.Player.Tribe.Tag;
                }
            }
            row.POINTS = village.Points;
            if (village.PreviousVillageDetails != null)
            {
                row.POINTSDIFF = village.Points - village.PreviousVillageDetails.Points;
                if (row.POINTSDIFF == 0)
                {
                    row.SetPOINTSDIFFNull();
                }
            }
            else
            {
                row.SetPOINTSDIFFNull();
            }
            row.POLYGON = polygon.Name;
            row.POLYGONGROUP = polygon.Group;
            row.POLYGONVISIBLE = polygon.Visible;
            row.BBCODE = village.BbCode();

            VILLAGE.Rows.Add(row);
        }

        public partial class VILLAGERow
        {
            public Village Village { get; set; }
            public Polygon Polygon { get; set; }
        }
    }
}
