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
        private readonly IEnumerable<PlayerOrTribeRow> _playerAutoComplete;
        private readonly IEnumerable<PlayerOrTribeRow> _tribeAutoComplete;
        #endregion

        public PlayerOrTribeRow[] GetPlayersAndTribes(bool players, bool tribes)
        {
            var dataSource = Enumerable.Empty<PlayerOrTribeRow>();
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
            _playerAutoComplete = Enumerable.Empty<PlayerOrTribeRow>();
            _tribeAutoComplete = Enumerable.Empty<PlayerOrTribeRow>();
        }

        public AutoCompleteCache(Dictionary<string, Player> players, Dictionary<string, Tribe> tribes)
        {
            _playerAutoComplete = players.Values.Select(x => new PlayerOrTribeRow(x));
            _tribeAutoComplete = tribes.Values.Select(x => new PlayerOrTribeRow(x));
        }

        public override string ToString()
        {
            return string.Format("Players={0}, Tribes={1}", _playerAutoComplete.Count(), _tribeAutoComplete.Count());
        }
    }
}
