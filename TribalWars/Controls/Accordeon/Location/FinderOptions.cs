using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using TribalWars.Data;
using TribalWars.Data.Villages;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
using TribalWars.Tools;

namespace TribalWars.Controls.Accordeon.Location
{
    #region Enums
    public enum SearchForEnum
    {
        Players = 0,
        Tribes = 1,
        Villages = 2
    }
    #endregion

    /// <summary>
    /// Wrapper for world searching options
    /// </summary>
    public class FinderOptions
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating what to search for
        /// </summary>
        public SearchForEnum SearchFor { get; set; }

        /// <summary>
        /// Gets or sets the tribe the results need to be filtered on
        /// </summary>
        public Tribe Tribe { get; set; }

        /// <summary>
        /// Gets or sets the search string
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the area that needs to be evaluated
        /// </summary>
        public FinderLocationEnum EvaluatedArea { get; set; }

        /// <summary>
        /// Gets or sets the beginning point search range
        /// </summary>
        public int PointsBetweenStart { private get; set; }

        /// <summary>
        /// Gets or sets the end point search range
        /// </summary>
        public int PointsBetweenEnd { private get; set; }

        /// <summary>
        /// Gets or sets the limit of search results
        /// </summary>
        public int ResultLimit { private get; set; }

        /// <summary>
        /// Gets or sets the specific searching method
        /// </summary>
        public FinderOptionsEnum SearchStrategy { get; set; }
        #endregion

        #region Constructors
        public FinderOptions(SearchForEnum search)
        {
            SearchFor = search;
        }
        #endregion

        #region Match Lists
        /// <summary>
        /// Finds all players matching the options
        /// </summary>
        public IEnumerable<Player> PlayerMatches()
        {
            switch (EvaluatedArea)
            {
                case FinderLocationEnum.VisibleMap:
                    return PlayerMatches(new List<Player>(World.Default.Players.Values.Where(World.Default.Map.Display.IsVisible)));

                case FinderLocationEnum.EntireMap:
                    return PlayerMatches(new List<Player>(World.Default.Players.Values));

                case FinderLocationEnum.Polygon:
                    return PlayerMatches(World.Default.Map.Manipulators.PolygonManipulator.GetAllPolygonVillages().GetPlayers().ToList());

                case FinderLocationEnum.ActiveRectangle:
                    var villagesInActiveRectangle = World.Default.Villages.Values.Where(x => World.Default.Monitor.ActiveRectangle.Contains(x.Location));
                    return PlayerMatches(villagesInActiveRectangle.GetPlayers().ToList());
            }
            return null;
        }

        /// <summary>
        /// Finds all players matching the options
        /// </summary>
        public IEnumerable<Player> PlayerMatches(List<Player> list)
        {
            var outList = new List<Player>();
            int results = 0;

            if (SearchStrategy == FinderOptionsEnum.Strongest)
                list.Sort();

            foreach (Player ply in list)
            {
                if (Match(ply))
                {
                    outList.Add(ply);
                    results++;
                }

                if (ResultLimit != 0 && results == ResultLimit)
                    return outList;
            }
            return outList;
        }

        /// <summary>
        /// Finds all tribes matching the options
        /// </summary>
        public IEnumerable<Tribe> TribeMatches()
        {
            switch (EvaluatedArea)
            {
                case FinderLocationEnum.VisibleMap:
                    return TribeMatches(new List<Tribe>(World.Default.Tribes.Values.Where(World.Default.Map.Display.IsVisible)));

                case FinderLocationEnum.EntireMap:
                    return TribeMatches(new List<Tribe>(World.Default.Tribes.Values));

                case FinderLocationEnum.Polygon:
                    return TribeMatches(World.Default.Map.Manipulators.PolygonManipulator.GetAllPolygonVillages().GetTribes().ToList());

                case FinderLocationEnum.ActiveRectangle:
                    var villagesInActiveRectangle = World.Default.Villages.Values.Where(x => World.Default.Monitor.ActiveRectangle.Contains(x.Location));
                    return TribeMatches(villagesInActiveRectangle.GetTribes().ToList());
            }
            return null;
        }

        /// <summary>
        /// Finds all tribes matching the options
        /// </summary>
        public IEnumerable<Tribe> TribeMatches(List<Tribe> list)
        {
            var outList = new List<Tribe>();
            int results = 0;

            if (SearchStrategy == FinderOptionsEnum.Strongest)
                list.Sort();

            foreach (Tribe tribe in list)
            {
                if (Match(tribe))
                {
                    outList.Add(tribe);
                    results++;
                }

                if (ResultLimit != 0 && results == ResultLimit)
                    return outList;
            }
            return outList;
        }

        /// <summary>
        /// Finds all villages matching the options
        /// </summary>
        public IEnumerable<Village> VillageMatches()
        {
            switch (EvaluatedArea)
            {
                case FinderLocationEnum.EntireMap:
                    return VillageMatches(new List<Village>(World.Default.Villages.Values));

                case FinderLocationEnum.VisibleMap:
                    return VillageMatches(new List<Village>(World.Default.Villages.Values.Where(World.Default.Map.Display.IsVisible)));

                case FinderLocationEnum.Polygon:
                    return VillageMatches(World.Default.Map.Manipulators.PolygonManipulator.GetAllPolygonVillages().ToList());

                case FinderLocationEnum.ActiveRectangle:
                    var list = World.Default.Villages.Values.Where(village => World.Default.Monitor.ActiveRectangle.Contains(village.Location)).ToList();
                    return VillageMatches(list);
            }

            return null;
        }

