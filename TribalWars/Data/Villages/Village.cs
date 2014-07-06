using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Drawing;
using TribalWars.Data.Players;
using TribalWars.Data.Units;
using TribalWars.Data.Reporting;
using TribalWars.Data.Tribes;
using TribalWars.Tools;

namespace TribalWars.Data.Villages
{
    /// <summary>
    /// Representation of a Tribal Wars village
    /// </summary>
    public sealed class Village : IEnumerable<Village>, IEquatable<Village>, IComparable<Village>
    {
        #region BonusTypes
        public enum BonusType
        {
            // TODO: The bonustypes may not be correctly indexed?
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
        private readonly int _id;
        private readonly int _x;
        private readonly int _y;

        private readonly int _playerId;
        private readonly Point _location;
        private readonly int _kingdom;
        private VillageType _type;
        private bool _typeIsSet;

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
                if (Type.HasFlag(VillageType.Comments) && string.IsNullOrWhiteSpace(_comments))
                {
                    LoadReports();
                }
                return _comments;
            }
            set
            {
                if (_comments != value)
                {
                    _comments = value;
                    if (string.IsNullOrEmpty(value))
                        Type = _type & ~ VillageType.Comments;
                    else
                        Type = _type | VillageType.Comments;

                    Reports.SaveComments();
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
                    LoadReports();
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
        public Player Player { get; set; }

        /// <summary>
        /// Gets the player ID of the village
        /// </summary>
        public int PlayerId
        {
            get { return _playerId; }
        }

        /// <summary>
        /// Gets or sets the points of the village
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Gets the bonusvillage type
        /// </summary>
        public BonusType Bonus { get; private set; }

        /// <summary>
        /// Gets the Tribal Wars Database ID of the village
        /// </summary>
        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets the X coordinate of the village
        /// </summary>
        public int X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the Y coordinate of the village
        /// </summary>
        public int Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets the Tribal Wars location string
        /// </summary>
        /// <example>X|Y</example>
        public string LocationString
        {
            get { return string.Format("{0}|{1}", _x, _y); }
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
            get { return _kingdom; }
        }

        /// <summary>
        /// Gets the village details of the previous downloaded data
        /// </summary>
        public Village PreviousVillageDetails { get; private set; }

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
        /// Gets tooltip information
        /// </summary>
        public VillageTooltip Tooltip
        { 
            get { return new VillageTooltip(this); }
        }
        #endregion

        #region Constructors
        internal Village(string[] pVillage)
        {
            // $id, $name, $x, $y, $tribe, $points, $rank
            int.TryParse(pVillage[0], out _id);
            Name = System.Web.HttpUtility.UrlDecode(pVillage[1]);
            _x = int.Parse(pVillage[2]);
            _y = int.Parse(pVillage[3]);
            Points = int.Parse(pVillage[5]);
            _playerId = int.Parse(pVillage[4]);
            Bonus = (BonusType)int.Parse(pVillage[6]);
            _location = new Point(_x, _y);
            if (_location.IsValidGameCoordinate())
            {
                _kingdom = _location.Kingdom();
            }
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
        public string BBCode()
        {
            return string.Format("[village]({0}|{1})[/village] ({2:#,0}pts)", X, Y, Points);
        }

        /// <summary>
        /// Returns the BBCode for the player
        /// </summary>
        public string BBCodeExtended()
        {
            if (HasPlayer) return Player.BbCodeExtended(this);
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
        /// <param name="unit">Unit which speed will be used</param>
        /// <returns>Required TimeSpan for a one way trip</returns>
        public static TimeSpan TravelTime(Village v1, Village v2, Unit unit)
        {
            Point start = v1.Location;
            Point end = v2.Location;

            int x = start.X - end.X;
            int y = start.Y - end.Y;

            double distance = Math.Sqrt(x * x + y * y);
            var secs = (int)Math.Round(distance * unit.Speed * 60);
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
            return (_x == other._x && _y == other._y);
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
                return String.Compare(x.Name, y.Name, StringComparison.Ordinal);
            }
            #endregion
        }
        #endregion

        #region Private Implementation
        private void LoadReports()
        {
            _reports = new VillageReportCollection(this);
        }
        #endregion
    }
}
