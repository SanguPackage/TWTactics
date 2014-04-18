using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

using System.Text.RegularExpressions;
using TribalWars.Data.Villages;
using TribalWars.Data.Tribes;
using System.Windows.Forms;

namespace TribalWars.Data.Players
{
    /// <summary>
    /// Represents a Tribal Wars player
    /// </summary>
    public class Player : IComparable<Player>, IEquatable<Player>, IEnumerable<Village>
    {
        // add a monitor variable here :)
        // parse report: calculate when sent and it it for graph display here

        #region Fields
        private int _Id;
        private string _Name;
        private Tribe _Tribe;
        internal int _TribeID;
        private List<Village> _Villages = new List<Village>();

        private int _Points;
        private int _Rank;
        private Player _previousPlayerDetails;
        private List<Village> _lostVillages;
        private List<Village> _gainedVillages;

        private string _comments;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the comments on the village
        /// </summary>
        public string Comments
        {
            get { return _comments; }
            set
            {
                if (_comments != value)
                {
                    _comments = value;
                    Save();
                }
            }
        }

        /// <summary>
        /// Gets the villages lost since the data download
        /// </summary>
        public List<Village> LostVillages
        {
            get { return _lostVillages; }
        }

        /// <summary>
        /// Gets the villages gained since the data download
        /// </summary>
        public List<Village> GainedVillages
        {
            get { return _gainedVillages; }
        }

        /// <summary>
        /// Gets or sets the Tribal Wars Database ID
        /// </summary>
        public int ID
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// Gets or sets the player name
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// Gets or sets the player Tribe
        /// </summary>
        public Tribe Tribe
        {
            get { return _Tribe; }
            set { _Tribe = value; }
        }

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
        public List<Village> Villages
        {
            get { return _Villages; }
            set { _Villages = value; }
        }

        /// <summary>
        /// Gets or sets the total points of the player
        /// </summary>
        public int Points
        {
            get { return _Points; }
            set { _Points = value; }
        }

        /// <summary>
        /// Gets or sets the rank of the player
        /// </summary>
        public int Rank
        {
            get { return _Rank; }
            set { _Rank = value; }
        }

        /// <summary>
        /// Gets the average points per village of the player
        /// </summary>
        public int AveragePointsPerVillage
        {
            get { return (int)(Points / Villages.Count); }
        }

        /// <summary>
        /// Gets the Player details of the previous downloaded data
        /// </summary>
        public Player PreviousPlayerDetails
        {
            get { return _previousPlayerDetails; }
        }

        /// <summary>
        /// Gets the amount of conquered and lost villages
        /// </summary>
        public string ConquerString
        {
            get
            {
                if (PreviousPlayerDetails == null) return null;
                if (_gainedVillages == null) CalculateConquers();
                if (_gainedVillages.Count == 0 && (_lostVillages == null) || _lostVillages.Count == 0) return null;
                return string.Format("+{0}-{1}", _gainedVillages.Count.ToString(), _lostVillages.Count.ToString());
            }
        }

        private void CalculateConquers()
        {
            _gainedVillages = new List<Village>();
            foreach (Village vil in Villages)
            {
                if (!PreviousPlayerDetails.Villages.Contains(vil))
                    _gainedVillages.Add(vil);
            }
            _lostVillages = new List<Village>();
            foreach (Village vil in PreviousPlayerDetails.Villages)
            {
                if (!Villages.Contains(vil))
                    _lostVillages.Add(vil);
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
                if (_previousPlayerDetails == null) return false;

                return (_previousPlayerDetails.Tribe != null && !_previousPlayerDetails.Tribe.Equals(Tribe))
                         || (Tribe != null && !Tribe.Equals(_previousPlayerDetails.Tribe));
            }
        }

        /// <summary>
        /// Gets a string describing the player
        /// </summary>
        public string Tooltip
        {
            get
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("Points: {0}", _Points.ToString("#,0"));
                str.AppendLine();
                str.AppendFormat("Villages: {0}", _Villages.Count.ToString());
                string conquer = ConquerString;
                if (conquer != null) str.AppendFormat(" ({0})", conquer);
                if (_Tribe != null)
                {
                    str.AppendLine();
                    str.AppendFormat("Tribe: {0} (Rank: {1})", _Tribe.Tag, _Tribe.Rank.ToString());
                }

                return str.ToString().Trim();
            }
        }
        #endregion

        #region Constructors
        public Player(Village vil)
        {
            if (vil != null && vil.HasPlayer)
            {
                Player p = vil.Player;
                this._Id = p.ID;
                this._Name = p.Name;
                this._Points = p.Points;
                this._Rank = p.Rank;
                this._Tribe = p.Tribe;
                this._Villages = p.Villages;
            }
        }

        internal Player(string[] pPlayer)
        {
            //$id, $name, $ally, $villages, $points, $rank
            int.TryParse(pPlayer[0], out this._Id);
            this._Name = System.Web.HttpUtility.UrlDecode(pPlayer[1]);
            int.TryParse(pPlayer[2], out this._TribeID);
            int.TryParse(pPlayer[4], out this._Points);
            int.TryParse(pPlayer[5], out this._Rank);
        }
        #endregion