        /// <summary>
        /// Finds all villages matching the options
        /// </summary>
        public IEnumerable<Village> VillageMatches(List<Village> list)
        {
            var outList = new List<Village>();
            int results = 0;

            if (SearchStrategy == FinderOptionsEnum.Strongest)
                list.Sort();

            foreach (Village village in list)
            {
                if (Match(village))
                {
                    outList.Add(village);
                    results++;
                }

                if (ResultLimit != 0 && results == ResultLimit)
                    return outList;
            }
            return outList;
        }
        #endregion

        #region Single Match
        /// <summary>
        /// Checks if the player matches the current search criteria
        /// </summary>
        public bool Match(Player ply)
        {
            if (Tribe != null && !Tribe.Equals(ply.Tribe))
                return false;

            if (!string.IsNullOrEmpty(Text) && !ply.Name.ToUpper(CultureInfo.InvariantCulture).Contains(Text))
            {
                return false;
            }
            switch (SearchStrategy)
            {
                case FinderOptionsEnum.Inactives:
                    if (ply.PreviousPlayerDetails == null)
                        return false;
                    if (ply.Any(village => village.PreviousVillageDetails == null || village.Points > village.PreviousVillageDetails.Points))
                        return false;
                    break;

                case FinderOptionsEnum.LostPoints:
                    if (ply.PreviousPlayerDetails == null || ply.Points >= ply.PreviousPlayerDetails.Points)
                        return false;
                    break;

                case FinderOptionsEnum.TribeChange:
                    if (!ply.TribeChange)
                        return false;
                    break;
            }


            if (ply.Points < PointsBetweenStart || (PointsBetweenEnd > 0 && ply.Points > PointsBetweenEnd))
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the tribe matches the current search criteria
        /// </summary>
        public bool Match(Tribe tribe)
        {
            if (Text.Length != 0 && !tribe.Name.ToUpper(CultureInfo.InvariantCulture).Contains(Text) && !tribe.Tag.ToUpper(CultureInfo.InvariantCulture).Contains(Text))
            {
                return false;
            }

            if (tribe.AllPoints < PointsBetweenStart || (PointsBetweenEnd > 0 && tribe.AllPoints > PointsBetweenEnd))
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the village matches the current search criteria
        /// </summary>
        public bool Match(Village village)
        {
            if (SearchStrategy == FinderOptionsEnum.Nobled)
            {
                if (!village.HasPlayer || village.PreviousVillageDetails == null || village.Player.Equals(village.PreviousVillageDetails.Player))
                    return false;

                if (Tribe != null && 
                    (
                        (!village.HasTribe || !Tribe.Equals(village.Player.Tribe))
                        && 
                        (!village.PreviousVillageDetails.HasTribe || !Tribe.Equals(village.PreviousVillageDetails.Player.Tribe))
                     )
                    )
                    return false;
            }
            else
            {
                if (Tribe != null && (!village.HasTribe || !Tribe.Equals(village.Player.Tribe)))
                    return false;

                switch (SearchStrategy)
                {
                    case FinderOptionsEnum.NewInactives:
                        if (village.HasPlayer || village.PreviousVillageDetails == null || !village.PreviousVillageDetails.HasPlayer)
                            return false;
                        break;
                    case FinderOptionsEnum.LostPoints:
                        if (village.PreviousVillageDetails == null || village.Points >= village.PreviousVillageDetails.Points)
                            return false;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(Text) && !village.ToString().ToUpper(CultureInfo.InvariantCulture).Contains(Text))
            {
                return false;
            }

            if (village.Points < PointsBetweenStart || (PointsBetweenEnd > 0 && village.Points > PointsBetweenEnd))
                return false;

            return true;
        }
        #endregion

        #region Enums
        /// <summary>
        /// List of specific search methods
        /// </summary>
        public enum FinderOptionsEnum
        {
            /// <summary>
            /// Search all villages
            /// </summary>
            All = 0,
            /// <summary>
            /// Search villages that have gone abandoned since last data download
            /// </summary>
            NewInactives = 2,
            /// <summary>
            /// Search the strongest players and tribes
            /// </summary>
            Strongest = 1,
            /// <summary>
            /// Search villages that were nobled since the last data download
            /// </summary>
            Nobled = 5,
            /// <summary>
            /// Search villages that lost points since the last data download
            /// </summary>
            LostPoints = 4,
            /// <summary>
            /// Search players that have not grown points since the last data download
            /// </summary>
            Inactives = 3,
            /// <summary>
            /// Search players that have changed tribes since the last data download
            /// </summary>
            TribeChange = 6
        }

        /// <summary>
        /// List of the different locations where to search
        /// </summary>
        public enum FinderLocationEnum
        {
            /// <summary>
            /// Loop through the entire map
            /// </summary>
            EntireMap = 0,
            /// <summary>
            /// Loop through the visible part of the map
            /// </summary>
            VisibleMap = 1,
            /// <summary>
            /// Loop through the active rectangle
            /// </summary>
            ActiveRectangle = 2,
            /// <summary>
            /// Loop throught the selected polygon(s)
            /// </summary>
            Polygon = 3
        }
        #endregion
    }
}
