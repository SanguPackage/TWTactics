#region Using
using System.Collections.Generic;
using System.Globalization;
using TribalWars.Data.Maps.Manipulators.Helpers;
using TribalWars.Data.Villages;
#endregion

namespace TribalWars.Controls
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
            row.KINGDOM = village.Kingdom.ToString(CultureInfo.InvariantCulture);
            row.LOCATION = village.LocationString;
            row.NAME = village.Name;
            if (village.HasPlayer)
            {
                row.PLAYER = village.Player.Name;
                row.PLAYERID = village.PlayerId;
                if (village.HasTribe)
                {
                    row.TRIBE = village.Player.Tribe.Tag;
                    row.TRIBEID = village.Player.Tribe.Id;
                }
            }
            row.POINTS = village.Points;
            if (village.PreviousVillageDetails != null)
            {
                row.POINTSDIFF = village.Points - village.PreviousVillageDetails.Points;
            }
            else
            {
                row.POINTSDIFF = 0;
            }
            row.POLYGON = polygon.Name;
            row.POLYGONGROUP = polygon.Group;
            row.POLYGONVISIBLE = polygon.Visible;
            row.BBCODE = village.BBCode();

            VILLAGE.Rows.Add(row);
        }
    }
}
