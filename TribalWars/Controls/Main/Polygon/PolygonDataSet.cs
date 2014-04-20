#region Using
using TribalWars.Data.Villages;
#endregion

namespace TribalWars.Controls
{
    partial class PolygonDataSet
    {
        public void AddVILLAGERow(Village village, string polygonName)
        {
            VILLAGERow row = VILLAGE.NewVILLAGERow();
            row.KINGDOM = village.Kingdom.ToString();
            row.LOCATION = village.LocationString;
            row.NAME = village.Name;
            if (village.HasPlayer)
            {
                row.PLAYER = village.Player.Name;
                row.PLAYERID = village.PlayerID;
                if (village.HasTribe)
                {
                    row.TRIBE = village.Player.Tribe.Name;
                    row.TRIBEID = village.Player.Tribe.Id;
                }
            }
            row.POINTS = village.Points;
            if (village.PreviousVillageDetails != null)
            {
                row.POINTSDIFF = village.Points - village.PreviousVillageDetails.Points;
            }
            else row.POINTSDIFF = 0;
            row.POLYGON = polygonName;
            row.BBCODE = village.BBCode();

            VILLAGE.Rows.Add(row);
        }
    }
}
