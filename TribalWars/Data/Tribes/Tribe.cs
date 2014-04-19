using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using TribalWars.Data.Villages;
using TribalWars.Data.Players;

namespace TribalWars.Data.Tribes
{
    /// <summary>
    /// Represents a Tribal Wars Tribe
    /// </summary>
    public class Tribe : IEquatable<Tribe>, IComparable<Tribe>, IEnumerable<Village>
    {
        #region Fields
        private int _Id;
        private string _Name;
        private string _Tag;
        private List<Player> _Players = new List<Player>();
        private int _Villages;

        private int _Points;
        private int _AllPoints;
        private int _Rank;
        private Tribe _previousTribeData;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Tribal Wars Database ID
        /// </summary>
        [Browsable(false)]
        public int ID
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// Gets or sets the tribe name
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// Gets or sets the tribe tag
        /// </summary>
        public string Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        /// <summary>
        /// Gets or sets the list of players in the tribe
        /// </summary>
        public List<Player> Players
        {
            get { return _Players; }
            set { _Players = value; }
        }

        /// <summary>
        /// Gets or sets the total amount of villages in the tribe
        /// </summary>
        public int Villages
        {
            get { return _Villages; }
            set { _Villages = value; }
        }

        /// <summary>
        /// Gets or sets the points of the top 40 players in the tribe
        /// </summary>
        public int Points
        {
            get { return _Points; }
            set { _Points = value; }
        }

        /// <summary>
        /// Gets or sets the points of all players in the tribe
        /// </summary>
        public int AllPoints
        {
            get { return _AllPoints; }
            set { _AllPoints = value; }
        }

        /// <summary>
        /// Gets or sets the rank of the tribe
        /// </summary>
        public int Rank
        {
            get { return _Rank; }
            set { _Rank = value; }
        }

        /// <summary>
        /// Gets the average points per player
        /// </summary>
        [Browsable(false)]
        public int AveragePointsPerTribe
        {
            get { return (int)(AllPoints / Players.Count); }
        }

        /// <summary>
        /// Gets the Tribe details of the previous downloaded data
        /// </summary>
        public Tribe PreviousTribeDetails
        {
            get { return _previousTribeData; }
        }

        /// <summary>
        /// Gets a string describing the amount of players
        /// that left and joined the tribe since the last
        /// data download
        /// </summary>
        public string PlayerDifferenceString
        {
            get
            {
                if (PreviousTribeDetails == null) return null;

                int gained = 0;
                foreach (Player vil in Players)
                {
                    if (!PreviousTribeDetails.Players.Contains(vil))
                        gained++;
                }
                int lost = 0;
                foreach (Player vil in PreviousTribeDetails.Players)
                {
                    if (!Players.Contains(vil))
                        lost++;
                }
                if (lost == 0 && gained == 0) return null;
                return string.Format("+{0}-{1}", gained.ToString(), lost.ToString());
            }
        }

        /// <summary>
        /// Gets a string describing the tribe
        /// </summary>
        public string Tooltip
        {
            get
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("Name: {0}", _Name);
                str.AppendLine();
                str.AppendFormat("Rank: {0}", _Rank.ToString());
                str.AppendLine();
                str.AppendFormat("Points: {0}", _AllPoints.ToString("#,0"));
                str.AppendLine();
                str.AppendFormat("Players: {0}", _Players.Count.ToString());

                return str.ToString();
            }
        }
        #endregion

        #region Constructors
        internal Tribe(string[] pAlly)
        {
            //$id, $name, $tag, $members, $villages, $points, $all_points, $rank
            int.TryParse(pAlly[0], out _Id);
            _Name = System.Web.HttpUtility.UrlDecode(pAlly[1]);
            _Tag = System.Web.HttpUtility.UrlDecode(pAlly[2]);
            int.TryParse(pAlly[4], out _Villages);
            int.TryParse(pAlly[5], out _Points);
            int.TryParse(pAlly[6], out _AllPoints);
            int.TryParse(pAlly[7], out _Rank);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the Tribe with the previous downloaded data
        /// </summary>
        public void SetPreviousDetails(Tribe tribe)
        {
            _previousTribeData = tribe;
        }

        public override string ToString()
        {
            return string.Format("{0}", this.Tag);
        }

        public string BBCodeSimple()
        {
            return string.Format("[ally]{0}[/ally]", Tag);
        }

        public string BBCode()
        {
            return string.Format("[b]{5}[/b] [ally]{0}[/ally]{3}Points: {2:#,0}pts ({6:#,0}avg){3}Players: {1}{3}Rank: {4}", Tag, Players.Count, AllPoints, Environment.NewLine, Rank, Name, AveragePointsPerTribe);
        }

        public string BBCodeExtended()
        {
            return BBCodeExtended(0);
        }

        public string BBCodeExtended(int minFilter)
        {
            StringBuilder str = new StringBuilder(100);
            str.AppendLine(string.Format("{0}{1}{1}[b]Player Details[/b]", BBCode(), Environment.NewLine));
            Players.Sort();
            foreach (Player tribe in Players)
            {
                str.Append(string.Format("{0} [quote={1}]", tribe.BBCode(), tribe.Name));

                List<Village> villages = tribe.Villages.FindAll(delegate(Village vil) { return vil.Points > minFilter; });
                if (villages.Count > 0)
                {
                    villages.Sort();
                    foreach (Village vil in villages)
                    {
                        str.AppendLine(vil.BBCode());
                    }
                }
                else
                {
                    str.Append("*DEAD*");
                }
                str.AppendLine("[/quote]");
            }
            return str.ToString();
        }

        /// <summary>
        /// Adds a player to the tribe
        /// </summary>
        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }
        #endregion

        #region IEquatable<Tribe> Members
        public bool Equals(Tribe other)
        {
            if ((object)other == null) return false;
            return _Tag == other.Tag;
        }

        public int CompareTo(Tribe other)
        {
            return other.AllPoints - _AllPoints;
        }

        public override int GetHashCode()
        {
            return AllPoints.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return Equals(obj as Tribe);
        }

        public static bool operator ==(Tribe left, Tribe right)
        {
            if (ReferenceEquals(left, right)) return true;
            if ((object)left == null || (object)right == null) return false;
            return left.Tag == right.Tag;

        }

        public static bool operator !=(Tribe left, Tribe right)
        {
            return !(left == right);
        }
        #endregion

        #region IEnumerable<Village> Members
        public IEnumerator<Village> GetEnumerator()
        {
            foreach (Player ply in _Players)
            {
                foreach (Village vil in ply.Villages)
                    yield return vil;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}