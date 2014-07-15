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
        private IEnumerable<VillagePlayerTribeRow> _playerAutoComplete;
        private IEnumerable<VillagePlayerTribeRow> _tribeAutoComplete;
        #endregion

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
        }

        public AutoCompleteCache(Dictionary<string, Player> players, Dictionary<string, Tribe> tribes)
        {
            _playerAutoComplete = players.Values.Select(x => new VillagePlayerTribeRow(x));
            _tribeAutoComplete = tribes.Values.Select(x => new VillagePlayerTribeRow(x));
        }

        public override string ToString()
        {
            return string.Format("Players={0}, Tribes={1}", _playerAutoComplete.Count(), _tribeAutoComplete.Count());
        }
    }
}
