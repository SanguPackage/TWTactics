#region Using
using System.ComponentModel;
using TribalWars.Tools.PropertyGrid.Tribes;
using TribalWars.Tools.PropertyGrid.Villages;
using TribalWars.Villages;

#endregion

namespace TribalWars.Tools.PropertyGrid.Players
{
    /// <summary>
    /// Stand alone player descriptor
    /// </summary>
    [TypeConverter(typeof(PropertySorter)), DefaultProperty("Villages")]
    public class ExtendedPlayerDescriptor
    {
        #region Constants
        protected const string PROPERTY_CATEGORY = "Player";
        #endregion

        #region Fields
        public Player Player;
        #endregion

        #region Constructors
        public ExtendedPlayerDescriptor(Player ply)
        {
            Player = ply;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return Player.Name;
        }
        #endregion

        #region Properties
        [Category(PROPERTY_CATEGORY), PropertyOrder(10)]
        public string Name
        {
            get { return Player.Name; }
            set { }
        }

        [Category(PROPERTY_CATEGORY), TypeConverter(typeof(ExpandableObjectConverter)), PropertyOrder(15)]
        public VillageCollection Villages
        {
            get { return new VillageCollection(Player); }
        }

        [Category(PROPERTY_CATEGORY), PropertyOrder(20)]
        public TribeDescriptor Tribe
        {
            get
            {
                if (Player.HasTribe) return new TribeDescriptor(Player.Tribe);
                return null;
            }
            set { }
        }

        [Category(PROPERTY_CATEGORY), PropertyOrder(40)]
        public string Points
        {
            get
            {
                string str = Player.Points.ToString("#,0");
                if (Player.PreviousPlayerDetails != null)
                {
                    var prevPoints = Player.PreviousPlayerDetails.Points;
                    if (prevPoints != 0 && prevPoints != Player.Points)
                    {
                        int dif = Player.Points - prevPoints;
                        if (dif < 0) str += " (" + Common.GetPrettyNumber(dif) + ")";
                        else str += " (+" + Common.GetPrettyNumber(dif) + ")";
                    }
                }
                return str;

                //return Player.Points.ToString("#,0");
            }
            set { }
        }

        [Category(PROPERTY_CATEGORY), PropertyOrder(50)]
        public string Rank
        {
            get { return Player.Rank.ToString("#,0"); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), PropertyOrder(55), Editor(typeof(ClipboardCopierUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string BBCode
        {
            get { return Player.BbCode(); }
            set { }
        }
        #endregion
    }
}
