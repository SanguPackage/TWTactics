using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Tools;
using TribalWars.Worlds;

namespace TribalWars.Villages
{
    /// <summary>
    /// Represents a Tribal Wars player
    /// </summary>
    public sealed class Player : IComparable<Player>, IEquatable<Player>, IEnumerable<Village>
    {
        #region Fields
        private int _id;
        internal int TribeId;

        private int _points;
        private int _rank;

        private List<Village> _lostVillages;
        private readonly object _lostVillagesLock = new object();
        private List<Village> _gainedVillages;
        private readonly object _gainedVillagesLock = new object();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the villages lost since the data download
        /// </summary>
        public List<Village> LostVillages
        {
            get
            {
                lock (_lostVillagesLock)
                {
                    return _lostVillages;
                }
            }
            private set { _lostVillages = value; }
        }

        /// <summary>
        /// Gets the villages gained since the data download
        /// </summary>
        public List<Village> GainedVillages
        {
            get
            {
                lock (_gainedVillagesLock)
                {
                    return _gainedVillages;
                }
            }
            private set { _gainedVillages = value; }
        }

        /// <summary>
        /// Gets or sets the Tribal Wars Database ID
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Gets or sets the player name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the player Tribe
        /// </summary>
        public Tribe Tribe { get; set; }

        /// <summary>
        /// Gets a value indicating the player has joined a tribe
        /// </summary>
        public bool HasTribe
        {
            get { return Tribe != null; }
        }

        /// <summary>
        /// Gets or sets the list of villages belonging to the player
        /// </summary>
        public List<Village> Villages { get; set; }

        /// <summary>
        /// Gets or sets the total points of the player
        /// </summary>
        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        /// <summary>
        /// Gets or sets the rank of the player
        /// </summary>
        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        /// <summary>
        /// Gets the Player details of the previous downloaded data
        /// </summary>
        public Player PreviousPlayerDetails { get; private set; }

        /// <summary>
        /// Gets the amount of conquered and lost villages
        /// </summary>
        public string ConquerString
        {
            get
            {
                if (PreviousPlayerDetails == null) return null;
                if (GainedVillages.Count == 0 && LostVillages.Count == 0) return null;
                if (LostVillages.Count == 0) return string.Format("+{0}", GainedVillages.Count);
                return string.Format("+{0} -{1}", GainedVillages.Count, LostVillages.Count);
            }
        }

