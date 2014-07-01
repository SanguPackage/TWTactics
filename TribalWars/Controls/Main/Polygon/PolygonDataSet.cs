#region Using
using System.Globalization;
using TribalWars.Data.Maps.Manipulators.Helpers;
using TribalWars.Data.Villages;
#endregion

namespace TribalWars.Controls
{
    partial class PolygonDataSet
    {
        public void AddVILLAGERow(Village village, Polygon polygon)
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
            row.BBCODE = village.BBCode();

            VILLAGE.Rows.Add(row);
        }
    }
}
