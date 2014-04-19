using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Drawing;
using System.ComponentModel;

using System.Text.RegularExpressions;
using TribalWars.Data.Players;
using TribalWars.Data.Units;
using TribalWars.Data.Reporting;
using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Tribes;

namespace TribalWars.Data.Villages
{
      /*      public static bool IsClickIn(Point game, Point village, int RectangleSize)
        {
            // this shouldn't really be here
            //if (game.X >= village.X && game.Y >= village.Y && game.X <= village.X + RectangleSize && game.Y <= village.Y + RectangleSize)
            if (game.X == village.X && game.Y == village.Y)
                return true;

            return false;
        }*/

    // TODO: check this:
    /*public class VillageDistanceComparer : IComparer<Village>
    {
        // I probably need to implement the IEnumerable<Village>
        // instead and take two villages as ctor input and return all that are within the maxDistance
        // this is to be used by the report village listing
        private int _MaxDistance;

        public int MaxDistance
        {
            get { return _MaxDistance; }
            set { _MaxDistance = value; }
        }

        public VillageDistanceComparor(int maxDistance)
        {
            _MaxDistance = maxDistance;
        }

        #region IComparer<Village> Members
        public int Compare(Village x, Village y)
        {
            if (x - y < _MaxDistance)
                return -1;

            return 1;
        }
        #endregion
    }*/

    /// <summary>
    /// Representation of a Tribal Wars village
    /// </summary>
    public class Village : IEnumerable<Village>, IEquatable<Village>, IComparable<Village>
    {
        #region BonusTypes
        public enum BonusType
        {
            None = 0,
            Clay,
            Iron,
            Wood,
            Res,
            Barrack,
            Farm,
            Stable,
            Workshop
        }
        #endregion

        #region Fields
        private int _Id;
        internal int _X;
        internal readonly int _Y;

        private Player _player;
        internal readonly int PlayerId;
        internal int _Points;
        private readonly Point _location;
        protected readonly int _Kingdom;
        private VillageType _type;
        private bool _typeIsSet;
        private string _tooltip;

        private VillageReportCollection _reports;
        private string _comments;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the user defined comments on the village
        /// </summary>
        public string Comments
        {
            get
            {
                if (_player == null)
                    return _comments;

                if (string.IsNullOrEmpty(_comments))
                    return _player.Comments;

                if (!string.IsNullOrEmpty(_player.Comments))
                    return string.Format("{0}{1}---------------------------------------------{1}{2}", _comments, Environment.NewLine, _player.Comments);

                return _comments;
            }
            set
            {
                // TODO BUG: enter comment, then click on a village: it is not saved
                // enter comment, click on the map on the background: it is saved
                if (_comments != value)
                {
                    _comments = value;
                    if (string.IsNullOrEmpty(value))
                        Type = _type & ~ VillageType.Comments;
                    else
                        Type = _type | VillageType.Comments;
                    Reports.Save();
                }
            }
        }

        /// <summary>
        /// Gets the collection of reports
        /// </summary>
        public VillageReportCollection Reports
        {
            get
            {
                if (_reports == null)
                {
                    _reports = new VillageReportCollection(this);
                }
                return _reports;
            }
        }

        /// <summary>
        /// Gets or sets the current name of the village
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the location of the village
        /// </summary>
        public Point Location
        {
            get { return _location; }
        }

        /// <summary>
        /// Gets or sets the owner of the village
        /// </summary>
        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        /// <summary>
        /// Gets the player ID of the village
        /// </summary>
        public int PlayerID
        {
            get { return PlayerId; }
        }

        /// <summary>
        /// Gets or sets the points of the village
        /// </summary>
        public int Points
        {
            get { return _Points; }
            set { _Points = value; }
        }

        /// <summary>
        /// Gets the bonusvillage type
        /// </summary>
        public BonusType Bonus { get; private set; }

        /// <summary>
        /// Gets the Tribal Wars Database ID of the village
        /// </summary>
        public int Id
        {
            get { return _Id; }
        }

        /// <summary>
        /// Gets the X coordinate of the village
        /// </summary>
        public int X
        {
            get { return _X; }
        }

        /// <summary>
        /// Gets the Y coordinate of the village
        /// </summary>
        public int Y
        {
            get { return _Y; }
        }

        /// <summary>
        /// Gets the Tribal Wars location string
        /// </summary>
        /// <example>X|Y</example>
        public string LocationString
        {
            get { return string.Format("{0}|{1}", _X, _Y); }
        }

        /// <summary>
        /// Gets a value indicating if the village is not abandoned
        /// </summary>
        public bool HasPlayer
        {
            get { return Player != null; }
        }

