using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using TribalWars.Data.Villages;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;

namespace TribalWars.Controls.Accordeon.Location
{
    #region Enums
    public enum SearchForEnum
    {
        Villages,
        Players,
        Tribes
    }
    #endregion

    /// <summary>
    /// Wrapper for world searching options
    /// </summary>
    public class FinderOptions
    {
        #region Fields
        private FinderLocationEnum _evaluate;
        private int _pointsBetweenStart;
        private int _pointsBetweenEnd;
        private int _resultLimit;
        private FinderOptionsEnum _options;
        private string _text;
        private Tribe _tribe;
        private SearchForEnum _searchFor;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating what to search for
        /// </summary>
        public SearchForEnum SearchFor
        {
            get { return _searchFor; }
            set { _searchFor = value; }
        }

        /// <summary>
        /// Gets or sets the tribe the results need to be filtered on
        /// </summary>
        public Tribe Tribe
        {
            get { return _tribe; }
            set { _tribe = value; }
        }

        /// <summary>
        /// Gets or sets the search string
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// Gets or sets the area that needs to be evaluated
        /// </summary>
        public FinderLocationEnum Evaluate
        {
            get { return _evaluate; }
            set { _evaluate = value; }
        }

        /// <summary>
        /// Gets a value indicating whether only the currently
        /// displayed villages should be returned
        /// </summary>
        public bool EvaluateScreenOnly
        {
            get { return _evaluate == FinderLocationEnum.VisibleMap; }
        }

        /// <summary>
        /// Gets or sets the beginning point search range
        /// </summary>
        public int PointsBetweenStart
        {
            get { return _pointsBetweenStart; }
            set { _pointsBetweenStart = value; }
        }

        /// <summary>
        /// Gets or sets the end point search range
        /// </summary>
        public int PointsBetweenEnd
        {
            get { return _pointsBetweenEnd; }
            set { _pointsBetweenEnd = value; }
        }

        /// <summary>
        /// Gets or sets the limit of search results
        /// </summary>
        public int ResultLimit
        {
            get { return _resultLimit; }
            set { _resultLimit = value; }
        }

        /// <summary>
        /// Gets or sets the specific searching method
        /// </summary>
        public FinderOptionsEnum Options
        {
            get { return _options; }
            set { _options = value; }
        }
        #endregion

        #region Match Lists
        /// <summary>
        /// Finds all players matching the options
        /// </summary>
        public IEnumerable<Player> PlayerMatches()
        {
            switch (Evaluate)
            {
                case FinderLocationEnum.VisibleMap:
                    // TODO
                    return null; // PlayerMatches(new List<Player>(World.Default.Map.Controller.VisiblePlayers.Values));
                case FinderLocationEnum.EntireMap:
                    return PlayerMatches(new List<Player>(World.Default.Players.Values));
                case FinderLocationEnum.Polygon:
                    return null;
                case FinderLocationEnum.ActiveRectangle:
                    List<Player> list = new List<Player>();
                    foreach (Village village in World.Default.Villages.Values)
                    {
                        if (World.Default.Monitor.ActiveRectangle.Contains(village.Location))
                        {
                            if (village.HasPlayer && !list.Contains(village.Player))
                                list.Add(village.Player);
                        }
                    }
                    return PlayerMatches(list);
            }
            return null;
        }

        /// <summary>
        /// Finds all players matching the options
        /// </summary>
        public IEnumerable<Player> PlayerMatches(List<Player> list)
        {
            List<Player> outList = new List<Player>();
            int results = 0;

            if (Options == FinderOptionsEnum.Strongest)
                list.Sort();

            foreach (Player ply in list)
            {
                if (Match(ply))
                {
                    outList.Add(ply);
                    results++;
                }

                if (_resultLimit != 0 && results == _resultLimit)
                    return outList;
            }
            return outList;
        }

        /// <summary>
        /// Finds all tribes matching the options
        /// </summary>
        public IEnumerable<Tribe> TribeMatches()
        {
            switch (Evaluate)
            {
                case FinderLocationEnum.VisibleMap:
                    // todo
                    return null; // TribeMatches(new List<Tribe>(World.Default.Map.Controller.VisibleTribes.Values));
                case FinderLocationEnum.EntireMap:
                    return TribeMatches(new List<Tribe>(World.Default.Tribes.Values));
                case FinderLocationEnum.Polygon:
                    return null;
                case FinderLocationEnum.ActiveRectangle:
                    List<Tribe> list = new List<Tribe>();
                    foreach (Village village in World.Default.Villages.Values)
                    {
                        if (World.Default.Monitor.ActiveRectangle.Contains(village.Location))
                        {
                            if (village.HasTribe && !list.Contains(village.Player.Tribe))
                                list.Add(village.Player.Tribe);
                        }
                    }
                    return TribeMatches(list);
            }
            return null;
        }

