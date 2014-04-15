using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Villages;

namespace TribalWars.Controls
{
    public class MapDistanceVillageComparor : IComparable<MapDistanceVillageComparor>
    {
        public double TotalSecondsLeft;
        public MapDistanceVillageControl MapDistanceVillage;
        public int UnitSelectedIndex;
        public Village Village;

        public MapDistanceVillageComparor(MapDistanceVillageControl userControl, double totalSeconds)
        {
            // used for sorting
            TotalSecondsLeft = totalSeconds;

            // Need reference for Target village etc
            MapDistanceVillage = userControl;

            // Need explicit references to change the order of the user controls
            // in the container
            UnitSelectedIndex = userControl.UnitSelectedIndex;
            Village = userControl.Village;
        }

        #region IComparable<MapDistanceVillageComparor> Members
        public int CompareTo(MapDistanceVillageComparor other)
        {
            if (other == null) return -1;
            if (TotalSecondsLeft == other.TotalSecondsLeft) return 0;
            return TotalSecondsLeft > other.TotalSecondsLeft ? 1 : -1;
        }
        #endregion
    }
}
