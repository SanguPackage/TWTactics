using System.Drawing;

namespace TribalWars.Maps.Drawing.Helpers
{
    /// <summary>
    /// Mapping between the game and map locations of a village
    /// </summary>
    public class VillageDrawingLocation
    {
        public Point GameLocation { get; private set; }

        public Point MapLocation { get; private set; }

        public VillageDrawingLocation(Point gameLocation, Point mapRectangle)
        {
            GameLocation = gameLocation;
            MapLocation = mapRectangle;
        }

        public override string ToString()
        {
            return string.Format("GameLocation={0}, MapLocation={1}", GameLocation, MapLocation);
        }
    }
}