        private void CalculateConquers(Player previous)
        {
            GainedVillages.Clear();
            foreach (Village vil in Villages)
            {
                if (!previous.Villages.Contains(vil))
                {
                    GainedVillages.Add(vil);
                }
            }

            LostVillages.Clear();
            foreach (Village vil in previous.Villages)
            {
                if (!Villages.Contains(vil))
                {
                    LostVillages.Add(vil);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether there has been a tribe change
        /// since the previous data download
        /// </summary>
        public bool TribeChange
        {
            get
            {
                if (PreviousPlayerDetails == null) return false;

                return (PreviousPlayerDetails.Tribe != null && !PreviousPlayerDetails.Tribe.Equals(Tribe))
                         || (Tribe != null && !Tribe.Equals(PreviousPlayerDetails.Tribe));
            }
        }

        /// <summary>
        /// Gets a string describing the player
        /// </summary>
        public string Tooltip
        {
            get
            {
                var str = new StringBuilder();
                str.AppendFormat("Points: {0}", Common.GetPrettyNumber(_points));
                str.AppendLine();
                str.AppendFormat("Villages: {0}", Common.GetPrettyNumber(Villages.Count));
                string conquer = ConquerString;
                if (conquer != null) str.AppendFormat(" ({0})", conquer);
                if (Tribe != null)
                {
                    str.AppendLine();
                    str.AppendFormat("Tribe: {0} (Rank: {1})", Tribe.Tag, Common.GetPrettyNumber(Tribe.Rank));
                }

                return str.ToString().Trim();
            }
        }
        #endregion

        #region Constructors
        public Player()
        {
            Villages = new List<Village>();
            GainedVillages = new List<Village>();
            LostVillages = new List<Village>();
        }

        internal Player(string[] pPlayer) : this()
        {
            //$id, $name, $ally, $villages, $points, $rank
            int.TryParse(pPlayer[0], out _id);
            Name = System.Web.HttpUtility.UrlDecode(pPlayer[1]);
            int.TryParse(pPlayer[2], out TribeId);
            int.TryParse(pPlayer[4], out _points);
            int.TryParse(pPlayer[5], out _rank);
        }
        #endregion

        #region BBCode
        public override string ToString()
        {
            return string.Format("{0} ({1:#,0}pts|{2}vils)", Name, Points, Villages.Count);
        }

        public string BbCode()
        {
            string str = string.Format("[player]{0}[/player]", Name);
            if (Villages.Count > 1) str += string.Format(" ({0:#,0}pts|{1}vils)", Points, Villages.Count);
            return str;
        }

        public string BbCodeExtended()
        {
            return BbCodeExtended(0, null, true);
        }

        public string BbCodeExtended(bool showTribe)
        {
            return BbCodeExtended(0, null, showTribe);
        }

        public string BbCodeExtended(Village selectedVillage)
        {
            return BbCodeExtended(0, selectedVillage, true);
        }

        public string BbCodeExtended(int minFilter)
        {
            return BbCodeExtended(minFilter, null, true);
        }

        public string BbCodeExtended(int minFilter, Village selectedVillage)
        {
            return BbCodeExtended(minFilter, selectedVillage, true);
        }

        public string BbCodeExtended(int minFilter, bool showTribe)
        {
            return BbCodeExtended(minFilter, null, showTribe);
        }

        public string BbCodeExtended(int minFilter, Village selectedVillage, bool showTribe)
        {
            List<Village> villages = Villages.FindAll(vil => vil.Points > minFilter);

            // Build it
            var str = new StringBuilder(100);
            str.Append(BbCode());
            if (HasTribe && showTribe) str.Append(Environment.NewLine + string.Format("{0}", Tribe.BbCode()));
            if (villages.Count > 1) str.Append(Environment.NewLine + "[b]Villages[/b][quote]");
            villages.Sort();
            bool isFirst = true;
            foreach (Village vil in villages)
            {
                if (!isFirst || Villages.Count == 1) str.AppendLine();
                else isFirst = false;
                if (selectedVillage != null && vil == selectedVillage)
                {
                    str.AppendFormat("---> {0} <---", vil.BbCode());
                }
                else
                {
                    str.Append(vil.BbCode());
                }
            }
            if (villages.Count > 1) str.Append("[/quote]");
            return str.ToString();
        }

        public string BbCodeMatt()
        {
            // Build it
            var str = new StringBuilder(100);
            str.Append("[b]");
            if (HasTribe)
            {
                str.AppendFormat("[ally]{0}[/ally] Target: {1}", Tribe.Tag, BbCode());
            }
            else
            {
                str.Append(BbCode());
            }

            //http://nl.twstats.com/image.php?type=playerssgraph&id=661959&s=nl10&graph=points
            str.AppendLine();
            string link = string.Format(World.Default.Settings.TwStats.Player, Id);
            str.AppendFormat("[url={0}]TWStats Link[/url]", link);

            str.AppendLine();
            str.AppendLine();

            link = string.Format(World.Default.Settings.TwStats.PlayerGraph, Id, World.TwStatsLinks.Graphs.points);
            str.AppendFormat("[img]{0}[/img]", link);

            link = string.Format(World.Default.Settings.TwStats.PlayerGraph, Id, World.TwStatsLinks.Graphs.rank);
            str.AppendFormat("[img]{0}[/img]", link);

            str.AppendLine();

            link = string.Format(World.Default.Settings.TwStats.PlayerGraph, Id, World.TwStatsLinks.Graphs.odd);
            str.AppendFormat("[img]{0}[/img]", link);

            link = string.Format(World.Default.Settings.TwStats.PlayerGraph, Id, World.TwStatsLinks.Graphs.villages);
            str.AppendFormat("[img]{0}[/img]", link);

            str.AppendLine();
            str.AppendLine();

            Villages.Sort(new Village.VillageComparer());
            int cnt = 0;
            string[] players = { }; // "rogier1986;edgile;kezmania;floris 5;ruuuler;belgium4ever;hoendroe;sjarlowitsky;unusually talented".Split(';');
            int seperator = 0;
            if (players.Length > 0)
                seperator = Convert.ToInt32(Math.Floor((decimal)Villages.Count / players.Length));

            int currentPlayer = 0;
            foreach (Village vil in Villages)
            {
                if (cnt != 0 || Villages.Count == 1)
                {
                    str.AppendLine();
                }
                if (seperator > 0 && cnt % seperator == 0 && players.Length > currentPlayer + 1)
                {
                    currentPlayer++;
                    str.AppendLine();
                    str.AppendLine();
                    str.AppendLine(players[currentPlayer]);
                }
                
                cnt++;
                str.AppendFormat("{0} {1}: ", cnt, vil.BbCode());

                if (cnt % 240 == 0)
                {
                    str.AppendLine("[/b]");
                    str.AppendLine();
                    str.AppendLine();
                    str.AppendLine();
                    str.AppendLine("Next part:");

                    if (players.Length > currentPlayer)
                        str.AppendLine("[b]" + players[currentPlayer]);
                    else
                        str.AppendLine("[b]");
                }
            }
            str.AppendLine();
            str.Append("[/b]");
            return str.ToString();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the Player with the previous downloaded data
        /// </summary>
        public void SetPreviousDetails(Player player)
        {
            CalculateConquers(player);
            PreviousPlayerDetails = player;
        }

        /// <summary>
        /// Adds a village to the player
        /// </summary>
        public void AddVillage(Village village)
        {
            Villages.Add(village);
        }
        #endregion

        #region IEquatable<Player> Members
        public int CompareTo(Player other)
        {
            return other._points - _points;
        }

        public bool Equals(Player other)
        {
            if ((object)other == null) return false;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return Equals(obj as Player);
        }

        public override int GetHashCode()
        {
            return Rank.GetHashCode();
        }

        public static bool operator ==(Player left, Player right)
        {
            if (ReferenceEquals(left, right)) return true;
            if ((object)left == null || (object)right == null) return false;
            return left.Name == right.Name;

        }

        public static bool operator !=(Player left, Player right)
        {
            return !(left == right);
        }
        #endregion

        #region IEnumerable<Village> Members
        public IEnumerator<Village> GetEnumerator()
        {
            foreach (Village vil in Villages)
                yield return vil;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}