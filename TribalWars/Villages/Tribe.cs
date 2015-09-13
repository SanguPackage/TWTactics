using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using TribalWars.Controls;
using TribalWars.Maps;
using TribalWars.Tools;
using TribalWars.Villages.Helpers;

namespace TribalWars.Villages
{
    /// <summary>
    /// Represents a Tribal Wars Tribe
    /// </summary>
    public class Tribe : IVisible, IEquatable<Tribe>, IComparable<Tribe>, IEnumerable<Village>
    {
        #region Fields
        private int _id;
        private int _villages;

        private readonly int _points;
        private int _allPoints;
        private int _rank;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Tribal Wars Database ID
        /// </summary>
        [Browsable(false)]
        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the tribe name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the tribe tag
        /// </summary>
        public string Tag { get; private set; }

        /// <summary>
        /// Gets or sets the list of players in the tribe
        /// </summary>
        public List<Player> Players { get; private set; }

        /// <summary>
        /// Gets or sets the total amount of villages in the tribe
        /// </summary>
        public int Villages
        {
            get { return _villages; }
        }

        /// <summary>
        /// Gets or sets the points of all players in the tribe
        /// </summary>
        public int AllPoints
        {
            get { return _allPoints; }
        }

        /// <summary>
        /// Gets or sets the rank of the tribe
        /// </summary>
        public int Rank
        {
            get { return _rank; }
        }

        /// <summary>
        /// Gets the average points per player
        /// </summary>
        [Browsable(false)]
        public int AveragePointsPerTribe
        {
            get
            {
                if (Players.Any())
                {
                    return AllPoints / Players.Count;
                }
                return 0;
            }
        }

        /// <summary>
        /// Gets the Tribe details of the previous downloaded data
        /// </summary>
        public Tribe PreviousTribeDetails { get; private set; }

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

                int gained = Players.Count(vil => !PreviousTribeDetails.Players.Contains(vil));
                int lost = PreviousTribeDetails.Players.Count(vil => !Players.Contains(vil));
                if (lost == 0 && gained == 0) return null;
                return string.Format("+{0}-{1}", gained, lost);
            }
        }

        public string AllPointsDiff
        {
            get
            {
                string str = Common.GetPrettyNumber(AllPoints);
                if (PreviousTribeDetails != null)
                {
                    var prevPoints = PreviousTribeDetails.AllPoints;
                    if (prevPoints != 0 && prevPoints != AllPoints)
                    {
                        int dif = AllPoints - prevPoints;
                        if (dif < 0) str += " (" + Common.GetPrettyNumber(dif) + ")";
                        else str += " (+" + Common.GetPrettyNumber(dif) + ")";
                    }
                }
                return str;
            }
        }

        /// <summary>
        /// Gets a string describing the tribe
        /// </summary>
        public string Tooltip
        {
            get
            {
                var str = new StringBuilder();
				str.AppendFormat(ControlsRes.Tooltip_Name, Name);
                str.AppendLine();
				str.AppendFormat(ControlsRes.Tooltip_Rank, _rank);
                str.AppendLine();
				str.AppendFormat(ControlsRes.Tooltip_Points, AllPointsDiff);
                str.AppendLine();
				str.AppendFormat(ControlsRes.Tooltip_Tribe_Players, Common.GetPrettyNumber(Players.Count), string.IsNullOrWhiteSpace(PlayerDifferenceString) ? "" : "(" + PlayerDifferenceString + ")");

                if (Players.Count > 0)
                {
                    str.AppendLine();
                    str.AppendLine();
					str.AppendLine(ControlsRes.Tooltip_Tribe_StrongestPlayers);
                    string strongestPlayers = "";
                    foreach (var player in Players.OrderByDescending(x => x.Points).Take(5))
                    {
                        strongestPlayers += ", " + player.Name;
                    }
                    str.Append(strongestPlayers.Substring(2));
                }

                return str.ToString();
            }
        }
        #endregion

        #region Constructors
        internal Tribe(string[] pAlly)
        {
            Players = new List<Player>();
            //$id, $name, $tag, $members, $villages, $points, $all_points, $rank
            int.TryParse(pAlly[0], out _id);
            Name = System.Web.HttpUtility.UrlDecode(pAlly[1]);
            Tag = System.Web.HttpUtility.UrlDecode(pAlly[2]);
            int.TryParse(pAlly[4], out _villages);
            int.TryParse(pAlly[5], out _points);
            int.TryParse(pAlly[6], out _allPoints);
            int.TryParse(pAlly[7], out _rank);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the Tribe with the previous downloaded data
        /// </summary>
        public void SetPreviousDetails(Tribe tribe)
        {
            PreviousTribeDetails = tribe;
        }

        public override string ToString()
        {
			string str = string.Format(ControlsRes.ToString_Tribe, Tag, Common.GetPrettyNumber(AllPoints), Players.Count);
            if (Rank < 40)
            {
                str = string.Format("#{0} {1}", Rank, str);
            }
            return str;
        }

        public string BbCodeSimple()
        {
            return string.Format("[ally]{0}[/ally]", Tag);
        }

        public string BbCode()
        {
			return string.Format(ControlsRes.BbCode_Tribe, Tag, Players.Count, AllPoints, Environment.NewLine, Rank, Name, AveragePointsPerTribe);
        }

        public string BbCodeExtended()
        {
            return BbCodeExtended(0);
        }

        public string BbCodeExtended(int minFilter)
        {
            var str = new StringBuilder(100);
            str.AppendLine(string.Format("{0}{1}{1}[b]Player Details[/b]", BbCode(), Environment.NewLine));
            Players.Sort();
            foreach (Player tribe in Players)
            {
                str.Append(string.Format("{0} [quote={1}]", tribe.BbCode(), tribe.Name));

                List<Village> villages = tribe.Villages.FindAll(vil => vil.Points > minFilter);
                if (villages.Count > 0)
                {
                    villages.Sort();
                    foreach (Village vil in villages)
                    {
                        str.AppendLine(vil.BbCode());
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
            return Tag == other.Tag;
        }

        public int CompareTo(Tribe other)
        {
            return other.AllPoints - _allPoints;
        }

        public override int GetHashCode()
        {
            return AllPoints.GetHashCode();
        }

        public bool IsVisible(Map map)
        {
            return map.Display.IsVisible(this);
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
            return Players.SelectMany(ply => ply.Villages).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}