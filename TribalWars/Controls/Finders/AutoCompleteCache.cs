using System.Collections.Generic;
using System.Linq;
using TribalWars.Villages;
using TribalWars.Worlds;

namespace TribalWars.Controls.Finders
{
    /// <summary>
    /// Cache for the <see cref="PlayerTribeDropdown"/>
    /// </summary>
    public class AutoCompleteCache
    {
        #region Fields
        //private readonly World.WorldVillagesCollection _villages;
        //private readonly Dictionary<string, Player> _players;
        //private readonly Dictionary<string, Tribe> _tribes;

        private IEnumerable<VillagePlayerTribeRow> _playerAutoComplete;
        private IEnumerable<VillagePlayerTribeRow> _tribeAutoComplete;
        //private IEnumerable<VillagePlayerTribeRow> _villageAutoComplete;
        //private bool _villagesAdded;

        #endregion

        //private IEnumerable<VillagePlayerTribeRow> GetAutoCompleteDataSource(int? xCoordVillages = null)
        //{
        //    if (AllowVillage && xCoordVillages.HasValue)
        //    {
        //        return World.Default.Villages.Values.Where(vil => vil.X == xCoordVillages.Value).Select(x => new VillagePlayerTribeRow(x));
        //    }
        //    else
        //    {
        //        var dataSource = Enumerable.Empty<VillagePlayerTribeRow>();
        //        if (AllowPlayer)
        //        {
        //            dataSource = dataSource.Concat(_playerAutoComplete);
        //        }
        //        if (AllowTribe)
        //        {
        //            dataSource = dataSource.Concat(_tribeAutoComplete);
        //        }
        //        return dataSource;
        //    }
        //}

        public VillagePlayerTribeRow[] GetPlayersAndTribes(bool players, bool tribes)
        {
            var dataSource = Enumerable.Empty<VillagePlayerTribeRow>();
            if (players)
            {
                dataSource = dataSource.Concat(_playerAutoComplete);
            }
            if (tribes)
            {
                dataSource = dataSource.Concat(_tribeAutoComplete);
            }
            return dataSource.ToArray();
        }

        public AutoCompleteCache()
        {
            _playerAutoComplete = Enumerable.Empty<VillagePlayerTribeRow>();
            _tribeAutoComplete = Enumerable.Empty<VillagePlayerTribeRow>();

            //_villagesAdded = false;
            //if (AllowVillage)
            //{
            //    _villageAutoComplete = World.Default.Villages.Values.Select(x => new VillagePlayerTribeRow(x));
            //}
        }

        public AutoCompleteCache(Dictionary<string, Player> players, Dictionary<string, Tribe> tribes)
        {
            //_villages = villages;
            //_players = players;
            //_tribes = tribes;

            _playerAutoComplete = World.Default.Players.Select(x => new VillagePlayerTribeRow(x));
            _tribeAutoComplete = World.Default.Tribes.Select(x => new VillagePlayerTribeRow(x));
        }
    }
}