        /// <summary>
        /// Finds all tribes matching the options
        /// </summary>
        public IEnumerable<Tribe> TribeMatches(List<Tribe> list)
        {
            List<Tribe> outList = new List<Tribe>();
            int results = 0;

            if (this.Options == FinderOptionsEnum.Strongest)
                list.Sort();

            foreach (Tribe tribe in list)
            {
                if (Match(tribe))
                {
                    outList.Add(tribe);
                    results++;
                }

                if (_resultLimit != 0 && results == _resultLimit)
                    return outList;
            }
            return outList;
        }

        /// <summary>
        /// Finds all villages matching the options
        /// </summary>
        public IEnumerable<Village> VillageMatches()
        {
            switch (Evaluate)
            {
                case FinderLocationEnum.EntireMap:
                    // todo
                    //return null; // VillageMatches(new List<Village>(World.Default.Villages.Values));
                case FinderLocationEnum.VisibleMap:
                    // todo
                    //return null; // VillageMatches(new List<Village>(World.Default.Map.Controller.VisibleVillages.Values));
                case FinderLocationEnum.Polygon:
                    //return null;
                case FinderLocationEnum.ActiveRectangle:
                    List<Village> list = new List<Village>();
                    foreach (Village village in World.Default.Villages.Values)
                    {
                        if (World.Default.Monitor.ActiveRectangle.Contains(village.Location))
                        {
                            list.Add(village);
                        }
                    }
                    return VillageMatches(list);
            }
            return null;
        }

        /// <summary>
        /// Finds all villages matching the options
        /// </summary>
        public IEnumerable<Village> VillageMatches(List<Village> list)
        {
            List<Village> outList = new List<Village>();
            int results = 0;

            if (this.Options == FinderOptionsEnum.Strongest)
                list.Sort();

            foreach (Village village in list)
            {
                if (Match(village))
                {
                    outList.Add(village);
                    results++;
                }

                if (_resultLimit != 0 && results == _resultLimit)
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
            if (_tribe != null && !_tribe.Equals(ply.Tribe))
                return false;

            if (!string.IsNullOrEmpty(_text) && !ply.Name.ToUpper(CultureInfo.InvariantCulture).Contains(_text))
            {
                return false;
            }
            switch (Options)
            {
                case FinderOptionsEnum.Inactives:
                    if (ply.PreviousPlayerDetails == null)
                        return false;
                    foreach (Village village in ply)
                        if (village.Points > village.PreviousVillageDetails.Points)
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


            if (ply.Points < _pointsBetweenStart || (_pointsBetweenEnd > 0 && ply.Points > _pointsBetweenEnd))
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the tribe matches the current search criteria
        /// </summary>
        public bool Match(Tribe tribe)
        {
            if (_text.Length != 0 && !tribe.Name.ToUpper(CultureInfo.InvariantCulture).Contains(_text) && !tribe.Tag.ToUpper(CultureInfo.InvariantCulture).Contains(_text))
            {
                return false;
            }

            if (tribe.AllPoints < _pointsBetweenStart || (_pointsBetweenEnd > 0 && tribe.AllPoints > _pointsBetweenEnd))
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the village matches the current search criteria
        /// </summary>
        public bool Match(Village village)
        {
            if (Options == FinderOptionsEnum.Nobled)
            {
                if (!village.HasPlayer || village.PreviousVillageDetails == null || village.Player.Equals(village.PreviousVillageDetails.Player))
                    return false;

                if (_tribe != null && 
                    (
                        (!village.HasTribe || !_tribe.Equals(village.Player.Tribe))
                        && 
                        (!village.PreviousVillageDetails.HasTribe || !_tribe.Equals(village.PreviousVillageDetails.Player.Tribe))
                     )
                    )
                    return false;
            }
            else
            {
                if (_tribe != null && (!village.HasTribe || !_tribe.Equals(village.Player.Tribe)))
                    return false;

                switch (Options)
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

            if (!string.IsNullOrEmpty(_text) && !village.ToString().ToUpper(CultureInfo.InvariantCulture).Contains(_text))
            {
                return false;
            }

            if (village.Points < _pointsBetweenStart || (_pointsBetweenEnd > 0 && village.Points > _pointsBetweenEnd))
                return false;

            return true;
        }
        #endregion

        #region Search Lists
        /// <summary>
        /// Creates the list of villages to search through
        /// </summary>
        private List<Village> GetVillageList()
        {
            List<Village> list = new List<Village>();
            foreach (Village village in World.Default.Villages.Values)
            {
                switch (Evaluate)
                {
                    case FinderLocationEnum.ActiveRectangle:
                        if (World.Default.Monitor.ActiveRectangle.Contains(village.Location))
                            list.Add(village);
                        break;
                    case FinderLocationEnum.VisibleMap:

                    default:
                        list.Add(village);
                        break;
                }
            }
            return list;
        }

        /// <summary>
        /// Creates the list of tribes to search through
        /// </summary>
        private List<Tribe> GetTribeList()
        {
            List<Tribe> list = new List<Tribe>();
            foreach (Village village in World.Default.Villages.Values)
            {
                if (World.Default.Monitor.ActiveRectangle.Contains(village.Location))
                {
                    if (village.HasTribe && !list.Contains(village.Player.Tribe))
                        list.Add(village.Player.Tribe);
                }
            }
            return list;
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
