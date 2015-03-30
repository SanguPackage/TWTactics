using System.Collections.Generic;
using System.ComponentModel;
using TribalWars.Villages;

namespace TribalWars.Tools.PropertyGrid.Villages
{
    /// <summary>
    /// Descriptor used from the ExtendedVillageDescriptor
    /// </summary>
    [TypeConverter(typeof(PropertySorter)), Editor(typeof(VillagePointerUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class VillagePlayerDescriptor : IEnumerable<Village>
    {
        #region Constants
        protected const string PROPERTY_CATEGORY = "Player";
        #endregion

        #region Fields
        public Player Player;
        #endregion

        #region Constructors
        public VillagePlayerDescriptor(Player ply)
        {
            Player = ply;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            var conquer = Player.ConquerString;
            if (!string.IsNullOrWhiteSpace(conquer))
                return string.Format("{0} ({1})", Player.Name, conquer);

            return Player.Name;
        }
        #endregion

        #region Properties
        [Category(PROPERTY_CATEGORY), PropertyOrder(40)]
        public string Points
        {
            get { return Player.Points.ToString("#,0"); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), PropertyOrder(50)]
        public string Rank
        {
            get { return Player.Rank.ToString("#,0"); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), PropertyOrder(60)]
        public string Villages
        {
            get { return Player.Villages.Count.ToString(); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), PropertyOrder(65), Editor(typeof(ClipboardCopierUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string BBCode
        {
            get { return Player.BbCode(); }
            set { }
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