        /// <summary>
        /// Gets a value indicating if the player is in a tribe
        /// </summary>
        public bool HasTribe
        {
            get { return Player != null && Player.HasTribe; }
        }

        /// <summary>
        /// Gets the Kingdom the current village is in
        /// </summary>
        public int Kingdom
        {
            get { return _Kingdom; }
        }

        /// <summary>
        /// Gets the village details of the previous downloaded data
        /// </summary>
        public Village PreviousVillageDetails { get; private set; }

        /// <summary>
        /// Gets the user defined type of the village
        /// </summary>
        public Image TypeImage
        {
            get
            {
                switch (Type)
                {
                    case VillageType.Attack:
                        return Buildings.Images.Barracks;
                    case VillageType.Defense:
                        return Properties.Resources.Defense;
                    case VillageType.Farm:
                        return Buildings.Images.Farm;
                    case VillageType.Scout:
                        return Units.Images.Scout;
                    case VillageType.Noble:
                        return Units.Images.Noble;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the purpose of a village
        /// </summary>
        public VillageType Type
        {
            get
            {
                if (!_typeIsSet)
                {
                    _type = World.Default.GetVillageType(this);
                    _typeIsSet = true;
                }
                return _type;
            }
            set
            {
                _type = value;
                World.Default.SetVillageType(this, value);
                _typeIsSet = true;
            }
        }

        /// <summary>
        /// Gets the tooltip for the village
        /// </summary>
        public string Tooltip
        {
            get
            {
                if (_tooltip == null)
                {
                    // TODO get tooltip will be different depending on the
                    // "current map control"
                    // so this will be moved away from here after all

                    var str = new StringBuilder();

                    // Calculate previous stuff
                    int prevPoints = 0;
                    Player prevPlayer = null;
                    Tribe prevTribe = null;
                    if (PreviousVillageDetails != null)
                    {
                        prevPoints = PreviousVillageDetails.Points;
                        prevPlayer = PreviousVillageDetails.Player;
                        if (prevPlayer != null)
                        {
                            if (prevPlayer.HasTribe) prevTribe = prevPlayer.Tribe;
                        }
                    }

                    // Start output
                    str.AppendFormat("Points: {0}", Points.ToString("#,0"));
                    if (prevPoints != 0 && prevPoints != Points)
                    {
                        int dif = Points - prevPoints;
                        if (dif < 0) str.AppendFormat(" ({0})", dif.ToString("#,0"));
                        else str.AppendFormat(" (+{0})", dif.ToString("#,0"));
                    }

                    if (HasPlayer)
                    {
                        str.AppendLine();
                        str.AppendFormat("Owner: {0} ({1} points)", Player.Name, Player.Points.ToString("#,0"));
                        if (prevPlayer != null && !prevPlayer.Equals(Player))
                        {
                            str.AppendLine();
                            str.AppendFormat("Nobled from {0}", prevPlayer.Name);
                        }
                        else if (prevPlayer == null && PreviousVillageDetails != null)
                        {
                            str.AppendLine();
                            str.Append("Nobled abandoned");
                        }
                        str.AppendLine();
                        string conquers = Player.ConquerString;
                        if (string.IsNullOrEmpty(conquers))
                        {
                            str.AppendFormat("Villages: {0}", Player.Villages.Count.ToString("#,0"));
                        }
                        else
                        {
                            str.AppendFormat("Villages: {0} ({1})", Player.Villages.Count.ToString("#,0"), conquers);
                        }
                        if (HasTribe)
                        {
                            str.AppendLine();
                            str.AppendFormat("Tribe: {0} ({1} points)", Player.Tribe.Tag, Player.Tribe.AllPoints.ToString("#,0"));
                            if (prevTribe != null && !prevTribe.Equals(Player.Tribe))
                            {
                                str.AppendLine();
                                str.AppendFormat("Changed from {0}", prevTribe.Tag);
                            }
                        }
                    }
                    else
                    {
                        str.AppendLine();
                        if (prevPlayer != null)
                        {
                            str.AppendFormat("Abandoned by {0} ({1})", prevPlayer.Name, prevPlayer.Points.ToString("#,0"));
                            str.AppendLine();
                            str.AppendFormat("Villages: {0}", prevPlayer.Villages.Count.ToString(CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            str.Append("Abandoned");
                        }
                    }

                    _tooltip = str.ToString();
                }

                return _tooltip;
            }
        }
        #endregion

        #region Constructors
        internal Village(string[] pVillage)
        {
            // $id, $name, $x, $y, $tribe, $points, $rank
            int.TryParse(pVillage[0], out _Id);
            Name = System.Web.HttpUtility.HtmlDecode(System.Web.HttpUtility.UrlDecode(pVillage[1]));
            int.TryParse(pVillage[2], out _X);
            int.TryParse(pVillage[3], out _Y);
            int.TryParse(pVillage[5], out _Points);
            int.TryParse(pVillage[4], out PlayerId);
            int bonus;
            int.TryParse(pVillage[6], out bonus);
            Bonus = (BonusType)bonus;
            _Kingdom = (int)(Math.Floor((double)X / 100) + 10 * Math.Floor((double)Y / 100));
            _location = new Point(_X, _Y);
        }
        #endregion

        #region BBCode
        public override string ToString()
        {
            return string.Format("{0} ({1}|{2}) K{3}", Name, X.ToString(), Y.ToString(), Kingdom.ToString());
            //return string.Format("{0} ({1}|{2})", Name, X.ToString(), Y.ToString());
        }

        /// <summary>
        /// Returns the standard BBCode
        /// </summary>
        /// <returns>[village](X|Y)[/village]</returns>
        public string BBCodeSimple()
        {
            return string.Format("[village]({0}|{1})[/village]", X, Y);
        }

        /// <summary>
        /// Returns the BBCode with village points
        /// </summary>
        /// <returns>[village](X|Y)[/village] (pts)</returns>
        public virtual string BBCode()
        {
            return string.Format("[village]({0}|{1})[/village] ({2:#,0}pts)", X, Y, Points);
        }

        /// <summary>
        /// Returns the BBCode for the player
        /// </summary>
        public virtual string BBCodeExtended()
        {
            if (HasPlayer) return Player.BBCodeExtended(this);
            return BBCode();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the players for the village
        /// </summary>
        public void SetPlayer(Player player)
        {
            Player = player;
        }

        /// <summary>
        /// Sets the Village with the previous downloaded data
        /// </summary>
        public void SetPreviousDetails(Village village)
        {
            PreviousVillageDetails = village;
        }

        /// <summary>
        /// Initializes the comments field from the source file
        /// </summary>
        public void SetComments(string comments)
        {
            _comments = comments;
        }

        /// <summary>
        /// Load a Village from the Tribal Wars village representation
        /// </summary>
        /// <param name="input"></param>
        public static Village Parse(string input)
        {
            input = input.TrimStart('(').TrimEnd(')');
            Village vil = null;
            int index = input.IndexOf('|');
            int x, y;
            if (index > -1 && int.TryParse(input.Substring(0, index), out x) && int.TryParse(input.Substring(index + 1), out y))
            {
                World.Default.Villages.TryGetValue(new Point(x, y), out vil);
            }
            return vil;
        }

        /// <summary>
        /// Calculates the time between two villages
        /// </summary>
        /// <param name="v1">Start village</param>
        /// <param name="v2">End village</param>
        /// <param name="Unit">Unit which speed will be used</param>
        /// <returns>Required TimeSpan for a one way trip</returns>
        public static TimeSpan TravelTime(Village v1, Village v2, Unit Unit)
        {
            Point start = v1.Location;
            Point end = v2.Location;

            int x = start.X - end.X;
            int y = start.Y - end.Y;

            double distance = Math.Sqrt(x * x + y * y);
            int secs = (int)Math.Round(distance * Unit.Speed * 60);
            return new TimeSpan(0, 0, secs);
        }

        /// <summary>
        /// The village has been nobled
        /// </summary>
        /// <param name="player">New village owner</param>
        public void Nobled(Player player)
        {
            Reports.Clear();
            Player = player;
            Reports.CurrentSituation.Loyalty = 25;
            Reports.CurrentSituation.Defense.Clear();
            Type = VillageType.None;
        }
        #endregion

        #region IComparable<Village> Members
        public int CompareTo(Village other)
        {
            return other.Points - Points;
        }

        public bool Equals(Village other)
        {
            if ((object)other == null) return false;
            return (_X == other._X && _Y == other._Y);
        }

        public override int GetHashCode()
        {
            return LocationString.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return Equals(obj as Village);
        }

        public static bool operator ==(Village left, Village right)
        {
            if (ReferenceEquals(left, right)) return true;
            if ((object)left == null || (object)right == null) return false;
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Village left, Village right)
        {
            return !(left == right);
        }
        #endregion

        #region IEnumerable<Village> Members
        public IEnumerator<Village> GetEnumerator()
        {
            yield return this;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region IComparer<Village>
        public class VillageComparer : IComparer<Village>
        {
            #region IComparer<Village> Members
            public int Compare(Village x, Village y)
            {
                return x.Name.CompareTo(y.Name);
            }
            #endregion
        }
        #endregion
    }
}