        #region BBCode
        public override string ToString()
        {
            return string.Format("{0} ({1:#,0}pts|{2}vils)", this.Name, this.Points, this.Villages.Count);
        }

        public virtual string BBCode()
        {
            string str;
            str = string.Format("[player]{0}[/player]", Name);
            if (Villages.Count > 1) str += string.Format(" ({0:#,0}pts|{1}vils)", Points, Villages.Count);
            return str;
        }

        public virtual string BBCodeExtended()
        {
            return BBCodeExtended(0, null, true);
        }

        public string BBCodeExtended(bool showTribe)
        {
            return BBCodeExtended(0, null, showTribe);
        }

        public string BBCodeExtended(Village selectedVillage)
        {
            return BBCodeExtended(0, selectedVillage, true);
        }

        public string BBCodeExtended(int minFilter)
        {
            return BBCodeExtended(minFilter, null, true);
        }

        public string BBCodeExtended(int minFilter, Village selectedVillage)
        {
            return BBCodeExtended(minFilter, selectedVillage, true);
        }

        public string BBCodeExtended(int minFilter, bool showTribe)
        {
            return BBCodeExtended(minFilter, null, showTribe);
        }

        public string BBCodeExtended(int minFilter, Village selectedVillage, bool showTribe)
        {
            List<Village> villages = Villages.FindAll(delegate(Village vil) { return vil.Points > minFilter; });

            // Build it
            StringBuilder str = new StringBuilder(100);
            str.Append(BBCode());
            if (HasTribe && showTribe) str.Append(Environment.NewLine + string.Format("{0}", Tribe.BBCode()));
            if (villages.Count > 1) str.Append(Environment.NewLine + "[b]Villages[/b][quote]");
            villages.Sort();
            bool IsFirst = true;
            foreach (Village vil in villages)
            {
                if (!IsFirst || Villages.Count == 1) str.AppendLine();
                else IsFirst = false;
                if (selectedVillage != null && vil == selectedVillage)
                {
                    str.AppendFormat("---> {0} <---", vil.BBCode());
                }
                else
                {
                    str.Append(vil.BBCode());
                }
            }
            if (villages.Count > 1) str.Append("[/quote]");
            return str.ToString();
        }

        public string BBCodeMatt()
        {
            //List<Village> villages = Villages.FindAll(delegate(Village vil) { return vil.Points > minFilter; });

            // Build it
            StringBuilder str = new StringBuilder(100);
            str.Append("[b]");
            if (HasTribe)
            {
                str.AppendFormat("[ally]{0}[/ally] Target: {1}", Tribe.Tag, BBCode());
            }
            else
            {
                str.Append(BBCode());
            }

            //http://nl.twstats.com/image.php?type=playerssgraph&id=661959&s=nl10&graph=points
            str.AppendLine();
            string link = string.Format(World.Default.TWStats.Player, ID.ToString());
            str.AppendFormat("[url={0}]TWStats Link[/url]", link);

            str.AppendLine();
            str.AppendLine();

            link = string.Format(World.Default.TWStats.PlayerGraph, ID.ToString(), World.TWStatsLinks.Graphs.points.ToString());
            str.AppendFormat("[img]{0}[/img]", link);

            link = string.Format(World.Default.TWStats.PlayerGraph, ID.ToString(), World.TWStatsLinks.Graphs.rank.ToString());
            str.AppendFormat("[img]{0}[/img]", link);

            str.AppendLine();

            link = string.Format(World.Default.TWStats.PlayerGraph, ID.ToString(), World.TWStatsLinks.Graphs.odd.ToString());
            str.AppendFormat("[img]{0}[/img]", link);

            link = string.Format(World.Default.TWStats.PlayerGraph, ID.ToString(), World.TWStatsLinks.Graphs.villages.ToString());
            str.AppendFormat("[img]{0}[/img]", link);

            str.AppendLine();
            str.AppendLine();
            //if (HasTribe && showTribe) str.Append(Environment.NewLine + string.Format("{0}", Tribe.BBCode()));
            //if (Villages.Count > 1) str.Append(Environment.NewLine + "[b]Villages[/b][quote]");
            Villages.Sort(new Village.VillageComparer());
            int cnt = 0;
            string[] players = { }; // "rogier1986;edgile;kezmania;floris 5;ruuuler;belgium4ever;hoendroe;sjarlowitsky;unusually talented".Split(';');
            int seperator = 0;
            if (players.Length > 0)
                seperator = System.Convert.ToInt32(Math.Floor((decimal)Villages.Count / players.Length));

            int currentPlayer = -1;
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
                str.AppendFormat("{0} {1}: ", cnt.ToString(), vil.BBCode());

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
        /// Saves the player to disk
        /// </summary>
        public void Save()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the Tribe of the player
        /// </summary>
        public void SetTribe(Tribe ally)
        {
            Tribe = ally;
        }

        /// <summary>
        /// Sets the Player with the previous downloaded data
        /// </summary>
        public void SetPreviousDetails(Player player)
        {
            _previousPlayerDetails = player;
            CalculateConquers();
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
            return other._Points - this._Points;
        }

        public bool Equals(Player other)
        {
            if ((object)other == null) return false;
            return _Name == other._Name;
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
            foreach (Village vil in _Villages)
                yield return vil;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}