using System.Collections.Generic;
using System.ComponentModel;
using TribalWars.Tools.PropertyGrid.Villages;
using TribalWars.Villages;

namespace TribalWars.Tools.PropertyGrid.Players
{
    /// <summary>
    /// Descriptor used in ExtendedTribeDescriptor
    /// </summary>
    [TypeConverter(typeof(PropertySorter)), Editor(typeof(VillagePointerUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PlayerDescriptor : IEnumerable<Village>
    {
        #region Constants
        protected const string PROPERTY_CATEGORY = "Player";
        #endregion

        #region Fields
        public Player Player;
        #endregion

        #region Constructors
        public PlayerDescriptor(Player ply)
        {
            Player = ply;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return Player.Points.ToString("#,0");
        }
        #endregion

        #region Properties
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

        [Category(PROPERTY_CATEGORY), TypeConverter(typeof(ExpandableObjectConverter)), PropertyOrder(60)]
        public VillageCollection Villages
        {
            get
            {
                return new VillageCollection(Player.Villages);
            }
        }
        #endregion

        #region IEnumerable<Village> Members
        public IEnumerator<Village> GetEnumerator()
        {
            return Player.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
